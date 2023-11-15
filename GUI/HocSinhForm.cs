using DocumentFormat.OpenXml.Drawing;
using ManagerStudent.BLL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
        //InsertStudent
        private void button1_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text;
            DateTime ngaySinh = txtDate.Value;
            //kiểm tra giá trị của SelectedItem và tránh lỗi khi không có mục nào được chọn
            string gioiTinh = cboxGioiTinh.SelectedItem?.ToString();
            string diaChi = txtDiaChi.Text;
            string email = txtEmail.Text;
            string soDienThoai = txtSoDienThoai.Text;
            //string image = System.IO.Path.GetFileName(openFileDialog1.FileName);
            string image = System.IO.Path.GetFileName(txtImage.Text);
            //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            // string destinationPath = path + "//Image//HocSinh//" + image;
            //System.IO.File.Copy(openFileDialog1.FileName, image);

            if (string.IsNullOrEmpty(hoTen))
            {
                //MessageBox.Show("Trường Họ tên không thể bỏ trống");
                MessageBox.Show("Trường Họ tên không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(gioiTinh))
            {
                //MessageBox.Show("Trường Giới tính không thể bỏ trống");
                MessageBox.Show("Trường Giới tính không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!string.IsNullOrEmpty(soDienThoai) && soDienThoai.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải chứa đúng 10 số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                student = new Student(hoTen,gioiTinh,diaChi,ngaySinh,email,soDienThoai,image);
                bool result = studentBLL.insertStudent(student);
                if (result)
                {
                    MessageBox.Show("Thêm học sinh " + hoTen + " thành công!");
                    Console.WriteLine(student);
                    GetListStudent();
                    pictureBox1_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //GetListStudent();
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
            // TODO: This line of code loads data into the 'studentManagerDataSet2.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.studentManagerDataSet2.Student);
            // TODO: This line of code loads data into the 'managerStudentDataSet1._Student_' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'studentManagerDataSet.Student' table. You can move, or remove it, as needed.
            //this.studentTableAdapter.Fill(this.studentManagerDataSet.Student);
            GetListStudent();
            /*PhanLop*/
            /*List<AcademicYear> academicYears = studentBLL.GetAcademicYears();
            foreach(AcademicYear a in academicYears)
            {
                txtYearOld.Items.Add(a.Name);
                txtYearNew.Items.Add(a.Name);
            }
            List<Grade> grades = studentBLL.GetGrades();
            foreach(Grade g in grades)
            {
                txtKhoiOld.Items.Add(g.Name);
                txtKhoiNew.Items.Add(g.Name);
            }*/
            List<StudentClassSemesterAcademicYear> academicYears = studentBLL.GetAcademicYears();
            List<StudentClassSemesterAcademicYear> distinctAcademicYears = academicYears
                .GroupBy(a => a.academicyearID)
                .Select(g => g.First())
                .ToList();
            foreach (StudentClassSemesterAcademicYear a in distinctAcademicYears)
            {
                string AcademicName = studentBLL.getAcademicName(a.academicyearID);
                txtYearOld.Items.Add(AcademicName);
                txtYearNew.Items.Add(AcademicName);
            }
        }
        //Xu ly fill dataTable lên dataGridView
        public void GetListStudent()
        {
            dataTableStudent.DataSource = studentBLL.GetListStudent();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = txtYearNew.SelectedItem.ToString();
            int getIDAc = studentBLL.getIdAca(selected);
            Console.WriteLine(getIDAc);
            List<StudentClassSemesterAcademicYear> grades = studentBLL.GetGrades(getIDAc);
            Console.WriteLine(grades);
            txtKhoiNew.Items.Clear();
            List<StudentClassSemesterAcademicYear> distinctGrades = grades
                .GroupBy(a => a.gradeID)
                .Select(g => g.First())
                .ToList();
            /* txtYearOld.Items.Clear();
             txtYearNew.Items.Clear();*/
            foreach (StudentClassSemesterAcademicYear a in distinctGrades)
            {
                string grade = studentBLL.getNameGrade(a.gradeID);
                txtKhoiNew.Items.Add(grade);
            }
        
    }

        private void button6_Click(object sender, EventArgs e)
        {

        }

      /*  private void HocSinhForm_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentManagerDataSet1.Student' table. You can move, or remove it, as needed.
           // this.studentTableAdapter.Fill(this.studentManagerDataSet1.Student);
           // GetListStudent();
        }*/

        private void cboxGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtMaStudent.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSoDienThoai.Text = string.Empty;
            picStudent.Image = null;
            txtImage.Text = null;
        }
        //Upload Image
        private void button8_Click(object sender, EventArgs e)
        {
            // openFileDialog1.InitialDirectory = "D://ManagerStudent//Image//Student";
            string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = System.IO.Path.Combine(appDirectory, "image", "HocSinh");
            openFileDialog1.InitialDirectory = folderPath;
            openFileDialog1.Title = "Chọn hình ảnh để tải lên";
            openFileDialog1.Filter = "Các định dạng(*.jpg;*.jpeg;*.gif;*.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    /*if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        picStudent.Image = new Bitmap(openFileDialog1.FileName);
                        picStudent.SizeMode = PictureBoxSizeMode.StretchImage;
                    }*/
                    string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                    txtImage.Text = path;
                    picStudent.Image = new Bitmap(openFileDialog1.FileName);
                    picStudent.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataTableStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataTableStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataTableStudent.Rows[e.RowIndex];
                pictureBox1_Click(sender, e);
                //Check
                if (row.Cells[0].Value != null)
                {
                    string maStudent = row.Cells[0].Value.ToString();
                    txtMaStudent.Text = maStudent;

                }
                if (row.Cells[1].Value != null)
                {
                    string tenStudent = row.Cells[1].Value.ToString();
                    txtHoTen.Text = tenStudent;
                }
                if (row.Cells[2].Value!= null)
                {
                    // string ngaySinh = row.Cells[2].Value.ToString();
                    //txtDate.Text = ngaySinh;
                    string gioiTinh = row.Cells[2].Value.ToString();
                    cboxGioiTinh.Text = gioiTinh;

                }
                if (row.Cells[3].Value != null)
                {
                    string ngaySinh = row.Cells[3].Value.ToString();
                    txtDate.Text=ngaySinh;
                }
                if (row.Cells[4].Value != null)
                {
                    string diaChi = row.Cells[4].Value.ToString();
                    txtDiaChi.Text = diaChi;
                }
                if (row.Cells[5].Value != null)
                {
                    string email = row.Cells[5].Value.ToString();
                    txtEmail.Text = email;
                }
                if (row.Cells[6].Value != null)
                {
                    string soDienThoai = row.Cells[6].Value.ToString();
                    txtSoDienThoai.Text = soDienThoai;
                }
                if (row.Cells[7].Value != null && !string.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                {
                    string fileName = row.Cells[7].Value.ToString();
                    // string rootDirectory = Application.StartupPath;
                    //string fullImagePath = Path.Combine(rootDirectory, imagePath);
                    // string folderPath = "D:\\ManagerStudent";
                    picStudent.Image = null;
                    string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    //string folderPath = "D:\\ManagerStudent";
                    string fullImagePath = appDirectory + fileName;
                    txtImage.Text = fileName;
                    //string fullImagePath =folderPath+fileName;
                    Console.WriteLine(fullImagePath);
                    if (File.Exists(fullImagePath))
                    {
                        try
                        {
                            Image image = Image.FromFile(fullImagePath);
                            picStudent.Image = image;
                            //File.Copy(fileName,fullImagePath, true);
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
               /* else
                {
                    picStudent.Image = null;
                }*/
            }
        }
        //DeleteStudent
        private void button3_Click(object sender, EventArgs e)
        {
            if(dataTableStudent != null && dataTableStudent.SelectedRows.Count > 0)
            {
                string idStudent = dataTableStudent.SelectedRows[0].Cells[0].Value.ToString();
                DialogResult confirm = MessageBox.Show("Bạn muốn xóa học sinh này ?","Xác nhận xóa",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    bool isLoiKhoaNgoai;
                    bool deleteStudent = studentBLL.deleteStudent(idStudent, out isLoiKhoaNgoai);
                    if (deleteStudent)
                    {
                        if (isLoiKhoaNgoai)
                        {
                            MessageBox.Show("Không thể xóa học sinh này vì có dữ liệu liên quan đến học sinh này trong hệ thống." +
                                "Vui lòng xóa các dữ liệu liên quan trước khi tiếp tục", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dataTableStudent.Rows.Remove(dataTableStudent.SelectedRows[0]);
                            MessageBox.Show("Xóa học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // GetListStudent();
                            pictureBox1_Click(sender, e);//reset
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hệ thống lỗi! Vui lòng thử lại sau!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                /*else
                {
                    MessageBox.Show("Vui lòng chọn một hàng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }*/

            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataTableStudent != null && dataTableStudent.SelectedRows.Count > 0)
            {
                int id = int.Parse(txtMaStudent.Text);
                string hoTen = txtHoTen.Text;
                DateTime ngaySinh = txtDate.Value;
                string gioiTinh = cboxGioiTinh.SelectedItem.ToString();
                string diaChi = txtDiaChi.Text;
                string email = txtEmail.Text;
                string soDienThoai = txtSoDienThoai.Text;
                /* string folderPath = "D:\\ManagerStudent";
                 // string imageFilePath = string.Empty;
                 string fullImagePath = folderPath + picStudent.Text;
                 // string image = System.IO.Path.GetFileName(openFileDialog1.FileName);
                 string image = string.IsNullOrEmpty(fullImagePath) ? null : System.IO.Path.GetFileName(openFileDialog1.FileName);*/
                string txtImg = txtImage.Text;
                if (!string.IsNullOrEmpty(soDienThoai) && soDienThoai.Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải chứa đúng 10 số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else {
                    string image = System.IO.Path.GetFileName(txtImg);
                    student = new Student(id, hoTen, gioiTinh, diaChi, ngaySinh, email, soDienThoai, image);
                    bool result = studentBLL.updateStudent(student);

                    if (result)
                    {

                        GetListStudent();
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pictureBox1_Click(sender, e);
                        // Di chuyển đến hàng đã sửa trong bảng
                        foreach (DataGridViewRow row in dataTableStudent.Rows)
                        {
                            if (int.Parse(row.Cells[0].Value.ToString()) == id)
                            {
                                dataTableStudent.CurrentCell = row.Cells[0];
                                dataTableStudent.FirstDisplayedScrollingRowIndex = row.Index;
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hệ thống lỗi!Hãy thử lại vào lần sau!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để chỉnh sửa thông tin", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            TaoLopForm taoLopForm = new TaoLopForm();
            taoLopForm.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = txtYearOld.SelectedItem.ToString();
            int getIDAc = studentBLL.getIdAca(selected);
            Console.WriteLine(getIDAc);
            List<StudentClassSemesterAcademicYear> grades = studentBLL.GetGrades(getIDAc);
            Console.WriteLine(grades);
            txtKhoiOld.Items.Clear();
            List<StudentClassSemesterAcademicYear> distinctGrades = grades
                .GroupBy(a => a.gradeID)
                .Select(g => g.First())
                .ToList();
            /* txtYearOld.Items.Clear();
             txtYearNew.Items.Clear();*/
            foreach (StudentClassSemesterAcademicYear a in distinctGrades)
            {
                string grade = studentBLL.getNameGrade(a.gradeID);
                txtKhoiOld.Items.Add(grade);
            }
        }

        private void txtKhoiOld_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = txtKhoiOld.SelectedItem.ToString();
            string gradeID = studentBLL.getMaKhoi(selected);
            List<Class> cls = studentBLL.getClassInGrade(gradeID);
            txtClassOld.Items.Clear();
            foreach (Class c in cls)
            {
                txtClassOld.Items.Add(c.Name);
            }
        }

        private void txtKhoiNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = txtKhoiNew.SelectedItem.ToString();
            string gradeID = studentBLL.getMaKhoi(selected);
            List<Class> cls = studentBLL.getClassInGrade(gradeID);
            txtClassNew.Items.Clear();
            foreach(Class c in cls)
            {
                txtClassNew.Items.Add(c.Name);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
