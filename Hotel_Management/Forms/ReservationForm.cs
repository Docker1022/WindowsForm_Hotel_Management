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
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

namespace Hotel_Management
{
    public partial class ReservationForm : Form
    {
        private PhongDTO _phongInfo;
        private KhachHangDAO khachHangDAO;
        private DatPhongDAO datPhongDAO;
        public ReservationForm()
        {
            InitializeComponent();
            khachHangDAO = new KhachHangDAO();
            datPhongDAO = new DatPhongDAO();


        }

        public ReservationForm(PhongDTO phongInfo)
        {
            InitializeComponent();
            _phongInfo = phongInfo ?? throw new ArgumentNullException(nameof(phongInfo), "Thông tin phòng không được cung cấp!");
            khachHangDAO = new KhachHangDAO();
            datPhongDAO = new DatPhongDAO();
            UpdatePhongInfo();

        }

        private void UpdatePhongInfo()
        {
            if (_phongInfo != null)
            {
                txtDatPhong_LoaiPhong.Text = _phongInfo.LoaiPhong;
                txtDatPhong_MaPhong.Text = _phongInfo.MaPhong;
                txtDatPhong_Tang.Text = _phongInfo.Tang;
                txtDatPhong_GiaPhong.Text = _phongInfo.GiaPhong.ToString("N0") + " VND";
            }
        }


        private void guna2HtmlLabel30_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel28_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel24_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_ReservationForm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ReservationForm_Load(object sender, EventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel29_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel19_Click(object sender, EventArgs e)
        {

        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtDatPhong_MaKH.Text))
                    throw new Exception("Vui lòng nhập Mã khách hàng!");
                if (string.IsNullOrWhiteSpace(txtDatPhong_HoKH.Text))
                    throw new Exception("Vui lòng nhập Họ!");
                if (string.IsNullOrWhiteSpace(txtDatPhong_TenKH.Text))
                    throw new Exception("Vui lòng nhập Tên!");
                if (dtpDatPhong_NgaySinhKH.Value > DateTime.Now.Date)
                    throw new Exception("Ngày sinh không hợp lệ!");
                if (string.IsNullOrWhiteSpace(txtDatPhong_CCCD_KH.Text))
                    throw new Exception("Vui lòng nhập CCCD!");
                if (string.IsNullOrWhiteSpace(txtDatPhong_SDT_KH.Text))
                    throw new Exception("Vui lòng nhập Số điện thoại!");
                if (cboDatPhong_QuocTichKH.SelectedItem == null)
                    throw new Exception("Vui lòng chọn Quốc tịch!");
                if (dtpDatPhong_CheckIn.Value.Date < DateTime.Now.Date)
                    throw new Exception("Ngày Check-in không được nhỏ hơn ngày hiện tại!");
                if (dtpDatPhong_CheckOut.Value.Date < dtpDatPhong_CheckIn.Value.Date)
                    throw new Exception("Ngày Check-out không được nhỏ hơn ngày Check-in!");


                // Tạo thông tin khách hàng
                KhachHangDTO khachHang = new KhachHangDTO
                {
                    MaKH = txtDatPhong_MaKH.Text,
                    Ho = txtDatPhong_HoKH.Text,
                    Ten = txtDatPhong_TenKH.Text,
                    NgaySinh = dtpDatPhong_NgaySinhKH.Value,
                    CCCD = txtDatPhong_CCCD_KH.Text,
                    SDT = txtDatPhong_SDT_KH.Text,
                    QuocTich = cboDatPhong_QuocTichKH.SelectedItem.ToString()
                };

                // Kiểm tra khách hàng tồn tại
                var existingKhachHang = khachHangDAO.GetAllKhachHang().Find(kh => kh.MaKH == khachHang.MaKH);
                if (existingKhachHang == null)
                {
                    // Thêm khách hàng mới
                    khachHangDAO.InsertKhachHang(khachHang);
                }

                // Tạo thông tin đặt phòng
                string maDatPhong = datPhongDAO.GenerateMaDatPhong();
                string maKhachHang = txtDatPhong_MaKH.Text;
                string maPhong = txtDatPhong_MaPhong.Text;
                DateTime checkIn = dtpDatPhong_CheckIn.Value;
                DateTime checkOut = dtpDatPhong_CheckOut.Value;

                // Gọi phương thức từ DatPhongDAO để đặt phòng
                datPhongDAO.DatPhong(maDatPhong, maKhachHang, maPhong, checkIn, checkOut);

                MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtDatPhong_MaKH.Text = "";
            txtDatPhong_HoKH.Text = "";
            txtDatPhong_TenKH.Text = "";
            dtpDatPhong_NgaySinhKH.Value = DateTime.Now;
            txtDatPhong_CCCD_KH.Text = "";
            txtDatPhong_SDT_KH.Text = "";
            cboDatPhong_QuocTichKH.SelectedIndex = -1;
            dtpDatPhong_CheckIn.Value = DateTime.Now;
            dtpDatPhong_CheckOut.Value = DateTime.Now;
        }

        private void txtDatPhong_HoKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtDatPhong_MaKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel33_Click(object sender, EventArgs e)
        {

        }
    }
}
