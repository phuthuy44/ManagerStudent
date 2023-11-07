using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class Student : People
    {
        public Student() { 
        
        }

        public Student(string name, string gender, string address, DateTime birthday, string email, string phone, string image) : base( name, gender, address, birthday, email, phone, image)
        {
        }

        public Student(string name, string gender, string address, DateTime birthday, string email, string phone, string image, DateTime createDate, DateTime updateDate) : base(name, gender, address, birthday, email, phone, image, createDate, updateDate)
        {
        }
        public Student(int iD, string name, string gender, string address, DateTime birthday, string email, string phone, string image) : base(iD, name, gender, address, birthday, email, phone, image)
        {
        }
    }
}
