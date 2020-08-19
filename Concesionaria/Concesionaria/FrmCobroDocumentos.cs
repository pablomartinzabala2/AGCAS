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
    public partial class FrmCobroDocumentos : Form
    {
        public FrmCobroDocumentos()
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
            Clases.cCobranza cob = new Clases.cCobranza();
            Clases.cFunciones fun = new Clases.cFunciones();
            DataTable trdo = cob.GetCobranzaxPatente(txtPatente.Text);
            trdo = fun.TablaaMiles(trdo, "Importe");
            trdo = fun.TablaaMiles(trdo, "ImportePagado");
            trdo = fun.TablaaMiles(trdo, "Punitorio");
            trdo = fun.TablaaMiles(trdo, "Saldo");
            Grilla.DataSource = trdo;
            int ban = 0;
            if (trdo.Rows.Count >0)
                if (trdo.Rows[0]["CodVenta"].ToString() != "")
                {
                    ban = 1;
                    txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                    txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                    txtDescripcion.Text = trdo.Rows[0]["Descripcion"].ToString();
                }
            if (ban == 0)
            {
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDescripcion.Text = "";
            }
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Visible = false;
            Grilla.Columns[6].Visible = false;
            Grilla.Columns[4].HeaderText = "Fecha Pago";
            Grilla.Columns[4].Width = 130;
            Grilla.Columns[7].Width = 180;
            Grilla.Columns[8].HeaderText = "Imp. pagado";
            Grilla.Columns[8].Width = 120; 

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Grilla.Rows.Count < 2)
            {
                MessageBox.Show("Debe ingresar un registro para continuar", Clases.cMensaje.Mensaje());   
                return;
            }

            if (Grilla.CurrentRow ==null )
            {
                MessageBox.Show("Debe ingresar un registro para continuar", Clases.cMensaje.Mensaje());
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

            Int32 CodCobranza = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            //Double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[2].Value.ToString());
            double Tope = fun.ToDouble(txtTope.Text);
            double Importe = fun.ToDouble(txtImporte.Text);
            double Saldo = fun.ToDouble(txtSaldo.Text);
            Double Punitorio = 0;
            if (txtPunitorio.Text != "")
                Punitorio = fun.ToDouble(txtPunitorio.Text);
            if (Importe > Tope)
            {
                MessageBox.Show("El importe ingresado supera el monto total", Clases.cMensaje.Mensaje());
                return;
            }

            Saldo = Tope - Importe;

            string Descripcion = "REGISTRO DE COBRANZA PATENTE " + txtPatente.Text;
            Int32 CodVenta = Convert.ToInt32(Grilla.CurrentRow.Cells[1].Value.ToString());
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            Clases.cCobranza cobranza = new Clases.cCobranza();
            cobranza.RegistrarCobranza(CodCobranza, txtFecha.Text, Importe, Saldo);
             Clases.cMovimiento mov = new Clases.cMovimiento();
             Clases.cPunitorioCobranza objPunitorio = new Clases.cPunitorioCobranza();
             //Importe = Importe + Punitorio;
            mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado, Importe, ((-1) * Importe), 0, 0, 0, Fecha,Descripcion);
             Clases.cSaldoCobranza saldoCob = new Clases.cSaldoCobranza();
             saldoCob.InsertarSaldoCob(CodCobranza, Fecha, Importe);
             if (Punitorio > 0)
             {
                 Descripcion = "COBRO DE PUNITORIO, PATENTE " + txtPatente.Text;
                 objPunitorio.GrabarPunitorio(CodVenta, CodCobranza, Punitorio, Fecha);
                 mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado, Punitorio, 0, 0, 0, 0, Fecha, Descripcion);
             }
                 
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
             CargarGrilla(); 
        }

        private void FrmCobroDocumentos_Load(object sender, EventArgs e)
        {
            if (Principal.CodigoPrincipalAbm != null)
            {
                txtPatente.Text = Principal.CodigoPrincipalAbm.ToString();
                CargarGrilla();
                btnGrabar.Enabled = false;
                btnAnular.Enabled = false;
                txtFecha.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void Grilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            if (Grilla.CurrentRow == null)
                return;
            txtImporte.Text = Grilla.CurrentRow.Cells[2].Value.ToString();
            txtTope.Text = Grilla.CurrentRow.Cells[2].Value.ToString();
            string Saldo = Grilla.CurrentRow.Cells[9].Value.ToString();
            txtCodCobranza.Text = Grilla.CurrentRow.Cells[0].Value.ToString();
            Int32 CodCobranza = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            Clases.cPunitorioCobranza objPun = new Clases.cPunitorioCobranza();
            Double Punitorio = objPun.GetImportePunitorio(CodCobranza);
            txtPunitorio.Text = Punitorio.ToString();
            if (Punitorio > 0)
                txtPunitorio.Text = fun.FormatoEnteroMiles(txtPunitorio.Text);
            else
                txtPunitorio.Text = "0";
            if (Saldo.Trim() == "")
            {
                Saldo = txtImporte.Text;
            }

            if (Grilla.CurrentRow.Cells[4].Value.ToString() == "")
            {
                btnAnular.Enabled = false;
                btnGrabar.Enabled = true;
                btnPagarSaldo.Visible  = false; 
            }
            else
            {
                btnAnular.Enabled = true;
                btnGrabar.Enabled = false;
                btnPagarSaldo.Visible = true;
            }

            txtSaldo.Text = Saldo; 
            
        }

        private void btnPagarSaldo_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta.", Clases.cMensaje.Mensaje());
                return;
            }
            if (txtSaldo.Text != "")
            {
                double saldo = fun.ToDouble(txtSaldo.Text);
                if (saldo == 0)
                {
                    MessageBox.Show("El saldo ya ha sido cancelado", Clases.cMensaje.Mensaje()); 
                    return;
                }  
                Int32 CodCobranza = Convert.ToInt32(txtCodCobranza.Text);
                double Importe = fun.ToDouble(txtImporte.Text);
                if (Importe > saldo)
                {
                    MessageBox.Show("El saldo supera el saldo", Clases.cMensaje.Mensaje());
                    return;
                }
                Clases.cSaldoCobranza saldoCob = new Clases.cSaldoCobranza();
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                Clases.cCobranza cob = new Clases.cCobranza();
                cob.PagarSaldo(CodCobranza, Fecha, Importe);
                string Descripcion = "PAGO DE SALDO PATENTE " + txtPatente.Text;
                Clases.cMovimiento mov = new Clases.cMovimiento();
                mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, Importe, 0, 0, 0, 0, Fecha, Descripcion);
                saldoCob.InsertarSaldoCob(CodCobranza, Fecha, Importe);
                Double Punitorio = 0;
                if (txtPunitorio.Text != "")
                    Punitorio = fun.ToDouble(txtPunitorio.Text);
                Int32 CodVenta = Convert.ToInt32(Grilla.CurrentRow.Cells[1].Value.ToString());
                if (Punitorio > 0)
                {
                    Clases.cPunitorioCobranza objPunitorio = new Clases.cPunitorioCobranza();
                    Descripcion = "COBRO DE PUNITORIO, PATENTE " + txtPatente.Text;
                    objPunitorio.GrabarPunitorio(CodVenta, CodCobranza, Punitorio, Fecha);
                    mov.RegistrarMovimientoDescripcion(CodVenta, Principal.CodUsuarioLogueado, Punitorio, 0, 0, 0, 0, Fecha, Descripcion);
                }
                MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAlarma_Click(object sender, EventArgs e)
        {
            string Patente = txtPatente.Text;
            string Nombre = txtNombre.Text + " " + txtApellido.Text;
            string Union = Patente + "&" + Nombre;
            Principal.Comodin = Union;
            Principal.CodigoPrincipalAbm = null;
                      
            FrmRegistrarAlarma form = new FrmRegistrarAlarma();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCodCobranza.Text == "")
            {
                MessageBox.Show("Debe ingresar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Principal.CampoNombreSecundario = txtCodCobranza.Text;
            Principal.NombreTablaSecundario = "MensajesCobranza";
            FrmMensaje form = new FrmMensaje();
            form.ShowDialog();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow.Cells[8].Value.ToString() == "")
            {
                MessageBox.Show("Debe seleccionar una cobranza para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtCodCobranza.Text =="")
            {
                MessageBox.Show ("Debe ingresar una cobranza",Clases.cMensaje.Mensaje ());
                return;
            }

            Int32 CodCobranza = Convert.ToInt32 (txtCodCobranza.Text);

            Clases.cFunciones fun = new Clases.cFunciones ();
            double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[8].Value.ToString());
            Clases.cCobranza cob = new Clases.cCobranza();
            cob.AnularCobranza(CodCobranza);
            string Descripcion = "ANULACION DE COBRANZA " + txtPatente.Text;
            Clases.cPunitorioCobranza objPun = new Clases.cPunitorioCobranza(); 
            Double Punitorio = objPun.GetImportePunitorio(CodCobranza);
            objPun.BorrarPunitorio(CodCobranza);
           // Importe = Importe + Punitorio;
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, -1 * Importe, 0, 0, 0, 0, Convert.ToDateTime(txtFecha.Text), Descripcion);
            //anulo punitorio
            Descripcion = "ANULACION DE PUNITORIO " + txtPatente.Text;
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, -1 * Punitorio , 0, 0, 0, 0, Convert.ToDateTime(txtFecha.Text), Descripcion);
            CargarGrilla();
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtCodCobranza.Text == "")
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje());
                return;
            }
            Principal.CodigoPrincipalAbm = txtCodCobranza.Text;
            FrmDetalleSaldoCobranza form = new FrmDetalleSaldoCobranza();
            form.ShowDialog();
        }

        
   
    }
}
