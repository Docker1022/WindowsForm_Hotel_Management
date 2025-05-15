using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Hotel_Management
{
    public class DBConnect
    {
        private static readonly string connectionString;
        private static SqlConnection conn;

        // Khởi tạo static constructor để bắt lỗi rõ hơn
        static DBConnect()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đọc chuỗi kết nối từ App.config: " + ex.Message);
            }
        }

        // Mở kết nối
        public static SqlConnection GetConnection()
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Chuỗi kết nối bị trống hoặc null. Vui lòng kiểm tra khai báo connectionString.");
                }

                conn = new SqlConnection(connectionString);
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                return conn;
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
        }

        // Đóng kết nối
        public static void CloseConnection()
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }
}
