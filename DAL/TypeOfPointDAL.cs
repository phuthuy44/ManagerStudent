using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class TypeOfPointDAL
    {
        public TypeOfPointDAL() { }
        public DataTable TypeOfPointData()
        {
            DataTable dataTable = new DataTable();
            try
            {
                SqlConnection con = initConnect.ConnectToDatabase();
                string sql = "EXECUTE GetDataTypeOfPoint";
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                Console.WriteLine(dataTable.Rows.Count);
                con.Close();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            return dataTable;
        }
    }
}
