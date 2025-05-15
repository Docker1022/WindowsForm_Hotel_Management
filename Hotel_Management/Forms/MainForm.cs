using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Hotel_Management.UserControls;

namespace Hotel_Management
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            // Shadow form
            Guna2ShadowForm shadow = new Guna2ShadowForm();
            shadow.SetShadowForm(this);

            // Fade-in animation
            Guna2AnimateWindow animate = new Guna2AnimateWindow();
            animate.TargetForm = this;
            animate.AnimationType = Guna2AnimateWindow.AnimateWindowType.AW_BLEND;
            animate.Interval = 1000;

            this.FormBorderStyle = FormBorderStyle.None;

            // Kích thước 80% màn hình
            var screen = Screen.PrimaryScreen.WorkingArea;
            this.Size = new Size((int)(screen.Width * 0.8), (int)(screen.Height * 0.8));
        }

        private void ShowUserControl(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;

            // Áp dụng hiệu ứng chuyển đổi
            guna2Transition1.Show(uc);

            panelMain.Controls.Add(uc);
            uc.BringToFront();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Đóng MainForm
                this.Close();
                // Mở lại LoginForm
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Đóng MainForm
                this.Close();
                // Mở lại LoginForm
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ucDanhSachPhong1_Load(object sender, EventArgs e)
        {

        }

        private void ucDanhSachPhong1_Load_1(object sender, EventArgs e)
        {

        }

        private void ucDanhSachPhong1_Load_2(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Đóng MainForm
                this.Close();
                // Mở lại LoginForm
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void btnBangDieuKhien_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ucBangDieuKhien());
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnDanhSachDatPhong_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ucDanhSachPhong());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ucKhachHang());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ucNhanVien());
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ucHoaDon());
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ucBaoCaoDoanhThu());
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ucTaiKhoan());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ShowUserControl(new ucThongKe());
        }
    }
}
