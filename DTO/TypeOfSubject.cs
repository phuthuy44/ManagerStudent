using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class TypeOfSubject
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public int quantityTest {  get; set; }
        public TypeOfSubject(string iD, string name, int quantityTest)
        {
            ID = iD;
            Name = name;
            this.quantityTest = quantityTest;
        }

        public TypeOfSubject() { }
    }
}
