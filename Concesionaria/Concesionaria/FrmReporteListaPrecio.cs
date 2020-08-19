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
    public partial class FrmReporteListaPrecio : Form
    {
        public FrmReporteListaPrecio()
        {
            InitializeComponent();
        }

        private void FrmReporteListaPrecio_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'CONCESIONARIADataSet.ReporteAuto' table. You can move, or remove it, as needed.
            this.ReporteAutoTableAdapter.Fill(this.CONCESIONARIADataSet.ReporteAuto);
            // TODO: This line of code loads data into the 'CONCESIONARIADataSet.Reporte' table. You can move, or remove it, as needed.
            this.ReporteTableAdapter.Fill(this.CONCESIONARIADataSet.Reporte);

            this.reportViewer1.RefreshReport();
        }
    }
}
