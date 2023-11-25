using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ManagerStudent.DTO;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Data.SqlTypes;

namespace ManagerStudent.DAL
{
    internal class TeacherDAL : initConnect
    {
        
        public DataTable GetListTeacher()
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT
                    t.ID as N'Mã GV',
                    t.teacherName as N'Tên Giáo viên',
                    t.gender as N'Giới tính',
                    t.birthday as N'Ngày sinh',
                    t.email,
                    t.phonenumber as N'Số điện thoại',
                    t.address as N'Địa chỉ',
                    t.image as N'Hình ảnh',
                    STRING_AGG(s.subjectName, ', ') WITHIN GROUP (ORDER BY s.subjectName) AS N'Chuyên môn'
                FROM
                    Teacher AS t
                JOIN
                    SubjectOfTeacher AS s2 ON t.ID = s2.teacherID
                JOIN
                    Subject AS s ON s2.subjectID = s.ID
                GROUP BY
                    t.ID, t.teacherName, t.gender, t.birthday, t.email, t.phonenumber, t.address, t.image";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public DataTable GetSubjectTeacher()
        {
            DataTable sbteacher = new DataTable();
            string sql = @" SELECT
                    t.ID as N'Mã GV',
                    t.teacherName as N'Tên Giáo viên',
                    STRING_AGG(s.subjectName, ', ') WITHIN GROUP (ORDER BY s.subjectName) AS N'Chuyên môn'
                FROM
                    Teacher AS t
                JOIN
                    SubjectOfTeacher AS s2 ON t.ID = s2.teacherID
                JOIN
                    Subject AS s ON s2.subjectID = s.ID
                GROUP BY
                    t.ID, t.teacherName";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(sbteacher);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return sbteacher;
        }
        public bool insertTeacher(Teacher teacher)
        {
            
            string sql = "INSERT INTO Teacher (teacherName,birthday,gender,address,email, phonenumber, image) " +
                         "Values(@Hoten,@NgaySinh,@gioitinh,@diaChi,@email,@soDienThoai,@image)";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Hoten", teacher.Name);
                cmd.Parameters.AddWithValue("@NgaySinh", teacher.Birthday);
                cmd.Parameters.AddWithValue("@gioitinh", teacher.Gender);
                cmd.Parameters.AddWithValue("@diaChi", teacher.Address);
                cmd.Parameters.AddWithValue("@email", teacher.Email);
                cmd.Parameters.AddWithValue("@soDienThoai", teacher.Phone);
                cmd.Parameters.AddWithValue("@image", "\\Image\\GiaoVien\\" + teacher.Image);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }
        public bool EditTeacher(Teacher teacher)
        {
            string sql = "UPDATE Teacher set teacherName=@hoten, gender=@gender,birthday=@birthday," +
                "email=@email,phonenumber=@phonenumber,address=@address,image=@image WHERE id=@id";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", teacher.ID);
                cmd.Parameters.AddWithValue("@hoten", teacher.Name);
                cmd.Parameters.AddWithValue("@birthday", teacher.Birthday);
                cmd.Parameters.AddWithValue("@gender", teacher.Gender);
                cmd.Parameters.AddWithValue("@address", teacher.Address);
                cmd.Parameters.AddWithValue("@email", teacher.Email);
                cmd.Parameters.AddWithValue("@phonenumber", teacher.Phone);
                cmd.Parameters.AddWithValue("@image", "\\Image\\GiaoVien\\" + teacher.Image);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }
        public bool DeleteTeacher(int id)
        {
            String sql = "DELETE FROM Teacher where ID = @idteacher";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idteacher", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }
        public bool DeleteSubOfTeacher(int id)
        {
            String sql = "DELETE FROM SubjectOfTeacher where teacherID = @idteacher";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idteacher", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }
        public bool DeleteAssignment(int id)
        {
            String sql = "DELETE FROM Assignment where teacherID = @idteacher";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idteacher", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }

        public bool InsertSubOfTecher(int id1 , int id2)
        {
            string sql = "INSERT INTO SubjectOfTeacher(teacherID,subjectID) Values(@id1,@id2)";
            SqlConnection conn = initConnect.ConnectToDatabase();           
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id1", id1);
                cmd.Parameters.AddWithValue("@id2", id2);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }
        public int GetIdSubject(string sbname)
        {
            string sql = "Select id FROM Subject Where subjectName = @sbname";
            int id =new int();
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql,conn);
                cmd.Parameters.AddWithValue("@sbname", sbname);
                SqlDataReader reader = cmd.ExecuteReader();     
                if(reader.Read())
                {
                    id = reader.GetInt32(0);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return id; 
        }
        public int GetIdTeacherLast()
        {
            string sql = @" SELECT TOP 1 ID
                            FROM Teacher
                            ORDER BY ID DESC";
            int id = new int();
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql,conn);
                SqlDataReader r = cmd.ExecuteReader();
                if(r.Read())
                {
                    id= r.GetInt32(0);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return id;
        }
    }
}
