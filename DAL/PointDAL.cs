using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class PointDAL
    {
        public bool UpdateStudentPoint(int studentID, string academicyearName, string semesterName,
                                        string subjectName, double regularPoint, double midtermPoint, double finalPoint)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string query = @"EXEC UpdatePoint @studentID, @academicyearName, @semesterName, 
                                      @subjectName, @regularPoint, @midtermPoint, @finalPoint";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentID", studentID);
                command.Parameters.AddWithValue("@academicyearName", academicyearName);
                command.Parameters.AddWithValue("@semesterName", semesterName);
                command.Parameters.AddWithValue("@subjectName", subjectName);
                command.Parameters.AddWithValue("@regularPoint", regularPoint);
                command.Parameters.AddWithValue("@midtermPoint", midtermPoint);
                command.Parameters.AddWithValue("@finalPoint", finalPoint);
                int rowsAffected = command.ExecuteNonQuery();
                // Trả về true nếu cập nhật thành công
                /*return rowsAffected > 0;*/
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                return false;
            }
            return true;
        }

        public bool InsertStudentPoint(int studentID, string academicyearName, string semesterName,
                                string subjectName, string pointName, double point)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string query = @"EXEC InsertPoint @studentID, @academicyearName, @semesterName, 
                    @subjectName, @pointName, @point";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentID", studentID);
                command.Parameters.AddWithValue("@academicyearName", academicyearName);
                command.Parameters.AddWithValue("@semesterName", semesterName);
                command.Parameters.AddWithValue("@subjectName", subjectName);
                command.Parameters.AddWithValue("@pointName", pointName);
                command.Parameters.AddWithValue("@point", point);

                int rowsAffected = command.ExecuteNonQuery();
                // Trả về true nếu cập nhật thành công
                /*return rowsAffected > 0;*/
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                return false;
            }
            return true;
        }

        public DataTable GetStudentPoints(string academicYearName, string semesterName, string className, string subjectName)
        {
            DataTable dataTable = new DataTable();
            {
                try
                {
                    SqlConnection connection = initConnect.ConnectToDatabase();
                    string query = "EXEC DATA_POINT @academicYearName, @semesterName, @className, @subjectName";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@subjectName", subjectName);
                    command.Parameters.AddWithValue("@academicYearName", academicYearName);
                    command.Parameters.AddWithValue("@semesterName", semesterName);
                    command.Parameters.AddWithValue("@className", className);
                    Console.WriteLine("vo chua?");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dataTable;
            }
        }

        public List<AcademicYear> GetAcademicYears()
        {
            List<AcademicYear> academicYears = new List<AcademicYear>();

            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                // Truy vấn cơ sở dữ liệu để lấy dữ liệu các năm học
                string query = "SELECT * FROM AcademicYear";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Nam hoc vao combobox");
                while (reader.Read())
                {
                    // Đọc dữ liệu từ SqlDataReader và tạo các đối tượng AcademicYearDTO tương ứng

/*                    academicYears.Add(new AcademicYear(
                    (int)reader.GetSqlInt32(reader.GetOrdinal("ID")),
                    reader.GetSqlString(reader.GetOrdinal("academicyearName")).ToString(),
                    (DateTime)reader.GetSqlDateTime(reader.GetOrdinal("startDate")),
                    (DateTime)reader.GetSqlDateTime(reader.GetOrdinal("finishDate"))
                    ));
                    {*/
                    // Đọc dữ liệu từ SqlDataReader và tạo các đối tượng AcademicYearDTO tương ứng
                    AcademicYear academicYear = new AcademicYear();
                    academicYear.ID = (int)reader["ID"];
                    academicYear.Name = (string)reader["academicyearName"];
                    academicYear.startDate = (DateTime)reader["startDate"];
                    academicYear.finishDate = (DateTime)reader["finishDate"];
                    academicYears.Add(academicYear);
                }
                reader.Close();
                connection.Close();
            }

            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine(ex.Message);
            }
            return academicYears;
        }
        public DataTable GetallClasses()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = initConnect.ConnectToDatabase();
                string query = "SELECT * FROM Class";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        public DataTable GetallSemester()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = initConnect.ConnectToDatabase();
                string query = "SELECT * FROM Semester";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        public DataTable GetallSuject()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = initConnect.ConnectToDatabase();
                string query = "SELECT * FROM Subject";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }
        public DataTable GetStudentsNameandID(int academicYearID, int semesterID, int classID)
        {
            DataTable dataTable = new DataTable();
            {
                try
                {
                    SqlConnection connection = initConnect.ConnectToDatabase();
                    string query = @"SELECT studentID, name FROM StudentClassSemesterAcademicYear
                                    INNER JOIN Student s ON s.ID = studentID
                                        WHERE classID = @classID 
                                            AND semesterID = @semesterID 
                                            AND academicyearID = @academicyearID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@academicyearID", academicYearID);
                    command.Parameters.AddWithValue("@semesterID", semesterID);
                    command.Parameters.AddWithValue("@classID", classID);
                    Console.WriteLine("ma hoc sinh, ten hoc sinh vao combobox");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dataTable;
            }
        }
    }
}
