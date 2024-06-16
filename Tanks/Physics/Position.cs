using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Tanks.Physics
{
    internal class Position
    {
        private double _x;
        private double _y;
        Rect space;
        public Position(Rect space)
        {
            this.space = space;
        }
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                double result = value;
                if (result < 0)
                {
                    result = 0;
                }
                if (result > space.Width)
                {
                    result = space.Width;
                }
                _x = result;
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                double result = value;
                if (result < 0)
                {
                    result = 0;
                }
                if (result > space.Height)
                {
                    result = space.Height;
                }
                _y = result;
            }
        }
    }
}
