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
                    t.ID N'Mã GV',
                    t.teacherName N'Tên giáo viên',
                    t.gender N'Giới tính',
                    t.birthday N'Ngày sinh',
                    t.email,
                    t.phonenumber N'Số điện thoại',
                    t.address N'Địa chỉ',
                    t.image N'Hình ảnh',
                    STRING_AGG(s.subjectName, ', ') WITHIN GROUP (ORDER BY s.subjectName) AS N'Chuyên môn'
                FROM
                    Teacher t
                JOIN
                    SubjectOfTeacher s2 ON t.ID = s2.teacherID
                JOIN
                    Subject s ON s2.subjectID = s.ID
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
        public DataTable SearchAllTeacher( string s)
        {
            DataTable dt = new DataTable();
            string sql = @" SELECT
                    t.ID N'Mã GV',
                    t.teacherName N'Tên giáo viên',
                    t.gender N'Giới tính',
                    t.birthday N'Ngày sinh',
                    t.email,
                    t.phonenumber N'Số điện thoại',
                    t.address N'Địa chỉ',
                    t.image N'Hình ảnh',
                    STRING_AGG(s.subjectName, ', ') WITHIN GROUP (ORDER BY s.subjectName) AS N'Chuyên môn'
                FROM
                    Teacher t
                JOIN
                    SubjectOfTeacher s2 ON t.ID = s2.teacherID
                JOIN
                    Subject s ON s2.subjectID = s.ID
                WHERE
                    t.ID LIKE '%' + @s + '%' OR
                    t.teacherName LIKE '%' + @s + '%' OR
                    t.gender LIKE '%' + @s + '%' OR
                    CONVERT(NVARCHAR, t.birthday, 112) LIKE '%' + @s + '%' OR
                    t.email LIKE '%' + @s + '%' OR
                    CONVERT(NVARCHAR, t.phonenumber) LIKE '%' + @s + '%' OR
                    t.address LIKE '%' + @s + '%'   
                GROUP BY
                    t.ID, t.teacherName, t.gender, t.birthday, t.email, t.phonenumber, t.address, t.image";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@s", s);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(dt);


            }
            catch (Exception ex)
            {
                Console.WriteLine("SearchAllTeacher " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public DataTable GetAssignment()
        {
            DataTable sbteacher = new DataTable();
            string sql = @"SELECT ay.academicyearName N'Năm học', s2.semesterName N'Học kỳ', c.className N'Lớp', 
                                  t.teacherName N'Tên giáo viên', s.subjectName N'Môn', p.positionName N'Chức vụ'
                           FROM Teacher t, Assignment a, Class c, Position p, AcademicYear ay, Subject s, Semester s2  
                           WHERE t.ID = a.teacherID AND a.classID = c.ID AND a.positionID = p.ID  AND ay.ID = a.academicyearID 
                           AND s.ID = a.subjectID AND a.semesterID = s2.ID";
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
        public DataTable GetAssignmentTeacher( int id, string ayname, string semesName)
        {
            DataTable AoT = new DataTable();
            string sql = @"SELECT  p.positionName N'Chức vụ', c.className N'Lớp', 
                           s.subjectName N'Môn' 
                           FROM Teacher t, Assignment a, Class c, Position p, AcademicYear ay, Subject s, Semester s2  
                           WHERE t.ID = a.teacherID AND a.classID = c.ID AND a.positionID = p.ID  AND ay.ID = a.academicyearID 
                            AND s.ID = a.subjectID AND a.semesterID = s2.ID AND a.teacherID = @id AND ay.academicyearName = @ayName 
                            AND s2.semesterName = @semesName";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@ayName", ayname);
                cmd.Parameters.AddWithValue("@semesName", semesName);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(AoT);


            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAssignmentTeacher" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return AoT;
        }
        public DataTable GetAssignmentClass(string clsname, string ayname, string semesName)
        {
            DataTable AoC = new DataTable();
            string sql = @"SELECT t.teacherName N'Tên giáo viên',s.subjectName N'Môn', p.positionName N'Chức vụ' 
                           FROM Teacher t, Assignment a, Class c, Position p, AcademicYear ay, Subject s, Semester s2  
                           WHERE t.ID = a.teacherID AND a.classID = c.ID AND a.positionID = p.ID  AND ay.ID = a.academicyearID 
                           AND s.ID = a.subjectID AND a.semesterID = s2.ID AND c.className = @clsname AND ay.academicyearName = @ayName 
                           AND s2.semesterName = @semesName";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@clsname", clsname);
                cmd.Parameters.AddWithValue("@ayName", ayname);
                cmd.Parameters.AddWithValue("@semesName", semesName);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(AoC);


            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAssignmentClass" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return AoC;
        }
        public DataTable GetAcademicYear()
        {
            DataTable Ay = new DataTable();
            string sql = @" SELECT * from AcademicYear";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(Ay);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return Ay;
        }
        public DataTable GetSemester()
        {
            DataTable sm = new DataTable();
            string sql = @" SELECT * from Semester";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(sm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return sm;
        }
        public DataTable GetClass()
        {
            DataTable cls = new DataTable();
            string sql = @" SELECT * from Class";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(cls);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return cls;
        }
        public DataTable GetPosition()
        {
            DataTable ps = new DataTable();
            string sql = @" SELECT * from Position";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(ps);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ps;
        }
        public DataTable GetSubjectTeacher( int id)
        {
            DataTable sb = new DataTable();
            string sql = @"SELECT * 
                           FROM Subject s, SubjectOfTeacher sot  
                           WHERE sot.teacherID = @id AND s.ID =sot.subjectID";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(sb);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return sb;
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
        public bool DeleteTechnical(int id)
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
        public int GetIdAY(string name)
        {
            string sql = "Select id FROM AcademicYear Where academicyearName = @name";
            int id = new int();
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
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
        public int GetIdSemester(string name)
        {
            string sql = "Select id FROM Semester Where semesterName = @name";
            int id = new int();
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
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
        public int GetIdClass(string name)
        {
            string sql = "Select id FROM Class Where className = @name";
            int id = new int();
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
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
        public int GetIdPosition(string name)
        {
            string sql = "Select id FROM Position Where positionName = @name";
            int id = new int();
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
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


        public bool CheckClass( int idCls, int idSb, int idAy, int idSe)
        {
            string sql = "SELECT * FROM Assignment a WHERE classID= @idCls AND subjectID = @idSb  AND academicyearID = @idAy AND  semesterID= @idSe";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idCls", idCls );
                cmd.Parameters.AddWithValue("@idSb", idSb );
                cmd.Parameters.AddWithValue("@idAy", idAy);
                cmd.Parameters.AddWithValue("@idSe", idSe);
                SqlDataReader rd = cmd.ExecuteReader();
                if(rd.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }
        public bool CheckPosition(int idCls, int idAy, int idSe)
        {
            string sql = "SELECT * FROM Assignment a, Position p " +
                "WHERE classID= @idCls AND a.positionID = p.ID AND academicyearID = @idAy AND  semesterID= @idSe AND p.positionName= N'Giáo viên chủ nhiệm'" ;
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idCls", idCls);
                cmd.Parameters.AddWithValue("@idAy", idAy);
                cmd.Parameters.AddWithValue("@idSe", idSe);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close(); }
        }

        public bool DeleteAssignment(int idCls, int idSb, int idAy, int idSe, int idTea)
        {

            string sql = "DELETE FROM Assignment " +
                         "WHERE teacherID = @idTea AND classID = @idCls AND semesterID = @idSe " +
                         "AND academicyearID = @idAy AND subjectID = @idSb";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idTea",idTea);
                cmd.Parameters.AddWithValue("@idCls",idCls);
                cmd.Parameters.AddWithValue("@idSe", idSe);
                cmd.Parameters.AddWithValue("@idAy", idAy);
                cmd.Parameters.AddWithValue("@idSb", idSb);
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
        public bool InsertAssignment(int idCls, int idSb, int idAy, int idSe, int idTea, int idPos)
        {

            string sql = "INSERT INTO Assignment(teacherID,classID,semesterID,positionID,academicyearID, subjectID) " +
                         "Values(@idTea,@idCls,@idSe,@idPos,@idAy,@idSb)";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idTea", idTea);
                cmd.Parameters.AddWithValue("@idCls", idCls);
                cmd.Parameters.AddWithValue("@idSe", idSe);
                cmd.Parameters.AddWithValue("@idPos", idPos);
                cmd.Parameters.AddWithValue("@idAy", idAy);
                cmd.Parameters.AddWithValue("@idSb", idSb);
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
    }
}
