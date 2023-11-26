using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class Class
    {
        public int ID { get; set; }

        public string Name { get; set; }
     
        public int maxStudent { get; set; }

        public Class(int iD, string name,int maxStudent)
        {
            ID = iD;
            Name = name;     
            this.maxStudent = maxStudent;

        }
        public Class() { }

    }
}
