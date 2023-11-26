using DocumentFormat.OpenXml.Spreadsheet;
using ManagerStudent.BLL;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
using OfficeOpenXml.Drawing.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            Shown += (sender, e) => dgvClass.ClearSelection();
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;

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
            txtTenKhoi.Enabled = true;
            btnAdd.Enabled = !edit;
            btnEdit.Enabled = edit;
            btnDelete.Enabled = edit;
        }
        public void SetControlClass(bool edit)
        {
            txtMaLop.Enabled = false;
            txtTenLop.Enabled = true;
            txtMaxHocSinh.Enabled = true;
            txtSoLuongHocSinh.Enabled = true;
            btnThem.Enabled = !edit;
            btnSua.Enabled = edit;
            btnXoa.Enabled = edit;


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
            Regex regex = new Regex(@"^(K|k)hối lớp (1[0-2]|[1-9])$");
            string tenKhoi = txtTenKhoi.Text.Trim();
            if (!regex.IsMatch(tenKhoi))
            {
                MessageBox.Show("Tên khối không đúng định dạng. Vui lòng nhập lại theo định dạng 'Khối lớp x' (với x là số từ 1 đến 12).");
                return;
            }
            if (string.IsNullOrEmpty(tenKhoi))
                {
                    MessageBox.Show("Vui lòng nhập tên khối");
                }
           
            else
                {
                   /* row = dgvGrade.Rows.Count;*/
                    Grade gradeDTO = new Grade();
                    int lastID = gradeBll.GetLastGradeId();
                    int newID = lastID + 1;
                    gradeDTO.ID = newID;
                txtMaKhoi.Text = gradeDTO.ID.ToString();
                    gradeDTO.Name = tenKhoi;
                    string result = gradeBll.insertGrade(gradeDTO);
                    if (result == "Tên đã tồn tại")
                    {
                        MessageBox.Show(result);
                        return; // Dừng thực thi của hàm btnAdd_Click
                    }
                    MessageBox.Show(result, "Bạn đã thêm thành công");
                    loadDataGrade();
                    ResetGrade();
                  dgvGrade.ClearSelection();
                }
            }

        

        private void dgvGrade_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvGrade.Rows[e.RowIndex];
            txtMaKhoi.Text = row.Cells[0].Value.ToString();
            txtTenKhoi.Text = row.Cells[1].Value.ToString();          

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvGrade.SelectedRows.Count > 0)
                {
                    int selectedIndex = dgvGrade.SelectedRows[0].Index;
                    Grade gradeDTO = grade[selectedIndex];

                Regex regex = new Regex(@"^(K|k)hối lớp (1[0-2]|[1-9])$");
                string tenKhoi = txtTenKhoi.Text.Trim();
                if (!regex.IsMatch(tenKhoi))
                {
                    MessageBox.Show("Tên khối không đúng định dạng. Vui lòng nhập lại theo định dạng 'Khối lớp x' (với x là số từ 1 đến 12).");
                    return;
                }

                if (string.IsNullOrEmpty(tenKhoi))
                    {
                        MessageBox.Show("Vui lòng nhập tên khối");
                }

                else if (gradeBll.checkUpdateGrade(tenKhoi, Convert.ToInt32(txtMaKhoi.Text)))
                     {
                    MessageBox.Show("Tên khối đã tồn tại. Vui lòng nhập lại.");

                     }

                else
                {
                        gradeDTO.Name = tenKhoi;
                        MessageBox.Show(gradeBll.updateGrade(gradeDTO), "Bạn đã sửa thành công");
                        loadDataGrade(); // Cập nhật dữ liệu trên DataGridView
                        ResetGrade();
                    dgvGrade.ClearSelection();
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
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khối này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    gradeBll.deleteGrade(gradeDTO.ID);
                    MessageBox.Show("Bạn đã xóa thành công");
                    loadDataGrade(); // Cập nhật dữ liệu trên DataGridView
                    ResetGrade();
                    dgvGrade.ClearSelection();
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

      

        private void btnThem_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^((1[0-2]|[1-9])A[1-9])$");

            string tenLop = txtTenLop.Text.Trim();
            if (!regex.IsMatch(tenLop))
            {
                MessageBox.Show("Tên lớp không đúng định dạng. Vui lòng nhập lại theo định dạng 'xAy' (với x và y là số từ 1 đến 12).");
                return;
            }


            if (string.IsNullOrEmpty(tenLop))
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
            else if (Convert.ToInt32(txtSoLuongHocSinh.Text) > Convert.ToInt32(txtMaxHocSinh.Text))
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
                clsDTO.Name = tenLop;
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
                dgvClass.ClearSelection();
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
                Regex regex = new Regex(@"^((1[0-2]|[1-9])A[1-9])$");

                string tenLop = txtTenLop.Text.Trim();
                if (!regex.IsMatch(tenLop))
                {
                    MessageBox.Show("Tên lớp không đúng định dạng. Vui lòng nhập lại theo định dạng 'xAy' (với x và y là số từ 1 đến 12).");
                    return;
                }

                if (string.IsNullOrEmpty(tenLop))
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
                else if (clsBll.checkUpdateClass(txtTenLop.Text, Convert.ToInt32(txtMaLop.Text)))
                {
                    MessageBox.Show("Tên khối đã tồn tại. Vui lòng nhập lại.");

                }
                else if (Convert.ToInt32(txtSoLuongHocSinh.Text) > Convert.ToInt32(txtMaxHocSinh.Text))
                {
                    MessageBox.Show("Số lượng hoc sinh không được lớn hơn max học sinh");
                }
                else
                {
                    clsDTO.Name = tenLop;
                    clsDTO.maxStudent = Convert.ToInt32(txtMaxHocSinh.Text);
                    clsDTO.realStudent = Convert.ToInt32(txtSoLuongHocSinh.Text);
                    clsBll.updateClass(clsDTO);
                    MessageBox.Show("Bạn đã sửa thành công");
                    loadDataClass();
                    ResetClass();
                    dgvClass.ClearSelection();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp để sửa đổi");
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
                    dgvClass.ClearSelection();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp để xóa");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;
            switch (index)
            {
                case 0:
                    loadDataClass();
                    break;
                case 1:
                    loadDataGrade();
                    break;
            }
            //Xóa lựa chọn trong dataGridView khi chuyển tab
            if (tabControl1.SelectedTab == tabPage1)
            {
                dgvClass.ClearSelection();
                ResetClass();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                dgvGrade.ClearSelection();
                ResetGrade();
            }

        }

        private void dgvGrade_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGrade.SelectedRows.Count > 0)
            {
                // SetControl thành true nếu có hàng được chọn
                SetControl(true);
            }
            else
            {
                // SetControl thành false nếu không có hàng được chọn
                SetControl(false);
            }

        }

        private void dgvClass_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClass.SelectedRows.Count > 0)
            {
                // SetControl thành true nếu có hàng được chọn
                SetControlClass(true);
            }
            else
            {
                // SetControl thành false nếu không có hàng được chọn
                SetControlClass(false);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            try
            {
                // Gọi hàm searchGrades để tìm kiếm dữ liệu
                List<Class> searchResults = clsBll.searchClass(searchTerm);

                if (searchResults.Count > 0)
                {
                    // Cập nhật nguồn dữ liệu của DataGridView
                    dgvClass.DataSource = searchResults;
                    dgvClass.ClearSelection();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Đường dẫn của tệp đã chọn: " + openFileDialog.FileName);
                    dgvGrade.DataSource = ConnectExcel.ImportExcelToDataTable(openFileDialog.FileName);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Đường dẫn của tệp đã chọn: " + openFileDialog.FileName);
                    dgvClass.DataSource = ConnectExcel.ImportExcelToDataTable(openFileDialog.FileName);
                }
            }
        }
    }
}
