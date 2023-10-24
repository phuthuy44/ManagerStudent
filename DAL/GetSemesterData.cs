using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DAL
{
    internal class GetSemesterData
    {
        public IList<Semester> GetAllSemester()
        {
            IList<Semester> semesters = new List<Semester>();
            return semesters;
        }
    }
}
