using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Beam
{
    /// <summary>
    /// Triển Khai tính toán cốt thép dầm khi đã biết M,B,H,Rb,Rs,Rsc:
    /// 1. Tính toán theo trường hợp đặt cốt thép đơn:Chỉ đặt As ở vùng kéo còn vùng nén thì riêng bê tông đủ chịu
    /// 2. Thông số vào: M,B,H,Rb,Rs,Rsc,Es
    /// 3. Kết quả: As.
    /// </summary>
   public class ReinforcementBeamMaster : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ChooseSteel chooseSteel1;
        private ChooseSteel chooseSteel2;
        private BeamDesign beamDesign;

        public double M { get; set; }
        public double Ho { get =>BeamDesign.BeamProperties.Depth - ao; }
        private double ao { get => (this.ChooseSteel1.As * (BeamDesign.BeamModel.A+ ChooseSteel1.Phi / 2) + ChooseSteel2.As * (BeamDesign.BeamModel.A + ChooseSteel1.Phi / 2 + 10)) / (AsChon); }
        public double Location { get; set; }
        public string NameFramce { get; set; }
        public double AnphaM { get => Math.Abs(M) / (BeamDesign.BeamModel.Yb * BeamDesign.BeamModel.ConcreteProperties.Rb * BeamDesign.BeamProperties.Depth * Math.Pow(Ho, 2)); }
        public double Epxilon { get => 1 - Math.Sqrt(1 - 2 * AnphaM); }
        public double Zeta { get => 1 - 0.5 * Epxilon; }
        public double As
        {
            get
            {
                return Math.Abs(M) / (BeamDesign.BeamModel.SteelPropertiesMaster.Rs * Zeta * Ho);
            }
        }
        public double AsChon { get => ChooseSteel1.As + ChooseSteel2.As; }

        public double NhuyTT { get => AsChon / (BeamDesign.BeamProperties.Width * Ho); }

        public double X
        {
            get
            {
                double as_;

                if (this == BeamDesign.ReinforcementBeamMasters[1])
                {
                    as_ = Math.Max(BeamDesign.ReinforcementBeamMasters[0].ChooseSteel1.As, BeamDesign.ReinforcementBeamMasters[2].ChooseSteel1.As);
                }
                else
                {
                    as_ = BeamDesign.ReinforcementBeamMasters[1].ChooseSteel1.As;
                }
                return Math.Abs((BeamDesign.BeamModel.SteelPropertiesMaster.Rs * AsChon - BeamDesign.BeamModel.SteelPropertiesMaster.Rsc * as_) / (BeamDesign.BeamModel.ConcreteProperties.Rb * BeamDesign.BeamProperties.Width * BeamDesign.BeamModel.Yb));
            }
        }

        public double Mu
        {
            get
            {
                double as_;
                double phi;

                if (this == BeamDesign.ReinforcementBeamMasters[1])
                {
                    as_ = Math.Max(BeamDesign.ReinforcementBeamMasters[0].ChooseSteel1.As, BeamDesign.ReinforcementBeamMasters[2].ChooseSteel1.As);
                    phi = Math.Max(BeamDesign.ReinforcementBeamMasters[0].ChooseSteel1.Phi, BeamDesign.ReinforcementBeamMasters[2].ChooseSteel1.Phi);
                }
                else
                {
                    as_ = BeamDesign.ReinforcementBeamMasters[1].ChooseSteel1.As;
                    phi = BeamDesign.ReinforcementBeamMasters[1].ChooseSteel1.Phi;
                }
                return (BeamDesign.BeamModel.ConcreteProperties.Rb * BeamDesign.BeamProperties.Width * X * (Ho - 0.5 * X) + BeamDesign.BeamModel.SteelPropertiesMaster.Rsc * as_ * (Ho - BeamDesign.BeamModel.A - phi / 2));
            }
        }

        public string CheckAs { get => (As <= AsChon) ? "Ok" : "NO"; }
        public string CheckNhuy { get => (NhuyTT <= BeamDesign.BeamModel.NhuyMax && NhuyTT >= 0.001) ? "Ok" : "NO"; }
        public string CheckMu { get => (Math.Abs(M) <= Mu) ? "Ok" : "NO"; }
        public string CheckEpxilon { get => (Math.Abs(Epxilon) <= BeamDesign.BeamModel.EpxilonR) ? "Ok" : "NO"; }

        public ChooseSteel ChooseSteel1 { get => chooseSteel1; set { chooseSteel1 = value; this.OnPropertyChanged(); } }
        public ChooseSteel ChooseSteel2 { get => chooseSteel2; set { chooseSteel2 = value; this.OnPropertyChanged(); } }
        internal BeamDesign BeamDesign { get => beamDesign; set { beamDesign = value; this.OnPropertyChanged(); } }

        public ReinforcementBeamMaster(BeamDesign beamDesign, double m,double location)
        {
            this.BeamDesign = beamDesign;
            NameFramce = beamDesign.NameNumber;
            M = m;
            this.Location = location;
            ChooseSteel1 = new ChooseSteel(18, 2);
            
            ChooseSteel2 = new ChooseSteel(18, 0);
        }
        public double MuFormat { get => Mu / 1000000; }
        public double MFormat { get => M / 1000000; }
    }
}
