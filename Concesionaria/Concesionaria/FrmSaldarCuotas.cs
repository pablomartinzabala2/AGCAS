using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Concesionaria.Clases;
using System.Data.SqlClient;
namespace Concesionaria
{
    public partial class FrmSaldarCuotas : Form
    {
        cFunciones fun;
        DataTable tbCuotas;
        public FrmSaldarCuotas()
        {
            InitializeComponent();
        }

        private void FrmSaldarCuotas_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            if (Principal.CodigoPrincipalAbm != null)
            {
                Int32 CodVenta = Convert.ToInt32(Principal.CodigoPrincipalAbm);
                BuscarCuotas(CodVenta);
                BuscarPatente(CodVenta);
            }
            
            fun = new cFunciones();
            tbCuotas = new DataTable();
            tbCuotas.Columns.Add("CodVenta");
            tbCuotas.Columns.Add("Cuota");
            tbCuotas.Columns.Add("Importe");
        }

        private void BuscarPatente(Int32 CodVenta)
        {
            Clases.cVenta venta = new Clases.cVenta();
            DataTable trdo = venta.GetVentaxCodigo(CodVenta);
            if (trdo.Rows.Count > 0)
                txtPatente.Text = trdo.Rows[0]["Patente"].ToString();
        }

        private void BuscarCuotas(Int32 CodVenta)
        {
            
            cCuota cuota = new cCuota();
            DataTable trdo = cuota.GetCuotasAdeudadasxCodVenta(CodVenta);
            Grilla.DataSource = trdo;
            if (Grilla.Rows.Count > 0)
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 70;
            Grilla.Columns[2].HeaderText = "Importe";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar una cuota");
                return;
            }
            string Valor = Grilla.CurrentRow.Cells[0].Value.ToString();
            Valor = Valor + ";" + Grilla.CurrentRow.Cells[1].Value.ToString();
            Valor = Valor + ";" + Grilla.CurrentRow.Cells[2].Value.ToString();
            if (fun.Buscar(tbCuotas, "Cuota", Grilla.CurrentRow.Cells[1].Value.ToString()) == false)
            {
                tbCuotas = fun.AgregarFilas(tbCuotas, Valor);
                GrillaCuotas.DataSource = tbCuotas;
            }
            else
            {
                Mensaje("Ya se ha selecionado la cuota");
            }
            CalcularTotales();
            if (GrillaCuotas.Rows.Count >0)
            GrillaCuotas.Columns[0].Visible = false;
            GrillaCuotas.Columns[0].Visible = false;
            GrillaCuotas.Columns[1].Width = 70;
            GrillaCuotas.Columns[2].HeaderText = "Importe";
         }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, Clases.cMensaje.Mensaje());
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (GrillaCuotas.CurrentRow == null)
            {
                Mensaje("Debe seleccionar una cuota");
                return;
            }
            string Cuota = GrillaCuotas.CurrentRow.Cells[1].Value.ToString ();
            tbCuotas = fun.EliminarFila (tbCuotas ,"Cuota",Cuota);
            GrillaCuotas.DataSource = tbCuotas;
            CalcularTotales();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CalcularTotales()
        {
            int Cantidad = GrillaCuotas.Rows.Count -1;
            txtCuotas.Text = Cantidad.ToString();
            txtTotal.Text = fun.TotalizarColumna(tbCuotas, "Importe").ToString();
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (tbCuotas.Rows.Count < 1)
            {
                Mensaje("Debe tener cuotas a cancelar");
                return;
            }
             cCuota objCuota = new cCuota ();
             Int32 CodVenta = 0;
             Double TotalCobrado = 0;
            Int32 Cuota = 0;
            Double Importe = 0;
            DateTime FechaPago = Convert.ToDateTime(txtFecha.Text);
            int Saldo = 0;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Clases.cConexion.Cadenacon();
            con.Open();
             SqlTransaction Transaccion;
            Transaccion = con.BeginTransaction();
            try
            {
                for (int i = 0; i < GrillaCuotas.Rows.Count - 1; i++)
                {
                    Cuota = Convert.ToInt32(GrillaCuotas.Rows[i].Cells[1].Value.ToString ());
                    CodVenta = Convert.ToInt32(GrillaCuotas.Rows[i].Cells[0].Value.ToString());
                    Importe = Convert.ToDouble(GrillaCuotas.Rows[i].Cells[2].Value.ToString());
                    objCuota.GrabarCuotaTransaccion(con, Transaccion  , CodVenta, Cuota, FechaPago, Importe, Saldo);
                }
                TotalCobrado = fun.ToDouble(txtTotal.Text);
                string Descrip = "COBRO DE DOCUMENTO, PATENTE: " + txtPatente.Text;
                cMovimiento mov = new cMovimiento();
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                mov.RegistrarMovimientoDescripcionTransaccion(con, Transaccion, CodVenta,
                    Principal.CodUsuarioLogueado, TotalCobrado, 0, 0, 0, 0, Fecha,
                    Descrip, -1);
                Transaccion.Commit();
                con.Close();
                Mensaje("Datos Grabados Correctamente");
            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                con.Close();
                Mensaje("Hubo un error en el proceso de grabación");
            }
            tbCuotas.Clear();
            GrillaCuotas.DataSource = tbCuotas;
            if (Principal.CodigoPrincipalAbm != null)
            {
                Int32 CodigoVenta = Convert.ToInt32(Principal.CodigoPrincipalAbm);
                BuscarCuotas(CodigoVenta);
            }
        }

        private void BuscarPatentexCodVenta(Int32 CodVenta)
        {
            cVenta venta = new cVenta();
            DataTable trdo = venta.GetVentaxCodigo(CodVenta);
            if (trdo.Rows.Count > 0)
                txtPatente.Text = trdo.Rows[0]["Patente"].ToString();
        }
    }
}
