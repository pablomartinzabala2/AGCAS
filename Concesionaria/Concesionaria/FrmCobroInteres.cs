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
    public partial class FrmCobroInteres : Form
    {
        public FrmCobroInteres()
        {
            InitializeComponent();
        }

        private void FrmCobroInteres_Load(object sender, EventArgs e)
        {    
            CmbOpciones.Items.Add("Agregar");
            CmbOpciones.Items.Add("Descontar");
            CmbOpciones.SelectedIndex = 0;
            dpFechaPago.Value = DateTime.Now;
            if (Principal.CodigoPrincipalAbm != null)
            {
                GetDatosxPrestamo(Convert.ToInt32(Principal.CodigoPrincipalAbm));
            }
        }

        public void GetDatosxPrestamo(Int32 CodPrestamo)
        {  
            VerificarPagoInteres(CodPrestamo);
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cPrestamoCobrar prestamo = new Clases.cPrestamoCobrar();
            DataTable trdo = prestamo.GetPrestamoxCodigo(CodPrestamo);
            if (trdo.Rows.Count > 0)
            {  
                txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txtDireccion.Text = trdo.Rows[0]["Direccion"].ToString();
                if (trdo.Rows[0]["Fecha"].ToString()!="")
                    dpFecha.Value  = Convert.ToDateTime (trdo.Rows[0]["Fecha"].ToString());
                txtPorcentaje.Text = trdo.Rows[0]["PorcentajeInteres"].ToString();
                txtTelefono.Text = trdo.Rows[0]["Telefono"].ToString();
                if (trdo.Rows[0]["FechaVencimiento"].ToString()!="")
                    dpFechaVencimiento.Value =Convert.ToDateTime (trdo.Rows[0]["FechaVencimiento"].ToString());
                txtMontoApagar.Text = trdo.Rows[0]["ImporteaPagar"].ToString();
                txtImporte.Text = trdo.Rows[0]["Importe"].ToString();
                if (txtMontoApagar.Text != "")
                    txtMontoApagar.Text = fun.ParteEntera(txtMontoApagar.Text);
                if (txtMontoApagar.Text != "")
                    txtMontoApagar.Text = fun.FormatoEnteroMiles(txtMontoApagar.Text);

                if (txtImporte.Text != "")
                    txtImporte.Text = fun.ParteEntera(txtImporte.Text);
                if (txtImporte.Text != "")
                    txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
            }
            CargarGrilla(CodPrestamo);
            CargarDetalle(CodPrestamo);
        }

        private void VerificarPagoInteres(Int32 CodPrestamo)
        {
             
           // Clases.cPrestamo prestamo = new Clases.cPrestamo();
            cPrestamoCobrar prestamo = new cPrestamoCobrar();
            DataTable trdo = prestamo.GetPrestamoxCodigo(CodPrestamo);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["FechaPago"].ToString() != "")
                {
                    btnGrabar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                }
            }
        }

        private void CargarGrilla(Int32 CodPrestamo)
        {
            cCobroInteres cobro = new cCobroInteres();
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cPagoIntereses pago = new Clases.cPagoIntereses();
            // DataTable trdo = pago.GetInteresesPagadosxCodPrestamo(CodPrestamo);
            DataTable trdo = cobro.GetInteresesPagadosxCodPrestamo(CodPrestamo);
           trdo = fun.TablaaFechas(trdo, "Importe");
            Grilla.DataSource = trdo;
            fun.AnchoColumnas(Grilla, "0;37;63");
            
        }

        private void CargarDetalle(Int32 CodPrestamo)
        {
            
            Clases.cDetallePrestamoCobrar detalle = new Clases.cDetallePrestamoCobrar();
            DataTable trdo = detalle.GetDetallePrestamo(CodPrestamo);
            Clases.cFunciones fun = new Clases.cFunciones();
            trdo = fun.TablaaMiles(trdo, "Importe");
            GrillaDetallePrestamo.DataSource = trdo;
            fun.AnchoColumnas (GrillaDetallePrestamo,"40;30;30");
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtMontoModificar.Text == "")
            {
                MessageBox.Show("Debe ingresar un monto para continuar ", Clases.cMensaje.Mensaje());
                return;
            }

            Int32 CodPrestamo = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            DateTime Fecha = dpFechaPago.Value;
            double Importe = fun.ToDouble(txtMontoModificar.Text);
            string DescripcionDetalle = "INGRESO PRESTAMO A COBRAR " + Importe.ToString().Replace(",", ".");
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
            txtImporte.Text = (fun.ToDouble(txtImporte.Text) + fun.ToDouble(MontoModificar.ToString())).ToString();
            txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
            CalcularPorcentaje();
             
            Clases.cDetallePrestamoCobrar detalle = new Clases.cDetallePrestamoCobrar();
            detalle.InsertarDetallePrestamo(CodPrestamo, Importe, DescripcionDetalle, Fecha);
            //cargo el nuevo porcentaje
            double Por = Convert.ToDouble(txtPorcentaje.Text.Replace(".", ","));
            double MontoFinal = fun.ToDouble(txtImporte.Text);
            double ImporteaPagar = fun.ToDouble(txtMontoApagar.Text);
            DateTime FechaVencimiento = dpFechaVencimiento.Value;
             
            Clases.cPrestamoCobrar prestamo = new Clases.cPrestamoCobrar();
            prestamo.ModificarPorcentajePrestamo(CodPrestamo, Por, ImporteaPagar, Fecha, MontoFinal);
            CargarDetalle(CodPrestamo);
            string DescripcionMovimiento = "";
            if (MontoModificar > 0)
                DescripcionMovimiento = " INGRESO PRESTAMO A COBRAR" + txtNombre.Text;
            else
                DescripcionMovimiento = " RETIRO PRESTAMO A COBRAR" + txtNombre.Text;
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, (-1) * MontoModificar, 0, 0, 0, 0, Fecha, DescripcionMovimiento);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
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
                    Por = Convert.ToDouble(txtPorcentaje.Text.Replace(".", ","));

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

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(dpFechaPago.Value.ToShortDateString ()) == false)
            {
                MessageBox.Show("La fecha de pago es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            string Descripcion = "COBRO DE INTERÉS " + txtNombre.Text.ToString();
            Int32 CodPrestamo = Convert.ToInt32(Principal.CodigoPrincipalAbm);
            DateTime Fecha = Convert.ToDateTime(dpFechaPago.Value);
            double Importe = fun.ToDouble(txtMontoApagar.Text);
            cPrestamo Prestamo = new cPrestamo();
             
            Clases.cMovimiento mov = new Clases.cMovimiento();
            Clases.cCobroInteres Cobro = new Clases.cCobroInteres();
            Cobro.RegistrarCobro(CodPrestamo, Fecha, Importe);
            //   Prestamo.RegistrarDevolucion(CodPrestamo, Fecha);
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado,  Importe, 0, 0, 0, 0, Fecha, Descripcion);
            MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
            CargarGrilla(CodPrestamo);
            GetDatosxPrestamo(CodPrestamo);

        }
    }
}
