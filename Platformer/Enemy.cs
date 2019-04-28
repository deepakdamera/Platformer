using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Enemy
    {


        #region Fields

        SpriteEffects se;


        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;


        public Vector2 _position;

        private Boolean isAttacking;
        public Boolean IsAttacking { get { return isAttacking; }set { isAttacking = value; } }
        
        public Texture2D _texture;

        public bool facingRight;

        public int screenWidth =GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        //  velocity of the enemy
        public Vector2 Velocity;


        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }


        public float Speed = 3f;

        public Vector2 Velocity;

        public Vector2 Acceleration = new Vector2(9.8f, 0);


        // x co-ordinate movement
        // using this var for moving background along with the character
        public int Xtrans = 0;

        #endregion
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D walker;
        Rectangle walker2;
        #region Methods


        public Enemy(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Enemy(Texture2D texture)
        {
            _texture = texture;
           
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, _position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("error animation mngr");
        }


        public virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && _position.X > 50)
                Velocity.X = -Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))

            {
                Velocity.X = Speed;

            } 

        }


        protected virtual void SetAnimations()
        {

            if (isAttacking)
            {
                if (!facingRight)
                {
                    _animationManager.Play(_animations["enemyattackR"]);
                }
                else
                {
                    _animationManager.Play(_animations["enemyattackL"]);
                }
               
               
            }
            
           else  if (Velocity.X > 0 &&!isAttacking)
                _animationManager.Play(_animations["enemywalkR"]);
            else if (Velocity.X < 0 &&!isAttacking)

            {
                _animationManager.Play(_animations["enemywalkL"]);

            }
            else _animationManager.Stop();
        }



        public Enemy(Dictionary<string, Animation> animations, GraphicsDeviceManager g)
        {
            graphics = g;
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Enemy(Texture2D texture)
        {
            _texture = texture;
        }



        public virtual void Update(GameTime gameTime, List<Player> sprites)
        {
            Move();

            SetAnimations();

            _animationManager.Update(gameTime);

            Position += Velocity;

            Xtrans = (int)Velocity.X;
            _prevPos = Position;


            if (_position.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2)
            {
                _position.X = (float)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2);
            }

            Velocity = Vector2.Zero;


        }

        public bool IsTouching(Tile tile, Player sprite
            )
        {

            return _position.X + 25f + this.Velocity.X >= tile.
                position.X && this._position.Y < tile.position.Y && this._position.X + this.Velocity.X + 10f <= tile.


                position.X + Tile.Texture.Width;



        }

        // Returns true if player is on top the tile, false otherwise
        public bool tileTouching(Tile tile, Player player)
        {
            // Checks if the player is in bounds horizontally 
            if ((player._position.X >= tile.position.X) && (player._position.X <= tile.position.X + Tile.Texture.Width))
                // Checks if the player is at the right height 
                if ((player._position.Y <= tile.position.Y))
                    return true;
                else
                    return false;
            else
                return false;
        }

        #endregion


        private void RandomMove(Player player)
        {
            Random r = new Random();
            //&& this.Position.X > screenWidth / 2 && this.Position.X > screenWidth / 3
            if (r.Next(0, 50) > 15 && !isAttacking)
            {
                if (player._position.X - this.Position.X > this._animations.ElementAt(0).Value.FrameWidth
                    ) { 

                this.Velocity.X = 1f;
                facingRight = true; }
            
            else if (player._position.X - this.Position.X < 0)
                {
                    this.Velocity.X = -1f;
                    facingRight = true;
                }
                


            }

            if (!IsAttacking)
            {
                this.Attack(player);
            }
        }

        public virtual void Update(GameTime gameTime, Player player)
        {

            
            if (!IsAttacking)
            {
                RandomMove(player);
                Position += Velocity;
            }
            
            

            SetAnimations();

            _animationManager.Update(gameTime);
            
       
            Velocity = Vector2.Zero;


        }
        private void Attack
            (Player player)
        {
            if ((this._position.X-player._position.X)<100 && (this._position.X - player._position.X) >0&& player.IsAlive)
            {
               // this.Velocity = Vector2.Zero;
                this.isAttacking=true;
                player.Health = player.Health-34;
            player.CheckHealth();
            }
            else if ((this._position.X - player._position.X)< 100 && (this._position.X - player._position.X) > 0 && player.IsAlive)
            {

            }
            
        }

    }
}
