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
        public string statusID {  get; set; }
        public string teacherID {  get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public DateTime finishDate { get; set; }
        public Account(string userName, string statusID, string teacherID, string password, string name, DateTime createDate, DateTime updateDate, DateTime finishDate)
        {
            this.userName = userName;
            this.statusID = statusID;
            this.teacherID = teacherID;
            this.password = password;
            Name = name;
            this.createDate = createDate;
            this.updateDate = updateDate;
            this.finishDate = finishDate;
        }
        public Account() { }
    }
}
