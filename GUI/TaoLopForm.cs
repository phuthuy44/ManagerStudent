using ManagerStudent.BLL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class TaoLopForm : Form
    {
        private StudentBLL studentBLL;
        private HocSinhForm hocSinhForm;

        public TaoLopForm(HocSinhForm hocSinhForm)
        {
            studentBLL = new StudentBLL();
            this.hocSinhForm = hocSinhForm;
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        //Luu
        private void button3_Click(object sender, EventArgs e)
        {
            // Kiểm tra các combobox đã được chọn hay chưa
            if (txtNH.SelectedItem == null || txtKhoi.SelectedItem == null || txtSemester.Text == null || txtClass.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int idNH = studentBLL.getIdAca(txtNH.SelectedItem.ToString());
            int idKhoi = studentBLL.getGradeID(txtKhoi.SelectedItem.ToString());
            int idSemes = studentBLL.getIDSemester(txtSemester.SelectedItem.ToString());
            string selectedClass = txtClass.SelectedItem.ToString();
            int classID = studentBLL.getClassID(selectedClass);
            int txt = int.Parse(txtConLai.Text);
            List<int> selectedStudentIDs = new List<int>();
            DataTable dataStudent = dataTableRandom.DataSource as DataTable;
            if (dataStudent == null)
            {
                MessageBox.Show("Chưa có danh sách để thêm học sinh", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                for (int i = dataStudent.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow item = dataTableRandom.Rows[i];
                    int studentID = Convert.ToInt32(item.Cells["Mã học sinh"].Value);
                    selectedStudentIDs.Add(studentID);
                    StudentClassSemesterAcademicYear p = new StudentClassSemesterAcademicYear(studentID, classID, idSemes, idNH, idKhoi);
                    studentBLL.insertStudent(p);
                    dataStudent.Rows.RemoveAt(i);
                    txt--;
                    txtRandom.Text = "0";
                    txtConLai.Text = txt.ToString();
                    hocSinhForm.updateTableWhenSelectedClass_New();
                    hocSinhForm.updateTableWhenSelectedClass_Old();



                }
                MessageBox.Show("Thêm học sinh vào lớp " + selectedClass + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TaoLopForm_Load(object sender, EventArgs e)
        {
            List<AcademicYear> ac = studentBLL.getYear();
            foreach (AcademicYear a in ac)
            {
                /*string AcademicName = studentBLL.getAcademicName(a.academicyearID);*/
                string s = a.Name;
                txtNH.Items.Add(s);
               txtNH.SelectedIndex = 0;
            }

            List<Grade> g = studentBLL.GetGrades();
            foreach (Grade grade in g)
            {
                string s = grade.Name;
                txtKhoi.Items.Add(s);
                txtKhoi.SelectedIndex = 0;
            }

            List<Class> cls = studentBLL.GetClasses();
            foreach (Class c in cls)
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
            /*txtMax.Text = "0";
            txtSiSo.Text = "0";
            txtConLai.Text = "0";*/
            txtRandom.Text = "0";
            txtClass_SelectedIndexChanged(sender, new EventArgs());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idNH = studentBLL.getIdAca(txtNH.SelectedItem.ToString());
            int idKhoi = studentBLL.getGradeID(txtKhoi.SelectedItem.ToString());
            int idSemes = studentBLL.getIDSemester(txtSemester.Text);
            string selectedClass = txtClass.SelectedItem.ToString();
            int classID = studentBLL.getClassID(selectedClass);
            txtMax.Text = studentBLL.getMaxStudentInClass(classID).ToString();
            txtSiSo.Text = studentBLL.getCurrentStudent(idNH,idKhoi,classID,idSemes).ToString();
            txtConLai.Text = studentBLL.getQuantity(idNH, idKhoi, classID, idSemes).ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int txt = int.Parse(txtConLai.Text);
            int randomtxt = int.Parse(txtRandom.Text);
            if(randomtxt == 0)
            {
                MessageBox.Show("Nhập số lượng cần tạo!","Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if(randomtxt > txt)
            {
                MessageBox.Show("Số lượng cần tạo lớn hơn số lượng chứa hiện tại của lớp! Hãy tạo lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //int txtRandom = int.Parse(txtRandom.ToString());
                DataTable dataTable = studentBLL.getStudentNotinAssignment_TOP(randomtxt);
                // DataView dataView = new DataView(dataTable);
                dataTableRandom.DataSource = dataTable;
                dataTableRandom.DataBindings.Clear();
            }
        }

    }
}
