using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Enemy
    {

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
