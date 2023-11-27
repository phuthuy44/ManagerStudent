using ManagerStudent.BLL;
using ManagerStudent.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class ThongKeForm : Form
    {
        private TeacherBLL teacherBLL;
        private ThongKeBLL thongkeBLL;
        public ThongKeForm()
        {
            InitializeComponent();
            teacherBLL = new TeacherBLL();
            thongkeBLL = new ThongKeBLL();
        }

        private void ThongKeForm_Load(object sender, EventArgs e)
        {
            TableSL.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TableSL.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cbNHds.Items.Add("Tất cả");
            cbNHhk.Items.Add("Tất cả");
            cbNHsl.Items.Add("Tất cả");
            cbHKds.Items.Add("Tất cả");
            cbHKhk.Items.Add("Tất cả");
            cbHKsl.Items.Add("Tất cả");
            
            
            cbSL.SelectedIndex = 0;
            cbNHds.SelectedIndex = 0;
            cbNHhk.SelectedIndex = 0;
            cbNHsl.SelectedIndex = 0;
            cbHKds.SelectedIndex = 0;
            cbHKsl.SelectedIndex = 0;
            cbSL.SelectedIndex = 0;
            DataTable ay = teacherBLL.GetAcademicYear();
            DataTable sm = teacherBLL.GetSemester();
            foreach (DataRow row in ay.Rows)
            {
                string acayear = row["academicyearName"].ToString();

                cbNHds.Items.Add(acayear);
                cbNHhk.Items.Add(acayear);
                cbNHsl.Items.Add(acayear);
            }
            foreach (DataRow row in sm.Rows)
            {
                string smName = row["semesterName"].ToString();

                cbHKds.Items.Add(smName);
                cbHKhk.Items.Add(smName);
                cbHKsl.Items.Add(smName);
            }
            FillAllNumberStudent();
            dataGridView1.DataSource = thongkeBLL.StatisticalCapacity(cbNHds.Text, cbHKds.Text);
            dataGridView2.DataSource = thongkeBLL.StatisticalConduct(cbNHhk.Text, cbHKhk.Text);
        }

        private void FillAllNumberStudent()
        {
            TableSL.DataSource = thongkeBLL.GetAllNumberStudent();
        }
        private void FillGradeNumberStudent()
        {
            TableSL.DataSource = thongkeBLL.GetGradeNumberStudent();
        }
        private void FillClassNumberStudent()
        {
            TableSL.DataSource = thongkeBLL.GetClassNumberStudent();
        }
        private void FillClassAyearNumberStudent(string ayName)
        {
            TableSL.DataSource = thongkeBLL.GetClassAyearNumberStudent(ayName);
        }
        private void FillGradeAyearNumberStudent(string ayName)
        {
            TableSL.DataSource = thongkeBLL.GetClassAyearNumberStudent(ayName);
        }
        private void FillAllAyearNumberStudent(string ayName)
        {
            TableSL.DataSource = thongkeBLL.GetAllAyearNumberStudent(ayName);
        }
        private void FillClassSemesNumberStudent(string seName)
        {
            TableSL.DataSource = thongkeBLL.GetClassSemesNumberStudent(seName);
        }
        private void FillGradeSemesNumberStudent(string seName)
        {
            TableSL.DataSource = thongkeBLL.GetGradeSemesNumberStudent(seName);
        }
        private void FillAllSemesNumberStudent(string seName)
        {
            TableSL.DataSource = thongkeBLL.GetAllSemesNumberStudent(seName);
        }
        private void FillNumberStudent(string ayName, string seName)
        {
            TableSL.DataSource = thongkeBLL.GetNumberStudent(ayName, seName);
        }
        private void FillClassASNumberStudent(string ayName, string seName)
        {
            TableSL.DataSource = thongkeBLL.GetClassASNumberStudent(ayName, seName);
        }
        private void FillGradeASNumberStudent(string ayName, string seName)
        {
            TableSL.DataSource = thongkeBLL.GetGradeASNumberStudent(ayName, seName);
        }
        private void cbSL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sl = cbSL.SelectedItem?.ToString();
            string ayName = cbNHsl.SelectedItem?.ToString();
            string seName = cbHKsl.SelectedItem?.ToString();
            if (sl == "Tất cả")
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillAllNumberStudent();
                    }
                    else
                    {
                        FillAllSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillAllAyearNumberStudent(ayName);
                    }

                }
            }
            else if (cbSL.SelectedItem?.ToString() == "Lớp")
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillClassNumberStudent();
                    }
                    else
                    {
                        FillClassSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillClassASNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillClassAyearNumberStudent(ayName);
                    }
                }
            }
            else
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillGradeNumberStudent();
                    }
                    else
                    {
                        FillGradeSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillGradeASNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillGradeAyearNumberStudent(ayName);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sl = cbSL.SelectedItem?.ToString();
            string ayName = cbNHsl.SelectedItem?.ToString();
            string seName = cbHKsl.SelectedItem?.ToString();
            if (sl == "Tất cả")
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillAllNumberStudent();
                    }
                    else
                    {
                        FillAllSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillAllAyearNumberStudent(ayName);
                    }

                }
            }
            else if (cbSL.SelectedItem?.ToString() == "Lớp")
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillClassNumberStudent();
                    }
                    else
                    {
                        FillClassSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillClassASNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillClassAyearNumberStudent(ayName);
                    }
                }
            }
            else
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillGradeNumberStudent();
                    }
                    else
                    {
                        FillGradeSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillGradeASNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillGradeAyearNumberStudent(ayName);
                    }
                }
            }
        }

        private void cbNHsl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sl = cbSL.SelectedItem?.ToString();
            string ayName = cbNHsl.SelectedItem?.ToString();
            string seName = cbHKsl.SelectedItem?.ToString();
            if (sl == "Tất cả")
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillAllNumberStudent();
                    }
                    else
                    {
                        FillAllSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillAllAyearNumberStudent(ayName);
                    }

                }
            }
            else if (cbSL.SelectedItem?.ToString() == "Lớp")
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillClassNumberStudent();
                    }
                    else
                    {
                        FillClassSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillClassASNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillClassAyearNumberStudent(ayName);
                    }
                }
            }
            else
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillGradeNumberStudent();
                    }
                    else
                    {
                        FillGradeSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillGradeASNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillGradeAyearNumberStudent(ayName);
                    }
                }
            }
        }

        private void cbHKsl_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sl = cbSL.SelectedItem?.ToString();
            string ayName = cbNHsl.SelectedItem?.ToString();
            string seName = cbHKsl.SelectedItem?.ToString();
            if (sl == "Tất cả")
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillAllNumberStudent();
                    }
                    else
                    {
                        FillAllSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillAllAyearNumberStudent(ayName);
                    }

                }
            }
            else if (cbSL.SelectedItem?.ToString() == "Lớp")
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillClassNumberStudent();
                    }
                    else
                    {
                        FillClassSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillClassASNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillClassAyearNumberStudent(ayName);
                    }
                }
            }
            else
            {
                if (ayName == "Tất cả")
                {
                    if (seName == "Tất cả")
                    {
                        FillGradeNumberStudent();
                    }
                    else
                    {
                        FillGradeSemesNumberStudent(seName);
                    }
                }
                else
                {
                    if (seName != "Tất cả")
                    {
                        FillGradeASNumberStudent(ayName, seName);
                    }
                    else
                    {
                        FillGradeAyearNumberStudent(ayName);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = thongkeBLL.StatisticalCapacity(cbNHds.Text, cbHKds.Text);
        }

        private void cbNHds_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = thongkeBLL.StatisticalCapacity(cbNHds.Text, cbHKds.Text);
        }

        private void cbHKds_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = thongkeBLL.StatisticalCapacity(cbNHds.Text, cbHKds.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
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
                        ConnectExcel.ExportDataToExcel(filePath, (DataTable)dataGridView1.DataSource);
                        MessageBox.Show("Đã lưu tệp: " + filePath);
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = thongkeBLL.StatisticalConduct(cbNHds.Text, cbHKds.Text);
        }

        private void cbNHhk_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = thongkeBLL.StatisticalConduct(cbNHhk.Text, cbHKhk.Text);
        }

        private void cbHKhk_SelectedValueChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = thongkeBLL.StatisticalConduct(cbNHhk.Text, cbHKhk.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.DataSource == null)
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
                        ConnectExcel.ExportDataToExcel(filePath, (DataTable)dataGridView2.DataSource);
                        MessageBox.Show("Đã lưu tệp: " + filePath);
                    }
                }
            }
        }
    }
}
