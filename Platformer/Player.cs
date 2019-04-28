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
    class Player
    {
        #region Fields

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        public Vector2 _position;

        protected Vector2 _prevPos;

        public Texture2D _texture;

        #endregion

        #region Properties

        private int health;
        public int Health { get { return health; } set { health = value; } }
        public int Lives  { get; set; }

        private bool isAlive;
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

        public bool IsAttacking { get; set; }




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

        public float Speed = 2f;

        public Vector2 Velocity;


        Boolean hasJumped = false;



        // x co-ordinate movement
        // using this var for moving background along with the character
        public int Xtrans = 0;

        #endregion

        #region Methods

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

            if(Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && _position.X > 50)
                Velocity.X = -Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) )

            {
                Velocity.X = Speed;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                Velocity.X = 15f;
                _position.Y -= 170f;
                Velocity.Y = 3f*10;
                hasJumped = true;

            }

            if (hasJumped == true)
            {
                float i = 10;
                Velocity.Y += 0.25f * i;




            }
            if (_position.Y > (0.858) * GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
            {
                hasJumped = false;
            }
            if (hasJumped == false)
            {
                Velocity.Y = 0f;
                //Velocity.X = 0f;


            }

            //else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            // Velocity.Y = Speed


        }

        protected virtual void SetAnimations()
        {
            if (Velocity.X > 0)
                _animationManager.Play(_animations["WalkRight"]);
            /* else if (Velocity.X < 0)
               _animationManager.Play(_animations["WalkLeft"]);
             else if (Velocity.Y > 0)
               _animationManager.Play(_animations["WalkDown"]);
             else if (Velocity.Y < 0)
               _animationManager.Play(_animations["WalkUp"]);*/


            else if (Velocity.X < 0)

            {
                _animationManager.Play(_animations["WalkLeft"]);
            }

            else if (IsAttacking)
            {
                _animationManager.Play(_animations["attack"]);

            }
            else _animationManager.Stop();
        }
        

    public Player(Dictionary<string, Animation> animations)
    {
      _animations = animations;
      _animationManager = new AnimationManager(_animations.First().Value);
            health = 100;
            IsAlive = true;
    }

    public Player(Texture2D texture)
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


            if (_position.X > GraphicsDeviceManager.DefaultBackBufferWidth)
            {
                _position.X= (float)(GraphicsDeviceManager.DefaultBackBufferWidth / 2);
            }

            Velocity = Vector2.Zero;

       
    }

        public bool IsTouching(Tile tile,Player sprite
            )
        {

            return _position.X +25f +this.Velocity.X >= tile.
                position.X && this._position.Y < tile.position.Y  && this._position.X + this.Velocity.X +10f<= tile.

          
                position.X+Tile.Texture.Width;
                
             
        }

       public void CheckHealth()
        {
            if (this.Health <= 0)
            {
                IsAlive = false;
            }
        }

        #endregion

    }
}