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
    internal class GetTypeOfPointData
    {
        public DataTable GetAllTypeOfPoint()
        {
            IList<TypeOfPoint> typeOfPoints = new List<TypeOfPoint>();
            try {
                SqlConnection sqlConnection = initConnect.ConnectToDatabase();
                string ssql = "EXEC GetDataTypeOfPoint";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(ssql, sqlConnection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
            return typeOfPoi;
        }
    }
}
