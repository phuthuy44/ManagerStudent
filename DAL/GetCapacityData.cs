using System;
using System.Data;
using System.Data.SqlClient;

namespace ManagerStudent.DAL
{
    internal class GetCapacityData
    {
        public DataTable GetAllCapacity()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "SELECT * FROM Capacity";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataTable);

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTable;
        }

        public DataTable FindCapacity(string str)
        {
            DataTable dataTable = new DataTable();

            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();

                string sql = "EXEC FindCapacity @STR";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@STR", str);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return dataTable;
        }
    }
}
