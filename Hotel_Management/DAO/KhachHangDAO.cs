using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management
{
    public class KhachHangDAO
    {
        public List<KhachHangDTO> GetAllKhachHang()
        {
            List<KhachHangDTO> khachHangList = new List<KhachHangDTO>();
            string query = "SELECT * FROM KHACH_HANG";

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
                                KhachHangDTO kh = new KhachHangDTO
                                {
                                    MaKH = reader["MaKH"].ToString(),
                                    Ho = reader["Ho"].ToString(),
                                    Ten = reader["Ten"].ToString(),
                                    NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                    CCCD = reader["CCCD"].ToString(),
                                    SDT = reader["SDT"].ToString(),
                                    QuocTich = reader["QuocTich"].ToString()
                                };
                                khachHangList.Add(kh);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách khách hàng: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }

            return khachHangList;
        }

        public void UpdateKhachHang(KhachHangDTO kh)
        {
            string query = "UPDATE KHACH_HANG SET Ho = @Ho, Ten = @Ten, NgaySinh = @NgaySinh, CCCD = @CCCD, SDT = @SDT, QuocTich = @QuocTich WHERE MaKH = @MaKH";
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", kh.MaKH);
                        cmd.Parameters.AddWithValue("@Ho", kh.Ho);
                        cmd.Parameters.AddWithValue("@Ten", kh.Ten);
                        cmd.Parameters.AddWithValue("@NgaySinh", kh.NgaySinh);
                        cmd.Parameters.AddWithValue("@CCCD", kh.CCCD);
                        cmd.Parameters.AddWithValue("@SDT", kh.SDT);
                        cmd.Parameters.AddWithValue("@QuocTich", kh.QuocTich);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật thông tin khách hàng: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public void DeleteKhachHang(List<string> maKHList)
        {
            string query = "DELETE FROM KHACH_HANG WHERE MaKH IN ({0})";
            string paramPlaceholders = string.Join(",", maKHList.Select((_, index) => $"@MaKH{index}"));

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(string.Format(query, paramPlaceholders), conn))
                    {
                        for (int i = 0; i < maKHList.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@MaKH{i}", maKHList[i]);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa khách hàng: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public void InsertKhachHang(KhachHangDTO kh)
        {
            string query = @"INSERT INTO KHACH_HANG (MaKH, Ho, Ten, NgaySinh, CCCD, SDT, QuocTich) 
                         VALUES (@MaKH, @Ho, @Ten, @NgaySinh, @CCCD, @SDT, @QuocTich)";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", kh.MaKH);
                        cmd.Parameters.AddWithValue("@Ho", kh.Ho);
                        cmd.Parameters.AddWithValue("@Ten", kh.Ten);
                        cmd.Parameters.AddWithValue("@NgaySinh", kh.NgaySinh);
                        cmd.Parameters.AddWithValue("@CCCD", kh.CCCD);
                        cmd.Parameters.AddWithValue("@SDT", kh.SDT);
                        cmd.Parameters.AddWithValue("@QuocTich", kh.QuocTich);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm khách hàng: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }
    }
}
