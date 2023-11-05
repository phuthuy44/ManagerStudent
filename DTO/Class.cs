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
        public int realStudent { get; set; }
        public int quantityMale {  get; set; }
        public int quantityFemale { get; set; }
        public Class(int iD, string name,int maxStudent, int realStudent, int quantityMale, int quantityFemale)
        {
            ID = iD;
            Name = name;
           
            this.maxStudent = maxStudent;
            this.realStudent = realStudent;
            this.quantityMale = quantityMale;
            this.quantityFemale = quantityFemale;
        }

        public Class() { }

    }
}
