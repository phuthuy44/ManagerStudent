using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProject.DTO
{
    public class Status
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Status(string iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public Status() { }
    }
}
