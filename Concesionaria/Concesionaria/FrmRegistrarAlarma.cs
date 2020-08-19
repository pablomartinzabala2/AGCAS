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
    public partial class FrmRegistrarAlarma : Form
    {
        public FrmRegistrarAlarma()
        {
            InitializeComponent();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtDescripcion.Text =="")
            {
                MessageBox.Show("Debe ingresar un motivo", Clases.cMensaje.Mensaje());
                return;
            }

            Clases.cAlarma alarma = new Clases.cAlarma();
            string Patente = txtPatente.Text;
            string Cliente = txtCliente.Text;

            if (Principal.CodigoPrincipalAbm == null)
                alarma.GrabarAlarma(txtDescripcion.Text.ToUpper(), Convert.ToDateTime(txtFechaDesde.Text), Cliente, Patente);
            else
                alarma.ModificarAlarma(Convert.ToInt32(Principal.CodigoPrincipalAbm), txtDescripcion.Text, Convert.ToDateTime(txtFechaDesde.Text), Cliente, Patente);
            MessageBox.Show("Alarma registrada correctamente", Clases.cMensaje.Mensaje());
            txtDescripcion.Text = "";
            txtPatente.Text = "";
            txtCliente.Text = "";
            Principal.CodigoPrincipalAbm = null;
        }

        private void FrmRegistrarAlarma_Load(object sender, EventArgs e)
        {
            if (Principal.CodigoPrincipalAbm != null)
            {
                Int32 CodAlerta = Convert.ToInt32(Principal.CodigoPrincipalAbm);
                Clases.cAlarma alarma = new Clases.cAlarma();
                DataTable trdo = alarma.GetAlarmaxCodigo(CodAlerta);
                if (trdo.Rows.Count > 0)
                {
                    DateTime Fecha = Convert.ToDateTime (trdo.Rows[0]["Fecha"].ToString ());
                    txtFechaDesde.Text = Fecha.ToShortDateString();
                    txtDescripcion.Text = trdo.Rows[0]["Nombre"].ToString();
                    txtPatente.Text = trdo.Rows[0]["Patente"].ToString();
                    txtCliente.Text = trdo.Rows[0]["Cliente"].ToString();
                }
            }

            if (Principal.Comodin !="")
                if (Principal.Comodin != null)
                {
                    string Lista = Principal.Comodin;
                    string[] vec = Lista.Split('&');
                    txtPatente.Text = vec[0];
                    txtCliente.Text = vec[1];
                }
        }
    }
}
