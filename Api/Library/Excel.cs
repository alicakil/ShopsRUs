using Microsoft.AspNetCore.Mvc;

namespace Api.Library
{
    public class Excel
    {
        public ActionResult DownloadExcel(string FileName, dynamic mydata)
        {
            FileName += "_" + (DateTime.Now.Year - 2000) + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute;

            string root = System.IO.Path.GetTempPath();

            string filePath = root + @"\" + FileName + ".xlsx";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                System.Threading.Thread.Sleep(1000);
            }

            // excel creataion
            var workbook = new ClosedXML.Excel.XLWorkbook();
            workbook.AddWorksheet("MysheetName");
            var ws = workbook.Worksheet("MysheetName");

            int rowno = 1;
            int colno = 1;

            // Header
            foreach (var prop in mydata[0].GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                string title = prop.Name;
                ws.ColumnWidth = 26;
                ws.Cell(1, colno).Value = title;
                ws.ActiveCell = ws.Cell(1, colno);
                ws.ActiveCell.Style.Font.Bold = true;
                ws.ActiveCell.Style.Fill.SetBackgroundColor(ClosedXML.Excel.XLColor.Almond);
                ws.ActiveCell.Style.Border.BottomBorder = ClosedXML.Excel.XLBorderStyleValues.Thick;

                colno++;
            }
            colno = 1;
            rowno++;

            // Data
            foreach (var row in mydata)
            {
                foreach (var prop in row.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                {
                    string title = prop.Name;
                    string val = prop.GetValue(row, null).ToString();

                    ws.Cell(rowno, colno).Value = val;
                    colno++;
                }
                colno = 1;
                rowno++;
            }



            workbook.SaveAs(filePath);


            if (!System.IO.File.Exists(filePath))
                return null;

            byte[] filebytes = System.IO.File.ReadAllBytes(filePath);
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new Microsoft.AspNetCore.Mvc.FileContentResult(filebytes, "application/octet-stream") { FileDownloadName = FileName + ".xlsx" };
        }


    }
}
