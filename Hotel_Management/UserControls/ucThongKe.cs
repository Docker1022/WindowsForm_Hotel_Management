using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management
{
    public partial class ucThongKe : UserControl
    {
        public ucThongKe()
        {
            InitializeComponent();
            ConfigureChart();
        }

        private void ConfigureChart()
        {
            chartThongKe.ChartAreas[0].AxisX.Interval = 1;
            chartThongKe.ChartAreas[0].AxisX.LabelStyle.Format = "0"; // Hiển thị số tháng (1, 2, ..., 12)
            chartThongKe.ChartAreas[0].AxisX.Title = "Tháng";
            chartThongKe.ChartAreas[0].AxisX.Minimum = 1; // Đặt giá trị nhỏ nhất là 1
            chartThongKe.ChartAreas[0].AxisX.Maximum = 12; // Đặt giá trị lớn nhất là 12
            chartThongKe.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chartThongKe.ChartAreas[0].AxisY.Title = "Doanh Thu (VND)";
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void ucThongKe_Load(object sender, EventArgs e)
        {

        }

        private void btnXemThongKe_Click(object sender, EventArgs e)
        {
            
        }

        private void chartThongKe_Click(object sender, EventArgs e)
        {

        }

        private void btnXemThongKe_Click_1(object sender, EventArgs e)
        {
            int nam = dtpThongKe.Value.Year;

            ThongKeDAO dao = new ThongKeDAO();
            var data = dao.GetDoanhThuTheoThang(nam);

            chartThongKe.Series["DoanhThu"].Points.Clear();
            for (int thang = 1; thang <= 12; thang++)
            {
                decimal doanhThu = 0;
                foreach (var item in data)
                {
                    if (item.Ngay.Month == thang)
                    {
                        doanhThu = item.DoanhThu;
                        break;
                    }
                }
                chartThongKe.Series["DoanhThu"].Points.AddXY(thang, doanhThu);
            }
        }
    }
}
