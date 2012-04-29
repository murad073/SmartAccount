using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace GKS.Model
{
    public static class ExportManager
    {
        public static void ExportToExcel(object[,] data)
        {
#if SILVERLIGHT
      dynamic excel = AutomationFactory.CreateObject("Excel.Application");
#else
            dynamic excel = Microsoft.VisualBasic.Interaction.CreateObject("Excel.Application", string.Empty);
#endif

            excel.ScreenUpdating = false;
            dynamic workbook = excel.workbooks;
            workbook.Add();

            dynamic worksheet = excel.ActiveSheet;

            const int left = 1;
            const int top = 1;
            int height = data.GetLength(0);
            int width = data.GetLength(1);
            int bottom = top + height - 1;
            int right = left + width - 1;

            if (height == 0 || width == 0)
                return;

            dynamic rg = worksheet.Range[worksheet.Cells[top, left], worksheet.Cells[bottom, right]];
#if SILVERLIGHT
      //With setting range value for recnagle export will be fast, but this aproach doesn't work in Silverlight
      for (int i = 1; i <= height; i++)
      {
        object[] row = new object[width];
        for (int j = 1; j <= width; j++)
        {
          row[j - 1] = data[i - 1, j - 1];
        }
        dynamic r = worksheet.Range[worksheet.Cells[i, left], worksheet.Cells[i, right]];
        r.Value = row;
        r = null;
      }
#else
            rg.Value = data;
#endif

            // Set borders
            for (int i = 1; i <= 4; i++)
                rg.Borders[i].LineStyle = 1;

            // Set auto columns width
            rg.EntireColumn.AutoFit();

            // Set header view
            dynamic rgHeader = worksheet.Range[worksheet.Cells[top, left], worksheet.Cells[top, right]];
            rgHeader.Font.Bold = true;
            rgHeader.Interior.Color = 189 * (int)Math.Pow(16, 4) + 129 * (int)Math.Pow(16, 2) + 78; // #4E81BD

            // Show excel app
            excel.ScreenUpdating = true;
            excel.Visible = true;

#if SILVERLIGHT
#else
            Marshal.ReleaseComObject(rg);
            Marshal.ReleaseComObject(rgHeader);
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excel);
#endif
            rg = null;
            rgHeader = null;
            worksheet = null;
            workbook = null;
            excel = null;
            GC.Collect();
        }
    }
}
