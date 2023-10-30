using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class TypeOfPoint
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public int Coefficient {  get; set; }
        public TypeOfPoint(int iD, string name, int coefficient)
        {
            ID = iD;
            Name = name;
            Coefficient = coefficient;
        }

        public TypeOfPoint() { }
    }
}
