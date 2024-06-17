using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    static class GlobalTimer
    {
        private static double _elapsedTime = 0;
        public static double ElapsedTimeInSeconds
        {
            get => _elapsedTime; 
            set
            {
                _elapsedTime = value;
                TotalTimeInSecods += value;
            }
        }
        public static double TotalTimeInSecods {  get; set; } = 0;
    }
}
