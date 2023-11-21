using DocumentFormat.OpenXml.Spreadsheet;
using ManagerStudent.BLL;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class ClassForm : Form
    {
        List<Grade> grade;
        GradeBLL gradeBll = new GradeBLL();
        List<Class> dscls;
        ClassBLL clsBll = new ClassBLL();
        private int row;


        public ClassForm()
        {

            InitializeComponent();
        }

        public void loadDataGrade()
        {


        }

        public void loadDataClass()
        {

        }

        public void ResetGrade()
        {

        }

        public void ResetClass()
        {

        }

        public void SetControl(bool edit)
        {


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



        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void dgvGrade_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            loadDataGrade();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void dgvGrade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            /*    string searchTerm = txtTimKiem.Text;

                try
                {
                    // Gọi hàm SearchGrades để tìm kiếm dữ liệu
                    List<Grade> searchResults = gradeBll.searchGrades(searchTerm);

                    if (searchResults.Count > 0)
                    {
                        // Cập nhật nguồn dữ liệu của DataGridView
                        dgvGrade.DataSource = searchResults;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy kết quả tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/


        }

        private void btnThem_Click(object sender, EventArgs e)
        {


        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void dgvClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*            if (e.RowIndex == -1) return;
                        DataGridViewRow row = dgvClass.Rows[e.RowIndex];
                        txtMaLop.Text = row.Cells[0].Value.ToString();
                        txtTenLop.Text = row.Cells[1].Value.ToString();
                        txtMaxHocSinh.Text = row.Cells[2].Value.ToString();
                        txtSoLuongHocSinh.Text = row.Cells[3].Value.ToString();*/
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }
    }
}
