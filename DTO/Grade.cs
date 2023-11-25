using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class Grade
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Grade(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public Grade() { }
    }
}
