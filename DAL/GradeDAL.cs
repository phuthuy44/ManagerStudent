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
    public class GradeDAL:initConnect
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
                    grade.maxClassOfGrade = Convert.ToInt32(dt.Rows[i]["maxclassofGrade"]);
                    grade.realClassOfGrade = Convert.ToInt32(dt.Rows[i]["quantityclassofGrade"]);
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
                int maxClassOfGrade = grade.maxClassOfGrade;
                int realClassOfGrade = grade.realClassOfGrade;
                string sql = "insert into Grade(gradeName,maxclassofGrade,quantityclassofGrade) values('" + name + "','" + maxClassOfGrade + "','" + realClassOfGrade + "')";
                dt = init.Runquery(sql);
                init.Update(dt);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
              
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
                int maxClassOfGrade = grade.maxClassOfGrade;
                int realClassOfGrade = grade.realClassOfGrade;
                string sql = "UPDATE Grade SET gradeName = '" + name + "', maxclassofGrade = '" + maxClassOfGrade + "', quantityclassofGrade = '" + realClassOfGrade + "' WHERE ID = " + ma;
                dt = init.Runquery(sql);
                init.Update(dt);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public void DeleteGrade(Grade grade)
        {
            try
            {
               ConnectToDatabase();
                int ma = grade.ID;
                string sql = "DELETE FROM Grade WHERE ID = " + ma;
                dt = init.Runquery(sql);
                init.Update(dt);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);       
            }
        }

        /*public List<Grade> SearchGrades(string searchTerm)
        {
            List<Grade> searchResults = new List<Grade>();
            try
            {
                SqlConnection connection = ConnectToDatabase();

                string sql = "SELECT * FROM Grade WHERE gradeName LIKE @searchTerm";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
            
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }

                foreach (DataRow row in dt.Rows)
                {
                    Grade grade = new Grade();
                    grade.ID = Convert.ToInt32(row["ID"]);
                    grade.Name = row["gradeName"].ToString();
                    grade.maxClassOfGrade = Convert.ToInt32(row["maxclassofGrade"]);
                    grade.realClassOfGrade = Convert.ToInt32(row["quantityclassofGrade"]);
                    searchResults.Add(grade);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ tại đây
                Console.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
            return searchResults;
        }*/




    }
}
