using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ManagerStudent.DAL
{
    internal class StudentDAL
    {
        //Hien thi len data len DataGridView
        public DataTable GetListStudent()
        {
            DataTable dataTable = new DataTable();
            string sql = " SELECT * FROM Student";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(dataTable);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                conn.Close();
            }
            return dataTable;
        }
        public bool insertStudent(Student student)
        {
           // string filename = Path.GetFileName(?);
            string sql= "INSERT INTO Student (name,birthday,gender,address, email, numberPhone, image) Values(@Hoten,@NgaySinh,@gioitinh,@diaChi,@email,@soDienThoai,@HinhAnh)";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Hoten", student.Name);
                cmd.Parameters.AddWithValue("@NgaySinh", student.Birthday);
                cmd.Parameters.AddWithValue("@gioitinh", student.Gender);
                cmd.Parameters.AddWithValue("@diaChi", student.Address);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@soDienThoai", student.Phone);
                cmd.Parameters.AddWithValue("@HinhAnh","\\Image\\HocSinh\\"+ student.Image);
                cmd.ExecuteNonQuery();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }
        public bool deleteStudent(string idStudent,out bool isLoiKhoaNgoai)
        {
            string sql = "DELETE FROM Student where id = @idStudent";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
               SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idStudent", idStudent);
                cmd.ExecuteNonQuery ();
                isLoiKhoaNgoai = false;
                return true;
            }
            catch (SqlException ex)
            {
               if(ex.Number == 547)
                {
                    Console.WriteLine("Lỗi:Không thể xóa học sinh này vì có khóa ngoại tham chiếu");
                    isLoiKhoaNgoai = true;
                }
                else
                {
                    Console.WriteLine("Lỗi:" + ex.Message);
                    isLoiKhoaNgoai=false;
                }
               return false;
            }
            finally { conn.Close(); }
        }
        public bool updateStudent(Student student)
        {
            string sql = "update Student set name=@HoTen,birthday=@ngaySinh,gender=@gioiTinh,address=@diaChi,email=@Email,numberPhone=@soDienThoai,image=@HinhAnh WHERE id = @id";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HoTen", student.Name);
                cmd.Parameters.AddWithValue("@ngaySinh", student.Birthday);
                cmd.Parameters.AddWithValue("@gioiTinh", student.Gender);
                cmd.Parameters.AddWithValue("@diaChi", student.Address);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@soDienThoai", student.Phone);
                cmd.Parameters.AddWithValue("@HinhAnh", "\\Image\\HocSinh\\" + student.Image);
                cmd.Parameters.AddWithValue("@id", student.ID);
                cmd.ExecuteNonQuery();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine("Lỗi:"+ ex.Message);
                return false;
            }
            finally { conn.Close(); }  

        }
        /*Danh cho View Quan he*/
        /*Fill khoi*/
        /*PhanLop */
        public List<StudentClassSemesterAcademicYear> getAcademicYearsInAssignmentClass()
        {
            List<StudentClassSemesterAcademicYear> academicYears = new List<StudentClassSemesterAcademicYear>();
            string sql = "select academicYearID from StudentClassSemesterAcademicYear";//AcademicYear$
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudentClassSemesterAcademicYear academic = new StudentClassSemesterAcademicYear();
                    academic.academicyearID = reader.GetInt32(0);
                    academicYears.Add(academic);
                }
            }catch(Exception ex)
            {
                return null;
            }
            finally { 
                conn.Close(); 
            }
            return academicYears;
        }
        //Chuyen maNam thanh tenNam
        public string getAcademicName(int idAcademic)
        {
            string getAcademicName = null;
            string sql = "Select AcademicyearName from AcademicYear where ID = @ID ";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", idAcademic);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    getAcademicName = reader.GetString(0);
                }
            }
            catch(Exception ex)
            {

            }
            finally { conn.Close(); }
            return getAcademicName;
        }
        //Chuyen teNam thanh maNam
        public int getIDAcademic(string name)
        {
            int idAcademic = 0 ;
            string sql = "Select ID from AcademicYear where AcademicyearName = @name";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    idAcademic = reader.GetInt32(0);
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return idAcademic;
        }
        public List<StudentClassSemesterAcademicYear> getGrade(int idYear)
        {
            List<StudentClassSemesterAcademicYear> grade = new List<StudentClassSemesterAcademicYear>();
            string sql = "select gradeID from StudentClassSemesterAcademicYear where academicyearID = @id"; ;
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@id", idYear);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    StudentClassSemesterAcademicYear grades = new StudentClassSemesterAcademicYear();
                    grades.gradeID = reader.GetInt32(0);
                    grade.Add(grades);
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
            return grade;
        }
        public string namGrade(int id)
        {
            string name = null;
            string sql = "select gradeName from grade where ID = @id";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    name = reader.GetString(0);
                }
            }
            catch(Exception ex) { return null; }
            finally { conn.Close(); }
            return name;
        }
        public List<Class> getClassInGrade(string maKhoi)
        {
            List <Class> classes = new List<Class>();
            string sql = "SELECT * FROM class$ where gradeID= @maKhoi";//class$
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@maKhoi", maKhoi);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                   Class cls = new Class();
                    cls.Name = reader["className"].ToString();
                    cls.gradeID = reader["gradeID"].ToString() ;
                    classes.Add(cls);

                }
            }catch(Exception ex)
            {
                return null;
            }
            finally { conn.Close(); }
            return classes;
        }
        public string getMaGrade(string tenKhoi)
        {
            string gradeID = null;
            string sql = "select ID from grade$ where gradeName = @tenKhoi";//grade$
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tenKhoi", tenKhoi);
                SqlDataReader r = cmd.ExecuteReader();
                if(r.Read())
                {
                    gradeID = r.GetString(0);
                }

            }
            catch(Exception ex) { return null; }
            finally
            {
                conn.Close();
            }
            return gradeID;
        }
    }
}
