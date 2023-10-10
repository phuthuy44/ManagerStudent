/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using OfficeOpenXml;


namespace ManagerStudent.DAL
{
	public class connectExcel
	{
         
		public connectExcel()
		{
			
        }
		public static void exportDataToExcel(string path, String sheet_name, IList<IList<object>> data) {
            ExcelPackage. = LicenseContext.NonCommercial;
            // Tạo một tệp Excel mới
            //var newFile = new FileInfo("/CSharpProject/CSharpProject/CSharpProject/DAO/Excel/demo2.xlsx");
            var newFile = new FileInfo(path);
            using (var package = new ExcelPackage(newFile))
            {
                // Tạo một trang tính (worksheet) mới
                // Kiểm tra xem trang tính có tồn tại trong tệp Excel không
                bool sheetExists = package.Workbook.Worksheets.Any(sheet => sheet.Name == sheet_name);
                if (sheetExists)
                {
                    // Delete the worksheet
                    package.Workbook.Worksheets.Delete(package.Workbook.Worksheets[sheet_name]);
                }
                var worksheet = package.Workbook.Worksheets.Add(sheet_name);
                int i = 0;
                foreach (var rows in data)
                {
                    i++;
                    int j = 0;
                    foreach (var col in rows)
                    {
                        j++;
                        worksheet.Cells[i, j].Value = $"{col} "; 
                    }
                }
                // Lưu tệp Excel
                package.Save();
            }
        }
        //    public static IList<IList<object>> importDataFromExcel(string path)
        //    {
        //        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    }
    }
}

*/