﻿using GoodLuck.Model.Beam;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Excel
{
    class ExportToExcel
    {
        public ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        public void ExportFloor(ViewModel.BeamViewModel beamViewModel, string pathTemplate, string fileSave)
        {

            using (ExcelPackage p = new ExcelPackage())
            {
                using (FileStream stream = new FileStream(pathTemplate, FileMode.Open, FileAccess.ReadWrite))
                {
                    p.Load(stream);
                }
                p.Workbook.Properties.Author = beamViewModel.ProjectInformation.EngineerName;
                p.Workbook.Properties.Title = beamViewModel.ProjectInformation.ProjectName;
                ExcelWorksheet ws = p.Workbook.Worksheets[0];
                ws.Cells["D2"].Value = beamViewModel.BeamDesignFull.BeamDesigns[0].BeamProperties.Story;
                ws.Cells["D4"].Value = beamViewModel.ProjectInformation.ProjectName;
                ws.Cells["D5"].Value = beamViewModel.ProjectInformation.Address;
                ws.Cells["D6"].Value = beamViewModel.ProjectInformation.EngineerName;
                ws.Cells["F8"].Value = beamViewModel.BeamDesignFull.BeamModel.ConcreteProperties.ConcreteName;
                ws.Cells["F10"].Value = beamViewModel.BeamDesignFull.BeamModel.SteelPropertiesMaster.SteelName;
                ws.Cells["AB8"].Value = beamViewModel.BeamDesignFull.BeamModel.Yb;
                ws.Cells["AB9"].Value = beamViewModel.BeamDesignFull.BeamModel.A;

                int row = 16;
                foreach (BeamDesignFull designFull in beamViewModel.BeamDesignFulls)
                {
                    int col = 1;
                    ws.Cells[col, row, col, row + designFull.BeamDesigns.Count * 3 - 1].Merge = true;
                    ws.Cells[col++, row].Value = designFull.NameBeam;

                    foreach (BeamDesign beam in designFull.BeamDesigns)
                    {
                        ws.Cells[col, row, col, row + 3 - 1].Merge = true;
                        ws.Cells[col++, row].Value = beam.NameNumber;
                        foreach (ReinforcementBeamMaster beamMaster in beam.ReinforcementBeamMasters)
                        {
                            ws.Cells[col++, row].Value = beamMaster.Location;
                            ws.Cells[col++, row].Value = beam.BeamProperties.Width;
                            ws.Cells[col++, row].Value = beam.BeamProperties.Depth;
                            ws.Cells[col++, row].Value = designFull.BeamModel.A;
                            ws.Cells[col++, row].Value = "=G" + row + "-H" + row;
                            ws.Cells[col++, row].Value = beamMaster.M;
                            ws.Cells[col++, row].Value = "=ABS(J" + row + ")*10^6/($AB$8*$H$8*F" + row + "*I" + row + "^2)";
                            ws.Cells[col++, row].Value = "=ROUND(1-SQRT(1-2*K" + row + "),3)";
                            ws.Cells[col++, row].Value = "=1-0.5*L" + row + "";
                            ws.Cells[col++, row].Value = "=IF(L" + row + "<$AB$10,\"Ok\",\"Not Ok\")";
                            ws.Cells[col++, row].Value = "=MAX(ABS(J" + row + ")*10^6/($H$10*M16*I" + row + "),0.001*F" + row + "*I" + row + ")";
                            ws.Cells[col++, row].Value = "=(O" + row + ")/(F" + row + "*I" + row + ")";
                            ws.Cells[col++, row].Value = beamMaster.ChooseSteel1.Quantity;
                            ws.Cells[col++, row].Value = beamMaster.ChooseSteel1.Phi;
                            ws.Cells[col++, row].Value = "+";
                            ws.Cells[col++, row].Value = beamMaster.ChooseSteel2.Quantity;
                            ws.Cells[col++, row].Value = beamMaster.ChooseSteel2.Phi;
                            ws.Cells[col++, row].Value = "=(Q" + row + "*PI()*R" + row + "^2/4+T" + row + "*PI()*U" + row + "^2/4)";
                            ws.Cells[col++, row].Value = "=IF(V" + row + ">O" + row + ",\"Ok\",\"Not Ok\")";
                            if (beamMaster.M <= 0)
                            {
                                if(beamMaster == beam.ReinforcementBeamMasters[0])
                                {
                                    ws.Cells[col++, row].Value = "=($H$10*V" + row + "-$H$10*(Q" + (row + 1) + "*PI()*R" + (row + 1) + "^2/4))/($AB$8*$H$8*F" + row + ")";
                                    ws.Cells[col++, row].Value = "=($AB$8*$H$8*F" + row + "*X" + row + "*(I" + row + "-0.5*X" + row + ")+$H$10*(Q" + (row + 1) + "*PI()*R" + (row + 1) + "^2/4)*(I" + row + "-H" + (row + 1) + "))*10^-6";

                                }
                                else
                                {
                                    ws.Cells[col++, row].Value = "=($H$10*V" + row + "-$H$10*(Q" + (row - 1) + "*PI()*R" + (row - 1) + "^2/4))/($AB$8*$H$8*F" + row + ")";
                                    ws.Cells[col++, row].Value = "=($AB$8*$H$8*F" + (row) + "*X" + row + "*(I" + row + "-0.5*X" + row + ")+$H$10*(Q" + (row - 1) + "*PI()*R" + (row - 1) + "^2/4)*(I" + (row - 2) + "-H" + (row - 1) + "))*10^-6";

                                }
                            }
                            else
                            {
                                ws.Cells[col++, row].Value = "=($H$10*V" + row + "-$H$10*MIN((Q" + (row - 1) + "*PI()*R" + (row - 1) + "^2/4),(Q" + (row + 1) + "*PI()*R" + (row + 1) + "^2/4)))/($AB$8*$H$8*F" + (row) + ")";
                                ws.Cells[col++, row].Value = "=($AB$8*$H$8*F" + row + "*X" + row + "*(I" + row + "-0.5*X" + row + ")+$H$10*MIN((Q" + (row - 1) + "*PI()*R" + (row - 1) + "^2/4),(Q" + (row + 1) + "*PI()*R" + (row + 1) + "^2/4))*(I+ row +-MIN(H" + (row - 1) + ",H" + (row + 1) + ")))*10^-6";
                            }
                            ws.Cells[col++, row].Value = "=IF(ABS(J" + row + ")<Y" + row + ",\"Ok\",\"Not Ok\")";
                            ws.Cells[col++, row].Value = "=(V" + row + ")/(F" + row + "*G" + row + ")";
                            ws.Cells[col++, row].Value = "=$AB$10*$H$8/$H$10";
                            ws.Cells[col++, row++].Value = "=IF(AND(AA16>0.1%,AA" + row + "<AB" + row + "),\"Ok\",\"No - Ok\")";
                        }
                    }
                }
                Byte[] bin = p.GetAsByteArray();
                File.WriteAllBytes(fileSave, bin);
            }
        }

    }

}
