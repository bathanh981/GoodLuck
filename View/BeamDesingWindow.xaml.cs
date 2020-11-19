using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GoodLuck.View
{
    /// <summary>
    /// Interaction logic for BeamDesingWindow.xaml
    /// </summary>
    public partial class BeamDesingWindow : Window
    {

        public BeamDesingWindow(Model.ProjectInformation projectInformation)
        {
            InitializeComponent();
            this.DataContext = new ViewModel.BeamViewModel(projectInformation);
            this.combolist.ItemsSource = new Model.Etabs.LoadCombinations().MyName;
            cbxChonBetong.ItemsSource = Model.Material.MaterialDB.ConcreteProperties;
            cbxChonThepChu.ItemsSource = Model.Material.MaterialDB.SteelProperties;
            cbxChonThepDai.ItemsSource = Model.Material.MaterialDB.SteelProperties;
            phi1.ItemsSource = ChooseSteel;
            phi2.ItemsSource = ChooseSteel;
            n1.ItemsSource = ChooseQuantity;
            n2.ItemsSource = ChooseQuantity;

        }
        public IEnumerable<int> ChooseSteel = new int[15]
        {
            6,8,10,12,14,16,18,20,22,25,28,30,32,36,40
        };


        public IEnumerable<int> ChooseQuantity = new int[]
        {
            0,1,2,3,4,5,6,7,8,9,10,11,12,13,14
        };
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ViewModel.BeamViewModel beamView = (ViewModel.BeamViewModel)this.DataContext;
            beamView.BeamDesignFull.ResetReinforcementBeamMasters();
                    }
    }
}
