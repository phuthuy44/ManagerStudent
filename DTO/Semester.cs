using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProject.DTO
{
    public class Semester
    {
        public string ID { get; set; }
        /*public required string Name { get; set; }
        public required string Coefficient { get; set; }*/
        public  string Name { get; set; }
        public  string Coefficient { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
        public Semester(string iD, string name, string coefficient, DateTime startDate, DateTime finishDate)
        {
            ID = iD;
            Name = name;
            Coefficient = coefficient;
            this.startDate = startDate;
            this.finishDate = finishDate;
        }

        public Semester() { }
    }
}
