using ManagerStudent.BLL;
using System;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class HocSinhForm : Form
    {
        private StudentBLL studentBLL;

        public HocSinhForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            // TODO: This line of code loads data into the 'studentManagerDataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.studentManagerDataSet.Student);


        }
        //Xu ly fill dataTable lên dataGridView
        public void GetListStudent()
        {
            dataTableStudent.DataSource = studentBLL.GetListStudent();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
