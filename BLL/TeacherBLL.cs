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
        public DataTable GetAssignmentTeacher()
        {
            return teacherDAL.GetAssignmentTeacher(); 
        }
        public DataTable GetAcademicYear()
        {
            return teacherDAL.GetAcademicYear();
        }
        public DataTable GetSemester() {
            return teacherDAL.GetSemester();
        }
        public DataTable GetClass()
        {
            return teacherDAL.GetClass();
        }
        public DataTable GetPosition()
        {
            return teacherDAL.GetPosition();
        }
        public DataTable GetSubjectTeacher(int id)
        {
            return teacherDAL.GetSubjectTeacher(id);
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
            return teacherDAL.InsertSubOfTecher(id1, id2);
            
        }
        public bool UpdateTeacher(Teacher teacher)
        {
            return teacherDAL.EditTeacher(teacher); 
        }
        public bool DeleteTeacher(int id)
        {
            return teacherDAL.DeleteTeacher(id);
        }
        public bool DeleteSubOfTeacher(int id)
        {
            return teacherDAL.DeleteSubOfTeacher(id);
        }
        public bool DeleteAssignment(int id)
        {
            return teacherDAL.DeleteAssignment(id);
        }
    }
}
