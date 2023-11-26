using ManagerStudent.BLL;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq;
using ManagerStudent.DTO;
using System.Data;
using System.Reflection.Emit;
using ManagerStudent.DAL;

namespace ManagerStudent.GUI
{
    public partial class PointForm : Form
    {
        private DataTable studentPointsData;
        private int messageBoxCount = 0;
        private DataTable studentIdNameData;
        public PointForm()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, System.EventArgs e)
        {

        }

        private void PointForm_Load(object sender, System.EventArgs e)
        {
            //Đổ dữ liệu vào combobox tab Điểm
            PointBLL academicYearPointBLL = new PointBLL();
            academicYearPointBLL.LoadAcademicYearsIntoComboBox(comboBox1);

            PointBLL classesBLL = new PointBLL();
            DataTable classData = classesBLL.GetClassesData();
            comboBox2.ValueMember = "ID";
            comboBox2.DisplayMember = "className";
            comboBox2.DataSource = classData;

            //Chỉ chọn, không chỉnh sửa & set combobox trống
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndex = -1;

            PointBLL semestersBLL = new PointBLL();
            DataTable semesterData = semestersBLL.GetSemesterData();
            comboBox3.ValueMember = "ID";
            comboBox3.DisplayMember = "semesterName";
            comboBox3.DataSource = semesterData;

            //Chỉ chọn, không chỉnh sửa
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;

            PointBLL sujectBLL = new PointBLL();
            DataTable sujectData = sujectBLL.GetSujectData();
            comboBox4.ValueMember = "ID";
            comboBox4.DisplayMember = "subjectName";
            comboBox4.DataSource = sujectData;

            //Chỉ chọn, không chỉnh sửa & set combobox trống
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.SelectedIndex = -1;

            //Đổ dữ liệu vào combobox tab Điểm lớp
            PointBLL academicYearPointClasseBLL = new PointBLL();
            academicYearPointClasseBLL.LoadAcademicYearsIntoComboBox(comboBox6);

            PointBLL classesClassBLL = new PointBLL();
            DataTable classDataClass = classesClassBLL.GetClassesData();
            comboBox7.ValueMember = "ID";
            comboBox7.DisplayMember = "className";
            comboBox7.DataSource = classDataClass;

            //Chỉ chọn, không chỉnh sửa & set combobox trống
            comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox7.SelectedIndex = -1;

            PointBLL semestersClassBLL = new PointBLL();
            DataTable semesterDataClass = semestersClassBLL.GetSemesterData();
            comboBox8.ValueMember = "ID";
            comboBox8.DisplayMember = "semesterName";
            comboBox8.DataSource = semesterDataClass;

            //Chỉ chọn, không chỉnh sửa
            comboBox8.DropDownStyle = ComboBoxStyle.DropDownList;

            PointBLL sujectClassBLL = new PointBLL();
            DataTable sujectDataClass = sujectClassBLL.GetSujectData();
            comboBox5.ValueMember = "ID";
            comboBox5.DisplayMember = "subjectName";
            comboBox5.DataSource = sujectDataClass;

            //Chỉ chọn, không chỉnh sửa & set combobox trống
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.SelectedIndex = -1;

            //Đổ dữ liệu vào combobox tab Điểm học sinh
            PointBLL academicYearPointStudentBLL = new PointBLL();
            academicYearPointStudentBLL.LoadAcademicYearsIntoComboBox(comboBox9);

            PointBLL classesStudentBLL = new PointBLL();
            DataTable classDataStudent = classesStudentBLL.GetClassesData();
            comboBox10.ValueMember = "ID";
            comboBox10.DisplayMember = "className";
            comboBox10.DataSource = classDataStudent;

            //Chỉ chọn, không chỉnh sửa & set combobox trống
            comboBox10.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox10.SelectedIndex = -1;

            PointBLL semestersStudentBLL = new PointBLL();
            DataTable semesterDataStudent = semestersStudentBLL.GetSemesterData();
            comboBox11.ValueMember = "ID";
            comboBox11.DisplayMember = "semesterName";
            comboBox11.DataSource = semesterDataStudent;

            //Chỉ chọn, không chỉnh sửa
            comboBox11.DropDownStyle = ComboBoxStyle.DropDownList;

            /*PointBLL studentIdNameBLL = new PointBLL();
            studentIdNameBLL.LoadStudentsNameAndIdIntoComboBox(comboBox12, comboBox13);*/

            //comboBox9, comboBox 10, comboBox11 lần lượt là: Năm học, Lớp, Học kì
            comboBox9.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox10.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox11.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            comboBox12.Enabled = false;
            comboBox13.Enabled = false;
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem cả ComboBox "Năm học", "Lớp học" và "Học kỳ" đều đã được chọn
            if (comboBox9.SelectedIndex != -1 && comboBox10.SelectedIndex != -1 && comboBox11.SelectedIndex != -1)
            {
                // Kích hoạt ComboBox "Mã học sinh" và "Tên học sinh"
                comboBox12.Enabled = true;
                comboBox13.Enabled = true;

                int academicYearID = (int)comboBox9.SelectedValue;
                int semesterID = (int)comboBox11.SelectedValue;
                int classID = (int)comboBox10.SelectedValue;

                PointBLL studentIdNameBLL = new PointBLL();
                studentIdNameData = studentIdNameBLL.LoadStudentsNameAndIdIntoComboBox(academicYearID, semesterID, classID);

                comboBoxStudentIdName();
            }
            else
            {
                // Vô hiệu hóa ComboBox "Mã học sinh" và "Tên học sinh"
                comboBox12.Enabled = false;
                comboBox13.Enabled = false;

                // Xóa dữ liệu trong ComboBox "Mã học sinh" và "Tên học sinh"
                comboBox12.DataSource = null;
                comboBox13.DataSource = null;

            }
        }

