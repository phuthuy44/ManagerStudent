using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerStudent.DAL
{
    internal class StudentDAL
    {
        //Hien thi len data len DataGridView
        public DataTable GetListStudent()
        {
            DataTable dataTable = new DataTable();
            string sql = " SELECT * FROM Student";
            try
            {

                SqlCommand cmd = new SqlCommand(sql, initConnect.ConnectToDatabase());
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error! Can not fill data.");
                Console.WriteLine(ex.Message);
            }
            finally {
                initConnect.CloseConnection(initConnect.ConnectToDatabase());
            }
            return dataTable;
        }
    }
}
