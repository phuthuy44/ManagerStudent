using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class PointDAL
    {
        private SqlConnection connection;

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
        public List<Class> GetClasses()
        {
            List<Class> classes = new List<Class>();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string query = "SELECT * FROM Class";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Lop hoc vao combobox");
                while (reader.Read())
                {
                    Class classObj = new Class();
                    classObj.ID = (int)reader["ID"];
                    classObj.Name = (string)reader["className"];
                    classObj.maxStudent = (int)reader["maxStudent"];
                    classObj.realStudent = (int)reader["quantityStudent"];
                    classObj.quantityMale = (int)reader["quantityMale"];
                    classObj.quantityFemale = (int)reader["quantityFemale"];

                    classes.Add(classObj);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return classes;
        }

        public List<Semester> GetSemesters()
        {
            List<Semester> semesters = new List<Semester>();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string query = "SELECT * FROM Semester";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Hoc ki vao combobox");
                while (reader.Read())
                {
                    Semester semester = new Semester();
                    semester.ID = (int)reader["ID"];
                    semester.Name = (string)reader["semesterName"];
                    semester.Coefficient = (int)reader["coefficient"];
                    semesters.Add(semester);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return semesters;
        }

        public List<Subject> GetSubjects()
        {
            List<Subject> subjects = new List<Subject>();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string query = "SELECT * FROM Subject";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Mon hoc vao combobox");
                while (reader.Read())
                {
                    Subject subject = new Subject();
                    subject.ID = (int)reader["ID"];
                    subject.Name = (string)reader["subjectName"];

                    subjects.Add(subject);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return subjects;
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
