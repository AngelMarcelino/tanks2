using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Controlls;

namespace Tanks
{
    class Environment
    {
        public static Environment Init()
        {
            var controllSet = new ControllSet();
            var tank1 = new Tank(controllSet.Controlls1);
            return new Environment
            {
                ControllSet = controllSet,
                Tank = tank1,
            };
        }

        public ControllSet ControllSet { get; set; }
        public Tank Tank { get; set; }
        public double ElapsedTime { get; set; } = 0;

        public void UpdateState()
        {
            Tank.UpdateState();
        }

        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            Tank.Draw(args);
        }
    }
}
