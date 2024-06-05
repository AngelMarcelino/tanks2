using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Tanks.Controlls
{
    class ControllSet
    {
        public ControllSet()
        {
            Controlls1 = new Controll(new KeyBindings());
            Controlls2 = new Controll(new KeyBindings());
        }
        public Controll Controlls1 { get; private set; }
        public Controll Controlls2 { get; private set; }

        public void HandleKeyUp(VirtualKey key)
        {
            Controlls1.UnsetControll(key);
            Controlls2.UnsetControll(key);
        }

        public void HandleKeyDown(VirtualKey key) {
            Controlls1.SetControll(key);
            Controlls2.SetControll(key);
        }
    }
}
