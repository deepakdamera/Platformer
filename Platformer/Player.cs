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


        GraphicsDeviceManager graphics;

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        public Vector2 _position;

        public bool Contact = false;

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
        public float Speed = 1f;


        public Vector2 Velocity;

        public Vector2 Acceleration = new Vector2(9.8f,0);


        Boolean hasJumped = false;

        public Boolean jumping = false;

        int jumpCount = 0;

        Boolean falling = false;

        public bool grounded = false;

        // x co-ordinate movement
        // using this var for moving background along with the character
        public int Xtrans = 0;

        #endregion

        #region Methods

        public void Gravity()
            {
                ;
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

            if(Keyboard.GetState().IsKeyDown(Keys.E))
            {

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && _position.X > 50)
                Velocity.X = -Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) )

            {
                Velocity.X = Speed;

            }

            // Jump
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && grounded)
            {
                //Velocity.X = 15f;
                //_position.Y -= 170f;
                // Velocity.Y = 3f*10;
                if (jumpCount == 0)
                    jumpCount = 75;
                jumping = true;
                grounded = false;
                hasJumped = true;
                //hasJumped = true;

            }

            if (jumpCount > 0)
            {
               
                    jumpCount--;
                // jump speed
                _position.Y -= 3.5f;

                if (jumpCount == 0)
                {
                    
                    hasJumped = false;
                    jumping = false;
                }
            }                   

          
          
            /*if (_position.Y < (.7) * GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                _position.Y ++;*/
            if (hasJumped == false)
            {
               // Velocity.Y = 0f;
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
        

    public Player(Dictionary<string, Animation> animations, GraphicsDeviceManager g)
    {
      graphics = g;
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
      
            // Removed for testing
      Position += Velocity;

            Xtrans = (int)Velocity.X;
            // Testing movement
            //Xtrans = (int)_position.X;
            _prevPos = Position;



            if (_position.X > graphics.PreferredBackBufferWidth/2)
            {
                _position.X= (float)(graphics.PreferredBackBufferWidth / 2);



            Velocity = Vector2.Zero;

       
    }

        public bool IsTouching(Tile tile,Player sprite
            )
        {

            return _position.X +25f +this.Velocity.X >= tile.
                position.X && this._position.Y < tile.position.Y  && this._position.X + this.Velocity.X +10f<= tile.

          
                position.X+Tile.Texture.Width;

                
             
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