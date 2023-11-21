using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ManagerStudent.DAL
{
    internal class TeacherDAL : initConnect
    {
        DataTable dt = new DataTable();
        public DataTable GetListTeacher()
        {

            string sql = @" SELECT  ID as N'Mã GV',
		                    teacherName as N'Tên Giáo viên'	,
		                    gender as N'Giới tính',
		                    birthday as N'Ngày sinh',
		                    email 	,
		                    phonenumber as N'Số điện thoại',
		                    address as N'Địa chỉ'
                            FROM Teacher";

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
    }
}
