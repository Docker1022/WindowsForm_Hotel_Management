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
    public partial class ucNhanVien : UserControl
    {
        private NhanVienDAO nhanVienDAO;

        public ucNhanVien()
        {
            InitializeComponent();
            

        }

        private void LoadNhanVien()
        {
            try
            {
                List<NhanVienDTO> nhanVienList = nhanVienDAO.GetAllNhanVien();
                dgvNhanVien.DataSource = nhanVienList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dgvNhanVien.AutoGenerateColumns = false;
            dgvNhanVien.Columns.Clear();

            // Danh sách cột và cấu hình hiển thị
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNV", DataPropertyName = "MaNV", HeaderText = "Mã NV", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ho", DataPropertyName = "Ho", HeaderText = "Họ", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ten", DataPropertyName = "Ten", HeaderText = "Tên", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            var colNgaySinh = new DataGridViewTextBoxColumn { Name = "NgaySinh", DataPropertyName = "NgaySinh", HeaderText = "Ngày Sinh", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } };
            colNgaySinh.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvNhanVien.Columns.Add(colNgaySinh);

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "GioiTinh", DataPropertyName = "GioiTinh", HeaderText = "Giới Tính", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "CCCD", DataPropertyName = "CCCD", HeaderText = "CCCD", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "SDT", DataPropertyName = "SDT", HeaderText = "SĐT", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", DataPropertyName = "Email", HeaderText = "Email", Width = 250, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiaChiThuongTru", DataPropertyName = "DiaChiThuongTru", HeaderText = "Địa Chỉ", Width = 400, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "Phuong", DataPropertyName = "Phuong", HeaderText = "Phường", Width = 200, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quan", DataPropertyName = "Quan", HeaderText = "Quận", Width = 200, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThanhPho", DataPropertyName = "ThanhPho", HeaderText = "Thành Phố", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tinh", DataPropertyName = "Tinh", HeaderText = "Tỉnh", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            var colNgayBatDau = new DataGridViewTextBoxColumn { Name = "NgayBatDau", DataPropertyName = "NgayBatDau", HeaderText = "Ngày Bắt Đầu", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } };
            colNgayBatDau.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvNhanVien.Columns.Add(colNgayBatDau);

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "ChucVu", DataPropertyName = "ChucVu", HeaderText = "Chức Vụ", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "CaLamViec", DataPropertyName = "CaLamViec", HeaderText = "Ca Làm Việc", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            var colLuong = new DataGridViewTextBoxColumn { Name = "Luong", DataPropertyName = "Luong", HeaderText = "Lương", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } };
            colLuong.DefaultCellStyle.Format = "#,##0 VNĐ";
            colLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNhanVien.Columns.Add(colLuong);

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", DataPropertyName = "TrangThai", HeaderText = "Trạng Thái", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });       
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void ucNhanVien_Load(object sender, EventArgs e)
        {
            nhanVienDAO = new NhanVienDAO();
            ConfigureDataGridView();
            if (!DesignMode) // Chỉ chạy khi không ở chế độ thiết kế
            {
                LoadNhanVien();
            }
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCCCD_NV_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            ThemNhanVien themNV = new ThemNhanVien();
            themNV.ShowDialog();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dgvNhanVien.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chỉ lấy dòng đầu tiên nếu nhiều dòng được chọn (ưu tiên dòng đầu)
                if (dgvNhanVien.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Vui lòng chỉ chọn một nhân viên để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow row = dgvNhanVien.SelectedRows[0];
                NhanVienDTO selectedNV = row.DataBoundItem as NhanVienDTO;

                if (selectedNV != null)
                {
                    // Mở form SuaNhanVien và truyền dữ liệu nhân viên
                    SuaNhanVien suaForm = new SuaNhanVien(selectedNV);
                    suaForm.ShowDialog();
                    LoadNhanVien(); // Làm mới DataGridView sau khi chỉnh sửa
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiemNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                var nhanVienList = nhanVienDAO.GetAllNhanVien();
                var filteredList = new List<NhanVienDTO>();

                foreach (var nv in nhanVienList)
                {
                    bool match = true;

                    // Lọc theo Mã nhân viên (không phân biệt hoa thường)
                    if (!string.IsNullOrWhiteSpace(txtMaNV.Text) && !string.Equals(nv.MaNV, txtMaNV.Text, StringComparison.OrdinalIgnoreCase))
                        match = false;

                    // Lọc theo Họ (không phân biệt hoa thường)
                    if (!string.IsNullOrWhiteSpace(txtHoNV.Text) && !string.Equals(nv.Ho, txtHoNV.Text, StringComparison.OrdinalIgnoreCase))
                        match = false;

                    // Lọc theo Tên (không phân biệt hoa thường)
                    if (!string.IsNullOrWhiteSpace(txtTenNV.Text) && !string.Equals(nv.Ten, txtTenNV.Text, StringComparison.OrdinalIgnoreCase))
                        match = false;

                    // Lọc theo CCCD (không phân biệt hoa thường)
                    if (!string.IsNullOrWhiteSpace(txtCCCD_NV.Text) && !string.Equals(nv.CCCD, txtCCCD_NV.Text, StringComparison.OrdinalIgnoreCase))
                        match = false;

                    // Lọc theo Ca làm việc
                    if (cboCaLamViecNV.SelectedItem != null && nv.CaLamViec.ToString() != cboCaLamViecNV.SelectedItem.ToString())
                        match = false;

                    // Lọc theo Chức vụ (không phân biệt hoa thường)
                    if (cboChucVuNV.SelectedItem != null && !string.Equals(nv.ChucVu, cboChucVuNV.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase))
                        match = false;

                    // Lọc theo Trạng thái (không phân biệt hoa thường)
                    if (cboTrangThaiNV.SelectedItem != null && !string.Equals(nv.TrangThai, cboTrangThaiNV.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase))
                        match = false;

                    if (match)
                        filteredList.Add(nv);
                }

                dgvNhanVien.DataSource = filteredList;

                txtMaNV.Clear();
                txtHoNV.Clear();
                txtTenNV.Clear();
                txtCCCD_NV.Clear();
                cboCaLamViecNV.SelectedItem = null;
                cboChucVuNV.SelectedItem = null;
                cboTrangThaiNV.SelectedItem = null;
                
                if (filteredList.Count == 0)
                    MessageBox.Show("Không tìm thấy nhân viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dgvNhanVien.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận trước khi xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {dgvNhanVien.SelectedRows.Count} nhân viên?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                // Thu thập danh sách MaNV của các dòng được chọn
                List<string> maNVList = new List<string>();
                foreach (DataGridViewRow row in dgvNhanVien.SelectedRows)
                {
                    NhanVienDTO nv = row.DataBoundItem as NhanVienDTO;
                    if (nv != null)
                    {
                        maNVList.Add(nv.MaNV);
                    }
                }

                // Gọi phương thức xóa
                nhanVienDAO.DeleteNhanVien(maNVList);

                // Làm mới DataGridView
                LoadNhanVien();
                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThemNV_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }
    }
}