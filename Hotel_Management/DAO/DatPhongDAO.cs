using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Hotel_Management
{
    public class DatPhongDAO
    {
        public static ThongTinChiTietPhongDTO LayThongTinChiTietPhong(string maPhong)
        {
            ThongTinChiTietPhongDTO info = null;
            string query = @"
                SELECT 
                    P.MaPhong, P.SoPhong,
                    CONCAT(RT.Ten, ' ', O.Ten) AS TenLoaiDayDu,
                    (RT.Gia + O.Gia) AS GiaPhong,
                    DP.MaDatPhong, DP.CheckIn, DP.CheckOut,
                    KH.Ho, KH.Ten, KH.CCCD, KH.SDT, KH.QuocTich,
                    P.TrangThai
                FROM PHONG P
                LEFT JOIN ROOM_TYPE RT ON P.MaLoaiPhong = RT.MaLoaiPhong
                LEFT JOIN OCCUPANCY O ON P.MaSoNguoi = O.MaSoNguoi
                LEFT JOIN DAT_PHONG DP ON P.MaPhong = DP.MaPhong
                LEFT JOIN KHACH_HANG KH ON DP.MaKH = KH.MaKH
                WHERE P.MaPhong = @MaPhong
                AND (DP.CheckOut IS NULL OR DP.CheckOut >= GETDATE())
            ";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("MaPhong", maPhong);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                info = new ThongTinChiTietPhongDTO
                                {
                                    MaDatPhong = reader["MaDatPhong"] == DBNull.Value ? null : reader["MaDatPhong"].ToString(),
                                    MaPhong = reader["MaPhong"].ToString(),
                                    SoPhong = reader["SoPhong"].ToString(),
                                    LoaiPhong = reader["TenLoaiDayDu"].ToString(),
                                    GiaPhong = reader.GetDecimal(reader.GetOrdinal("GiaPhong")),
                                    TrangThai = reader["TrangThai"].ToString(),
                                    CheckIn = reader["CheckIn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CheckIn"]),
                                    CheckOut = reader["CheckOut"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["CheckOut"]),
                                    Ho = reader["Ho"].ToString(),
                                    Ten = reader["Ten"].ToString(),
                                    CCCD = reader["CCCD"].ToString(),
                                    SoDienThoai = reader["SDT"].ToString(),
                                    QuocTich = reader["QuocTich"].ToString()
                                };

                                if (info.CheckIn.HasValue && info.CheckOut.HasValue)
                                {
                                    info.SoNgayO = (info.CheckOut.Value - info.CheckIn.Value).Days;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin chi tiết phòng: " + ex.Message);
                throw;
            }

            return info;
        }

        public void DatPhong(string maDatPhong, string maKhachHang, string maPhong, DateTime checkIn, DateTime checkOut)
        {
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    // Thêm bản ghi vào DAT_PHONG
                    string insertQuery = @"INSERT INTO DAT_PHONG (MaDatPhong, MaPhong, MaKH, CheckIn, CheckOut, TrangThai) 
                                      VALUES (@MaDatPhong, @MaPhong, @MaKH, @CheckIn, @CheckOut, @TrangThai)";
                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                        cmdInsert.Parameters.AddWithValue("@MaKH", maKhachHang);
                        cmdInsert.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmdInsert.Parameters.AddWithValue("@CheckIn", checkIn);
                        cmdInsert.Parameters.AddWithValue("@CheckOut", checkOut);
                        cmdInsert.Parameters.AddWithValue("@TrangThai", "Đã đặt");
                        cmdInsert.ExecuteNonQuery();
                    }

                    // Cập nhật TrangThai trong PHONG thành "Bận"
                    string updateQuery = @"UPDATE PHONG SET TrangThai = @TrangThai WHERE MaPhong = @MaPhong";
                    using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmdUpdate.Parameters.AddWithValue("@TrangThai", "Bận");
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đặt phòng: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public string GenerateMaDatPhong()
        {
            string maDatPhong = "DP01";
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");
                    string query = "SELECT COUNT(*) FROM DAT_PHONG";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int count = (int)cmd.ExecuteScalar() + 1;
                        maDatPhong = "DP" + count.ToString("D2");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tạo mã đặt phòng: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
            return maDatPhong;
        }

        public void CheckOut(string maDatPhong, string maNV)
        {
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");

                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Bước 1: Cập nhật trạng thái phòng thành "Trống"
                            string updatePhongQuery = @"UPDATE PHONG 
                                                SET TrangThai = N'Trống' 
                                                WHERE MaPhong = (SELECT MaPhong FROM DAT_PHONG WHERE MaDatPhong = @MaDatPhong)";
                            using (SqlCommand cmd = new SqlCommand(updatePhongQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                                cmd.ExecuteNonQuery();
                            }

                            // Bước 2: Cập nhật trạng thái đặt phòng thành "Đã trả"
                            string updateDatPhongQuery = @"UPDATE DAT_PHONG 
                                                    SET TrangThai = N'Đã trả' 
                                                    WHERE MaDatPhong = @MaDatPhong";
                            using (SqlCommand cmd = new SqlCommand(updateDatPhongQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                                cmd.ExecuteNonQuery();
                            }

                            // Bước 3: Sinh mã hóa đơn mới
                            string maHD = GenerateUniqueMaHD(conn, transaction);

                            // Bước 4: Tính tiền dịch vụ
                            decimal tienDichVu = 0;
                            string tienDichVuQuery = @"SELECT SUM(ChiPhi) 
                                                FROM SU_DUNG_DICH_VU 
                                                WHERE MaDatPhong = @MaDatPhong";
                            using (SqlCommand cmd = new SqlCommand(tienDichVuQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                                object result = cmd.ExecuteScalar();
                                tienDichVu = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                            }

                            // Bước 5: Tính tiền phòng
                            decimal tienPhong = 0;
                            string tienPhongQuery = @"SELECT DATEDIFF(DAY, DP.CheckIn, DP.CheckOut) + 1 AS SoNgay,
                                                RT.Gia + OC.Gia AS GiaPhong
                                                FROM DAT_PHONG DP
                                                JOIN PHONG P ON DP.MaPhong = P.MaPhong
                                                JOIN ROOM_TYPE RT ON P.MaLoaiPhong = RT.MaLoaiPhong
                                                JOIN OCCUPANCY OC ON P.MaSoNguoi = OC.MaSoNguoi
                                                WHERE DP.MaDatPhong = @MaDatPhong";
                            using (SqlCommand cmd = new SqlCommand(tienPhongQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        int soNgay = reader.GetInt32(0);
                                        decimal giaPhong = reader.GetDecimal(1);
                                        tienPhong = soNgay * giaPhong;
                                    }
                                }
                            }

                            // Bước 6: Tính tổng tiền
                            decimal tongTien = tienDichVu + tienPhong;

                            // Bước 7: Thêm hóa đơn mới, bao gồm TienDichVu và TienPhong
                            string insertHoaDonQuery = @"INSERT INTO HOA_DON (MaHD, MaDatPhong, MaNV, NgayLap, ThoiDiem, TienDichVu, TienPhong, TongTien, TrangThai)
                                                    VALUES (@MaHD, @MaDatPhong, @MaNV, @NgayLap, @ThoiDiem, @TienDichVu, @TienPhong, @TongTien, N'Chưa thanh toán')";
                            using (SqlCommand cmd = new SqlCommand(insertHoaDonQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaHD", maHD);
                                cmd.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                                cmd.Parameters.AddWithValue("@MaNV", maNV);
                                cmd.Parameters.AddWithValue("@NgayLap", DateTime.Now.Date);
                                cmd.Parameters.AddWithValue("@ThoiDiem", DateTime.Now.ToString("HH:mm:ss"));
                                cmd.Parameters.AddWithValue("@TienDichVu", tienDichVu);
                                cmd.Parameters.AddWithValue("@TienPhong", tienPhong);
                                cmd.Parameters.AddWithValue("@TongTien", tongTien);
                                cmd.ExecuteNonQuery();
                            }

                            // Commit transaction
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Lỗi khi thực hiện giao dịch trả phòng: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi trả phòng: " + ex.Message);
            }
        }

        // Hàm sinh mã hóa đơn không trùng lặp
        private string GenerateUniqueMaHD(SqlConnection conn, SqlTransaction transaction)
        {
            string maHD;
            string getMaxMaHDQuery = "SELECT ISNULL(MAX(MaHD), 'HD00') FROM HOA_DON";
            using (SqlCommand cmd = new SqlCommand(getMaxMaHDQuery, conn, transaction))
            {
                string maxMaHD = (string)cmd.ExecuteScalar();
                int currentNumber = int.Parse(maxMaHD.Substring(2)); // Lấy phần số từ "HD"
                maHD = "HD" + (currentNumber + 1).ToString("D2");    // Sinh mã mới
            }
            return maHD;
        }
    }
}
