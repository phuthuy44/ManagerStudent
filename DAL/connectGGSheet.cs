using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;


namespace ManagerStudent.DAL
{
    public class ConnectGGSheet
    {
        public static SheetsService GetSheetsService()
        {
            // Đường dẫn đến tệp JSON chứa thông tin xác thực OAuth 2.0
            string credentialFilePath = "./DAL/credentials.json";

            // Xác thực với tệp JSON
            GoogleCredential credential;
            using (var stream = new System.IO.FileStream(credentialFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(SheetsService.Scope.Spreadsheets);
            }

            // Tạo dịch vụ Google Sheets
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Demo",
            });

            return service;
        }

        static readonly string spreadsheetId = "1KJ0htYeD5OqBNWKdndyu-W3JLNvXjdtEz5tTz-fAdDE";

        public static IList<IList<object>> ReadDataFromGoogleSheets(String sheet_name)
        {
            try
            {
                var sheetsService = GetSheetsService();
                string range = sheet_name; // Vùng dữ liệu cần đọc
                SpreadsheetsResource.ValuesResource.GetRequest request =
                    sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);

                ValueRange response = request.Execute();
                IList<IList<object>> values = response.Values;

                if (values != null && values.Count > 0)
                {
                    Console.WriteLine("Hoàn thành.");
                    return values;
                }
                else
                {
                    Console.WriteLine("Không tìm thấy dữ liệu.");
                    return new List<IList<object>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc dữ liệu từ Google Sheets: " + ex.Message);
            }
            return new List<IList<object>>();
        }

        public static void WriteDataToGoogleSheets(string sheet_name, List<IList<object>> valuesToWrite)
        {
            try
            {
                var sheetsService = GetSheetsService();

                // Định dạng vùng dữ liệu bạn muốn ghi vào
                string range = sheet_name ; // Vùng dữ liệu cần ghi

                // Tạo đối tượng ValueRange để chứa dữ liệu cần ghi
                var valueRange = new ValueRange
                {
                    Values = valuesToWrite
                };

                // Gọi API để cập nhật dữ liệu
                SpreadsheetsResource.ValuesResource.UpdateRequest request =
                    sheetsService.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
                request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

                UpdateValuesResponse response = request.Execute();
                Console.WriteLine("Dữ liệu đã được ghi vào Google Sheets.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi ghi dữ liệu vào Google Sheets: " + ex.Message);
            }
        }
    }
}


