using ManagerStudent.BLL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class ClassForm : Form
    {
        List<Grade> grade;
        GradeBLL gradeBll = new GradeBLL();

        public ClassForm()
        {
            InitializeComponent();
        }

        public void loadData()
        {
             grade = gradeBll.getAll();
            dgvGrade.DataSource =  grade;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ClassForm_Load(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

      

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
          if(string.IsNullOrEmpty(txtTenKhoi.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khối");
            } else if(string.IsNullOrEmpty(txtSoLuongKhoi.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng trong khối");
            } else if(string.IsNullOrEmpty(txtSoLuongLop.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng lớp trong khối");
            } else
            {
                Grade gradeDTO = new Grade();
             /*   gradeDTO.ID = Convert.ToInt32(txtMaKhoi.Text);*/
                gradeDTO.Name = txtTenKhoi.Text;
                gradeDTO.maxClassOfGrade = Convert.ToInt32(txtSoLuongKhoi.Text);
                gradeDTO.realClassOfGrade = Convert.ToInt32(txtSoLuongLop.Text);
                gradeBll.insertGrade(gradeDTO);
                MessageBox.Show("Bạn đã thêm thành công");
                loadData();
            }
        }
    }
}
