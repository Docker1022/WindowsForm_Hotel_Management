using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management
{
    public class PhongDTO
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public string Tang { get; set; }
        public decimal GiaPhong { get; set; }
        public string LoaiPhongDayDu { get; set; } // VD: "Suite Double Room"
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool TinhTrang { get; set; } // true = Bận, false = Trống

        public PhongDTO() { }

        public PhongDTO(string maPhong, string tenPhong, string loaiPhongDayDu, bool tinhTrang)
        {
            MaPhong = maPhong;
            TenPhong = tenPhong;
            LoaiPhongDayDu = loaiPhongDayDu;
            TinhTrang = tinhTrang;
        }
    }
}
