using Hotel_Management.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management.UserControls
{
    public partial class ucTaiKhoan : UserControl
    {
        private TaiKhoanDAO taiKhoanDAO;
        public ucTaiKhoan()
        {
            InitializeComponent();
        }

        private void ConfigureDataGridView()
        {
            dgvTaiKhoan.AutoGenerateColumns = false;
            dgvTaiKhoan.Columns.Clear();
            // Danh sách cột và cấu hình hiển thị
            dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNV", DataPropertyName = "MaNV", HeaderText = "Mã NV", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenDangNhap", DataPropertyName = "TenDangNhap", HeaderText = "Tên Đăng Nhập", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvTaiKhoan.Columns.Add(new DataGridViewTextBoxColumn { Name = "MatKhau", DataPropertyName = "MatKhau", HeaderText = "Mật Khẩu", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
        }

        private void LoadTaiKhoan()
        {
            try
            {
                DataTable dt = taiKhoanDAO.LayDanhSachTaiKhoan();
                dgvTaiKhoan.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemTK_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các TextBox
            string maNV = txtMaNV.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(maNV) || string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi phương thức thêm tài khoản
                taiKhoanDAO.ThemTaiKhoan(maNV, tenDangNhap, matKhau);

                // Thông báo thành công
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới DataGridView
                DataTable dt = taiKhoanDAO.LayDanhSachTaiKhoan();
                dgvTaiKhoan.DataSource = dt;

                // Xóa các TextBox sau khi thêm thành công
                txtMaNV.Clear();
                txtTenDangNhap.Clear();
                txtMatKhau.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucTaiKhoan_Load(object sender, EventArgs e)
        {
            taiKhoanDAO = new TaiKhoanDAO();
            ConfigureDataGridView();
            if(!DesignMode) // Chỉ chạy khi không ở chế độ thiết kế
            {
                LoadTaiKhoan();
            }
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn trong DataGridView không
            if (dgvTaiKhoan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy MaNV từ dòng được chọn
            string maNV = dgvTaiKhoan.SelectedRows[0].Cells["MaNV"].Value.ToString();

            // Xác nhận xóa từ người dùng
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản của nhân viên {maNV} không?",
                                                  "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            try
            {
                // Gọi phương thức xóa tài khoản
                taiKhoanDAO.XoaTaiKhoan(maNV);

                // Thông báo thành công
                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới DataGridView
                DataTable dt = taiKhoanDAO.LayDanhSachTaiKhoan();
                dgvTaiKhoan.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không phải header hoặc dòng không hợp lệ
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
            }
        }

        private void btnCapNhatTK_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các TextBox
            string maNV = txtMaNV.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(maNV) || string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi phương thức cập nhật tài khoản
                taiKhoanDAO.CapNhatTaiKhoan(maNV, tenDangNhap, matKhau);

                // Thông báo thành công
                MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới DataGridView
                DataTable dt = taiKhoanDAO.LayDanhSachTaiKhoan();
                dgvTaiKhoan.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bntTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string tenDangNhap = txtTenDangNhap.Text.Trim();
                if (string.IsNullOrWhiteSpace(tenDangNhap))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataTable dt = taiKhoanDAO.TimKiemTaiKhoan(tenDangNhap);
                dgvTaiKhoan.DataSource = dt;
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy tài khoản nào với tên đăng nhập này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  
    }
}
