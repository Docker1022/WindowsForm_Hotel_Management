namespace Hotel_Management.Forms
{
    partial class LoadReportViewBaoCao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewerDoanhThu = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ctbThoat = new Guna.UI2.WinForms.Guna2ControlBox();
            this.SuspendLayout();
            // 
            // reportViewerDoanhThu
            // 
            this.reportViewerDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerDoanhThu.DocumentMapWidth = 19;
            this.reportViewerDoanhThu.Location = new System.Drawing.Point(0, 0);
            this.reportViewerDoanhThu.Name = "reportViewerDoanhThu";
            this.reportViewerDoanhThu.ServerReport.BearerToken = null;
            this.reportViewerDoanhThu.Size = new System.Drawing.Size(900, 600);
            this.reportViewerDoanhThu.TabIndex = 21;
            this.reportViewerDoanhThu.Load += new System.EventHandler(this.reportViewerDoanhThu_Load);
            // 
            // ctbThoat
            // 
            this.ctbThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctbThoat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(152)))), ((int)(((byte)(166)))));
            this.ctbThoat.IconColor = System.Drawing.Color.White;
            this.ctbThoat.Location = new System.Drawing.Point(855, 0);
            this.ctbThoat.Name = "ctbThoat";
            this.ctbThoat.Size = new System.Drawing.Size(45, 29);
            this.ctbThoat.TabIndex = 22;
            this.ctbThoat.Click += new System.EventHandler(this.ctbThoat_Click);
            // 
            // LoadReportViewBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.ctbThoat);
            this.Controls.Add(this.reportViewerDoanhThu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadReportViewBaoCao";
            this.Text = "LoadReportViewBaoCao";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerDoanhThu;
        private Guna.UI2.WinForms.Guna2ControlBox ctbThoat;
    }
}