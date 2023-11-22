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
    internal class GetSemesterData
    {
        public DataTable GetAllSemester()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection conn = initConnect.ConnectToDatabase();
                string sql = "EXEC GetDataSemester";
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return dataTable;
        }
        public DataTable FindSemester(string str)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "EXEC FindSemester @STR";
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

        public bool insertSemester(string semesterName, int coefficient)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = "insert into Semester (semesterName, coefficient) values (@semesterName, @coefficient)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@semesterName", semesterName);
                command.Parameters.AddWithValue("@coefficient", coefficient);
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

        public bool updateSemester(string semesterName, int coefficient, int ID)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"update Semester
                               set semesterName = @semesterName, 
                                    coefficient = @coefficient 
                               where ID = @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@semesterName", semesterName);
                command.Parameters.AddWithValue("@coefficient", coefficient);
                command.Parameters.AddWithValue("@ID", ID);
                int rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        public bool deleteSemester(string semesterName)
        {
            try
            {
                SqlConnection sqlConnection = initConnect.ConnectToDatabase();
                string sql = "delete from Semester where semesterName  = @semesterName";
                SqlCommand command = new SqlCommand(sql, sqlConnection);
                command.Parameters.AddWithValue("@semesterName", semesterName);
                int rowsAffected = command.ExecuteNonQuery();
                /*return rowsAffected > 0;*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool checkInsertSemesterName(string semesterName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"SELECT COUNT(*) FROM Semester WHERE LOWER(semesterName) = LOWER(@semesterName)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@semesterName", semesterName);
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

        public bool checkUpdateSemesterName(int ID, string semesterName)
        {
            try
            {
                SqlConnection connection = initConnect.ConnectToDatabase();
                string sql = @"SELECT COUNT(*) FROM Semester WHERE LOWER(semesterName) = LOWER(@semesterName) 
                               AND ID != @ID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@semesterName", semesterName);
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
            /*public IList<Semester> GetAllSemester()
            {
                IList<Semester> semesters = new List<Semester>();
                try
                {
                    SqlConnection connection = initConnect.ConnectToDatabase();
                    string ssql = "EXECUTE GetDataSemester";
                    SqlCommand sqlCommand = new SqlCommand(ssql, connection);
                    SqlDataReader reader = sqlCommand.ExecuteReader(); 
                    while (reader.Read())
                    {
                        semesters.Add(new Semester(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetString(reader.GetOrdinal("semesterName")),
                                reader.GetInt32(reader.GetOrdinal("coefficient"))
                            ));
                       // Console.WriteLine(reader.GetInt32(1));
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return semesters;
            }*/
        }
    }
}
