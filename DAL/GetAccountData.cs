﻿using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class GetAccountData
    {
        //Lấy danh sách các tài khoản trong bảng account
        public IList<Account> GetAllAccount()
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
                    accountList.Add(
                        new Account(
                            sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("username")).ToString(),
                            sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("password")).ToString(),
                            sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("teacherID")),
                            DateTime.Parse(sqlDataReader.GetString(3)),
                            DateTime.Parse(sqlDataReader.GetString(4)),
                            DateTime.Parse(sqlDataReader.GetString(5)),
                            1));
                    Console.WriteLine(
                        sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("username"))+" "+
                        sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("password"))+" "+
                       sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("teacherID")).ToString());
                    
                }
                //initConnect.CloseConnection(conn);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                log.writeLog(e.Message);
            }
            
            //Trả về danh sách
            return accountList;
        }
        public Account GetAccountByUsername(string username)
        {
            Account account = new Account();
            string sql = "SELECT * FROM Account WHERE username = '"+username+")";
            try
            {
                SqlConnection conn = initConnect.ConnectToDatabase();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    account.userName = (string)sqlDataReader.GetSqlString(sqlDataReader.GetOrdinal("username"));
                }
                initConnect.CloseConnection(conn);
            }catch(Exception e)
            {

            }

            return account;
        }
        
        
    }
}
