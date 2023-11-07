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
    internal class GetSemesterData
    {
        public DataTable GetAllSemester()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection conn = initConnect.ConnectToDatabase();
                string sql = "EXEC GetDataSemester";
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return dataTable;
        }
        /*public IList<Semester> GetAllSemester()
        {
            IList<Semester> semesters = new List<Semester>();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string ssql = "EXECUTE GetDataSemester";
                SqlCommand sqlCommand = new SqlCommand(ssql, connection);
                SqlDataReader reader = sqlCommand.ExecuteReader(); 
                while (reader.Read())
                {
                    semesters.Add(new Semester(
                            reader.GetInt32(reader.GetOrdinal("ID")),
                            reader.GetString(reader.GetOrdinal("semesterName")),
                            reader.GetInt32(reader.GetOrdinal("coefficient"))
                        ));
                   // Console.WriteLine(reader.GetInt32(1));
                }
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return semesters;
        }*/
    }
}
