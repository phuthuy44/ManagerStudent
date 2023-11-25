using ManagerStudent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerStudent.DTO;
using System.Windows.Forms;

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
        public DataTable GetSubjectTeacher()
        {
            return teacherDAL.GetSubjectTeacher(); 
        }
        public bool InsertTeacher(Teacher teacher)
        {
            bool rs = teacherDAL.insertTeacher(teacher);
            if(rs)
            {
                return true;
            }
            else {
                return false; 
            }
        }
        public int GetIdSubject(string sbname)
        {
            return teacherDAL.GetIdSubject(sbname);
        }
        public int GetIdTeacherLast()
        {
            return teacherDAL.GetIdTeacherLast();
        }
        public bool InsertSubTecher(int id1, int id2)
        {
            bool rs = teacherDAL.InsertSubOfTecher(id1, id2);
            if(rs)
            {
                return true;
            }
            else { return false; }
        }

    }
}
