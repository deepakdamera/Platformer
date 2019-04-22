using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Tile
    {

        public static Texture2D Texture { get; set; }
        public Vector2 position;
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        }
        public static void LoadContent(ContentManager content, int type)
        {
           

            Texture = content.Load<Texture2D>("Tile");

        }

        public Tile(Vector2 position)
        {
            this.position = position;
        }

        public void Update(float Xtrans)
        {
            position.X -= Xtrans;
        }

      

    }

    // blocks modification of location of blocks
    // credit 1 
    static class Shared
    {
        public static readonly Random random = new Random();

    }
}
