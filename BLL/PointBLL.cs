using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
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

        public void LoadClassesIntoComboBox(ComboBox comboBox)
        {
            classDAL = new PointDAL();
            List<Class> classes = classDAL.GetClasses();

            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "ID";
            comboBox.DataSource = classes;

            //ComboBox không chỉnh sửa được và chỉ cho phép chọn giá trị
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            //Để trống Combobox lớp học
            comboBox.SelectedIndex = -1;
        }

        public void LoadSemestersIntoComboBox(ComboBox comboBox)
        {
            semesterDAL = new PointDAL();
            List<Semester> semesters = semesterDAL.GetSemesters();

            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "ID";
            comboBox.DataSource = semesters;

            //ComboBox không chỉnh sửa được và chỉ cho phép chọn giá trị
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        public void LoadSubjectsIntoComboBox(ComboBox comboBox)
        {
            subjectDAL = new PointDAL();
            List<Subject> subjects = subjectDAL.GetSubjects();

            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "ID";
            comboBox.DataSource = subjects;

            //ComboBox không chỉnh sửa được và chỉ cho phép chọn giá trị
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            //Để trống Combobox lớp học
            comboBox.SelectedIndex = -1;
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
