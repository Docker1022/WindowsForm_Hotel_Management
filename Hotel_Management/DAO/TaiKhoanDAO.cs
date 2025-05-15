using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DAO
{
    public class TaiKhoanDAO
    {
        public DataTable LayDanhSachTaiKhoan()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");

                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT MaNV, TenDangNhap, MatKhau FROM TAI_KHOAN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách tài khoản: " + ex.Message);
            }
            return dt;
        }

        public void ThemTaiKhoan(string maNV, string tenDangNhap, string matKhau)
        {
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");

                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    // Bước 1: Kiểm tra xem MaNV có tồn tại trong bảng NHAN_VIEN không
                    string checkNhanVienQuery = "SELECT COUNT(*) FROM NHAN_VIEN WHERE MaNV = @MaNV";
                    using (SqlCommand checkCmd = new SqlCommand(checkNhanVienQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaNV", maNV);
                        int nhanVienCount = (int)checkCmd.ExecuteScalar();
                        if (nhanVienCount == 0)
                            throw new Exception("Mã nhân viên không tồn tại trong bảng NHAN_VIEN!");
                    }

                    // Bước 2: Kiểm tra xem TenDangNhap đã tồn tại chưa (đảm bảo tính duy nhất)
                    string checkTenDangNhapQuery = "SELECT COUNT(*) FROM TAI_KHOAN WHERE TenDangNhap = @TenDangNhap";
                    using (SqlCommand checkCmd = new SqlCommand(checkTenDangNhapQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        int tenDangNhapCount = (int)checkCmd.ExecuteScalar();
                        if (tenDangNhapCount > 0)
                            throw new Exception("Tên đăng nhập đã tồn tại!");
                    }

                    // Bước 3: Thêm tài khoản mới vào bảng TAI_KHOAN
                    string insertQuery = @"INSERT INTO TAI_KHOAN (MaNV, TenDangNhap, MatKhau) 
                                    VALUES (@MaNV, @TenDangNhap, @MatKhau)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm tài khoản: " + ex.Message);
            }
        }

        public void XoaTaiKhoan(string maNV)
        {
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");

                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    // Xóa tài khoản dựa trên MaNV
                    string deleteQuery = "DELETE FROM TAI_KHOAN WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                            throw new Exception("Không tìm thấy tài khoản với Mã nhân viên: " + maNV);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa tài khoản: " + ex.Message);
            }
        }

        public void CapNhatTaiKhoan(string maNV, string tenDangNhap, string matKhau)
        {
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");

                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    // Kiểm tra xem MaNV có tồn tại trong bảng TAI_KHOAN không
                    string checkQuery = "SELECT COUNT(*) FROM TAI_KHOAN WHERE MaNV = @MaNV";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaNV", maNV);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                            throw new Exception("Tài khoản với Mã nhân viên " + maNV + " không tồn tại!");
                    }

                    // Kiểm tra xem TenDangNhap đã tồn tại cho một MaNV khác không (trừ chính nó)
                    string checkTenDangNhapQuery = "SELECT COUNT(*) FROM TAI_KHOAN WHERE TenDangNhap = @TenDangNhap AND MaNV != @MaNV";
                    using (SqlCommand checkCmd = new SqlCommand(checkTenDangNhapQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        checkCmd.Parameters.AddWithValue("@MaNV", maNV);
                        int tenDangNhapCount = (int)checkCmd.ExecuteScalar();
                        if (tenDangNhapCount > 0)
                            throw new Exception("Tên đăng nhập đã được sử dụng bởi tài khoản khác!");
                    }

                    // Cập nhật tài khoản
                    string updateQuery = @"UPDATE TAI_KHOAN 
                                    SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau 
                                    WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật tài khoản: " + ex.Message);
            }
        }

        public DataTable TimKiemTaiKhoan(string tenDangNhap)
        {
            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn == null) throw new Exception("Kết nối cơ sở dữ liệu không thành công!");
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    // Tìm kiếm tài khoản dựa trên TenDangNhap
                    string searchQuery = "SELECT * FROM TAI_KHOAN WHERE TenDangNhap LIKE @TenDangNhap";
                    using (SqlCommand cmd = new SqlCommand(searchQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", "%" + tenDangNhap + "%");
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            // Trả về DataTable chứa kết quả tìm kiếm
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm tài khoản: " + ex.Message);
            }
        }
    }
}
