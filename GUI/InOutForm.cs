using ManagerStudent.DAL;
using ManagerStudent.DTO;
using ManagerStudent.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class InOutForm : Form
    {
        public InOutForm()
        {
            InitializeComponent();
        }

        public string context = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="" && textBox2.Text == "")
            {

            }
            else
            {

                IList<IList<string>> conducts = connectExcel.importDataFromExcel(textBox2.Text, context);
                if (context == "Conduct")
                {
                    Application.OpenForms.OfType<CategoryForm>().FirstOrDefault().fillCategoryFom(conducts);
                }
                //Hàm Application.OpenForms trả về một collection chứa tất cả các form đang mở trong ứng dụng
                //phương thức mở rộng OfType<CategoryForm>(), chúng ta chỉ lấy ra các form có kiểu dữ liệu là CategoryForm.
               
                /*categoryForm.Show();*/
               
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox2.Text = openFileDialog1.FileName.Replace("\\", "\\\\");
            Console.WriteLine(openFileDialog1.FileName.Replace("\\", "\\\\"));
        }

        private void InOutForm_Resize(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" || textBox2.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void InOutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {

                button3.Enabled = true;
                textBox1.Text = "";
                textBox1.Enabled = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox1.Enabled = true;
                textBox2.Text = "";
                button3.Enabled=false;
            }
        }
    }
}
