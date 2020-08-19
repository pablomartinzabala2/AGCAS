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
    public partial class FrmCobroCheque2 : Form
    {
        public FrmCobroCheque2()
        {
            InitializeComponent();
        }

        private void FrmCobroCheque2_Load(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            fun.LlenarCombo(cmbBanco, "Banco", "Nombre", "CodBanco");
            if (Principal.CodigoPrincipalAbm != "")
            {
                GetCheque(Convert.ToInt32(Principal.CodigoPrincipalAbm));
            }
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, "Sistema");
        }

        private void GetCheque(Int32 CodCheque)
        {
            cChequeCobrar cheque = new cChequeCobrar();
            DataTable trdo = cheque.GetChequexCodigo(CodCheque);
            if (trdo.Rows.Count > 0)
            {
                DateTime Fecha = Convert.ToDateTime (trdo.Rows[0]["Fecha"].ToString ());
                
                Int32? CodBanco = null;
                string Nombre ="";
                string Apellido ="";
                Double Importe =0;
                string Patente ="";

                if (trdo.Rows[0]["Vencimiento"].ToString ()!="")
                {
                   DateTime  Vencimiento = Convert.ToDateTime (trdo.Rows[0]["Vencimiento"].ToString ());
                    txtVencimiento.Text = Vencimiento.ToShortDateString ();
                }
                if (trdo.Rows[0]["CodBanco"].ToString ()!="")
                    CodBanco = Convert.ToInt32 (trdo.Rows[0]["CodBanco"].ToString ());
                Nombre = trdo.Rows[0]["Nombre"].ToString ();
                Apellido = trdo.Rows[0]["Apellido"].ToString (); 
                Patente = trdo.Rows[0]["Patente"].ToString ();
                Importe = Convert.ToDouble (trdo.Rows[0]["Importe"].ToString ());

                txtNombre.Text = Nombre ;
                txtApellido.Text = Apellido ;
                txtImporte.Text = Importe.ToString ();
                txtFecha.Text = Fecha.ToShortDateString ();
                txtTelefono.Text = trdo.Rows[0]["Telefono"].ToString ();
                txtPatente.Text = Patente;
                txtEntregadoa.Text = trdo.Rows[0]["EntregadoA"].ToString();
                if (trdo.Rows[0]["CodBanco"].ToString() != "")
                {
                    cmbBanco.SelectedValue = trdo.Rows[0]["CodBanco"].ToString();
                }
                txtNumeroCheque.Text = trdo.Rows[0]["NumeroCheque"].ToString();
              

            }
        }

        private void btnGrabarEntrega_Click(object sender, EventArgs e)
        {
            if (txtEntregadoa.Text == "")
            {
                Mensaje("Debe ingresar el destinatario del cheque ");
                return;
            }
            string EntregadoA = txtEntregadoa.Text;
            cChequeCobrar cheque = new cChequeCobrar();
            Int32 CodCheque = Convert.ToInt32(Principal.CodigoPrincipalAbm.ToString());
            cheque.GrabarEntregado(CodCheque, EntregadoA);
            Mensaje("Datos grabados correctamente");

        }
    }
}
