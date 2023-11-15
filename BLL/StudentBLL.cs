using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                Console.WriteLine("Thêm học sinh thành công!");
                return true;
            }
            else
            {
                return false;

            }
        }
        public bool deleteStudent(string idStudent,out bool isLoiKhoaNgoai)
        {
            bool success = studentDAL.deleteStudent(idStudent,out isLoiKhoaNgoai);
            if(success)
            {
                Console.WriteLine("Xóa học sinh thành công");
                return true;
            }
            else
            {
                Console.WriteLine("Hệ thống lỗi! Không thể xóa!");
                return false;
            }
        }
        public bool updateStudent(Student student)
        {
            bool success=studentDAL.updateStudent(student);
            if (success)
            {
                Console.WriteLine("Cập nhật thông tin thành công!");
                return true;
            }
            else
            {
                Console.WriteLine("Hệ thống lỗi! Không thể cập nhật thông tin");
                return false;
            }
        }
        /*PhanLop*/
        public List<StudentClassSemesterAcademicYear> GetAcademicYears()
        {
            return studentDAL.getAcademicYearsInAssignmentClass();
        }
        public string getAcademicName(int ma)
        {
            return studentDAL.getAcademicName(ma);
        }
        public int getIdAca(string name)
        {
            return studentDAL.getIDAcademic(name);
        }
        public List<StudentClassSemesterAcademicYear> GetGrades(int idYear)
        {
            return studentDAL.getGrade(idYear);
        }
        public string getNameGrade(int id)
        {
            return studentDAL.namGrade(id);
        }
        public List<Class> getClassInGrade(string maKhoi)
        {
            return studentDAL.getClassInGrade(maKhoi);
        }
        public string getMaKhoi(string tenKhoi)
        {
            return studentDAL.getMaGrade(tenKhoi);
        }
    }
}
