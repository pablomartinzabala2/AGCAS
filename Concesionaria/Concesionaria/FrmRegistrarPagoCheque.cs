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
    public partial class FrmRegistrarPagoCheque : Form
    {
        public FrmRegistrarPagoCheque()
        {
            InitializeComponent();
        }

        private void FrmRegistrarPagoCheque_Load(object sender, EventArgs e)
        {
            if (Principal.CodigoPrincipalAbm != null)
            {
                Cargar(Convert.ToInt32(Principal.CodigoPrincipalAbm));
            }
        }

        private void Cargar(Int32 CodCheque)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cChequesaPagar cheque = new Clases.cChequesaPagar();
            DataTable trdo = cheque.GetChequesPagarxCodigo(CodCheque);
            if (trdo.Rows.Count > 0)
            {
                txtNroCheque.Text = trdo.Rows[0]["NroCheque"].ToString();
                txtImporte.Text = trdo.Rows[0]["Importe"].ToString();
                txtCliente.Text = trdo.Rows[0]["Nombre"].ToString();
                txtCliente.Text = txtCliente.Text + " " + trdo.Rows[0]["Apellido"].ToString();
                txtPatente.Text = trdo.Rows[0]["Patente"].ToString();
                txtSaldo.Text = trdo.Rows[0]["Saldo"].ToString();
                if (trdo.Rows[0]["FechaPago"].ToString() != "")
                {
                    DateTime Fecha = Convert.ToDateTime(trdo.Rows[0]["FechaPago"].ToString());
                    txtFecha.Text = Fecha.ToShortDateString();
                    
                }

                if (trdo.Rows[0]["FechaVencimiento"].ToString() != "")
                {  
                    DateTime Fecha = Convert.ToDateTime(trdo.Rows[0]["FechaVencimiento"].ToString());
                    txtFechaVto.Text = Fecha.ToShortDateString();

                }

                if (txtSaldo.Text != "")
                {
                    txtSaldo.Text = fun.SepararDecimales(txtSaldo.Text);
                    txtSaldo.Text = fun.FormatoEnteroMiles(txtSaldo.Text);
                }

                if (txtImporte.Text != "")
                {
                    txtImporte.Text = fun.SepararDecimales(txtImporte.Text);
                    txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
                }

                if (txtSaldo.Text == "0")
                {
                    btnGrabar.Enabled = false;
                }
            }

            Clases.cPagoCheque pago = new Clases.cPagoCheque();
            DataTable tresul = pago.GetPagosCheques(CodCheque);
            tresul = fun.TablaaMiles(tresul, "Importe");
            Grilla.DataSource = tresul;
           // Grilla.Columns[0].Visible = false;
          //  Grilla.Columns[1].Visible = false;
            Grilla.Columns[2].Width = 280;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtImporteAPagar.Text == "")
            {
                MessageBox.Show("Debe ingresar un importe a pagar", Clases.cMensaje.Mensaje());
                return;
            }
            double Importe = fun.ToDouble(txtImporteAPagar.Text);
            double Saldo = fun.ToDouble(txtSaldo.Text);
            if (Importe > Saldo)
            {
                MessageBox.Show("El monto a pagar supera el sado", Clases.cMensaje.Mensaje());
                return;
            }
            Int32 CodCheque = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            Clases.cPagoCheque pagoCheque = new Clases.cPagoCheque(); 
            Clases.cChequesaPagar cheque = new Clases.cChequesaPagar();
            Clases.cMovimiento mov = new Clases.cMovimiento();
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            //cheque.PagarCheque(Convert.ToInt32(Principal.CodigoPrincipalAbm), Fecha);
            pagoCheque.InsertarPagoCheque(CodCheque, Importe, Fecha);
            string Descripcion = "PAGO DE CHEQUE " + txtCliente.Text.ToUpper ();
            Descripcion = Descripcion + ", PATENTE " + txtPatente.Text ;
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, -1 * Importe, 0, 0, 0, 0, Fecha, Descripcion);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            Cargar(CodCheque);
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            Int32 CodPago = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            Int32 CodCheque = Convert.ToInt32(Grilla.CurrentRow.Cells[1].Value.ToString());
            string msj = "Confirma anular el pago ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cPagoCheque objPago = new Clases.cPagoCheque();
            double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[2].Value.ToString()); 
            Clases.cChequesaPagar cheque = new Clases.cChequesaPagar();
            Clases.cMovimiento mov = new Clases.cMovimiento();
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            //cheque.AnularPagarCheque (Convert.ToInt32(Principal.CodigoPrincipalAbm));
            objPago.AnularPagoCheque(CodPago, CodCheque, Importe);
            string Descripcion = "ANULACION PAGO DE CHEQUE " + txtCliente.Text.ToUpper();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado,  Importe, 0, 0, 0, 0, Fecha, Descripcion);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            btnAnular.Enabled = false;
            Cargar(CodCheque);
        }

        private void txtImporteAPagar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }
    }
}
