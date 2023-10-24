
using ManagerStudent.BLL;
using ManagerStudent.DTO;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void ValidateActionLogin()
        {
            if (textBox1.Text != "")
            {
                AccountBLL accountBLL = new AccountBLL();
                Response response = JsonConvert.DeserializeObject<Response>(accountBLL.CheckAccount(textBox1.Text, textBox2.Text));

                if (!response.Status)
                {
                    MessageBox.Show(response.Message);
                    if (textBox2.Text == "")
                    {
                        textBox1.Focus();
                    }
                    else
                    {
                        textBox2.Focus();
                    }

                }
                else
                {
                    MessageBox.Show(response.Message);
                    MainForm mainForm = new MainForm();
                    this.Hide();
                    mainForm.Show();

                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!");
                textBox1.Focus();
            }
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            ValidateActionLogin();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ValidateActionLogin();
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ValidateActionLogin();
                e.Handled = true;
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
