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
    public partial class FrmListadoPrenda : Form
    {
        public FrmListadoPrenda()
        {
            InitializeComponent();
            DateTime fecha = DateTime.Now;
            DateTime fecha1 = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha1.ToShortDateString();
            txtFechaHasta.Text = fecha.ToShortDateString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
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

            int Impagos = 0;
            if (chkImpagos.Checked == true)
                Impagos = 1;

            Clases.cPrenda prenda = new Clases.cPrenda();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            DataTable trdo = prenda.GetPrendasxFecha(FechaDesde, FechaHasta, Impagos,txtPatente.Text ,txtApellido.Text);
            txtTotal.Text = fun.TotalizarColumna(trdo, "Importe").ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
            trdo = fun.TablaaMiles(trdo, "Importe");
            trdo = fun.TablaaMiles(trdo, "ImportePagado");
            trdo = fun.TablaaMiles(trdo, "Diferencia");
            Grilla.DataSource = trdo;
            Grilla.Columns[1].HeaderText = "Descripción";
            Grilla.Columns[1].Width = 150;
            Grilla.Columns[2].Width = 350;  
            Grilla.Columns[5].HeaderText = "Fecha Pago";
            Grilla.Columns[5].Width = 140;
            Grilla.Columns[6].Visible = false;
            Grilla.Columns[7].HeaderText = "Pagado";
            Grilla.Columns[8].HeaderText = "Diferencia";
        }

        private void btnCobroPrenda_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            string CodigoPrenda = Grilla.CurrentRow.Cells[6].Value.ToString();
            Principal.CodigoPrincipalAbm = CodigoPrenda;
            FrmCobroPrenda form = new FrmCobroPrenda();
            form.ShowDialog();
        }

        private void FrmListadoPrenda_Load(object sender, EventArgs e)
        {

        }
    }
}
