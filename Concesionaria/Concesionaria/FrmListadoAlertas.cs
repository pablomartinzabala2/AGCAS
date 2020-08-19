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
    public partial class FrmListadoAlertas : Form
    {
        public FrmListadoAlertas()
        {
            InitializeComponent();
            DateTime fechahoy = DateTime.Now;
            txtFechaHasta.Text = fechahoy.ToShortDateString();
            fechahoy = fechahoy.AddMonths(-1);
            txtFechaDesde.Text = fechahoy.ToShortDateString();
        }

        private void FrmListadoAlertas_Load(object sender, EventArgs e)
        {
            DateTime fechahoy = DateTime.Now;
            txtFechaHasta.Text = fechahoy.ToShortDateString();
            fechahoy = fechahoy.AddMonths(-1);
            txtFechaDesde.Text = fechahoy.ToShortDateString();
            Buscar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
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

            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            string texto = txtDescripcion.Text;
            Clases.cAlarma alarma = new Clases.cAlarma();
            DataTable trdo = alarma.GetAlertasxRangoFecha(FechaDesde, FechaHasta, texto, txtPatente.Text.Trim(), txtNombreCliente.Text.Trim());
            Grilla.DataSource = trdo;
            Grilla.Columns[1].HeaderText = "Descripción";
            Grilla.Columns[1].Width = 200;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[2].Width = 100;
            Grilla.Columns[3].Width = 250;
            Grilla.Columns[4].Width = 90;

        }

        private void btnEditarAlerta_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro");
                return;
            }
            Principal.Comodin = "";
            Principal.CodigoPrincipalAbm = Grilla.CurrentRow.Cells[0].Value.ToString(); 
            FrmRegistrarAlarma frm = new FrmRegistrarAlarma();
            frm.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro", "Sistema");
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
            Int32 CodAlarma = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            Clases.cAlarma alarma = new Clases.cAlarma();
            alarma.EliminarAlarma(CodAlarma);
            Buscar();
        }

        private void btnAgregarAlerta_Click(object sender, EventArgs e)
        {
            FrmRegistrarAlarma frm = new FrmRegistrarAlarma();
            frm.ShowDialog();
        }

        
        
      
    }
}
