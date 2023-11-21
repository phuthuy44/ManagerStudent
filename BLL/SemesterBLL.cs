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
        public DataTable FindSemester(string str)
        {
            return getSemesterData.FindSemester(str);
        }
        public bool insertSemesters(string semesterName, int coefficient)
        {
            GetSemesterData insertSemesterDataBLL = new GetSemesterData();
            return insertSemesterDataBLL.insertSemester(semesterName, coefficient);
        }

        public bool updateSemesters(string semesterName, int coefficient, int ID)
        {
            GetSemesterData updateSemesterData = new GetSemesterData();
            return updateSemesterData.updateSemester(semesterName, coefficient, ID);

        }
        public bool deleteSemesters(string semesterName)
        {
            GetSemesterData deleteSemesterData = new GetSemesterData();
            return deleteSemesterData.deleteSemester(semesterName);
        }

        public bool checkInsertSemesterName(string semesterName)
        {
            GetSemesterData checkInsertSemesterData = new GetSemesterData();
            return checkInsertSemesterData.checkInsertSemesterName(semesterName);
        }
        public bool checkUpdateSemesterName(int ID, string semesterName)
        {
            GetSemesterData checkInsertSemesterData = new GetSemesterData();
            return checkInsertSemesterData.checkUpdateSemesterName(ID, semesterName);
        }
    }
}
