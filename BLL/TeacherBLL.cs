using ManagerStudent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ManagerStudent.BLL
{
    internal class TeacherBLL
    {
        private TeacherDAL teacherDAL;
        public TeacherBLL() { 
            teacherDAL = new TeacherDAL();
        } 
        public DataTable GetDataTeacher()
        {
            return teacherDAL.GetListTeacher();
        }

    }
}
