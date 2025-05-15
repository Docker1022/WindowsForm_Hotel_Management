using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hotel_Management
{
    public partial class ucPhong : UserControl
    {
        private string tenPhong;
        private string loaiVaTrangThai;
        private bool isOccupied;
        private bool isSelected;

        public event EventHandler PhongClicked; // Sự kiện click công khai

        [Category("Custom Props")]
        public string TenPhong
        {
            get { return tenPhong; }
            set
            {
                tenPhong = value;
                lblTenPhong.Text = value;
            }
        }

        [Category("Custom Props")]
        public string LoaiVaTrangThai
        {
            get { return loaiVaTrangThai; }
            set
            {
                loaiVaTrangThai = value;
                lblLoaiTrangThai.Text = value;
            }
        }

        [Category("Custom Props")]
        public bool IsOccupied
        {
            get { return isOccupied; }
            set
            {
                isOccupied = value;
                CapNhatMauNen();
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                this.BorderStyle = isSelected ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
                CapNhatMauNen();
            }
        }

        private Label lblTenPhong;
        private Label lblLoaiTrangThai;

        public ucPhong()
        {
            InitializeComponent();
            this.Width = 150;
            this.Height = 100;
            this.BorderStyle = BorderStyle.FixedSingle;

            lblTenPhong = new Label()
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Height = 40
            };

            lblLoaiTrangThai = new Label()
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("Segoe UI", 9, FontStyle.Italic)
            };

            this.Controls.Add(lblLoaiTrangThai);
            this.Controls.Add(lblTenPhong);

            // Bắt sự kiện click
            this.Click += OnClick;
            lblTenPhong.Click += OnClick;
            lblLoaiTrangThai.Click += OnClick;
        }

        private void OnClick(object sender, EventArgs e)
        {
            PhongClicked?.Invoke(this, EventArgs.Empty);
        }

        private void CapNhatMauNen()
        {
            if (isSelected)
            {
                this.BackColor = Color.Yellow;
            }
            else
            {
                this.BackColor = isOccupied ? Color.LightCoral : Color.LightGreen;
            }
        }


        private void ucPhong_Load(object sender, EventArgs e)
        {

        }
    }
}
