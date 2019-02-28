using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{

   

    // ??
    public class Game1 : Game
    {



        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // if you see this, progress has been made ..
        // trying git cmds
        // Title Screen 
        Texture2D titlescreen;
        Texture2D titlescreen_a;
      
        float opacity = 0f;
        int opacDirection = 1;
        Rectangle TitleScreen = new 

        // fit user's screen bounds
            Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        // Title Screen //

       // Initialize controller/keyboard
        GamePadState controller = GamePad.GetState(PlayerIndex.One);
        KeyboardState keyboard = Keyboard.GetState();

        // My first comment!
        // Comment 2 doods!
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Sets the game to 1080p fullscreen by default
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.IsFullScreen = true;

        }


        protected override void Initialize()
        {
            


            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            titlescreen = Content.Load<Texture2D>("titlescreen");
            titlescreen_a = Content.Load<Texture2D>("titlescreen(1)");

           
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            controller = GamePad.GetState(PlayerIndex.One);

            base.Update(gameTime);
        }

        public float drawTitle(float i)
        {

            spriteBatch.Draw(titlescreen, TitleScreen, Color.White);
                spriteBatch.Draw(titlescreen_a, TitleScreen, Color.White*i);
            if (i > 1f || i<0f)
                opacDirection *= -1;
            return i + .01f*opacDirection;
            
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen);
            spriteBatch.Begin();
            
            opacity = drawTitle(opacity);

            Menu m = new Menu();
            spriteBatch.Draw(m.texture, TitleScreen, Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
