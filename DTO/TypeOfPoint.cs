using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProject.DTO
{
    public class TypeOfPoint
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public int Coefficient {  get; set; }
        public TypeOfPoint(string iD, string name, int coefficient)
        {
            ID = iD;
            Name = name;
            Coefficient = coefficient;
        }

        public TypeOfPoint() { }
    }
}
