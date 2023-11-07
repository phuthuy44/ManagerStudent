using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            string sql= "INSERT INTO Student (name,birthday,gender,address, email, numberPhone, image) Values(@Hoten,@NgaySinh,@gioitinh,@diaChi,@email,@soDienThoai,@HinhAnh)";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Hoten", student.Name);
                cmd.Parameters.AddWithValue("@NgaySinh", student.Birthday);
                cmd.Parameters.AddWithValue("gioitinh", student.Gender);
                cmd.Parameters.AddWithValue("@diaChi", student.Address);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@soDienThoai", student.Phone);
                cmd.Parameters.AddWithValue("@HinhAnh", student.Image);
                cmd.ExecuteNonQuery();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }
    }
}
