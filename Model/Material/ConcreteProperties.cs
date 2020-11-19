using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Material
{
    public class ConcreteProperties
    {
        private string concreteName;
        private double rb;
        private double rbt;
        private double eb;
        private double rb_set;
        private double rbt_set;

        public string ConcreteName { get => concreteName; set { concreteName = value; } }
        public double Rb { get => rb; set => rb = value; }
        public double Rbt { get => rbt; set => rbt = value; }
        public double Eb { get => eb; set => eb = value; }
        public double Rb_set { get => rb_set; set => rb_set = value; }
        public double Rbt_set { get => rbt_set; set => rbt_set = value; }

        public ConcreteProperties(string concreteName, double rb, double rbt, double eb, double rb_set, double rbt_set)
        {
            ConcreteName = concreteName;
            Rb = rb;
            Rbt = rbt;
            Eb = eb;
            Rb_set = rb_set;
            Rbt_set = rbt_set;
        }

       
    }

}
