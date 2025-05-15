using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management
{
    public class ThongKeDAO
    {
        public List<ThongKeDTO> GetDoanhThuTheoThang(int nam)
        {
            List<ThongKeDTO> listThongKe = new List<ThongKeDTO>();
            string query = @"
            SELECT MONTH(NgayLap) AS Thang, SUM(TongTien) AS DoanhThu
            FROM HOA_DON
            WHERE YEAR(NgayLap) = @Nam
            GROUP BY MONTH(NgayLap)
            ORDER BY MONTH(NgayLap)";

            try
            {
                using (SqlConnection conn = DBConnect.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nam", nam);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int thang = Convert.ToInt32(reader["Thang"]);
                                ThongKeDTO dto = new ThongKeDTO
                                {
                                    Ngay = new DateTime(nam, thang, 1), // Tạo ngày đại diện cho tháng
                                    DoanhThu = Convert.ToDecimal(reader["DoanhThu"])
                                };
                                listThongKe.Add(dto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy doanh thu theo tháng: " + ex.Message);
            }
            finally
            {
                DBConnect.CloseConnection();
            }

            return listThongKe;
        }
    }
}
