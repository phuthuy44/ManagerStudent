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
        public int maxClassOfGrade { get; set; }
        public int realClassOfGrade { get; set; }
        public Grade(int iD, string name, int maxClassOfGrade, int realClassOfGrade)
        {
            ID = iD;
            Name = name;
            this.maxClassOfGrade = maxClassOfGrade;
            this.realClassOfGrade = realClassOfGrade;
        }

        public Grade() { }
    }
}
