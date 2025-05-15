using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management
{
    public partial class LoginForm : Form
    {
        private Guna2TextBox txtUsername;
        private Guna2TextBox txtPassword;
        private Guna2Button btnLogin;
        private Guna2CheckBox chkRemember;
        private Guna2Panel glassPanel;
        private Guna2Panel glassPanel2;
        private Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
        public LoginForm()
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
            animate.Interval = 2000;

            this.FormBorderStyle = FormBorderStyle.None;
            // this.StartPosition = FormStartPosition.CenterScreen;

            // Kích thước 80% màn hình
            var screen = Screen.PrimaryScreen.WorkingArea;
            this.Size = new Size((int)(screen.Width * 0.8), (int)(screen.Height * 0.8));

            // Hình nền
            Image original = Image.FromFile("D:\\DOWNLOADS\\LT Windows\\images\\login.jpg");
            Image resized = ResizeImage(original, this.Size.Width, this.Size.Height);
            this.BackgroundImage = resized;
            this.BackgroundImageLayout = ImageLayout.None;

            // Panel trong suốt để chứa khối login
            var loginPanel = new Guna2Panel
            {
                Size = new Size(420, 360),
                FillColor = Color.FromArgb(90, 255, 255, 255),
                BackColor = Color.Transparent,
                BorderRadius = 20,
                Location = new Point((this.Width - 420) / 2, (this.Height - 360) / 2)
            };
            this.Controls.Add(loginPanel);

            // Tiêu đề
            var lblTitle = new Label
            {
                Text = "Welcome to Luxury Hotel",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point((this.Width - 340) / 2, 40),
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblTitle);

            // Username textbox
            var txtUser = new Guna2TextBox
            {
                PlaceholderText = "Username",
                Size = new Size(300, 35),
                Location = new Point(35, 40),
                BorderRadius = 10,
                Font = new Font("Segoe UI", 11),
                IconLeft = Image.FromFile("D:\\DOWNLOADS\\LT Windows\\images\\user.png")
            };
            loginPanel.Controls.Add(txtUser);
            // Password textbox
            var txtPass = new Guna2TextBox
            {
                PlaceholderText = "Password",
                Size = new Size(300, 35),
                Location = new Point(35, 100),
                BorderRadius = 10,
                Font = new Font("Segoe UI", 11),
                PasswordChar = '●',
                IconLeft = Image.FromFile("D:\\DOWNLOADS\\LT Windows\\images\\password.png")
            };
            loginPanel.Controls.Add(txtPass);

            // Checkbox Show password
            var cbShowPass = new Guna2CheckBox
            {
                Text = "Show Password",
                Location = new Point(40, 190),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true
            };
            cbShowPass.CheckedChanged += (s, e) =>
            {
                txtPass.PasswordChar = cbShowPass.Checked ? '\0' : '●';
            };
            loginPanel.Controls.Add(cbShowPass);

            // Forgot password (label)
            var lblForgot = new LinkLabel
            {
                Text = "Forgot Password?",
                Location = new Point(250, 190),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                LinkColor = Color.DodgerBlue,
                AutoSize = true
            };
            loginPanel.Controls.Add(lblForgot);

            // Button login
            var btnLogin = new Guna2Button
            {
                Text = "Login",
                Size = new Size(130, 40),
                Location = new Point(60, 250),
                BorderRadius = 12,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FillColor = Color.FromArgb(8, 217, 214) // Màu xanh dương đậm sang trọng
            };
            btnLogin.Click += (s, e) =>
            {
                string username = txtUser.Text.Trim();
                string password = txtPass.Text.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "SELECT COUNT(*) FROM TAI_KHOAN WHERE TenDangNhap = @user AND MatKhau = @pass";

                try
                {
                    using (SqlConnection conn = DBConnect.GetConnection())
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Mở MainForm
                            MainForm mainForm = new MainForm();
                            mainForm.Show();

                            this.Hide(); // Ẩn LoginForm
                        }
                        else
                        {
                            MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            loginPanel.Controls.Add(btnLogin);
            // Thiết lập phím Enter sẽ bấm nút Login
            this.AcceptButton = btnLogin;

            // Exit button
            var btnExit = new Guna2Button
            {
                Text = "Exit",
                Size = new Size(130, 40),
                Location = new Point(230, 250),
                BorderRadius = 12,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FillColor = Color.FromArgb(255, 46, 99) // Màu đỏ rượu vang
            };
            btnExit.Click += (s, e) =>
            {
                var confirm = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };
            loginPanel.Controls.Add(btnExit);
            // Cho phép nhấn ESC để thoát
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    btnExit.PerformClick();
                }
            };

        }

        private Image ResizeImage(Image originalImage, int width, int height)
        {
            Bitmap resized = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, width, height);
            }
            return resized;
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
    }
}
