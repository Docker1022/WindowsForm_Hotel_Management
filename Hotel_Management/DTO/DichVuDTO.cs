using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management
{
    public class DichVuDTO
    {
        public int ID { get; set; }
        public int MaDV { get; set; }
        public string TenDV { get; set; }
        public DateTime NgaySuDung { get; set; }
        public string ThoiDiem { get; set; }
        public int SoLuong { get; set; }
        public Decimal DonGia { get; set; }
        public Decimal ThanhTien { get; set; }

    }
}
