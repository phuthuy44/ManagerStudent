using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using ManagerStudent.BLL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class AddStudentForm : Form
    {
        private StudentBLL studentBLL;
        private HocSinhForm hocSinhForm;
        public AddStudentForm(HocSinhForm hocsinhForm)
        {
            studentBLL = new StudentBLL();
            this.hocSinhForm = hocsinhForm;
            InitializeComponent();
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            getListStudentNotInAssigment();
            List<AcademicYear> academicYears = studentBLL.getYear();
            /*List<StudentClassSemesterAcademicYear> distinctAcademicYears = academicYears
                .GroupBy(a => a.academicyearID)
                .Select(g => g.First())
                .ToList();*/

            foreach (AcademicYear a in academicYears)
            {
                /*string AcademicName = studentBLL.getAcademicName(a.academicyearID);*/
                string s = a.Name;
                txtYear.Items.Add(s);
                txtYear.SelectedIndex = 0;
            }

            List<Grade> g = studentBLL.GetGrades();
            foreach (Grade grade in g)
            {
                string s = grade.Name;
                txtKhoi.Items.Add(s);
                txtKhoi.SelectedIndex = 0;
            }

            List<Class> cls = studentBLL.GetClasses();
            foreach(Class c in cls)
            {
                string s = c.Name;
                txtClass.Items.Add(s);
                txtClass.SelectedIndex = 0;
            }

            List<Semester> semester = studentBLL.getSemester();
            foreach (Semester s in semester)
            {
                string semesterName = s.Name;
                txtSemester.Items.Add(semesterName);
                txtSemester.SelectedIndex = 0;
            }
            updateTableWhenSelectedClass();

        }
        public void getListStudentNotInAssigment()
        {
            dataTableStudentNotInAssigment.DataSource = studentBLL.getStudentNotInAssignment();
        }
        private void txtYear_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtKhoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*string selected = txtKhoi.SelectedItem.ToString();
            int gradeID = studentBLL.getGradeID(selected);
            List<StudentClassSemesterAcademicYear> cls = studentBLL.getClass(gradeID);
            txtClass.Items.Clear();
            //List< StudentClassSemesterAcademicYear> distinctClass = cls.GroupBy(a => a.classID).Select(g => g.First()).ToList();
            foreach (StudentClassSemesterAcademicYear c in cls)
            {
                string className = studentBLL.getClassName(c.classID);
                txtClass.Items.Add(className);
                txtClass.SelectedIndex = 0;
            }*/
        }

        public void updateTableWhenSelectedClass()
        {
            string selectedYear = txtYear.SelectedItem.ToString();
            string selectedGrade = txtKhoi.SelectedItem.ToString();
            string selected = txtClass.SelectedItem.ToString();
            string selectedSe = txtSemester.Text;
            int yearID = studentBLL.getIdAca(selectedYear);
            int gradeID = studentBLL.getGradeID(selectedGrade);
            int classID = studentBLL.getClassID(selected);
            int semesID = studentBLL.getIDSemester(selectedSe);
            Console.WriteLine(classID);
            lblQuantity.Text = studentBLL.getQuantity(yearID,gradeID,classID, semesID).ToString();
            DataTable dataTable = studentBLL.getListStudentInClass(yearID,gradeID, classID, semesID);
            // DataView dataView = new DataView(dataTable);
            lblClass.Text = selected;
            dataTableClass.Columns.Clear();
            dataTableClass.Columns.Add("ID", "Mã học sinh");
            dataTableClass.Columns.Add("Name", "Tên học sinh");
            dataTableClass.Columns.Add("Gender", "Giới tính");
            // Map the columns to the corresponding columns in the DataTable

            dataTableClass.Columns["ID"].DataPropertyName = "ID";
            dataTableClass.Columns["Name"].DataPropertyName = "name";
            dataTableClass.Columns["Gender"].DataPropertyName = "gender";
            dataTableClass.DataSource = dataTable;
            dataTableClass.DataBindings.Clear();
        }

        private void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
           updateTableWhenSelectedClass();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string namhoc = txtYear.SelectedItem.ToString();
            int namhocID = studentBLL.getIdAca(namhoc);
            string khoi = txtKhoi.SelectedItem.ToString();
            int khoiID = studentBLL.getGradeID(khoi);
            string hocky = txtSemester.SelectedItem.ToString();
            int hockyID = studentBLL.getIDSemester(hocky);
            string classtxt = txtClass.SelectedItem.ToString();
            int classID = studentBLL.getClassID(classtxt);
            int quantity = int.Parse(lblQuantity.Text);
            List<int> selectedStudentIDs = new List<int>();
            DataTable dataClass = dataTableClass.DataSource as DataTable;
            DataTable dataStudent = dataTableStudentNotInAssigment.DataSource as DataTable;
            if(quantity < 1)
            {
                MessageBox.Show("Lớp đã đầy! Không thể thêm","Cảnh báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (DataGridViewRow item in dataTableStudentNotInAssigment.SelectedRows)
            {
                if (quantity < 1)
                {
                    MessageBox.Show("Lớp đã đầy! Không thể thêm", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    int studentID = Convert.ToInt32(item.Cells["Mã học sinh"].Value);//Lay ID cua hoc sinh tu cot "ID" cua hang do va them vao danh sach 
                    selectedStudentIDs.Add(studentID);
                    //Them mot hang moi trong newDataTable 
                    DataRow newRow = dataClass.NewRow();
                    newRow["ID"] = item.Cells["Mã học sinh"].Value;
                    newRow["Name"] = item.Cells["Tên học sinh"].Value;
                    newRow["Gender"] = item.Cells["Giới tính"].Value;
                    dataClass.Rows.Add(newRow);
                    StudentClassSemesterAcademicYear p = new StudentClassSemesterAcademicYear(studentID, classID, hockyID, namhocID, khoiID);
                    studentBLL.insertStudent(p);
                    dataStudent.Rows.RemoveAt(item.Index);
                    quantity--;
                    lblQuantity.Text = quantity.ToString();
                    hocSinhForm.updateTableWhenSelectedClass_New();
                    hocSinhForm.updateTableWhenSelectedClass_Old();
                }

            }
            MessageBox.Show("Thêm học Sinh vào lớp" + classtxt + " thành công!","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
