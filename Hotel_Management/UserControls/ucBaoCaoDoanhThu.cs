using Hotel_Management.Forms;
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
    public partial class ucBaoCaoDoanhThu : UserControl
    {
        private DoanhThuDAO doanhThuDAO;
        public ucBaoCaoDoanhThu()
        {
            InitializeComponent();
            doanhThuDAO = new DoanhThuDAO();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void ucBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox5_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox6_Click(object sender, EventArgs e)
        {

        }

        private void btnDoanhThuNgay_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpDoanhThuNgay_Begin.Value.Date;
            DateTime denNgay = dtpDoanhThuNgay_End.Value.Date;

            var dt = doanhThuDAO.LayDoanhThuTheoNgay(tuNgay, denNgay);

            if (dt.Rows.Count > 0)
            {
                LoadReportViewBaoCao form = new LoadReportViewBaoCao();
                

                form.HienThiBaoCao(
                    "D:\\DOWNLOADS\\LT Windows\\Hotel_Management\\Hotel_Management\\Reports\\DoanhThuNgayReport.rdlc",
                    "DoanhThuNgayDataset",
                    dt
                );
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu trong khoảng thời gian này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnDoanhThuThang_Click(object sender, EventArgs e)
        {
            DateTime thangNam = dtpDoanhThuThang.Value;
            DateTime tuNgay = new DateTime(thangNam.Year, thangNam.Month, 1);
            DateTime denNgay = tuNgay.AddMonths(1).AddDays(-1);

            var dt = doanhThuDAO.LayDoanhThuTheoNgay(tuNgay, denNgay);

            if (dt.Rows.Count > 0)
            {
                LoadReportViewBaoCao form = new LoadReportViewBaoCao();
               

                form.HienThiBaoCao(
                    "D:\\DOWNLOADS\\LT Windows\\Hotel_Management\\Hotel_Management\\Reports\\DoanhThuThangReport.rdlc",
                    "DoanhThuThangDataset",
                    dt
                );
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu trong tháng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDoanhThuQuy_Click(object sender, EventArgs e)
        {
            int quy = int.Parse(cboQuy.SelectedItem.ToString());
            int nam = dtpDoanhThuQuy.Value.Year;

            DateTime tuNgay, denNgay;
            switch (quy)
            {
                case 1: tuNgay = new DateTime(nam, 1, 1); denNgay = new DateTime(nam, 3, 31); break;
                case 2: tuNgay = new DateTime(nam, 4, 1); denNgay = new DateTime(nam, 6, 30); break;
                case 3: tuNgay = new DateTime(nam, 7, 1); denNgay = new DateTime(nam, 9, 30); break;
                case 4: tuNgay = new DateTime(nam, 10, 1); denNgay = new DateTime(nam, 12, 31); break;
                default:
                    MessageBox.Show("Chọn quý hợp lệ!"); return;
            }

            var dt = doanhThuDAO.LayDoanhThuTheoNgay(tuNgay, denNgay);

            if (dt.Rows.Count > 0)
            {
                LoadReportViewBaoCao form = new LoadReportViewBaoCao();
                

                form.HienThiBaoCao(
                    "D:\\DOWNLOADS\\LT Windows\\Hotel_Management\\Hotel_Management\\Reports\\DoanhThuQuyReport.rdlc",
                    "DoanhThuQuyDataset",
                    dt
                );
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu trong quý này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDoanhThuNam_Click(object sender, EventArgs e)
        {
            int nam = dtpDoanhThuNam.Value.Year;
            DateTime tuNgay = new DateTime(nam, 1, 1);
            DateTime denNgay = new DateTime(nam, 12, 31);

            var dt = doanhThuDAO.LayDoanhThuTheoNgay(tuNgay, denNgay);

            if (dt.Rows.Count > 0)
            {
                LoadReportViewBaoCao form = new LoadReportViewBaoCao();
              

                form.HienThiBaoCao(
                    "D:\\DOWNLOADS\\LT Windows\\Hotel_Management\\Hotel_Management\\Reports\\DoanhThuNamReport.rdlc",
                    "DoanhThuNamDataset",
                    dt
                );
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu trong năm này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDoanhThuLoaiPhong_Click(object sender, EventArgs e)
        {
            string loaiPhong = cboLoaiPhong.SelectedItem?.ToString() ?? "";
            DateTime tuNgay = dtpDoanhThuLoaiPhong_Begin.Value.Date;
            DateTime denNgay = dtpDoanhThuLoaiPhong_End.Value.Date;

            if (string.IsNullOrEmpty(loaiPhong))
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dt = doanhThuDAO.LayDoanhThuTheoLoaiPhong(loaiPhong, tuNgay, denNgay);

            if (dt.Rows.Count > 0)
            {
                LoadReportViewBaoCao form = new LoadReportViewBaoCao();
               

                form.HienThiBaoCao(
                    "D:\\DOWNLOADS\\LT Windows\\Hotel_Management\\Hotel_Management\\Reports\\DoanhThuLoaiPhongReport.rdlc",
                    "DoanhThuLoaiPhongDataset",
                    dt
                );
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu loại phòng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDoanhThuDichVu_Click(object sender, EventArgs e)
        {
            string tenDichVu = cboLoaiDichVu.SelectedItem?.ToString() ?? "";
            DateTime tuNgay = dtpDoanhThuDichVu_Begin.Value.Date;
            DateTime denNgay = dtpDoanhThuDichVu_End.Value.Date;

            if (string.IsNullOrEmpty(tenDichVu))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dt = doanhThuDAO.LayDoanhThuTheoDichVu(tenDichVu, tuNgay, denNgay);

            if (dt.Rows.Count > 0)
            {
                LoadReportViewBaoCao form = new LoadReportViewBaoCao();
               

                form.HienThiBaoCao(
                    "D:\\DOWNLOADS\\LT Windows\\Hotel_Management\\Hotel_Management\\Reports\\DoanhThuDichVuReport.rdlc",
                    "DoanhThuDichVuDataset",
                    dt
                );
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu dịch vụ này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboQuy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void btnDoanhThuDichVu_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDoanhThuQuy_Click_1(object sender, EventArgs e)
        {

        }

        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void customGradientGroupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
