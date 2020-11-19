using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Etabs
{
  public  class EtabsData
    {
        public cSapModel CSapModel { get; set; }
        public cPluginCallback CPlugin { get; set; }

        public static EtabsData Instance { get; set; }

        public EtabsData(ref cSapModel cSapModel, ref cPluginCallback cPlugin)
        {
            CSapModel = cSapModel;
            CPlugin = cPlugin;
        }
    
    }
}
