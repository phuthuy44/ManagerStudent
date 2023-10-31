using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;


namespace ManagerStudent.DAL
{
    public class connectExcel
    {
        public connectExcel()
        {

        }

        public static void exportDataToExcel(string path, string sheetName, IList<IList<object>> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Tạo một tệp Excel mới
            var newFile = new FileInfo(path);

            using (var package = new ExcelPackage(newFile))
            {
                // Kiểm tra xem trang tính có tồn tại trong tệp Excel không
                var worksheet = package.Workbook.Worksheets.Add(sheetName);

                int i = 0;
                foreach (var row in data)
                {
                    i++;
                    int j = 0;
                    foreach (var cell in row)
                    {
                        j++;
                        worksheet.Cells[i, j].Value = cell;
                    }
                }

                // Lưu tệp Excel
                package.Save();
            }
        }

        public static IList<IList<string>> importDataFromExcel(string path, string sheetName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var existingFile = new FileInfo(path);

            using (var package = new ExcelPackage(existingFile))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];

                int rows = worksheet.Dimension.Rows;
                int columns = worksheet.Dimension.Columns;

                var importedData = new List<IList<string>>();

                for (int row = 1; row <= rows; row++)
                {

                    var rowData = new List<string>();
                    for (int col = 1; col <= columns; col++)
                    {
                        var cellValue = worksheet.Cells[row, col].Value.ToString();
                        //Console.WriteLine(cellValue);
                        rowData.Add(cellValue);
                    }
                    importedData.Add(rowData);
                }

                return importedData;
            }
        }
    }
}
