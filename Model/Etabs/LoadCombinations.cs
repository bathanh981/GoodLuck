using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoodLuck.Model.Etabs
{
 public class LoadCombinations
    {
        private int numberNames;

        private string[] myName;
        public int NumberNames { get => numberNames; set => numberNames = value; }
        public string[] MyName { get => myName; set => myName = value; }

        public LoadCombinations()
        {
            int a = EtabsData.Instance.CSapModel.RespCombo.GetNameList(ref numberNames, ref myName);
            if(numberNames==0)
            {
                throw new ExceptionUser.ExceptionUser("Error: Không load được combination.");
            }
        }
        public void SetCombo(string combo)
        {
            EtabsData.Instance.CSapModel.Results.Setup.SetComboSelectedForOutput(combo, true);
        }
        public void ResetCombo()
        {
            EtabsData.Instance.CSapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            int a = EtabsData.Instance.CSapModel.RespCombo.GetNameList(ref numberNames, ref myName);
            if (numberNames == 0)
            {
                throw new ExceptionUser.ExceptionUser("Error: Không load được combination.");
            }
        }


    }
}
