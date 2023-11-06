using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class Subject
    {
        public int ID {  get; set; }
        public string typeofsubjectID {  get; set; }
        public string Name { get; set; }
        public Subject(int iD, string typeofsubjectID, string name)
        {
            ID = iD;
            this.typeofsubjectID = typeofsubjectID;
            Name = name;
        }

        public Subject() { }
    }
}
