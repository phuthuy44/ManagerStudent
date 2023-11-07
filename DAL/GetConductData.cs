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
