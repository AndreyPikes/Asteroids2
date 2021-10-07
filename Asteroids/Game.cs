using Asteroids.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {

        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        static BaseObject[] asteroids;
        static BaseObject[] stars;
        static BaseObject[] _rockets;
        static Bullet bullet;

        public static int Width { get; set; }
        public static int Height { get; set; }


        public static void Init(Form form)
        {
            _context = BufferedGraphicsManager.Current;
            Graphics g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            if (Width > 1000 || Width <= 0 || Height > 1000 || Height <= 0) throw new ArgumentOutOfRangeException();

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            // Фон
            //Buffer.Graphics.FillRectangle(Brushes.Blue, new Rectangle(0, 0, Width, Height));
            Buffer.Graphics.DrawImage(Resources.background, new Rectangle(0, 0, Width, Height));

            foreach (var star in stars)
            {
                star.Draw();
            }

            // Планета
            //Buffer.Graphics.FillEllipse(Brushes.Red, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.DrawImage(Resources.planet, new Rectangle(100, 100, 200, 200));

            foreach (var asteroid in asteroids)
            {
                asteroid.Draw();
            }

            foreach (var rocket in _rockets)
            {
                rocket.Draw();
            }

            bullet.Draw();

            Buffer.Render();
        }

        public static void Update()
        {
            foreach (var star in stars)
            {
                star.Update();
            }

            foreach (var asteroid in asteroids)
            {
                asteroid.Update();

                if (asteroid.Collision(bullet))
                {
                    bullet.SetOnStartPosition();
                    if (asteroid is Asteroid) (asteroid as Asteroid).SetOnOppositePosition(Width);
                }
            }

            bullet.Update();
        }


        public static void Load()
        {
            Random random = new Random();
            asteroids = new BaseObject[10];

            bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(30, 30));

            for (int i = 0; i < asteroids.Length; i++)
            {
                var size = random.Next(10, 40);
                asteroids[i] = new Asteroid(new Point(600 - size * 10, i * 20 + size), new Point(-i - 3, -i - 3), new Size(size, size));
            }

            stars = new BaseObject[10];
            for (int i = 0; i < stars.Length; i++)
            {
                var size = random.Next(20, 30);
                stars[i] = new Star(new Point(600 - size * 20, i * 40 + 15), new Point(i + 1, i + 1), new Size(20, 20));
            }

            _rockets = new BaseObject[1];
            _rockets[0] = new Rocket(new Point(0, Height / 2 - 50), new Point(0, 0), new Size(100, 100));
        }
    }
}
