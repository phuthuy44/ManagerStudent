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
        private SqlConnection connection;
        public bool UpdateStudentPoint(string studentName, int regularPoint, int midtermPoint, int finalPoint)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string query = @"UPDATE Point 
                         SET Point = @regularPoint 
                         WHERE StudentID = (SELECT ID FROM Student WHERE name = @studentName) 
                         AND TypeOfPointID = 1;

                         UPDATE Point 
                         SET Point = @midtermPoint 
                         WHERE StudentID = (SELECT ID FROM Student WHERE name = @studentName) 
                         AND TypeOfPointID = 2;

                         UPDATE Point 
                         SET Point = @finalPoint 
                         WHERE StudentID = (SELECT ID FROM Student WHERE name = @studentName) 
                         AND TypeOfPointID = 3;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentName", studentName);
                command.Parameters.AddWithValue("@regularPoint", regularPoint);
                command.Parameters.AddWithValue("@midtermPoint", midtermPoint);
                command.Parameters.AddWithValue("@finalPoint", finalPoint);
                int rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
                return false;
            }
            return true;
        }

        public DataTable GetStudentPoints(int academicYearID, int semesterID, int classID, int subjectID)
        {
            DataTable dataTable = new DataTable();
            {
                try
                {
                    SqlConnection connection = initConnect.ConnectToDatabase();
                    string query = @"select s.name AS 'Tên học sinh', p.point AS 'Điểm thường xuyên', p1.point AS 'Điểm giữa kì', p2.point AS 'Điểm cuối kì'
                        FROM StudentClassSemesterAcademicYear scsay
                            LEFT JOIN Student s ON s.ID = scsay.StudentID
                            LEFT JOIN Class c ON c.ID = scsay.ClassID
                            LEFT JOIN Semester s2 ON s2.ID = scsay.SemesterID
                            LEFT JOIN AcademicYear ay ON ay.ID = scsay.AcademicYearID
                            LEFT JOIN Point p ON p.StudentID = scsay.StudentID
                                AND p.AcademicYearID = scsay.AcademicYearID
                                AND p.SemesterID = scsay.SemesterID 
                            LEFT JOIN TypeOfPoint top2 ON top2.ID = p.TypeOfPointID
                            LEFT JOIN Point p1 ON p1.StudentID = scsay.StudentID
                                AND p1.AcademicYearID = scsay.AcademicYearID
                                AND p1.SemesterID = scsay.SemesterID
                            LEFT JOIN TypeOfPoint top3 on top3.ID = p1.typeofpointID
                            LEFT JOIN Point p2 ON p2.StudentID = scsay.StudentID
                                AND p2.AcademicYearID = scsay.AcademicYearID
                                AND p2.SemesterID = scsay.SemesterID
                            LEFT JOIN TypeOfPoint top4 on top4.ID = p2.typeofpointID
                            LEFT JOIN Subject s3 ON s3.ID = p.SubjectID
                            WHERE s3.ID = @sujectID
                                AND ay.ID = @academicyearID
                                AND s2.ID = @semesterID
                                AND c.ID = @classID
                                AND p.typeofpointID = 1
                                AND p1.typeofpointID = 2
                                AND p2.typeofpointID = 3";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@sujectID", subjectID);
                    command.Parameters.AddWithValue("@academicyearID", academicYearID);
                    command.Parameters.AddWithValue("@semesterID", semesterID);
                    command.Parameters.AddWithValue("@classID", classID);
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

        public List<Student> GetStudentsNameandID()
        {
            List<Student> students = new List<Student>();

            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string query = "SELECT ID, name FROM Student";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Ma hs, Ten hs vao comboBox");
                while (reader.Read())
                {
                    Student student = new Student();
                    student.ID = (int)reader["ID"];
                    student.Name = (string)reader["name"];

                    students.Add(student);
                }
                reader.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return students;
        }
    }
}
