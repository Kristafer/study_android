using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Kernel: ICloneable
    {
        public int Klaster { get; set; }

        public Color Color { get; set; }

        public KPoint KPoint { get; set; }

        public bool Equals(Kernel b)
        {
            return this.KPoint.X == b.KPoint.X && this.KPoint.Y == b.KPoint.Y;
        }

        public Object Clone()
        {
           return new Kernel()
            {
                Color = this.Color,
                Klaster = this.Klaster,
                KPoint = new KPoint()
                {
                    X = this.KPoint.X,
                    Y = this.KPoint.Y,
                    Klaster = this.KPoint.Klaster,
                }
            };
        }
    }
}
