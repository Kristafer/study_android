using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Algorithm
    {
        private List<Group> groups;
        public Algorithm()
        {

        }

        public void SetData(int count, double[,] data)
        {
            groups = new List<Group>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new Group(0,0));
                groups[i].Name = "x" + (i + 1);
            }

            for (int i = 0; i < groups.Count; i++)
                for (int j = 0; j < groups.Count; j++)
                    if (i != j)
                        groups[i].Distances.Add(new Distance(data[i, j], groups[j]));

        }
    }
}
