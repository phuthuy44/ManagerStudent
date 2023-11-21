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
                /*string sql = "SELECT * FROM Capacity";*/
                string sql = @"SELECT ID as N'Mã học lực', 
                                  capacityName AS N'Tên học lực', 
                                  upperLimit AS N'Điểm cận trên', 
                                  lowerLimit AS N'Điểm cận dưới', 
                                  paraPoint AS N'Điểm khống chế'
                               FROM Capacity";
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
        public bool checkInsertCapacityName(string capacityName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "SELECT COUNT(*) FROM Capacity WHERE LOWER(capacityName) = LOWER(@capacityName)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@capacityName", capacityName);
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

        public bool checkUpdateCapacityName(int ID, string capacityName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"SELECT COUNT(*) FROM Capacity WHERE LOWER(capacityName) = LOWER(@capacityName) 
                               AND ID != @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@capacityName", capacityName);
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

        public bool insertCapacity(string capacityName, float upperLimit, float lowerLimit, float paraPoint)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"insert into Capacity (capacityName, upperLimit, lowerLimit, paraPoint) 
                                values (@capacityName, @upperLimit, @lowerLimit, @paraPoint)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@capacityName", capacityName);
                command.Parameters.AddWithValue("@upperLimit", upperLimit);
                command.Parameters.AddWithValue("@lowerLimit", lowerLimit);
                command.Parameters.AddWithValue("@paraPoint", paraPoint);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            /*return true;*/
        }

        public bool updateCapacity(int ID, string capacityName, float upperLimit, float lowerLimit, float paraPoint)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"update Capacity
                                    set capacityName = @capacityName, 
                                    upperLimit = @upperLimit, 
                                    lowerLimit = @lowerLimit, 
                                    paraPoint = @paraPoint
                                where ID = @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@capacityName", capacityName);
                command.Parameters.AddWithValue("@upperLimit", upperLimit);
                command.Parameters.AddWithValue("@lowerLimit", lowerLimit);
                command.Parameters.AddWithValue("@paraPoint", paraPoint);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool deleteCapacity(string capacityName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "delete from Capacity where capacityName  = @capacityName";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@capacityName", capacityName);
                int rowAffected = command.ExecuteNonQuery();

                return rowAffected > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            //return true;
        }
    }
}
