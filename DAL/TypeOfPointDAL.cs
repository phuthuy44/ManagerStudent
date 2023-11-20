using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class TypeOfPointDAL
    {
        public TypeOfPointDAL() { }
        public DataTable TypeOfPointData()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection con = initConnect.ConnectToDatabase();
                string sql = "EXECUTE GetDataTypeOfPoint";
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                Console.WriteLine(dataTable.Rows.Count);
                con.Close();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            return dataTable;
        }
        public DataTable FindTypeOfPoint(string str)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "EXEC FindTypeOfPoint @STR";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.AddWithValue("@STR", str);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dt);
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        public bool insertTypeofPoint(string pointName, int coefficient)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "insert into TypeOfPoint (pointName, coefficient) values (@pointName, @coefficient)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@pointName", pointName);
                command.Parameters.AddWithValue("@coefficient", coefficient);
                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool updateTypeofPoint(int ID, string pointName, int coefficient)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"update TypeOfPoint 
                                set pointName = @pointName, coefficient = @coefficient
                                where ID = @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@pointName", pointName);
                command.Parameters.AddWithValue("@coefficient", coefficient);
                command.ExecuteNonQuery();
                int rowAffected = command.ExecuteNonQuery();
                return rowAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            /*return true;*/
        }

        public bool deleteTypeofPoint(string pointName)
        {
            try
            {
                SqlConnection sqlConnection = initConnect.ConnectToDatabase();
                string sql = "";
                SqlCommand command = new SqlCommand(sql, sqlConnection);
                command.Parameters.AddWithValue("@pointName", pointName);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool checkUpdateTypeofPointName(string pointName, int ID)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"SELECT COUNT(*) FROM Conduct WHERE LOWER(pointName) = LOWER(@pointName) 
                               AND ID != @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@pointName", pointName);
                command.Parameters.AddWithValue("@ID", ID);
                //Phương thức ExecuteScalar() trả về kết quả của truy vấn đó dưới dạng một giá trị duy nhất.
                int existingCount = (int)command.ExecuteScalar();
                return existingCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool checkInsertTypeofPointName(string pointName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "SELECT COUNT(*) FROM TypeOfPoint WHERE LOWER(pointName) = LOWER(@pointName)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@pointName", pointName);
                //Phương thức ExecuteScalar() trả về kết quả của truy vấn đó dưới dạng một giá trị duy nhất.
                int existingCount = (int)command.ExecuteScalar();
                return existingCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
