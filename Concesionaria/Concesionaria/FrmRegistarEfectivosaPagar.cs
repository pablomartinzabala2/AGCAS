using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Concesionaria
{
    public partial class FrmRegistarEfectivosaPagar : Form
    {
        Clases.cFunciones fun;
        public FrmRegistarEfectivosaPagar()
        {
            InitializeComponent();
            fun = new Clases.cFunciones();
           
        }

        private void FrmRegistarEfectivosaPagar_Load(object sender, EventArgs e)
        {
            if (Principal.CodigoPrincipalAbm !=null)
                CargarDatos (Convert.ToInt32 (Principal.CodigoPrincipalAbm)); 
        }

        private void CargarDatos(Int32 CodRegistro)
        {
            Clases.cEfectivoaPagar obj = new Clases.cEfectivoaPagar ();
            DataTable trdo = obj.GetEfectivosaPagarxCodigo(CodRegistro);
            if (trdo.Rows.Count >0)
            {
                string cliente = trdo.Rows[0]["Nombre"].ToString ();
                cliente = cliente + " " + trdo.Rows[0]["Apellido"].ToString ();
                txtCliente.Text = cliente ;
                string auto =trdo.Rows[0]["Patente"].ToString ();
                auto = auto + " " + trdo.Rows[0]["Descripcion"].ToString ();
                txtDescripcion.Text = auto;
                txtPatente.Text = trdo.Rows[0]["Patente"].ToString();
                txtImporte.Text = trdo.Rows[0]["Importe"].ToString (); 
                txtSaldo.Text = trdo.Rows[0]["Saldo"].ToString ();
                 txtFecha.Text =trdo.Rows[0]["FechaPago"].ToString ();
                 
                 if (txtImporte.Text != "")
                 {
                     txtImporte.Text = fun.SepararDecimales(txtImporte.Text);
                     txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
                 }

                 if (txtSaldo.Text != "")
                 {
                     txtSaldo.Text = fun.SepararDecimales(txtSaldo.Text);
                     txtSaldo.Text = fun.FormatoEnteroMiles(txtSaldo.Text);
                 }
                 txtIngresarImporte.Focus();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (fun.ValidarFecha(txtFecha.Text)==false)
            {
                Mensaje("La fecha ingresada es incorrecta");
                return;
            }

            if (txtIngresarImporte.Text == "")
            {
                Mensaje("Debe ingresar un importe para continuar");
                return;
            }
            
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            Int32 CodRegistro = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            double Importe = fun.ToDouble(txtIngresarImporte.Text);
            double Saldo = fun.ToDouble(txtSaldo.Text);
            double aPagar = fun.ToDouble(txtIngresarImporte.Text);
            double ImporteInicial = fun.ToDouble(txtImporte.Text);
            if (aPagar > Saldo)
            {
                Mensaje("El Importe a pagar supera el saldo");
                return;
            }
            Clases.cSaldoEfectivoPagar objSaldo = new Clases.cSaldoEfectivoPagar();
           
            Clases.cEfectivoaPagar obj = new Clases.cEfectivoaPagar();
            obj.ActualizarPago(CodRegistro, Fecha, Importe);
            string Descripcion = "PAGO EFECTIVO " + txtCliente.Text;
            objSaldo.InsertarSaldo(CodRegistro, Fecha, Importe);
            Descripcion = Descripcion + ", PATENTE " + txtPatente.Text;
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, -1 * Importe, 0, 0, 0, 0, Fecha, Descripcion);
            Mensaje("Datos grabados correctamente");
            txtImporte.Text = "";
            btnGrabar.Enabled = false; 
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, Clases.cMensaje.Mensaje());
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                Mensaje("La fecha ingresada es incorrecta");
                return;
            }
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            Int32 CodRegistro = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            Clases.cEfectivoaPagar obj = new Clases.cEfectivoaPagar();
            string Descripcion = "ANULACION DE PAGO A " + txtCliente.Text;
            Descripcion = Descripcion + ", PATENTE " + txtPatente.Text;
            double Total = fun.ToDouble(txtImporte.Text);
            double Saldo = fun.ToDouble(txtSaldo.Text);
            double Pagado = Total - Saldo;

            if (Total == Saldo)
            {
                Mensaje("No hay registros para anular");
                return;
            }

            Clases.cSaldoEfectivoPagar saldoEf = new Clases.cSaldoEfectivoPagar();
            obj.Anular(CodRegistro);
            saldoEf.Borrar(CodRegistro);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Convert.ToInt32(Principal.CodigoPrincipalAbm), Pagado, 0, 0, 0, 0, Fecha, Descripcion);
            Mensaje("Datos grabados correctamente");
            btnAnular.Enabled = false;
            btnGrabar.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDetalleSaldosEfectivosaPagar frm = new FrmDetalleSaldosEfectivosaPagar();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMensajeEfectivo frm = new FrmMensajeEfectivo();
            frm.ShowDialog();
        }
    }
}
