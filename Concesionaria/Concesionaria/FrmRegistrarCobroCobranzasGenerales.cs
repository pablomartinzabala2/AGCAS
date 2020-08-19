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
    public partial class FrmRegistrarCobroCobranzasGenerales : Form
    {
        public FrmRegistrarCobroCobranzasGenerales()
        {
            InitializeComponent();
        }

        private void FrmRegistrarCobroCobranzasGenerales_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            if (Principal.CodigoPrincipalAbm != null)
            {
                txtCodCobranza.Text = Principal.CodigoPrincipalAbm.ToString();
                GetCobranzas(Convert.ToInt32(Principal.CodigoPrincipalAbm));
            }
                
        }

        public void GetCobranzas(Int32 CodCobranza)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cCobranzaGeneral cob = new Clases.cCobranzaGeneral();
            DataTable trdo = cob.GetCobranzaxCodigo(CodCobranza);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["Fecha"].ToString() != "")
                {
                    DateTime Fecha = Convert.ToDateTime(trdo.Rows[0]["Fecha"].ToString());
                    txtFecha.Text = Fecha.ToShortDateString();
                }

                if (trdo.Rows[0]["FechaPago"].ToString() != "")
                {
                    DateTime Fecha = Convert.ToDateTime(trdo.Rows[0]["FechaPago"].ToString());
                    txtFechaCobro.Text  = Fecha.ToShortDateString();
                }

                if (trdo.Rows[0]["FechaPago"].ToString() == "")
                {
                    btnGuardar.Enabled = true;
                    btnAnular.Enabled = false;
                    btnPagarSaldo.Visible = false;
                }
                else
                {
                    btnGuardar.Enabled = false;
                    btnAnular.Enabled = true;
                    btnPagarSaldo.Visible = true;
                }
                txtTotalCobrado.Text = trdo.Rows[0]["ImportePagado"].ToString();  
                txtDescripcion.Text = trdo.Rows[0]["Descripcion"].ToString();
                txtImporte.Text = trdo.Rows[0]["Importe"].ToString();
                txtPatente.Text = trdo.Rows[0]["Patente"].ToString();
                txtSaldo.Text = trdo.Rows[0]["Saldo"].ToString();
                if (txtImporte.Text != "")
                {
                    txtImporte.Text = fun.SepararDecimales(txtImporte.Text);
                    txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
                }

                if (txtTotalCobrado.Text != "")
                {
                    txtTotalCobrado.Text = fun.SepararDecimales(txtTotalCobrado.Text);
                    txtTotalCobrado.Text = fun.FormatoEnteroMiles(txtTotalCobrado.Text);
                }

                if (txtSaldo.Text != "")
                {
                    txtSaldo.Text = fun.SepararDecimales(txtSaldo.Text);
                    txtSaldo.Text = fun.FormatoEnteroMiles(txtSaldo.Text);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            if (txtMontoaPagar.Text == "")
            {
                Mensaje("Debe ingresar un monto a cobrar");
                return;
            }
            double Saldo = fun.ToDouble(txtSaldo.Text);
            double Importe = fun.ToDouble(txtMontoaPagar.Text);
            if (Saldo == 0)
            {
                Mensaje("No hay saldo para cancelar");
                return;
            }
            if (fun.ValidarFecha (txtFechaCobro.Text)==false)
            {
                Mensaje ("Debe ingresar una fecha de cobro");
                return ;
            }
            if (Importe > Saldo)
            {
                Mensaje("El importe a cobrar es mayor al saldo");
                return;
            }

            if (Saldo > Importe)
            {
                var result = MessageBox.Show("El importe es inferior al saldo, desea continuar", "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.No)
                {
                    return;
                }
            }  
            Int32 CodCobranza = Convert.ToInt32 (txtCodCobranza.Text);
            DateTime Fecha = Convert.ToDateTime (txtFechaCobro.Text);
            string Descripción = "COBRANZA GENERAL ,PATENTE " + txtPatente.Text;
            Clases.cCobranzaGeneral cob = new Clases.cCobranzaGeneral();
            cob.RegistrarCobro(CodCobranza, Fecha, Importe);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, Importe, 0, 0, 0, 0, Fecha, Descripción);
            Mensaje("Datos grabados correctamente ");
            GetCobranzas(CodCobranza);
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, Clases.cMensaje.Mensaje());
        }

        private void txtMontoaPagar_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtMontoaPagar.Text != "")
            {
                txtMontoaPagar.Text = fun.FormatoEnteroMiles(txtMontoaPagar.Text);
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            if (fun.ValidarFecha (txtFecha.Text)==false)
            {
                Mensaje ("La fecha ingresada es incorrecta");
                return ;
            }
            double ImporteAnular = fun.ToDouble(txtTotalCobrado.Text);
            Int32 CodCobranza = Convert.ToInt32(txtCodCobranza.Text);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            Clases.cCobranzaGeneral cob = new Clases.cCobranzaGeneral();
            DateTime fecha = Convert.ToDateTime (txtFecha.Text);
            cob.AnularCobranza(CodCobranza);
            string descrip = "ANULACIÓN " + txtDescripcion.Text;
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, -1 * ImporteAnular, 0, 0, 0, 0, fecha, descrip);
            GetCobranzas (CodCobranza);
            Mensaje ("Datos grabados correctamente");
        }

        private void btnPagarSaldo_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtMontoaPagar.Text == "")
            {
                Mensaje("Debe ingresar un monto a cobrar");
                return;
            }
            double Saldo = fun.ToDouble(txtSaldo.Text);
            double Importe = fun.ToDouble(txtMontoaPagar.Text);
            if (Saldo == 0)
            {
                Mensaje("No hay saldo para cancelar");
                return;
            }
            if (Importe > Saldo)
            {
                Mensaje("El importe a cobrar es mayor al saldo");
                return;
            } 
            Clases.cSaldoCobranzaGeneral saldoCob = new Clases.cSaldoCobranzaGeneral();
            Int32 CodCobranza = Convert.ToInt32(txtCodCobranza.Text);
            DateTime Fecha = Convert.ToDateTime(txtFechaCobro.Text);
            string Descripción ="PAGO SALDO " + txtDescripcion.Text;
            Clases.cCobranzaGeneral cob = new Clases.cCobranzaGeneral();
            cob.PagarSaldo(CodCobranza, Importe);
            saldoCob.InsertarSaldoCob(CodCobranza, Fecha, Importe);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, Importe, 0, 0, 0, 0, Fecha, Descripción);
            Mensaje("Datos grabados correctamente ");
            GetCobranzas(CodCobranza);
            

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (txtCodCobranza.Text == "")
            {
                Mensaje("Debe ingresar un registro");
                return;
            }
            Principal.CodigoPrincipalAbm = txtCodCobranza.Text; 
            FrmDetalleSaldoCobranzaGeneral frm = new FrmDetalleSaldoCobranzaGeneral();
            frm.ShowDialog();
        }

        private void btnAlarma_Click(object sender, EventArgs e)
        {
            string Patente = txtPatente.Text;
            string Nombre = "";
            string Union = Patente + "&" + Nombre;
            Principal.Comodin = Union;
            Principal.CodigoPrincipalAbm = null;
            FrmRegistrarAlarma form = new FrmRegistrarAlarma();
            form.ShowDialog();
        }
    }
}
