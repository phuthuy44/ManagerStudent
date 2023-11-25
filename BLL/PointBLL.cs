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
    internal class PointBLL
    {
        private PointDAL academicYearDAL;
        private PointDAL classDAL;
        private PointDAL semesterDAL;
        private PointDAL subjectDAL;
        private PointDAL subjectPointDAL;
        private PointDAL studentIdNameDAL;

        public bool UpdateStudentPoint(int studentID, string academicYearName, string semesterName,
                                        string subjectName,string className, string pointName, double Point)
        {
            // Gọi hàm DAL để cập nhật điểm
            PointDAL updatePointDAL = new PointDAL();
            return updatePointDAL.UpdateStudentPoint(studentID, academicYearName, semesterName, subjectName, className, pointName, Point);
        }
        public bool InsertStudentPoint(int studentID, string academicyearName, string semesterName,
                                string subjectName, string pointName, double point)
        {
            // Gọi hàm DAL để thêm điểm
            PointDAL insertPointDAL = new PointDAL();
            return insertPointDAL.InsertStudentPoint(studentID, academicyearName, semesterName,
                subjectName, pointName, point);
        }
        public DataTable GetStudentPoints(string academicYearName, string semesterName, string className, string subjectName)
        {
            subjectPointDAL = new PointDAL();
            return subjectPointDAL.GetStudentPoints(academicYearName, semesterName, className, subjectName);
        }
        public DataTable LoadStudentsNameAndIdIntoComboBox(int academicYearID, int semesterID, int classID)
        {
            studentIdNameDAL = new PointDAL();
            return studentIdNameDAL.GetStudentsNameandID(academicYearID, semesterID, classID);
        }

        public DataTable GetClassesData()
        {
            classDAL = new PointDAL();
            return classDAL.GetallClasses();
        }

        public DataTable GetSemesterData()
        {
            semesterDAL = new PointDAL();
            return semesterDAL.GetallSemester();
        }

        public DataTable GetSujectData()
        {
            subjectDAL = new PointDAL();
            return subjectDAL.GetallSuject();
        }

        public void LoadAcademicYearsIntoComboBox(ComboBox comboBox)
        {
            academicYearDAL = new PointDAL();
            List<AcademicYear> academicYears = academicYearDAL.GetAcademicYears();

            comboBox.ValueMember = "ID";
            comboBox.DisplayMember = "Name";
            comboBox.DataSource = academicYears;

            //ComboBox không chỉnh sửa được và chỉ cho phép chọn giá trị
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            //Lấy năm học hiện tại => bằng cách so sánh Ngày hiện tại với Ngày bắt đầu & Ngày kết thúc năm học
            DateTime currentDate = DateTime.Now;
            foreach (AcademicYear academicYear in academicYears)
            {
                if (currentDate >= academicYear.startDate && currentDate <= academicYear.finishDate)
                {
                    comboBox.SelectedValue = academicYear.ID;
                    break;
                }
            }
        }

        private PointDAL pointDAL; 

        public PointBLL() {  pointDAL = new PointDAL(); }
        public DataTable StudentSummary(int studentID, string academicyearName, string semesterName, string className)
        {
            Console.WriteLine(111111111);
            return pointDAL.StudentSummary(studentID, academicyearName, semesterName, className);
        }
        public DataTable StudentPoint(int studentID, string academicyearName, string semesterName, string className)
        {
            return pointDAL.StudentPoint(studentID, academicyearName, semesterName, className);
        }

    }
}
