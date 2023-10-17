using DocumentFormat.OpenXml.Office2010.Excel;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerStudent.DAL
{
    internal class GetAccountData
    {
        //Lấy danh sách các tài khoản trong bảng account
        /*public IList<Account> GetAllAccount()
        {
            log log = new log("..\\..\\file.log");
            //Khởi tạo danh sách 
            IList < Account > accountList = new List<Account>();
            string sql = "SELECT * FROM Account";
            try
            {
                SqlConnection conn = initConnect.ConnectToDatabase();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    //Thêm 1 dòng vào danh sách 
                    DateTime? createDate = null;
                    DateTime? updateDate = null;
                    DateTime? finishDate = null;
                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("createDate")))
                    {
                        createDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("createDate"));
                    }
                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("updateDate")))
                    {
                        createDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("updateDate"));
                    }
                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("finishDate")))
                    {
                        createDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("finishDate"));
                    }
                    accountList.Add(
                        new Account(
                            sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("username")).ToString(),
                            sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("password")).ToString(),
                            sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("teacherID")),
                            createDate,
                            updateDate,
                            finishDate,
                            1));
                    
                    *//*Console.WriteLine(
                        sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("username"))+" "+
                        sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("password"))+" "+
                        sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("teacherID")).ToString()+" "+
                        createDate.ToString()+" "+ updateDate.ToString()+" "+ finishDate.ToString());
                *//*
                }
                initConnect.CloseConnection(conn);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                log.writeLog(e.Message);
            }
            
            //Trả về danh sách
            return accountList;
        }
        */
        public Account GetAccountByUsername(string username, string password)
        {
            Account account = new Account();
            string sql = "SELECT ac.username , ac.password, " +
                "ac.createDate , ac.updateDate , ac.finishDate , " +
                "s.statusName , s.isActive  " +
                "FROM Account ac " +
                "LEFT JOIN AccountStatus as2 ON as2.accountID = ac.username " +
                "LEFT JOIN Status s ON s.ID = as2.statusID "+
                "WHERE ac.username = '"+username+"'" +
                "AND ac.password='"+password+"'"
                ;
            try
            {
                SqlConnection conn = initConnect.ConnectToDatabase();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    DateTime? createDate = null;
                    DateTime? updateDate = null;
                    DateTime? finishDate = null;
                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("createDate")))
                    {
                        createDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("createDate"));
                    }
                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("updateDate")))
                    {
                        createDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("updateDate"));
                    }
                    if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("finishDate")))
                    {
                        createDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("finishDate"));
                    }
                    account.finishDate = finishDate;
                    account.createDate = createDate;
                    account.updateDate = updateDate;
                    account.userName = sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("username")).ToString();
                    account.password = sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("password")).ToString();
                    //account.teacherID = (int)sqlDataReader.GetSqlInt32(sqlDataReader.GetOrdinal("teacherID"));
                    account.isActive = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("isActive"));
                    Console.WriteLine(
                            account.userName+" "+
                            account.password+" "+
                            //account.teacherID+" "+
                            account.createDate+" "+
                            account.updateDate+" "+
                            account.finishDate+" "+
                            account.isActive
                        );
                }
                initConnect.CloseConnection(conn);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return account;
        }
        
        
    }
}
