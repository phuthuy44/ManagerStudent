using System;
using System.Drawing;
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

        private void btnChangeTabColor(object sender, EventArgs e)
        {
            foreach (Control item in panel8.Controls)
            {
                item.BackColor = Color.SpringGreen;
            }
            Control click = (Control)sender;
            click.BackColor = Color.White;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            OpenFormChild(new HocSinhForm());
            btnChangeTabColor(panel2, null);

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
            btnChangeTabColor(panel5, null);

        }

        private void label3_Click(object sender, EventArgs e)
        {
            OpenFormChild(new ClassForm());
            btnChangeTabColor(panel4, null);

        }

        private void label4_Click(object sender, EventArgs e)
        {
            OpenFormChild(new PointForm());
            btnChangeTabColor(panel3, null);

        }

        private void label5_Click(object sender, EventArgs e)
        {
            OpenFormChild(new CategoryForm());
            btnChangeTabColor(panel1, null);

        }

        private void label6_Click(object sender, EventArgs e)
        {
            OpenFormChild(new ThongKeForm());
            btnChangeTabColor(panel6, null);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
