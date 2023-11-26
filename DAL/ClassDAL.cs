using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ManagerStudent.DTO;


namespace ManagerStudent.DAL
{
    public class ClassDAL:initConnect
    {
        DataTable dt = new DataTable();
        initConnect init = new initConnect();
        public List<Class> getAll()
        {
            List<Class> dscls = new List<Class>();
            try
            {
                ConnectToDatabase();
                string sql = "select * from Class";
                dt = init.Runquery(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Class cls = new Class();
                    cls.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    cls.Name = dt.Rows[i]["className"].ToString();
                    cls.maxStudent = Convert.ToInt32(dt.Rows[i]["maxStudent"]);
                    cls.realStudent = Convert.ToInt32(dt.Rows[i]["quantityStudent"]);
                    dscls.Add(cls);

                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
            return dscls;
        }
        public void InsertClass(Class cls)
        {
            try
            {
                ConnectToDatabase();
                int ma = cls.ID;
                string name = cls.Name;
                int maxStudent = cls.maxStudent;
                int realStudent = cls.realStudent;
                string sql = "insert into Class(className,maxStudent,quantityStudent) values('" + name + "','" + maxStudent + "','" + realStudent + "')";
                dt = init.Runquery(sql);
                init.Update(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public bool HasNameClass(string ten)
        {
            try
            {
                string sql = "select * from Class where className=N'" + ten + "'";
                dt = init.Runquery(sql);
                if (dt.Rows.Count == 0) return false;
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                return false;
            }
        }

        public int getlastclassid()
        {
            ConnectToDatabase();
            int lastId = 0;
            string sql = "SELECT TOP 1 ID FROM Class ORDER BY ID DESC";
            dt = init.Runquery(sql);
            if (dt.Rows.Count > 0)
            {
                lastId = Convert.ToInt32(dt.Rows[0]["ID"]);
            }
            return lastId;

        }
        public void UpdateClass(Class cls)
        {
            try
            {
                ConnectToDatabase();
                int ma = cls.ID;
                string name = cls.Name;
                int maxStudent = cls.maxStudent;
                int realStudent = cls.realStudent;
                string sql = "Update Class SET className= '" + name + "', maxStudent = '" + maxStudent + "',quantityStudent = '" + realStudent + "' WHERE ID = " + ma;
                dt = init.Runquery(sql);
                init.Update(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public bool checkUpdateClass(string Name, int ID)
        {
            try
            {
                ConnectToDatabase();
                string sql = "SELECT COUNT(*) FROM Class WHERE LOWER(className) = LOWER('" + Name + "') AND ID != " + ID;

                 dt = init.Runquery(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int count = Convert.ToInt32(dt.Rows[0][0]);
                    return count > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public bool DeleteClass(int ma)
        {
            try
            {
                ConnectToDatabase();
                string sql = "delete from Class WHERE ID = " + ma;
                dt = init.Runquery(sql);
                init.Update(dt);
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
                return false;
            }
        }
        public List<Class> SearchClass(string searchTerm)
        {
            List<Class> searchResults = new List<Class>();
            try
            {
                using (SqlConnection connection = ConnectToDatabase())
                {
                    string sql = "SELECT * FROM Class WHERE className LIKE @searchTerm";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            Class cls = new Class();
                            cls.ID = Convert.ToInt32(row["ID"]);
                            cls.Name = row["className"].ToString();
                            cls.maxStudent = Convert.ToInt32(row["maxStudent"]);
                            cls.realStudent = Convert.ToInt32(row["quantityStudent"]);
                            searchResults.Add(cls);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
            return searchResults;
        }


    }
}
