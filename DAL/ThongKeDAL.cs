using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace ManagerStudent.DAL
{
    internal class ThongKeDAL : initConnect
    {
        public DataTable GetAllNumberStudent()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT   ay.academicyearName  AS 'Năm học',
                                    s2.semesterName  AS 'Học kỳ',
                                    COUNT(*) AS 'Tổng số học sinh',
                                    SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                    SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID  
                            GROUP BY
                                ay.academicyearName, s2.semesterName
                            ORDER BY
                                ay.academicyearName, s2.semesterName ";

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
        public DataTable GetAllAyearNumberStudent(string ayName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT   s2.semesterName  AS 'Học kỳ',
                                    COUNT(*) AS 'Tổng số học sinh',
                                    SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                    SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            WHERE 
	                            academicyearName  = @ayName
                            GROUP BY
                                 s2.semesterName
                            ORDER BY
                                 s2.semesterName ";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@ayName", ayName);
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
        public DataTable GetAllSemesNumberStudent(string seName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT   ay.academicyearName  AS 'Năm học',
                                    COUNT(*) AS 'Tổng số học sinh',
                                    SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                    SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            WHERE 
	                            s2.semesterName = @seName
                            GROUP BY
                                 s2.semesterName
                            ORDER BY
                                 s2.semesterName ";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@seName", seName);
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
        public DataTable GetNumberStudent(string ayName, string seName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT   ay.academicyearName  AS 'Năm học',
                                    s2.semesterName  AS 'Học kỳ',
                                    COUNT(*) AS 'Tổng số học sinh',
                                    SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                    SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            WHERE 
	                            academicyearName  = @ayName AND
	                            s2.semesterName = @seName
                            GROUP BY
                                ay.academicyearName, s2.semesterName
                            ORDER BY
                                ay.academicyearName, s2.semesterName ";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ayName", ayName);
                cmd.Parameters.AddWithValue("@seName", seName);
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


        public DataTable GetGradeNumberStudent()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ay.academicyearName  AS 'Năm học',
                                  s2.semesterName  AS 'Học kỳ',
                                  g.gradeName AS 'Khối',
                                  COUNT(*) AS 'Tổng số học sinh',
                                  SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                  SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            JOIN
                                Grade g ON scs.gradeID = g.ID
                            GROUP BY
                                ay.academicyearName, s2.semesterName, g.gradeName
                            ORDER BY
                                ay.academicyearName, s2.semesterName, g.gradeName";

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
        public DataTable GetGradeAyearNumberStudent(string ayName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT s2.semesterName  AS 'Học kỳ',
                                  g.gradeName AS 'Khối',
                                  COUNT(*) AS 'Tổng số học sinh',
                                  SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                  SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            JOIN
                                Grade g ON scs.gradeID = g.ID
                            WHERE 
	                             academicyearName  = @ayName
                            GROUP BY
                                 s2.semesterName, g.gradeName
                            ORDER BY
                                 s2.semesterName, g.gradeName";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ayName", ayName);
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
        public DataTable GetGradeSemesNumberStudent(string seName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ay.academicyearName  AS 'Năm học',
                                  g.gradeName AS 'Khối',
                                  COUNT(*) AS 'Tổng số học sinh',
                                  SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                  SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            JOIN
                                Grade g ON scs.gradeID = g.ID
                            WHERE 
	                             s2.semesterName = @seName
                            GROUP BY
                                  ay.academicyearName, g.gradeName
                            ORDER BY
                                  ay.academicyearName, g.gradeName";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@seName", seName);
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
        public DataTable GetGradeASNumberStudent(string ayName, string seName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT   g.gradeName AS 'Khối',
                                    COUNT(*) AS 'Tổng số học sinh',
                                    SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                    SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            JOIN
                                Grade g ON scs.gradeID = g.ID
                            WHERE 
	                            academicyearName  = @ayName AND
	                            s2.semesterName = @seName
                            GROUP BY
                                g.gradeName
                            ORDER BY
                                g.gradeName ";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ayName", ayName);
                cmd.Parameters.AddWithValue("@seName", seName);
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


        public DataTable GetClassNumberStudent()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ay.academicyearName  AS 'Năm học',
                                  s2.semesterName  AS 'Học kỳ',
                                  c.className  AS 'Lớp',
                                  COUNT(*) AS 'Tổng số học sinh',
                                  SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                  SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            JOIN
                                 Class c ON scs.classID  = c.ID
                            GROUP BY
                                ay.academicyearName, s2.semesterName, c.className
                            ORDER BY
                                ay.academicyearName, s2.semesterName, c.className";

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
        public DataTable GetClassAyearNumberStudent( string ayName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT s2.semesterName  AS 'Học kỳ',
                                  c.className  AS 'Lớp',
                                  COUNT(*) AS 'Tổng số học sinh',
                                  SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                  SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            JOIN
                                 Class c ON scs.classID  = c.ID
                            WHERE 
	                             academicyearName  = @ayName
                            GROUP BY
                                 s2.semesterName, c.className
                            ORDER BY
                                 s2.semesterName, c.className";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ayName", ayName);
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
        public DataTable GetClassSemesNumberStudent(string seName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ay.academicyearName  AS 'Năm học',
                                  c.className  AS 'Lớp',
                                  COUNT(*) AS 'Tổng số học sinh',
                                  SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                  SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            JOIN
                                 Class c ON scs.classID  = c.ID
                            WHERE 
	                             s2.semesterName = @seName
                            GROUP BY
                                ay.academicyearName, c.className
                            ORDER BY
                                ay.academicyearName, c.className";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@seName", seName);
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
        public DataTable GetClassASNumberStudent(string ayName, string seName)
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT   c.className  AS 'Lớp',
                                    COUNT(*) AS 'Tổng số học sinh',
                                    SUM(CASE WHEN s.gender = N'Nam' THEN 1 ELSE 0 END) AS 'Số lượng nam',
                                    SUM(CASE WHEN s.gender = N'Nữ' THEN 1 ELSE 0 END) AS 'Số lượng nữ'
                            FROM
                                StudentClassSemesterAcademicYear scs
                            JOIN
                                Student s ON scs.studentID = s.ID
                            JOIN 
	                            AcademicYear ay  ON scs.academicyearID =ay.ID 
                            JOIN 
	                            Semester s2 ON scs.semesterID = s2.ID
                            JOIN
                                 Class c ON scs.classID  = c.ID
                            WHERE 
	                            academicyearName  = @ayName AND
	                            s2.semesterName = @seName
                            GROUP BY
                                c.className
                            ORDER BY
                                c.className ";

            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ayName", ayName);
                cmd.Parameters.AddWithValue("@seName", seName);
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

    }
}
