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
    class Tank : ICollisionable
    {
        
        public Position Position { get; private set; }

        public Movable movable { get; private set; }

        public double Width { get; set; } = 40;
        public double Height { get; set; } = 40;

        public int FireRateInRoundsPerMinute { get; set; } = 300;

        public double RoundsPerSecond { get => FireRateInRoundsPerMinute / 60; }

        public double TimeBetweenRounds { get => 1 / RoundsPerSecond;  }

        private double LastFire = 0;

        private const double ProjectileSpeed = 500;

        private Action<Projectile> createProjectile;

        public Tank(Controll controll, Rect space, Action<Projectile> createProjectile)
        {
            this.controll = controll;
            this.Position = new Position(space)
            {
                X = 40,
                Y = 40
            };
            this.movable = new Movable(Position);
            this.createProjectile = createProjectile;
        }

        private Controll controll;

        public void UpdateState()
        {
            movable.UpdateState(
                controll.GetActionState(GameplayAction.Up),
                controll.GetActionState(GameplayAction.Down),
                controll.GetActionState(GameplayAction.Left),
                controll.GetActionState(GameplayAction.Right)
            );
            Fire();
        }

        private void Fire()
        {
            if (LastFire != 0 && GlobalTimer.TotalTimeInSecods - LastFire <= TimeBetweenRounds)
            {
                return;
            }
            bool fired = false;
            if (controll.GetActionState(GameplayAction.FireUp))
            {
                createProjectile(new Projectile(Position.X, Position.Y, 0, -ProjectileSpeed));
                fired = true;
            }
            if (controll.GetActionState(GameplayAction.FireDown))
            {
                createProjectile(new Projectile(Position.X, Position.Y, 0, ProjectileSpeed));
                fired = true;
            }
            if (controll.GetActionState(GameplayAction.FireLeft))
            {
                createProjectile(new Projectile(Position.X, Position.Y, -ProjectileSpeed, 0));
                fired = true;
            }
            if (controll.GetActionState(GameplayAction.FireRight))
            {
                createProjectile(new Projectile(Position.X, Position.Y, ProjectileSpeed, 0));
                fired = true;
            }
            if (fired)
            {
                LastFire = GlobalTimer.TotalTimeInSecods;
            }
        }

        public Rect Shape
        {
            get
            {
                return new Rect(Position.X, Position.Y, Width, Height);
            }
        }

        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            args.DrawingSession.FillRectangle(Shape, Microsoft.UI.Colors.White);
            args.DrawingSession.DrawText($"XSpeed: {movable.XSpeed}", 40, 40, Microsoft.UI.Colors.AliceBlue);
            args.DrawingSession.DrawText($"YSpeed: {movable.YSpeed}", 40, 80, Microsoft.UI.Colors.AliceBlue);
            args.DrawingSession.DrawText($" FPS: {1 / GlobalTimer.ElapsedTimeInSeconds }", 40, 120, Microsoft.UI.Colors.AliceBlue);
        }
    }
}
