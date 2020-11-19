using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GoodLuck.Model;
namespace GoodLuck.ViewModel
{
    class MainWindowViewModel:BaseViewModel
    {
        ProjectInformation projectInformation = new ProjectInformation();

        public ProjectInformation ProjectInformation { get => projectInformation; set => projectInformation = value; }

        public MainWindowViewModel()
        {
            ProjectInformation = new ProjectInformation();
            DesignBeamCommand = new RelayCommand<object>((p) => { return true; }, (p) => { View.BeamDesingWindow wd = new View.BeamDesingWindow(ProjectInformation); wd.Show(); });
            DesignColumnCommand  = new RelayCommand<object>((p) => { return true; }, (p) => { View.ColumnDesignWindow wd = new View.ColumnDesignWindow(); wd.Show(); });
            DesignWallCommand  = new RelayCommand<object>((p) => { return true; }, (p) => { View.WallDesignWindow wd = new View.WallDesignWindow(); wd.Show(); });
        }
        public ICommand DesignBeamCommand { get; set; }
        public ICommand DesignColumnCommand  { get; set; }
        public ICommand DesignWallCommand  { get; set; }

    }
}
