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
    public partial class FrmCobroCheque : Form
    {
        public FrmCobroCheque()
        {
            InitializeComponent();
        }

        private void txtPatente_TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is GroupBox)
                {
                    foreach (Control g in c.Controls)
                    {
                        if (g is TextBox)
                            ((TextBox)g).CharacterCasing = CharacterCasing.Upper;
                    }
                    //Empleamos un casteo

                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtPatente.Text == "")
            {
                MessageBox.Show("Debe ingresar una patente", Clases.cMensaje.Mensaje()); 
                return;
            }
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            string Patente = txtPatente.Text;
            Clases.cCheque cheque = new Clases.cCheque();
            DataTable trdo = cheque.GetChequesxPatente(Patente);
            trdo = fun.TablaaMiles(trdo, "Importe");
            Grilla.DataSource = trdo;
            if (trdo.Rows.Count > 0)
            {
                txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txtDescripcion.Text = trdo.Rows[0]["Descripcion"].ToString();
            }
            Grilla.Columns[1].HeaderText = "Nro Cheque";
            Grilla.Columns[3].HeaderText = "Fecha Pago";
            Grilla.Columns[4].HeaderText = "Fecha Vto";

            Grilla.Columns[1].Width = 150;
            Grilla.Columns[2].Width = 150;
            Grilla.Columns[3].Width = 120;
            Grilla.Columns[4].Width = 120;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[5].Visible = false;
            Grilla.Columns[6].Visible = false;
            Grilla.Columns[7].Visible = false;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();

            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe Seleccionar un cheque para continuar ", Clases.cMensaje.Mensaje());
                return;
            }

            if (Grilla.Rows.Count < 2)
            {
                MessageBox.Show("Debe Seleccionar una prenda para continuar ", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtFecha.Text == "")
            {
                MessageBox.Show("Debe ingresar una fecha para continuar.", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta.", Clases.cMensaje.Mensaje());
                return;
            }

            DateTime  Fecha = Convert.ToDateTime(txtFecha.Text);
            Int32  CodVenta =Convert.ToInt32 ( Grilla.CurrentRow.Cells[0].Value.ToString());
            string NroCheque = Grilla.CurrentRow.Cells[1].Value.ToString();
            string Descripcion = "COBRO CHEQUE " + txtPatente.Text +" , " + NroCheque.ToString();
            Descripcion = Descripcion + ", " + txtEntregado.Text;
            Double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[2].Value.ToString());
            Clases.cCheque cheque = new Clases.cCheque();
            cheque.RegistrarCobroCheque(txtFecha.Text, Convert.ToInt32(CodVenta), NroCheque);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado, Importe, 0, 0, 0, 0, Fecha, Descripcion);
            CargarGrilla();
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe Seleccionar un cheque para continuar ", Clases.cMensaje.Mensaje());
                return;
            }

            if (Grilla.Rows.Count < 2)
            {
                MessageBox.Show("Debe Seleccionar una prenda para continuar ", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtFecha.Text == "")
            {
                MessageBox.Show("Debe ingresar una fecha para continuar.", Clases.cMensaje.Mensaje());
                return;
            }

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta.", Clases.cMensaje.Mensaje());
                return;
            }
            
            string NroCheque = Grilla.CurrentRow.Cells[1].Value.ToString();
            Double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[2].Value.ToString());
            Int32 CodVenta = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            Clases.cCheque Cheque = new Clases.cCheque();
            Cheque.AnularCobroCheque(CodVenta, NroCheque);
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);

            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimiento(CodVenta, Principal.CodUsuarioLogueado, (-1) * Importe, 0, 0, 0, 0, Fecha);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            CargarGrilla();
        }

        private void FrmCobroCheque_Load(object sender, EventArgs e)
        {
            if (Principal.CodigoPrincipalAbm != null)
            {
                Int32 CodAuto = Convert.ToInt32(Principal.CodigoPrincipalAbm);
                Clases.cAuto auto = new Clases.cAuto();
                DataTable trdo = auto.GetAutoxCodigo(CodAuto);
                if (trdo.Rows.Count > 0)
                {
                    string Patente = trdo.Rows[0]["Patente"].ToString();
                    txtPatente.Text = Patente;
                    CargarGrilla();
                }
            }
        }

        private void BtnVerGanancia_Click(object sender, EventArgs e)
        {
            if (txtEntregado.Text == "")
            {
                MessageBox.Show ("Debe ingresar un Nombre destinatario a entregar el cheque ");
                return ;
            }
             if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe Seleccionar un cheque para continuar ", Clases.cMensaje.Mensaje());
                return;
            }
             Clases.cCheque cheque = new Clases.cCheque();
             string NroCheque = Grilla.CurrentRow.Cells[1].Value.ToString();
             cheque.GrabarEntrega(NroCheque, txtEntregado.Text);
             MessageBox.Show("Datos Grabados Correctamente");
        }
    }
}
