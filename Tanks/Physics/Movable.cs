using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Tanks.Controlls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Tanks.Physics
{
    internal class Movable
    {
        private Position position;
        public double Acceleration { get; set; } = 4800;
        public double BrakingSpeed { get; set; } = 4800;
        public double Friction { get; set; } = 4000;
        public double BaseSpeed { get; set; } = 720;
        public double MaxSpeed { get => BaseSpeed; }
        public double MinSpeed { get => -BaseSpeed; }

        private double _xSpeed = 0;
        private double _ySpeed = 0;

        public const double SpeedMultiplier = 1.5;
        public const bool Vertical = true;
        public const bool Horizontal = false;
        public const double AccelerationMultiplier = 2;

        public Movable(Position position)
        {
            this.position = position;
        }

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

        public void UpdateState(bool up, bool down, bool left, bool right)
        {
            UpdateSpeed(up, down, left, right);
            UpdatePosition();
        }

        public void UpdateState()
        {
            UpdateSpeed(false, false, false, false);
            UpdatePosition();
        }

        private void UpdateSpeed(bool up, bool down, bool left, bool right)
        {
            double deltaX = 0;
            double deltaY = 0;
            double speedMultiplier = 1;
            if (up)
            {
                if (YSpeed > 0)
                {
                    Brake(Orientation.Vertical);
                }
                deltaY += -Acceleration * GlobalTimer.ElapsedTimeInSeconds;
            }
            if (down)
            {
                if (YSpeed < 0)
                {
                    Brake(Orientation.Vertical);
                }
                deltaY += Acceleration * GlobalTimer.ElapsedTimeInSeconds;
            }
            if (!up && !down)
            {
                DecreaseVelocityDueFriction(Orientation.Vertical);
            }
            if (left)
            {
                if (XSpeed > 0)
                {
                    Brake(Orientation.Horizontal);
                }
                deltaX += -Acceleration * GlobalTimer.ElapsedTimeInSeconds;
            }
            if (right)
            {
                if (XSpeed < 0)
                {
                    Brake(Orientation.Horizontal);
                }
                deltaX += Acceleration * GlobalTimer.ElapsedTimeInSeconds;
            }
            if (!left && !right)
            {
                DecreaseVelocityDueFriction(Orientation.Horizontal);
            }

            XSpeed += deltaX * speedMultiplier;
            YSpeed += deltaY * speedMultiplier;
        }

        private void Brake(Orientation direction)
        {
            if (direction == Orientation.Vertical)
            {
                YSpeed = DecreaseAbsoluteSpeedToZero(YSpeed, BrakingSpeed * GlobalTimer.ElapsedTimeInSeconds);
            }
            else
            {
                XSpeed = DecreaseAbsoluteSpeedToZero(XSpeed, BrakingSpeed * GlobalTimer.ElapsedTimeInSeconds);
            }
        }

        private void DecreaseVelocityDueFriction(Orientation direction)
        {
            if (direction == Orientation.Vertical)
            {
                YSpeed = DecreaseAbsoluteSpeedToZero(YSpeed, Friction * GlobalTimer.ElapsedTimeInSeconds);
            }
            else
            {
                XSpeed = DecreaseAbsoluteSpeedToZero(XSpeed, Friction * GlobalTimer.ElapsedTimeInSeconds);
            }
        }

        private void UpdatePosition()
        {
            double deltaX = XSpeed;
            double deltaY = YSpeed;

            position.X += deltaX * GlobalTimer.ElapsedTimeInSeconds;
            position.Y += deltaY * GlobalTimer.ElapsedTimeInSeconds;
        }
    }
}
