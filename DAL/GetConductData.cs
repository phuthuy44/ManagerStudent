using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class GetConductData
    {
        public IList<Conduct> GetAllConduct() {
            IList<Conduct> conducts = new List<Conduct>();
            try
            {
               
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "SELECT * FROM Conduct";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                Console.WriteLine("Step1");
                while (sqlDataReader.Read())
                {
                    conducts.Add(new Conduct(
                        (int)sqlDataReader.GetSqlInt32(sqlDataReader.GetOrdinal("ID")),
                        sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("conductName")).ToString(),
                        (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("upperLimit")),
                        (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("lowerLimit"))
                    ));
                    Console.WriteLine(
                        sqlDataReader.GetSqlInt32(sqlDataReader.GetOrdinal("ID")) + " "+
                        sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("conductName")).ToString() + " " +
                        (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("upperLimit")) + " " +
                        (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("lowerLimit")) 
                        );
                }
                connection.Close();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return conducts;
        }
    }
}
