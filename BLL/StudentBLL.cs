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
    internal class StudentBLL
    {
        
       private StudentDAL studentDAL;
        //private IList<Student> students;
        public StudentBLL()
        {
            studentDAL = new StudentDAL();
        }

        //Xu ly fill data len DataGridView
        public DataTable GetListStudent()
        {
            return studentDAL.GetListStudent();
        }
        //insert
        public bool insertStudent(Student student)
        {
            bool success = studentDAL.insertStudent(student);
            if (success)
            {
                Console.WriteLine("Student record inserted successfully");
                return true;
            }
            else
            {
                return false;

            }
        }

    }
}
