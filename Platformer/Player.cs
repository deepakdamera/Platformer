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

    protected Vector2 _position;

    protected Texture2D _texture;

    #endregion

    #region Properties

 

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

    public float Speed = 2.5f;

    public Vector2 Velocity;


     // x co-ordinate movement
     // using this var for moving background along with the character
    public int Xtrans=0;

    #endregion

    #region Methods

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      if (_texture != null)
        spriteBatch.Draw(_texture, Position, Color.White);
      else if (_animationManager != null)
        _animationManager.Draw(spriteBatch);
      else throw new Exception("error animation mngr");
    }

    public virtual void Move()
    {
      if (Keyboard.GetState().IsKeyDown(Keys.Up))
        Velocity.Y = -Speed;
      
      else if (Keyboard.GetState().IsKeyDown(Keys.Down));
       // Velocity.Y = Speed
      else if (Keyboard.GetState().IsKeyDown(Keys.Left)&&Position.X>50)
        Velocity.X = -Speed;
      else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Velocity.X = Speed;
               
            }
        
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
      else _animationManager.Stop();
    }

    public Player(Dictionary<string, Animation> animations)
    {
      _animations = animations;
      _animationManager = new AnimationManager(_animations.First().Value);
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

       Xtrans = (int) Velocity.X;

      Velocity = Vector2.Zero;

       
    }

    #endregion
  
    }
}