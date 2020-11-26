using GoodLuck.Model.Beam;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.ExcelModel
{
    public class ExportToExcel
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
                ws.Cells["D2"].Value = beamViewModel.BeamDesignFulls[0].BeamDesigns[0].BeamProperties.Story;
                ws.Cells["D4"].Value = beamViewModel.ProjectInformation.ProjectName;
                ws.Cells["D5"].Value = beamViewModel.ProjectInformation.Address;
                ws.Cells["D6"].Value = beamViewModel.ProjectInformation.EngineerName;
                ws.Cells["F8"].Value = beamViewModel.BeamDesignFulls[0].BeamModel.ConcreteProperties.ConcreteName;
                ws.Cells["F10"].Value = beamViewModel.BeamDesignFulls[0].BeamModel.SteelPropertiesMaster.SteelName;
                ws.Cells["AB8"].Value = beamViewModel.BeamDesignFulls[0].BeamModel.Yb;
                ws.Cells["AB9"].Value = beamViewModel.BeamDesignFulls[0].BeamModel.A;

                int row = 16;
                foreach (BeamDesignFull designFull in beamViewModel.BeamDesignFulls)
                {
                    // ws.Cells[col, row, col, row + designFull.BeamDesigns.Count * 3 - 1].Merge = true;
                    ws.Cells[row, 3].Value = designFull.NameBeam;

                    foreach (BeamDesign beam in designFull.BeamDesigns)
                    {
                        //  ws.Cells[col, row, col, row + 3 - 1].Merge = true;
                        ws.Cells[row, 4].Value = beam.NameNumber;
                        foreach (ReinforcementBeamMaster beamMaster in beam.ReinforcementBeamMasters)
                        {
                            try
                            {
                                int col = 5;
                                ws.Cells[row, col++].Formula = "=ROUND(" + beamMaster.Location + ",2)";
                                ws.Cells[row, col++].Value = beam.BeamProperties.Width;
                                ws.Cells[row, col++].Value = beam.BeamProperties.Depth;
                                ws.Cells[row, col++].Formula = "=(Q" + row + "*PI()*R" + row + "^2/4*($AB$9+R" + row + "/2)+T" + row + "*PI()*U" + row + "^2/4*($AB$9+R" + row + "+R" + row + "+U" + row + "/2))/(Q" + row + "*PI()*R" + row + "^2/4+T" + row + "*PI()*U" + row + "^2/4)";
                                ws.Cells[row, col++].Formula = "=G" + row + "-H" + row;
                                ws.Cells[row, col++].Value = beamMaster.MFormat;
                                ws.Cells[row, col++].Formula = "=ABS(J" + row + ")*10^6/($AB$8*$H$8*F" + row + "*I" + row + "^2)";
                                ws.Cells[row, col++].Formula = "=ROUND(1-SQRT(1-2*K" + row + "),3)";
                                ws.Cells[row, col++].Formula = "=1-0.5*L" + row + "";
                                ws.Cells[row, col++].Formula = "=IF(L" + row + "<$AB$10,\"Ok\",\"Not Ok\")";
                                ws.Cells[row, col++].Formula = "=MAX(ABS(J" + row + ")*10^6/($H$10*M" + row + "*I" + row + "),0.001*F" + row + "*I" + row + ")";
                                ws.Cells[row, col++].Formula = "=(O" + row + ")/(F" + row + "*I" + row + ")";
                                ws.Cells[row, col++].Value = beamMaster.ChooseSteel1.Quantity;
                                ws.Cells[row, col++].Value = beamMaster.ChooseSteel1.Phi;
                                ws.Cells[row, col++].Value = "+";
                                ws.Cells[row, col++].Value = beamMaster.ChooseSteel2.Quantity;
                                ws.Cells[row, col++].Value = beamMaster.ChooseSteel2.Phi;
                                ws.Cells[row, col++].Formula = "=(Q" + row + "*PI()*R" + row + "^2/4+T" + row + "*PI()*U" + row + "^2/4)";
                                ws.Cells[row, col++].Formula = "=IF(V" + row + ">O" + row + ",\"Ok\",\"Not Ok\")";
                                if (beamMaster.M <= 0)
                                {
                                    if (beamMaster == beam.ReinforcementBeamMasters[0])
                                    {
                                        ws.Cells[row, col++].Formula = "=($H$10*V" + row + "-$H$10*(Q" + (row + 1) + "*PI()*R" + (row + 1) + "^2/4))/($AB$8*$H$8*F" + row + ")";
                                        ws.Cells[row, col++].Formula = "=($AB$8*$H$8*F" + row + "*X" + row + "*(I" + row + "-0.5*X" + row + ")+$H$10*(Q" + (row + 1) + "*PI()*R" + (row + 1) + "^2/4)*(I" + row + "-H" + (row + 1) + "))*10^-6";

                                    }
                                    else
                                    {
                                        ws.Cells[row, col++].Formula = "=($H$10*V" + row + "-$H$10*(Q" + (row - 1) + "*PI()*R" + (row - 1) + "^2/4))/($AB$8*$H$8*F" + row + ")";
                                        ws.Cells[row, col++].Formula = "=($AB$8*$H$8*F" + row + "*X" + row + "*(I" + row + "-0.5*X" + row + ")+$H$10*(Q" + (row - 1) + "*PI()*R" + (row - 1) + "^2/4)*(I" + row + "-H" + row + "))*10^-6";

                                    }
                                }
                                else
                                {
                                    ws.Cells[row, col++].Formula = "=($H$10*V" + row + "-$H$10*MIN((Q" + (row - 1) + "*PI()*R" + (row - 1) + "^2/4),(Q" + (row + 1) + "*PI()*R" + (row + 1) + "^2/4)))/($AB$8*$H$8*F" + (row) + ")";
                                    ws.Cells[row, col++].Formula = "=($AB$8*$H$8*F" + row + "*X" + row + "*(I" + row + "-0.5*X" + row + ")+$H$10*(Q" + (row + 1) + "*PI()*R" + (row + 1) + "^2/4)*(I" + row + "-H" + (row + 1) + "))*10^-6";
                                }
                                ws.Cells[row, col++].Formula = "=IF(ABS(J" + row + ")<Y" + row + ",\"Ok\",\"Not Ok\")";
                                ws.Cells[row, col++].Formula = "=(V" + row + ")/(F" + row + "*G" + row + ")";
                                ws.Cells[row, col++].Formula = "=$AB$10*$H$8/$H$10";
                                ws.Cells[row, col++].Formula = "=IF(AND(AA" + row + ">0.1%,AA" + row + "<AB" + row + "),\"Ok\",\"No - Ok\")";
                                row++;
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }


                foreach (BeamDesignFull designFull in beamViewModel.BeamDesignFulls)
                {
                    try
                    {
                        string nameWs = "Dam " + designFull.NameBeam;
                        ExcelWorksheet worksheet;
                        int i = 1;
                        while (true)
                        {
                            try
                            {
                                worksheet = p.Workbook.Worksheets.Add(nameWs, p.Workbook.Worksheets["Cotdai"]);
                                break;
                            }
                            catch (Exception)
                            {
                                nameWs += "1";
                            }
                        }

                        //= p.Workbook.Worksheets.Add(nameWs, p.Workbook.Worksheets["Cotdai"]);
                        worksheet.Cells["F1"].Value = beamViewModel.ProjectInformation.ProjectName;
                        worksheet.Cells["H1"].Value = beamViewModel.ProjectInformation.Address;
                        worksheet.Cells["L1"].Value = DateTime.Today.ToString();
                        worksheet.Cells["C7"].Value = designFull.NameBeam;
                        worksheet.Cells["L8"].Value = designFull.BeamDesigns[0].BeamProperties.Width;
                        worksheet.Cells["L9"].Value = designFull.BeamDesigns[0].BeamProperties.Depth;
                        worksheet.Cells["L10"].Value = designFull.BeamModel.A;
                        worksheet.Cells["L12"].Formula = "=ROUND(" + designFull.Qmax / 1000 + ",2)";
                        worksheet.Cells["L13"].Value = designFull.Pmax / 1000;
                        worksheet.Cells["L16"].Value = designFull.BeamModel.ConcreteProperties.ConcreteName;
                        worksheet.Cells["L20"].Value = designFull.BeamModel.SteelPropertiesBelt.SteelName;
                        worksheet.Cells["L24"].Value = designFull.ReinforcementBeamBelt.ChooseSteel.Phi;
                        worksheet.Cells["L25"].Value = designFull.ReinforcementBeamBelt.ChooseSteel.Quantity;
                        worksheet.Cells["L26"].Value = designFull.ReinforcementBeamBelt.S;
                    }
                    catch (Exception) { }
                }
                p.Workbook.Worksheets.Delete("Cotdai");
                Byte[] bin = p.GetAsByteArray();
                File.WriteAllBytes(fileSave, bin);
            }
        }

    }

}
