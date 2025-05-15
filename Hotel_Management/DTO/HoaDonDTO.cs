using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management
{
    public class HoaDonDTO
    {
        public string MaHoaDon { get; set; }
        public string MaDatPhong { get; set; }
        public string MaPhong { get; set; }
        public string LoaiPhong { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public DateTime NgayLap { get; set; }
        public string HoKH { get; set; }
        public string TenKH { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string QuocTich { get; set; }
        public decimal TienDichVu { get; set; }

        public decimal TienPhong { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }

        public int SoNgayO { get; set; }
        public decimal GiaPhong { get; set; }
    }
}
