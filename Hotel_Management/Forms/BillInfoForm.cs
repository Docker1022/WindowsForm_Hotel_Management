using Microsoft.Reporting.WinForms;
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
    public partial class BillInfoForm : Form
    {
        private HoaDonDAO hoaDonDAO;
        public BillInfoForm()
        {
            InitializeComponent();
            hoaDonDAO = new HoaDonDAO(); // Khởi tạo đối tượng DAO
        }

        public void SetHoaDonInfo(string maHoaDon, string maPhong, string loaiPhong, DateTime checkIn, DateTime checkOut,
                              string hoKH, string tenKH, string cccd, string soDienThoai,
                              string quocTich, decimal giaPhong, int soNgayO,
                              decimal tienDichVu, decimal tienPhong, decimal tongTien, string trangThai)
        {
            // Gán dữ liệu vào các TextBox hoặc Label trên form
            lblMaHoaDon.Text = maHoaDon;
            lblMaPhong.Text = maPhong;           // P.203
            lblLoaiPhong.Text = loaiPhong;       // Family
            lblCheckIn.Text = checkIn.ToString("dd/MM/yyyy");   // 6/2/2023
            lblCheckOut.Text = checkOut.ToString("dd/MM/yyyy"); // 6/2/2023
            lblHoKH.Text = hoKH;                   // (phần họ)
            lblTenKH.Text = tenKH;                 // abcd demo
            lblCCCD_KH.Text = cccd;                 // 87678876
            lblSDT_KH.Text = soDienThoai;   // (nếu có)
            lblQuocTich.Text = quocTich;         // Việt Nam
            lblGiaPhong.Text = giaPhong.ToString("#,##0 ₫");   // 1.000.000 ₫
            lblSoNgayO.Text = soNgayO.ToString();              // 1
            lblTongTienPhong.Text = tienPhong.ToString("#,##0 ₫"); // Tính tổng tiền phòng
            lblTongTienDichVu.Text = tienDichVu.ToString("#,##0 ₫");          // 1.000.000 ₫
            lblThanhTien.Text = tongTien.ToString("#,##0 ₫");                 // 7.100.000 ₫
            lblTrangThaiBill.Text = trangThai; // Đã thanh toán
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_BillInfoForm_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ CSDL
                var (hoaDonData, dichVuData) = hoaDonDAO.LayDuLieuHoaDon(lblMaHoaDon.Text);

                if (hoaDonData.Rows.Count > 0)
                {
                    // Cập nhật trạng thái hóa đơn thành "Đã thanh toán"
                    hoaDonDAO.CapNhatTrangThaiHoaDon(lblMaHoaDon.Text, "Đã thanh toán");

                    // Thiết lập ReportViewer
                    reportViewerHoaDon.ProcessingMode = ProcessingMode.Local;
                    reportViewerHoaDon.LocalReport.ReportPath = "D:\\DOWNLOADS\\LT Windows\\Hotel_Management\\Hotel_Management\\Reports\\HoaDonReport.rdlc";

                    // Xóa nguồn cũ và gán nguồn mới
                    reportViewerHoaDon.LocalReport.DataSources.Clear();
                    reportViewerHoaDon.LocalReport.DataSources.Add(new ReportDataSource("HoaDonDataset", hoaDonData));
                    reportViewerHoaDon.LocalReport.DataSources.Add(new ReportDataSource("DichVuDataset", dichVuData));

                    // Làm mới và hiển thị Report
                    reportViewerHoaDon.RefreshReport();
                    reportViewerHoaDon.Visible = true;

                    MessageBox.Show("In hóa đơn thành công và đã cập nhật trạng thái thành 'Đã thanh toán'!",
                                   "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu hóa đơn với mã: " + lblMaHoaDon.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in hóa đơn hoặc cập nhật trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BillInfoForm_Load(object sender, EventArgs e)
        {

            this.reportViewerHoaDon.RefreshReport();
            this.reportViewerHoaDon.RefreshReport();
        }
    }
}
