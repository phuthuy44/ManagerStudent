using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class Conduct
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public float upperLimit {  get; set; }
        public float lowerLimit { get; set; }
        public Conduct(string iD, string name, float upperLimit, float lowerLimit)
        {
            ID = iD;
            Name = name;
            this.upperLimit = upperLimit;
            this.lowerLimit = lowerLimit;
        }
        public Conduct() { }
    }
}
