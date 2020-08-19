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
    public partial class FrmCobroCuotas : Form
    {
        public FrmCobroCuotas()
        {
            InitializeComponent();
        }

        private void FrmCobroCuotas_Load(object sender, EventArgs e)
        {
            InicializarComponentes();
            if (Principal.CodigoPrincipalAbm != null)
            {
                txtPatente.Text = Principal.CodigoPrincipalAbm;
                Buscar();
            }
        }

        private void InicializarComponentes()
        {
            Clases.cFunciones fun = new Clases.cFunciones(); 
            fun.LlenarCombo(cmbDocumento, "TipoDocumento", "Nombre", "CodTipoDoc");
            if (cmbDocumento.Items.Count > 0)
                cmbDocumento.SelectedIndex = 1;
            fun.LlenarCombo(CmbBarrio, "Barrio", "Nombre", "CodBarrio");
            PintarFormulario();
        }

        private void txtNroDoc_TextChanged(object sender, EventArgs e)
        {
            Int32 CodTipoDoc = 0;
            if (cmbDocumento.SelectedIndex > 0)
                CodTipoDoc = Convert.ToInt32(cmbDocumento.SelectedValue);
            string nroDocumento = txtNroDoc.Text;
            Clases.cCliente cliente = new Clases.cCliente();
            DataTable trdo = cliente.GetClientesxNroDoc(CodTipoDoc, nroDocumento);
            if (trdo.Rows.Count > 0)
            {
                txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txtTelefono.Text = trdo.Rows[0]["Telefono"].ToString();
                txtCelular.Text = trdo.Rows[0]["Celular"].ToString();
                txtCalle.Text = trdo.Rows[0]["Calle"].ToString();
                txtAltura.Text = trdo.Rows[0]["Numero"].ToString();
                if (trdo.Rows[0]["CodBarrio"].ToString() != "")
                    CmbBarrio.SelectedValue = trdo.Rows[0]["CodBarrio"].ToString();
                txtCodCLiente.Text = trdo.Rows[0]["CodCliente"].ToString();
                CargarPatentesxCliente(Convert.ToInt32(txtCodCLiente.Text));
            }
            else
                LimpiarCliente();
        }

        private void LimpiarCliente()
        {
            txtCodCLiente.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtTelefono.Text = "";
            txtCelular.Text = "";
            txtCalle.Text = "";
            txtAltura.Text = "";
            if (CmbBarrio.Items.Count > 0)
                CmbBarrio.SelectedIndex = 0;
            GrillaPatente.DataSource = null;
            GrillaCuotas.DataSource = null;
            txtCuota.Text = "";
            txtFecha.Text = "";
            txtImporte.Text = "";
            txtImportePagado.Text = "";
            txtSaldo.Text = "";
            txtTotalDeuda.Text = "";
            
        }

        private void CargarPatentesxCliente(Int32 CodCliente)
        {
            Clases.cAuto auto = new Clases.cAuto();
            DataTable trdo = auto.GetAutosCompradosxCuotasxCodCliente(CodCliente);
            Clases.cFunciones fun = new Clases.cFunciones();
            trdo = fun.TablaaMiles(trdo, "Capital");
            GrillaPatente.DataSource = trdo;
            GrillaPatente.Columns[0].Visible = false;
            GrillaPatente.Columns[1].Visible = false;
            GrillaPatente.Columns[3].Width = 534;
            int b = 0;
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodVenta"].ToString() != "")
                {
                    b = 1;
                    Int32 CodVenta = Convert.ToInt32(trdo.Rows[0]["CodVenta"].ToString());
                    CargarPlandeCuotas(CodVenta);
                }
            }
            else
            {
                return;
            }
            if (b == 0)
                GrillaCuotas.DataSource = null;
           
            GrillaCuotas.Columns[0].Visible = false;
            GrillaCuotas.Columns[1].Width = 73;
            GrillaCuotas.Columns[2].HeaderText = "Fecha vto.";
            GrillaCuotas.Columns[2].Width = 110;
            GrillaCuotas.Columns[4].HeaderText = "Fecha Pago.";
            GrillaCuotas.Columns[4].Width = 130;
            GrillaCuotas.Columns[5].HeaderText = "Importe Pago.";
            GrillaCuotas.Columns[5].Width = 140; 
        }

        private void btnCalcularCuotas_Click(object sender, EventArgs e)
        {

        }

        private void CargarPlandeCuotas(Int32 CodVenta)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cCuota cuota = new Clases.cCuota();
            DataTable trdo = cuota.GetCuotasxCodVenta(CodVenta);
            trdo = fun.TablaaMiles(trdo, "Importe");
            trdo = fun.TablaaMiles(trdo, "ImportePagado");
            trdo = fun.TablaaMiles(trdo, "Saldo");
            GrillaCuotas.DataSource = trdo;
        }

        private void GrillaPatente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string CodVenta = GrillaPatente.CurrentRow.Cells[0].Value.ToString();
            if (CodVenta != "")
            {
                CargarPlandeCuotas(Convert.ToInt32(CodVenta));
            }
        }

        private void GrillaCuotas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Cuota = GrillaCuotas.CurrentRow.Cells[1].Value.ToString();
            string Importe = GrillaCuotas.CurrentRow.Cells[3].Value.ToString();
            string ImportePagado = GrillaCuotas.CurrentRow.Cells[5].Value.ToString();
            string Fecha = GrillaCuotas.CurrentRow.Cells[2].Value.ToString();
            string CodVenta = GrillaCuotas.CurrentRow.Cells[0].Value.ToString();
            string sSaldo = GrillaCuotas.CurrentRow.Cells[6].Value.ToString();
            string FechaPago = GrillaCuotas.CurrentRow.Cells[4].Value.ToString();
            GetPunitorio(Convert.ToInt32(CodVenta), Convert.ToInt32(Cuota));
            Clases.cFunciones fun = new Clases.cFunciones();
            
            if (Cuota != "")
            {
                txtCuota.Text = Cuota;
                txtImporte.Text = Importe;
                txtImportePagado.Text = ImportePagado;
                txtFecha.Text = Fecha;
                txtSaldo.Text = sSaldo.ToString(); 
                if (txtImporte.Text != "")
                {
                    //string xx = trdo.Rows[0]["Importe"].ToString().Replace (",",".").ToString();
                   //txtImporte.Text = fun.TransformarEntero(Importe);
                    txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
                    txtCodVenta.Text = CodVenta.ToString();
                    if (txtImportePagado.Text != "")
                    {
                        btnAnular.Enabled = true;
                        txtImportePagado.ReadOnly = true;
                        btnGrabar.Enabled = false;
                        //txtImportePagado.Text = fun.TransformarEntero(txtImportePagado.Text);
                        txtImportePagado.Text = fun.FormatoEnteroMiles(txtImportePagado.Text);
                        txtSaldo.Text = (fun.ToDouble(txtImporte.Text) - fun.ToDouble(txtImportePagado.Text)).ToString();
                    }
                    else
                    {
                        
                        btnAnular.Enabled = false;
                        btnGrabar.Enabled = true;
                        txtImportePagado.ReadOnly = false; 
                    }
                }
               //obtengo la deuda total 
                Clases.cCuota objCuota = new Clases.cCuota ();
                txtTotalDeuda.Text = objCuota.GetSaldoDeudaCuotas(Convert.ToInt32(CodVenta)).ToString();
                if (txtTotalDeuda.Text != "")
                {
                    txtTotalDeuda.Text = fun.TransformarEntero(txtTotalDeuda.Text);
                    txtTotalDeuda.Text = fun.FormatoEnteroMiles(txtTotalDeuda.Text);
                }
            }
            int ban = 0;
            if (txtSaldo.Text != "0")
                if (txtSaldo.Text != "")
                {
                    btnPagarSaldo.Visible = true;
                    txtImportePagado.ReadOnly = false; 
                    ban = 1;
                }

            if (ban == 0)
            {
                txtImportePagado.ReadOnly = true; 
                btnPagarSaldo.Visible = false;
            }
            if (FechaPago != "")
                btnPagarSaldo.Visible = true;
            else
                btnPagarSaldo.Visible = false; 
        }

        private void GetPunitorio(Int32 CodVenta, Int32 Cuota)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            Clases.cPunitorioCuota puni = new Clases.cPunitorioCuota();
            Double Importe = puni.GetImportePunitorio(CodVenta, Cuota);
            txtPunitorio.Text = Importe.ToString();
            if (txtPunitorio.Text != "")
                txtPunitorio.Text = fun.FormatoEnteroMiles(txtPunitorio.Text);
        }

        private void txtImportePagado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == ",")
                e.Handled = true;
        }

        private void txtImportePagado_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtImportePagado.Text = fun.FormatoEnteroMiles(txtImportePagado.Text);
            double Importe = 0;
            double ImportePagado = 0;
            double Saldo =0;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtCuota.Text == "")
            {
                MessageBox.Show("Debe ingresar una cuota para continuar.", Clases.cMensaje.Mensaje());
                return;
            }
            if (txtImportePagado.Text == "")
            {
                MessageBox.Show ("Debe ingresar un monto de la cuota.",Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones ();
            double Importe = fun.ToDouble(txtImporte.Text);
            double ImportePagado = fun.ToDouble(txtImportePagado.Text);
            double Saldo = fun.ToDouble(txtSaldo.Text); 
            if (ImportePagado > Saldo)
            {
                MessageBox.Show("El importe pagado supera el saldo", Clases.cMensaje.Mensaje());
                return;
            }
            Int32 CodUsuario = Principal.CodUsuarioLogueado;
            if (ImportePagado > Importe)
            {
                MessageBox.Show("El importe a abonar es superior al valor de la cuota", Clases.cMensaje.Mensaje());
                return;
            }

            if (ImportePagado < Importe)
            {
                var result = MessageBox.Show("El importe abonado es inferior al valor de la cuota, Confirma Pago de la cuota?", Clases.cMensaje.Mensaje () ,
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            Double Punitorio = 0;
            if (txtPunitorio.Text != "")
                Punitorio = fun.ToDouble(txtPunitorio.Text);
            Int32 CodVenta = Convert.ToInt32(txtCodVenta.Text);
            Int32 Cuota = Convert.ToInt32(txtCuota.Text);
            DateTime FechaPago = Convert.ToDateTime(txtFecha.Text);
            Saldo = Importe - ImportePagado;
            Clases.cCuota objCuota = new Clases.cCuota();
            if (objCuota.GrabarCuota(CodVenta, Cuota, FechaPago, ImportePagado, Saldo, CodUsuario,txtPatente.Text,Punitorio))
            {
                MessageBox.Show("Cuota grabada correctamente.", Clases.cMensaje.Mensaje());
                CargarPlandeCuotas(Convert.ToInt32(CodVenta));
                
                txtTotalDeuda.Text = objCuota.GetSaldoDeudaCuotas(Convert.ToInt32(CodVenta)).ToString();
                if (txtTotalDeuda.Text != "")
                {
                    txtTotalDeuda.Text = fun.TransformarEntero(txtTotalDeuda.Text);
                    txtTotalDeuda.Text = fun.FormatoEnteroMiles(txtTotalDeuda.Text);
                }
                LimpiarTextCuotas();
            }
            else
            {
                MessageBox.Show("Hubo un error en el proceso de grabación.", Clases.cMensaje.Mensaje());
            }
            
        }

        private void PintarFormulario()
        {
            foreach (Control c in this.Controls)
            {
                string name = c.Name;
                if (c is TextBox)
                    c.BackColor = Color.LightGray;
                if (c is GroupBox)
                {
                    foreach (Control g in c.Controls)
                    {
                        if (g is TextBox || g is MaskedTextBox)
                            g.BackColor = Clases.cConfiguracion.ColorTextBox();
                        //g.BackColor = System.Drawing.SystemColors.Control;   
                    }
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (txtCuota.Text == "")
            {
                MessageBox.Show("Debe ingresar una cuota para continuar.", Clases.cMensaje.Mensaje());
                return;
            }
            if (txtImportePagado.Text == "")
            {
                MessageBox.Show("Debe ingresar un monto de la cuota.", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            double Importe = fun.ToDouble(txtImporte.Text);
            double ImportePagado = fun.ToDouble(txtImportePagado.Text);
            ImportePagado = ImportePagado * (-1);
            Int32 CodUsuario = Principal.CodUsuarioLogueado;
            Int32 Cuota = Convert.ToInt32(txtCuota.Text);
            Int32 CodVenta = Convert.ToInt32(txtCodVenta.Text);
            GetPunitorio(CodVenta, Cuota);
            Double Punitorio = fun.ToDouble(txtPunitorio.Text);
            Punitorio = Punitorio * (-1);
            Clases.cCuota objCuota = new Clases.cCuota();
            if (objCuota.AnularCuota(CodVenta, Cuota, ImportePagado, CodUsuario, txtPatente.Text, Punitorio))
            {
                MessageBox.Show("Datos grabados correctamente.", Clases.cMensaje.Mensaje());
                CargarPlandeCuotas(CodVenta); 
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            if (txtPatente.Text == "")
            {
                MessageBox.Show("Debe ingresar una patente", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cAuto objAuto = new Clases.cAuto();
            DataTable tauto = objAuto.GetAutoxPatente(txtPatente.Text);
            if (tauto.Rows.Count > 0)
            {
                if (tauto.Rows[0]["CodAuto"].ToString() != "")
                {
                    Int32 CodAuto = Convert.ToInt32(tauto.Rows[0]["CodAuto"].ToString());
                    Clases.cVenta objVenta = new Clases.cVenta();
                    Int32 CodVenta = objVenta.GetMaximaCodVentaxAuto(CodAuto);
                    txtCodVenta.Text = CodVenta.ToString();
                    if (CodVenta > 0)
                    {
                        DataTable tcli = objVenta.GetVentaxCodigo(CodVenta);
                        if (tcli.Rows.Count > 0)
                        {
                            Int32 CodCliente = Convert.ToInt32(tcli.Rows[0]["CodCliente"].ToString());
                            CargarPatentesxCliente(CodCliente);
                            CargarCLiente(CodCliente);
                        }
                        CargarPlandeCuotas(CodVenta);
                    }
                }
            }
        }

        private void CargarCLiente(Int32 CodCliente)
        {
            Clases.cCliente cliente = new Clases.cCliente();
            DataTable trdo = cliente.GetClientesxCodigo(CodCliente);
            if (trdo.Rows.Count > 0)
            {
                txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txtTelefono.Text = trdo.Rows[0]["Telefono"].ToString();
                txtCelular.Text = trdo.Rows[0]["Celular"].ToString();
                txtCalle.Text = trdo.Rows[0]["Calle"].ToString();
                txtAltura.Text = trdo.Rows[0]["Numero"].ToString();
                if (trdo.Rows[0]["CodBarrio"].ToString() != "")
                    CmbBarrio.SelectedValue = trdo.Rows[0]["CodBarrio"].ToString();
                txtCodCLiente.Text = trdo.Rows[0]["CodCliente"].ToString();
                txtNroDoc.Text = trdo.Rows[0]["NroDocumento"].ToString();
                CargarPatentesxCliente(Convert.ToInt32(txtCodCLiente.Text));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCodVenta.Text == "")
            {
                MessageBox.Show("Debe ingresar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Principal.CampoNombreSecundario = txtCodVenta.Text;
            Principal.NombreTablaSecundario = "MensajesCuotas";
            FrmMensaje form = new FrmMensaje();
            form.ShowDialog();
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

        private void btnPagarSaldo_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtImportePagado.Text == "")
            {
                MessageBox.Show("Debe ingresar un importe", Clases.cMensaje.Mensaje());
                return;
            }

            double Saldo = fun.ToDouble(txtSaldo.Text);
            double Importe = fun.ToDouble(txtImportePagado.Text);
            Double Punitorio = 0;
            if (txtPunitorio.Text != "")
                Punitorio = fun.ToDouble(txtPunitorio.Text);
            if (Importe > Saldo)
            {
                MessageBox.Show("El saldo es superior al importe", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cMovimiento mov = new Clases.cMovimiento();
            Clases.cSaldoCuota saldoCuotas = new Clases.cSaldoCuota();
            string Descripcion = "PAGO DE SALDO CUOTA" + txtCuota.Text;
            Descripcion = Descripcion + ", PATENTE " + txtPatente.Text;
            Int32 Cuota = Convert.ToInt32(txtCuota.Text);
            Int32 CodVenta = Convert.ToInt32(txtCodVenta.Text);
            
            Clases.cCuota cuota = new Clases.cCuota();
            DateTime Fecha = Convert.ToDateTime (txtFecha.Text);
            cuota.PagarSaldoCuota(CodVenta, Cuota, Importe);
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, Importe, 0, 0, 0, 0, Fecha, Descripcion);
            saldoCuotas.InsertarSaldoCob(CodVenta, Cuota,Fecha, Importe);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (txtCodVenta.Text == "")
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje());
                return;
            }
            Principal.CodigoPrincipalAbm = txtCodVenta.Text;
            FrmSaldoCuotas frm = new FrmSaldoCuotas();
            frm.ShowDialog();
        }

        private void txtCodCLiente_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPagarTodasCuotas_Click(object sender, EventArgs e)
        {
            if (txtCodVenta.Text == "")
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje());
                return;
            }  
            
            Principal.CodigoPrincipalAbm = txtCodVenta.Text;
            FrmSaldarCuotas frm = new FrmSaldarCuotas();
            frm.ShowDialog();
        }

        private void txtPunitorio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == ",")
                e.Handled = true;
            
        }

        private void txtPunitorio_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtPunitorio.Text = fun.FormatoEnteroMiles(txtPunitorio.Text);
        }

        private void LimpiarTextCuotas()
        {
            txtCodVenta.Text = "";
            txtCuota.Text = "";
            txtFecha.Text = "";
            txtImporte.Text = "";
            txtImportePagado.Text = "";
            txtSaldo.Text = "";
            txtTotalDeuda.Text = "";
            txtPunitorio.Text = "";
        }
    }
}
