using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ManagerStudent.DAL
{
    internal class GetConductData
    {

        public DataTable GetallConduct()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = initConnect.ConnectToDatabase();
                string sql = "SELECT * FROM Conduct";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                conn.Close();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        public DataTable FindConduct(string str)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "EXEC FindConduct @STR";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.Parameters.AddWithValue("@STR", str);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dt);
                connection.Close();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        public bool insertConduct(string conductName, int upperLimit, int lowerLimit)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "EXEC insertConduct @conductName, @upperLimit, @lowerLimit";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@conductName", conductName);
                command.Parameters.AddWithValue("@upperLimit", upperLimit);
                command.Parameters.AddWithValue("@lowerLimit", lowerLimit);
                int rowsAffected = command.ExecuteNonQuery();
                // Trả về true nếu cập nhật thành công
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            /*return true;*/
        }
        public bool deleteConduct(string conductName)
        {
            try
            {
                SqlConnection sqlConnection = initConnect.ConnectToDatabase();
                string sql = "delete from Conduct where conductName  = @conductName";
                SqlCommand command = new SqlCommand(sql, sqlConnection);
                command.Parameters.AddWithValue("@conductName", conductName);
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool updateConduct(string conductName, int upperLimit, int lowerLimit, int ID)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"update Conduct
                               set conductName = @conductName, 
                                    upperLimit = @upperLimit, 
                                    lowerLimit = @lowerLimit
                               where ID = @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@conductName", conductName);
                command.Parameters.AddWithValue("@upperLimit", upperLimit);
                command.Parameters.AddWithValue("@lowerLimit", lowerLimit);
                command.Parameters.AddWithValue("@ID", ID);

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

        public bool checkUpdateConductName(string conductName, int ID)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"SELECT COUNT(*) FROM Conduct WHERE LOWER(conductName) = LOWER(@conductName) 
                               AND ID != @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@conductName", conductName);
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

        public bool checkInsertConductName(string conductName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "SELECT COUNT(*) FROM Conduct WHERE LOWER(ConductName) = LOWER(@conductName)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@conductName", conductName);
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
        /* public IList<Conduct> GetAllConduct() {
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
                     *//*Console.WriteLine(
                         sqlDataReader.GetSqlInt32(sqlDataReader.GetOrdinal("ID")) + " "+
                         sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("conductName")).ToString() + " " +
                         (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("upperLimit")) + " " +
                         (float)sqlDataReader.GetSqlDouble(sqlDataReader.GetOrdinal("lowerLimit")) 
                         );*//*
                 }
                 connection.Close();
             }catch(Exception ex)
             {
                 Console.WriteLine(ex.Message);
             }
             return conducts;
         }*/
    }
}
