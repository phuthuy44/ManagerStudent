using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using Google.Apis.Util;
using ManagerStudent.BLL;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Kernel;
using Table = iText.Layout.Element.Table;
using Cell = iText.Layout.Element.Cell;
using Paragraph = iText.Layout.Element.Paragraph;
using Document = iText.Layout.Document;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using Text = iText.Layout.Element.Text;
using System.Windows;

namespace ManagerStudent.GUI
{
    public partial class HocSinhForm : Form
    {
        private StudentBLL studentBLL;
        private ParentBLL parentBLL;
        private Parent parent;
        private Student student;
        //private BindingSource bindingSourceClassNew, bindingSourceClassOld;
        private DataTable originalDataTable; // Biến lưu trữ bản sao của dữ liệu ban đầu
        public HocSinhForm()
        {
            InitializeComponent();
            studentBLL = new StudentBLL();
            parentBLL = new ParentBLL();
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

            // Calculate the age based on the date of birth
            DateTime today = DateTime.Today;
            Console.WriteLine(today);
            int age = today.Year - ngaySinh.Year;
            if (ngaySinh > today.AddYears(-age))
            {
                age--;
            }
            if (string.IsNullOrEmpty(hoTen))


            {
                //MessageBox.Show("Trường Họ tên không thể bỏ trống");
                MessageBox.Show("Trường Họ tên không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(gioiTinh))
            {
                //MessageBox.Show("Trường Giới tính không thể bỏ trống");
                MessageBox.Show("Trường Giới tính không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (age < 15 || age > 20)
            {
                MessageBox.Show("Tuổi phải nằm trong khoảng từ 16 đến 20", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!string.IsNullOrEmpty(soDienThoai) && soDienThoai.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải chứa đúng 10 số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                student = new Student(hoTen, gioiTinh, diaChi, ngaySinh, email, soDienThoai, image);
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
            // TODO: This line of code loads data into the 'studentManagerDataSet3.Student' table. You can move, or remove it, as needed.
            // studentTableAdapter1.Fill(this.studentManagerDataSet3.Student);
            // TODO: This line of code loads data into the 'studentManagerDataSet2.Student' table. You can move, or remove it, as needed.
            //this.studentTableAdapter.Fill(this.studentManagerDataSet2.Student);
            // TODO: This line of code loads data into the 'managerStudentDataSet1._Student_' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'studentManagerDataSet.Student' table. You can move, or remove it, as needed.
            //this.studentTableAdapter.Fill(this.studentManagerDataSet.Student);
            GetListStudent();
            cbSearch.SelectedIndex = 0;
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
            /*List<StudentClassSemesterAcademicYear> distinctAcademicYears = academicYears
                .GroupBy(a => a.academicyearID)
                .Select(g => g.First())
                .ToList();*/
            foreach (StudentClassSemesterAcademicYear a in academicYears)
            {
                string AcademicName = studentBLL.getAcademicName(a.academicyearID);
                txtYearOld.Items.Add(AcademicName);
                txtYearNew.Items.Add(AcademicName);
                txtNamHocInQuanHe.Items.Add(AcademicName);
                txtTraCuuNH.Items.Add(AcademicName);
                txtYearOld.SelectedIndex = 0;
                txtYearNew.SelectedIndex = 0;
                txtNamHocInQuanHe.SelectedIndex = 0;
                txtTraCuuNH.SelectedIndex = 0;
            }

            List<Semester> semester = studentBLL.getSemester();
            foreach (Semester s in semester)
            {
                string semesterName = s.Name;
                txtSemesterOld.Items.Add(semesterName);
                txtSemesterNew.Items.Add(semesterName);
                cbSeInQuanHe.Items.Add(semesterName);
                txtTraCuuHK.Items.Add(semesterName);
                cbSeInQuanHe.SelectedIndex = 0;
                txtSemesterOld.SelectedIndex = 0;
                txtSemesterNew.SelectedIndex = 0;
                txtTraCuuHK.SelectedIndex = 0;
            }
            txtGioiTinhCha.Text = "Nam";
            txtGioiTinhMe.Text = "Nữ";
            updateTableWhenSelectedClass_Old();
            updateTableWhenSelectedClass_New();
            cbClassInQuanhe_SelectedIndexChanged(sender, new EventArgs());
            
        }
        public void getListSTudentTraCuu(int yearID, int gradeID, int classID, int semesterID)
        {
            dataTraCuu.DataSource = studentBLL.getListStudentInClassTraCuu(yearID, gradeID, classID, semesterID);
        }
        //Xu ly fill dataTable lên dataGridView
        public void GetListStudent()
        {
            originalDataTable = studentBLL.GetListStudent().Copy();
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
            /*List<StudentClassSemesterAcademicYear> distinctGrades = grades
                .GroupBy(a => a.gradeID)
                .Select(g => g.First())
                .ToList();*/
            /* txtYearOld.Items.Clear();
             txtYearNew.Items.Clear();*/
            foreach (StudentClassSemesterAcademicYear a in grades)
            {
                string grade = studentBLL.getNameGrade(a.gradeID);
                txtKhoiNew.Items.Add(grade);
                txtKhoiNew.SelectedIndex = 0;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataTableStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataTableStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
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
                if (row.Cells[2].Value != null)
                {
                    // string ngaySinh = row.Cells[2].Value.ToString();
                    //txtDate.Text = ngaySinh;
                    string gioiTinh = row.Cells[2].Value.ToString();
                    cboxGioiTinh.Text = gioiTinh;

                }
                if (row.Cells[3].Value != null)
                {
                    string ngaySinh = row.Cells[3].Value.ToString();
                    txtDate.Text = ngaySinh;
                }
                if (row.Cells[4].Value != null)
                {
                    string diaChi = row.Cells[4].Value.ToString();
                    txtDiaChi.Text = diaChi;
                }
                if (row.Cells[6].Value != null)
                {
                    string email = row.Cells[6].Value.ToString();
                    txtEmail.Text = email;
                }
                if (row.Cells[5].Value != null)
                {
                    string soDienThoai = row.Cells[5].Value.ToString();
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
                            System.Drawing.Image image = System.Drawing.Image.FromFile(fullImagePath);
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
            if (dataTableStudent != null && dataTableStudent.SelectedRows.Count > 0)
            {
                string idStudent = dataTableStudent.SelectedRows[0].Cells[0].Value.ToString();
                DialogResult confirm = MessageBox.Show("Bạn muốn xóa học sinh này ?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        //UpdateStudent
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
                DateTime today = DateTime.Today;
                Console.WriteLine(today);
                int age = today.Year - ngaySinh.Year;
                if (ngaySinh > today.AddYears(-age))
                {
                    age--;
                }
                if (!string.IsNullOrEmpty(soDienThoai) && soDienThoai.Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải chứa đúng 10 số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (age < 15 || age > 20)
                {
                    MessageBox.Show("Tuổi phải nằm trong khoảng từ 16 đến 20", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
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
            TaoLopForm taoLopForm = new TaoLopForm(this);
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
            /*List<StudentClassSemesterAcademicYear> distinctGrades = grades
                .GroupBy(a => a.gradeID)
                .Select(g => g.First())
                .ToList();*/
            /* txtYearOld.Items.Clear();
             txtYearNew.Items.Clear();*/
            foreach (StudentClassSemesterAcademicYear a in grades)
            {
                string grade = studentBLL.getNameGrade(a.gradeID);
                txtKhoiOld.Items.Add(grade);
                txtKhoiOld.SelectedIndex = 0;
            }
        }

        private void txtKhoiOld_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = txtKhoiOld.SelectedItem.ToString();
            int gradeID = studentBLL.getGradeID(selected);
            List<StudentClassSemesterAcademicYear> cls = studentBLL.getClass(gradeID);
            txtClassOld.Items.Clear();
            //List< StudentClassSemesterAcademicYear> distinctClass = cls.GroupBy(a => a.classID).Select(g => g.First()).ToList();
            foreach (StudentClassSemesterAcademicYear c in cls)
            {
                string className = studentBLL.getClassName(c.classID);
                txtClassOld.Items.Add(className);
                txtClassOld.SelectedIndex = 0;
            }
        }

        private void txtKhoiNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*string selected = txtKhoiNew.SelectedItem.ToString();
            string gradeID = studentBLL.getMaKhoi(selected);
            List<Class> cls = studentBLL.getClassInGrade(gradeID);
            txtClassNew.Items.Clear();
            foreach(Class c in cls)
            {
                txtClassNew.Items.Add(c.Name);
            }*/
            string selected = txtKhoiNew.SelectedItem.ToString();
            int gradeID = studentBLL.getGradeID(selected);
            List<StudentClassSemesterAcademicYear> cls = studentBLL.getClass(gradeID);
            txtClassNew.Items.Clear();
            //List<StudentClassSemesterAcademicYear> distinctClass = cls.GroupBy(a => a.classID).Select(g => g.First()).ToList();
            foreach (StudentClassSemesterAcademicYear c in cls)
            {
                string className = studentBLL.getClassName(c.classID);
                txtClassNew.Items.Add(className);
                txtClassNew.SelectedIndex = 0;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtClassOld_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateTableWhenSelectedClass_Old();
        }

        public void updateTableWhenSelectedClass_Old()
        {
            string selectedYear = txtYearOld.SelectedItem.ToString();
            string gradeSelected = txtKhoiOld.SelectedItem.ToString();
            string selected = txtClassOld.SelectedItem.ToString();
            string selectedSe = txtSemesterOld.Text;
            int yearID = studentBLL.getIdAca(selectedYear);
            int gradeID = studentBLL.getGradeID(gradeSelected);
            int classID = studentBLL.getClassID(selected);
            int semesID = studentBLL.getIDSemester(selectedSe);
            //lblOld.Text = getQuantity(classID,semesID).ToString();
            lblOld.Text = getQuantity(yearID, gradeID, classID, semesID).ToString();
            Console.WriteLine(classID);
            DataTable dataTable = studentBLL.getListStudentInClass(yearID, gradeID, classID, semesID);
            // DataView dataView = new DataView(dataTable);
            dataTableClassOld.Columns.Clear();
            dataTableClassOld.Columns.Add("ID", "Mã học sinh");
            dataTableClassOld.Columns.Add("Name", "Tên học sinh");
            dataTableClassOld.Columns.Add("Gender", "Giới tính");
            // Map the columns to the corresponding columns in the DataTable

            dataTableClassOld.Columns["ID"].DataPropertyName = "ID";
            dataTableClassOld.Columns["Name"].DataPropertyName = "name";
            dataTableClassOld.Columns["Gender"].DataPropertyName = "gender";
            dataTableClassOld.DataSource = dataTable;
            dataTableClassOld.DataBindings.Clear();
        }

        private void txtClassNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateTableWhenSelectedClass_New();

        }
        public int getQuantity(int idYear, int idGrade, int idClass, int idSe)
        {
            int getQuantityStudent = studentBLL.getQuantity(idYear, idGrade, idClass, idSe);
            return getQuantityStudent;
        }
        public void updateTableWhenSelectedClass_New()
        {
            string selectedYear = txtYearNew.SelectedItem.ToString();
            string gradeSelected = txtKhoiNew.SelectedItem.ToString();
            string selected = txtClassNew.SelectedItem.ToString();
            string selectedSe = txtSemesterNew.Text;
            int yearID = studentBLL.getIdAca(selectedYear);
            int gradeID = studentBLL.getGradeID(gradeSelected);
            int classID = studentBLL.getClassID(selected);
            int semesID = studentBLL.getIDSemester(selectedSe);

            //lblNew.Text = getQuantity(classID, semesID).ToString();
            lblNew.Text = getQuantity(yearID, gradeID, classID, semesID).ToString();

            Console.WriteLine(classID);
            DataTable dataTable = studentBLL.getListStudentInClass(yearID, gradeID, classID, semesID);
            // DataView dataView = new DataView(dataTable);
            dataTableClassNew.Columns.Clear();
            dataTableClassNew.Columns.Add("ID", "Mã học sinh");
            dataTableClassNew.Columns.Add("Name", "Tên học sinh");
            dataTableClassNew.Columns.Add("Gender", "Giới tính");

            // Map the columns to the corresponding columns in the DataTable
            dataTableClassNew.Columns["ID"].DataPropertyName = "ID";
            dataTableClassNew.Columns["Name"].DataPropertyName = "name";
            dataTableClassNew.Columns["Gender"].DataPropertyName = "gender";
            dataTableClassNew.DataSource = dataTable;
            // dataTableClassNew.DataBindings.Clear();*/
        }
        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = txtNamHocInQuanHe.SelectedItem.ToString();
            int getNamID = studentBLL.getIdAca(selected);
            List<StudentClassSemesterAcademicYear> grades = studentBLL.GetGrades(getNamID);
            cbGradeInQuanHe.Items.Clear();
            /*List<StudentClassSemesterAcademicYear> distinctGrades = grades
                .GroupBy(a => a.gradeID)
                .Select(g => g.First())
                .ToList();*/
            /* txtYearOld.Items.Clear();
             txtYearNew.Items.Clear();*/
            foreach (StudentClassSemesterAcademicYear a in grades)
            {
                string grade = studentBLL.getNameGrade(a.gradeID);
                cbGradeInQuanHe.Items.Add(grade);
                cbGradeInQuanHe.SelectedIndex = 0;
            }
        }

        private void cbGradeInQuanHe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbGradeInQuanHe.SelectedItem.ToString();
            int gradeID = studentBLL.getGradeID(selected);
            List<StudentClassSemesterAcademicYear> cls = studentBLL.getClass(gradeID);
            cbClassInQuanhe.Items.Clear();
            // List<StudentClassSemesterAcademicYear> distinClass = cls.GroupBy(a => a.classID).Select(g => g.First()).ToList();
            foreach (StudentClassSemesterAcademicYear a in cls)
            {
                string classes = studentBLL.getClassName(a.classID);
                cbClassInQuanhe.Items.Add(classes);
                cbClassInQuanhe.SelectedIndex = 0;
            }
        }

        private void cbClassInQuanhe_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idNH = studentBLL.getIdAca(txtNamHocInQuanHe.SelectedItem.ToString());
            int idGrade = studentBLL.getGradeID(cbGradeInQuanHe.SelectedItem.ToString());
            int idSe = studentBLL.getIDSemester(cbSeInQuanHe.Text);
            string selected = cbClassInQuanhe.SelectedItem.ToString();
            int classID = studentBLL.getClassID(selected);
            Console.WriteLine(classID);
            List<StudentClassSemesterAcademicYear> stu = studentBLL.getStudentIdFromPhanLop(idNH, idGrade, idSe, classID);
            cbStudentIDInQuanHe.Items.Clear();
            /*List<StudentClassSemesterAcademicYear> distinStudent = stu
                .GroupBy(a => a.studentID)
                .Select(g => g.First())
                .ToList();*/
            foreach (StudentClassSemesterAcademicYear s in stu)
            {
                cbStudentIDInQuanHe.Items.Add(s.studentID);
                //Console.WriteLine(s.studentID);
                cbStudentIDInQuanHe.SelectedIndex = 0;
            }


;
        }

        private void cbStudentIDInQuanHe_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = int.Parse(cbStudentIDInQuanHe.SelectedItem.ToString());
            List<Student> stu = studentBLL.getStudentInformFromID(selected);
            if (stu.Count > 0)
            {
                txtHoTenStudent.Text = "";
                txtNgaySinhStudent.Text = "";
                txtGioiTinhStudent.Text = "";
                txtSoDienThoaiStudent.Text = "";
                txtDiaChiStudent.Text = "";
                txtNgayTaoStudent.Text = "";
                pictureBox17.Image = null;
                //Cha
                pictureBox14_Click(sender, e);
                pictureBox15_Click(sender, e);

                Student student = stu[0];
                string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                //string folderPath = "D:\\ManagerStudent";
                string fullImagePath = appDirectory + student.Image;
                txtHoTenStudent.Text = student.Name;
                txtNgaySinhStudent.Text = student.Birthday.ToString();
                txtGioiTinhStudent.Text = student.Gender;
                txtSoDienThoaiStudent.Text = student.Phone;
                txtDiaChiStudent.Text = student.Address;
                txtNgayTaoStudent.Text = student.createDate.ToString();
                if (File.Exists(fullImagePath))
                {
                    try
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(fullImagePath);
                        pictureBox17.Image = image;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi!Không thể tải ảnh: " + ex.Message);
                    }
                }


            }
            getDataCha(selected);
            getDataMe(selected);

        }
        public void getDataCha(int id)
        {
            List<Parent> parents = parentBLL.getDataCha(id);

            if (parents.Count > 0)
            {
                string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Parent p = parents[0];
                txtHoTenCha.Text = p.Name;
                dateTimeCha.Text = p.Birthday.ToString();
                txtGioiTinhCha.Text = p.Gender;
                txtDiaChiCha.Text = p.Address;
                txtSDTCha.Text = p.Phone;
                txtImageCha.Text = p.Image;
                Console.WriteLine(txtImageCha.Text);
                string fullImagePath = appDirectory + "\\Image\\HocSinh-Cha\\" + txtImageCha.Text;
                //string fullImagePath =folderPath+fileName;
                Console.WriteLine(fullImagePath);
                if (File.Exists(fullImagePath))
                {
                    try
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(fullImagePath);
                        picCha.Image = image;
                        //File.Copy(fileName,fullImagePath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi!Không thể tải ảnh: " + ex.Message);
                    }
                }
                txtCreateCha.Text = p.createDate.ToString();


            }
        }
        public void getDataMe(int id)
        {
            List<Parent> parents = parentBLL.getDataMe(id);
            if (parents.Count > 0)
            {
                string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                Parent p = parents[0];
                txtHoTenMe.Text = p.Name;
                dateTimeMe.Text = p.Birthday.ToString();
                txtDiaChiMe.Text = p.Address;
                txtGioiTinhMe.Text = p.Gender;
                txtSDTMe.Text = p.Phone;
                txtImageMe.Text = p.Image;
                string fullImagePath = appDirectory + "\\Image\\HocSinh-Cha\\" + txtImageMe.Text;
                //string fullImagePath =folderPath+fileName;
                Console.WriteLine(fullImagePath);
                if (File.Exists(fullImagePath))
                {
                    try
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromFile(fullImagePath);
                        picMe.Image = image;
                        //File.Copy(fileName,fullImagePath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi!Không thể tải ảnh: " + ex.Message);
                    }
                }
                txtNgayTaoMe.Text = p.createDate.ToString();
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            txtHoTenCha.Text = "";
            txtSDTCha.Text = "";
            txtDiaChiCha.Text = "";
            dateTimeCha.Text = "";
            txtImageCha.Text = "";
            picCha.Image = null;
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            txtHoTenMe.Text = "";
            txtSDTMe.Text = "";
            dateTimeMe.Text = "";
            txtDiaChiMe.Text = "";
            txtImageMe.Text = "";
            picMe.Image = null;

        }
        //Upload Image - Cha In View Quan he
        private void button12_Click(object sender, EventArgs e)
        {
            string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = System.IO.Path.Combine(appDirectory, "Image", "HocSinh-Cha");
            openFileDialog2.InitialDirectory = folderPath;
            openFileDialog2.Title = "Chọn hình ảnh để tải lên";
            openFileDialog2.Filter = "Các định dạng(*.jpg;*.jpeg;*.gif;*.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            openFileDialog2.FilterIndex = 1;
            try
            {
                if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    /*if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        picStudent.Image = new Bitmap(openFileDialog1.FileName);
                        picStudent.SizeMode = PictureBoxSizeMode.StretchImage;
                    }*/
                    string path = System.IO.Path.GetFullPath(openFileDialog2.FileName);
                    txtImageCha.Text = path;
                    picCha.Image = new Bitmap(openFileDialog2.FileName);
                    picCha.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int selected = int.Parse(cbStudentIDInQuanHe.SelectedItem.ToString());
            string txttenCha = txtHoTenCha.Text;
            DateTime ngaySinh = dateTimeCha.Value;
            string gioiTinh = txtGioiTinhCha.Text;
            string soDT = txtSDTCha.Text;
            string diachi = txtDiaChiCha.Text;
            string image = System.IO.Path.GetFileName(txtImageCha.Text);
            DateTime today = DateTime.Today;
            Console.WriteLine(today);
            int age = today.Year - ngaySinh.Year;
            if (ngaySinh > today.AddYears(-age))
            {
                age--;
            }
            if (!string.IsNullOrEmpty(soDT) && soDT.Length != 10)
            {
                MessageBox.Show("Lỗi! Số điện thoại phải là 10 chữ số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (age < 20)
            {
                MessageBox.Show("Tuổi phải từ 20 tuổi trở lên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {
                parent = new Parent(selected, txttenCha, gioiTinh, diachi, ngaySinh, soDT, image);
                bool result = parentBLL.insertParent(parent);
                if (result)
                {
                    MessageBox.Show("Thêm thông tin thành công!");
                }
                else
                {
                    MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        //UploadImage-Me
        private void button13_Click(object sender, EventArgs e)
        {
            string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = System.IO.Path.Combine(appDirectory, "Image", "HocSinh-Cha");
            openFileDialog2.InitialDirectory = folderPath;
            openFileDialog2.Title = "Chọn hình ảnh để tải lên";
            openFileDialog2.Filter = "Các định dạng(*.jpg;*.jpeg;*.gif;*.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            openFileDialog2.FilterIndex = 1;
            try
            {
                if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    /*if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        picStudent.Image = new Bitmap(openFileDialog1.FileName);
                        picStudent.SizeMode = PictureBoxSizeMode.StretchImage;
                    }*/
                    string path = System.IO.Path.GetFullPath(openFileDialog2.FileName);
                    txtImageMe.Text = path;
                    picMe.Image = new Bitmap(openFileDialog2.FileName);
                    picMe.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Insert-Me
        private void button15_Click(object sender, EventArgs e)
        {
            int selected = int.Parse(cbStudentIDInQuanHe.SelectedItem.ToString());
            string txtten = txtHoTenMe.Text;
            DateTime ngaySinh = dateTimeMe.Value;
            string gioiTinh = txtGioiTinhMe.Text;
            string soDT = txtSDTMe.Text;
            string diachi = txtDiaChiMe.Text;
            string image = System.IO.Path.GetFileName(txtImageMe.Text);
            DateTime today = DateTime.Today;
            Console.WriteLine(today);
            int age = today.Year - ngaySinh.Year;
            if (ngaySinh > today.AddYears(-age))
            {
                age--;
            }
            if (!string.IsNullOrEmpty(soDT) && soDT.Length != 10)
            {
                MessageBox.Show("Lỗi! Số điện thoại phải là 10 chữ số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (age < 20)
            {
                MessageBox.Show("Tuổi phải từ 20 tuổi trở lên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                parent = new Parent(selected, txtten, gioiTinh, diachi, ngaySinh, soDT, image);
                bool result = parentBLL.insertParent(parent);
                if (result)
                {
                    MessageBox.Show("Thêm thông tin thành công!");
                }
                else
                {
                    MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void dataTableClassOld_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            AddStudentForm form = new AddStudentForm(this);
            form.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cbSearch.SelectedItem.ToString();
            string searchText = txtSearch.Text;

            if (string.IsNullOrEmpty(searchText))
            {
                //dataTableStudent.DataSource = originalDataTable; // Hiển thị dữ liệu ban đầu khi TextBox tìm kiếm rỗng
                GetListStudent();
            }
            else
            {
                DataTable search = studentBLL.searchStudent(selected, searchText);
                dataTableStudent.DataSource = search;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);
            /* string selected = cbSearch.SelectedItem.ToString();
             string searchText = txtSearch.Text;

             if (string.IsNullOrEmpty(searchText))
             {
                 dataTableStudent.DataSource = originalDataTable; // Hiển thị dữ liệu ban đầu khi TextBox tìm kiếm rỗng
             }
             else
             {
                 DataTable search = studentBLL.searchStudent(selected, searchText);
                 dataTableStudent.DataSource = search;
             }*/
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            int idNH = studentBLL.getIdAca(txtYearNew.SelectedItem.ToString());
            int idKhoi = studentBLL.getGradeID(txtKhoiNew.SelectedItem.ToString());
            int idClass = studentBLL.getClassID(txtClassNew.SelectedItem.ToString());
            /*int idNH_old = studentBLL.getIdAca(txtYearOld.SelectedItem.ToString());
            int idKhoi_Old = studentBLL.getGradeID(txtKhoiOld.SelectedIndex.ToString());
            int idClass_old = studentBLL.getClassID(txtClassOld.SelectedItem.ToString());
            int idSes_old = studentBLL.getIDSemester(txtSemesterOld.Text);*/
            int idSes = studentBLL.getIDSemester(txtSemesterNew.Text);
            List<int> selectedStudentIDs = new List<int>();//tao mot danh sach cac id cua hoc sinh duoc chon de chuyen lop

            //lay du lieu nguon cua hai doi tuong va gan lan luot cho oldDataTable và new DataTable 
            DataTable oldDataTable = dataTableClassOld.DataSource as DataTable;
            DataTable newDataTable = dataTableClassNew.DataSource as DataTable;

            // If either DataTable is null, return
            if (oldDataTable == null || newDataTable == null)
            {
                return;
            }
            string selectedCurrentClass = txtClassOld.SelectedItem.ToString();
            string selectedSemester = txtSemesterOld.SelectedItem.ToString();
            string selectedtNH = txtYearOld.SelectedItem.ToString();
            string selectedGrade = txtKhoiOld.SelectedItem.ToString();
            string selectedNHNew = txtYearNew.SelectedItem.ToString();
            string selectedGradeNew = txtKhoiNew.SelectedItem.ToString();
            string selectedSemesterNew = txtSemesterNew.SelectedItem.ToString();
            string selectedNewClass = txtClassNew.SelectedItem.ToString();
            int quantity = int.Parse(lblNew.Text);
            int quantityold = int.Parse(lblOld.Text);

            if (selectedCurrentClass == selectedNewClass && selectedSemester == selectedSemesterNew && selectedtNH == selectedNHNew && selectedGrade == selectedGradeNew)
            {
                MessageBox.Show("Vui lòng chọn một lớp khác với lớp hiện tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (quantity < 1)
            {
                MessageBox.Show("Lớp đã đầy !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            else
            {
                //Lap qua cac hang da chon trong dataTableClassOld
                foreach (DataGridViewRow item in dataTableClassOld.SelectedRows)
                {
                    if (quantity < 1)
                    {
                        MessageBox.Show("Lớp đã đầy !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        int studentID = Convert.ToInt32(item.Cells["ID"].Value);//Lay ID cua hoc sinh tu cot "ID" cua hang do va them vao danh sach 
                        selectedStudentIDs.Add(studentID);

                        //Them mot hang moi trong newDataTable 
                        DataRow newRow = newDataTable.NewRow();
                        newRow["ID"] = item.Cells["ID"].Value;
                        newRow["Name"] = item.Cells["Name"].Value;
                        newRow["Gender"] = item.Cells["Gender"].Value;
                        newDataTable.Rows.Add(newRow);

                        StudentClassSemesterAcademicYear p = new StudentClassSemesterAcademicYear(studentID, idClass, idSes, idNH, idKhoi);
                        studentBLL.updateStudentInPhanLop(p);
                        oldDataTable.Rows.RemoveAt(item.Index);
                        quantity--;
                        quantityold++;
                        lblNew.Text = quantity.ToString();
                        lblOld.Text = quantityold.ToString();
                        //lblOld.Text = quantity.ToString();
                    }
                }


                /*//Xoa cac hang da chon tu dataTable
                foreach (DataGridViewRow item in dataTableClassOld.SelectedRows)
                {
                    oldDataTable.Rows.RemoveAt(item.Index);
                }*/
                MessageBox.Show("Chuyển lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                /* lblNew.Text = getQuantity(idClass, idSes).ToString();*/
                //lblOld.Text = getQuantity(idClass_old, idSes_old).ToString();



                //Xoa cac hang da chon tu dataTable
                /*foreach (DataGridViewRow item in dataTableClassOld.SelectedRows)
                {
                    oldDataTable.Rows.RemoveAt(item.Index);
                }
                MessageBox.Show("Chuyển lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblNew.Text = getQuantity(idClass, idSes).ToString();
                lblOld.Text = getQuantity(idClass_old, idSes_old).ToString();*/
            }
        }

        private void exportExcel_Click(object sender, EventArgs e)
        {
            string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = System.IO.Path.Combine(appDirectory, "excel", "DanhSachHocSinh");

            // Kiểm tra xem thư mục tồn tại chưa, nếu chưa thì tạo mới
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Tạo đường dẫn cho file Excel mặc định
            string defaultExcelFilePath = System.IO.Path.Combine(folderPath, "");

            // Hiển thị hộp thoại SaveFileDialog để cho phép người dùng chọn đường dẫn và đặt tên cho file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "";
            saveFileDialog.InitialDirectory = folderPath;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Gọi hàm xuất Excel với đường dẫn đã chọn
                ExportToExcel(dataTableStudent, saveFileDialog.FileName);
                MessageBox.Show("Dữ liệu đã được xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Nếu người dùng không chọn đường dẫn, sử dụng đường dẫn mặc định
                ExportToExcel(dataTableStudent, defaultExcelFilePath);
            }
        }
        private void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                // Write headers to Excel
                for (int i = 1; i <= dataGridView.Columns.Count; i++)
                {
                    worksheet.Cells[1, i].Value = dataGridView.Columns[i - 1].HeaderText;
                }

                // Write data from DataGridView to Excel
                for (int i = 1; i <= dataGridView.Columns.Count; i++)
                {
                    // Check if the column contains DateTime values
                    if (dataGridView.Columns[i - 1].ValueType == typeof(DateTime))
                    {
                        for (int j = 1; j <= dataGridView.Rows.Count; j++)
                        {
                            if (dataGridView.Rows[j - 1].Cells[i - 1].Value is DateTime dateTimeValue)
                            {
                                string formattedDateTime = dateTimeValue.ToString("MM/dd/yyyy hh:mm:ss tt");
                                worksheet.Cells[j + 1, i].Value = formattedDateTime;
                            }
                            else
                            {
                            }
                        }
                    }
                    else
                    {
                        for (int j = 1; j <= dataGridView.Rows.Count; j++)
                        {
                            worksheet.Cells[j + 1, i].Value = dataGridView.Rows[j - 1].Cells[i - 1].Value;
                        }
                    }
                }

                // Save the Excel file to the specified path
                FileInfo excelFile = new FileInfo(filePath);

                excelPackage.SaveAs(excelFile);
            }
        }
        //NhapExcel
        private void button4_Click(object sender, EventArgs e)
        {
            string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = System.IO.Path.Combine(appDirectory, "excel", "DanhSachHocSinh");
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Thiết lập các thuộc tính cho OpenFileDialog
            openFileDialog.Title = "Chọn tệp Excel để import";
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.InitialDirectory = folderPath;

            // Hiển thị hộp thoại để chọn tệp
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn của tệp đã chọn
                string filePath = openFileDialog.FileName;

                // Gọi hàm ImportFromExcel với đường dẫn tệp đã chọn
                ImportFromExcel(filePath);
            }
        }
        private void ImportFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial; // Set the license context
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                List<Student> stu = new List<Student>();
                DataTable StudentClone = originalDataTable.Clone();
                StudentClone.Columns.Add("Mã học sinh");
                StudentClone.Columns.Add("Tên học sinh");
                StudentClone.Columns.Add("Giới tính");
                StudentClone.Columns.Add("Ngày sinh");
                StudentClone.Columns.Add("Địa chỉ");
                StudentClone.Columns.Add("Email");
                StudentClone.Columns.Add("Số điện thoại");
                StudentClone.Columns.Add("Hình ảnh");
                StudentClone.Columns.Add("Ngày tạo");
                StudentClone.Columns.Add("Ngày chỉnh sửa");
              var student = originalDataTable.AsEnumerable().ToList();
                foreach(DataRow row in student)
                {
                    StudentClone.ImportRow(row);
                }
                // Bắt đầu từ dòng thứ 2 để bỏ qua dòng đầu tiên
                int startRow = 2;
                if (package.Workbook.Worksheets.Count > 0)
                {
                    ExcelWorksheets worksheets = package.Workbook.Worksheets;
                    ExcelWorksheet worksheet = worksheets[0]; // Lấy sheet đầu tiên
                                                              // Lấy dữ liệu từ Excel và thêm vào DataTable StudentClone
                    for (int row = startRow; row <= worksheet.Dimension.End.Row; row++)
                    {

                        string maHS = worksheet.Cells[row, 1].Text;
                        string tenHS = worksheet.Cells[row, 2].Text;
                        string gioitinh = worksheet.Cells[row, 3].Text;
                        DateTime ngaySinh = DateTime.Parse(worksheet.Cells[row, 4].Text);
                        string diachi = worksheet.Cells[row, 5].Text;
                        string email = worksheet.Cells[row, 7].Text;
                        string soDT = worksheet.Cells[row, 6].Text;
                        string image = worksheet.Cells[row, 8].Text;
                        /**DateTime ngayTao = DateTime.Parse(worksheet.Cells[row, 9].Text);
                        DateTime ngaycapNhat = DateTime.Parse(worksheet.Cells[row, 10].Text);*/
                        DateTime? ngayTao = null;
                        if (DateTime.TryParse(worksheet.Cells[row, 9].Text, out DateTime parsedCreateDate))
                        {
                            ngayTao = parsedCreateDate;
                        }
                        DateTime? ngaycapNhat = null;
                        if (DateTime.TryParse(worksheet.Cells[row, 10].Text, out DateTime parsedUpdateDate))
                        {
                            ngaycapNhat = parsedUpdateDate;
                        }
                        DateTime ngayTaoValue = ngayTao.HasValue ? ngayTao.Value : DateTime.MinValue;
                        DateTime ngaycapNhatValue = ngaycapNhat.HasValue ? ngaycapNhat.Value : DateTime.MinValue;
                        Student st = new Student(int.Parse(maHS),tenHS, gioitinh, diachi, ngaySinh, email, soDT, image, ngayTaoValue, ngaycapNhatValue);
                        bool existsInDatabase = studentBLL.checkStudentCount(st.ID);
                        HashSet<int> existingIDs = new HashSet<int>(StudentClone.AsEnumerable().Select(rows => rows.Field<int>("ID")));

                        if (!existsInDatabase)
                        {
                            DataRow newRow = StudentClone.NewRow();
                            newRow["ID"] = st.ID;
                            newRow["Name"] = st.Name;
                            newRow["Gender"] = st.Gender;
                            newRow["birthday"] = st.Birthday;
                            newRow["Address"] = st.Address;
                            newRow["email"] = st.Email;
                            newRow["numberPhone"] = st.Phone;
                            newRow["image"] = st.Image;
                            newRow["createDate"] = ngayTao.HasValue ? (DateTime)ngayTao : DateTime.MinValue; // Ép kiểu DateTime?
                            newRow["updateDate"] = ngaycapNhat.HasValue ? (DateTime)ngaycapNhat : DateTime.MinValue; // Ép kiểu DateTime?
                            Student st2 = new Student(tenHS, gioitinh, diachi, ngaySinh, email, soDT, image, ngayTaoValue, ngaycapNhatValue);
                            StudentClone.Rows.Add(newRow);
                           studentBLL.insertStudent(st2);
                        }
                    }
                    

                    MessageBox.Show("Import dữ liệu thành công!",
                             "Thông báo",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                    //GetListStudent();
                    dataTableStudent.DataSource = StudentClone;
                    GetListStudent();
                }
            }
        }

        private void txtTraCuuNH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = txtTraCuuNH.SelectedItem.ToString();
            int selectedID = studentBLL.getIdAca(selected);           
            List<StudentClassSemesterAcademicYear> g = studentBLL.GetGrades(selectedID);
            txtTraCuuKhoi.Items.Clear();
            // List<StudentClassSemesterAcademicYear> distinClass = cls.GroupBy(a => a.classID).Select(g => g.First()).ToList();
            foreach (StudentClassSemesterAcademicYear a in g)
            {
                string classes = studentBLL.getNameGrade(a.gradeID);
                txtTraCuuKhoi.Items.Add(classes);
                txtTraCuuKhoi.SelectedIndex = 0;
            }
        }

        private void txtTraCuuKhoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = txtTraCuuKhoi.SelectedItem.ToString();
            int gradeID = studentBLL.getGradeID(selected);
            List<StudentClassSemesterAcademicYear> cls = studentBLL.getClass(gradeID);
            txtTraCuuLop.Items.Clear();
            //List<StudentClassSemesterAcademicYear> distinctClass = cls.GroupBy(a => a.classID).Select(g => g.First()).ToList();
            foreach (StudentClassSemesterAcademicYear c in cls)
            {
                string className = studentBLL.getClassName(c.classID);
                txtTraCuuLop.Items.Add(className);
                txtTraCuuLop.SelectedIndex = 0;
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string selectedYear = txtTraCuuNH.SelectedItem.ToString();
            string gradeSelected = txtTraCuuKhoi.SelectedItem.ToString();
            string selected = txtTraCuuLop.SelectedItem.ToString();
            string selectedSe = txtTraCuuHK.Text;
            int yearID = studentBLL.getIdAca(selectedYear);
            int gradeID = studentBLL.getGradeID(gradeSelected);
            int classID = studentBLL.getClassID(selected);
            int semesID = studentBLL.getIDSemester(selectedSe);
            getListSTudentTraCuu(yearID, gradeID, classID, semesID);
        }
        //XuatDs-Lop
        private void button9_Click(object sender, EventArgs e)
        {
            string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = System.IO.Path.Combine(appDirectory, "excel", "DanhSachHS_Lop");

            // Kiểm tra xem thư mục tồn tại chưa, nếu chưa thì tạo mới
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Tạo đường dẫn cho file Excel mặc định
            string defaultExcelFilePath = System.IO.Path.Combine(folderPath, "");

            // Hiển thị hộp thoại SaveFileDialog để cho phép người dùng chọn đường dẫn và đặt tên cho file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.FileName = "";
            saveFileDialog.InitialDirectory = folderPath;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Gọi hàm xuất Excel với đường dẫn đã chọn
                ExportToExcel(dataTraCuu, saveFileDialog.FileName);
                MessageBox.Show("Dữ liệu đã được xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Nếu người dùng không chọn đường dẫn, sử dụng đường dẫn mặc định
                ExportToExcel(dataTraCuu, defaultExcelFilePath);
            }
        }
        //Xuat pdf
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataTraCuu.Rows.Count > 0)
            {
                string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string folderPath = System.IO.Path.Combine(appDirectory, "excel", "DanhSachHS_Lop");
                // Tạo đường dẫn cho file Excel mặc định
                string defaultExcelFilePath = System.IO.Path.Combine(folderPath, "");
                // Kiểm tra xem thư mục tồn tại chưa, nếu chưa thì tạo mới
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = "";
                saveFileDialog.InitialDirectory = folderPath;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportDataToPdf(saveFileDialog.FileName);
                    MessageBox.Show("Dữ liệu Export thành công!!!", "Info");
                }
                else
                {
                    ExportDataToPdf(defaultExcelFilePath);

                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất ra tệp PDF", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void ExportDataToPdf(string filePath)
        {
            using (PdfWriter pdfWriter = new PdfWriter(filePath))
            {
                PdfDocument pdf = new PdfDocument(pdfWriter);
                Document document = new Document(pdf);
                // Sử dụng font mặc định của iText
                // Set default font for the document
                // Replace the line where you create the default font with the following code

                PdfFont defaultFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN,PdfEncodings.UTF8);

                document.SetFont(defaultFont);
                Table table = new Table(dataTraCuu.Columns.Count);
                table.SetWidth(UnitValue.CreatePercentValue(100));
                table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                // Thêm header
                foreach (DataGridViewColumn column in dataTraCuu.Columns)
                {
                    Cell cell = new Cell();
                    Paragraph headerParagraph = new Paragraph(column.HeaderText.ToString()).SetFont(defaultFont).SetFontSize(12).SetBold();
                    cell.Add(headerParagraph);
                    cell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.GREEN);
                    table.AddCell(cell);
                }

                // Thêm dữ liệu
                foreach (DataGridViewRow row in dataTraCuu.Rows)
                {
                    bool hasData = false; // Kiểm tra xem có dữ liệu trong hàng hay không

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                        {
                            hasData = true;
                            break;
                        }
                    }

                    if (hasData)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            Cell pdfCell = new Cell();
                            Paragraph cellParagraph = new Paragraph(cell.Value.ToString() ?? "").SetFont(defaultFont).SetFontSize(10);
                            pdfCell.Add(cellParagraph);
                            table.AddCell(pdfCell);
                        }
                    }
                }

                document.Add(table);
                document.Close();
            }
        }


    }
}
