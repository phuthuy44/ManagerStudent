using System;
using System.Data.SqlClient;
namespace ManagerStudent.DAL {

    public class initConnect
    {
        // "Data Source=<tên_máy_chủ>;Initial Catalog=<tên_cơ_sở_dữ_liệu>;User ID=<tên_người_dùng>;Password=<mật_khẩu>;"
        static string connectionString = "Data Source=localhost;Initial Catalog=demo;User ID=sa;Password=reallyStrongPwd123;";

        public static SqlConnection ConnectToDatabase()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Kết nối thành công!");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối hoặc truy vấn: " + ex.Message);
                // Xử lý lỗi ở đây, có thể ném exception hoặc trả về null tùy thuộc vào yêu cầu của bạn.
                throw; // Ném exception để báo lỗi kết nối.
            }
        }

        public static void CloseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                Console.WriteLine("Đóng kết nối thành công!");
            }
        }
        public static void writeDataToTableAcademicYear(IList<IList<object>> value)
        {
            ConnectToDatabase();
            string sql = "SELECT * FROM dbo.AcademicYear";
            SqlCommand command = new SqlCommand(sql, ConnectToDatabase());
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Console.WriteLine(dataReader.GetSqlString(dataReader.GetOrdinal("ID")));
            }

        }
    }

}