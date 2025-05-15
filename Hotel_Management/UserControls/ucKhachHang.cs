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
using System.Linq.Expressions;

namespace Hotel_Management
{
    public partial class ucKhachHang : UserControl
    {
        private KhachHangDAO khachHangDAO;
        public ucKhachHang()
        {
            InitializeComponent();
           
        }

        private void LoadKhachHang()
        {
            try
            {
                List<KhachHangDTO> khachHangList = khachHangDAO.GetAllKhachHang();
                dgvKhachHang.DataSource = khachHangList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.Columns.Clear();

            // Danh sách cột và cấu hình hiển thị
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaKH", DataPropertyName = "MaKH", HeaderText = "Mã KH", Width = 70, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ho", DataPropertyName = "Ho", HeaderText = "Họ", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Ten", DataPropertyName = "Ten", HeaderText = "Tên", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            var colNgaySinh = new DataGridViewTextBoxColumn { Name = "NgaySinh", DataPropertyName = "NgaySinh", HeaderText = "Ngày Sinh", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } };
            colNgaySinh.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvKhachHang.Columns.Add(colNgaySinh);

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "CCCD", DataPropertyName = "CCCD", HeaderText = "CCCD", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "SDT", DataPropertyName = "SDT", HeaderText = "SĐT", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuocTich", DataPropertyName = "QuocTich", HeaderText = "Quốc tịch", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ucKhachHang_Load(object sender, EventArgs e)
        {
            khachHangDAO = new KhachHangDAO();
            ConfigureDataGridView(); // Cấu hình cột trước
            if (!DesignMode) // Chỉ chạy khi không ở chế độ thiết kế
            {
                LoadKhachHang();
            }
            
  
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
                txtHoKH.Text = row.Cells["Ho"].Value?.ToString();
                txtTenKH.Text = row.Cells["Ten"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["NgaySinh"].Value?.ToString(), out DateTime ngaySinh))
                {
                    dtpNgaySinhKH.Value = ngaySinh;
                }

                txtCCCD_KH.Text = row.Cells["CCCD"].Value?.ToString();
                txtSDT_KH.Text = row.Cells["SDT"].Value?.ToString();
                txtQuocTich.Text = row.Cells["QuocTich"].Value?.ToString();
            }
        }

        private void dtpNgaySinhKH_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtSDT_KH_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboQuocTich_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaKH.Text))
                    throw new Exception("Vui lòng nhập Mã khách hàng!");
                if (string.IsNullOrWhiteSpace(txtHoKH.Text))
                    throw new Exception("Vui lòng nhập Họ!");
                if (string.IsNullOrWhiteSpace(txtTenKH.Text))
                    throw new Exception("Vui lòng nhập Tên!");
                if (dtpNgaySinhKH.Value > DateTime.Now)
                    throw new Exception("Ngày sinh không hợp lệ!");
                if (string.IsNullOrWhiteSpace(txtCCCD_KH.Text))
                    throw new Exception("Vui lòng nhập CCCD!");
                if (string.IsNullOrWhiteSpace(txtSDT_KH.Text))
                    throw new Exception("Vui lòng nhập SĐT!");
                if (string.IsNullOrWhiteSpace(txtQuocTich.Text))
                    throw new Exception("Vui lòng chọn Quốc tịch!");
                if (MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Tạo đối tượng KhachHangDTO với thông tin đã chỉnh sửa
                    KhachHangDTO khachHang = new KhachHangDTO
                    {
                        MaKH = txtMaKH.Text,
                        Ho = txtHoKH.Text,
                        Ten = txtTenKH.Text,
                        NgaySinh = dtpNgaySinhKH.Value,
                        CCCD = txtCCCD_KH.Text,
                        SDT = txtSDT_KH.Text,
                        QuocTich = txtQuocTich.Text
                    };
                    khachHangDAO.UpdateKhachHang(khachHang);
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadKhachHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dgvKhachHang.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một khách hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận trước khi xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {dgvKhachHang.SelectedRows.Count} khách hàng?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                // Thu thập danh sách MaNV của các dòng được chọn
                List<string> maKHList = new List<string>();
                foreach (DataGridViewRow row in dgvKhachHang.SelectedRows)
                {
                    KhachHangDTO kh = row.DataBoundItem as KhachHangDTO;
                    if (kh != null)
                    {
                        maKHList.Add(kh.MaKH);
                    }
                }

                // Gọi phương thức xóa
                khachHangDAO.DeleteKhachHang(maKHList);

                // Làm mới DataGridView
                LoadKhachHang();
                MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
