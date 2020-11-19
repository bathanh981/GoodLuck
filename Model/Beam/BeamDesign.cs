using GoodLuck.Model.Etabs;
using GoodLuck.Model.Material;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoodLuck.Model.Beam
{
    public class BeamDesign : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string nameNumber;
        private ReinforcementBeamBelt reinforcementBeamBelt;
        private ObservableCollection<ReinforcementBeamMaster> reinforcementBeamMasters;
        private FarmeForce beamForce;
        private FarmeProperties beamProperties;
        private BeamModel beamModel;

        /// <summary>
        /// Tên dầm trong etabs
        /// </summary>
        public string NameNumber { get => nameNumber; set { nameNumber = value; } }


        /// <summary>
        /// Bài toán tính cốt đai dầm
        /// </summary>
        public ReinforcementBeamBelt ReinforcementBeamBelt { get => reinforcementBeamBelt; set { reinforcementBeamBelt = value; OnPropertyChanged(); } }
        /// <summary>
        /// Bài toán tính thép chủ dầm
        /// </summary>
        public ObservableCollection<ReinforcementBeamMaster> ReinforcementBeamMasters { get => reinforcementBeamMasters; set { reinforcementBeamMasters = value; OnPropertyChanged(); } }
        public FarmeForce BeamForce { get => beamForce; set { beamForce = value; OnPropertyChanged(); } }
        public FarmeProperties BeamProperties { get => beamProperties; set { beamProperties = value; OnPropertyChanged(); } }


        public double[] Mgoi1 { get; set; }
        public double[] Mgoi2 { get; set; }
        public double[] Mmax { get; set; }
        public double Vmax { get; set; }

        public BeamDesign(string nameNumber)
        {
            this.nameNumber = nameNumber;
            BeamProperties = new FarmeProperties(nameNumber);
            ReinforcementBeamMasters = new ObservableCollection<ReinforcementBeamMaster>();
            BeamForce = new FarmeForce(nameNumber);
            if (BeamForce.NumberResults > 0)
            {
                Mgoi1 = new double[]
                {
                 (BeamForce.M3[BeamForce.NumberResults / 2 ]>0)?0:BeamForce.M3[BeamForce.NumberResults / 2 ],
                 BeamForce.ObjSta[BeamForce.NumberResults / 2 ]
                 };
                Mgoi2 = new double[]
                {
                    (BeamForce.M3[BeamForce.NumberResults -1]>0)?0:BeamForce.M3[BeamForce.NumberResults-1 ],                 
                    BeamForce.ObjSta[BeamForce.NumberResults -1 ]
                 };
                Mmax = new double[]
                {
                     BeamForce.M3[0],
                     BeamForce.ObjSta[0]
                 };
                Vmax = Math.Abs(BeamForce.V2[0]);

                for (int i = 0; i < BeamForce.NumberResults - 1; i++)
                {
                    if (Mmax[0] < BeamForce.M3[i])
                    {
                        Mmax[0] = BeamForce.M3[i];
                        Mmax[1] = BeamForce.ObjSta[i];
                    }
                    Vmax = (Vmax > Math.Abs(BeamForce.V2[i])) ? Math.Abs(BeamForce.V2[i]) : Vmax;
                }
                ReinforcementBeamMasters.Add(new ReinforcementBeamMaster(this, this.Mgoi1[0], this.Mgoi1[1]));
                ReinforcementBeamMasters.Add(new ReinforcementBeamMaster(this, this.Mmax[0], this.Mmax[1]));
                ReinforcementBeamMasters.Add(new ReinforcementBeamMaster(this, this.Mgoi2[0], this.Mgoi2[1]));
            }

        }

        public BeamModel BeamModel { get => beamModel; set => beamModel = value; }

        public bool CheckThangHang(BeamDesign beam)
        {
            double[] vt1 = new double[]
            {
                this.BeamProperties.Point1.X-this.BeamProperties.Point2.X,
                this.BeamProperties.Point1.Y-this.BeamProperties.Point2.Y,
                this.BeamProperties.Point1.Z-this.BeamProperties.Point2.Z
            };
            double[] vt2 = new double[]
             {
                beam.BeamProperties.Point1.X-beam.BeamProperties.Point2.X,
                beam.BeamProperties.Point1.Y-beam.BeamProperties.Point2.Y,
                beam.BeamProperties.Point1.Z-beam.BeamProperties.Point2.Z
             };

            double cos = (vt1[0] * vt2[0] + vt1[1] * vt2[1] + vt1[2] * vt2[2]) / (Math.Sqrt(vt1[0] * vt1[0] + vt1[1] * vt1[1] + vt1[2] * vt1[2]) * Math.Sqrt(vt2[0] * vt2[0] + vt2[1] * vt2[1] + vt2[2] * vt2[2]));
            return cos == 1 || cos == -1;
        }

    }
}
