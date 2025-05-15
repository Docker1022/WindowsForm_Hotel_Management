using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management.Forms
{
    public partial class LoadReportViewBaoCao : Form
    {
        public LoadReportViewBaoCao()
        {
            InitializeComponent();
        }

        private void reportViewerDoanhThu_Load(object sender, EventArgs e)
        {

        }

        public void HienThiBaoCao(string reportPath, string datasetName, DataTable dt)
        {
            reportViewerDoanhThu.ProcessingMode = ProcessingMode.Local;
            reportViewerDoanhThu.LocalReport.ReportPath = reportPath;

            reportViewerDoanhThu.LocalReport.DataSources.Clear();
            reportViewerDoanhThu.LocalReport.DataSources.Add(new ReportDataSource(datasetName, dt));

            reportViewerDoanhThu.RefreshReport();
        }

        private void ctbThoat_Click(object sender, EventArgs e)
        {
            LoadReportViewBaoCao.ActiveForm.Close();
        }
    }
}
