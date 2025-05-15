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
    public partial class ThemNhanVien : Form
    {
        public ThemNhanVien()
        {
            InitializeComponent();
        }

        public void AddNhanVien(NhanVienDTO nv)
        {
            string query = @"INSERT INTO NHAN_VIEN (MaNV, Ho, Ten, NgaySinh, GioiTinh, CCCD, SDT, Email, 
                                                DiaChiThuongTru, Phuong, Quan, ThanhPho, Tinh, 
                                                NgayBatDau, ChucVu, CaLamViec, Luong, TrangThai)
                         VALUES (@MaNV, @Ho, @Ten, @NgaySinh, @GioiTinh, @CCCD, @SDT, @Email, 
                                 @DiaChiThuongTru, @Phuong, @Quan, @ThanhPho, @Tinh, 
                                 @NgayBatDau, @ChucVu, @CaLamViec, @Luong, @TrangThai)";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
                        cmd.Parameters.AddWithValue("@Ho", nv.Ho);
                        cmd.Parameters.AddWithValue("@Ten", nv.Ten);
                        cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                        cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                        cmd.Parameters.AddWithValue("@CCCD", nv.CCCD);
                        cmd.Parameters.AddWithValue("@SDT", nv.SDT);
                        cmd.Parameters.AddWithValue("@Email", nv.Email);
                        cmd.Parameters.AddWithValue("@DiaChiThuongTru", nv.DiaChiThuongTru);
                        cmd.Parameters.AddWithValue("@Phuong", nv.Phuong);
                        cmd.Parameters.AddWithValue("@Quan", nv.Quan);
                        cmd.Parameters.AddWithValue("@ThanhPho", nv.ThanhPho);
                        cmd.Parameters.AddWithValue("@Tinh", nv.Tinh);
                        cmd.Parameters.AddWithValue("@NgayBatDau", nv.NgayBatDau);
                        cmd.Parameters.AddWithValue("@ChucVu", nv.ChucVu);
                        cmd.Parameters.AddWithValue("@CaLamViec", nv.CaLamViec);
                        cmd.Parameters.AddWithValue("@Luong", nv.Luong);
                        cmd.Parameters.AddWithValue("@TrangThai", nv.TrangThai ?? "Đang làm việc");

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm nhân viên: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel32_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel27_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel20_Click(object sender, EventArgs e)
        {

        }

        private void txtTang_TextChanged(object sender, EventArgs e)
        {

        }

        private void ThemNhanVien_Load(object sender, EventArgs e)
        {

        }

        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
                    throw new Exception("Vui lòng nhập Mã nhân viên!");
                if (string.IsNullOrWhiteSpace(txtHoNhanVien.Text))
                    throw new Exception("Vui lòng nhập Họ!");
                if (string.IsNullOrWhiteSpace(txtTenNhanVien.Text))
                    throw new Exception("Vui lòng nhập Tên!");
                if (dtpNgaySinhNhanVien.Value > DateTime.Now)
                    throw new Exception("Ngày sinh không hợp lệ!");
                if (cboGioiTinhNhanVien.SelectedItem == null)
                    throw new Exception("Vui lòng chọn Giới tính!");
                if (string.IsNullOrWhiteSpace(txtCCCD_NhanVien.Text))
                    throw new Exception("Vui lòng nhập CCCD!");
                if (string.IsNullOrWhiteSpace(txtSDT_NhanVien.Text))
                    throw new Exception("Vui lòng nhập Số điện thoại!");
                if (string.IsNullOrWhiteSpace(txtDiaChiThuongTruNhanVien.Text))
                    throw new Exception("Vui lòng nhập Địa chỉ thường trú!");
                if (string.IsNullOrWhiteSpace(txtPhuongNhanVien.Text))
                    throw new Exception("Vui lòng nhập Phường!");
                if (string.IsNullOrWhiteSpace(txtQuanNhanVien.Text))
                    throw new Exception("Vui lòng nhập Quận!");
                if (string.IsNullOrWhiteSpace(txtThanhPhoNhanVien.Text))
                    throw new Exception("Vui lòng nhập Thành phố!");
                // Chỉ yêu cầu chọn Tỉnh nếu ThanhPho không phải là TP.HCM
                if (txtThanhPhoNhanVien.Text.Trim().ToUpper() != "TP.HCM" && cboTinhNhanVien.SelectedItem == null)
                    throw new Exception("Vui lòng chọn Tỉnh!");
                if (dtpNgayBatDauNhanVien.Value > DateTime.Now)
                    throw new Exception("Ngày bắt đầu không hợp lệ!");
                if (cboChucVuNhanVien.SelectedItem == null)
                    throw new Exception("Vui lòng chọn Chức vụ!");
                if (cboCaLamViecNhanVien.SelectedItem == null)
                    throw new Exception("Vui lòng chọn Ca làm việc!");
                if (!decimal.TryParse(txtLuongNhanVien.Text, out decimal luong) || luong <= 0)
                    throw new Exception("Lương không hợp lệ! Vui lòng nhập số lớn hơn 0.");

                // Tạo đối tượng NhanVienDTO nếu tất cả thông tin hợp lệ
                NhanVienDTO newNV = new NhanVienDTO
                {
                    MaNV = txtMaNhanVien.Text,
                    Ho = txtHoNhanVien.Text,
                    Ten = txtTenNhanVien.Text,
                    NgaySinh = dtpNgaySinhNhanVien.Value,
                    GioiTinh = cboGioiTinhNhanVien.SelectedItem.ToString(),
                    CCCD = txtCCCD_NhanVien.Text,
                    SDT = txtSDT_NhanVien.Text,
                    Email = txtEmail_NhanVien.Text,
                    DiaChiThuongTru = txtDiaChiThuongTruNhanVien.Text,
                    Phuong = txtPhuongNhanVien.Text,
                    Quan = txtQuanNhanVien.Text,
                    ThanhPho = txtThanhPhoNhanVien.Text,
                    Tinh = cboTinhNhanVien.SelectedItem.ToString(),
                    NgayBatDau = dtpNgayBatDauNhanVien.Value,
                    ChucVu = cboChucVuNhanVien.SelectedItem.ToString(),
                    CaLamViec = int.Parse(cboCaLamViecNhanVien.SelectedItem.ToString()),
                    Luong = luong,
                    TrangThai = "Đang làm việc" // Mặc định trạng thái là "Đang làm việc"
                };

                AddNhanVien(newNV);
                MessageBox.Show("Nhân viên đã được thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear form fields after successful insertion
                txtMaNhanVien.Clear();
                txtHoNhanVien.Clear();
                txtTenNhanVien.Clear();
                dtpNgaySinhNhanVien.Value = DateTime.Now;
                cboGioiTinhNhanVien.SelectedIndex = -1;
                txtCCCD_NhanVien.Clear();
                txtSDT_NhanVien.Clear();
                txtEmail_NhanVien.Clear();
                txtDiaChiThuongTruNhanVien.Clear();
                txtPhuongNhanVien.Clear();
                txtQuanNhanVien.Clear();
                txtThanhPhoNhanVien.Clear();
                cboTinhNhanVien.SelectedIndex = -1;
                dtpNgayBatDauNhanVien.Value = DateTime.Now;
                cboChucVuNhanVien.SelectedIndex = -1;
                cboCaLamViecNhanVien.SelectedIndex = -1;
                txtLuongNhanVien.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_ThemNhanVien_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
