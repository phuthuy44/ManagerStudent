using ManagerStudent.DAL;
using ManagerStudent.DTO;
using ManagerStudent.GUI;
using Newtonsoft.Json;
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
        public string CheckAccount(string username, string password)
        {
            Response response = new Response();

            var account = GetAccountData.GetAccountByUsername(username, password);

            if (account.userName == null)
            {
                response.Status = false;
                response.Message = "Tên đăng nhập hoặc mật khẩu chưa chính xác.";
            }
            else if (!account.isActive)
            {
                response.Status = false;
                response.Message = "Tài khoản này đang bị khoá.";
            }
            else
            {
                response.Status = true;
                response.Message = "Đăng nhập thành công.";
            }

            return JsonConvert.SerializeObject(response);
        }
    }
}
