using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Hotel_Management
{
    public class DoanhThuDAO
    {
        public DataTable LayDoanhThuTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            DataTable doanhThuData = new DataTable();
            string query = @"
        SELECT 
            p.MaPhong,
            rt.Ten + ' ' + oc.Ten AS LoaiPhong,
            kh.Ho + ' ' + kh.Ten AS TenKhachHang,
            CONVERT(VARCHAR(10), dp.CheckIn, 103) AS CheckIn,
            CONVERT(VARCHAR(10), dp.CheckOut, 103) AS CheckOut,
            FORMAT(hd.TienPhong, '#,##0') + N' ₫' AS TongTienPhong,
            FORMAT(hd.TienDichVu, '#,##0') + N' ₫' AS TongTienDichVu,
            FORMAT(hd.TongTien, '#,##0') + N' ₫' AS TongTien
        FROM HOA_DON hd
        INNER JOIN DAT_PHONG dp ON hd.MaDatPhong = dp.MaDatPhong
        INNER JOIN PHONG p ON dp.MaPhong = p.MaPhong
        INNER JOIN ROOM_TYPE rt ON p.MaLoaiPhong = rt.MaLoaiPhong
        INNER JOIN OCCUPANCY oc ON p.MaSoNguoi = oc.MaSoNguoi
        INNER JOIN KHACH_HANG kh ON dp.MaKH = kh.MaKH
        WHERE hd.NgayLap BETWEEN @TuNgay AND @DenNgay       
    "; // Lấy doanh thu theo ngày lập hóa đơn

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(doanhThuData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return doanhThuData;
        }

        public DataTable LayDoanhThuTheoLoaiPhong(string loaiPhong, DateTime tuNgay, DateTime denNgay)
        {
            DataTable doanhThuData = new DataTable();
            string query = @"
                            SELECT 
                                p.MaPhong,
                                rt.Ten + ' ' + oc.Ten AS LoaiPhong,
                                kh.Ho + ' ' + kh.Ten AS TenKhachHang,
                                CONVERT(VARCHAR(10), dp.CheckIn, 103) AS CheckIn,
                                CONVERT(VARCHAR(10), dp.CheckOut, 103) AS CheckOut,
                                FORMAT(hd.TienPhong, '#,##0') + N' ₫' AS TongTienPhong,
                                FORMAT(hd.TienDichVu, '#,##0') + N' ₫' AS TongTienDichVu,
                                FORMAT(hd.TongTien, '#,##0') + N' ₫' AS TongTien
                            FROM HOA_DON hd
                            INNER JOIN DAT_PHONG dp ON hd.MaDatPhong = dp.MaDatPhong
                            INNER JOIN PHONG p ON dp.MaPhong = p.MaPhong
                            INNER JOIN ROOM_TYPE rt ON p.MaLoaiPhong = rt.MaLoaiPhong
                            INNER JOIN OCCUPANCY oc ON p.MaSoNguoi = oc.MaSoNguoi
                            INNER JOIN KHACH_HANG kh ON dp.MaKH = kh.MaKH
                            WHERE (rt.Ten + ' ' + oc.Ten) = @LoaiPhong
                              AND hd.NgayLap BETWEEN @TuNgay AND @DenNgay
                            ";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LoaiPhong", loaiPhong);
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(doanhThuData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return doanhThuData;
        }

        public DataTable LayDoanhThuTheoDichVu(string tenDichVu, DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = new DataTable();
            string query = @"
                        SELECT 
                            dv.Ten AS [TenDichVu],
                            kh.Ho + ' ' + kh.Ten AS [TenKhachHang],
                            CONVERT(VARCHAR(10), dp.CheckIn, 103) AS [CheckIn],
                            FORMAT(dv.Gia, '#,##0') + N' ₫' AS [DonGia],
                            COUNT(*) AS [SoLuong],
                            FORMAT(COUNT(*) * dv.Gia, '#,##0') + N' ₫' AS [ThanhTien],
                            FORMAT(sddv.NgaySuDung, 'dd/MM/yyyy') AS [NgaySuDung]
                        FROM SU_DUNG_DICH_VU sddv
                        INNER JOIN DICH_VU dv ON sddv.IDDichVu = dv.ID
                        INNER JOIN DAT_PHONG dp ON sddv.MaDatPhong = dp.MaDatPhong
                        INNER JOIN KHACH_HANG kh ON dp.MaKH = kh.MaKH
                        WHERE dv.Ten = @TenDichVu
                          AND sddv.NgaySuDung BETWEEN @TuNgay AND @DenNgay
                        GROUP BY 
                            dv.Ten, kh.Ho, kh.Ten, dp.CheckIn, dv.Gia, sddv.NgaySuDung
                        ORDER BY sddv.NgaySuDung;

                            ";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDichVu", tenDichVu);
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn doanh thu dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }


    }
}
