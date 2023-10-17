using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class Subject
    {
        public string ID {  get; set; }
        public string typeofsubjectID {  get; set; }
        public string Name { get; set; }
        public Subject(string iD, string typeofsubjectID, string name)
        {
            ID = iD;
            this.typeofsubjectID = typeofsubjectID;
            Name = name;
        }

        public Subject() { }
    }
}
