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
    public partial class ucDanhSachPhong : UserControl
    {
        private ucPhong phongDangChon;
        public ucDanhSachPhong()
        {
            InitializeComponent();
  
        }

        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ucDanhSachPhong_Load(object sender, EventArgs e)
        {
            if (!DesignMode) // Chỉ chạy khi không ở chế độ thiết kế
            {
                LoadLoaiPhongDayDu();
                LoadDanhSachPhong(); // Không truyền gì → lấy toàn bộ
            }
        }

        private void LoadLoaiPhongDayDu()
        {
            cboLoaiPhong.Items.Clear();
            var danhSachLoai = PhongDAO.LayDanhSachLoaiPhongDayDu();
            foreach (var loai in danhSachLoai)
            {
                cboLoaiPhong.Items.Add(loai);
            }
        }

        private void LoadDanhSachPhong(string loaiPhong = "")
        {
            flpDanhSachPhong.Controls.Clear();
            var danhSachPhong = PhongDAO.LayDanhSachPhong(loaiPhong);

            foreach (var phong in danhSachPhong)
            {
                ucPhong uc = new ucPhong();
                uc.TenPhong = phong.MaPhong;
                uc.LoaiVaTrangThai = $"{phong.LoaiPhongDayDu} - {(phong.TinhTrang ? "Bận" : "Trống")}";
                uc.IsOccupied = phong.TinhTrang;

                uc.PhongClicked += Uc_PhongClicked;

                flpDanhSachPhong.Controls.Add(uc);
            }
        }

        private void Uc_PhongClicked(object sender, EventArgs e)
        {
            ucPhong phongDuocClick = sender as ucPhong;
            if (phongDuocClick == null) return; // Kiểm tra null

            if (phongDangChon != null && phongDangChon != phongDuocClick)
            {
                phongDangChon.IsSelected = false;
            }

            phongDuocClick.IsSelected = true;
            phongDangChon = phongDuocClick;
            

            // Nếu phòng TRỐNG thì mở form đặt phòng
            if (!phongDuocClick.IsOccupied)
            {
                // Gọi database để lấy chi tiết phòng này
             
                PhongDTO phongInfo = PhongDAO.LayThongTinChiTietPhong(phongDuocClick.TenPhong);

                ReservationForm form = new ReservationForm(phongInfo);
                form.ShowDialog(); // hoặc .Show() nếu muốn không modal
            }
            // Nếu phòng BẬN thì mở form chi tiết phòng
            else
            {
                ThongTinChiTietPhongDTO thongTinPhong = DatPhongDAO.LayThongTinChiTietPhong(phongDuocClick.TenPhong);

                RoomInfoForm form = new RoomInfoForm(thongTinPhong);
                form.ShowDialog(); // hoặc .Show() nếu muốn không modal
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string loaiPhong = cboLoaiPhong.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(loaiPhong))
            {
                MessageBox.Show("Vui lòng chọn loại phòng để tìm kiếm.");
                return;
            }

            try
            {
                LoadDanhSachPhong(loaiPhong); // Gọi như cũ
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
        private void flpDanhSachPhong_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDanhSachPhong(); // Gọi lại để tải lại danh sách phòng
        }

        private void guna2HtmlLabel15_Click(object sender, EventArgs e)
        {

        }
    }
}
