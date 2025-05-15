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

namespace Hotel_Management.UserControls
{
    public partial class ucBangDieuKhien : UserControl
    {
        public ucBangDieuKhien()
        {
            InitializeComponent();
        }

        private void LoadBangDieuKhienTongQuat()
        {
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    string query = @"
                        SELECT 
                            (SELECT COUNT(*) FROM NHAN_VIEN) AS TongNhanVien,
                            (SELECT ISNULL(SUM(TongTien), 0) FROM HOA_DON WHERE CONVERT(DATE, NgayLap) = CONVERT(DATE, GETDATE())) AS DoanhThuHomNay,
                            (SELECT COUNT(*) FROM PHONG WHERE TrangThai = N'Trống') AS SoPhongTrong,
                            (SELECT ISNULL(SUM(TongTien), 0) FROM HOA_DON) AS TongDoanhThu
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblTongNhanVien.Text = reader["TongNhanVien"].ToString();
                                lblTongDoanhThuHomNay.Text = string.Format("{0:N0} ₫", reader["DoanhThuHomNay"]);
                                lblSoPhongTrong.Text = reader["SoPhongTrong"].ToString();
                                lblTongDoanhThu.Text = string.Format("{0:N0} ₫", reader["TongDoanhThu"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê tổng quát: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        private void ucBangThongKe_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadBangDieuKhienTongQuat();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
