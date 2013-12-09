using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StudyingController.Common
{
    public class ExportHelper
    {
        public static void ExportToExcelWithHeader(string title, List<List<string>> data, List<string> header)
        {
            Microsoft.Office.Interop.Excel.Application objApp;
            Microsoft.Office.Interop.Excel._Workbook objBook;
            Microsoft.Office.Interop.Excel.Workbooks objBooks;
            Microsoft.Office.Interop.Excel.Sheets objSheets;
            Microsoft.Office.Interop.Excel._Worksheet objSheet;
            Microsoft.Office.Interop.Excel.Range range;

            try
            {
                objApp = new Microsoft.Office.Interop.Excel.Application();
                objBooks = objApp.Workbooks;
                objBook = objBooks.Add(Missing.Value);
                objSheets = objBook.Worksheets;
                objSheet = (Microsoft.Office.Interop.Excel._Worksheet)objSheets.get_Item(1);

                objSheet.Name = title;

                range = objSheet.get_Range("A1", Missing.Value);
                range.get_Resize(1, header.Count);
                range.EntireRow.Font.Bold = true;

                for (int col = 0; col < header.Count; col++)
                    range.set_Item(1, col + 1, header[col]);
                range = objSheet.get_Range("A2", Missing.Value);
                //range.get_Resize(data.First().Count, data.Count);

                for (int row = 0; row < data.Count; row++)
                    for (int col = 0; col < data.First().Count; col++)
                        range.set_Item(row+1, col+1, data[row][col]);

                objApp.Visible = true;
                objApp.UserControl = true;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
