using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Platformer
{
    public class Animation
    {
       

        public int CurrFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public int FrameSpeed { get; set; }
        public int FrameWidth { get { return Texture.Width / FrameCount; } } 
        public bool isLooping { get; set; }

        public Texture2D Texture { get; private set; }
        public Animation( Texture2D texture, int frameCount)
        {
            Texture = texture;
            FrameCount = frameCount;
            isLooping = true;

            FrameSpeed = (int) 1f;

        }

    }


}
