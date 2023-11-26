using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ManagerStudent.DAL
{
    public class GradeDAL : initConnect
    {
        DataTable dt;
        initConnect init = new initConnect();

        public List<Grade> GetAll()
        {
            List<Grade> dsgrade = new List<Grade>();
            try
            {
                ConnectToDatabase();
                string sql = "select * from Grade";
                dt = init.Runquery(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Grade grade = new Grade();
                    grade.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    grade.Name = dt.Rows[i]["gradeName"].ToString();
                    dsgrade.Add(grade);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
            return dsgrade;
        }
        public void InsertGrade(Grade grade)
        {
            try
            {
                ConnectToDatabase();
                int ma = grade.ID;
                string name = grade.Name;
                string sql = "insert into Grade(gradeName) values(N'" + name + "')";
                dt = init.Runquery(sql);
                init.Update(dt);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);

            }
        }

        public bool HasNameGrade(string ten)
        {
            try
            {
                string sql = "select * from Grade where gradeName=N'" + ten + "'";
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

        public int getlastgradeid()
        {
            ConnectToDatabase();
            int lastId = 0;
            string sql = "SELECT TOP 1 ID FROM Grade ORDER BY ID DESC";
            dt = init.Runquery(sql);
            if (dt.Rows.Count > 0)
            {
                lastId = Convert.ToInt32(dt.Rows[0]["ID"]);
            }
            return lastId;
        }
        public void UpdateGrade(Grade grade)
        {
            try
            {
                ConnectToDatabase();
                int ma = grade.ID;
                string name = grade.Name;

                string sql = "UPDATE Grade SET gradeName = N'" + name + "' WHERE ID = " + ma;
                dt = init.Runquery(sql);
                init.Update(dt);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public bool checkUpdateGrade(string Name, int ID)
        {
            try
            {
                ConnectToDatabase();
                string sql = "SELECT COUNT(*) FROM Grade WHERE LOWER(gradeName) = LOWER(N'" + Name + "') AND ID != " + ID;
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


        public bool DeleteGrade(int ma)
        {
            try
            {
                ConnectToDatabase();
                string sql = "delete from Grade WHERE ID = " + ma;
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


        public List<Grade> SearchGrades(string searchTerm)
        {
            List<Grade> searchResults = new List<Grade>();
            try
            {
                using (SqlConnection connection = ConnectToDatabase())
                {
                    string sql = "SELECT * FROM Grade WHERE gradeName LIKE @searchTerm";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            Grade grade = new Grade();
                            grade.ID = Convert.ToInt32(row["ID"]);
                            grade.Name = row["gradeName"].ToString();
                            searchResults.Add(grade);
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
