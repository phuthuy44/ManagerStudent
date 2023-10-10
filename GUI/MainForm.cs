using System;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class MainForm : Form
    {

        private Panel currentPanel;
        private Form FormChild;

        public MainForm()
        {
            InitializeComponent();
        }


        private void OpenFormChild(Form childForm)
        {
            if (FormChild != null)
            {
                FormChild.Close();
            }
            FormChild = childForm;
            childForm.TopLevel = false;
            panel_Right.Controls.Clear();
            panel_Right.Controls.Add(childForm);
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Show();

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            OpenFormChild(new HocSinhForm());

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelHocSinh_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            OpenFormChild(new TeacherForm());

        }

        private void label3_Click(object sender, EventArgs e)
        {
            OpenFormChild(new ClassForm());

        }

        private void label4_Click(object sender, EventArgs e)
        {
            OpenFormChild(new PointForm());

        }

        private void label5_Click(object sender, EventArgs e)
        {
            OpenFormChild(new CategoryForm());

        }

        private void label6_Click(object sender, EventArgs e)
        {
            OpenFormChild(new ThongKeForm());

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
