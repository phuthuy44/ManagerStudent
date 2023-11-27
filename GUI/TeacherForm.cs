using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Table = iText.Layout.Element.Table;
using Cell = iText.Layout.Element.Cell;
using Paragraph = iText.Layout.Element.Paragraph;
using Document = iText.Layout.Document;
using iText.Layout.Properties;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using ManagerStudent.BLL;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using iText.Kernel.XMP.Impl;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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
            if (tc.Rows.Count!=0)
            {
                TableTeacher.DataSource = tc;
                TableTeacher.Columns[1].Width = 150;
                TableTeacher.Columns[2].Width = 50;
                TableTeacher.Columns[0].Width = 30;
                TableTeacher.Columns[8].Width = 150;
                TableTeacher.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                TableTeacher.Columns["Tên Giáo viên"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                TableTeacher.Columns["Chuyên môn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                TableTeacher.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
            }
            else
            {
                TableTeacher.DataSource = tc;
            }
            ReloadForm();
        }
        public void FillTableTechnical()
        {
            TableAssignment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            TableAssignment.DataSource = teacherBLL.GetAssignment();
        }
        public void FillTableAssignmentTeacher(int id, string ayName, string semesName)
        {
            TableAssignment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TableAssignment.DataSource = teacherBLL.GetAssignmentTeacher(id, ayName, semesName);
        }
        public void FillTableAssignmentClass(string clsname, string ayName, string semesName)
        {
            TableAssignment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TableAssignment.DataSource = teacherBLL.GetAssignmentClass(clsname, ayName, semesName);
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
            cbGender.SelectedItem = null;
            cbTechnical.SelectedItem = null;
            cbGender.Text = string.Empty;
            cbTechnical.Text = string.Empty;
            txtMaGV.Text = string.Empty; 
            txtHoTenGV.Text = string.Empty;
            txtEmailGV.Text = string.Empty;
            txtSDTGV.Text = string.Empty;
            picGV.Image = null;
            txtDiaChiGV.Text = string.Empty;
            
        }

        private void TeacherForm_Load(object sender, System.EventArgs e)
        {
            
            FillTableTeacher();
            FillTableTechnical();
            DataTable subject = pointBLL.GetSujectData();
            DataTable ay = teacherBLL.GetAcademicYear();
            DataTable sm = teacherBLL.GetSemester();
            DataTable cls = teacherBLL.GetClass();
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
            foreach (DataRow row in tc.Rows)
            {
                string teacherName = row["Mã GV"].ToString() + " - " + row["Tên giáo viên"].ToString();
                cbGV.Items.Add(teacherName);
            }
            foreach (DataRow row in cls.Rows)
            {
                string clsName = row["className"].ToString();

                cbPCL.Items.Add(clsName);
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
                string image = "\\Image\\GiaoVien\\" + System.IO.Path.GetFileName(FileDialog.FileName);
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
                    if (path.Contains(folderPath))
                    {
                        picGV.Image = new Bitmap(FileDialog.FileName);
                        picGV.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else{
                        string destinationFilePath = System.IO.Path.Combine(folderPath, System.IO.Path.GetFileName(path));

                        
                        if (!System.IO.File.Exists(destinationFilePath))
                        {
                            System.IO.File.Copy(path, destinationFilePath);
                        }
                        picGV.Image = new Bitmap(FileDialog.FileName);
                        picGV.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                        
                    
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
                string image = "\\Image\\GiaoVien\\" + System.IO.Path.GetFileName(FileDialog.FileName);
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
            DialogResult flag = MessageBox.Show("Bạn có chắc muốn xóa giáo viên " + txtHoTenGV.Text + " ?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (flag == DialogResult.Yes)
            {

                if (!string.IsNullOrEmpty(txtMaGV.Text))
                {
                    int id = int.Parse(txtMaGV.Text);
                    bool rs1 = teacherBLL.DeleteTechnical(id);
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
            string search =txtSearch.Text;
            TableTeacher.DataSource= teacherBLL.SearchAllTeacher(search);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            ReloadForm();
        }
  


        private void cbGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCM.Items.Clear();
            cbCV.Items.Clear();
            cbCM.SelectedItem = null;
            cbCV.SelectedItem = null;
            string ayName = cbNH.SelectedItem?.ToString();
            string semesName = cbHK.SelectedItem?.ToString();
            string teacherName = cbGV.SelectedItem?.ToString();
            string clsname = cbPCL.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(teacherName))
            {
                int id = int.Parse(teacherName.Split('-')[0]);
                DataTable sbOfTea = teacherBLL.GetSubjectTeacher(id);
                DataTable ps = teacherBLL.GetPosition();
                foreach (DataRow row in sbOfTea.Rows)
                {
                    string subjectName = row["subjectName"].ToString();

                    cbCM.Items.Add(subjectName);
                }
                foreach (DataRow row in ps.Rows)
                {
                    string positionName = row["positionName"].ToString();

                    cbCV.Items.Add(positionName);
                }
                if (string.IsNullOrEmpty(clsname)) {
                    FillTableAssignmentTeacher(id, ayName, semesName);
                }
                
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cbHK.SelectedItem = null;
            cbNH.SelectedItem = null;
            cbPCL.SelectedItem = null;
            cbCM.SelectedItem = null;
            cbGV.SelectedItem = null;
            cbCV.SelectedItem = null;
            FillTableTechnical();
        }

        private void cbNH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ayName = cbNH.SelectedItem?.ToString();
            string semesName = cbHK.SelectedItem?.ToString();
            string clsname = cbPCL.SelectedItem?.ToString();
            string teacherName = cbGV.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(teacherName) && !string.IsNullOrEmpty(semesName) && string.IsNullOrEmpty(clsname))
            {
                
                int id = int.Parse(teacherName.Split('-')[0]);
                FillTableAssignmentTeacher(id, ayName, semesName);
            }
            else if(!string.IsNullOrEmpty(clsname) && !string.IsNullOrEmpty(semesName))
            {
                FillTableAssignmentClass(clsname, ayName, semesName);
            }

        }

        private void cbHK_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ayName = cbNH.SelectedItem?.ToString();
            string semesName = cbHK.SelectedItem?.ToString();
            string clsname = cbPCL.SelectedItem?.ToString();
            string teacherName = cbGV.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(teacherName) && string.IsNullOrEmpty(clsname) && !string.IsNullOrEmpty(ayName))
            {
               
                int id = int.Parse(teacherName.Split('-')[0]);
                FillTableAssignmentTeacher(id, ayName, semesName);
            }
            else if (!string.IsNullOrEmpty(clsname)&& !string.IsNullOrEmpty(ayName))
            {
                FillTableAssignmentClass(clsname, ayName, semesName);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string ayName = cbNH.SelectedItem?.ToString();
            string semesName = cbHK.SelectedItem?.ToString();
            string teacherName = cbGV.SelectedItem?.ToString();
            string sbName = cbCM.SelectedItem?.ToString();
            string clsName = cbPCL.SelectedItem?.ToString();
            string posName = cbCV.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(ayName))
            {
                MessageBox.Show("Trường năm học không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(semesName))
            {
                MessageBox.Show("Trường học kỳ không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(teacherName))
            {
                MessageBox.Show("Trường giáo viên không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(sbName))
            {
                MessageBox.Show("Trường chuyên môn không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(clsName))
            {
                MessageBox.Show("Trường phân công lớp không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (string.IsNullOrEmpty(posName))
            {
                MessageBox.Show("Trường chức vụ không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                int idAy = teacherBLL.GetIdAY(ayName);
                int idSe = teacherBLL.GetIdSemester(semesName);
                int idSb = teacherBLL.GetIdSubject(sbName);
                int idTea = int.Parse(teacherName.Split('-')[0]);
                int idPos = teacherBLL.GetIdPosition(posName);
                int idCls = teacherBLL.GetIdClass(clsName);
                if(posName=="Giáo viên chủ nhiệm")
                {
                    if(!teacherBLL.CheckPosition(idCls, idAy, idSe))
                    {
                        if(!teacherBLL.CheckClass(idCls, idSb, idAy, idSe))
                        {
                            if (teacherBLL.InsertAssignment(idCls, idSb, idAy, idSe, idTea, idPos))
                            {
                                MessageBox.Show("Lưu thành công!");
                                FillTableAssignmentClass(clsName, ayName, semesName);
                            }
                            else
                            {
                                MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lớp " + clsName + " đã có giáo viên dạy môn "+ sbName, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else {
                        MessageBox.Show("Lớp" + clsName +" đã có giáo viên chủ nhiệm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (!teacherBLL.CheckClass(idCls, idSb, idAy, idSe))
                    {
                        if (teacherBLL.InsertAssignment(idCls, idSb, idAy, idSe, idTea, idPos))
                        {
                            MessageBox.Show("Lưu thành công!");
                            FillTableAssignmentClass(clsName, ayName, semesName);
                        }
                        else
                        {
                            MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lớp " + clsName + " đã có giáo viên dạy môn " + sbName, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
        }

        private void cbPCL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ayname = cbNH.SelectedItem?.ToString();
            string semname = cbHK.SelectedItem?.ToString();
            string clsname = cbPCL.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(clsname) && !string.IsNullOrEmpty(ayname) && !string.IsNullOrEmpty(semname))
            {
                FillTableAssignmentClass(clsname, ayname, semname);
            }
           
        }
        private void btnFillAssignment_Click(object sender, EventArgs e)
        {
            string ayName = cbNH.SelectedItem?.ToString();
            string semesName = cbHK.SelectedItem?.ToString();
            string clsname = cbPCL.SelectedItem?.ToString();
            string teacherName = cbGV.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(teacherName) && string.IsNullOrEmpty(clsname) 
                &&!string.IsNullOrEmpty(ayName) && !string.IsNullOrEmpty(semesName))
            {

                int id = int.Parse(teacherName.Split('-')[0]);
                FillTableAssignmentTeacher(id, ayName, semesName);
            }
            else if (!string.IsNullOrEmpty(clsname)
                 && !string.IsNullOrEmpty(ayName) && !string.IsNullOrEmpty(semesName))
            {
                FillTableAssignmentClass(clsname, ayName, semesName);
            }
        }

        private void btnDA_Click(object sender, EventArgs e)
        {
            string ayName = cbNH.SelectedItem?.ToString();
            string semesName = cbHK.SelectedItem?.ToString();
            string teacherName = cbGV.SelectedItem?.ToString();
            string sbName = cbCM.SelectedItem?.ToString();
            string clsName = cbPCL.SelectedItem?.ToString();
            string posName = cbCV.SelectedItem?.ToString();
            DialogResult flag = MessageBox.Show("Bạn có chắc muốn xóa giáo viên " + teacherName.Split('-')[1] +" dạy môn " + sbName + " của lớp "+ clsName + " ?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (flag == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(ayName))
                {
                    MessageBox.Show("Trường năm học không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrEmpty(semesName))
                {
                    MessageBox.Show("Trường học kỳ không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrEmpty(teacherName))
                {
                    MessageBox.Show("Trường giáo viên không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrEmpty(sbName))
                {
                    MessageBox.Show("Trường chuyên môn không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrEmpty(clsName))
                {
                    MessageBox.Show("Trường phân công lớp không thể bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    int idAy = teacherBLL.GetIdAY(ayName);
                    int idSe = teacherBLL.GetIdSemester(semesName);
                    int idSb = teacherBLL.GetIdSubject(sbName);
                    int idTea = int.Parse(teacherName.Split('-')[0]);
                    /*int idPos = teacherBLL.GetIdPosition(posName);*/
                    int idCls = teacherBLL.GetIdClass(clsName);
                    if (teacherBLL.DeleteAssignment(idCls, idSb, idAy, idSe, idTea))
                    {
                        MessageBox.Show("Xóa thành công!");
                        FillTableAssignmentClass(clsName, ayName, semesName);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi! Hãy thử lại sau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
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
                                string formattedDateTime = dateTimeValue.ToString("MM/dd/yyyy");
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

        private void button5_Click(object sender, EventArgs e)
        {
            string appDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderPath = System.IO.Path.Combine(appDirectory, "excel", "DanhSachGiaoVien");

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
                ExportToExcel(TableTeacher, saveFileDialog.FileName);
                MessageBox.Show("Dữ liệu đã được xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Nếu người dùng không chọn đường dẫn, sử dụng đường dẫn mặc định
                ExportToExcel(TableTeacher, defaultExcelFilePath);
            }
        }
        private void ImportFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial; // Set the license context
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                List<Student> stu = new List<Student>();
                DataTable TeacherClone = tc.Clone();
                var student = tc.AsEnumerable().ToList();
                foreach (DataRow row in student)
                {
                    TeacherClone.ImportRow(row);
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

                        int idgv = int.Parse(worksheet.Cells[row, 1].Text);
                        string namegv = worksheet.Cells[row, 2].Text;
                        string gender = worksheet.Cells[row, 3].Text;
                        DateTime birthday = DateTime.Parse(worksheet.Cells[row, 4].Text);
                        string email = worksheet.Cells[row, 5].Text;
                        string address = worksheet.Cells[row, 6].Text;
                        string numberphone = worksheet.Cells[row, 7].Text;
                        string image = worksheet.Cells[row, 8].Text;
                        string technical = worksheet.Cells[row, 9].Text;
                        Teacher teacherEx = new Teacher(namegv, gender, address, birthday, email, numberphone, image);
                        bool existsInDatabase = teacherBLL.CheckTeacher(idgv);
                        HashSet<int> existingIDs = new HashSet<int>(TeacherClone.AsEnumerable().Select(rows => rows.Field<int>("Mã GV")));

                        if (!existsInDatabase)
                        {
                            Teacher teacherEx2 = new Teacher(idgv,namegv, gender, address, birthday, email, numberphone, image);
                            teacherBLL.InsertTeacher(teacherEx2);

                            string[] cm = technical.Split(',');
                            foreach (string name in cm)
                            {
                                int idsub = teacherBLL.GetIdSubject(name);
                                int idtea = teacherBLL.GetIdTeacherLast();
                                bool result = teacherBLL.InsertSubTecher(idtea, idsub);
                                if(result) {
                                    Console.WriteLine("Thêm chuyên môn excel thành công");
                                }
                            }
                            //idsub = teacherBLL.GetIdSubject(technical);
                            
                        }
                    }
                    MessageBox.Show("Import dữ liệu thành công!",
                             "Thông báo",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                    FillTableTeacher();
                }
            }
        }

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
    }
}
