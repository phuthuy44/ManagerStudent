using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class ParentDAL
    {
        public List<Parent> getDataCha(int id)
        {
            List<Parent> data = new List<Parent>();
            string sql = "SELECT * FROM PARENT WHERE Parent.studentID= @id and Parent.gender= N'Nam'";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Parent p = new Parent();
                    p.Name = reader["Name"].ToString();
                    p.Birthday = (DateTime)reader["birthday"];
                    p.Gender = reader["gender"].ToString();
                    p.Phone = reader["numberPhone"].ToString();
                    p.Address = reader["address"].ToString();
                    p.Image = reader["image"].ToString();
                    p.createDate = (DateTime)reader["createDate"];
                    data.Add(p);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return data;
        }
        public List<Parent> getDataMe(int id)
        {
            List<Parent> data = new List<Parent>();
            string sql = "SELECT * FROM PARENT WHERE Parent.studentID= @id and Parent.gender= N'Nữ'";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Parent p = new Parent();
                    p.Name = reader["Name"].ToString();
                    p.Birthday = (DateTime)reader["birthday"];
                    p.Gender = reader["gender"].ToString();
                    p.Phone = reader["numberPhone"].ToString();
                    p.Address = reader["address"].ToString();
                    p.Image = reader["image"].ToString();
                    p.createDate = (DateTime)reader["createDate"];
                    data.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return data;
        }
    }
}
