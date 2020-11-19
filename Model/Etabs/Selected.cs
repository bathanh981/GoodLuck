using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoodLuck.Model.Etabs
{
   public class Selected
    {
        public int NumberItems = 0;

        public int[] ObjectTypes = { };

        public string[] ObjectNames = { };

        public Selected()
        {
            int i= EtabsData.Instance.CSapModel.SelectObj.GetSelected(ref NumberItems, ref ObjectTypes, ref ObjectNames);
            if (i != 0)
            {
                MessageBox.Show("Lỗi khi Selected phần tử", "Thông báo");
                return;
            }
            if (NumberItems == 0) MessageBox.Show("Bạn chưa chọn phần tử!", "Thông báo");
        }

    }
}
