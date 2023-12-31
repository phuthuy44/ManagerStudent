﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class AcademicYear
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime finishDate { get; set; }
        public AcademicYear(int iD, string name, DateTime startDate, DateTime finishDate)
        {
            ID = iD;
            Name = name;
            this.startDate = startDate;
            this.finishDate = finishDate;
        }

        public AcademicYear() { }
    }
}
