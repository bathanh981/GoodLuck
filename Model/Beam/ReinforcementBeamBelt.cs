using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Beam
{
    /// <summary>
    /// Tính toán cốt đai dầm theo TCVN 55744-2018
    /// Tính toán cấu kiện bê tông cốt thép chịu uốn theo tiết diện nghiêng theo điều kiện:  Q <= Qb+Qsw
    /// 1. Qb là lực cắt chịu bởi bê tông trong tiết diện nghiêng.
    /// 2. Qsw là lực cắt chịu bởi cốt thép ngang trong tiết diện nghiêng.
    /// 3. Q là lực cắt trên tiết diện nghiêng với chiều dài Hình chiếu C lên trục dọc cấu kiện.
    /// 4. Hình chiếu C hợp với trục dọc góc 45 độ.
    /// Thông số vào: 
    /// </summary>
    public class ReinforcementBeamBelt
    {
        private BeamDesign beamDesign;
        private ChooseSteel chooseSteel;
        private double s;

        public double Phin
        {
            get
            {
                if (BeamDesign.P > 0)
                {
                    double phi2 = 0.1 * BeamDesign.P / (BeamDesign.BeamModel.ConcreteProperties.Rb * BeamDesign.BeamProperties.Width * BeamDesign.ReinforcementBeamMasters[0].Ho);
                    return (phi2 <= 0.5) ? phi2 : 0.5;
                }
                else
                {
                    double phi = -0.2 * BeamDesign.P / (BeamDesign.BeamModel.ConcreteProperties.Rb * BeamDesign.BeamProperties.Width * BeamDesign.ReinforcementBeamMasters[0].Ho);
                    return (phi <= 0.8) ? phi : 0.8;

                }
            }
        }
        public double Qbmin { get => BeamDesign.BeamModel.ConcreteProperties.Phib3 * (1 + Phin) * BeamDesign.BeamModel.ConcreteProperties.Rbt * BeamDesign.BeamProperties.Width * BeamDesign.ReinforcementBeamMasters[0].Ho; }
        public double Qbmax { get => 2.5 * BeamDesign.BeamModel.ConcreteProperties.Rbt * BeamDesign.BeamProperties.Width * BeamDesign.ReinforcementBeamMasters[0].Ho; }
        public double Qbo { get => (BeamDesign.BeamModel.ConcreteProperties.Phib4 * (1 + Phin) * BeamDesign.BeamModel.ConcreteProperties.Rbt * BeamDesign.BeamProperties.Width * BeamDesign.ReinforcementBeamMasters[0].Ho * BeamDesign.ReinforcementBeamMasters[0].Ho) / C0; }

        public double Qb
        {
            get
            {
                double q = (BeamDesign.BeamModel.ConcreteProperties.Phib2 * (1 + Phin) * BeamDesign.BeamModel.ConcreteProperties.Rbt * BeamDesign.BeamProperties.Width * BeamDesign.ReinforcementBeamMasters[0].Ho * BeamDesign.ReinforcementBeamMasters[0].Ho) / C;
                return (q < Qbmin ? Qbmin : (q > Qbmax) ? Qbmax : q);
            }
        }

        public double Qsw { get => BeamDesign.BeamModel.SteelPropertiesBelt.Rsw * ChooseSteel.As / S * C0; }
        public double Mb { get => BeamDesign.BeamModel.ConcreteProperties.Phib2 * (1 + Phin) * BeamDesign.BeamModel.ConcreteProperties.Rbt * BeamDesign.BeamProperties.Width * BeamDesign.ReinforcementBeamMasters[0].Ho * BeamDesign.ReinforcementBeamMasters[0].Ho; }
        public double C_ { get => Math.Sqrt(Mb / (ChooseSteel.As * BeamDesign.BeamModel.SteelPropertiesBelt.Rsw / S)); }

        public double C0 { get => (C_ < BeamDesign.ReinforcementBeamMasters[0].Ho) ? BeamDesign.ReinforcementBeamMasters[0].Ho : C_; }
        public double C { get => (C_ > 2 * BeamDesign.ReinforcementBeamMasters[0].Ho) ? 2 * BeamDesign.ReinforcementBeamMasters[0].Ho : C_; }


        public BeamDesign BeamDesign { get => beamDesign; set => beamDesign = value; }
        public string CheckQ { get => (BeamDesign.Vmax <= Qb + Qsw) ? "OK" : "NOT"; }

        // Khả năng bê tông không bị phá hoại trên tiết diện nghiêng - ứng suất nén chính

        public double Phiw1
        {
            get
            {
                double anpha = BeamDesign.BeamModel.SteelPropertiesBelt.Es / BeamDesign.BeamModel.ConcreteProperties.Eb;
                double nhuyW = ChooseSteel.As / (BeamDesign.BeamProperties.Width * S);
                double phi = 1 + 5 * anpha * nhuyW;
                return (phi < 1.3) ? phi : 1.3;
            }
        }
        public double UngSuatNenChinh { get => 0.3 * Phiw1 * BeamDesign.BeamModel.ConcreteProperties.Phib1 * BeamDesign.BeamModel.ConcreteProperties.Rb * BeamDesign.ReinforcementBeamMasters[0].Ho * BeamDesign.BeamProperties.Width; }

        public string CheckUngSuat { get => (BeamDesign.Vmax < UngSuatNenChinh) ? "OK" : "NOT"; }
        public double CotDaiCauTaoGiuaDam { get => Math.Min(3 / 4 * BeamDesign.BeamProperties.Depth, 300); }
        public ChooseSteel ChooseSteel { get => chooseSteel; set => chooseSteel = value; }
        public double S { get => s; set => s = value; }

        public ReinforcementBeamBelt(BeamDesign beamDesign)
        {
            BeamDesign = beamDesign;
            ChooseSteel = new ChooseSteel(6, 2);
            S = 300;
            Run();
        }
        public void Run()
        {
            while (CheckQ.Equals("NOT"))
            {
                S -= 50;
                if (S < 100)
                {
                    ChooseSteel.NextPhi();
                    S = 300;
                }
            }
        }

    }
}
