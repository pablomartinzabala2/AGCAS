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
    public partial class FrmCobroDocumentosAnteriores : Form
    {
        public FrmCobroDocumentosAnteriores()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            if (txtCodGrupo.Text  == "")
            {
                MessageBox.Show("Debe ingresar una patente", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cCuotasAnteriores cuotas = new Clases.cCuotasAnteriores();
            DataTable trdo = cuotas.GetCuotasAnterioresxCodGrupo(Convert.ToInt32(txtCodGrupo.Text));
            if (trdo.Rows.Count > 0)
            {
                txtPatente.Text = trdo.Rows[0]["Patente"].ToString();
                txtDescripcion.Text = trdo.Rows[0]["Descripcion"].ToString();
                txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txtCodGrupo.Text = trdo.Rows[0]["CodGrupo"].ToString();
                txtTelefono.Text = trdo.Rows[0]["Telefono"].ToString();
                txtAnio.Text = trdo.Rows[0]["Anio"].ToString();
                if (trdo.Rows[0]["CodMarca"].ToString() != "")
                {
                    cmb_CodMarca.SelectedValue = trdo.Rows[0]["CodMarca"].ToString();
                }
                CargarPlandeCuotas(Convert.ToInt32(txtCodGrupo.Text));
            }
            else
            {
                txtDescripcion.Text = "";
                txtTelefono.Text = "";
                txtApellido.Text = "";
                txtNombre.Text = "";
                txtCodGrupo.Text = "";
                txtAnio.Text = "";
                cmb_CodMarca.SelectedIndex = 0;
                GrillaCuotas.DataSource = null;
            }
        }

        private void CargarPlandeCuotas(Int32 CodCompra)
        {
            Clases.cCuotasAnteriores cuota = new Clases.cCuotasAnteriores();
            DataTable trdo = cuota.GetCuotasxCodVenta(CodCompra);
            GrillaCuotas.DataSource = trdo;
            GrillaCuotas.Columns[0].Visible = false;
            GrillaCuotas.Columns[2].HeaderText = "Fecha Vto";
            GrillaCuotas.Columns[4].HeaderText = "Fecha Pago";
            GrillaCuotas.Columns[4].Width = 120;
            GrillaCuotas.Columns[5].HeaderText = "Imp. pago";
            GrillaCuotas.Columns[4].Width = 150; 
        }

        private void GrillaCuotas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Cuota = GrillaCuotas.CurrentRow.Cells[1].Value.ToString();
            string Importe = GrillaCuotas.CurrentRow.Cells[3].Value.ToString();
            string ImportePagado = GrillaCuotas.CurrentRow.Cells[5].Value.ToString();
            string Fecha = GrillaCuotas.CurrentRow.Cells[2].Value.ToString();
            string CodGrupo = GrillaCuotas.CurrentRow.Cells[0].Value.ToString();
            GetPunitorio(Convert.ToInt32(CodGrupo), Convert.ToInt32(Cuota));
            Clases.cFunciones fun = new Clases.cFunciones();

            if (Cuota != "")
            {
                txtCuota.Text = Cuota;
                txtImporte.Text = Importe;
                txtImportePagado.Text = ImportePagado;
                txtFecha.Text = Fecha;
                if (txtImporte.Text != "")
                {  
                    //string xx = trdo.Rows[0]["Importe"].ToString().Replace (",",".").ToString();
                    txtImporte.Text = fun.TransformarEntero(Importe);
                    txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
                    txtCodGrupo.Text = CodGrupo.ToString();
                    if (txtImportePagado.Text != "")
                    {
                        btnAnular.Enabled = true;
                        btnPagarSaldo.Visible = true;
                       // txtImportePagado.ReadOnly = true;
                        btnGrabar.Enabled = false;
                        txtImportePagado.Text = fun.TransformarEntero(txtImportePagado.Text);
                        txtImportePagado.Text = fun.FormatoEnteroMiles(txtImportePagado.Text);
                        txtSaldo.Text = (fun.ToDouble(txtImporte.Text) - fun.ToDouble(txtImportePagado.Text)).ToString();
                    }
                    else
                    {
                        btnPagarSaldo.Visible = false;
                        txtSaldo.Text = "";
                        btnAnular.Enabled = false;
                        btnGrabar.Enabled = true;
                       // txtImportePagado.ReadOnly = false;
                    }
                }
                //obtengo la deuda total 
                Clases.cCuotasAnteriores objCuota = new Clases.cCuotasAnteriores();
                txtTotalDeuda.Text = objCuota.GetSaldoDeudaCuotas(Convert.ToInt32(CodGrupo)).ToString();
                if (txtTotalDeuda.Text != "")
                {
                    txtTotalDeuda.Text = fun.TransformarEntero(txtTotalDeuda.Text);
                    txtTotalDeuda.Text = fun.FormatoEnteroMiles(txtTotalDeuda.Text);
                }
            }
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
                MessageBox.Show("Debe ingresar un monto de la cuota.", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            double Importe = fun.ToDouble(txtImporte.Text);
            double ImportePagado = fun.ToDouble(txtImportePagado.Text);
            Int32 CodUsuario = Principal.CodUsuarioLogueado;
            if (ImportePagado > Importe)
            {
                MessageBox.Show("El importe a abonar es superior al valor de la cuota", Clases.cMensaje.Mensaje());
                return;
            }

            if (ImportePagado < Importe)
            {
                var result = MessageBox.Show("El importe abonado es inferior al valor de la cuota, Confirma Pago de la cuota?", Clases.cMensaje.Mensaje(),
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question);

                // If the no button was pressed ...
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            Int32 CodGrupo = Convert.ToInt32(txtCodGrupo.Text);
            Int32 Cuota = Convert.ToInt32(txtCuota.Text);
            DateTime FechaPago = Convert.ToDateTime(txtFecha.Text);
            double Saldo = Importe - ImportePagado;
            Double Punitorio = 0;
            if (txtPunitorio.Text != "")
                Punitorio = fun.ToDouble(txtPunitorio.Text);
            Clases.cCuotasAnteriores objCuota = new Clases.cCuotasAnteriores();
            if (objCuota.GrabarCuota(CodGrupo, Cuota, FechaPago, ImportePagado, Saldo, CodUsuario, txtPatente.Text,Punitorio))
            {
                MessageBox.Show("Cuota grabada correctamente.", Clases.cMensaje.Mensaje());
                CargarPlandeCuotas(Convert.ToInt32(CodGrupo));

                txtTotalDeuda.Text = objCuota.GetSaldoDeudaCuotas(Convert.ToInt32(CodGrupo)).ToString();
                if (txtTotalDeuda.Text != "")
                {
                    txtTotalDeuda.Text = fun.TransformarEntero(txtTotalDeuda.Text);
                    txtTotalDeuda.Text = fun.FormatoEnteroMiles(txtTotalDeuda.Text);
                }
            }
            else
            {
                MessageBox.Show("Hubo un error en el proceso de grabación.", Clases.cMensaje.Mensaje());
            }
        }

        private void txtPatente_Leave(object sender, EventArgs e)
        {
            txtPatente.Text = txtPatente.Text.ToUpper();
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
            Int32 CodGrupo = Convert.ToInt32(txtCodGrupo.Text);
            GetPunitorio(CodGrupo, Cuota);
            Double Punitorio = 0;
            if (txtPunitorio.Text != "")
                Punitorio = fun.ToDouble(txtPunitorio.Text);
            Punitorio = Punitorio * (-1);
            Clases.cCuotasAnteriores objCuota = new Clases.cCuotasAnteriores();
            if (objCuota.AnularCuota(CodGrupo, Cuota, ImportePagado, CodUsuario, txtPatente.Text,Punitorio))
            {
                MessageBox.Show("Datos grabados correctamente.", Clases.cMensaje.Mensaje());
                CargarPlandeCuotas(CodGrupo);
            }
        }

        private void FrmCobroDocumentosAnteriores_Load(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmb_CodMarca, "Marca", "Nombre", "CodMarca");
            if (Principal.CodigoPrincipalAbm != null)
            {
                txtCodGrupo.Text = Principal.CodigoPrincipalAbm.ToString();
                Buscar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCodGrupo.Text == "")
            {
                MessageBox.Show("Debe ingresar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Principal.CampoNombreSecundario = txtCodGrupo.Text;
            Principal.NombreTablaSecundario = "MensajesCuotasAnteriores";
            FrmMensaje form = new FrmMensaje();
            form.ShowDialog();
        }

        private void txtImportePagado_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtImportePagado.Text = fun.FormatoEnteroMiles(txtImportePagado.Text);
            double Importe = 0;
            double ImportePagado = 0;
            double Saldo = 0;

            if (txtImporte.Text != "")
            {
                Importe = fun.ToDouble(txtImporte.Text);
            }
            if (txtImportePagado.Text != "")
            {
                ImportePagado = fun.ToDouble(txtImportePagado.Text);
            }
            Saldo = Importe - ImportePagado;
            txtSaldo.Text = Saldo.ToString();
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
            Int32 CodGrupo = Convert.ToInt32(txtCodGrupo.Text);

            Clases.cSaldoCuotasAnteriores salCuotas = new Clases.cSaldoCuotasAnteriores(); 
            Clases.cCuotasAnteriores  cuota = new Clases.cCuotasAnteriores();
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            cuota.PagarSaldoCuota(CodGrupo, Cuota, Importe);
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, Importe, 0, 0, 0, 0, Fecha, Descripcion);
            salCuotas.InsertarSaldoCob(CodGrupo, Cuota, Fecha, Importe);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (txtCodGrupo.Text == "")
            {
                MessageBox.Show("Debe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            FrmDetalleSaldoCuotasAnteriores frm = new FrmDetalleSaldoCuotasAnteriores();
            Principal.CodigoPrincipalAbm = txtCodGrupo.Text;
            frm.ShowDialog();
        }

        private void GetPunitorio(Int32 CodGrupo, Int32 Cuota)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cPunitorioCuotasAnteriores puni = new Clases.cPunitorioCuotasAnteriores();
            Double Importe = puni.GetImportePunitorio(CodGrupo, Cuota);
            txtPunitorio.Text = Importe.ToString();
            if (txtPunitorio.Text != "")
                txtPunitorio.Text = fun.FormatoEnteroMiles(txtPunitorio.Text);
        }
    }
}