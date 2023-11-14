using Google.Apis.Sheets.v4.Data;
using ManagerStudent.BLL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
            conductTab();
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
        private void conductTab()
        {
            conductBLL = new ConductBLL();
            dataGridView2.DataSource=conductBLL.GetConductData();
            
            
        }
        private void capacityTab()
        {
            capacity = new CapacityManager();

            // Đặt DataTable làm nguồn dữ liệu của DataGridView
            dataGridView3.DataSource = capacity.GetAllCapacity();

        }
        private void SemesterTab()
        {
            SemesterBLL semester = new SemesterBLL();
            dataGridView4.DataSource = semester.SemesterData();

        }
        private void TypeOfPointTab()
        {
            TypeOfPointBLL pointBLL = new TypeOfPointBLL();
            dataGridView5.DataSource = pointBLL.TypeOfPointData();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;
            switch (index)
            {
                case 0:
                    conductTab();
                    break;
                case 1:
                    capacityTab();
                    break;
                case 2:
                    SemesterTab();
                    break;
                case 3:
                    TypeOfPointTab();
                    break;
            }
            
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        public void closedForm()
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void FindConductData()
        {
            conductBLL = new ConductBLL();
            dataGridView2.DataSource = conductBLL.FindConduct(textBox2.Text.Trim());
        }

        private void FindCapacityData()
        {
            capacity = new CapacityManager();
            dataGridView3.DataSource = capacity.FindCapacity(textBox5.Text.Trim());
        }
        private void button6_Click(object sender, EventArgs e)
        {
            FindConductData();
            }

        private void button11_Click(object sender, EventArgs e)
        {
            FindCapacityData();
        }

        private ConductBLL conductBLL;
        private CapacityManager capacity;

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        
            if (e.RowIndex>=0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                textBox11.Text = row.Cells[0].Value.ToString();
                textBox10.Text = row.Cells[1].Value.ToString();
                textBox7.Text = row.Cells[2].Value.ToString();
                textBox6.Text = row.Cells[3].Value.ToString();
                textBox12.Text = row.Cells[4].Value.ToString();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Thực hiện các xử lý khi nhấn phím Enter tại đây
                FindConductData();

                // Ngăn không cho phím Enter được hiển thị trong textBox2
                e.Handled = true;
            }
        }

        private void tabControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Thực hiện các xử lý khi nhấn phím Enter tại đây
                FindCapacityData();

                // Ngăn không cho phím Enter được hiển thị trong textBox2
                e.Handled = true;
            }
        }
    }
}
