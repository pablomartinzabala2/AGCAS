using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Concesionaria
{
    public partial class FrmReporteVenta : Form
    {
        public FrmReporteVenta()
        {
            InitializeComponent();
        }

        private void FrmReporteVenta_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'COPIACONCESIONARIADataSet.ReporteAuto' table. You can move, or remove it, as needed.
            this.ReporteAutoTableAdapter.Fill(this.COPIACONCESIONARIADataSet.ReporteAuto);

            this.reportViewer1.RefreshReport();
        }
    }
}
