using DocumentFormat.OpenXml.Office.CustomUI;
using ManagerStudent.BLL;
using ManagerStudent.DTO;
using OfficeOpenXml.LoadFunctions.Params;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private void conductTab()
        {
            ConductBLL conductBLL = new ConductBLL();
            Response response = conductBLL.GetConductData();

            //Kiểm tra lấy dữ liệu thành công không
            if (!response.Status) {
                MessageBox.Show(response.Message);
            }
            else
            {
                dataGridView2.ReadOnly = true; // Khoá toàn bộ DataGridView
                                               //Xoá bảng để tránh bị chồng dữ liệu
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                //Tạo dòng đầu tiên để chứa tên các cột
                dataGridView2.Columns.AddRange(
                    new DataGridViewTextBoxColumn { Name = "ColumnName1", HeaderText = "Xếp loại" },
                    new DataGridViewTextBoxColumn { Name = "ColumnName2", HeaderText = "Điểm cận trên" },
                    new DataGridViewTextBoxColumn { Name = "ColumnName3", HeaderText = "Điểm cận dưới" }
                );
                //Đặt độ rộng cho từng cột
                dataGridView2.Columns[0].Width = 200; // Đặt độ rộng của cột 0 là 200 pixel
                dataGridView2.Columns[1].Width = 150; // Đặt độ rộng của cột 1 là 150 pixel
                dataGridView2.Columns[2].Width = 150; // Đặt độ rộng của cột 2 là 150 pixel

                //Lấy danh sách dữ liệu
                IList<Conduct> conducts = (IList<Conduct>)response.Data;

                //Fill dữ liệu vào bảng
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
        private void capacityTab()
        {
            CapacityBLL capacity = new CapacityBLL();
            Response response = capacity.GetCapacityData();
            //Kiểm tra lấy dữ liệu thành công không
            if (!response.Status)
            {
                MessageBox.Show(response.Message);
            }else {
                dataGridView3.ReadOnly = true; // Khoá toàn bộ DataGridView

                //Xoá bảng để tránh bị chồng dữ liệu
                dataGridView3.Rows.Clear();
                dataGridView3.Columns.Clear();

                //Tạo dòng đầu tiên để chứa tên các cột
                dataGridView3.Columns.AddRange(
                    new DataGridViewTextBoxColumn { Name = "ColumnName1", HeaderText = "Xếp loại" },
                    new DataGridViewTextBoxColumn { Name = "ColumnName2", HeaderText = "Điểm cận trên" },
                    new DataGridViewTextBoxColumn { Name = "ColumnName3", HeaderText = "Điểm cận dưới" },
                    new DataGridViewTextBoxColumn { Name = "ColumnName4", HeaderText = "Điểm khống chế" }
                );
                //Đặt độ rộng cho từng cột
                dataGridView3.Columns[0].Width = 150; // Đặt độ rộng của cột 0 là 150 pixel
                dataGridView3.Columns[1].Width = 100; // Đặt độ rộng của cột 1 là 100 pixel
                dataGridView3.Columns[2].Width = 100; // Đặt độ rộng của cột 2 là 100 pixel
                dataGridView3.Columns[3].Width = 120; // Đặt độ rộng của cột 3 là 120 pixel

                //Lấy danh sách dữ liệu
                IList<Capacity> capacities = (IList<Capacity>)response.Data;
                //Fill dữ liệu vào bảng
                foreach (var i in capacities)
                {
                    Console.WriteLine(i);
                    dataGridView3.Rows.Add(new string[]
                    {
                        i.Name, i.upperLimit.ToString(), i.lowerLimit.ToString(), i.paraPoint.ToString()
                    });
                }

            }

        }
        private void SemesterTab()
        {
            SemesterBLL semester = new SemesterBLL();
            Response response = semester.GetDataSemester();
            //Kiểm tra lấy dữ liệu thành công không
            if (!response.Status)
            {
                MessageBox.Show(response.Message);
            }
            else
            {
                dataGridView4.ReadOnly = true; // Khoá toàn bộ DataGridView

                //Xoá bảng để tránh bị chồng dữ liệu
                dataGridView4.Rows.Clear();
                dataGridView4.Columns.Clear();

                //Tạo dòng đầu tiên để chứa tên các cột
                dataGridView4.Columns.AddRange(
                    new DataGridViewTextBoxColumn { Name = "ColumnName1", HeaderText = "Mô tả" },
                    new DataGridViewTextBoxColumn { Name = "ColumnName2", HeaderText = "Hệ số" }
                );

                //Đặt độ rộng cho từng cột
                dataGridView4.Columns[0].Width = 250; // Đặt độ rộng của cột 0 là 250 pixel
                dataGridView4.Columns[1].Width = 250; // Đặt độ rộng của cột 1 là 250 pixel
               
                //Lấy danh sách dữ liệu
                IList<Semester> semesters = (IList<Semester>)response.Data;

                //Fill dữ liệu vào bảng
                foreach (var i in semesters)
                {
                    Console.WriteLine(i);
                    dataGridView4.Rows.Add(new string[]
                    {
                        i.Name, i.Coefficient.ToString(),
                    });
                }

            }

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;
            switch (index)
            {
                case 1:
                    conductTab();
                    break;
                case 2:
                    capacityTab();
                    break;
                case 3:
                    SemesterTab();
                    break;
            }
            
        }
        public void fillCategoryFom(IList<IList<string>> conducts) {
            dataGridView2.ReadOnly = true; // Khoá toàn bộ DataGridView

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "ColumnName1", HeaderText = "Xếp loại" },
                new DataGridViewTextBoxColumn { Name = "ColumnName2", HeaderText = "Điểm cận trên" },
                new DataGridViewTextBoxColumn { Name = "ColumnName3", HeaderText = "Điểm cận dưới" }
            );
            dataGridView2.Columns[0].Width = 200; // Đặt độ rộng của cột 0 là 200 pixel
            dataGridView2.Columns[1].Width = 150; // Đặt độ rộng của cột 1 là 150 pixel
            dataGridView2.Columns[2].Width = 150; // Đặt độ rộng của cột 2 là 150 pixel

            bool is_first_row = false;
            foreach (var i in conducts)
            {
                if (!is_first_row)
                {
                    is_first_row = true;
                    continue;
                }
                dataGridView2.Rows.Add(i.ToArray());
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
            
            InOutForm inOutForm = new InOutForm();
            InOutForm form = Application.OpenForms.OfType<InOutForm>().FirstOrDefault();
            //form.context = "Conduct";
            if (form == null)
            {
                inOutForm.context = "Conduct";
                inOutForm.Show();
            }
        }
    }
}
