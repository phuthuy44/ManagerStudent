using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ManagerStudent.GUI
{
    public partial class MainForm : Form
    {
        private HocSinhForm hocSinhForm;
        public MainForm()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(1847, 936);//kich thuoc co dinh
            this.FormBorderStyle = FormBorderStyle.FixedDialog; //vo hieu hoa kha nang thay doi kich thuoc
            this.MaximizeBox = false;//vo hieu hoa nut maximize
            this.MinimizeBox = false;//vo hieu hoa nut minimum
            panelHocSinh.Click += panelHocSinh_Click;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {
            panelHocSinh.BackColor = SystemColors.ButtonHighlight;
        }

        private void MainForm_MouseLeave(object sender, EventArgs e)
        {
            // panelHocSinh.BackColor = Color.FromArgb(128, 255, 128);
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {

        }

        private void panelGiaoVien_Click(object sender, EventArgs e)
        {

        }

        private void panel2_MouseHover_1(object sender, EventArgs e)
        {
            panelGiaoVien.BackColor = SystemColors.ButtonHighlight;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            panelGiaoVien.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_MouseHover(object sender, EventArgs e)
        {
            panelLopKhoi.BackColor = SystemColors.ButtonHighlight;
        }

        private void panelLopKhoi_MouseLeave(object sender, EventArgs e)
        {
            panelLopKhoi.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void panelDiem_MouseHover(object sender, EventArgs e)
        {
            panelDiem.BackColor = SystemColors.ButtonHighlight;
        }

        private void panelDiem_MouseLeave(object sender, EventArgs e)
        {
            panelDiem.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void panelKetQua_MouseLeave(object sender, EventArgs e)
        {
            panelKetQua.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void panelKetQua_MouseHover(object sender, EventArgs e)
        {
            panelKetQua.BackColor = SystemColors.ButtonHighlight;
        }

        private void panelThongKe_MouseHover(object sender, EventArgs e)
        {
            panelThongKe.BackColor = SystemColors.ButtonHighlight;
        }

        private void panelThongKe_MouseLeave(object sender, EventArgs e)
        {
            panelThongKe.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void panelDangXuat_MouseHover(object sender, EventArgs e)
        {
            panelDangXuat.BackColor = SystemColors.ButtonHighlight;
        }

        private void panelDangXuat_MouseLeave(object sender, EventArgs e)
        {
            panelDangXuat.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void panelLopKhoi_MouseHover(object sender, EventArgs e)
        {

        }

        private void panelHocSinh_MouseLeave(object sender, EventArgs e)
        {
           // panelHocSinh.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            panelHocSinh.BackColor = SystemColors.ButtonHighlight;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panelHocSinh.BackColor = Color.FromArgb(128, 255, 128);

        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            panelGiaoVien.BackColor = SystemColors.ButtonHighlight;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panelGiaoVien.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            panelLopKhoi.BackColor = SystemColors.ButtonHighlight;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            panelLopKhoi.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            panelDiem.BackColor = SystemColors.ButtonHighlight;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            panelDiem.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            panelKetQua.BackColor = SystemColors.ButtonHighlight;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            panelKetQua.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            panelThongKe.BackColor = SystemColors.ButtonHighlight;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            panelThongKe.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void panelHocSinh_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panelNguoiDung_MouseHover(object sender, EventArgs e)
        {
            panelNguoiDung.BackColor = SystemColors.ButtonHighlight;
        }

        private void panelNguoiDung_MouseLeave(object sender, EventArgs e)
        {
            panelNguoiDung.BackColor = Color.FromArgb(128, 255, 128);
        }

        private void panelHocSinh_MouseClick(object sender, MouseEventArgs e)
        {
           /* panelHocSinh.BackColor = SystemColors.ButtonHighlight;
            if (hocSinhForm != null)
            {
                hocSinhForm.Dispose();
            }
            // Tạo một instance của StudentForm
            hocSinhForm = new HocSinhForm();
            // Thiết lập thuộc tính Dock của studentForm để lấp đầy panelRight
            hocSinhForm.Dock = DockStyle.Fill;
            // Thêm studentForm vào panelRight
            hocSinhForm.TopLevel = false;
            panelRight.Controls.Clear();
            // panelRight.Controls.Add(hocSinhForm);
            panelRight.Controls.Add(hocSinhForm);
            hocSinhForm.Show();*/
        }

        private void panelHocSinh_Click(object sender, EventArgs e)
        {
            panelHocSinh.BackColor = SystemColors.ButtonHighlight;
            if (hocSinhForm != null)
            {
                hocSinhForm.Dispose();
            }
            // Tạo một instance của StudentForm
            hocSinhForm = new HocSinhForm();
            // Thiết lập thuộc tính Dock của studentForm để lấp đầy panelRight
            hocSinhForm.Dock = DockStyle.Fill;
            // Thêm studentForm vào panelRight
            hocSinhForm.TopLevel = false;
            panelRight.Controls.Clear();
            // panelRight.Controls.Add(hocSinhForm);
            panelRight.Controls.Add(hocSinhForm);
            hocSinhForm.Show();
        }
    }
    }
