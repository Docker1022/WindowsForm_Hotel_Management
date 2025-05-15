using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management
{
    public partial class ucHoaDon : UserControl
    {
        private HoaDonDAO hoaDonDAO;
        private DataGridViewRow selectedHoaDonRow;
        public ucHoaDon()
        {
            InitializeComponent();
        }

        private void LoadHoaDon()
        {
            try
            {
                List<HoaDonDTO> hoaDonList = hoaDonDAO.GetAllHoaDon();
                dgvHoaDon.DataSource = hoaDonList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dgvHoaDon.AutoGenerateColumns = false;
            dgvHoaDon.Columns.Clear();

            // Danh sách cột và cấu hình hiển thị
            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaHoaDon",
                DataPropertyName = "MaHoaDon",
                HeaderText = "Mã Hóa Đơn",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaDatPhong",
                DataPropertyName = "MaDatPhong",
                HeaderText = "Mã Đặt Phòng",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPhong",
                DataPropertyName = "MaPhong",
                HeaderText = "Mã Phòng",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LoaiPhong",
                DataPropertyName = "LoaiPhong",
                HeaderText = "Loại Phòng",
                Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            var colCheckIn = new DataGridViewTextBoxColumn
            {
                Name = "CheckIn",
                DataPropertyName = "CheckIn",
                HeaderText = "Check In",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            colCheckIn.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvHoaDon.Columns.Add(colCheckIn);

            var colCheckOut = new DataGridViewTextBoxColumn
            {
                Name = "CheckOut",
                DataPropertyName = "CheckOut",
                HeaderText = "Check Out",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            colCheckOut.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvHoaDon.Columns.Add(colCheckOut);

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaNV",
                DataPropertyName = "MaNV",
                HeaderText = "Mã NV",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenNV",
                DataPropertyName = "TenNV",
                HeaderText = "Tên Nhân Viên",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            var colNgayLap = new DataGridViewTextBoxColumn
            {
                Name = "NgayLap",
                DataPropertyName = "NgayLap",
                HeaderText = "Ngày Lập",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            colNgayLap.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvHoaDon.Columns.Add(colNgayLap);

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoKH",
                DataPropertyName = "HoKH",
                HeaderText = "Họ KH",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenKH",
                DataPropertyName = "TenKH",
                HeaderText = "Tên KH",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CCCD",
                DataPropertyName = "CCCD",
                HeaderText = "CCCD",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoDienThoai",
                DataPropertyName = "SoDienThoai",
                HeaderText = "SĐT",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QuocTich",
                DataPropertyName = "QuocTich",
                HeaderText = "Quốc Tịch",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            var colTienDichVu = new DataGridViewTextBoxColumn
            {
                Name = "TienDichVu",
                DataPropertyName = "TienDichVu",
                HeaderText = "Tiền Dịch Vụ",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            colTienDichVu.DefaultCellStyle.Format = "#,##0 ₫"; // Định dạng số tiền (không thập phân)
            dgvHoaDon.Columns.Add(colTienDichVu);

            var colTienPhong = new DataGridViewTextBoxColumn
            {
                Name = "TienPhong",
                DataPropertyName = "TienPhong",
                HeaderText = "Tiền Phòng",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            colTienPhong.DefaultCellStyle.Format = "#,##0 ₫"; // Định dạng số tiền (không thập phân)
            dgvHoaDon.Columns.Add(colTienPhong);

            var colTongTien = new DataGridViewTextBoxColumn
            {
                Name = "TongTien",
                DataPropertyName = "TongTien",
                HeaderText = "Tổng Tiền",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            colTongTien.DefaultCellStyle.Format = "#,##0 ₫"; // Định dạng số tiền (không thập phân)
            dgvHoaDon.Columns.Add(colTongTien);

            dgvHoaDon.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng Thái",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void ucHoaDoan_Load(object sender, EventArgs e)
        {
            hoaDonDAO = new HoaDonDAO();
            ConfigureDataGridView();
            if (!DesignMode) // Chỉ chạy khi không ở chế độ thiết kế
            {
                LoadHoaDon();
            }
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedHoaDonRow = dgvHoaDon.Rows[e.RowIndex];
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiemHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                var hoaDonList = hoaDonDAO.GetAllHoaDon(); // Lấy tất cả hóa đơn
                var filteredList = new List<HoaDonDTO>();

                foreach (var hd in hoaDonList)
                {
                    bool match = true;

                    // Lọc theo Mã hóa đơn
                    if (!string.IsNullOrWhiteSpace(txtMaHD.Text) &&
                        !string.Equals(hd.MaHoaDon, txtMaHD.Text, StringComparison.OrdinalIgnoreCase))
                        match = false;

                    // Lọc theo Mã đặt phòng
                    if (!string.IsNullOrWhiteSpace(txtMaDP.Text) &&
                        !string.Equals(hd.MaDatPhong, txtMaDP.Text, StringComparison.OrdinalIgnoreCase))
                        match = false;

                    // Lọc theo Mã nhân viên
                    if (!string.IsNullOrWhiteSpace(txtMaNV.Text) &&
                        !string.Equals(hd.MaNV, txtMaNV.Text, StringComparison.OrdinalIgnoreCase))
                        match = false;

                    // Lọc theo Mã phòng
                    if (cboMaPhong.SelectedItem != null &&
                        !string.Equals(hd.MaPhong, cboMaPhong.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase))
                        match = false;

                    // Lọc theo Tên khách hàng (cho phép chứa từ khóa tìm kiếm)
                    if (!string.IsNullOrWhiteSpace(txtTenKH.Text) &&
                        hd.TenKH.IndexOf(txtTenKH.Text, StringComparison.OrdinalIgnoreCase) < 0)
                        match = false;

                    // Lọc theo Trạng thái
                    if (cboTrangThai.SelectedItem != null &&
                        !string.Equals(hd.TrangThai, cboTrangThai.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase))
                        match = false;

                    if (match)
                        filteredList.Add(hd);
                }

                dgvHoaDon.DataSource = filteredList;

                if (filteredList.Count == 0)
                    MessageBox.Show("Không tìm thấy hóa đơn nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dgvHoaDon.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận trước khi xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {dgvHoaDon.SelectedRows.Count} hóa đơn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                // Thu thập danh sách MaHD của các dòng được chọn
                List<string> maHDList = new List<string>();
                foreach (DataGridViewRow row in dgvHoaDon.SelectedRows)
                {
                    HoaDonDTO hd = row.DataBoundItem as HoaDonDTO;
                    if (hd != null)
                    {
                        maHDList.Add(hd.MaHoaDon);
                    }
                }

                // Gọi phương thức xóa
                hoaDonDAO.DeleteHoaDon(maHDList);

                // Làm mới DataGridView
                LoadHoaDon();
                MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemHoaDon_Click(object sender, EventArgs e)
        {
            if (selectedHoaDonRow == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maHoaDon = selectedHoaDonRow.Cells["MaHoaDon"].Value.ToString();
                string maPhong = selectedHoaDonRow.Cells["MaPhong"].Value.ToString();
                string loaiPhong = selectedHoaDonRow.Cells["LoaiPhong"].Value.ToString();
                DateTime checkIn = Convert.ToDateTime(selectedHoaDonRow.Cells["CheckIn"].Value);
                DateTime checkOut = Convert.ToDateTime(selectedHoaDonRow.Cells["CheckOut"].Value);
                string hoKH = selectedHoaDonRow.Cells["HoKH"].Value.ToString();
                string tenKH = selectedHoaDonRow.Cells["TenKH"].Value.ToString();
                string cccd = selectedHoaDonRow.Cells["CCCD"].Value.ToString();
                string soDienThoai = selectedHoaDonRow.Cells["SoDienThoai"].Value.ToString();
                string quocTich = selectedHoaDonRow.Cells["QuocTich"].Value.ToString();
                decimal giaPhong = Convert.ToDecimal(selectedHoaDonRow.Cells["TienPhong"].Value);
                int soNgayO = (checkOut - checkIn).Days + 1;
                decimal tienDichVu = Convert.ToDecimal(selectedHoaDonRow.Cells["TienDichVu"].Value);
                decimal tienPhong = Convert.ToDecimal(selectedHoaDonRow.Cells["TienPhong"].Value);
                decimal tongTien = Convert.ToDecimal(selectedHoaDonRow.Cells["TongTien"].Value);
                string trangThai = selectedHoaDonRow.Cells["TrangThai"].Value.ToString();

                BillInfoForm billInfoForm = new BillInfoForm();
                billInfoForm.SetHoaDonInfo(maHoaDon, maPhong, loaiPhong, checkIn, checkOut, hoKH, tenKH, cccd,
                                           soDienThoai, quocTich, giaPhong, soNgayO,
                                           tienDichVu, tienPhong, tongTien, trangThai);
                billInfoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
