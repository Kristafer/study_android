using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    public class Group : HPoint
    {

        public List<Distance> Distances = new List<Distance>();
        public List<Group> SubGroups = new List<Group>();
        public string Name;

        public Group(double x, double y): base(x,y) {
        }
        public Group() : base()
        {
        }

        public Distance GetDistance(Group group)
        {
            return Distances.FirstOrDefault(distance => distance.Group.Equals(group));
        }

        public void DeleteDistances(List<Group> deleteList)
        {
            foreach (Group deleteGroup in deleteList)
            {
                var deleteDistances = (Distances.Where(distance => distance.Group.Equals(deleteGroup))).ToList();
                foreach (Distance deleteDistance in deleteDistances)
                    Distances.Remove(deleteDistance);
            }
        }
    }
}
