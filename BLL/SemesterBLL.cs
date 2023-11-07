using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.BLL
{
    internal class SemesterBLL
    {
        private GetSemesterData getSemesterData;

        public SemesterBLL()
        {
            getSemesterData = new GetSemesterData();
        }
        public DataTable SemesterData()
        {
            return getSemesterData.GetAllSemester();
        }
    }
}
