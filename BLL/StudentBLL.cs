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
        public int getGradeID(string name)
        {
            return studentDAL.getGradeID(name);
        }
        /*public List<Class> getClassInGrade(string maKhoi)
        {
            return studentDAL.getClassInGrade(maKhoi);
        }
        public string getMaKhoi(string tenKhoi)
        {
            return studentDAL.getMaGrade(tenKhoi);
        }*/
        public List<StudentClassSemesterAcademicYear> getClass(int id)
        {
            return studentDAL.getClassInGrade(id);
        }
        public string getClassName(int id)
        {
            return studentDAL.namClass(id);
        }
        public int getClassID(string name)
        {
            return studentDAL.getClassID(name);
        }
        public DataTable getListStudentInClass(int yearID, int gradeID,int classID, int idSe)
        {
            return studentDAL.getListStudentInClass(yearID,gradeID,classID, idSe);
        }
        public DataTable getListStudentInClassTraCuu(int yearID, int gradeID, int classID, int idSe)
        {
            return studentDAL.getListStudentInClassTraCuu(yearID,gradeID,classID, idSe);
        }
        public List<StudentClassSemesterAcademicYear> getStudentIdFromPhanLop(int id1, int id2, int id3,int id)
        {
            return studentDAL.getIDStudentFromPhanLop(id1,id2,id3,id);
        }
        public List<Student> getStudentInformFromID(int id)
        {
            return studentDAL.getDataIntoText(id);
        }
        public List<Semester> getSemester()
        {
            return studentDAL.getSemester();
        }
        public int getIDSemester(string name)
        {
            return studentDAL.getSemesterID(name);
        }
        public bool updateStudentInPhanLop(StudentClassSemesterAcademicYear student)
        {
            bool result = studentDAL.updateStudentInPhanLop(student);
            if(result)
            {
                Console.WriteLine("Success!");
                return true;
            }
            else
            {
                Console.WriteLine("Faild");
                return false;
            }
        }
        public bool insertStudent(StudentClassSemesterAcademicYear student)
        {
            bool result = studentDAL.insertStudentNotInAssginment(student);
            if (result)
            {
                Console.WriteLine("Success!");
            return true;
            }
            else
            {
                Console.WriteLine("Faild!");
                return false;
            }
        }
        public int getQuantity(int acID, int gradeID,int classID,int se)
        {
            return studentDAL.getQuantity(acID, gradeID,classID, se);
        }
        public int getCurrentStudent(int acID, int gradeID,int classID, int se)
        {
            return studentDAL.getCurrentStudentInClass(acID, gradeID,classID,se);
        }
        public int getMaxStudentInClass(int classID)
        {
            return studentDAL.getMaxStudentInClass(classID);
        }
        public DataTable getStudentNotInAssignment()
        {
            return studentDAL.GetListStudentNotInAssignment();
        }
        public DataTable getStudentNotinAssignment_TOP(int row)
        {
            return studentDAL.GetListStudentNotInAssignment_top(row);
        }
        public List<AcademicYear> getYear()
        {
            return studentDAL.getYear();
        }
        public List<Class> GetClasses()
        {
            return studentDAL.getClass();
        }
        public List<Grade> GetGrades()
        {
            return studentDAL.getGradeInAddStudentView();
        }
        public bool checkStudentCount(int studentCount)
        {
            return studentDAL.checkStudent(studentCount);
        }
        public DataTable searchStudent(string selected, string searchText)
        {
            DataTable search = new DataTable();
            if (selected == "Tên")
            {
                search = studentDAL.searchStudentByNam(searchText);
            }
            if (selected == "Mã")
            {
                if (int.TryParse(searchText, out int id)) 
                { 

                    search = studentDAL.searchStudentByID(id);
                }
            }
            if(selected == "Giới tính")
            {
                search = studentDAL.searchStudentByGender(searchText);
            }
            if(selected == "Số điện thoại")
            {
                search = studentDAL.searchStudentBySDT(searchText);
            }
            if(selected == "Email")
            {
                search = studentDAL.searchStudentByEmail(searchText);
            }
            return search;
        }
    }
}
