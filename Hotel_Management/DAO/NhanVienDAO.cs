using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management
{
    public class NhanVienDAO
    {
        public List<NhanVienDTO> GetAllNhanVien()
        {
            List<NhanVienDTO> nhanVienList = new List<NhanVienDTO>();
            string query = "SELECT * FROM NHAN_VIEN";

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
                                NhanVienDTO nv = new NhanVienDTO
                                {
                                    MaNV = reader["MaNV"].ToString(),
                                    Ho = reader["Ho"].ToString(),
                                    Ten = reader["Ten"].ToString(),
                                    NgaySinh = reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                                    GioiTinh = reader["GioiTinh"].ToString(),
                                    CCCD = reader["CCCD"].ToString(),
                                    SDT = reader["SDT"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    DiaChiThuongTru = reader["DiaChiThuongTru"].ToString(),
                                    Phuong = reader["Phuong"].ToString(),
                                    Quan = reader["Quan"].ToString(),
                                    ThanhPho = reader["ThanhPho"].ToString(),
                                    Tinh = reader["Tinh"].ToString(),
                                    NgayBatDau = reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                                    ChucVu = reader["ChucVu"].ToString(),
                                    CaLamViec = reader.GetInt32(reader.GetOrdinal("CaLamViec")),
                                    Luong = reader.GetDecimal(reader.GetOrdinal("Luong")),
                                    TrangThai = reader["TrangThai"].ToString()
                                };
                                nhanVienList.Add(nv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách nhân viên: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection(); // Đảm bảo đóng kết nối
            }

            return nhanVienList;
        }

        public void UpdateNhanVien(NhanVienDTO nv)
        {
            string query = @"UPDATE NHAN_VIEN 
                         SET Ho = @Ho, Ten = @Ten, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, 
                             CCCD = @CCCD, SDT = @SDT, Email = @Email, DiaChiThuongTru = @DiaChiThuongTru, 
                             Phuong = @Phuong, Quan = @Quan, ThanhPho = @ThanhPho, Tinh = @Tinh, 
                             NgayBatDau = @NgayBatDau, ChucVu = @ChucVu, CaLamViec = @CaLamViec, 
                             Luong = @Luong, TrangThai = @TrangThai
                         WHERE MaNV = @MaNV";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
                        cmd.Parameters.AddWithValue("@Ho", nv.Ho);
                        cmd.Parameters.AddWithValue("@Ten", nv.Ten);
                        cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
                        cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
                        cmd.Parameters.AddWithValue("@CCCD", nv.CCCD);
                        cmd.Parameters.AddWithValue("@SDT", nv.SDT);
                        cmd.Parameters.AddWithValue("@Email", nv.Email);
                        cmd.Parameters.AddWithValue("@DiaChiThuongTru", nv.DiaChiThuongTru);
                        cmd.Parameters.AddWithValue("@Phuong", nv.Phuong);
                        cmd.Parameters.AddWithValue("@Quan", nv.Quan);
                        cmd.Parameters.AddWithValue("@ThanhPho", nv.ThanhPho);
                        cmd.Parameters.AddWithValue("@Tinh", nv.Tinh);
                        cmd.Parameters.AddWithValue("@NgayBatDau", nv.NgayBatDau);
                        cmd.Parameters.AddWithValue("@ChucVu", nv.ChucVu);
                        cmd.Parameters.AddWithValue("@CaLamViec", nv.CaLamViec);
                        cmd.Parameters.AddWithValue("@Luong", nv.Luong);
                        cmd.Parameters.AddWithValue("@TrangThai", nv.TrangThai ?? "Đang làm việc");

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi chỉnh sửa nhân viên: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }

        public void DeleteNhanVien(List<string> maNVList)
        {
            string query = "DELETE FROM NHAN_VIEN WHERE MaNV IN ({0})";
            string paramPlaceholders = string.Join(",", maNVList.Select((_, index) => $"@MaNV{index}"));

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(string.Format(query, paramPlaceholders), conn))
                    {
                        for (int i = 0; i < maNVList.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@MaNV{i}", maNVList[i]);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa nhân viên: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }
        }
    }
}
