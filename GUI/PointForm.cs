using ManagerStudent.BLL;
using System;
using System.Windows.Forms;
//Push thử
namespace ManagerStudent.GUI
{
    public partial class PointForm : Form
    {
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

            PointBLL classPointBLL = new PointBLL();
            classPointBLL.LoadClassesIntoComboBox(comboBox2);

            PointBLL semesterPointBLL = new PointBLL();
            semesterPointBLL.LoadSemestersIntoComboBox(comboBox3);

            PointBLL subjectPointBLL = new PointBLL();
            subjectPointBLL.LoadSubjectsIntoComboBox(comboBox4);

            //Đổ dữ liệu vào combobox tab Điểm lớp
            PointBLL academicYearClassPointBLL = new PointBLL();
            academicYearClassPointBLL.LoadAcademicYearsIntoComboBox(comboBox6);

            PointBLL classClassPointBLL = new PointBLL();
            classClassPointBLL.LoadClassesIntoComboBox(comboBox7);

            PointBLL semesterClassPointBLL = new PointBLL();
            semesterClassPointBLL.LoadSemestersIntoComboBox(comboBox8);

            PointBLL subjectClassPointBLL = new PointBLL();
            subjectClassPointBLL.LoadSubjectsIntoComboBox(comboBox5);

            //Đổ dữ liệu vào combobox tab Điểm học sinh
            PointBLL academicYearStudentPointBLL = new PointBLL();
            academicYearStudentPointBLL.LoadAcademicYearsIntoComboBox(comboBox9);

            PointBLL classStudentPointBLL = new PointBLL();
            classStudentPointBLL.LoadClassesIntoComboBox(comboBox10);

            PointBLL semesterStudentPointBLL = new PointBLL();
            semesterStudentPointBLL.LoadSemestersIntoComboBox(comboBox11);

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

                // Hiển thị dữ liệu trong ComboBox "Mã học sinh" và "Tên học sinh"
                PointBLL studentIdNameBLL = new PointBLL();
                studentIdNameBLL.LoadStudentsNameAndIdIntoComboBox(comboBox12, comboBox13);
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
    }
}
