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
    public partial class FrmPagoComision : Form
    {
        public FrmPagoComision()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmPagoComision_Load(object sender, EventArgs e)
        {
            btnAnular.Enabled = false;
            btnGrabar.Enabled = false; 
            if (Principal.CodigoPrincipalAbm != null)
            {
                Int32 Codigo = Convert.ToInt32(Principal.CodigoPrincipalAbm);
                txtCodComision.Text = Codigo.ToString();
                CargarDatos(Codigo);
            }
        }

        private void CargarDatos(Int32 CodComision)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cComisionVendedor com = new Clases.cComisionVendedor();
            DataTable trdo = com.GetComisionesxCodigo(CodComision);
            if (trdo.Rows.Count > 0)
            {
                txtNombre.Text  = trdo.Rows[0]["Nombre"].ToString();
                txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txtPatente.Text = trdo.Rows[0]["Patente"].ToString();
                txtImporte.Text = trdo.Rows[0]["Importe"].ToString();
                if (trdo.Rows[0]["FechaPago"].ToString() != "")
                {
                    DateTime fecha = Convert.ToDateTime (trdo.Rows[0]["FechaPago"].ToString());
                    txtFecha.Text = fecha.ToShortDateString ();
                }
                txtImporte.Text = fun.SepararDecimales(txtImporte.Text);
                txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
                if (trdo.Rows[0]["FechaPago"].ToString() == "")
                {
                    btnAnular.Enabled = false;
                    btnGrabar.Enabled = true;
                }
                else
                {
                    btnAnular.Enabled = true;
                    btnGrabar.Enabled = false;
                }
            }
            
            
        }

       
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            
            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }



            Int32 CodComision = Convert.ToInt32 (txtCodComision.Text);
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            Clases.cComisionVendedor com = new Clases.cComisionVendedor();
            com.PagoComision(Fecha, CodComision);
            string Descripcion = "PAGO COMISIÓN " + txtNombre.Text + " " + txtApellido.Text ;
            Descripcion = Descripcion + ", PATENTE " + txtPatente.Text;
            double Importe = fun.ToDouble(txtImporte.Text);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion (-1,Principal.CodUsuarioLogueado ,-1 * Importe ,0,0,0,0,Fecha ,Descripcion);
            MessageBox.Show ("Datos grabados Correctamente",Clases.cMensaje.Mensaje ());
          
            btnGrabar.Enabled = false;
            btnAnular.Enabled = false;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            string msj = "Confirma anular el pago de la comisión ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }
            
            Int32 CodComision = Convert.ToInt32(txtCodComision.Text);
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            Clases.cComisionVendedor com = new Clases.cComisionVendedor();
            com.AnularPagoComision(CodComision);
            string Descripcion = "ANULACIÓN PAGO COMISIÓN " + txtNombre.Text + " " + txtApellido.Text;
            Descripcion = Descripcion + ", PATENTE " + txtPatente.Text;
            double Importe = fun.ToDouble(txtImporte.Text);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado,Importe, 0, 0, 0, 0, Fecha, Descripcion);
            MessageBox.Show("Datos grabados Correctamente", Clases.cMensaje.Mensaje());
            
            btnGrabar.Enabled = false;
            btnAnular.Enabled = false;
        }
    }
}
