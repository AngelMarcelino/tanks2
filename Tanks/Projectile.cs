using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tanks.Controlls;
using Tanks.Physics;
using Windows.Foundation;
using Windows.Media.Devices.Core;

namespace Tanks
{
    class Projectile
    {
        public bool ToRemove { get; set; } = false;
        public Position position { get; private set; }

        public Movable movable { get; private set; }

        public double Widht { get; set; } = 5;
        public double Height { get; set; } = 5;

        public Projectile(double x, double y, double xSpeed, double ySpeed)
        {
            this.position = new Position(new Rect(0, 0, 1200, 900))
            {
                X = x,
                Y = y
            };
            this.movable = new Movable(position)
            {
                Acceleration = 0,
                BaseSpeed = 10000,
                Friction = 10,
                XSpeed = xSpeed,
                YSpeed = ySpeed
            };
        }

        public void UpdateState()
        {
            var previousX = position.X;
            var previousY = position.Y;
            movable.UpdateState();
            if (position.X == previousX && position.Y == previousY)
            {
                ToRemove = true;
            }
        }

        public Rect Shape
        {
            get
            {
                return new Rect(position.X, position.Y, Widht, Height);
            }
        }

        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            args.DrawingSession.FillRectangle(Shape, Microsoft.UI.Colors.Green);
        }
    }
}