        private void comboBoxStudentIdName()
        {
            if (studentIdNameData != null && studentIdNameData.Rows.Count > 0)
            {
                //Đổ dữ liệu vào comboBox ID và Tên học sinh
                comboBox12.ValueMember = "studentID";
                comboBox12.DisplayMember = "studentID";
                comboBox12.DataSource = studentIdNameData;

                comboBox13.ValueMember = "studentID";
                comboBox13.DisplayMember = "name";
                comboBox13.DataSource = studentIdNameData;

                comboBox12.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox13.DropDownStyle = ComboBoxStyle.DropDownList;

                label17.Text = "";
            }
            else
            {
                label17.Text = "Không có dữ liệu để hiển thị trên ComboBox";

                comboBox12.Enabled = false;
                comboBox13.Enabled = false;

                comboBox12.DataSource = null;
                comboBox13.DataSource = null;
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void UpdateDataGridView2()
        {
            if (studentPointsData != null && studentPointsData.Rows.Count > 0)
            {
                // Tạo một DataTable mới để chứa dữ liệu với cột STT
                DataTable updatedData = new DataTable();
                //updatedData.Columns.Add("STT", typeof(int));
                updatedData.Merge(studentPointsData);

                // Tính giá trị STT cho từng dòng
               /* for (int i = 0; i < updatedData.Rows.Count; i++)
                {
                    updatedData.Rows[i]["STT"] = i + 1; // Giá trị STT
                }*/
                //Không chỉnh sửa được trên DataGridView
                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = updatedData;
                //dataGridView2.Columns["STT"].DisplayIndex = 0; // Đặt vị trí hiển thị cho cột STT
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị");
                //Set cho comboBox Lớp học, Môn học và dataGridView trống khi không có dữ liệu
                comboBox7.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
                dataGridView2.DataSource = -1;
            }
        }
        private void saveInsertPoints()
        {
            if (dataGridView1.DataSource != null)
            {
                DataTable updatedData = (DataTable)dataGridView1.DataSource;

                int successCount = 0; // Số điểm đã được thêm thành công

                // Lặp qua từng dòng của DataTable
                foreach (DataRow row in updatedData.Rows)
                {
                    // Lấy thông tin cần thiết từ mỗi dòng
                    int studentID = Convert.ToInt32(row["Mã học sinh"]);

                    string academicyearName = comboBox1.Text;
                    string semesterName = comboBox3.Text;
                    string subjectName = comboBox4.Text;

                    // Thêm điểm đánh giá thường xuyên
                    if (!Convert.IsDBNull(row["Điểm đánh giá thường xuyên"]))
                    {
                        string pointName = "Điểm đánh giá thường xuyên";
                        double point = Convert.ToDouble(row["Điểm đánh giá thường xuyên"]);

                        // Gọi hàm BLL để cập nhật điểm
                        PointBLL bll = new PointBLL();
                        bool result = bll.InsertStudentPoint(studentID, academicyearName, semesterName,
                            subjectName, pointName, point);

                        if (result)
                        {
                            successCount++;
                        }
                    }

                    // Thêm điểm giữa kỳ
                    if (!Convert.IsDBNull(row["Điểm giữa kỳ"]))
                    {
                        string pointName = "Điểm giữa kỳ";
                        double point = Convert.ToDouble(row["Điểm giữa kỳ"]);

                        // Gọi hàm BLL để cập nhật điểm
                        PointBLL bll = new PointBLL();
                        bool result = bll.InsertStudentPoint(studentID, academicyearName, semesterName,
                            subjectName, pointName, point);

                        if (result)
                        {
                            successCount++;
                        }
                    }

                    // Thêm điểm cuối kỳ
                    if (!Convert.IsDBNull(row["Điểm cuối kỳ"]))
                    {
                        string pointName = "Điểm cuối kỳ";
                        double point = Convert.ToDouble(row["Điểm cuối kỳ"]);

                        // Gọi hàm BLL để cập nhật điểm
                        PointBLL bll = new PointBLL();
                        bool result = bll.InsertStudentPoint(studentID, academicyearName, semesterName,
                            subjectName, pointName, point);

                        if (result)
                        {
                            successCount++;
                        }
                    }
                }

                // Kiểm tra số lượng điểm đã được thêm thành công
                if (successCount > 0)
                {
                    MessageBox.Show("Đã thêm thành công " + successCount + " điểm.");
                }
                else
                {
                    MessageBox.Show("Không có điểm nào được thêm.");
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để lưu.");
            }
        }
        private void saveUpdatePoints()
        {
            // Kiểm tra nếu DataGridView có dữ liệu
            if (dataGridView1.DataSource != null && dataGridView1.DataSource is DataTable updatedData)
            {
                // Lặp qua từng dòng của DataTable
                foreach (DataRow row in updatedData.Rows)
                {

                    // Khai báo biến để lưu dữ liệu sau khi chuyển đổi
                    int studentID;
                    double regularPoint, midtermPoint, finalPoint;

                    // Thực hiện chuyển đổi và kiểm tra
                    bool isStudentIDValid = int.TryParse(row["Mã học sinh"].ToString(), out studentID);
                    bool isRegularPointValid = double.TryParse(row["Điểm đánh giá thường xuyên"].ToString(), out regularPoint);
                    bool isMidtermPointValid = double.TryParse(row["Điểm giữa kỳ"].ToString(), out midtermPoint);
                    bool isFinalPointValid = double.TryParse(row["Điểm cuối kỳ"].ToString(), out finalPoint);

                    // Kiểm tra xem tất cả các giá trị đã được chuyển đổi thành công chưa
                    if (isStudentIDValid || isRegularPointValid || isMidtermPointValid || isFinalPointValid)
                    {
                        // Gọi hàm BLL để cập nhật điểm
                        PointBLL bll = new PointBLL();
                        bool success = true;

                        // Thực hiện cập nhật điểm cho từng loại điểm
                        if (!bll.UpdateStudentPoint(studentID, tmpAcademicYearName, tmpSemesterName, tmpSubjectName, tmpClassName, "Điểm đánh giá thường xuyên", regularPoint))
                            success = false;

                        if (!bll.UpdateStudentPoint(studentID, tmpAcademicYearName, tmpSemesterName, tmpSubjectName, tmpClassName, "Điểm giữa kỳ", midtermPoint))
                            success = false;

                        if (!bll.UpdateStudentPoint(studentID, tmpAcademicYearName, tmpSemesterName, tmpSubjectName, tmpClassName, "Điểm cuối kỳ", finalPoint))
                            success = false;

                        if (success)
                        {
                            MessageBox.Show("Điểm đã được cập nhật thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra khi cập nhật điểm.");
                        }
                    }
                    else
                    {
                        continue;
                    }

                }
            }


        }
        private void UpdateDataGridView1()
        {
            tmpAcademicYearName = comboBox1.Text;
            tmpSemesterName = comboBox3.Text;
            tmpClassName = comboBox2.Text;
            tmpSubjectName = comboBox4.Text;

            /*Console.WriteLine(academicYearName);
            Console.WriteLine($"{academicYearName}");
            Console.WriteLine(className);
            Console.WriteLine(academicYearName, semesterName, className, subjectName);*/

            PointBLL bll = new PointBLL();
            studentPointsData = bll.GetStudentPoints(tmpAcademicYearName, tmpSemesterName, tmpClassName, tmpSubjectName);

            /*            dataGridView1.DataSource = studentPointsData;*/
            if (studentPointsData != null && studentPointsData.Rows.Count > 0)
            {
                // Tạo một DataTable mới để chứa dữ liệu với cột STT
                DataTable updatedData = new DataTable();
                updatedData.Columns.Add("STT", typeof(int));
                updatedData.Merge(studentPointsData);

                // Tính giá trị STT cho từng dòng
                for (int i = 0; i < updatedData.Rows.Count; i++)
                {
                    updatedData.Rows[i]["STT"] = i + 1; // Giá trị STT
                }
                dataGridView1.DataSource = updatedData;
                dataGridView1.Columns["STT"].DisplayIndex = 0; // Đặt vị trí hiển thị cho cột STT
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị");
                //Set cho comboBox Lớp học và Môn học trống
                comboBox2.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                dataGridView1.DataSource = null;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1 && comboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn lớp và môn học");
            }
            else if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn lớp học");
            }
            else if (comboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn môn học");
            }
            else
            {
                UpdateDataGridView1();

                originalData = ((DataTable)dataGridView1.DataSource).Copy();
                
            }
        }
        private DataTable originalData;
        private string tmpAcademicYearName;
        private string tmpSemesterName;
        private string tmpClassName;
        private string tmpSubjectName;
        private void button2_Click(object sender, EventArgs e)
        {
            // Lưu bản sao của dữ liệu ban đầu

            // Khi cần kiểm tra, so sánh dữ liệu hiện tại với bản sao ban đầu
            bool hasChanges = !((DataTable)dataGridView1.DataSource).AsEnumerable()
                                .SequenceEqual(originalData.AsEnumerable(), DataRowComparer.Default);
            //if (hasChanges)
            //{
                saveUpdatePoints();
                //MessageBox.Show("Update!");
            //}
            /*saveUpdatePoints();*/
            //saveInsertPoints();
            //messageBoxCount = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == -1 && comboBox5.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn lớp và môn học");
            }
            else if (comboBox7.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn lớp học");
            }
            else if (comboBox5.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn môn học");
            }
            else
            {
                PointBLL bll = new PointBLL();
                string academicYearName = comboBox6.Text;
                string semesterName = comboBox8.Text;
                string className = comboBox7.Text;
                string subjectName = comboBox5.Text;

                studentPointsData = bll.GetStudentPoints(academicYearName, semesterName, className, subjectName);
                UpdateDataGridView2();
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Không cho chỉnh sửa 3 cột bên dưới
            // Kiểm tra nếu đang chỉnh sửa cột "STT" hoặc cột "Tên học sinh"
            /*if (e.ColumnIndex == dataGridView1.Columns["STT"].Index ||
                e.ColumnIndex == dataGridView1.Columns["Mã học sinh"].Index ||
                e.ColumnIndex == dataGridView1.Columns["Tên học sinh"].Index)
            {
                // Ngăn chặn chỉnh sửa trên cột "STT" và cột "Tên học sinh"
                e.Cancel = true;
            }*/
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Điểm đánh giá thường xuyên" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "Điểm giữa kỳ" ||
                dataGridView1.Columns[e.ColumnIndex].Name == "Điểm cuối kỳ")
            {
                if (!string.IsNullOrEmpty(e.FormattedValue?.ToString()))
                {
                    if (!float.TryParse(e.FormattedValue.ToString(), out float point))
                    {
                        MessageBox.Show("Điểm vừa nhập không hợp lệ!");
                        dataGridView1.CancelEdit();
                    }
                    else if (point < 0)
                    {
                        MessageBox.Show("Điểm không được âm!");
                        dataGridView1.CancelEdit();
                    }
                    else if (point > 10)
                    {
                        MessageBox.Show("Điểm không được lớn hơn 10!");
                        dataGridView1.CancelEdit();
                    }

                }
            }
        }

        private PointBLL pointBLL;
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox10.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn lớp học");
            } else if (comboBox12.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn mã học sinh");
            }
            else
            {
                pointBLL = new PointBLL();
                dataGridView3.DataSource = pointBLL.StudentPoint(int.Parse(comboBox12.Text), comboBox9.Text, comboBox11.Text, comboBox10.Text);
                dataGridView4.DataSource = pointBLL.StudentSummary(int.Parse(comboBox12.Text), comboBox9.Text, comboBox11.Text, comboBox10.Text);
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu để xuất!");
            }
            else
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Tệp Excel (*.xlsx)|*.xlsx|Tệp Excel cũ (*.xls)|*.xls";
                    saveFileDialog.Title = "Lưu tệp tin";

                    DialogResult result = saveFileDialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                    {
                        // Lưu tệp với đường dẫn đã chọn
                        string filePath = saveFileDialog.FileName;
                        //gọi phương thức
                        //lưu với kiểu dữ liệu là datatable
                        ConnectExcel.ExportDataToExcel(filePath, (DataTable)dataGridView3.DataSource);


                        MessageBox.Show("Đã lưu tệp: " + filePath);
                    }
                }
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.DataSource==null) {
                MessageBox.Show("Không có dữ liệu để xuất!");
            }
            else
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Tệp Excel (*.xlsx)|*.xlsx|Tệp Excel cũ (*.xls)|*.xls";
                    saveFileDialog.Title = "Lưu tệp tin";

                    DialogResult result = saveFileDialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                    {
                        // Lưu tệp với đường dẫn đã chọn
                        string filePath = saveFileDialog.FileName;
                        //gọi phương thức
                        //lưu với kiểu dữ liệu là datatable
                        ConnectExcel.ExportDataToExcel(filePath, (DataTable)dataGridView2.DataSource);
                        MessageBox.Show("Đã lưu tệp: " + filePath);
                    }
                }
            }
        }
    }
}
