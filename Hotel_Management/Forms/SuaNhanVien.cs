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

namespace Hotel_Management
{
    public partial class SuaNhanVien : Form
    {
        private NhanVienDTO nhanVien;
        private NhanVienDAO nhanVienDataAccess;
        public SuaNhanVien()
        {
            InitializeComponent();
        }

        public SuaNhanVien(NhanVienDTO nv)
        {
            InitializeComponent();
            nhanVien = nv;
            nhanVienDataAccess = new NhanVienDAO();
            LoadNhanVienData(); // Điền dữ liệu vào form khi khởi tạo
        }

        private void LoadNhanVienData()
        {
            if (nhanVien != null)
            {
                txtChinhSuaMaNV.Text = nhanVien.MaNV;
                txtChinhSuaHoNV.Text = nhanVien.Ho;
                txtChinhSuaTenNV.Text = nhanVien.Ten;
                dtpChinhSuaNgaySinhNV.Value = nhanVien.NgaySinh;
                cboChinhSuaGioiTinhNV.SelectedItem = nhanVien.GioiTinh;
                txtChinhSuaCCCD_NV.Text = nhanVien.CCCD;
                txtChinhSuaSDT_NV.Text = nhanVien.SDT;
                txtChinhSuaEmail_NV.Text = nhanVien.Email;
                txtChinhSuaDiaChiThuongTruNV.Text = nhanVien.DiaChiThuongTru;
                txtChinhSuaPhuongNV.Text = nhanVien.Phuong;
                txtChinhSuaQuanNV.Text = nhanVien.Quan;
                txtChinhSuaThanhPhoNV.Text = nhanVien.ThanhPho;
                cboChinhSuaTinhNV.SelectedItem = nhanVien.Tinh;
                dtpChinhSuaNgayBatDauNV.Value = nhanVien.NgayBatDau;
                cboChinhSuaChucVuNV.SelectedItem = nhanVien.ChucVu;
                cboChinhSuaCaLamViecNV.SelectedItem = nhanVien.CaLamViec.ToString();
                txtChinhSuaLuongNV.Text = nhanVien.Luong.ToString();
            }
        }

        private void btnExit_ChinhSuaNhanVien_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnChinhSuaNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(txtChinhSuaMaNV.Text))
                    throw new Exception("Vui lòng nhập Mã nhân viên!");
                if (string.IsNullOrWhiteSpace(txtChinhSuaHoNV.Text))
                    throw new Exception("Vui lòng nhập Họ!");
                if (string.IsNullOrWhiteSpace(txtChinhSuaTenNV.Text))
                    throw new Exception("Vui lòng nhập Tên!");
                if (dtpChinhSuaNgaySinhNV.Value > DateTime.Now)
                    throw new Exception("Ngày sinh không hợp lệ!");
                if (cboChinhSuaGioiTinhNV.SelectedItem == null)
                    throw new Exception("Vui lòng chọn Giới tính!");
                if (string.IsNullOrWhiteSpace(txtChinhSuaCCCD_NV.Text))
                    throw new Exception("Vui lòng nhập CCCD!");
                if (string.IsNullOrWhiteSpace(txtChinhSuaSDT_NV.Text))
                    throw new Exception("Vui lòng nhập Số điện thoại!");
                if (string.IsNullOrWhiteSpace(txtChinhSuaDiaChiThuongTruNV.Text))
                    throw new Exception("Vui lòng nhập Địa chỉ thường trú!");
                if (string.IsNullOrWhiteSpace(txtChinhSuaPhuongNV.Text))
                    throw new Exception("Vui lòng nhập Phường!");
                if (string.IsNullOrWhiteSpace(txtChinhSuaQuanNV.Text))
                    throw new Exception("Vui lòng nhập Quận!");
                if (string.IsNullOrWhiteSpace(txtChinhSuaThanhPhoNV.Text))
                    throw new Exception("Vui lòng nhập Thành phố!");
                // Chỉ yêu cầu chọn Tỉnh nếu ThanhPho không phải là TP.HCM
                if (txtChinhSuaThanhPhoNV.Text.Trim().ToUpper() != "TP.HCM" && cboChinhSuaTinhNV.SelectedItem == null)
                    throw new Exception("Vui lòng chọn Tỉnh!");
                if (dtpChinhSuaNgayBatDauNV.Value > DateTime.Now)
                    throw new Exception("Ngày bắt đầu không hợp lệ!");
                if (cboChinhSuaChucVuNV.SelectedItem == null)
                    throw new Exception("Vui lòng chọn Chức vụ!");
                if (cboChinhSuaCaLamViecNV.SelectedItem == null)
                    throw new Exception("Vui lòng chọn Ca làm việc!");
                if (!decimal.TryParse(txtChinhSuaLuongNV.Text, out decimal luong) || luong <= 0)
                    throw new Exception("Lương không hợp lệ! Vui lòng nhập số lớn hơn 0.");

                // Tạo đối tượng NhanVienDTO với thông tin đã chỉnh sửa
                NhanVienDTO updatedNV = new NhanVienDTO
                {
                    MaNV = txtChinhSuaMaNV.Text,
                    Ho = txtChinhSuaHoNV.Text,
                    Ten = txtChinhSuaTenNV.Text,
                    NgaySinh = dtpChinhSuaNgaySinhNV.Value,
                    GioiTinh = cboChinhSuaGioiTinhNV.SelectedItem.ToString(),
                    CCCD = txtChinhSuaCCCD_NV.Text,
                    SDT = txtChinhSuaSDT_NV.Text,
                    Email = txtChinhSuaEmail_NV.Text,
                    DiaChiThuongTru = txtChinhSuaDiaChiThuongTruNV.Text,
                    Phuong = txtChinhSuaPhuongNV.Text,
                    Quan = txtChinhSuaQuanNV.Text,
                    ThanhPho = txtChinhSuaThanhPhoNV.Text,
                    Tinh = cboChinhSuaTinhNV.SelectedItem?.ToString() ?? "",
                    NgayBatDau = dtpChinhSuaNgayBatDauNV.Value,
                    ChucVu = cboChinhSuaChucVuNV.SelectedItem.ToString(),
                    CaLamViec = int.Parse(cboChinhSuaCaLamViecNV.SelectedItem.ToString()),
                    Luong = luong,
                    TrangThai = "Đang làm việc"
                };

                nhanVienDataAccess.UpdateNhanVien(updatedNV);
                MessageBox.Show("Thông tin nhân viên đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form sau khi chỉnh sửa thành công
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void txtChinhSuaHoNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChinhSuaNhanVien_Click_1(object sender, EventArgs e)
        {

        }
    }
}
