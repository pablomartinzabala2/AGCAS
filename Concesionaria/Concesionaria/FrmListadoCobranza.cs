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
    public partial class FrmListadoCobranza : Form
    {
        public FrmListadoCobranza()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //arreglar que no graba el cod cliente en cobranza
            //en eo form de venta
            
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                MessageBox.Show("Fecha desde incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFechaHasta.Text) == false)
            {
                MessageBox.Show("Fecha hasta incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (Convert.ToDateTime(txtFechaDesde.Text) > Convert.ToDateTime(txtFechaHasta.Text))
            {
                MessageBox.Show("La fecha desde debe ser inferior a la fecha hasta", Clases.cMensaje.Mensaje());
                return;
            }

            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            Int32 SoloImpago = 0;
            if (chkImpago.Checked == true)
                SoloImpago = 1;
            Clases.cCobranza cob = new Clases.cCobranza ();
            DataTable trdo = cob.GetCobranzaxFecha(FechaDesde, FechaHasta, txtPatente.Text, txtApellido.Text, SoloImpago);
            trdo = fun.TablaaMiles(trdo, "Saldo");
            txtTotal.Text = fun.TotalizarColumna(trdo, "Saldo").ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
            trdo = fun.TablaaMiles(trdo, "Importe");
            Grilla.DataSource = trdo;
            Grilla.Columns[4].HeaderText = "Fecha Pago";
            Grilla.Columns[6].HeaderText = "Fecha Comp.";
            Grilla.Columns[1].Width = 150;
            Grilla.Columns[2].Width = 200;
            Grilla.Columns[4].Width = 120;
            Grilla.Columns[5].Width = 120;
            Grilla.Columns[6].Width = 158;
        }

        private void FrmListadoCobranza_Load(object sender, EventArgs e)
        {
            DateTime fechahoy = DateTime.Now;
            txtFechaHasta.Text = fechahoy.ToShortDateString();
            fechahoy = fechahoy.AddMonths(-1);
            txtFechaDesde.Text = fechahoy.ToShortDateString();
           
        }

        private void btnCobroCheque_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            string Patente = Grilla.CurrentRow.Cells[1].Value.ToString ();
            Principal.CodigoPrincipalAbm = Patente;
            FrmCobroDocumentos cobro = new FrmCobroDocumentos();
            cobro.ShowDialog();
        }
    }
}
