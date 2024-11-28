using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Physics
{
    internal interface ICollisionable
    {
        public Position Position { get; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
