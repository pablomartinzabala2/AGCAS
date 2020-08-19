using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Concesionaria.Clases;

namespace Concesionaria
{
    public partial class FrmListadoTarjeta : Form
    {
        public FrmListadoTarjeta()
        {
            InitializeComponent();
        }

        private void FrmListadoTarjeta_Load(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime Fecha = DateTime.Now;
            txtFecha.Text = Fecha.ToShortDateString();
            txtFechaHasta.Text = Fecha.ToShortDateString();
            Fecha = Fecha.AddMonths(-1);
            txtFechaDesde.Text = Fecha.ToShortDateString();
            fun.LlenarCombo(cmbTarjeta, "Tarjeta", "Nombre", "CodTarjeta");
        }

        private void Mensaje(string Msj)
        {
            MessageBox.Show(Msj, "Sistema");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                Mensaje("La fecha desde es incorrecta");
                return;
            }

            if (fun.ValidarFecha(txtFechaHasta.Text) == false)
            {
                Mensaje("La fecha hasta es incorrecta");
                return;
            }
            CargarGrilla();
            
        }

        private void CargarGrilla()
        {
            cFunciones fun = new cFunciones();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            Int32? CodTarjeta = null;
            if (cmbTarjeta.SelectedIndex > 0)
                CodTarjeta = Convert.ToInt32(cmbTarjeta.SelectedValue);
            string Patente = txtPatente.Text;
            cTarjeta tarj = new cTarjeta();
            DataTable trdo = tarj.GetVentaxTarjeta(FechaDesde, FechaHasta, CodTarjeta, Patente);
            Double Total = fun.TotalizarColumna(trdo, "Importe");
            trdo = fun.TablaaMiles(trdo, "Importe");
            trdo = fun.TablaaMiles(trdo, "Saldo");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Visible = false;
            Grilla.Columns[3].Width = 250;
            Grilla.Columns[5].Width = 250;
            Grilla.Columns[4].Width = 130;
            Grilla.Columns[6].Width = 120;
            txtTotal.Text = Total.ToString();
            txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
        }

        private void btnCobro_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones ();
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar una fila para continuar");
                return;
            }
            string sFecha = Grilla.CurrentRow.Cells[7].Value.ToString();
            if (sFecha != "")
            {
                Mensaje("Ya se ha realizado el cobro");
                return;
            }
            if (fun.ValidarFecha (txtFecha.Text)==false)
            {
                Mensaje ("La fecha ingresada es incorrecta");
                return ;
            }
            DateTime Fecha = Convert.ToDateTime (txtFecha.Text);
            Int32 CodVenta = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            Int32 CodTarjeta = Convert.ToInt32(Grilla.CurrentRow.Cells[1].Value.ToString());
            Double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[4].Value.ToString());
            string Patente = Grilla.CurrentRow.Cells[2].Value.ToString();
            cTarjeta tarjeta = new cTarjeta();
            tarjeta.RegistrarCobro(CodVenta, CodTarjeta,Fecha);
            string Descripcion = "Cobro de tarjeta , Patente " + txtPatente.Text ;
            cMovimiento mov = new cMovimiento();
            mov.RegistrarMovimientoDescripcion (CodVenta ,Principal.CodUsuarioLogueado ,Importe ,0,0,0,0,Fecha ,Descripcion);
            Mensaje ("Datos grabados Correctamente");
            CargarGrilla ();
        }

        private void btnBuscarTarjeta_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                Mensaje("La fecha desde es incorrecta");
                return;
            }

            if (fun.ValidarFecha(txtFechaHasta.Text) == false)
            {
                Mensaje("La fecha hasta es incorrecta");
                return;
            }
            CargarGrilla();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar una fila para continuar");
                return;
            }
            string sFecha = Grilla.CurrentRow.Cells[7].Value.ToString();
            if (sFecha == "")
            {
                Mensaje("Debe realizar el cobro antes de anular");
                return;
            }

            string msj = "Confirma anular el pago de la tarjeta ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }

            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            Int32 CodVenta = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            Int32 CodTarjeta = Convert.ToInt32(Grilla.CurrentRow.Cells[1].Value.ToString());
            Double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[4].Value.ToString());
            string Patente = Grilla.CurrentRow.Cells[2].Value.ToString();
            cTarjeta tarjeta = new cTarjeta();
            tarjeta.AnularCobro(CodVenta, CodTarjeta);
            string Descripcion = "Anulación Cobro de tarjeta , Patente " + txtPatente.Text;
            cMovimiento mov = new cMovimiento();
            mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado,(-1)* Importe, 0, 0, 0, 0, Fecha, Descripcion);
            Mensaje("Datos grabados Correctamente");
            CargarGrilla();
        }
    }
}
