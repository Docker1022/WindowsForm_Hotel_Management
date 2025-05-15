using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management
{
    public class HoaDonDAO
    {
        public List<HoaDonDTO> GetAllHoaDon()
        {
            List<HoaDonDTO> hoaDonList = new List<HoaDonDTO>();
            string query = @"
                SELECT 
                    hd.MaHD AS MaHoaDon,
                    hd.MaDatPhong,
                    p.MaPhong,
                    rt.Ten + ' ' + oc.Ten AS LoaiPhong,
                    dp.CheckIn,
                    dp.CheckOut,
                    hd.MaNV,
                    nv.Ho + ' ' + nv.Ten AS TenNV,
                    hd.NgayLap,
                    kh.Ho AS HoKH,
                    kh.Ten AS TenKH,
                    kh.CCCD,
                    kh.SDT AS SoDienThoai,
                    kh.QuocTich,
                    hd.TienDichVu,
                    hd.TienPhong,
                    hd.TongTien,
                    hd.TrangThai
                FROM 
                    HOA_DON hd
                JOIN 
                    DAT_PHONG dp ON hd.MaDatPhong = dp.MaDatPhong
                JOIN 
                    PHONG p ON dp.MaPhong = p.MaPhong
                JOIN 
                    ROOM_TYPE rt ON p.MaLoaiPhong = rt.MaLoaiPhong
                JOIN 
                    OCCUPANCY oc ON p.MaSoNguoi = oc.MaSoNguoi
                JOIN 
                    KHACH_HANG kh ON dp.MaKH = kh.MaKH
                JOIN 
                    NHAN_VIEN nv ON hd.MaNV = nv.MaNV
            ";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HoaDonDTO hd = new HoaDonDTO
                                {
                                    MaHoaDon = reader["MaHoaDon"].ToString(),
                                    MaDatPhong = reader["MaDatPhong"].ToString(),
                                    MaPhong = reader["MaPhong"].ToString(),
                                    LoaiPhong = reader["LoaiPhong"].ToString(),
                                    CheckIn = Convert.ToDateTime(reader["CheckIn"]),
                                    CheckOut = Convert.ToDateTime(reader["CheckOut"]),
                                    MaNV = reader["MaNV"].ToString(),
                                    TenNV = reader["TenNV"].ToString(),
                                    NgayLap = Convert.ToDateTime(reader["NgayLap"]),
                                    HoKH = reader["HoKH"].ToString(),
                                    TenKH = reader["TenKH"].ToString(),
                                    CCCD = reader["CCCD"].ToString(),
                                    SoDienThoai = reader["SoDienThoai"].ToString(),
                                    QuocTich = reader["QuocTich"].ToString(),
                                    TienDichVu = Convert.ToDecimal(reader["TienDichVu"]),
                                    TienPhong = Convert.ToDecimal(reader["TienPhong"]),
                                    TongTien = Convert.ToDecimal(reader["TongTien"]),
                                    TrangThai = reader["TrangThai"].ToString()
                                };
                                hoaDonList.Add(hd);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách hóa đơn: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }

            return hoaDonList;
        }

        public (DataTable HoaDonData, DataTable DichVuData) LayDuLieuHoaDon(string maHoaDon)
        {
            DataTable hoaDonData = new DataTable(); // Dữ liệu hóa đơn
            DataTable dichVuData = new DataTable(); // Dữ liệu dịch vụ

            // Truy vấn 1: Lấy thông tin hóa đơn
            string queryHoaDon = @"
                                SELECT 
                        hd.MaHD, 
                        kh.Ho + ' ' + kh.Ten AS TenKhachHang, 
                        kh.CCCD, 
                        kh.SDT AS SoDienThoai, 
                        kh.QuocTich,
                        p.MaPhong, 
                        rt.Ten + ' ' + oc.Ten AS LoaiPhong, 
                        FORMAT(rt.Gia + oc.Gia, '#,##0') + N' ₫' AS GiaPhong,
                        CONVERT(VARCHAR(10), dp.CheckIn, 103) AS CheckIn, 
                        CONVERT(VARCHAR(10), dp.CheckOut, 103) AS CheckOut,
                        (DATEDIFF(DAY, dp.CheckIn, dp.CheckOut) + 1) AS SoNgayO,
                        FORMAT(hd.TienPhong, '#,##0') + N' ₫' AS TongTienPhong, 
                        FORMAT(hd.TienDichVu, '#,##0') + N' ₫' AS TongTienDichVu,
                        FORMAT(hd.TongTien, '#,##0') + N' ₫' AS ThanhTien
                    FROM HOA_DON hd
                    LEFT JOIN DAT_PHONG dp ON hd.MaDatPhong = dp.MaDatPhong
                    LEFT JOIN KHACH_HANG kh ON dp.MaKH = kh.MaKH
                    LEFT JOIN PHONG p ON dp.MaPhong = p.MaPhong
                    LEFT JOIN ROOM_TYPE rt ON p.MaLoaiPhong = rt.MaLoaiPhong
                    LEFT JOIN OCCUPANCY oc ON p.MaSoNguoi = oc.MaSoNguoi
                    WHERE hd.MaHD = @MaHD";

            // Truy vấn 2: Lấy danh sách dịch vụ
            string queryDichVu = @"
                                SELECT 
                                    dv.Ten AS TenDichVu,
                                    FORMAT(dv.Gia, '#,##0') + N' ₫' AS DonGia,
                                    COUNT(sddv.IDDichVu) AS SoLuong,
                                    FORMAT(dv.Gia * COUNT(sddv.IDDichVu), '#,##0') + N' ₫' AS ThanhTien
                                FROM 
                                    HOA_DON hd
                                INNER JOIN 
                                    DAT_PHONG dp ON hd.MaDatPhong = dp.MaDatPhong
                                INNER JOIN 
                                    SU_DUNG_DICH_VU sddv ON dp.MaDatPhong = sddv.MaDatPhong
                                INNER JOIN 
                                    DICH_VU dv ON sddv.IDDichVu = dv.ID
                                WHERE 
                                    hd.MaHD = @MaHD
                                GROUP BY 
                                    dv.Ten, dv.Gia
                    ";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    // Lấy dữ liệu hóa đơn
                    using (SqlCommand cmdHoaDon = new SqlCommand(queryHoaDon, conn))
                    {
                        cmdHoaDon.Parameters.AddWithValue("@MaHD", maHoaDon);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmdHoaDon))
                        {
                            adapter.Fill(hoaDonData);
                        }
                    }

                    // Lấy dữ liệu dịch vụ
                    using (SqlCommand cmdDichVu = new SqlCommand(queryDichVu, conn))
                    {
                        cmdDichVu.Parameters.AddWithValue("@MaHD", maHoaDon);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmdDichVu))
                        {
                            adapter.Fill(dichVuData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return (hoaDonData, dichVuData);
        }

        public void DeleteHoaDon(List<string> maHDList)
        {
            string query = "DELETE FROM HOA_DON WHERE MaHD IN ({0})";
            string paramPlaceholders = string.Join(",", maHDList.Select((_, index) => $"@MaHD{index}"));

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(string.Format(query, paramPlaceholders), conn))
                    {
                        for (int i = 0; i < maHDList.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@MaHD{i}", maHDList[i]);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa hóa đơn: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public void CapNhatTrangThaiHoaDon(string maHoaDon, string trangThai)
        {
            using (SqlConnection conn = DBConnect.GetConnection())
            {
                if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                string updateQuery = @"UPDATE HOA_DON SET TrangThai = @TrangThai WHERE MaHD = @MaHD";
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHD", maHoaDon);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
