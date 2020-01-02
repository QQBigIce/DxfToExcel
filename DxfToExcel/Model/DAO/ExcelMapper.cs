using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace CADToExcel.Model.DAO
{
    class ExcelMapper
    {
        public static Dictionary<string, int> WriteData(string path, Dictionary<string, int> dict)
        {
            FileInfo file = new FileInfo(path);
            Dictionary<string, int> failDic = dict;
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorksheet Ws = excelPackage.Workbook.Worksheets[1];
                for (int i = 6; (i < Ws.AutoFilterAddress.End.Row); i++)
                {
                    string tmp = "B" + i.ToString();
                    if (Ws.Cells["B" + i.ToString()].Value == null) continue;
                    foreach (KeyValuePair<string, int> kvp in dict)
                    {
                        if (Ws.Cells[tmp].Value.ToString().Equals(kvp.Key))
                        {
                            Ws.Cells['F' + i.ToString()].Value = kvp.Value;
                            failDic.Remove(kvp.Key);
                            break;
                        }
                    }                    
                }
                file = new FileInfo("D:\\20XX.XX.XX_XX村水电清单.xlsx");
                excelPackage.SaveAs(file);
            }
            return failDic;
        }


        //# 把传入的数量写入到表格
        //        def writeNum(self, num):
        //        row = self.cell.row
        //        self.ws['F' + str(row)].value = num
    }
}
