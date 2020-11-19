using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoodLuck.Model.Etabs
{
    /// <summary>
    /// Lấy thông tin thuộc tính của phần tử thanh bằng tên phần tử
    /// </summary>
   public class FarmeProperties
    {
        private string _NameNumber;
        private string nameSection = "";
        private string sAuto;
        private string lable;
        private string story;
        private string fileName;
        private string matProp;
        private double depth;
        private double width;
        private int color;
        private string notes;
        private string gUID;
        private Point point1;
        private Point point2;
        public string NameSection { get => nameSection; set => nameSection = value; }
        public string SAuto { get => sAuto; set => sAuto = value; }
        public string Lable { get => lable; set => lable = value; }
        public string Story { get => story; set => story = value; }
        public string FileName { get => fileName; set => fileName = value; }
        public string MatProp { get => matProp; set => matProp = value; }
        public double Depth { get => depth; set => depth = value; }
        public double Width { get => width; set => width = value; }
        public int Color { get => color; set => color = value; }
        public string Notes { get => notes; set => notes = value; }
        public string GUID { get => gUID; set => gUID = value; }
        public string NameNumber { get => _NameNumber; set => _NameNumber = value; }
        public double Lenght { get => point1.Lenght(Point2); }
        public Point Point1 { get => point1; set => point1 = value; }
        public Point Point2 { get => point2; set => point2 = value; }

        public FarmeProperties(string nameNumber)
        {
            NameNumber = nameNumber;
            Run(); 
        }
        public void Run()
        {
            if(EtabsData.Instance.CSapModel.FrameObj.GetLabelFromName(NameNumber, ref lable, ref story) != 0)
            {
                MessageBox.Show("Lỗi khi lấy thông tin phần tử "+ NameNumber, "Thông báo");
                return;
            }


            if(EtabsData.Instance.CSapModel.FrameObj.GetSection(NameNumber, ref nameSection, ref sAuto) != 0)
            {
                MessageBox.Show("Lỗi khi lấy thông tin phần tử " + NameNumber, "Thông báo");
                return;
            }

            if(EtabsData.Instance.CSapModel.PropFrame.GetRectangle(NameSection,
                       ref fileName,
                       ref matProp,
                       ref depth,
                       ref width,
                       ref color,
                       ref notes,
                       ref gUID) != 0)
            {
                MessageBox.Show("Lỗi khi lấy thông tin phần tử " + NameNumber, "Thông báo");
                return;
            }
            string p1 = "", p2 = "";
            if (EtabsData.Instance.CSapModel.FrameObj.GetPoints(NameNumber, ref p1, ref p2) != 0)
            {
                MessageBox.Show("Lỗi khi lấy thông tin phần tử " + NameNumber, "Thông báo");
                return;
            }
            else
            {
                Point1 = new Point(p1);
                Point2 = new Point(p2);
            }
        }
      
    }
}
