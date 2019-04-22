using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class HealthBar
    {

        Texture2D texture;
        Vector2 position;
        public Rectangle rectangle;
        public int health { get; set; }

        public HealthBar(Texture2D tex ,Vector2 newPos, int newHealth)
        {
            texture = tex;
            position = newPos;
            health = newHealth;
        }

        public void Update()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (health>0)
            {
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
        }



    }
}
