using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AnimationManager
{
    private Animation _animation;
    private float timer;
    public Vector2 Position { get; set; }

    public AnimationManager(Animation animation)
    {
        if (_animation == animation)
            return;

        _animation = animation;
        _animation.CurrFrame = 0;
        _timer = 0;
    }

    public void Play(Animation animation)
    {
        if (_animation == animation)
            return;
        _animation = animation;
        animation.CurrFrame = 0;
        timer = 0;
    }
	private Animation()
	{
	}

    public void Stop()
    {
        timer = 0;
        _animation.CurrFrame = 0;
    }
}
