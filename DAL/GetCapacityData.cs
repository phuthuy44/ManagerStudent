using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class GetCapacityData
    {
        public IList<Capacity> GetAllCapacity() {
            IList<Capacity> capacities = new List<Capacity>();
            try
            {
               
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "SELECT * FROM Capacity";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    capacities.Add(new Capacity(
                        (int)sqlDataReader.GetSqlInt32(sqlDataReader.GetOrdinal("ID")),
                        sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("capacityName")).ToString(),
                        (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("upperLimit")),
                        (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("lowerLimit")),
                        (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("paraPoint"))
                    ));
                    Console.WriteLine(
                        sqlDataReader.GetSqlInt32(sqlDataReader.GetOrdinal("ID")) + " "+
                        sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("capacityName")).ToString() + " " +
                        (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("upperLimit")) + " " +
                        (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("lowerLimit")) 
                        );
                }
                connection.Close();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return capacities;
        }
    }
}
