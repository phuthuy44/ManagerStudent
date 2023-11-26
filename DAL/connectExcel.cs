using System;
using System.Data;
using System.IO;
using ExcelDataReader;
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

        public static DataTable ImportExcelToDataTable(string filePath)
        {
            // Tạo DataTable để lưu trữ dữ liệu từ tệp Excel
            DataTable dataTable = new DataTable();

            // Sử dụng FileStream để đọc tệp Excel
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Đọc dữ liệu từ tệp Excel bằng ExcelDataReader
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Đọc dữ liệu từ Excel và lưu vào DataTable
                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // Sử dụng hàng đầu tiên làm tên cột
                        }
                    });

                    if (result.Tables.Count > 0)
                    {
                        dataTable = result.Tables[0]; // Lấy DataTable đầu tiên từ DataSet
                    }
                }
            }

            return dataTable;
        }
    }
}
