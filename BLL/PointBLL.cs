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
        private PointDAL studentDAL;
        private PointDAL subjectPointDAL;

        public bool UpdateStudentPoint(string studentName, int regularPoint, int midtermPoint, int finalPoint)
        {
            // Gọi hàm DAL để cập nhật điểm
            PointDAL dal = new PointDAL();
            return dal.UpdateStudentPoint(studentName, regularPoint, midtermPoint, finalPoint);
        }
        public DataTable GetStudentPoints(int academicYearID, int semesterID, int classID, int subjectID)
        {
            subjectPointDAL = new PointDAL();
            return subjectPointDAL.GetStudentPoints(academicYearID, semesterID, classID, subjectID);
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

        public void LoadStudentsNameAndIdIntoComboBox(ComboBox comboBoxID, ComboBox comboBoxName)
        {
            studentDAL = new PointDAL();
            List<Student> students = studentDAL.GetStudentsNameandID();


            comboBoxID.DisplayMember = "ID";
            comboBoxID.ValueMember = "ID";
            comboBoxID.DataSource = students;

            comboBoxName.DisplayMember = "Name";
            comboBoxName.ValueMember = "ID";
            comboBoxName.DataSource = students;

            //ComboBox không chỉnh sửa được và chỉ cho phép chọn giá trị
            comboBoxID.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxName.DropDownStyle = ComboBoxStyle.DropDownList;

            //Để trống Combobox ID học sinh, Tên học sinh khi Enable = True (thuộc tính của comboBox)
            comboBoxID.SelectedIndex = -1;
            comboBoxName.SelectedIndex = -1;
        }
    }
}
