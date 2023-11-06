using ManagerStudent.BLL;
using ManagerStudent.DTO;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class HocSinhForm : Form
    {
        private StudentBLL studentBLL;
        private Student student;
        public HocSinhForm()
        {
            InitializeComponent();
            studentBLL = new StudentBLL();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text;
            DateTime ngaySinh = txtDate.Value;
            string gioiTinh = cboxGioiTinh.SelectedItem.ToString();
            string diaChi = txtDiaChi.Text;
            string email = txtEmail.Text;
            string soDienThoai = txtSoDienThoai.Text;
            string image = picStudent.ToString();

            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Trường Họ tên không thể bỏ trống");
            }
            else if (string.IsNullOrEmpty(gioiTinh))
            {
                MessageBox.Show("Trường Giới tính không thể bỏ trống");
            }
            else
            {
                Student st = new Student(hoTen,gioiTinh,diaChi,ngaySinh,email,soDienThoai,image);
                bool result = studentBLL.insertStudent(st);
                if (result)
                {
                    MessageBox.Show("Thêm học sinh " + hoTen + " thành công!");
                    Console.WriteLine(st);
                }
                else
                {
                    MessageBox.Show("Lỗi! Hãy thử lại sau");
                }
            }
            GetListStudent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void HocSinhForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentManagerDataSet.Student' table. You can move, or remove it, as needed.
            //this.studentTableAdapter.Fill(this.studentManagerDataSet.Student);
            GetListStudent();

        }
        //Xu ly fill dataTable lên dataGridView
        public void GetListStudent()
        {
            dataTableStudent.DataSource = studentBLL.GetListStudent();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void HocSinhForm_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentManagerDataSet1.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.studentManagerDataSet1.Student);

        }

        private void cboxGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
