using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class GetSubjectData
    {
        public DataTable GetAllSubject()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"select ID, subjectName from Subject";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                dataAdapter.Fill(dataTable);
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTable;
        }

        public DataTable FindSubject(string str)
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "exec FindSubject @STR";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@STR", str);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                dataAdapter.Fill(dataTable);
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dataTable;
        }

        public bool insertSubject(string subjectName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "insert into Subject (subjectName) values (@subjectName)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@subjectName", subjectName);

                int rowAffected = command.ExecuteNonQuery();
                return rowAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool updateSubject(int ID, string subjectName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"update Subject 
                                    set subjectName = @subjectName
                               where ID = @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@subjectName", subjectName);

                int rowAffected = command.ExecuteNonQuery();
                return rowAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool deleteSubject(string subjectName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "delete Subject where subjectName = @subjectName";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@subjectName", subjectName);

                int rowAffected = command.ExecuteNonQuery();
                return rowAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool checkInsertSubject(string subjectName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"select COUNT (*) from Subject 
                               where LOWER(subjectName) = LOWER(@subjectName)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@subjectName", subjectName);

                int existingCount = (int)command.ExecuteScalar();
                return existingCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool checkUpdateSubject(int ID, string subjectName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"select COUNT (*) from Subject 
                               where LOWER(subjectName) = LOWER(@subjectName)
                                     and ID != @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", ID);
                command.Parameters.AddWithValue("@subjectName", subjectName);

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
