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
        public bool isActive {  get; set; }
        public int teacherID {  get; set; }
        public string password { get; set; }
        public DateTime? createDate { get; set; }
        public DateTime? updateDate { get; set; }
        public DateTime? finishDate { get; set; }
        public Account(string userName, string password, int teacherID, DateTime? createDate, DateTime? updateDate, DateTime? finishDate,  bool isActive)
        {
            this.userName = userName;
            this.isActive = isActive;
            this.teacherID = teacherID;
            this.password = password;
            this.createDate = createDate;
            this.updateDate = updateDate;
            this.finishDate = finishDate;
        }

        public Account() { }
    }
}
