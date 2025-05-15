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
using System.Data.SqlClient;
using System.Globalization;

namespace Hotel_Management
{
    public partial class RoomInfoForm : Form
    {
        private ThongTinChiTietPhongDTO _thongTinPhong;
        private DichVuDAO dichVuDAO;
        private DatPhongDAO datPhongDAO;
        public RoomInfoForm()
        {
            InitializeComponent();
            dichVuDAO = new DichVuDAO();
            datPhongDAO = new DatPhongDAO();
        }

        public RoomInfoForm(ThongTinChiTietPhongDTO thongTinPhong)
        {
            InitializeComponent();
            _thongTinPhong = thongTinPhong;
            dichVuDAO = new DichVuDAO();
            datPhongDAO = new DatPhongDAO();

            // Gán dữ liệu cho các label
            lblMaPhong.Text = _thongTinPhong.MaPhong;
            lblLoaiPhong.Text = _thongTinPhong.LoaiPhong;
            lblCheckIn.Text = _thongTinPhong.CheckIn?.ToString("dd/MM/yyyy") ?? "Chưa có thông tin";
            lblCheckOut.Text = _thongTinPhong.CheckOut?.ToString("dd/MM/yyyy") ?? "Chưa có thông tin";
            lblTrangThaiPhong.Text = _thongTinPhong.TrangThai;
            lblGiaPhong.Text = string.Format("{0:#,0} VND", _thongTinPhong.GiaPhong);
            lblHoKH.Text = _thongTinPhong.Ho;
            lblTenKH.Text = _thongTinPhong.Ten;
            lblCCCD_KH.Text = _thongTinPhong.CCCD;
            lblSDT_KH.Text = _thongTinPhong.SoDienThoai;
            lblQuocTich.Text = _thongTinPhong.QuocTich;
            lblSoNgayO.Text = _thongTinPhong.SoNgayO.ToString() + " ngày";
            lblMaDatPhong.Text = _thongTinPhong.MaDatPhong; // Gán MaDatPhong cho label

            LoadDichVuComboBox();
            LoadListView();
        }

        private void LoadDichVuComboBox()
        {
            cboTenDichVu.Items.Clear();
            var dichVuList = dichVuDAO.GetAllDichVu();
            foreach (var dv in dichVuList)
            {
                cboTenDichVu.Items.Add(new { ID = dv.MaDV, Ten = dv.TenDV, Gia = dv.DonGia });
            }
            cboTenDichVu.DisplayMember = "Ten";
            cboTenDichVu.ValueMember = "ID";
        }

        private void LoadListView()
        {
            if (string.IsNullOrWhiteSpace(lblMaDatPhong.Text))
            {
                MessageBox.Show("Không tìm thấy Mã đặt phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lvDichVu.Items.Clear();
            var dichVuList = dichVuDAO.GetDichVuByMaDatPhong(lblMaDatPhong.Text);
            int stt = 1;
            foreach (var dv in dichVuList)
            {
                var item = new ListViewItem(stt.ToString());
                item.SubItems.Add(dv.TenDV ?? "Không có dữ liệu");
                item.SubItems.Add(dv.NgaySuDung.ToString("dd/MM/yyyy") ?? "Không có dữ liệu");
                item.SubItems.Add(DateTime.Parse(dv.ThoiDiem).ToString("HH:mm:ss") ?? "Không có dữ liệu");
                item.SubItems.Add(dv.DonGia.ToString("#,##0 đ"));
                lvDichVu.Items.Add(item);
                stt++;
            }
        }


        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel20_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void RoomInfoForm_Load(object sender, EventArgs e)
        {
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }

        private void dgvDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {

        }

        private void btnTraPhong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblMaDatPhong.Text))
            {
                MessageBox.Show("Không tìm thấy Mã đặt phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                datPhongDAO.CheckOut(lblMaDatPhong.Text, txtMaNV.Text);
                MessageBox.Show("Trả phòng thành công! Vui lòng kiểm tra hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi trả phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemDichVu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblMaDatPhong.Text))
            {
                MessageBox.Show("Không tìm thấy Mã đặt phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboTenDichVu.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedDichVu = (dynamic)cboTenDichVu.SelectedItem;
            int idDichVu = selectedDichVu.ID;
            decimal chiPhi = selectedDichVu.Gia;
            DateTime ngaySuDung = DateTime.Now;
            string thoiDiem = DateTime.Now.ToString("HH:mm:ss");

            try
            {
                bool result = dichVuDAO.ThemSuDungDichVu(lblMaDatPhong.Text, idDichVu, ngaySuDung, thoiDiem, chiPhi);
                if (result)
                {
                    LoadListView();
                    MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể thêm dịch vụ. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaDichVu_Click(object sender, EventArgs e)
        {
            if (lvDichVu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            int deletedCount = 0;

            try
            {
                foreach (ListViewItem selectedItem in lvDichVu.SelectedItems)
                {
                    string tenDichVu = selectedItem.SubItems[1].Text;
                    string strNgaySuDung = selectedItem.SubItems[2].Text;
                    string strThoiDiem = selectedItem.SubItems[3].Text;

                    var dichVuList = dichVuDAO.GetAllDichVu();
                    var dichVu = dichVuList.FirstOrDefault(dv => dv.TenDV == tenDichVu);
                    if (dichVu == null)
                    {
                        MessageBox.Show($"Không tìm thấy dịch vụ: {tenDichVu}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    int idDichVu = dichVu.MaDV;
                    DateTime ngaySuDung = DateTime.ParseExact(strNgaySuDung, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string thoiDiem = strThoiDiem;

                    int rowsAffected = dichVuDAO.XoaSuDungDichVu(lblMaDatPhong.Text, idDichVu, ngaySuDung, thoiDiem);
                    if (rowsAffected > 0)
                        deletedCount++;
                }

                if (deletedCount > 0)
                {
                    LoadListView();
                    MessageBox.Show($"Xóa {deletedCount} dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể xóa dịch vụ nào. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
