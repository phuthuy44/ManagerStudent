using ManagerStudent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace ManagerStudent.BLL
{
    internal class ThongKeBLL
    {
        private ThongKeDAL thongkeDAL;
        public ThongKeBLL()
        {
            thongkeDAL = new ThongKeDAL();
        }
        public DataTable GetAllNumberStudent()
        {
            return thongkeDAL.GetAllNumberStudent();
        }
        public DataTable GetGradeNumberStudent()
        {
            return thongkeDAL.GetGradeNumberStudent();
        }
        public DataTable GetClassNumberStudent()
        {
            return thongkeDAL.GetClassNumberStudent();
        }

        public DataTable GetClassAyearNumberStudent(string ayName)
        {
            return thongkeDAL.GetClassAyearNumberStudent(ayName);
        }
        public DataTable GetClassSemesNumberStudent(string seName)
        {
            return thongkeDAL.GetClassSemesNumberStudent(seName);
        }
        public DataTable GetGradeAyearNumberStudent(string ayName)
        {
            return thongkeDAL.GetGradeAyearNumberStudent(ayName);
        }
        public DataTable GetGradeSemesNumberStudent(string seName)
        {
            return thongkeDAL.GetGradeSemesNumberStudent(seName);
        }
        public DataTable GetAllAyearNumberStudent(string ayName)
        {
            return thongkeDAL.GetAllAyearNumberStudent(ayName);
        }
        public DataTable GetAllSemesNumberStudent(string seName)
        {
            return thongkeDAL.GetAllSemesNumberStudent(seName);
        }

        public DataTable GetNumberStudent(string ayName, string seName)
        {
            return thongkeDAL.GetNumberStudent(ayName,seName);
        }
        public DataTable GetClassASNumberStudent(string ayName, string seName)
        {
            return thongkeDAL.GetClassASNumberStudent(ayName,seName);
        }
        public DataTable GetGradeASNumberStudent(string ayName, string seName)
        {
            return thongkeDAL.GetGradeASNumberStudent(ayName,seName);
        }
        public DataTable StatisticalCapacity(string academicyearName, string semesterName)
        {
            return thongkeDAL.StatisticalCapacity(academicyearName, semesterName);
        }
        public DataTable StatisticalConduct(string academicyearName, string semesterName)
        {
            return thongkeDAL.StatisticalConduct(academicyearName, semesterName);
        }
    }
}
