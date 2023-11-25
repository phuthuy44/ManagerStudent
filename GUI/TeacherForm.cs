using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using ManagerStudent.BLL;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ManagerStudent.GUI
{
    public partial class TeacherForm : Form
    {
        private TeacherBLL teacherBLL;
        private PointBLL pointBLL;   
        private Teacher teacher;
        private DataTable tc;
        public TeacherForm()
        {
            InitializeComponent();
            teacherBLL = new TeacherBLL();
            pointBLL= new PointBLL();
            tc = new DataTable();
        }
        public void FillTableTeacher()
        {
            tc = teacherBLL.GetDataTeacher();
            TableTeacher.DataSource = tc;
            TableTeacher.Columns[1].Width = 150;
            TableTeacher.Columns[2].Width = 40;
            TableTeacher.Columns[0].Width = 30;
            TableTeacher.Columns[8].Width = 150;
            TableTeacher.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TableTeacher.Columns["Tên Giáo viên"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TableTeacher.Columns["Chuyên môn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TableTeacher.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ReloadForm();
        }
        public void FillTableTechnical()
        {
            TableAssignment.DataSource = teacherBLL.GetAssignmentTeacher();
        }
        private int Age(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthDate.Year;

            // Kiểm tra xem ngày sinh đã đến hay chưa trong năm nay
            if (birthDate.Date > currentDate.AddYears(-age))
            {
                age--;
            }
            return age;
  
        }
        private void ReloadForm()
        {
            txtMaGV.Text = string.Empty; 
            txtHoTenGV.Text = string.Empty;
            txtEmailGV.Text = string.Empty;
            txtSDTGV.Text = string.Empty;
            cbGender.SelectedItem = null;
            picGV.Image = null;
            txtDiaChiGV.Text = string.Empty;
            cbTechnical.SelectedItem = null;
        }

        private void TeacherForm_Load(object sender, System.EventArgs e)
        {
            
            FillTableTeacher();
            FillTableTechnical();
            DataTable subject = pointBLL.GetSujectData();
            DataTable ay = teacherBLL.GetAcademicYear();
            DataTable sm = teacherBLL.GetSemester();
            DataTable cls = teacherBLL.GetClass();
            DataTable ps = teacherBLL.GetPosition();
            foreach (DataRow row in subject.Rows)
            {
                string subjectName = row["subjectName"].ToString();

                cbTechnical.Items.Add(subjectName);
            }
            foreach (DataRow row in ay.Rows)
            {
                string acayear = row["academicyearName"].ToString();

                cbNH.Items.Add(acayear);
            }
            foreach (DataRow row in sm.Rows)
            {
                string smName = row["semesterName"].ToString();

                cbHK.Items.Add(smName);
            }
            foreach (DataRow row in cls.Rows)
            {
                string clsName = row["className"].ToString();
        
                cbPCL.Items.Add(clsName);
            }
            foreach (DataRow row in ps.Rows)
            {
                string positionName = row["positionName"].ToString();

                cbCV.Items.Add(positionName);
            }
            foreach (DataRow row in tc.Rows)
            {
                string teacherName = row["Mã GV"].ToString() + " - " + row["Tên giáo viên"].ToString();
                cbGV.Items.Add(teacherName);
            }
        }


        private void TableTeacher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = TableTeacher.Rows[e.RowIndex];
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
                    txtSDTGV.Text = "0" + row.Cells[5].Value.ToString();
                }
                if (row.Cells[6].Value != null)
                {
                    txtDiaChiGV.Text = row.Cells[6].Value.ToString();
                }
                if (row.Cells[7].Value != null && !string.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                {
                    string fileName = row.Cells[7].Value.ToString();
                    picGV.Image = null;

                    string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    string fullImagePath = appDirectory + fileName;
                    Console.WriteLine(fullImagePath);
                    if (File.Exists(fullImagePath))
                    {
                        try
                        {
                            Image image = Image.FromFile(fullImagePath);
                            picGV.Image = image;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi!Không thể tải ảnh: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lỗi!Không tìm thấy ảnh.");
                    }
                }
                else
                {
                    picGV.Image = null;
                }

                if (row.Cells[8].Value != null)
                {
                    cbTechnical.Text= row.Cells[8].Value.ToString();
                }
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            int idsub;
            int idtecher;
            if (string.IsNullOrEmpty(txtMaGV.Text))
            {
                string name = txtHoTenGV.Text;
                string email = txtEmailGV.Text;
                string sdt = txtSDTGV.Text;
                string gender = cbGender.SelectedItem?.ToString();
                string image = System.IO.Path.GetFileName(FileDialog.FileName);
                string address = txtDiaChiGV.Text;
                string technical = cbTechnical.SelectedItem?.ToString();
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
                else if (Age(BirthdayGV.Value)<= 22 || Age(BirthdayGV.Value) >= 55)
                {
                    MessageBox.Show("Tuổi phải lớn hơn 22 và nhỏ hơn 55", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    teacher = new Teacher(name, gender, address, ngaySinh, email, sdt, image);
                    bool result = teacherBLL.InsertTeacher(teacher);
                    if (result)
                    {
                        idtecher = teacherBLL.GetIdTeacherLast();
                        idsub = teacherBLL.GetIdSubject(technical);
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
            else
            {
                DataGridViewRow selectedRow = TableTeacher.SelectedRows[0];
                if (cbTechnical.SelectedItem?.ToString() == selectedRow.Cells[8].Value.ToString())
                {
                    MessageBox.Show("Lỗi! Không thêm được giáo viên cùng chuyên môn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    idsub = teacherBLL.GetIdSubject(cbTechnical.SelectedItem?.ToString());
                    idtecher = int.Parse(txtMaGV.Text);
                    bool rs = teacherBLL.InsertSubTecher(idtecher, idsub);
                    if (rs)
                    {
                        MessageBox.Show("Thêm chuyên môn thành công");

                        FillTableTeacher();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = System.IO.Path.Combine(appDirectory, "image", "GiaoVien");
            FileDialog.InitialDirectory = folderPath;
            FileDialog.Title = "Chọn hình ảnh để tải lên";
            FileDialog.Filter = "Các định dạng(*.jpg;*.jpeg;*.gif;*.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            FileDialog.FilterIndex = 1;
            try
            {
                if (FileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                
                    string path = System.IO.Path.GetFullPath(FileDialog.FileName);
                    picGV.Image = new Bitmap(FileDialog.FileName);
                    picGV.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaGV.Text))
            {
                string name = txtHoTenGV.Text;
                string email = txtEmailGV.Text;
                string sdt = txtSDTGV.Text;
                string gender = cbGender.SelectedItem?.ToString();
                string image = System.IO.Path.GetFileName(FileDialog.FileName);
                string address = txtDiaChiGV.Text;
                //string technical = cbTechnical.SelectedItem?.ToString();
                DateTime ngaySinh = BirthdayGV.Value.Date;
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Trường họ tên không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrEmpty(gender))
                {
                    MessageBox.Show("Trường giới tính không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                /*else if (string.IsNullOrEmpty(technical))
                {
                    MessageBox.Show("Trường chuyên môn không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/
                else if (!string.IsNullOrEmpty(sdt) && sdt.Length != 10)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Age(BirthdayGV.Value) <= 22 || Age(BirthdayGV.Value) >= 55)
                {
                    MessageBox.Show("Tuổi phải lớn hơn 22 và nhỏ hơn 55", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //DataGridViewRow selectedRow = TableTeacher.SelectedRows[0];
                    int id = int.Parse(txtMaGV.Text);
                    teacher = new Teacher(id,name, gender, address, ngaySinh, email, sdt, image);
                    bool result = teacherBLL.UpdateTeacher(teacher);
                    if (result)
                    {
                            MessageBox.Show("Cập nhật thành công!");

                            FillTableTeacher();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lỗi! Hãy chọn một dòng của bản để sửa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }      
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult flag = MessageBox.Show("Bạn có chắc muốn xóa giáo viên "+ txtHoTenGV.Text + " ?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (flag == DialogResult.Yes)
            {

                if (!string.IsNullOrEmpty(txtMaGV.Text))
                {
                    int id = int.Parse(txtMaGV.Text);
                    bool rs1 = teacherBLL.DeleteAssignment(id);
                    bool rs2 = teacherBLL.DeleteSubOfTeacher(id);
                    if (rs1 && rs2)
                    {
                        bool rs3 = teacherBLL.DeleteTeacher(id);
                        if (rs3)
                            MessageBox.Show("Xóa thành công!");
                        FillTableTeacher();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi! Hãy chọn một dòng của bảng để xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
   
        }
        private void button10_Click(object sender, EventArgs e)
        {
            ReloadForm();
        }
  


        private void cbGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCM.Items.Clear();
            if (!string.IsNullOrEmpty(cbGV.SelectedItem?.ToString()))
            {
                int id = int.Parse(cbGV.SelectedItem?.ToString().Split('-')[0]);
                DataTable sbOfTea = teacherBLL.GetSubjectTeacher(id);
                foreach (DataRow row in sbOfTea.Rows)
                {
                    string subjectName = row["subjectName"].ToString();

                    cbCM.Items.Add(subjectName);
                }
            }
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cbHK.SelectedItem = null;
            cbNH.SelectedItem = null;
            cbPCL.SelectedItem = null;
            cbGV.SelectedItem = null;
            cbCV.SelectedItem = null;
        }
    }
}
