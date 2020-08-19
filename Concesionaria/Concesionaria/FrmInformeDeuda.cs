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
    public partial class FrmInformeDeuda : Form
    {
        public FrmInformeDeuda()
        {   
            InitializeComponent();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
            fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString(); 
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

            Boolean SoloImpago = false;
            if (chkImpagos.Checked == true)
                SoloImpago = true;
            //Clases.cFunciones fun = new Clases.cFunciones(); 
            Clases.cCuota cuota = new Clases.cCuota();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            DataTable trdo = cuota.GetDeuda(FechaDesde, FechaHasta, SoloImpago, txtPatente.Text, txtApellido.Text);
            trdo = fun.TablaaMiles(trdo, "Saldo");
            txtTotal.Text = fun.TotalizarColumna(trdo, "Importe").ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
            else
                txtTotal.Text = "0";
            trdo = fun.TablaaMiles(trdo, "Importe");
            trdo = fun.TablaaMiles(trdo, "ImportePagado");
            Grilla.DataSource = trdo;
            
            Grilla.Columns[2].HeaderText = "Importe Pagado";
            Grilla.Columns[6].HeaderText = "Teléfono";
            Grilla.Columns[1].Width = 150;
            Grilla.Columns[2].Width = 150;
            Grilla.Columns[3].Width = 150;
            Grilla.Columns[0].Width = 78;
            Grilla.Columns[5].Width = 150;
            Grilla.Columns[6].Width = 150;
            Grilla.Columns[7].Width = 150;
            Grilla.Columns[8].Width = 150;

            Grilla.Columns[6].Visible = false;
            Grilla.Columns[7].Visible = false;
            Grilla.Columns[8].Width = 120;
            Grilla.Columns[3].Width = 80;
            Grilla.Columns[9].Width = 100;
            Grilla.Columns[10].Width = 220;

        }

        private void btnCobroCheque_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje());
                return;
            }

            string Patente = Grilla.CurrentRow.Cells[9].Value.ToString();
            Principal.CodigoPrincipalAbm = Patente;
            FrmCobroCuotas form = new FrmCobroCuotas();
            form.ShowDialog();
        }
    }
}
