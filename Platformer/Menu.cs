using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{

   

    class Menu : Game
    {

        // Single Player
        // Multiplayer
        // Options 
        // Exit
        // Leaderboard
        protected override void LoadContent()
        {
            Texture2D singeplayer = Content.Load<Texture2D>("singleplayer");
            Texture2D multiplayer = Content.Load<Texture2D>("multiplayer");
            Texture2D instructions = Content.Load<Texture2D>("instructions");
            Texture2D exit = Content.Load<Texture2D>("exit");
        }

        public Texture2D texture;
        public SpriteBatch spritebatch;
               public Menu(Texture2D texture2D, SpriteBatch batch)
        {
            texture = texture2D;
            spritebatch = batch;
        }
        
    }
}
