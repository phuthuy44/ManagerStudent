using ManagerStudent.BLL;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class TeacherForm : Form
    {
        private TeacherBLL teacherBLL;
        private PointBLL pointBLL;   
        private Teacher teacher;

        public TeacherForm()
        {
            InitializeComponent();
            teacherBLL = new TeacherBLL();
            pointBLL=new PointBLL();
        }
        public void FillTableTeacher()
        {
            TableTeacher.DataSource = teacherBLL.GetDataTeacher();
            /*TableTeacher.Columns[1].Width = 150;*/
            /*TableTeacher.Columns[2].Width = 40;
            TableTeacher.Columns[0].Width = 30;*/
            TableTeacher.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TableTeacher.Columns["Tên Giáo viên"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TableTeacher.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        public void FillTableTechnical()
        {
            DataTable tc = teacherBLL.GetSubjectTeacher();
            TableAssignment.DataSource = tc;
        }

        private void TeacherForm_Load(object sender, System.EventArgs e)
        {
            
            FillTableTeacher();
            FillTableTechnical();
            DataTable subject = pointBLL.GetSujectData();
            foreach (DataRow row in subject.Rows)
            {
                string subjectname = row["SubjectName"].ToString();
        
                cbTechnical.Items.Add(subjectname);
            }
        }
        private async void InsertTecher()
        {

        }


        private void TableTeacher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = TableTeacher.Rows[e.RowIndex];
                //pictureBox1_Click(sender, e);
                //Check
                if (row.Cells[0].Value != null)
                {
                    txtMaGV.Text = row.Cells[0].Value.ToString();

                }
                if (row.Cells[1].Value != null)
                {
                    txtHoTenGV.Text = row.Cells[1].Value.ToString();
                }
                if (row.Cells[2].Value != null)
                {
                    cbGender.Text = row.Cells[2].Value.ToString();

                }
                if (row.Cells[3].Value != null)
                {
                    BirthdayGV.Text = row.Cells[3].Value.ToString();
                }
                if (row.Cells[4].Value != null)
                {
                    txtEmailGV.Text = row.Cells[4].Value.ToString();
                }
                if (row.Cells[5].Value != null)
                {
                    txtSDTGV.Text = row.Cells[5].Value.ToString();
                }
                if (row.Cells[6].Value != null)
                {
                    txtDiaChiGV.Text = row.Cells[6].Value.ToString();
                }
                if (row.Cells[7].Value != null)
                {
                    cbTechnical.Text= row.Cells[7].Value.ToString();
                }
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string name = txtHoTenGV.Text;
            string email = txtEmailGV.Text;
            string sdt = txtSDTGV.Text;
            string gender = cbGender.SelectedItem?.ToString();
            string image = System.IO.Path.GetFileName(picGV.Text);
            string address = txtDiaChiGV.Text;
            string technical = cbTechnical.SelectedItem?.ToString();
            int idsub = teacherBLL.GetIdSubject(technical);
            int idtecher;
            DateTime ngaySinh = BirthdayGV.Value.Date;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Trường họ tên không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Trường giới tính không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(technical))
            {
                MessageBox.Show("Trường chuyên môn không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!string.IsNullOrEmpty(sdt) && sdt.Length != 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                teacher = new Teacher(name, gender, address, ngaySinh, email, sdt, image);
                bool result = teacherBLL.InsertTeacher(teacher);
                if (result)
                {
                    idtecher = teacherBLL.GetIdTeacherLast();
                    bool result2 = teacherBLL.InsertSubTecher(idtecher, idsub);
                    if (result2)
                    {
                        MessageBox.Show("Thêm giáo viên " + name + " thành công!");

                        FillTableTeacher();
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
