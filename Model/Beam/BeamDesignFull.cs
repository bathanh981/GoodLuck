using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Beam
{
    public class BeamDesignFull : ViewModel.BaseViewModel
    {
        private ObservableCollection<BeamDesign> beamDesigns;
        private ObservableCollection<ReinforcementBeamMaster> reinforcementBeamMasters;
        private string nameBeam;
        private BeamModel beamModel;
        public string NameBeam { get => nameBeam; set { nameBeam = value; OnPropertyChanged(); } }
        public ObservableCollection<BeamDesign> BeamDesigns { get => beamDesigns; set { beamDesigns = value; OnPropertyChanged(); } }
        public ObservableCollection<ReinforcementBeamMaster> ReinforcementBeamMasters { get => reinforcementBeamMasters; set { reinforcementBeamMasters = value; OnPropertyChanged(); } }
        public BeamModel BeamModel { get => beamModel; set { beamModel = value; OnPropertyChanged(); } }

        private ReinforcementBeamBelt reinforcementBeamBelt;
        public double Length
        {
            get
            {
                double l = 0;
                foreach (BeamDesign beam in BeamDesigns)
                {
                    l += beam.BeamProperties.Lenght;
                }
                return l;
            }
        }


        public BeamDesignFull()
        {
            NameBeam = "D-Auto";
            BeamModel = new BeamModel();
            ReinforcementBeamMasters = new ObservableCollection<ReinforcementBeamMaster>();
            BeamDesigns = new ObservableCollection<BeamDesign>();
        }

        public void AddBeamDesign(BeamDesign beam)
        {
            beam.BeamModel = BeamModel;
            if (BeamDesigns.Count == 0) BeamDesigns.Add(beam);
            else
            {
                if (BeamDesigns[0].CheckThangHang(beam))// Dam Thang hang
                {
                    BeamDesigns.Add(beam);
                }
            }
            SortBeam();
        }
        public void SortBeam()
        {
            if (BeamDesigns.Count == 0) return;
            var a = BeamDesigns.OrderBy(x => x.BeamProperties.Point1.X)
                  .ThenBy(x => x.BeamProperties.Point1.Y)
                  .ToList();
            BeamDesigns = new ObservableCollection<BeamDesign>(a);

        }

        public void ResetReinforcementBeamMasters()
        {
            if (BeamDesigns.Count == 0) return;
            ReinforcementBeamMasters.Clear();
            ReinforcementBeamBelt = new ReinforcementBeamBelt(BeamDesigns[0]);
            foreach (BeamDesign beam in BeamDesigns)
            {
                foreach (ReinforcementBeamMaster master in beam.ReinforcementBeamMasters)
                {
                    ReinforcementBeamMasters.Add(master);
                }
            }
        }
        public double Qmax
        {
            get
            {
                double max = 0;
                foreach (BeamDesign beam in BeamDesigns)
                {
                    max = (max < beam.Vmax) ? beam.Vmax : max;
                }
                return max;
            }
        }
        public double Pmax
        {
            get
            {
                double max = 0;
                foreach (BeamDesign beam in BeamDesigns)
                {
                    max = (max < beam.P) ? beam.P : max;
                }
                return max;
            }
        }

        public ReinforcementBeamBelt ReinforcementBeamBelt { get => reinforcementBeamBelt; set => reinforcementBeamBelt = value; }
    }
}
