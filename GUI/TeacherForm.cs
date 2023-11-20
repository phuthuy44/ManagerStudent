using ManagerStudent.BLL;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class TeacherForm : Form
    {
        private TeacherBLL teacherBLL;
        private Teacher teacher;
        public TeacherForm()
        {
            InitializeComponent();
            teacherBLL = new TeacherBLL();

        }
        public void GetDataTeacher()
        {
            TableTeacher.DataSource = teacherBLL.GetDataTeacher();
        }

        private void TeacherForm_Load(object sender, System.EventArgs e)
        {
            GetDataTeacher();
            DataGridViewComboBoxColumn comboBoxColumn1 = new DataGridViewComboBoxColumn();
            comboBoxColumn1.Name = "comboBoxColumn1";
            comboBoxColumn1.HeaderText = "Lớp chủ nhiệm";
            comboBoxColumn1.Items.AddRange("Option 1", "Option 2", "Option 3");
            TableAssignment.Columns.Add(comboBoxColumn1);
            DataGridViewComboBoxColumn comboBoxColumn2 = new DataGridViewComboBoxColumn();
            comboBoxColumn2.Name = "comboBoxColumn2";
            comboBoxColumn2.HeaderText = "Lớp giảng dạy";
            comboBoxColumn2.Items.AddRange("Option 1", "Option 2", "Option 3");
            TableAssignment.Columns.Add(comboBoxColumn2);
        }
    }
}
