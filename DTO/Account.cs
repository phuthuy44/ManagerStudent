using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class Account
    {
        public string userName {  get; set; }
        public int statusID {  get; set; }
        public int teacherID {  get; set; }
        public string password { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
        public DateTime? finishDate { get; set; }
        public Account(string userName, string password, int teacherID, DateTime? createDate, DateTime? updateDate, DateTime? finishDate,  int statusID)
        {
            this.userName = userName;
            this.statusID = statusID;
            this.teacherID = teacherID;
            this.password = password;
            this.createDate = createDate;
            this.updateDate = updateDate;
            this.finishDate = finishDate;
        }

        public Account() { }
    }
}
