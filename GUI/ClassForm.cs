using DocumentFormat.OpenXml.Spreadsheet;
using ManagerStudent.BLL;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
using OfficeOpenXml.Drawing.Controls;
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
            grade = gradeBll.getAll();
            dgvGrade.DataSource = grade;

        }

        public void loadDataClass()
        {
            dscls = clsBll.getAll();
            dgvClass.DataSource = dscls;
        }

        public void ResetGrade()
        {
            txtMaKhoi.Text = "";
            txtTenKhoi.Text = "";
            txtSoLuongKhoi.Text = "";
            txtSoLuongLop.Text = "";
        }

        public void ResetClass()
        {
            txtMaLop.Text = "";
            txtTenLop.Text = "";
            txtMaxHocSinh.Text = "";
            txtSoLuongHocSinh.Text = "";
        }

        public void SetControl(bool edit)
        {
            txtMaKhoi.Enabled = false;
            /*   txtTenKhoi.Enabled = edit;
               txtSoLuongKhoi.Enabled = edit;
               txtSoLuongLop.Enabled = edit;
               btnAdd.Enabled =edit;
               btnEdit.Enabled =!edit;
               btnDelete.Enabled = !edit;*/


        }
        public void SetControlClass(bool edit)
        {
            txtMaLop.Enabled = false;
            /*   txtTenKhoi.Enabled = edit;
               txtSoLuongKhoi.Enabled = edit;
               txtSoLuongLop.Enabled = edit;
               btnAdd.Enabled =edit;
               btnEdit.Enabled =!edit;
               btnDelete.Enabled = !edit;*/


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ClassForm_Load(object sender, EventArgs e)
        {
            SetControl(true);
            SetControlClass(true);
            loadDataGrade();
            loadDataClass();
            foreach (DataGridViewColumn columnGrade in dgvGrade.Columns)
            {
                columnGrade.ReadOnly = true;
            }
            foreach (DataGridViewColumn columnClass in dgvClass.Columns)
            {
                columnClass.ReadOnly = true;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
 
                if (string.IsNullOrEmpty(txtTenKhoi.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khối");
                }
                else if (string.IsNullOrEmpty(txtSoLuongKhoi.Text))
                {
                    MessageBox.Show("Vui lòng nhập lớp tối đa trong khối");
                }
                else if (string.IsNullOrEmpty(txtSoLuongLop.Text))
                {
                    MessageBox.Show("Vui lòng nhập lớp thực tế trong khối");
                }

                else if (!txtSoLuongKhoi.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Lớp tối đa chỉ được nhập số từ 0-9");
                }
                else if (!txtSoLuongLop.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Lớp thực tế chỉ được nhập số từ 0-9");
                }
                else if (Convert.ToInt32(txtSoLuongLop.Text) > Convert.ToInt32(txtSoLuongKhoi.Text))
                {
                    MessageBox.Show("Số lượng lớp thực tế không được lớn hơn số lượng tối đa trong khối");
                }
                else
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
                    string result = gradeBll.insertGrade(gradeDTO);
                    if (result == "Tên đã tồn tại")
                    {
                        MessageBox.Show(result);
                        return; // Dừng thực thi của hàm btnAdd_Click
                    }
                    MessageBox.Show(result, "Bạn đã thêm thành công");
                    loadDataGrade();
                    ResetGrade();
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

                    if (string.IsNullOrEmpty(txtTenKhoi.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên khối");
                    }
                    else if (string.IsNullOrEmpty(txtSoLuongKhoi.Text))
                    {
                        MessageBox.Show("Vui lòng nhập lớp tối đa trong khối");
                    }
                    else if (string.IsNullOrEmpty(txtSoLuongLop.Text))
                    {
                        MessageBox.Show("Vui lòng nhập lớp thực tế trong khối");
                    }

                    else if (!txtSoLuongKhoi.Text.All(char.IsDigit))
                    {
                        MessageBox.Show("Lớp tối đa chỉ được nhập số từ 0-9");
                    }
                    else if (!txtSoLuongLop.Text.All(char.IsDigit))
                    {
                        MessageBox.Show("Lớp thực tế chỉ được nhập số từ 0-9");
                    }
                    else if (Convert.ToInt32(txtSoLuongLop.Text) > Convert.ToInt32(txtSoLuongKhoi.Text))
                    {
                        MessageBox.Show("Số lượng lớp thực tế không được lớn hơn số lượng tối đa trong khối");
                    }
                    else
                    {
                        gradeDTO.Name = txtTenKhoi.Text;
                        gradeDTO.maxClassOfGrade = Convert.ToInt32(txtSoLuongKhoi.Text);
                        gradeDTO.realClassOfGrade = Convert.ToInt32(txtSoLuongLop.Text);
                        MessageBox.Show(gradeBll.updateGrade(gradeDTO), "Bạn đã sửa thành công");
                        loadDataGrade(); // Cập nhật dữ liệu trên DataGridView
                        ResetGrade();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một khối để sửa đổi");
                }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            loadDataGrade();
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
                    gradeBll.deleteGrade(gradeDTO.ID);
                    MessageBox.Show("Bạn đã xóa thành công");
                    loadDataGrade(); // Cập nhật dữ liệu trên DataGridView
                    ResetGrade();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khối để xóa");
            }
        }

        private void dgvGrade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadDataGrade();
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
            if (string.IsNullOrEmpty(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập tên lớp");
            }
            else if (string.IsNullOrEmpty(txtMaxHocSinh.Text))
            {
                MessageBox.Show("Vui lòng nhập max học sinh");
            }
            else if (string.IsNullOrEmpty(txtSoLuongHocSinh.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng học sinh");
            }
            else if (!txtMaxHocSinh.Text.All(char.IsDigit))
            {
                MessageBox.Show("Max hoc sinh chỉ nhập được số từ 0-9");
            }
            else if (!txtSoLuongHocSinh.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số lượng hoc sinh chỉ nhập được số từ 0-9");
            }
            else if (Convert.ToInt32(txtSoLuongHocSinh.Text) >= Convert.ToInt32(txtMaxHocSinh.Text))
            {
                MessageBox.Show("Số lượng hoc sinh không được lớn hơn max học sinh");
            }
            else
            {
                row = dgvClass.Rows.Count;
                Class clsDTO = new Class();
                int lastId = clsBll.getlastclassid();
                int newId = lastId + 1;
                clsDTO.ID = newId;
                clsDTO.Name = txtTenLop.Text;
                clsDTO.maxStudent = Convert.ToInt32(txtMaxHocSinh.Text);
                clsDTO.realStudent = Convert.ToInt32(txtSoLuongHocSinh.Text);
                string result = clsBll.insertClass(clsDTO);
                if (result == "Tên đã tồn tại")
                {
                    MessageBox.Show(result);
                    return; // Dừng thực thi của hàm btnAdd_Click
                }
                MessageBox.Show(result, "Bạn đã thêm thành công");
                loadDataClass();
                ResetClass();
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadDataClass();
        }

        private void dgvClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvClass.Rows[e.RowIndex];
            txtMaLop.Text = row.Cells[0].Value.ToString();
            txtTenLop.Text = row.Cells[1].Value.ToString();
            txtMaxHocSinh.Text = row.Cells[2].Value.ToString();
            txtSoLuongHocSinh.Text = row.Cells[3].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvClass.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvClass.SelectedRows[0].Index;
                Class clsDTO = dscls[selectedIndex];
                if (string.IsNullOrEmpty(txtTenLop.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên lớp");
                }
                else if (string.IsNullOrEmpty(txtMaxHocSinh.Text))
                {
                    MessageBox.Show("Vui lòng nhập max học sinh");
                }
                else if (string.IsNullOrEmpty(txtSoLuongHocSinh.Text))
                {
                    MessageBox.Show("Vui lòng nhập số lượng học sinh");
                }
                else if (!txtMaxHocSinh.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Max hoc sinh chỉ nhập được số từ 0-9");
                }
                else if (!txtSoLuongHocSinh.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Số lượng hoc sinh chỉ nhập được số từ 0-9");
                }
                else if (Convert.ToInt32(txtSoLuongHocSinh.Text) >= Convert.ToInt32(txtMaxHocSinh.Text))
                {
                    MessageBox.Show("Số lượng hoc sinh không được lớn hơn max học sinh");
                }
                else
                {
                    clsDTO.Name = txtTenLop.Text;
                    clsDTO.maxStudent = Convert.ToInt32(txtMaxHocSinh.Text);
                    clsDTO.realStudent = Convert.ToInt32(txtSoLuongHocSinh.Text);
                    clsBll.updateClass(clsDTO);
                    MessageBox.Show("Bạn đã sửa thành công");
                    loadDataClass();
                    ResetClass();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khối để sửa đổi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvClass.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvClass.SelectedRows[0].Index;
                Class classDTO = dscls[selectedIndex];
                classDTO.Name = txtTenLop.Text;
                classDTO.maxStudent = Convert.ToInt32(txtMaxHocSinh.Text);
                classDTO.realStudent = Convert.ToInt32(txtSoLuongHocSinh.Text);
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khối này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    clsBll.deleteClass(classDTO.ID);
                    MessageBox.Show("Bạn đã xóa thành công");
                    loadDataClass(); // Cập nhật dữ liệu trên DataGridView
                    ResetClass();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khối để xóa");
            }
        }
    }
}
