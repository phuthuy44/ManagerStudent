using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class GetTypeOfPointData
    {
        public IList<TypeOfPoint> GetAllTypeOfPoint()
        {
            IList<TypeOfPoint> typeOfPoints = new List<TypeOfPoint>();
            try {
                SqlConnection sqlConnection = initConnect.ConnectToDatabase();
                string ssql = "EXEC GetDataTypeOfPoint";
                SqlCommand cmd = new SqlCommand(ssql, sqlConnection);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Console.WriteLine(sqlDataReader.GetString(1));
                }
                sqlConnection.Close();
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
            return typeOfPoints;
        }
    }
}
