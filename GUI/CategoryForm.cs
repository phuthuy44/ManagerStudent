using Google.Apis.Sheets.v4.Data;
using ManagerStudent.BLL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ManagerStudent.GUI
{

    public partial class CategoryForm : Form
    {
        private ConductBLL insertConductBLL;
        private ConductBLL deleteConductBLL;
        private ConductBLL updateConductBLL;
        private ConductBLL conductBLL;

        private CapacityManager capacityBLL;
        private CapacityManager insertCapacityBLL;
        private CapacityManager updateCapacityBLL;
        private CapacityManager deleteCapacityBLL;

        private SemesterBLL semesterBLL;
        private SemesterBLL insertSemesterBLL;
        private SemesterBLL updateSemesterBLL;
        private SemesterBLL deleteSemesterBLL;

        private TypeOfPointBLL typeofpointBLL;
        private TypeOfPointBLL insertTypeOfPointBLL;
        private TypeOfPointBLL updateTypeOfPointBLL;
        private TypeOfPointBLL deleteTypeOfPointBLL;

        private SubjectBLL subjectBLL;
        private SubjectBLL insertSubjectBLL;
        private SubjectBLL updateSubjectBLL;
        private SubjectBLL deleteSubjectBLL;


        public CategoryForm()
        {
            InitializeComponent();
            conductTab();
            Shown += (sender, e) => dataGridView2.ClearSelection();
            /*tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;*/
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
            dataGridView2.DataSource = conductBLL.GetConductData();
        }
        private void capacityTab()
        {
            capacityBLL = new CapacityManager();

            // Đặt DataTable làm nguồn dữ liệu của DataGridView
            dataGridView3.DataSource = capacityBLL.GetAllCapacity();

        }
        private void SemesterTab()
        {
            semesterBLL = new SemesterBLL();
            dataGridView4.DataSource = semesterBLL.SemesterData();

        }
        private void TypeOfPointTab()
        {
            typeofpointBLL = new TypeOfPointBLL();
            dataGridView5.DataSource = typeofpointBLL.TypeOfPointData();
        }

        private void SubjectTab()
        {
            subjectBLL = new SubjectBLL();
            dataGridView1.DataSource = subjectBLL.GetSubjectData();
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
                case 4:
                    SubjectTab();
                    break;
            }
            //Xóa lựa chọn trong dataGridView khi chuyển tab
            if (tabControl1.SelectedTab == tabPage2)
            {
                dataGridView2.ClearSelection();
                textBox4.Clear();
                textBox3.Clear();
                textBox1.Clear();
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                dataGridView3.ClearSelection();
                textBox11.Clear();
                textBox10.Clear();
                textBox7.Clear();
                textBox6.Clear();
                textBox12.Clear();
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                dataGridView4.ClearSelection();
                textBox15.Clear();
                textBox14.Clear();
                textBox8.Clear();
            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                dataGridView5.ClearSelection();
                textBox18.Clear();
                textBox17.Clear();
                textBox9.Clear();
            }
            else if (tabControl1.SelectedTab == tabPage1)
            {
                dataGridView1.ClearSelection();
                textBox21.Clear();
                textBox20.Clear();
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
            capacityBLL = new CapacityManager();
            dataGridView3.DataSource = capacityBLL.FindCapacity(textBox5.Text.Trim());
        }

        private void FindSemesterData()
        {

            semesterBLL = new SemesterBLL();
            dataGridView4.DataSource = semesterBLL.FindSemester(textBox13.Text.Trim());

        }

        private void FindTypeOfPoint()
        {

            typeofpointBLL = new TypeOfPointBLL();
            dataGridView5.DataSource = typeofpointBLL.FindTypeOfPoint(textBox16.Text.Trim());
        }

        private void FindSubject()
        {
            subjectBLL = new SubjectBLL();
            dataGridView1.DataSource = subjectBLL.FindSubjects(textBox22.Text.Trim());
        }
        private void button6_Click(object sender, EventArgs e)
        {
            FindConductData();
            textBox2.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FindCapacityData();
            textBox5.Clear();
        }

        private void insertCapacity(string conductName, float upperLimit, float lowerLimit, float paraPoint)
        {

            insertCapacityBLL = new CapacityManager();
            bool isInserted = insertCapacityBLL.insertCapacities(conductName, upperLimit, lowerLimit, paraPoint);

            if (isInserted)
            {
                MessageBox.Show("Thêm thành công!");
                textBox11.Clear();
                textBox10.Clear();
                textBox7.Clear();
                textBox6.Clear();
                textBox12.Clear();
                // Lấy dữ liệu từ cơ sở dữ liệu và gán cho dataGridView1
                capacityTab();
            }
            else
            {
                // Xử lý khi không thêm được dữ liệu
                MessageBox.Show("Không thêm được dữ liệu. Vui lòng thử lại.");
            }
        }
        private void insertConduct(string conductName, int upperLimit, int lowerLimit)
        {

            insertConductBLL = new ConductBLL();
            bool isInserted = insertConductBLL.insertConducts(conductName, upperLimit, lowerLimit);

            if (isInserted)
            {
                MessageBox.Show("Thêm thành công!");
                textBox4.Clear();
                textBox3.Clear();
                textBox1.Clear();
                // Lấy dữ liệu từ cơ sở dữ liệu và gán cho dataGridView1
                conductTab();
            }
            else
            {
                // Xử lý khi không thêm được dữ liệu
                MessageBox.Show("Không thêm được dữ liệu. Vui lòng thử lại.");
            }
        }

        private void insertSemester(string semesterName, int coefficient)
        {
            insertSemesterBLL = new SemesterBLL();
            bool isInserted = insertSemesterBLL.insertSemesters(semesterName, coefficient);

            if (isInserted)
            {
                MessageBox.Show("Thêm thành công!");
                textBox15.Clear();
                textBox14.Clear();
                textBox8.Clear();
                // Lấy dữ liệu từ cơ sở dữ liệu và gán cho dataGridView1
                SemesterTab();
            }
            else
            {
                // Xử lý khi không thêm được dữ liệu
                MessageBox.Show("Không thêm được dữ liệu. Vui lòng thử lại.");
            }
        }

        private void insertTypeofPoint(string pointName, int coefficient)
        {
            insertTypeOfPointBLL = new TypeOfPointBLL();
            bool isInserted = insertTypeOfPointBLL.insertTypeofPointBLL(pointName, coefficient);

            if (isInserted)
            {
                MessageBox.Show("Thêm thành công!");
                textBox18.Clear();
                textBox17.Clear();
                textBox9.Clear();
                // Lấy dữ liệu từ cơ sở dữ liệu và gán cho dataGridView1
                TypeOfPointTab();
            }
            else
            {
                // Xử lý khi không thêm được dữ liệu
                MessageBox.Show("Không thêm được dữ liệu. Vui lòng thử lại.");
            }
        }

        public void insertSubject(string subjectName)
        {
            insertSubjectBLL = new SubjectBLL();
            bool isInserted = insertSubjectBLL.insertSubjects(subjectName);

            if (isInserted)
            {
                MessageBox.Show("Thêm thành công!");
                textBox21.Clear();
                textBox20.Clear();
                SubjectTab();
            }
            else
            {
                MessageBox.Show("Không thêm được dữ liệu. Vui lòng thử lại.");
            }
        }
        private void updateConduct(string conductName, int upperLimit, int lowerLimit, int ID)
        {

            updateConductBLL = new ConductBLL();
            bool isUpdated = updateConductBLL.updateConducts(conductName, upperLimit, lowerLimit, ID);

            if (isUpdated)
            {
                MessageBox.Show("Sửa thành công!");
                textBox4.Clear();
                textBox3.Clear();
                textBox1.Clear();
                conductTab();
            }
            else
            {
                // Xử lý khi không thêm được dữ liệu
                MessageBox.Show("Không sửa được dữ liệu. Vui lòng thử lại.");
            }
        }

        private void updateCapacity(int ID, string conductName, float upperLimit, float lowerLimit, float paraPoint)
        {

            updateCapacityBLL = new CapacityManager();
            bool isUpdated = updateCapacityBLL.updateCapacities(ID, conductName, upperLimit, lowerLimit, paraPoint);

            if (isUpdated)
            {
                MessageBox.Show("Sửa thành công!");
                textBox11.Clear();
                textBox10.Clear();
                textBox7.Clear();
                textBox6.Clear();
                textBox12.Clear();
                capacityTab();
            }
            else
            {
                // Xử lý khi không thêm được dữ liệu
                MessageBox.Show("Không sửa được dữ liệu. Vui lòng thử lại.");
            }
        }

        private void updateSemester(string semesterName, int coefficient, int ID)
        {

            updateSemesterBLL = new SemesterBLL();
            bool isUpdated = updateSemesterBLL.updateSemesters(semesterName, coefficient, ID);

            if (isUpdated)
            {
                MessageBox.Show("Sửa thành công!");
                textBox15.Clear();
                textBox14.Clear();
                textBox8.Clear();
                SemesterTab();
            }
            else
            {
                // Xử lý khi không thêm được dữ liệu
                MessageBox.Show("Không sửa được dữ liệu. Vui lòng thử lại.");
            }
        }

        private void updateTypeofPoint(int ID, string pointName, int coefficient)
        {

            updateTypeOfPointBLL = new TypeOfPointBLL();
            bool isUpdated = updateTypeOfPointBLL.updateTypeofPointBLL(ID, pointName, coefficient);

            if (isUpdated)
            {
                MessageBox.Show("Sửa thành công!");
                textBox18.Clear();
                textBox17.Clear();
                textBox9.Clear();
                // Lấy dữ liệu từ cơ sở dữ liệu và gán cho dataGridView1
                TypeOfPointTab();
            }
            else
            {
                // Xử lý khi không thêm được dữ liệu
                MessageBox.Show("Không sửa được dữ liệu. Vui lòng thử lại.");
            }
        }

        private void updateSubject(int ID, string subjectName)
        {
            updateSubjectBLL = new SubjectBLL();
            bool isUpdated = updateSubjectBLL.updateSubjects(ID, subjectName);

            if (isUpdated)
            {

                MessageBox.Show("Sửa thành công!");
                textBox20.Clear();
                textBox21.Clear();
                SubjectTab();
            }
            else
            {
                MessageBox.Show("Không sửa được dữ liệu. Vui lòng thử lại!");
            }
        }

        private void deleteConduct(string conductName)
        {
            // Hiển thị hộp thoại xác nhận xóa
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hạnh kiểm này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    deleteConductBLL = new ConductBLL();
                    bool isDeleted = deleteConductBLL.deleteConducts(conductName);

                    if (isDeleted)
                    {
                        MessageBox.Show("Xóa thành công!");
                        textBox4.Clear();
                        textBox3.Clear();
                        textBox1.Clear();
                        // Lấy dữ liệu từ cơ sở dữ liệu và gán cho dataGridView1
                        conductTab();
                    }
                    else
                    {
                        // Xử lý khi không xóa được dữ liệu
                        MessageBox.Show("Không xóa được dữ liệu. Vui lòng thử lại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
            }
        }

        private void deleteCapacity(string capacityName)
        {
            // Hiển thị hộp thoại xác nhận xóa
            if (dataGridView3.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa học lực này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    deleteCapacityBLL = new CapacityManager();
                    bool isDeleted = deleteCapacityBLL.deleteCapacities(capacityName);

                    if (isDeleted)
                    {
                        MessageBox.Show("Xóa thành công!");
                        textBox11.Clear();
                        textBox10.Clear();
                        textBox7.Clear();
                        textBox6.Clear();
                        textBox12.Clear();
                        capacityTab();
                    }
                    else
                    {
                        MessageBox.Show("Không xóa được dữ liệu. Vui lòng thử lại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
            }
        }

        private void deleteSemester(string semesterName)
        {
            // Hiển thị hộp thoại xác nhận xóa
            if (dataGridView4.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa học kỳ này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    deleteSemesterBLL = new SemesterBLL();
                    bool isDeleted = deleteSemesterBLL.deleteSemesters(semesterName);

                    if (isDeleted)
                    {
                        MessageBox.Show("Xóa thành công!");
                        textBox15.Clear();
                        textBox14.Clear();
                        textBox8.Clear();
                        SemesterTab();
                    }
                    else
                    {
                        // Xử lý khi không thêm được dữ liệu
                        MessageBox.Show("Không xóa được dữ liệu. Vui lòng thử lại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
            }
        }

        private void deleteTypeofPoint(string pointName)
        {
            // Hiển thị hộp thoại xác nhận xóa
            if (dataGridView5.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa loại điểm này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    deleteTypeOfPointBLL = new TypeOfPointBLL();
                    bool isDeleted = deleteTypeOfPointBLL.deleteTypeofPointBLL(pointName);

                    if (isDeleted)
                    {
                        MessageBox.Show("Xóa thành công!");
                        textBox18.Clear();
                        textBox17.Clear();
                        textBox9.Clear();
                        TypeOfPointTab();
                    }
                    else
                    {
                        // Xử lý khi không thêm được dữ liệu
                        MessageBox.Show("Không xóa được dữ liệu. Vui lòng thử lại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
            }
        }

        private void deleteSubject(string subjectName)
        {
            // Hiển thị hộp thoại xác nhận xóa
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    deleteSubjectBLL = new SubjectBLL();
                    bool isDeleted = deleteSubjectBLL.deleteSubjects(subjectName);

                    if (isDeleted)
                    {
                        MessageBox.Show("Xóa thành công!");
                        textBox21.Clear();
                        textBox20.Clear();
                        SubjectTab();
                    }
                    else
                    {
                        // Xử lý khi không thêm được dữ liệu
                        MessageBox.Show("Không xóa được dữ liệu. Vui lòng thử lại.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
            }
        }

        private bool IsValidName(string input)
        {
            //"^[\\p{L}\\s]+$" dùng để kiểm tra xem tên hạnh kiểm chỉ chứa chữ cái và khoảng trắng
            Regex regex = new Regex("^[\\p{L}\\s]+$");
            return regex.IsMatch(input);
        }

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

        private void button10_Click(object sender, EventArgs e)
        {
            string conductName = textBox4.Text;
            int upperLimit;
            int lowerLimit;

            if (string.IsNullOrEmpty(conductName))
            {
                MessageBox.Show("Vui lòng nhập tên hạnh kiểm.");
                return;
            }

            if (!IsValidName(conductName))
            {
                MessageBox.Show("Tên hạnh kiểm chỉ được nhập chữ. Vui lòng nhập lại.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out upperLimit) ||
                !int.TryParse(textBox1.Text, out lowerLimit))
            {
                MessageBox.Show("Vui lòng nhập số nguyên cho điểm cận trên và điểm cận dưới.");
                return;
            }

            if (upperLimit < 0 || upperLimit > 100 || lowerLimit < 0 || lowerLimit > 100)
            {
                MessageBox.Show("Điểm cận trên và điểm cận dưới phải nằm trong khoảng từ 0 đến 100.");
                return;
            }

            if (lowerLimit >= upperLimit)
            {
                MessageBox.Show("Điểm cận dưới phải nhỏ hơn điểm cận trên.");
                return;
            }

            if ((upperLimit - lowerLimit) != 25)
            {
                MessageBox.Show("Điểm cận trên và điểm cận dưới phải cách nhau 25 điểm.");
                return;
            }

            if (conductBLL.checkInsertConductName(conductName))
            {
                MessageBox.Show("Tên hạnh kiểm đã tồn tại. Vui lòng nhập lại.");
                return;
            }
            insertConduct(conductName, upperLimit, lowerLimit);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string conductName = textBox4.Text;
            int upperLimit;
            int lowerLimit;
            int ID;

            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                ID = Convert.ToInt32(selectedRow.Cells["Column1"].Value);
            }

            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            if (string.IsNullOrEmpty(conductName))
            {
                MessageBox.Show("Vui lòng nhập tên hạnh kiểm.");
                return;
            }

            if (!IsValidName(conductName))
            {
                MessageBox.Show("Tên hạnh kiểm chỉ được nhập chữ. Vui lòng nhập lại.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out upperLimit) ||
                !int.TryParse(textBox1.Text, out lowerLimit))
            {
                MessageBox.Show("Vui lòng nhập số nguyên cho điểm cận trên và điểm cận dưới.");
                return;
            }

            if (upperLimit < 0 || upperLimit > 100 || lowerLimit < 0 || lowerLimit > 100)
            {
                MessageBox.Show("Điểm cận trên và điểm cận dưới phải nằm trong khoảng từ 0 đến 100.");
                return;
            }

            if (lowerLimit >= upperLimit)
            {
                MessageBox.Show("Điểm cận dưới phải nhỏ hơn điểm cận trên.");
                return;
            }

            if ((upperLimit - lowerLimit) != 25)
            {
                MessageBox.Show("Điểm cận trên và điểm cận dưới phải cách nhau 25 điểm.");
                return;
            }

            if (conductBLL.checkUpdateConductName(conductName, ID))
            {
                MessageBox.Show("Tên hạnh kiểm đã tồn tại. Vui lòng nhập lại.");
                return;
            }

            updateConduct(conductName, upperLimit, lowerLimit, ID);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string conductName = textBox4.Text;
            deleteConduct(conductName);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                textBox4.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox1.Text = row.Cells[3].Value.ToString();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string capacityName = textBox10.Text;
            float upperLimit;
            float lowerLimit;
            float paraPoint;


            if (string.IsNullOrEmpty(capacityName))
            {
                MessageBox.Show("Vui lòng nhập tên học lực.");
                return;
            }
            if (capacityBLL.checkInsertCapacityName(capacityName))
            {
                MessageBox.Show("Tên học lực đã tồn tại. Vui lòng nhập lại.");
                return;
            }

            if (!IsValidName(capacityName))
            {
                MessageBox.Show("Tên học lực chỉ được nhập chữ. Vui lòng nhập lại.");
                return;
            }

            if (!float.TryParse(textBox7.Text, out upperLimit) ||
                !float.TryParse(textBox6.Text, out lowerLimit) ||
                !float.TryParse(textBox12.Text, out paraPoint))
            {
                MessageBox.Show("Vui lòng nhập Số nguyên hoặc Số thập phân " +
                    "cho Điểm cận trên, Điểm cận dưới và Điểm khống chế!.");
                return;
            }

            if (upperLimit < 0 || upperLimit > 10 ||
                lowerLimit < 0 || lowerLimit > 10 ||
                paraPoint < 0 || paraPoint > 10)
            {
                MessageBox.Show("Điểm cận trên, Điểm cận dưới và Điểm khống chế " +
                    "phải nằm trong khoảng từ 0 đến 10.");
                return;
            }

            if (lowerLimit == 0 && paraPoint == 0)
            {
                DialogResult result = MessageBox.Show("Cả hai Điểm cận dưới và Điểm khống chế đều bằng 0. Bạn có muốn thêm dữ liệu?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    insertCapacity(capacityName, upperLimit, lowerLimit, paraPoint);
                }
            }
            else if (paraPoint >= lowerLimit || paraPoint >= upperLimit || lowerLimit >= upperLimit)
            {
                MessageBox.Show("Điểm khống chế phải nhỏ hơn Điểm cận dưới và Điểm cận dưới phải nhỏ hơn Điểm cận trên.");
            }
            else
            {
                insertCapacity(capacityName, upperLimit, lowerLimit, paraPoint);
            }

/*            if (paraPoint >= lowerLimit ||
                paraPoint >= upperLimit ||
                lowerLimit >= upperLimit)
            {
                MessageBox.Show("Điểm khống chế phải nhỏ hơn Điểm cận dưới " +
                    "và Điểm cận dưới phải nhỏ hơn điểm cận trên.");
                return;
            }
            insertCapacity(capacityName, upperLimit, lowerLimit, paraPoint);*/
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string capacityName = textBox10.Text;
            float upperLimit;
            float lowerLimit;
            float paraPoint;
            int ID;

            if (dataGridView3.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView3.SelectedRows[0];
                ID = Convert.ToInt32(selectedRow.Cells["Mã học lực"].Value);
            }

            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            if (string.IsNullOrEmpty(capacityName))
            {
                MessageBox.Show("Vui lòng nhập tên học lực.");
                return;
            }
            if (capacityBLL.checkUpdateCapacityName(ID, capacityName))
            {
                MessageBox.Show("Tên học lực đã tồn tại. Vui lòng nhập lại.");
                return;
            }

            if (!IsValidName(capacityName))
            {
                MessageBox.Show("Tên học lực chỉ được nhập chữ. Vui lòng nhập lại.");
                return;
            }

            if (!float.TryParse(textBox7.Text, out upperLimit) ||
                !float.TryParse(textBox6.Text, out lowerLimit) ||
                !float.TryParse(textBox12.Text, out paraPoint))
            {
                MessageBox.Show("Vui lòng nhập số nguyên hoặc số thập phân " +
                    "cho điểm cận trên, điểm cận dưới và điểm khống chế!.");
                return;
            }

            if (upperLimit < 0 || upperLimit > 10 ||
                lowerLimit < 0 || lowerLimit > 10 ||
                paraPoint < 0 || paraPoint > 10)
            {
                MessageBox.Show("Điểm cận trên, Điểm cận dưới và Điểm khống chế " +
                    "phải nằm trong khoảng từ 0 đến 10.");
                return;
            }

            if (lowerLimit == 0 && paraPoint == 0)
            {
                DialogResult result = MessageBox.Show("Cả hai Điểm cận dưới và Điểm khống chế đều bằng 0. Bạn có muốn thêm dữ liệu?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    updateCapacity(ID, capacityName, upperLimit, lowerLimit, paraPoint);
                }
            }
            else if (paraPoint >= lowerLimit || paraPoint >= upperLimit || lowerLimit >= upperLimit)
            {
                MessageBox.Show("Điểm khống chế phải nhỏ hơn Điểm cận dưới và Điểm cận dưới phải nhỏ hơn Điểm cận trên.");
            }
            else
            {
                updateCapacity(ID, capacityName, upperLimit, lowerLimit, paraPoint);
            }

            /*if (paraPoint >= lowerLimit ||
                paraPoint >= upperLimit ||
                lowerLimit >= upperLimit)
            {
                MessageBox.Show("Điểm khống chế phải nhỏ hơn Điểm cận dưới " +
                    "và Điểm cận dưới phải nhỏ hơn điểm cận trên.");
                return;
            }
            updateCapacity(ID, capacityName, upperLimit, lowerLimit, paraPoint);*/
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string capacityName = textBox10.Text;
            deleteCapacity(capacityName);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string semesterName = textBox14.Text;
            int coefficient;

            if (string.IsNullOrEmpty(semesterName))
            {
                MessageBox.Show("Vui lòng nhập tên học kỳ.");
                return;
            }

            Regex regex = new Regex(@"^(H|h)ọc kỳ [1-3]$");
            if (!regex.IsMatch(semesterName))
            {
                MessageBox.Show("Tên học kỳ không đúng định dạng. Vui lòng nhập lại theo định dạng 'Học kỳ x' (với x là số từ 1 đến 3).");
                return;
            }

            if (!int.TryParse(textBox8.Text, out coefficient))
            {
                MessageBox.Show("Vui lòng nhập số nguyên cho hệ số học kỳ.");
                return;
            }

            if (coefficient < 0 || coefficient > 3)
            {
                MessageBox.Show("Hệ số học kỳ nằm trong khoảng từ 1 đến 3.");
                return;
            }

            if (semesterBLL.checkInsertSemesterName(semesterName))
            {
                MessageBox.Show("Tên học kỳ đã tồn tại. Vui lòng nhập lại.");
                return;
            }

            insertSemester(semesterName, coefficient);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string semesterName = textBox14.Text;
            int coefficient;
            int ID;

            if (dataGridView4.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];
                ID = Convert.ToInt32(selectedRow.Cells["Column5"].Value);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            if (string.IsNullOrEmpty(semesterName))
            {
                MessageBox.Show("Vui lòng nhập tên học kỳ.");
                return;
            }

            Regex regex = new Regex(@"^(H|h)ọc kỳ [1-3]$");
            if (!regex.IsMatch(semesterName))
            {
                MessageBox.Show("Tên học kỳ không đúng định dạng. Vui lòng nhập lại theo định dạng 'Học kỳ x' (với x là số từ 1 đến 3).");
                return;
            }

            if (!int.TryParse(textBox8.Text, out coefficient))
            {
                MessageBox.Show("Vui lòng nhập số nguyên cho hệ số học kỳ.");
                return;
            }

            if (coefficient < 0 || coefficient > 3)
            {
                MessageBox.Show("Hệ số học kỳ nằm trong khoảng từ 1 đến 3.");
                return;
            }

            if (semesterBLL.checkUpdateSemesterName(ID, semesterName))
            {
                MessageBox.Show("Tên học kỳ đã tồn tại. Vui lòng nhập lại.");
                return;
            }

            updateSemester(semesterName, coefficient, ID);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string semesterName = textBox14.Text;
            deleteSemester(semesterName);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string pointName = textBox17.Text;
            int coefficient;

            if (string.IsNullOrEmpty(pointName))
            {
                MessageBox.Show("Vui lòng nhập tên học kỳ.");
                
                return;
            }

            if (!int.TryParse(textBox9.Text, out coefficient))
            {
                MessageBox.Show("Vui lòng nhập số nguyên cho hệ số học kỳ.");
                return;
            }

            if (coefficient < 0 || coefficient > 3)
            {
                MessageBox.Show("Hệ số học kỳ nằm trong khoảng từ 1 đến 3.");
                return;
            }

            if (typeofpointBLL.checkInsertTypeofPointName(pointName))
            {
                MessageBox.Show("Tên loại điểm đã tồn tại. Vui lòng nhập lại.");
                return;
            }

            if (!IsValidName(pointName))
            {
                MessageBox.Show("Tên loại điểm chỉ được nhập chữ. Vui lòng nhập lại.");
                return;
            }
            insertTypeofPoint(pointName, coefficient);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string pointName = textBox17.Text;
            int coefficient;
            int ID;

            if (dataGridView5.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView5.SelectedRows[0];
                ID = Convert.ToInt32(selectedRow.Cells["Column8"].Value);
            }

            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            if (string.IsNullOrEmpty(pointName))
            {
                MessageBox.Show("Vui lòng nhập tên học kỳ.");
                return;
            }

            if (!int.TryParse(textBox9.Text, out coefficient))
            {
                MessageBox.Show("Vui lòng nhập số nguyên cho hệ số học kỳ.");
                return;
            }

            if (coefficient < 0 || coefficient > 3)
            {
                MessageBox.Show("Hệ số học kỳ nằm trong khoảng từ 1 đến 3.");
                return;
            }

            if (typeofpointBLL.checkUpdateTypeofPointName(pointName, ID))
            {
                MessageBox.Show("Tên loại điểm đã tồn tại. Vui lòng nhập lại.");
                return;
            }

            if (!IsValidName(pointName))
            {
                MessageBox.Show("Tên loại điểm chỉ được nhập chữ. Vui lòng nhập lại.");
                return;
            }

            updateTypeofPoint(ID, pointName, coefficient);
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[e.RowIndex];
                textBox15.Text = row.Cells[0].Value.ToString();
                textBox14.Text = row.Cells[1].Value.ToString();
                textBox8.Text = row.Cells[2].Value.ToString();
            }
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView5.Rows[e.RowIndex];
                textBox18.Text = row.Cells[0].Value.ToString();
                textBox17.Text = row.Cells[1].Value.ToString();
                textBox9.Text = row.Cells[2].Value.ToString();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            FindSemesterData();
            textBox13.Clear();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            FindTypeOfPoint();
            textBox16.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox21.Text = row.Cells[0].Value.ToString();
                textBox20.Text = row.Cells[1].Value.ToString();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string subjectName = textBox20.Text;

            if (string.IsNullOrEmpty(subjectName))
            {
                MessageBox.Show("Vui lòng nhập tên môn học.");
                return;
            }

            if (!IsValidName(subjectName))
            {
                MessageBox.Show("Tên môn học chỉ được nhập chữ. Vui lòng nhập lại.");
                return;
            }

            if (subjectBLL.checkInsertSubjectName(subjectName))
            {
                MessageBox.Show("Tên môn học đã tồn tại. Vui lòng nhập lại.");
                return;
            }
            insertSubject(subjectName);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string subjectName = textBox20.Text;
            int ID;

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                ID = Convert.ToInt32(selectedRow.Cells["Column11"].Value);
            }

            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa.");
                return;
            }

            if (string.IsNullOrEmpty(subjectName))
            {
                MessageBox.Show("Vui lòng nhập tên môn học.");
                return;
            }

            if (!IsValidName(subjectName))
            {
                MessageBox.Show("Tên môn học chỉ được nhập chữ. Vui lòng nhập lại.");
                return;
            }

            if (subjectBLL.checkUpdateSubjectName(ID, subjectName))
            {
                MessageBox.Show("Tên môn học đã tồn tại. Vui lòng nhập lại.");
                return;
            }
            updateSubject(ID, subjectName);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FindSubject();
            textBox22.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string subjectName = textBox20.Text;
            deleteSubject(subjectName);
        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            string pointName = textBox17.Text;
            deleteTypeofPoint(pointName);
        }
    }
}
