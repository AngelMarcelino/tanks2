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
        
        public Position position { get; private set; }

        public Movable movable { get; private set; }

        public double Widht { get; set; } = 40;
        public double Height { get; set; } = 40;

        public Tank(Controll controll)
        {
            this.controll = controll;
            this.position = new Position()
            {
                X = 40,
                Y = 40
            };
            this.movable = new Movable(position);
        }

        private Controll controll;

        public void UpdateState()
        {
            movable.UpdateSpeed(
                controll.GetActionState(GameplayAction.Up),
                controll.GetActionState(GameplayAction.Down),
                controll.GetActionState(GameplayAction.Left),
                controll.GetActionState(GameplayAction.Right)
            );
            movable.UpdatePosition();
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
            args.DrawingSession.FillRectangle(Shape, Microsoft.UI.Colors.White);
            args.DrawingSession.DrawText($"XSpeed: {movable.XSpeed}", 40, 40, Microsoft.UI.Colors.AliceBlue);
            args.DrawingSession.DrawText($" YSpeed: {movable.YSpeed}", 40, 80, Microsoft.UI.Colors.AliceBlue);
        }
    }
}
