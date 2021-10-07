using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Rocket : Asteroid
    {
        public Rocket(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            
        }
        public override void SetImages()
        {
            images = new Image[1] { Resources.rocket };
        }
    }
}
