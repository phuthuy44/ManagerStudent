using ManagerStudent.DTO;
using System;
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
    }
}
