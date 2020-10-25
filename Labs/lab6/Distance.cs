using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Distance
    {
        public double Value { get; set; }
        public Group Group { get; set; }

        public Distance(double value, Group group)
        {
            Value = value;
            Group = group;
        }
    }
}
