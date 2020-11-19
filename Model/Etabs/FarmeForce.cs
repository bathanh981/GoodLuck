using ETABSv1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoodLuck.Model.Etabs
{
    /// <summary>
    /// Xuất giá trị nội lực theo tên phần tử thanh trong etabs
    /// </summary>
    public class FarmeForce
    {
        private string nameNumber;
        private int numberResults;
        private string[] obj;
        private double[] objSta;
        private string[] elm;
        private double[] elmSta;
        private string[] loadCase;
        private string[] stepType;
        private double[] stepNum;
        private double[] p;
        private double[] v2;
        private double[] v3;
        private double[] t;
        private double[] m2;
        private double[] m3;

        public int NumberResults { get => numberResults; set => numberResults = value; }
        public eItemTypeElm EItemTypeElm { get => eItemTypeElm.ObjectElm; }
        public string[] Obj { get => obj; set => obj = value; }
        public double[] ObjSta { get => objSta; set => objSta = value; }
        public string[] Elm { get => elm; set => elm = value; }
        public double[] ElmSta { get => elmSta; set => elmSta = value; }
        public string[] LoadCase { get => loadCase; set => loadCase = value; }
        public string[] StepType { get => stepType; set => stepType = value; }
        public double[] StepNum { get => stepNum; set => stepNum = value; }
        public double[] P { get => p; set => p = value; }
        public double[] V2 { get => v2; set => v2 = value; }
        public double[] V3 { get => v3; set => v3 = value; }
        public double[] T { get => t; set => t = value; }
        public double[] M2 { get => m2; set => m2 = value; }
        public double[] M3 { get => m3; set => m3 = value; }
        public string NameNumber { get => nameNumber; set => nameNumber = value; }

        public FarmeForce(string nameNumber)
        {
            NameNumber = nameNumber;
            Run();
        }
        /// <summary>
        /// Lấy nội lực của thanh trong etabs
        /// </summary>
        public void Run()
        {
            int k = EtabsData.Instance.CSapModel.Results.FrameForce(NameNumber, EItemTypeElm, ref numberResults,
               ref obj, ref objSta, ref elm, ref elmSta, ref loadCase,
               ref stepType, ref stepNum, ref p, ref v2,
               ref v3, ref t, ref m2, ref m3);
            if (k != 0)
            {
                MessageBox.Show("Lỗi khi lấy tải trọng thanh " + NameNumber, "Thông báo");
                return;
            }
        }

        

    }
}
