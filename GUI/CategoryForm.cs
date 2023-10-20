using DocumentFormat.OpenXml.Office.CustomUI;
using ManagerStudent.BLL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                // Create columns first
                /*dataGridView2.Columns.Add("ColumnName1", "Column Header 1");
                dataGridView2.Columns.Add("ColumnName2", "Column Header 2");
                dataGridView2.Columns.Add("ColumnName3", "Column Header 3");*/

                ConductBLL conductBLL = new ConductBLL();
                //MessageBox.Show(conductBLL.GetConductData().Message);
                dataGridView2.ReadOnly = true; // Khoá toàn bộ DataGridView

                dataGridView2.Rows.Clear();
                dataGridView2.Columns.AddRange(
                    new DataGridViewTextBoxColumn { Name = "ColumnName1", HeaderText = "Xếp loại" },
                    new DataGridViewTextBoxColumn { Name = "ColumnName2", HeaderText = "Điểm cận trên" },
                    new DataGridViewTextBoxColumn { Name = "ColumnName3", HeaderText = "Điểm cận dưới" }
                );
                dataGridView2.Columns[0].Width = 200; // Đặt độ rộng của cột 0 là 200 pixel
                dataGridView2.Columns[1].Width = 150; // Đặt độ rộng của cột 1 là 150 pixel
                dataGridView2.Columns[2].Width = 150; // Đặt độ rộng của cột 2 là 150 pixel

                IList<Conduct> conducts = (IList<Conduct>)conductBLL.GetConductData().Data;
                foreach (var i in conducts)
                {
                    Console.WriteLine(i);
                    dataGridView2.Rows.Add(new string[]
                    {
                        i.Name, i.upperLimit.ToString(), i.lowerLimit.ToString()
                    });
                }    
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            InOutForm inOutForm = new InOutForm();
            if (!inOutForm.Visible)
            {
                inOutForm.Show();
            }
            
        }
    }
}
