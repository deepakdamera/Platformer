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
    class Menu
    {

        // Single Player
        // Multiplayer
        // Options 
        // Exit
        // Leaderboard

        public Texture2D texture;
        public SpriteBatch spritebatch;
               public Menu(Texture2D texture2D, SpriteBatch batch)
        {
            texture = texture2D;
            spritebatch = batch;
        }
        
    }
}
