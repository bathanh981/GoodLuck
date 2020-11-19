using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Etabs
{
   public class Point
    {
        private double x, y, z;
        private string name;

        public Point(string name)
        {
            this.Name = name;
            Etabs.EtabsData.Instance.CSapModel.PointObj.GetCoordCartesian(name, ref x, ref y, ref z);
        }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }
        public string Name { get => name; set => name = value; }

        public double Lenght(Point p)
        {
            return Math.Sqrt(Math.Pow(this.x - p.x, 2) + Math.Pow(this.y - p.y, 2) + Math.Pow(this.z - p.z, 2));
        }
    }
}
