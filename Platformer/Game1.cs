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


        Menu m;

        // Initialize controller/keyboard
        GamePadState controller = GamePad.GetState(PlayerIndex.One);
        KeyboardState keyboard = Keyboard.GetState();


        Texture2D continueWithoutSaving, exit, instructions, multiplayer, newGame, returnToMainMenu, saveContinue, singePlayer, startGame, tryAgain;
        Point buttonSize = new Point(300, 75);
      
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

            // Buttons
            continueWithoutSaving = Content.Load<Texture2D>("continuewithoutsaving");
            exit = Content.Load<Texture2D>("exit");
            instructions = Content.Load<Texture2D>("instructions");
            multiplayer = Content.Load<Texture2D>("multiplayer");
            newGame = Content.Load<Texture2D>("newgame");
            returnToMainMenu = Content.Load<Texture2D>("returntomainmenu");
            saveContinue = Content.Load<Texture2D>("savecontinue");
            singePlayer = Content.Load<Texture2D>("singleplayer");
            startGame = Content.Load<Texture2D>("startgame");
            tryAgain = Content.Load<Texture2D>("tryagain");

            // m = new Menu(GraphicsDevice);



        }

        
        protected override void UnloadContent()
        {
            
        }


        public void menu()
        {
            // Sets the background color    
            GraphicsDevice.Clear(Color.Silver);
            spriteBatch.Draw(singePlayer, new Rectangle(new Point(graphics.PreferredBackBufferWidth /2-150, 190), buttonSize), Color.White);
            spriteBatch.Draw(multiplayer, new Rectangle(new Point(graphics.PreferredBackBufferWidth /2-150, 280), buttonSize), Color.White);
            spriteBatch.Draw(instructions, new Rectangle(new Point(graphics.PreferredBackBufferWidth / 2 - 150, 370), buttonSize), Color.White);
            spriteBatch.Draw(exit, new Rectangle(new Point(graphics.PreferredBackBufferWidth / 2 - 150, 460), buttonSize), Color.White);


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
            GraphicsDevice.Clear(Color.DarkRed);
            
            spriteBatch.Begin();
           
            menu();
           // opacity = drawTitle(opacity);
           // m.draw();
            //m.draw(spriteBatch);
            //spriteBatch.Draw(m.texture, TitleScreen, Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
