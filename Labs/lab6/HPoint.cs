using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class HPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public HPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public HPoint()
        {
            X = 0;
            Y = 0;
        }
    }
}
