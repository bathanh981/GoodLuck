using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Material
{
   public class SteelProperties
    {
        private string steelName;
        private double fy, rs, rsc, rsw, es;

        public string SteelName { get => steelName; set => steelName = value; }
        public double Fy { get => fy; set => fy = value; }
        public double Rs { get => rs; set => rs = value; }
        public double Rsc { get => rsc; set => rsc = value; }
        public double Rsw { get => rsw; set => rsw = value; }
        public double Es { get => es; set => es = value; }

        public SteelProperties(string steelName, double fy, double rs, double rsc, double rsw, double es)
        {
            SteelName = steelName;
            Fy = fy;
            Rs = rs;
            Rsc = rsc;
            Rsw = rsw;
            Es = es;
        }

    }
}
