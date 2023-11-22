using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    internal class Parent:People
    {
        public Parent() { 
        
        }

        public Parent(int iD, string name, string gender, string address, DateTime birthday, string phone, string image) : base(iD, name, gender, address, birthday, phone, image)
        {
        }

        public Parent(int iD, string name, string gender, string address, DateTime birthday, string email, string phone, string image, DateTime createDate) : base(iD, name, gender, address, birthday, email, phone, image, createDate)
        {

        }

        public Parent(int id, string name, string gender, string address, DateTime birthday, string birthplace, string email, string phone, string image) : base(id, name, gender, address, birthday, birthplace, email, phone, image)
        {
        }
    }
}
