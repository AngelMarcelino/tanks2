using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks.Controlls;
using Windows.Foundation;

namespace Tanks
{
    class Environment
    {
        public static Environment Init()
        {
            var controllSet = new ControllSet();
            
            var env =  new Environment
            {
                ControllSet = controllSet,
                Projectiles = new List<Projectile>()
            };
            var tank1 = new Tank(controllSet.Controlls1, new Rect(0, 0, 6000, 1500), (projectile) =>
            {
                env.Projectiles.Add(projectile);
            });
            var tank2 = new Tank(controllSet.Controlls2, new Rect(0, 0, 6000, 1500), (projectile) =>
            {
                env.Projectiles.Add(projectile);
            });
            env.Tank1 = tank1;
            env.Tank2 = tank2;
            return env;
        }
        public ControllSet ControllSet { get; set; }
        public Tank Tank1 { get; set; }
        public Tank Tank2 { get; set; }
        public double ElapsedTime { get; set; } = 0;
        private List<Projectile> Projectiles { get; set; }


        public void UpdateState()
        {
            Projectiles = Projectiles.Where(e => !e.ToRemove).ToList();
            Tank1.UpdateState();
            Tank2.UpdateState();
            Projectiles.ForEach(projectile => projectile.UpdateState());
        }

        public void Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
        {
            Tank1.Draw(args);
            Tank2.Draw(args);
            Projectiles.ToList().ForEach(projectile => projectile.Draw(args));
            args.DrawingSession.DrawText($"Numer of projectiles {Projectiles.Count}", 40, 160, Microsoft.UI.Colors.AliceBlue);
        }
    }
}
