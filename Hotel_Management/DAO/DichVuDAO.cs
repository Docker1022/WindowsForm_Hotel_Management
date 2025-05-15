using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hotel_Management
{
    public class DichVuDAO
    {
        public List<DichVuDTO> GetAllDichVu()
        {
            List<DichVuDTO> dichVuList = new List<DichVuDTO>();
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT ID, Ten, Gia FROM DICH_VU";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dichVuList.Add(new DichVuDTO
                                {
                                    MaDV = reader.GetInt32(reader.GetOrdinal("ID")),
                                    TenDV = reader["Ten"].ToString(),
                                    DonGia = reader.GetDecimal(reader.GetOrdinal("Gia"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách dịch vụ: " + ex.Message);
                throw;
            }
            return dichVuList;
        }

        public List<DichVuDTO> GetDichVuByMaDatPhong(string maDatPhong)
        {
            List<DichVuDTO> dichVuList = new List<DichVuDTO>();
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = @"
                        SELECT S.ID, S.IDDichVu, D.Ten, S.NgaySuDung, S.ThoiDiem, S.ChiPhi
                        FROM SU_DUNG_DICH_VU S
                        JOIN DICH_VU D ON S.IDDichVu = D.ID
                        WHERE S.MaDatPhong = @MaDatPhong";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dichVuList.Add(new DichVuDTO
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("ID")), // Lấy cột ID
                                    MaDV = reader.GetInt32(reader.GetOrdinal("IDDichVu")),
                                    TenDV = reader["Ten"].ToString(),
                                    NgaySuDung = reader.GetDateTime(reader.GetOrdinal("NgaySuDung")),
                                    ThoiDiem = reader["ThoiDiem"].ToString(),
                                    DonGia = reader.GetDecimal(reader.GetOrdinal("ChiPhi"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dịch vụ theo mã đặt phòng: " + ex.Message);
                throw;
            }
            return dichVuList;
        }

        // Thêm sử dụng dịch vụ
        public bool ThemSuDungDichVu(string maDatPhong, int idDichVu, DateTime ngaySuDung, string thoiDiem, decimal chiPhi)
        {
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");
                    string query = @"INSERT INTO SU_DUNG_DICH_VU (MaDatPhong, IDDichVu, NgaySuDung, ThoiDiem, ChiPhi) 
                                 VALUES (@MaDatPhong, @IDDichVu, @NgaySuDung, @ThoiDiem, @ChiPhi)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                        cmd.Parameters.AddWithValue("@IDDichVu", idDichVu);
                        cmd.Parameters.AddWithValue("@NgaySuDung", ngaySuDung);
                        cmd.Parameters.AddWithValue("@ThoiDiem", thoiDiem);
                        cmd.Parameters.AddWithValue("@ChiPhi", chiPhi);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        // Xóa dịch vụ đã sử dụng
        public int XoaSuDungDichVu(string maDatPhong, int idDichVu, DateTime ngaySuDung, string thoiDiem)
        {
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = @"DELETE FROM SU_DUNG_DICH_VU 
                             WHERE MaDatPhong = @MaDatPhong 
                             AND IDDichVu = @IDDichVu 
                             AND NgaySuDung = @NgaySuDung 
                             AND ThoiDiem = @ThoiDiem";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDatPhong", maDatPhong);
                        cmd.Parameters.AddWithValue("@IDDichVu", idDichVu);
                        cmd.Parameters.AddWithValue("@NgaySuDung", ngaySuDung);
                        cmd.Parameters.AddWithValue("@ThoiDiem", thoiDiem);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
