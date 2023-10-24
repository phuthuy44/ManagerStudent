using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class Capacity
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public float upperLimit {  get; set; }
        public float lowerLimit { get; set; }
        public float paraPoint { get; set; }
        public Capacity(int iD, string name, float upperLimit, float lowerLimit, float paraPoint)
        {
            ID = iD;
            Name = name;
            this.upperLimit = upperLimit;
            this.lowerLimit = lowerLimit;
            this.paraPoint = paraPoint;
        }

        public Capacity() { }

    }
}
