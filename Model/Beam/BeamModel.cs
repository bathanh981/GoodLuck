using GoodLuck.Model.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Beam
{
    /// <summary>
    /// Là một abstract Class chứa các thông số đầu vào và thông sô sau tính toán
    /// 1. Chứa hàm Run() để tính toán
    /// </summary>
    public class BeamModel
    {
        private ConcreteProperties concreteProperties;
        private SteelProperties steelPropertiesBelt;
        private SteelProperties steelPropertiesMaster;
        private double a;
        private double yb;
        private double nhuyMin;

        public double NhuyMin { get => nhuyMin; set { nhuyMin = value; } }
        public double EpxilonR { get => 0.8 / (1 + (SteelPropertiesMaster.Rs / SteelPropertiesMaster.Es) / 0.0035); }
        public double NhuyMax { get => ConcreteProperties.Rb * EpxilonR / SteelPropertiesMaster.Rs; }
        public ConcreteProperties ConcreteProperties { get => concreteProperties; set => concreteProperties = value; }
        public double A { get => a; set => a = value; }
        public double Yb { get => yb; set => yb = value; }
        public SteelProperties SteelPropertiesBelt { get => steelPropertiesBelt; set => steelPropertiesBelt = value; }
        public SteelProperties SteelPropertiesMaster { get => steelPropertiesMaster; set => steelPropertiesMaster = value; }

        public BeamModel()
        {
            NhuyMin = 0.01;
            A = 40;
            Yb = 1;
            ConcreteProperties = MaterialDB.GetConcreteProperties("B30");
            SteelPropertiesBelt = MaterialDB.GetSteelProperties("CB240_T");
            SteelPropertiesMaster = MaterialDB.GetSteelProperties("CB400_V");
        }
    }
}

