using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace Concesionaria
{
    public partial class FrmPagoInteres : Form
    {
        public FrmPagoInteres()
        {
            InitializeComponent();
        }

        private void FrmPagoInteres_Load(object sender, EventArgs e)
        {
            CmbOpciones.Items.Add ("Agregar");
            CmbOpciones.Items.Add ("Descontar");
            CmbOpciones.SelectedIndex =0;
            txtFechaPago.Text = DateTime.Now.ToShortDateString();
            if (Principal.CodigoPrincipalAbm != null)
            {
                GetDatosxPrestamo(Convert.ToInt32(Principal.CodigoPrincipalAbm)); 
            }
        }

        public void GetDatosxPrestamo(Int32 CodPrestamo)
        {
            VerificarPagoInteres(CodPrestamo);
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cPrestamo prestamo = new Clases.cPrestamo();
            DataTable trdo = prestamo.GetPrestamoxCodigo(CodPrestamo);
            if (trdo.Rows.Count > 0)
            {
                txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txtDireccion.Text = trdo.Rows[0]["Direccion"].ToString();
                txtFecha.Text = trdo.Rows[0]["Fecha"].ToString();
                txtPorcentaje.Text = trdo.Rows[0]["PorcentajeInteres"].ToString();
                txtTelefono.Text = trdo.Rows[0]["Telefono"].ToString();
                txtFechaVencimiento.Text = trdo.Rows[0]["FechaVencimiento"].ToString();
                txtMontoApagar.Text = trdo.Rows[0]["ImporteaPagar"].ToString();
                txtImporte.Text = trdo.Rows[0]["Importe"].ToString();
                if (txtMontoApagar.Text != "")
                    txtMontoApagar.Text = fun.ParteEntera(txtMontoApagar.Text);
                if (txtMontoApagar.Text != "")
                    txtMontoApagar.Text = fun.FormatoEnteroMiles (txtMontoApagar.Text);

                if (txtImporte.Text != "")
                    txtImporte.Text = fun.ParteEntera(txtImporte.Text);
                if (txtImporte.Text != "")
                    txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
            }
            CargarGrilla(CodPrestamo);
            CargarDetalle(CodPrestamo); 
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFechaPago.Text) == false)
            {
                MessageBox.Show("La fecha de pago es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            string Descripcion = "PAGO DE INTERÉS " + txtNombre.Text.ToString();
            Int32 CodPrestamo = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            DateTime Fecha = Convert.ToDateTime(txtFechaPago.Text);
            double Importe = fun.ToDouble(txtMontoApagar.Text);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            Clases.cPagoIntereses pago = new Clases.cPagoIntereses();
            pago.RegistrarPago(CodPrestamo, Fecha, Importe);
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, -1 * Importe, 0, 0, 0, 0, Fecha, Descripcion);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            CargarGrilla(CodPrestamo);
        }
        private void CargarGrilla(Int32 CodPrestamo)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cPagoIntereses pago = new Clases.cPagoIntereses();
            DataTable trdo = pago.GetInteresesPagadosxCodPrestamo(CodPrestamo);
            trdo = fun.TablaaFechas(trdo, "Importe");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false; 
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro para continuar", Clases.cMensaje.Mensaje());  
                return;
            }

            if (fun.ValidarFecha(txtFechaPago.Text) == false)
            {
                MessageBox.Show("La fecha de Pago Ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }
            string msj = "Confirma eliminar el Pago ";
            var result = MessageBox.Show(msj, "Información",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }

            DateTime Fecha = Convert.ToDateTime (txtFechaPago.Text); 
            Int32 CodPago = Convert.ToInt32 (Grilla.CurrentRow.Cells[0].Value);
            double Importe = fun.ToDouble(Grilla.CurrentRow.Cells[2].Value.ToString ());
            Clases.cPagoIntereses Pago = new Clases.cPagoIntereses();
            Pago.BorrarPago(CodPago);
            string Descripcion = "ANULACION PAGO " + txtNombre.Text;
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, Importe, 0, 0, 0, 0, Fecha, Descripcion);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje()); 
        }

        private void VerificarPagoInteres(Int32 CodPrestamo)
        {
            Clases.cPrestamo prestamo = new Clases.cPrestamo();
            DataTable trdo = prestamo.GetPrestamoxCodigo(CodPrestamo);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["FechaPago"].ToString() != "")
                {
                    btnGrabar.Enabled = false;
                    btnEliminar.Enabled = false;
                }
            }
        }

        private void CargarDetalle(Int32 CodPrestamo)
        {
            Clases.cDetallePrestamo detalle = new Clases.cDetallePrestamo();
            DataTable trdo = detalle.GetDetallePrestamo(CodPrestamo);
            Clases.cFunciones fun = new Clases.cFunciones();
            trdo = fun.TablaaMiles(trdo, "Importe");
            GrillaDetallePrestamo.DataSource = trdo;
        }

        private void txtMontoModificar_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtMontoModificar.Text != "")
            {
                txtMontoModificar.Text = fun.FormatoEnteroMiles(txtMontoModificar.Text); 
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            if (txtMontoModificar.Text == "")
            {
                MessageBox.Show("Debe ingresar un monto para continuar ", Clases.cMensaje.Mensaje());
                return;
            }
            
            Int32 CodPrestamo = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            DateTime Fecha = Convert.ToDateTime(txtFechaPago.Text);
            double Importe = fun.ToDouble(txtMontoModificar.Text); 
            string DescripcionDetalle = "INGRESO PRESTAMO " + Importe.ToString().Replace(",", ".");
            double MontoAnterio = fun.ToDouble(txtImporte.Text);
            double MontoModificar = fun.ToDouble(txtMontoModificar.Text);
            if (CmbOpciones.SelectedIndex == 0)
            {
                
                DescripcionDetalle = "AGREGAR CAPITAL " + Importe.ToString();
            }
            else
            {
                MontoModificar = -1 * MontoModificar;
                DescripcionDetalle = "DESCUENTO DE CAPITAL " + Importe.ToString();
            }
            txtImporte.Text = (fun.ToDouble(txtImporte.Text) +  fun.ToDouble(MontoModificar.ToString ())).ToString();
            txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
            CalcularPorcentaje(); 
            
            Clases.cDetallePrestamo detalle = new Clases.cDetallePrestamo();
            detalle.InsertarDetallePrestamo(CodPrestamo, Importe, DescripcionDetalle, Fecha);
            //cargo el nuevo porcentaje
            double Por = Convert.ToDouble (txtPorcentaje.Text.Replace (".",","));
            double MontoFinal = fun.ToDouble(txtImporte.Text);
            double ImporteaPagar = fun.ToDouble (txtMontoApagar.Text);
            DateTime FechaVencimiento = Convert.ToDateTime (txtFechaVencimiento.Text);
            Clases.cPrestamo prestamo = new Clases.cPrestamo();
            prestamo.ModificarPorcentajePrestamo (CodPrestamo ,Por ,ImporteaPagar ,Fecha ,MontoFinal );
            CargarDetalle(CodPrestamo);
            string DescripcionMovimiento = "";
            if (MontoModificar > 0)
                DescripcionMovimiento = " INGRESO PRESTAMO " + txtNombre.Text;
            else
                DescripcionMovimiento = " RETIRO PRESTAMO " + txtNombre.Text;
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, MontoModificar, 0, 0, 0, 0, Fecha, DescripcionMovimiento);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());

            
        }

        private void txtPorcentaje_Leave(object sender, EventArgs e)
        {
            CalcularPorcentaje();
        }

        private void CalcularPorcentaje()
        {
            if (txtPorcentaje.Text != "0" && txtImporte.Text != "0")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                double Por = 0;
                double Monto = 0;
                double aPagar = 0;
                if (txtPorcentaje.Text != "")
                    Por = Convert.ToDouble (txtPorcentaje.Text.Replace (".",","));

                if (txtImporte.Text != "")
                    Monto = fun.ToDouble(txtImporte.Text);
                aPagar = (Monto * Por) / 100;
                txtMontoApagar.Text = aPagar.ToString();
                if (txtMontoApagar.Text != "")
                {
                    decimal m = Convert.ToDecimal(aPagar);
                    txtMontoApagar.Text = decimal.Round(m, 0).ToString();
                    txtMontoApagar.Text = fun.FormatoEnteroMiles(txtMontoApagar.Text);
                }
                //txtMontoApagar.Text = fun.FormatoEnteroMiles(txtMontoApagar.Text);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Principal.CodigoPrincipalAbm == null)
            {
                MessageBox.Show("Debe ingresar un registro para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Principal.CampoNombreSecundario = Principal.CodigoPrincipalAbm;
            Principal.NombreTablaSecundario = "MensajesPrestamos";
            FrmMensaje form = new FrmMensaje();
            form.ShowDialog();
        }
    }
}
