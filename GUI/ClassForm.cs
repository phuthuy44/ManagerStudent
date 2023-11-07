using DocumentFormat.OpenXml.Spreadsheet;
using ManagerStudent.BLL;
using ManagerStudent.DAL;
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
        private int row;

        public ClassForm()
        {
          
            InitializeComponent();
        }

        public void loadData()
        {
           
            grade = gradeBll.getAll();        
            dgvGrade.DataSource =  grade;
        }

        public void Reset()
        {
            txtMaKhoi.Text = "";
            txtTenKhoi.Text = "";
            txtSoLuongKhoi.Text = "";
            txtSoLuongLop.Text = "";
        }

        public void SetControl(bool edit)
        {
            txtMaKhoi.Enabled =  false;
            txtTenKhoi.Enabled = edit;
            txtSoLuongKhoi.Enabled = edit;
            txtSoLuongLop.Enabled = edit;
            btnAdd.Enabled =edit;
            btnEdit.Enabled =!edit;
            btnDelete.Enabled = !edit;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ClassForm_Load(object sender, EventArgs e)
        {
            SetControl(true);
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
                row = dgvGrade.Rows.Count;
                Grade gradeDTO = new Grade();

                int lastID = gradeBll.GetLastGradeId();
                int newID = lastID + 1;
                gradeDTO.ID = newID;
                txtMaKhoi.Text = gradeDTO.ID.ToString();
                gradeDTO.Name = txtTenKhoi.Text;
                gradeDTO.maxClassOfGrade = Convert.ToInt32(txtSoLuongKhoi.Text);
                gradeDTO.realClassOfGrade = Convert.ToInt32(txtSoLuongLop.Text);
                gradeBll.insertGrade(gradeDTO);
                MessageBox.Show("Bạn đã thêm thành công");
           
                loadData();
                Reset();
              
            }
        }

        private void dgvGrade_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvGrade.Rows[e.RowIndex];
            txtMaKhoi.Text = row.Cells[0].Value.ToString();
            txtTenKhoi.Text = row.Cells[1].Value.ToString();
            txtSoLuongKhoi.Text = row.Cells[2].Value.ToString();
            txtSoLuongLop.Text = row.Cells[3].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvGrade.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvGrade.SelectedRows[0].Index;
                Grade gradeDTO = grade[selectedIndex];
                gradeDTO.Name = txtTenKhoi.Text;
                gradeDTO.maxClassOfGrade = Convert.ToInt32(txtSoLuongKhoi.Text);
                gradeDTO.realClassOfGrade = Convert.ToInt32(txtSoLuongLop.Text);
                gradeBll.updateGrade(gradeDTO);
                MessageBox.Show("Bạn đã sửa thành công");
                loadData(); // Cập nhật dữ liệu trên DataGridView
                Reset();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khối để sửa đổi");
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvGrade.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvGrade.SelectedRows[0].Index;
                Grade gradeDTO = grade[selectedIndex];
                gradeDTO.Name = txtTenKhoi.Text;
                gradeDTO.maxClassOfGrade = Convert.ToInt32(txtSoLuongKhoi.Text);
                gradeDTO.realClassOfGrade = Convert.ToInt32(txtSoLuongLop.Text);

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khối này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    gradeBll.deleteGrade(gradeDTO);
                    MessageBox.Show("Bạn đã xóa thành công");
                    loadData(); // Cập nhật dữ liệu trên DataGridView
                    Reset();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khối để xóa");
            }
        }

        private void dgvGrade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
