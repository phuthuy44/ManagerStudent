using ManagerStudent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerStudent.DTO;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Bibliography;
using Org.BouncyCastle.Crypto;

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
        public DataTable SearchAllTeacher(string s)
        {
            return teacherDAL.SearchAllTeacher(s);
        }
        
        public DataTable GetAssignment()
        {
            return teacherDAL.GetAssignment(); 
        }
        public DataTable GetAssignmentTeacher(int id, string ayName, string semesName)
        {
            return teacherDAL.GetAssignmentTeacher(id, ayName, semesName);
        }
        public DataTable GetAssignmentClass(string clsname, string ayName, string semesName)
        {
            return teacherDAL.GetAssignmentClass(clsname, ayName, semesName);
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
            return  teacherDAL.insertTeacher(teacher);
        }
        public bool InsertTeacherID(Teacher teacher)
        {
            return teacherDAL.insertTeacherID(teacher);
        }
        public int GetIdSubject(string sbname)
        {
            return teacherDAL.GetIdSubject(sbname);
        }
        public int GetIdAY(string name)
        {
            return teacherDAL.GetIdAY(name);
        }
        public int GetIdSemester(string name)
        {
            return teacherDAL.GetIdSemester(name);
        }
        public int GetIdClass(string name)
        {
            return teacherDAL.GetIdClass(name);
        }
        public int GetIdPosition(string name)
        {
            return teacherDAL.GetIdPosition(name);
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
        public bool DeleteTechnical(int id)
        {
            return teacherDAL.DeleteTechnical(id);
        }
        public bool DeleteAssignment(int idCls, int idSb, int idAy, int idSe, int idTea)
        {
            return teacherDAL.DeleteAssignment(idCls, idSb, idAy, idSe, idTea);
        }
        public bool InsertAssignment(int idCls, int idSb, int idAy, int idSe, int idTea, int idPos)
        {
            return teacherDAL.InsertAssignment(idCls, idSb, idAy, idSe, idTea, idPos);
        }

        public bool CheckTeacher(int id)
        {
            return teacherDAL.CheckTeacher(id);
        }
        public bool CheckClass(int idCls, int idSb, int idAy, int idSe)
        {
            return teacherDAL.CheckClass(idCls, idSb, idAy, idSe);
        }
        public bool CheckPosition(int idCls, int idAy, int idSe)
        {
            return teacherDAL.CheckPosition(idCls, idAy, idSe);
        }
    }
}
