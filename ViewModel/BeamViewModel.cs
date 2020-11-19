using ETABSv1;
using GoodLuck.Model;
using GoodLuck.Model.Beam;
using GoodLuck.Model.Etabs;
using GoodLuck.Model.Material;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace GoodLuck.ViewModel
{
   public  class BeamViewModel : BaseViewModel
    {
        public ProjectInformation ProjectInformation { get; set; }
        public string Combo { get; set; }
        public ICommand SelectedCommand { get; set; }
        public ICommand DeleteBeamSoBoCommand { get; set; }
        public ICommand ResetBeamSoBoCommand { get; set; }
        public ICommand DesignBeamSoBoCommand { get; set; }
        public ICommand ResetBeamCommand { get; set; }
        public ICommand ExportBeamCommand { get; set; }
        public ICommand AddBeamDesignFullCommand { get; set; }

        public Selected Selected { get; set; }
        private BeamDesignFull beamDesignFull;
        public BeamDesignFull BeamDesignFull { get => beamDesignFull; set { beamDesignFull = value; OnPropertyChanged(); } }

        private ObservableCollection<BeamDesignFull> beamDesignFulls;
        public BeamDesign BeamSoBo { get; set; }
        public ObservableCollection<BeamDesignFull> BeamDesignFulls { get => beamDesignFulls; set { beamDesignFulls = value; OnPropertyChanged(); } }

        public BeamViewModel( ProjectInformation projectInformation)
        {
            this.ProjectInformation = projectInformation;
            BeamDesignFull = new BeamDesignFull();
            BeamDesignFulls = new ObservableCollection<BeamDesignFull>();
            //Command Sơ Bộ
            SelectedCommand = new RelayCommand<object>((p) => { if (Combo.Equals("") && Combo == null) return false; return true; }, (p) =>
               {
                   Selected = new Selected();
                   LoadCombinations combinations = new LoadCombinations();
                   combinations.SetCombo(Combo);
                   for (int i = 0; i < Selected.NumberItems; i++)
                   {
                       eFrameDesignOrientation frameDesignOrientation = new eFrameDesignOrientation();
                       EtabsData.Instance.CSapModel.FrameObj.GetDesignOrientation(Selected.ObjectNames[i], ref frameDesignOrientation);
                       if (Selected.ObjectTypes[i] == 2 && frameDesignOrientation == eFrameDesignOrientation.Beam)
                       {
                           BeamDesignFull.AddBeamDesign(new BeamDesign(Selected.ObjectNames[i]));
                       }
                   }

               });
            DeleteBeamSoBoCommand = new RelayCommand<object>((p) =>
            {
                if (BeamSoBo == null) return false; return true;
            },
                (p) =>
                {
                    BeamDesignFull.BeamDesigns.Remove(BeamSoBo);
                }); ;
            ResetBeamSoBoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 BeamDesignFull.BeamDesigns.Clear();
                 Selected = new Selected();
                 LoadCombinations combinations = new LoadCombinations();
                 combinations.ResetCombo();
             });
            DesignBeamSoBoCommand = new RelayCommand<object>((p) =>
            {
              return true;
            }, (p) =>
            {
                BeamDesignFull.ResetReinforcementBeamMasters();
            });
            AddBeamDesignFullCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                BeamDesignFulls.Add(BeamDesignFull);
                BeamDesignFull = new BeamDesignFull();
            });
            ExportBeamCommand = new RelayCommand<object>((p) =>
            {
                if (BeamDesignFulls == null || BeamDesignFulls.Count == 0) return false;
                return true;
            }, (p) =>
            {
                ExportBeam();
            });
        }

        public void ExportBeam()
        {
           SaveFileDialog dialog = new SaveFileDialog();
            string filePath = "";
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }
            if (string.IsNullOrEmpty(filePath))
            {
                System.Windows.Forms.MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                return;
            }
            else
            {
                try
                {
                    //string path = "D:\\THXD\\DATN\\ChuyendeEtabs\\Program\\GoodLuck\\bin\\Debug\\Resource";
                    ExcelModel.ExportToExcel excel = new ExcelModel.ExportToExcel();
                    excel.ExportFloor(this, "Resource\\ThietkedamTCVN5574-2018.xlsx", filePath);
                    System.Windows.Forms.MessageBox.Show("Xuất file thành công", "Thông báo");
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Có lỗi khi Export file. Hãy kiểm tra lại!", "Thông báo");
                }

            }

        }
    }
}
