using System;
using System.Data;
using System.IO;
using OfficeOpenXml;

namespace ManagerStudent.DAL
{
    public class ConnectExcel
    {
        public ConnectExcel()
        {
        }

        public static void ExportDataToExcel(string path, DataTable data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var newFile = new FileInfo(path);

            using (var package = new ExcelPackage(newFile))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                int rowCount = data.Rows.Count;
                int columnCount = data.Columns.Count;

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        worksheet.Cells[i + 1, j + 1].Value = data.Rows[i][j].ToString();
                    }
                }

                package.Save();
            }
        }

        public static DataTable ImportDataFromExcel(string path, string sheetName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var existingFile = new FileInfo(path);

            using (var package = new ExcelPackage(existingFile))
            {
                var worksheet = package.Workbook.Worksheets[sheetName];

                int rows = worksheet.Dimension.Rows;
                int columns = worksheet.Dimension.Columns;

                var importedData = new DataTable();

                for (int col = 1; col <= columns; col++)
                {
                    importedData.Columns.Add(worksheet.Cells[1, col].Value.ToString());
                }

                for (int row = 2; row <= rows; row++)
                {
                    DataRow dataRow = importedData.NewRow();
                    for (int col = 1; col <= columns; col++)
                    {
                        dataRow[col - 1] = worksheet.Cells[row, col].Value.ToString();
                    }
                    importedData.Rows.Add(dataRow);
                }

                return importedData;
            }
        }
    }
}
