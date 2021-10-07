using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        private Point startPosition;

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {            
            this.currentImage = images[0];
            this.startPosition = pos;
        }

        public void SetOnStartPosition()
        {
            pos = startPosition;
        }

        public override void SetImages()
        {
            images = new Image[1] { Resources.bullet };
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(currentImage, pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;

            if (pos.X < 0) pos.X = Game.Width;
            if (pos.X > Game.Width) pos.X = 0;

            if (pos.Y < 0) pos.Y = Game.Height;
            if (pos.Y > Game.Height) pos.Y = 0;
        }

        
    }
}
