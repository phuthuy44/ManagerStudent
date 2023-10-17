using ManagerStudent.DAL;
using ManagerStudent.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerStudent.BLL
{
    internal class AccountBLL
    {
        GetAccountData GetAccountData = new GetAccountData();
        public bool CheckAccount(String username, string password)
        {
           // Button button =
           if (GetAccountData.GetAccountByUsername(username, password).userName==null)
            {
                return false;
            }
           return true;
        }
    }
}
