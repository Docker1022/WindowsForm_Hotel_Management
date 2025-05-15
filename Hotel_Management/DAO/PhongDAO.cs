using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management
{
    public class PhongDAO
    {
        public static List<PhongDTO> LayDanhSachPhong(string loaiPhongDayDu = "")
        {
            List<PhongDTO> danhSach = new List<PhongDTO>();
            string query = @"
                            SELECT P.MaPhong, P.SoPhong,
                                   CONCAT(RT.Ten, ' ', O.Ten) AS TenLoaiDayDu,
                                   P.TrangThai
                            FROM PHONG P
                            JOIN ROOM_TYPE RT ON P.MaLoaiPhong = RT.MaLoaiPhong
                            JOIN OCCUPANCY O ON P.MaSoNguoi = O.MaSoNguoi";

            if (!string.IsNullOrEmpty(loaiPhongDayDu))
                query += " WHERE CONCAT(RT.Ten, ' ', O.Ten) = @LoaiPhongDayDu";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(loaiPhongDayDu))
                            cmd.Parameters.AddWithValue("@LoaiPhongDayDu", loaiPhongDayDu);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string maPhong = reader["MaPhong"].ToString();
                                string soPhong = reader["SoPhong"].ToString();
                                string loai = reader["TenLoaiDayDu"].ToString();
                                string trangThai = reader["TrangThai"].ToString();

                                danhSach.Add(new PhongDTO(maPhong, soPhong, loai, trangThai == "Bận"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách phòng: " + ex.Message);
                throw;
            }

            return danhSach;
        }
        public static List<string> LayDanhSachLoaiPhongDayDu()
        {
            List<string> loaiPhong = new List<string>();
            string query = @"
                            SELECT DISTINCT CONCAT(RT.Ten, ' ', O.Ten) AS TenLoaiDayDu
                        FROM ROOM_TYPE RT
                        CROSS JOIN OCCUPANCY O
                        WHERE CONCAT(RT.Ten, ' ', O.Ten) NOT IN (
                            'Suite Single Room',
                            'Suite Twin Room',
                            'Suite Triple Room',
                            'Executive Suite Single Room',
                            'Executive Suite Twin Room',
                            'Executive Suite Triple Room',
                            'Presidential Suite Single Room',
                            'Presidential Suite Twin Room',
                            'Presidential Suite Triple Room',
                            'Presidential Suite Family Room'
                        )";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                loaiPhong.Add(reader["TenLoaiDayDu"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách loại phòng: " + ex.Message);
                throw;
            }

            return loaiPhong;
        }

        public static PhongDTO LayThongTinChiTietPhong(string maPhong)
        {
            PhongDTO phongInfo = null;
            string query = @"
                            SELECT P.MaPhong, P.SoPhong, 
                                   CONCAT(RT.Ten, ' ', O.Ten) AS TenLoaiDayDu, 
                                   P.Tang,
                                   (RT.Gia + O.Gia) AS GiaPhong
                            FROM PHONG P
                            JOIN ROOM_TYPE RT ON P.MaLoaiPhong = RT.MaLoaiPhong
                            JOIN OCCUPANCY O ON P.MaSoNguoi = O.MaSoNguoi
                            WHERE P.MaPhong = @MaPhong";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                phongInfo = new PhongDTO
                                {
                                    MaPhong = reader["MaPhong"].ToString(),
                                    LoaiPhong = reader["TenLoaiDayDu"].ToString(),
                                    Tang = reader["Tang"].ToString(),
                                    GiaPhong = reader.GetDecimal(reader.GetOrdinal("GiaPhong"))
                                };
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

            return phongInfo;
        }

    }
}
