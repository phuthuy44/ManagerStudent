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
            string sql = "SELECT * FROM PARENT WHERE Parent.studentID= @id and Parent.gender=N'Nam'";
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
                    Console.WriteLine(reader["image"].ToString());
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
        public bool insertParent(Parent parent)
        {
            string sql = " INSERT INTO Parent (studentID,name,birthday,gender,numberPhone,address,image) VALUES (@studentID,@name,@birthday,@gender,@numberPhone,@address,@image)";
            SqlConnection conn = initConnect.ConnectToDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@studentID", parent.ID);
                cmd.Parameters.AddWithValue("@name", parent.Name);
                cmd.Parameters.AddWithValue("@birthday", parent.Birthday);
                cmd.Parameters.AddWithValue("@gender", parent.Gender);
                cmd.Parameters.AddWithValue("@numberPhone", parent.Phone);
                cmd.Parameters.AddWithValue("@address", parent.Address);
                cmd.Parameters.AddWithValue("@image",parent.Image);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close ();
            }
        }
        public bool updateParentCha(Parent parent)
        {
            string sql = "UPDATE Parent set name = @name,birthday=@birthday,numberPhone=@phone,address=@address,image=@image where studentID=@id and gender= @gender";
            SqlConnection conn = initConnect.ConnectToDatabase ();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", parent.Name);
                cmd.Parameters.AddWithValue("@birthday", parent.Birthday);
                cmd.Parameters.AddWithValue("@phone", parent.Phone);
                cmd.Parameters.AddWithValue("@address", parent.Address);
                cmd.Parameters.AddWithValue("@image",parent.Image);
                cmd.Parameters.AddWithValue("@id", parent.ID);
                cmd.Parameters.AddWithValue("@gender",parent.Gender);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally { conn.Close (); }
        }
        public bool checkExistCha(int studentID, string gender)
        {
            bool isExist = false;
            string sql = "SELECT COUNT(*) FROM Parent where studentID =@id and gender =@gender";
            SqlConnection con = initConnect.ConnectToDatabase ();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", studentID);
                cmd.Parameters.AddWithValue("@gender", gender);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if(count > 0)
                {
                    isExist = true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            finally { con.Close (); }
            return isExist;
        }
    }
}
