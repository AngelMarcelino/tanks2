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
    class Tank
    {
        public const double Acceleration = 30;
        public const double BrakingSpeed = 300;
        public const double Friction = 50;
        public const double BaseSpeed = 20;
        private double _xSpeed = 0;
        private double _ySpeed = 0;
        public double MaxSpeed = BaseSpeed;
        public double MinSpeed = -BaseSpeed;
        public const double SpeedMultiplier = 1.5;
        public const bool Vertical = true;
        public const bool Horizontal = false;
        public const double AccelerationMultiplier = 2;

        public double XSpeed
        {
            get { return _xSpeed; }
            set
            {
                _xSpeed = LimitSpeeds(value);
            }
        }

        public double YSpeed
        {
            get { return _ySpeed; }
            set
            {
                _ySpeed = LimitSpeeds(value);
            }
        }

        public double X { get; set; } = 40; 
        public double Y { get; set; } = 40;
        public double Widht { get; set; } = 40;
        public double Height { get; set; } = 40;

        public Tank(Controll controll)
        {
            this.controll = controll;
        }

        private Controll controll;

        public void UpdateState()
        {
            UpdateSpeed();
            UpdatePosition();
        }

        private double DecreaseAbsoluteSpeedToZero(double speed, double acceleration)
        {
            if (speed == 0) return 0;
            if (speed < 0)
            {
                speed = speed + acceleration;
                if (speed > 0)
                {
                    speed = 0;
                }
            }
            if (speed > 0)
            {
                speed = speed - acceleration;
                if (speed < 0)
                {
                    speed = 0;
                }
            }
            return speed;
        }

        private double LimitSpeeds(double inputSpeed)
        {
            double _speed = 0;
            if (inputSpeed < MinSpeed)
            {
                _speed = MinSpeed;
            }
            else if (inputSpeed > MaxSpeed)
            {
                _speed = MaxSpeed;
            }
            else
            {
                _speed = inputSpeed;
            }
            return _speed;
        }

        private bool IsMoving()
        {
            return Controll.MovementGamePlayActions.Any(controll.GetActionState);
        }

        private void UpdateSpeed()
        {
            double deltaX = 0;
            double deltaY = 0;
            double speedMultiplier = 1;
            if (controll.GetActionState(GameplayAction.Up))
            {
                if (YSpeed > 0)
                {
                    Brake(Direction.Vertical);
                }
                deltaY += -Acceleration * GlobalTimer.ElapsedTimeInSeconds;
            }
            if (controll.GetActionState(GameplayAction.Down))
            {
                if (YSpeed < 0)
                {
                    Brake(Direction.Vertical);
                }
                deltaY += Acceleration * GlobalTimer.ElapsedTimeInSeconds;
            }
            if (!controll.GetActionState(GameplayAction.Up) && !controll.GetActionState(GameplayAction.Down))
            {
                YSpeed = DecreaseAbsoluteSpeedToZero(YSpeed, Friction * GlobalTimer.ElapsedTimeInSeconds);
            }
            if (controll.GetActionState(GameplayAction.Left))
            {
                if (XSpeed > 0)
                {
                    Brake(Direction.Horizontal);
                }
                deltaX += -Acceleration * GlobalTimer.ElapsedTimeInSeconds;
            }
            if (controll.GetActionState(GameplayAction.Right))
            {
                if (XSpeed < 0)
                {
                    Brake(Direction.Horizontal);
                }
                deltaX += Acceleration * GlobalTimer.ElapsedTimeInSeconds;
            }
            if (!controll.GetActionState(GameplayAction.Left) && !controll.GetActionState(GameplayAction.Right))
            {
                XSpeed = DecreaseAbsoluteSpeedToZero(XSpeed, Friction * GlobalTimer.ElapsedTimeInSeconds);
            }

            if (controll.GetActionState(GameplayAction.Speed))
            {
                speedMultiplier = AccelerationMultiplier;
                MaxSpeed = BaseSpeed * SpeedMultiplier;
                MinSpeed = -BaseSpeed * SpeedMultiplier;
            }
            else
            {
                MaxSpeed = BaseSpeed;
                MinSpeed = -BaseSpeed;
            }
            
            XSpeed += deltaX * speedMultiplier;
            YSpeed += deltaY * speedMultiplier;
        }

        private void Brake(Direction direction)
        {
            if (direction == Direction.Vertical)
            {
                YSpeed = DecreaseAbsoluteSpeedToZero(YSpeed, BrakingSpeed * GlobalTimer.ElapsedTimeInSeconds);
            }
            else
            {
                XSpeed = DecreaseAbsoluteSpeedToZero(XSpeed, BrakingSpeed * GlobalTimer.ElapsedTimeInSeconds);
            }
        }

        private void UpdatePosition()
        {
            double deltaX = XSpeed;
            double deltaY = YSpeed;

            X += deltaX;
            Y += deltaY;
        }

        public Rect Shape
        {
            get
            {
                return new Rect(X, Y, Widht, Height);
            }
        }

        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            args.DrawingSession.FillRectangle(Shape, Microsoft.UI.Colors.White);
            args.DrawingSession.DrawText($"XSpeed: {XSpeed}", 40, 40, Microsoft.UI.Colors.AliceBlue);
            args.DrawingSession.DrawText($" YSpeed: {YSpeed}", 40, 80, Microsoft.UI.Colors.AliceBlue);
        }
    }
}
