using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tanks.Controlls;
using Windows.Foundation;
using Windows.Media.Devices.Core;

namespace Tanks
{
    class Tank
    {
        public const double HighSpeed = 3000;
        public const double NormalSpeed = 1500;
        public double Speed { get; set; } = HighSpeed;
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
            double deltaX = 0;
            double deltaY = 0;
            if (controll.GetActionState(GameplayAction.Speed))
            {
                Speed = HighSpeed;
            } else
            {
                Speed = NormalSpeed;
            }

            if (controll.GetActionState(GameplayAction.Up))
            {
                deltaY += -Speed * GlobalTimer.ElapsedTimeInSeconds;
            }

            if(controll.GetActionState(GameplayAction.Down))
            {
                deltaY += Speed * GlobalTimer.ElapsedTimeInSeconds;
            }

            if(controll.GetActionState(GameplayAction.Left))
            {
                deltaX += -Speed * GlobalTimer.ElapsedTimeInSeconds;
            }

            if(controll.GetActionState(GameplayAction.Right))
            {
                deltaX += Speed * GlobalTimer.ElapsedTimeInSeconds;
            }

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
        }
    }
}
