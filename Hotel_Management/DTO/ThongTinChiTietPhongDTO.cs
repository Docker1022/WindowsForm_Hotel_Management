using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management
{
    public class ThongTinChiTietPhongDTO
    {
        public string MaDatPhong { get; set; }
        public string MaPhong { get; set; }
        public string SoPhong { get; set; }
        public string LoaiPhong { get; set; }
        public decimal GiaPhong { get; set; }
        public string TrangThai { get; set; }

        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public string Ho { get; set; }
        public string Ten { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string QuocTich { get; set; }
        public int SoNgayO { get; set; }
        public string MaNV { get; set; }
    }
}
