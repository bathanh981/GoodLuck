using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck
{
    class cPlugin
    {

        public void Main(ref cSapModel SapModel, ref cPluginCallback ISapPlugin)
        {

            Model.Etabs.EtabsData.Instance = new Model.Etabs.EtabsData(ref SapModel, ref ISapPlugin);
            MainWindow form = new MainWindow();
            form.Show();
        }


        public long Info(ref string Text)
        {
            Text = "my first plugin\n";
            return 0;
        }
    }
}
