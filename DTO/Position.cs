using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProject.DTO
{
    public class Position
    {
        public string ID {  get; set; }
        public string Name { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public DateTime finishDate{ get; set; }
        public Position(string iD, string name, DateTime createDate, DateTime updateDate, DateTime finishDate)
        {
            ID = iD;
            Name = name;
            this.createDate = createDate;
            this.updateDate = updateDate;
            this.finishDate = finishDate;
        }
        public Position() { }
    }
}
