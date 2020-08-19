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
    public partial class FrmListadoAnotaciones : Form
    {
        Clases.cFunciones fun;
        public FrmListadoAnotaciones()
        {
            InitializeComponent();
        }

        private void FrmListadoAnotaciones_Load(object sender, EventArgs e)
        {
            fun = new Clases.cFunciones();
            DateTime fechahoy = DateTime.Now;
            txtFechaHasta.Text = fechahoy.ToShortDateString();
            fechahoy = fechahoy.AddMonths(-1);
            txtFechaDesde.Text = fechahoy.ToShortDateString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                Mensaje("La fecha desde es incorrecta");
                return;
            }

            if (fun.ValidarFecha(txtFechaHasta.Text) == false)
            {
                Mensaje("La fecha desde es incorrecta");
                return;
            }
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            if (FechaDesde > FechaHasta)
            {
                Mensaje("La fecha desde es superior a la fecha hasta ");
                return;
            }

            Clases.cAnotacion obj = new Clases.cAnotacion();
            DataTable trdo = obj.GetAnotacionesxFecha(FechaDesde, FechaHasta, txtConcepto.Text.Trim ());
            double TotalIngreso = fun.TotalizarColumna(trdo, "ImporteIngreso");
            double TotalEgreso = fun.TotalizarColumna(trdo, "ImporteEgreso");
            trdo = fun.TablaaMiles(trdo, "ImporteIngreso");
            trdo = fun.TablaaMiles(trdo, "ImporteEgreso");
            Grilla.DataSource = trdo;
            double dif = TotalIngreso - TotalEgreso;
            txtTotal.Text = dif.ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
            Grilla.Columns[1].HeaderText = "Descripción";
            Grilla.Columns[3].HeaderText = "Ingreso";
            Grilla.Columns[4].HeaderText = "Egreso";
            Grilla.Columns[1].Width = 500;
            Grilla.Columns[3].Width = 100;
            Grilla.Columns[4].Width = 103;
            Grilla.Columns[0].Visible = false; 

        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, Clases.cMensaje.Mensaje());
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un registro");
                return;
            }

            string msj = "Confirma eliminar el Cliente ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }
            Int32 CodAnotacion = Convert.ToInt32 (Grilla.CurrentRow.Cells[0].Value.ToString ());
            Clases.cAnotacion obj = new Clases.cAnotacion();
            obj.Borrar(CodAnotacion);
            Buscar();
        }
    }
}
