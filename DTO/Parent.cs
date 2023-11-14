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

        public Parent(int iD, string name, string gender, string address, DateTime birthday, string email, string phone, string image, DateTime createDate) : base(iD, name, gender, address, birthday, email, phone, image, createDate)
        {

        }
    }
}
