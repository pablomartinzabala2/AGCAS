using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Concesionaria
{

    public partial class FrmAutos : Form
    {

        public FrmAutos()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmAutos_Load(object sender, EventArgs e)
        {
            try
            {
                InicializarComponentes();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString()); 
            }
            
            //PintarTextBox();
        }

        private void InicializarComponentes()
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LlenarCombo(cmb_CodMarca, "Marca", "Nombre", "CodMarca");
            fun.LlenarCombo(cmbCiudad, "Ciudad", "Nombre", "CodCiudad");
            if (cmbCiudad.Items.Count >0)
                cmbCiudad.SelectedValue = 1;
            fun.LlenarCombo(cmbDocumento, "TipoDocumento", "Nombre", "CodTipoDoc");
            if (cmbDocumento.Items.Count > 0)
                cmbDocumento.SelectedIndex = 1;
            cmbDocumento.Enabled = false;
            fun.LlenarCombo(CmbBarrio, "Barrio", "Nombre", "CodBarrio");
            //fun.LlenarCombo(CmbCategoriaGasto, "CategoriaGasto", "Nombre", "CodCategoriaGasto");
            fun.LlenarCombo(CmbGastoRecepcion, "CategoriaGastoRecepcion", "Descripcion", "Codigo");
            fun.LlenarCombo(CmbTipoCombustible, "TipoCombustible", "Nombre", "Codigo");
            fun.LlenarCombo(CmbBanco, "Banco", "Nombre", "CodBanco");
        }

        private void GrabarAutos(SqlConnection con, SqlTransaction Transaccion)
        {
            string Patente = "";
            Int32? CodMarca = null;
            string Descripcion = "";
            Int32? Kilometros = null;
            Int32? CodCiudad = null;
            int Propio = 0;
            int Concesion = 0;
            string Observacion = "";
            string Anio = "";
            Double? Importe = 0;
            Int32 CodStock = -1;
            Int32 CodAuto = 0;
            string Motor = "";
            string Chasis = "";
            string Color = "";


            Patente = txtPatente.Text;
            Color = txtColor.Text;
            if (cmbCiudad.SelectedIndex > 0)
                CodCiudad = Convert.ToInt32(cmbCiudad.SelectedValue);

            Descripcion = txtDescripcion.Text;
            Anio = txtAnio.Text;
            if (txtKilometros.Text != "")
                Kilometros = Convert.ToInt32(txtKilometros.Text.Replace(".", ""));
            if (cmb_CodMarca.SelectedIndex > 0)
            {
                CodMarca = Convert.ToInt32(cmb_CodMarca.SelectedValue);
            }

            if (radioPropio.Checked)
            {
                Propio = 1;
            }

            if (radioConcesion.Checked)
            {
                Concesion = 1;
            }

            if (txtImporte.Text != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                Importe = fun.ToDouble(txtImporte.Text);
            }

            Motor = txtMotor.Text;
            Chasis = txtChasis.Text;
            Int32? CodTipoCombustible = null;
            if (CmbTipoCombustible.SelectedIndex > 0)
                CodTipoCombustible = Convert.ToInt32(CmbTipoCombustible.SelectedValue);

            Clases.cAuto auto = new Clases.cAuto();
            Boolean Graba = true;
            if (txtCodAuto.Text != "")
                Graba = false;
            if (Graba)
            {
                //inserto el auto
                auto.AgregarAutoTransaccion(con, Transaccion, Patente, CodMarca, Descripcion,
                    Kilometros, CodCiudad, Propio, Concesion, Observacion, Anio, Importe, Motor, Chasis, Color, CodTipoCombustible);
                CodAuto = auto.GetMaxCodAutoTransaccion(con, Transaccion);
                txtCodAuto.Text = CodAuto.ToString();


            }
            else
            {
                auto.ModificarAuto(Patente, CodMarca, Descripcion,
                    Kilometros, CodCiudad, Propio, Concesion, Observacion, Anio, Importe, Motor, Chasis, Color);
            }
            if (txtCodStock.Text == "")
            {
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                //inserto el stock
                CodAuto = Convert.ToInt32(txtCodAuto.Text);
                Int32? CodCliente = null;
                if (txtCodCLiente.Text != "")
                    CodCliente = Convert.ToInt32(txtCodCLiente.Text);
                Clases.cStockAuto stockAuto = new Clases.cStockAuto();
                stockAuto.InsertarStockAutoTransaccion(con, Transaccion, CodAuto, Fecha.ToShortDateString(), CodCliente, Principal.CodUsuarioLogueado, Importe);
                Clases.cCosto costo = new Clases.cCosto();
                CodStock = stockAuto.GetMaxCodStockxAutoTransaccion(con, Transaccion, CodAuto);
                txtCodStock.Text = CodStock.ToString();

                //string DescripcionCompra = "Precio de compra ";
                //costo.InsertarCosto(CodAuto, Patente, Importe, Fecha, DescripcionCompra, CodStock);
            }

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtCodStock.Text != "")
            {
                MessageBox.Show("El vehículo ya esta cargado como stock", Clases.cMensaje.Mensaje());
                LimpiarAuto();
                LimpiarCliente();
                txtCodStock.Text = "";
                txtCodAuto.Text = "";
                return;
            }

            if (txtFecha.Text == "")
            {
                MessageBox.Show("Ingresar una fecha para  continuar", Clases.cMensaje.Mensaje());
                return;
            }

            Int32 Concesion = 0;
            if (radioConcesion.Checked)
                Concesion = 1;

            Clases.cFunciones fun = new Clases.cFunciones();
            double Total = 0;
            double Efectivo = 0;
            double Vehiculos = 0;
            double TotalCheques = 0;
            double EfectivoaPagar = 0;
            if (txtTotal.Text != "")
                Total = fun.ToDouble(txtTotal.Text);

            if (txtTotalEfectivo.Text != "")
                Efectivo = fun.ToDouble(txtTotalEfectivo.Text);

            if (txtTotalVehiculo.Text != "")
                Vehiculos = fun.ToDouble(txtTotalVehiculo.Text);

            if (txtTotalCheque.Text != "")
                TotalCheques = fun.ToDouble(txtTotalCheque.Text);

            if (txtEfectivoaPagar.Text != "")
                EfectivoaPagar = fun.ToDouble(txtEfectivoaPagar.Text);

            double dif = Total - Efectivo - Vehiculos - TotalCheques - EfectivoaPagar;
            if (Concesion == 0)
                if (dif != 0)
                {
                    MessageBox.Show("No coinciden los subtotales con el el total", Clases.cMensaje.Mensaje());
                    return;
                }

            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                MessageBox.Show("La fecha ingresada es incorrecta", Clases.cMensaje.Mensaje());
                return;
            }

            Int32 CodTipoDoc = 0;
            if (cmbDocumento.SelectedIndex > 0)
                CodTipoDoc = Convert.ToInt32(cmbDocumento.SelectedValue);
            Clases.cCliente cliente = new Clases.cCliente();
            string NroDocumento = txtNroDoc.Text;
            Boolean Nuevo = true;
            if (NroDocumento != "")
            {
                DataTable trdo = cliente.GetClientexNroDoc(CodTipoDoc, NroDocumento);

                if (trdo.Rows.Count > 0)
                {
                    if (trdo.Rows[0]["Nombre"].ToString() != "")
                        Nuevo = false;
                }

            }

            SqlConnection con = new SqlConnection();
            con.ConnectionString = Clases.cConexion.Cadenacon();
            con.Open();
            SqlTransaction Transaccion;
            Transaccion = con.BeginTransaction();
            try
            {
                if (GuardarCliente(con, Transaccion, Nuevo) == true)
                {
                    Int32 CodCompra = 0;
                    GrabarAutos(con, Transaccion);
                    
                    if (Concesion == 0)
                        CodCompra = GrabarCompra(con, Transaccion);
                    if (Concesion == 0)
                        GrabarGastosPagar(con, Transaccion, Convert.ToInt32(txtCodAuto.Text),CodCompra);
                    if (Concesion == 0)
                        GrabarCheques(con, Transaccion, CodCompra);
                    if (Concesion == 0)
                        GrabarMovimiento(con, Transaccion, CodCompra);
                    if (Concesion == 0)
                        GrabarMovimientoGastoRecepcion(con, Transaccion, CodCompra);
                    if (txtTotalEfectivosaPagar.Text != "" && txtTotalEfectivosaPagar.Text != "0")
                    {
                        Int32 CodAuto = Convert.ToInt32(txtCodAuto.Text);
                        Int32? CodCliente = null;
                        if (txtCodCLiente.Text != "")
                            CodCliente = Convert.ToInt32(txtCodCLiente.Text);
                        double ImporteaPagar = fun.ToDouble(txtEfectivoaPagar.Text);
                        Clases.cEfectivoaPagar objEft = new Clases.cEfectivoaPagar();
                        objEft.Insertar(con, Transaccion, Convert.ToDateTime(txtFecha.Text), ImporteaPagar, CodCompra, CodCliente, CodAuto);
                    }
                    if (txtTotalVehiculo.Text !="")
                        GrabarVenta(con, Transaccion);
                    Transaccion.Commit();
                    con.Close();
                    MessageBox.Show("Datos grabados correctamente", Clases.cMensaje.Mensaje());
                    LimpiarTodos();
                }

            }
            catch (Exception ex)
            {
                string msj = "Hubo un error en el proceso " + ex.Message.ToString();
                MessageBox.Show(msj, Clases.cMensaje.Mensaje());

                Transaccion.Rollback();
                con.Close();

            }

        }
        private void LimpiarTodos()
        {
            LimpiarAuto();
            LimpiarCliente();
            txtPatente.Text = "";
            txtNroDoc.Text = "";
            txtEfectivo.Text = "";
            GrillaCheques.DataSource = null;
            // txtImporteGasto.Text = "";
            txtTotalGastosRecepcion.Text = "";
            GrillaGastosRecepcion.DataSource = null;
            txtImporteGastoRecepcion.Text = "";
            txtTotalEfectivo.Text = "";
            txtTotalVehiculo.Text = "";
            txtTotalCheque.Text = "";
            txtTotal.Text = "";
            txtPatente2.Text = "";
            txtDescripcion2.Text = "";
            txtImporteVehiculo2.Text = "";
            txtCodStock2.Text = "";
            txtCostoxAuto.Text = "";
            LimpiarCliente();
            txtCodAuto2.Text = "";
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
            int b = 0;
            string Patente = txtPatente.Text;
            if (Patente.Length > 5)
            {
                Clases.cAuto auto = new Clases.cAuto();
                DataTable trdo = auto.GetAutoxPatente(Patente);
                if (trdo.Rows.Count > 0)
                {
                    b = 1;
                    txtDescripcion.Text = trdo.Rows[0]["Descripcion"].ToString();
                    txtAnio.Text = trdo.Rows[0]["Anio"].ToString();
                    txtKilometros.Text = trdo.Rows[0]["Kilometros"].ToString();
                    txtChasis.Text = trdo.Rows[0]["Chasis"].ToString();
                    txtColor.Text = trdo.Rows[0]["Color"].ToString();
                    txtMotor.Text = trdo.Rows[0]["Motor"].ToString();
                    Clases.cFunciones fun = new Clases.cFunciones();
                    if (txtKilometros.Text != "")
                    {
                        txtKilometros.Text = fun.FormatoEnteroMiles(txtKilometros.Text);
                    }
                    txtCodAuto.Text = trdo.Rows[0]["CodAuto"].ToString();

                    if (trdo.Rows[0]["Importe"].ToString() != "")
                    {
                        //string xx = trdo.Rows[0]["Importe"].ToString().Replace (",",".").ToString();
                        txtImporte.Text = fun.TransformarEntero(trdo.Rows[0]["Importe"].ToString());
                        txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
                    }

                    if (trdo.Rows[0]["CodCiudad"].ToString() != "")
                    {
                        cmbCiudad.SelectedValue = trdo.Rows[0]["CodCiudad"].ToString();
                    }

                    if (trdo.Rows[0]["CodMarca"].ToString() != "")
                    {
                        cmb_CodMarca.SelectedValue = trdo.Rows[0]["CodMarca"].ToString();
                    }

                    if (trdo.Rows[0]["Propio"].ToString() == "1")
                    {
                        radioPropio.Checked = true;
                        radioConcesion.Checked = false;
                    }

                    if (trdo.Rows[0]["Concesion"].ToString() == "1")
                    {
                        radioPropio.Checked = false;
                        radioConcesion.Checked = true;
                    }

                    Clases.cStockAuto stock = new Clases.cStockAuto();
                    DataTable trdo2 = stock.GetStockAutosVigentes(Convert.ToInt32(txtCodAuto.Text));
                    if (trdo2.Rows.Count > 0)
                    {
                        txtCodStock.Text = trdo2.Rows[0]["CodStock"].ToString();
                        GetGastos(Convert.ToInt32(txtCodStock.Text));
                        if (trdo2.Rows[0]["CodCliente"].ToString() != "")
                        {
                            txtCodCLiente.Text = trdo2.Rows[0]["CodCliente"].ToString();
                            GetClientesxCodigo(Convert.ToInt32(txtCodCLiente.Text));
                        }
                    }
                }
            }
            if (b == 0)
                LimpiarAuto();
        }

        private void GetGastos(Int32 CodStock)
        {/*
            Clases.cGasto gasto = new Clases.cGasto();
            DataTable trdo = gasto.GetGastosxCodStock(CodStock);
            Clases.cFunciones fun = new Clases.cFunciones();
            Grilla.DataSource = trdo;
            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                if (Grilla.Rows[i].Cells[2].Value.ToString() != "")
                {
                    Grilla.Rows[i].Cells[2].Value = fun.TransformarEntero(Grilla.Rows[i].Cells[2].Value.ToString());
                    // Grilla.Rows[i].Cells[2].Value = fun.FormatoEnteroMiles (fun.TransformarEntero(Grilla.Rows[i].Cells[2].Value.ToString ()));
                }
                Grilla.Columns[1].Width = 580;
                Grilla.Columns[0].Visible = false;
            }
           */
        }

        private void LimpiarAuto()
        {
            txtCodAuto.Text = "";
            txtCodStock.Text = "";
            cmb_CodMarca.SelectedIndex = 0;
            txtDescripcion.Text = "";
            txtAnio.Text = "";
            txtKilometros.Text = "";
            txtImporte.Text = "";
            txtChasis.Text = "";
            txtMotor.Text = "";
            GetGastos(-1);
            txtColor.Text = "";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
            if (CmbBarrio.Items.Count > 0)
                CmbBarrio.SelectedIndex = 0;
            txtCalle.Text = "";
            txtAltura.Text = "";
        }

        private void btnAgregarCiudad_Click(object sender, EventArgs e)
        {
            Principal.CampoIdSecundario = "CodCiudad";
            Principal.CampoNombreSecundario = "Nombre";
            Principal.NombreTablaSecundario = "Ciudad";
            FrmAltaBasica form = new FrmAltaBasica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Principal.CampoIdSecundarioGenerado != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                switch (Principal.NombreTablaSecundario)
                {
                    case "Ciudad":
                        fun.LlenarCombo(cmbCiudad, "Ciudad", "Nombre", "CodCiudad");
                        cmbCiudad.SelectedValue = Principal.CampoIdSecundarioGenerado;
                        break;
                    case "Marca":
                        fun.LlenarCombo(cmb_CodMarca, "Marca", "Nombre", "CodMarca");
                        cmb_CodMarca.SelectedValue = Principal.CampoIdSecundarioGenerado;
                        break;
                    case "Barrio":
                        fun.LlenarCombo(CmbBarrio, "Barrio", "Nombre", "CodBarrio");
                        CmbBarrio.SelectedValue = Principal.CampoIdSecundarioGenerado;
                        break;
                    case "CategoriaGasto":
                        // fun.LlenarCombo(CmbCategoriaGasto, "CategoriaGasto", "Nombre", "CodCategoriaGasto");
                        // CmbCategoriaGasto.SelectedValue = Principal.CampoIdSecundarioGenerado;
                        break;
                    case "CategoriaGastoRecepcion":
                        fun.LlenarCombo(CmbGastoRecepcion, "CategoriaGastoRecepcion", "Descripcion", "Codigo");
                        CmbGastoRecepcion.SelectedValue = Principal.CampoIdSecundarioGenerado;
                        break;

                }
            }

        }

        private Boolean GuardarCliente(SqlConnection con, SqlTransaction Transaccion, Boolean Nuevo)
        {
            /*  if (txtNroDoc.Text == "")
              {
                  MessageBox.Show("Debe ingresar un número de documento para continuar.", Clases.cMensaje.Mensaje());
                  return false;
              }
             * */
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Debe ingresar un nombre de un nombre para continuar.", Clases.cMensaje.Mensaje());
                return false;
            }

            if (txtApellido.Text == "")
            {
                MessageBox.Show("Debe ingresar un nombre de un apellido para continuar.", Clases.cMensaje.Mensaje());
                return false;
            }

            Int32? CodTipoDoc = null;
            if (cmbDocumento.SelectedIndex > 0)
                CodTipoDoc = Convert.ToInt32(cmbDocumento.SelectedValue);
            string NroDocumento = txtNroDoc.Text;
            Clases.cCliente cliente = new Clases.cCliente();


            string Nombre = txtNombre.Text;
            string Apellido = txtApellido.Text;
            string Telefono = txtTelefono.Text;
            string Celular = txtCelular.Text;
            string Calle = txtCalle.Text;
            string Altura = txtAltura.Text;
            Int32? CodBarrio = null;

            if (CmbBarrio.SelectedIndex > 0)
                CodBarrio = Convert.ToInt32(CmbBarrio.SelectedValue);

            if (Nuevo == true)
            {
                cliente.InsertarClienteTransaccion(con, Transaccion, CodTipoDoc, NroDocumento, Nombre,
                    Apellido, Telefono, Celular, Calle, Altura, CodBarrio);
                txtCodCLiente.Text = cliente.GetMaxClientetTransaccion(con, Transaccion).ToString();
            }
            else
            {
                cliente.ModificarClientetTransaccion(con, Transaccion, Convert.ToInt32(txtCodCLiente.Text), CodTipoDoc, NroDocumento, Nombre,
                    Apellido, Telefono, Celular,
                    Calle, Altura, CodBarrio);
            }
            return true;
        }

        private void btnNuevoBarrio_Click(object sender, EventArgs e)
        {
            Principal.CampoIdSecundario = "CodBarrio";
            Principal.CampoNombreSecundario = "Nombre";
            Principal.NombreTablaSecundario = "Barrio";
            FrmAltaBasica form = new FrmAltaBasica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void FrmAutos_Activated(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Principal.CampoIdSecundario = "CodMarca";
            Principal.CampoNombreSecundario = "Nombre";
            Principal.NombreTablaSecundario = "Marca";
            Principal.CampoIdSecundarioGenerado = "";
            FrmAltaBasica form = new FrmAltaBasica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void txtImporte_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void txtKms_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);

        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            e.Handled = fun.SoloNumerosEnteros(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            e.Handled = fun.SoloLetras(e);
        }

        private void txtNroDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            e.Handled = fun.SoloNumerosEnteros(e);
        }

        private void btnAgregarCategoriaGasto_Click(object sender, EventArgs e)
        {
            Principal.CampoIdSecundario = "CodCategoriaGasto";
            Principal.CampoNombreSecundario = "Nombre";
            Principal.NombreTablaSecundario = "CategoriaGasto";
            FrmAltaBasica form = new FrmAltaBasica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void txtPatente_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnAgregarGasto_Click(object sender, EventArgs e)
        {/*
            string CodCategoriaGasto = CmbCategoriaGasto.SelectedValue.ToString();
            Clases.cGasto gasto = new Clases.cGasto();
            string Nombre = gasto.GetGastoxCodigo(Convert.ToInt32(CodCategoriaGasto));
            string Importe = txtImporteGasto.Text;
            Clases.cFunciones fun = new Clases.cFunciones();

            string Lista = "CodCategoriaGasto;Nombre;Importe";
            DataTable trdo = fun.CrearTabla(Lista);

            string ListaValores = "";

            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                CodCategoriaGasto = Grilla.Rows[i].Cells[0].Value.ToString();
                Nombre = Grilla.Rows[i].Cells[1].Value.ToString();
                Importe = Grilla.Rows[i].Cells[2].Value.ToString();
                ListaValores = CodCategoriaGasto + ";" + Nombre + ";" + Importe;
                trdo = fun.AgregarFilas(trdo, ListaValores);
            }


            CodCategoriaGasto = CmbCategoriaGasto.SelectedValue.ToString();
            Nombre = gasto.GetGastoxCodigo(Convert.ToInt32(CodCategoriaGasto));
            Importe = txtImporteGasto.Text;
            ListaValores = CodCategoriaGasto + ";" + Nombre + ";" + Importe;
            trdo = fun.AgregarFilas(trdo, ListaValores);
            Grilla.DataSource = trdo;
            Grilla.Columns[1].Width = 580;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
             */
        }

        private void btnEliminarGasto_Click(object sender, EventArgs e)
        {
            /*
            if (Grilla.Rows.Count < 2)
                return;
            string CodCategoriaGastoSel = "";
            CodCategoriaGastoSel = Grilla.CurrentRow.Cells[0].Value.ToString();

            string Lista = "CodCategoriaGasto;Nombre;Importe";
            Clases.cFunciones fun = new Clases.cFunciones();
            DataTable trdo = fun.CrearTabla(Lista);
            string ListaValores = "";

            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                string CodCategoriaGasto = Grilla.Rows[i].Cells[0].Value.ToString();
                string Nombre = Grilla.Rows[i].Cells[1].Value.ToString();
                string Importe = Grilla.Rows[i].Cells[2].Value.ToString();
                ListaValores = CodCategoriaGasto + ";" + Nombre + ";" + Importe;
                trdo = fun.AgregarFilas(trdo, ListaValores);
            }

            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                if (trdo.Rows[i]["CodCategoriaGasto"].ToString() == CodCategoriaGastoSel)
                {
                    trdo.Rows[i].Delete();
                    i = 0;
                }
            }
            Grilla.DataSource = trdo;
              */
        }

        private void GetClientesxCodigo(Int32 CodCliente)
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

            }
            else
                LimpiarCliente();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarTodos();
        }

        private void PintarTextBox()
        {
            txtPatente.BackColor = Color.LightGray;
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

        private void txtImporteGasto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtImporte.Text = fun.FormatoEnteroMiles(txtImporte.Text);
            txtTotal.Text = txtImporte.Text;
        }

        private void txtImporteGasto_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            //txtImporteGasto.Text = fun.FormatoEnteroMiles(txtImporteGasto.Text);
        }

        private void txtKms_Leave(object sender, EventArgs e)
        {
            if (txtKilometros.Text != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                txtKilometros.Text = fun.FormatoEnteroMiles(txtKilometros.Text);
            }

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
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

        private void txtNombre_TextChanged(object sender, EventArgs e)
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

        private void txtApellido_TextChanged(object sender, EventArgs e)
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

        private void txtCalle_TextChanged(object sender, EventArgs e)
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

        private void txtImporteGasto_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregarGastodeRecepcion_Click(object sender, EventArgs e)
        {
            if (CmbGastoRecepcion.SelectedIndex < 1)
            {
                MessageBox.Show("Debe seleccionar una categoría de gasto de recepción ", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtImporteGastoRecepcion.Text == "")
            {
                MessageBox.Show("Debe ingresar un importe de gasto de recepción ", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cGasto gasto = new Clases.cGasto();
            string Descripcion = gasto.GetNombreGastoRecepcionxCodigo(Convert.ToInt32(CmbGastoRecepcion.SelectedValue));
            AgregarGastoRecepcion(CmbGastoRecepcion.SelectedValue.ToString(), Descripcion, txtImporteGastoRecepcion.Text, "Recepcion");
        }

        private void AgregarGastoRecepcion(string Codigo, string Descripcion, string Importe, string Tipo)
        {
            /*  for (int i = 0; i < GrillaGastosRecepcion.Rows.Count - 1; i++)
              {
                  if (GrillaGastosRecepcion.Rows[i].Cells[0].Value.ToString() == Codigo.ToString() && GrillaGastos.Rows[i].Cells[2].Value.ToString() == Tipo)
                  {
                      MessageBox.Show("Ya se ha ingresado el gasto", Clases.cMensaje.Mensaje());
                      return;
                  }
              }
             */
            DataTable tListado = new DataTable();
            tListado.Columns.Add("Codigo");
            tListado.Columns.Add("Descripcion");
            tListado.Columns.Add("Tipo");
            tListado.Columns.Add("Importe");

            for (int i = 0; i < GrillaGastosRecepcion.Rows.Count - 1; i++)
            {
                string sCodigo = GrillaGastosRecepcion.Rows[i].Cells[0].Value.ToString();
                string sDescripcion = GrillaGastosRecepcion.Rows[i].Cells[1].Value.ToString();
                string sTipo = GrillaGastosRecepcion.Rows[i].Cells[2].Value.ToString();
                string sImporte = GrillaGastosRecepcion.Rows[i].Cells[3].Value.ToString();

                DataRow r;
                r = tListado.NewRow();
                r[0] = sCodigo;
                r[1] = sDescripcion;
                r[2] = sTipo;
                r[3] = sImporte;

                tListado.Rows.Add(r);
            }
            DataRow r1;
            r1 = tListado.NewRow();
            r1[0] = Codigo;
            r1[1] = Descripcion;
            r1[2] = Tipo;
            r1[3] = Importe;

            tListado.Rows.Add(r1);
            GrillaGastosRecepcion.DataSource = tListado;
            Clases.cFunciones fun = new Clases.cFunciones();
            GrillaGastosRecepcion.Columns[0].Visible = false;
            GrillaGastosRecepcion.Columns[2].Visible = false;

            txtImporteGastoRecepcion.Text = "";
            GrillaGastosRecepcion.Columns[1].Width = 630;
            GrillaGastosRecepcion.Columns[1].HeaderText = "Descripción";

            txtTotalGastosRecepcion.Text = fun.CalcularTotalGrilla(GrillaGastosRecepcion, "Importe").ToString();
            if (txtTotalGastosRecepcion.Text != "")
            {
                txtTotalGastosRecepcion.Text = fun.FormatoEnteroMiles(txtTotalGastosRecepcion.Text);
            }
        }

        private void txtImporteGastoRecepcion_Leave(object sender, EventArgs e)
        {
            if (txtImporteGastoRecepcion.Text != "")
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                txtImporteGastoRecepcion.Text = fun.FormatoEnteroMiles(txtImporteGastoRecepcion.Text);
            }
        }

        private void btnEliminarGastoRecepcion_Click(object sender, EventArgs e)
        {
            if (GrillaGastosRecepcion.Rows.Count < 2)
                return;
            if (GrillaGastosRecepcion.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un gasto de recepción");
                return;
            }
            string Codigo = GrillaGastosRecepcion.CurrentRow.Cells[0].Value.ToString();
            string Tipo = GrillaGastosRecepcion.CurrentRow.Cells[2].Value.ToString();
            if (Codigo != "")
            {
                Clases.cGrilla.EliminarFilaxdosFiltros(GrillaGastosRecepcion, "Codigo", Codigo, "Tipo", Tipo);
            }
            Clases.cFunciones fun = new Clases.cFunciones();

            txtTotalGastosRecepcion.Text = fun.CalcularTotalGrilla(GrillaGastosRecepcion, "Importe").ToString();
            if (txtTotalGastosRecepcion.Text != "")
            {
                txtTotalGastosRecepcion.Text = fun.FormatoEnteroMiles(txtTotalGastosRecepcion.Text);
            }
        }

        private void txtImporteGastoRecepcion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void GrabarGastosdeRecepcion(Int32 CodStock)
        {
            string CodGastoRecepcion = "";
            Double Importe = 0;
            Clases.cMovimiento mov = new Clases.cMovimiento();
            for (int k = 0; k < GrillaGastosRecepcion.Rows.Count - 1; k++)
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                CodGastoRecepcion = GrillaGastosRecepcion.Rows[k].Cells[0].Value.ToString();
                Importe = fun.ToDouble(GrillaGastosRecepcion.Rows[k].Cells[3].Value.ToString());
                if (CodGastoRecepcion != "")
                {
                    Clases.cGasto gasto = new Clases.cGasto();
                    gasto.GrabarGastosRecepcionxCodStock(CodStock, Convert.ToInt32(CodGastoRecepcion), Importe, DateTime.Now);

                }
            }
        }

        private void GrabarMovimiento()
        {
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            string Descripcion = "COMPRA DE AUTO " + txtPatente.Text;
            Clases.cFunciones fun = new Clases.cFunciones();
            Double Importe = fun.ToDouble(txtImporte.Text);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcion(-1, Principal.CodUsuarioLogueado, (-1) * Importe, 0, 0, Importe, 0, Fecha, Descripcion);
        }

        private void bnAgregarGastosRecepcion_Click(object sender, EventArgs e)
        {
            Principal.CampoIdSecundario = "Codigo";
            Principal.CampoNombreSecundario = "Descripcion";
            Principal.NombreTablaSecundario = "CategoriaGastoRecepcion";
            FrmAltaBasica form = new FrmAltaBasica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
            //fun.LlenarCombo(CmbGastoRecepcion, "CategoriaGastoRecepcion", "Descripcion", "Codigo");
        }

        private void txtImporte_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void GrabarGastosPagar(SqlConnection con, SqlTransaction Transaccion, Int32 CodAuto,Int32 CodCompra)
        {
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);

            Clases.cFunciones fun = new Clases.cFunciones();
            string Nombre = "";
            Int32 CodStock = Convert.ToInt32(txtCodStock.Text);
            double Importe = 0;
            Clases.cGastosPagar gasto = new Clases.cGastosPagar();
            for (int i = 0; i < GrillaGastosRecepcion.Rows.Count - 1; i++)
            {
                Nombre = GrillaGastosRecepcion.Rows[i].Cells[1].Value.ToString();
                if (GrillaGastosRecepcion.Rows[i].Cells[3].Value.ToString() != "")
                    Importe = fun.ToDouble(GrillaGastosRecepcion.Rows[i].Cells[3].Value.ToString());
                else
                    Importe = 0;
                gasto.InsertarGastosPagarTransaccion(con, Transaccion, CodAuto, Nombre, Fecha, Importe, null, CodStock, CodCompra);
            }
        }

        private void txtEfectivo_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtEfectivo.Text = fun.FormatoEnteroMiles(txtEfectivo.Text);
            txtTotalEfectivo.Text = txtEfectivo.Text;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtPatente2.Text == "")
            {
                MessageBox.Show("Debe ingresar una patente para continuar ", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cVenta objVenta = new Clases.cVenta();
            string Patente = txtPatente2.Text;
            Clases.cStockAuto stock = new Clases.cStockAuto();
            DataTable trdo = stock.GetStockxPatente(Patente);
            int b = 0;
            Clases.cFunciones fun = new Clases.cFunciones();
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodStock"].ToString() != "")
                {
                    b = 1;
                    txtCodAuto2.Text = trdo.Rows[0]["CodAuto"].ToString();
                    txtDescripcion2.Text = trdo.Rows[0]["Descripcion"].ToString();
                    txtCodStock2.Text = trdo.Rows[0]["CodStock"].ToString();
                    double GastosTotalxAuto = objVenta.GetCostosTotalesxCodStock(Convert.ToInt32(txtCodStock2.Text));
                    //txtPatente2.Text = GastosTotalxAuto.ToString();
                    txtCostoxAuto.Text = fun.FormatoEnteroMiles(GastosTotalxAuto.ToString());
                }
            }
            if (b == 0)
            {
                MessageBox.Show("El auto no esta en el stock", Clases.cMensaje.Mensaje());
                txtDescripcion2.Text = "";
                txtCodStock2.Text = "";
                txtCostoxAuto.Text = "";
                txtImporteVehiculo2.Text = "";
            }
        }

        private void txtImporteVehiculo2_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtImporteVehiculo2.Text = fun.FormatoEnteroMiles(txtImporteVehiculo2.Text);
            txtTotalVehiculo.Text = txtImporteVehiculo2.Text;
        }

        private void BtnAgregarCheque_Click(object sender, EventArgs e)
        {
            if (txtCheque.Text == "")
            {
                MessageBox.Show("Debe ingresr un número de cheque", Clases.cMensaje.Mensaje());
                return;
            }

            if (txtImporteCheque.Text == "")
            {
                MessageBox.Show("Debe ingresr un importe de cheque", Clases.cMensaje.Mensaje());
                return;
            }

            if (CmbBanco.SelectedIndex < 1)
            {
                MessageBox.Show("Debe seleccionar un banco para continuar", Clases.cMensaje.Mensaje());
                return;
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            if (fun.ValidarFecha(txtFechaVencimiento.Text) == false)
            {
                MessageBox.Show("Debe ingresr una fecha de vencimiento para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            DataTable tbCheques = new DataTable();
            tbCheques.Columns.Add("NroCheque");
            tbCheques.Columns.Add("Importe");
            tbCheques.Columns.Add("FechaVencimiento");
            tbCheques.Columns.Add("CodBanco");
            tbCheques.Columns.Add("Banco");
            int i = 0;
            for (i = 0; i < GrillaCheques.Rows.Count - 1; i++)
            {
                string Cheque = GrillaCheques.Rows[i].Cells[0].Value.ToString();
                string Importe = GrillaCheques.Rows[i].Cells[1].Value.ToString();
                string FechaVencimiento = GrillaCheques.Rows[i].Cells[2].Value.ToString();
                string CodBanco = GrillaCheques.Rows[i].Cells[3].Value.ToString();
                string sBanco = GrillaCheques.Rows[i].Cells[4].Value.ToString();

                DataRow r = tbCheques.NewRow();
                r[0] = Cheque;
                r[1] = Importe;
                r[2] = FechaVencimiento;
                r[3] = CodBanco;
                r[4] = sBanco;
                tbCheques.Rows.Add(r);
            }
            Clases.cBanco objBanco = new Clases.cBanco();
            string banco = objBanco.GetBancoxCodigo(Convert.ToInt32(CmbBanco.SelectedValue));
            DataRow r1 = tbCheques.NewRow();
            r1[0] = txtCheque.Text;
            r1[1] = txtImporteCheque.Text;
            r1[2] = txtFechaVencimiento.Text;
            r1[3] = CmbBanco.SelectedValue;
            r1[4] = banco;
            tbCheques.Rows.Add(r1);
            GrillaCheques.DataSource = tbCheques;
            GrillaCheques.Columns[0].HeaderText = "Cheque";
            GrillaCheques.Columns[2].HeaderText = "Vencimiento";
            GrillaCheques.Columns[3].Visible = false;
            GrillaCheques.Columns[4].Width = 400;
            txtImporteCheque.Text = "";
            txtCheque.Text = "";
            txtFechaVencimiento.Text = "";
            double TotalCheques = 0;
            for (i = 0; i < tbCheques.Rows.Count; i++)
            {
                TotalCheques = TotalCheques + fun.ToDouble(tbCheques.Rows[i][1].ToString());
            }
            txtTotalCheque.Text = TotalCheques.ToString();
            //Clases.cFunciones fun = new Clases.cFunciones();
            txtTotalCheque.Text = fun.FormatoEnteroMiles(txtTotalCheque.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GrillaCheques.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una cheque para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            DataTable tbCheques = new DataTable();
            tbCheques.Columns.Add("NroCheque");
            tbCheques.Columns.Add("Importe");
            tbCheques.Columns.Add("FechaVencimiento");
            tbCheques.Columns.Add("CodBanco");
            tbCheques.Columns.Add("Banco");
            int i = 0;
            for (i = 0; i < GrillaCheques.Rows.Count - 1; i++)
            {
                string Cheque = GrillaCheques.Rows[i].Cells[0].Value.ToString();
                string Importe = GrillaCheques.Rows[i].Cells[1].Value.ToString();
                string FechaVencimiento = GrillaCheques.Rows[i].Cells[2].Value.ToString();
                string CodBanco = GrillaCheques.Rows[i].Cells[3].Value.ToString();
                string sBanco = GrillaCheques.Rows[i].Cells[4].Value.ToString();
                DataRow r = tbCheques.NewRow();
                r[0] = Cheque;
                r[1] = Importe;
                r[2] = FechaVencimiento;
                r[3] = CodBanco;
                tbCheques.Rows.Add(r);
            }

            string ChequeaBorrar = GrillaCheques.CurrentRow.Cells[0].Value.ToString();

            for (i = 0; i < tbCheques.Rows.Count; i++)
            {
                if (tbCheques.Rows[i]["NroCheque"].ToString() == ChequeaBorrar)
                {
                    tbCheques.Rows[i].Delete();
                    tbCheques.AcceptChanges();
                    i = tbCheques.Rows.Count;
                }
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            GrillaCheques.DataSource = tbCheques;
            double TotalCheques = 0;
            for (i = 0; i < tbCheques.Rows.Count; i++)
            {
                TotalCheques = TotalCheques + fun.ToDouble(tbCheques.Rows[i][1].ToString());
            }

            txtTotalCheque.Text = TotalCheques.ToString();
            //Clases.cFunciones fun = new Clases.cFunciones();
            txtTotalCheque.Text = fun.FormatoEnteroMiles(txtTotalCheque.Text);

        }

        private void GrabarCheques(SqlConnection con, SqlTransaction Transaccion, Int32 CodCompra)
        {
            Int32 CodAuto = Convert.ToInt32(txtCodAuto.Text);
            if (txtTotalCheque.Text != "")
            {
                if (txtTotalCheque.Text != "0")
                {
                    Clases.cFunciones fun = new Clases.cFunciones();
                    for (int j = 0; j < GrillaCheques.Rows.Count - 1; j++)
                    {
                        DateTime FechaVencimiento = Convert.ToDateTime(GrillaCheques.Rows[j].Cells[2].Value.ToString());
                        string sImporteCheque = GrillaCheques.Rows[j].Cells[1].Value.ToString();
                        string sqlCheque = "insert into ChequesPagar(NroCheque,Importe,Fecha,CodCliente,CodBanco,CodCompra,CodAuto,FechaVencimiento,Saldo)";
                        sqlCheque = sqlCheque + "values (";
                        sqlCheque = sqlCheque + "'" + GrillaCheques.Rows[j].Cells[0].Value.ToString() + "'";
                        sqlCheque = sqlCheque + "," + fun.ToDouble(sImporteCheque);
                        sqlCheque = sqlCheque + "," + "'" + txtFecha.Text + "'";
                        sqlCheque = sqlCheque + "," + txtCodCLiente.Text;
                        sqlCheque = sqlCheque + "," + GrillaCheques.Rows[j].Cells[3].Value.ToString();
                        sqlCheque = sqlCheque + "," + CodCompra.ToString();
                        sqlCheque = sqlCheque + "," + CodAuto.ToString();
                        sqlCheque = sqlCheque + "," + "'" + FechaVencimiento.ToShortDateString() + "'";
                        sqlCheque = sqlCheque + "," + fun.ToDouble(sImporteCheque);
                        sqlCheque = sqlCheque + ")";

                        SqlCommand ComandCheque = new SqlCommand();
                        ComandCheque.Connection = con;
                        ComandCheque.Transaction = Transaccion;
                        ComandCheque.CommandText = sqlCheque;
                        ComandCheque.ExecuteNonQuery();
                    }



                }
            }
        }

        private Int32 GrabarCompra(SqlConnection con, SqlTransaction Transaccion)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Int32 CodStokIngreso = Convert.ToInt32(txtCodStock.Text);
            double ImporteEfectivo = 0;
            if (txtEfectivo.Text != "")
                ImporteEfectivo = fun.ToDouble(txtEfectivo.Text);
            string sql = "Insert into Compra";
            sql = sql + "(CodStockEntrada";
            if (txtCodStock2.Text != "")
            {
                sql = sql + ",CodStockSalida";
                sql = sql + ",ImporteAutoPartePago";
            }
            sql = sql + ",ImporteEfectivo";
            sql = sql + ")";
            sql = sql + "Values(" + txtCodStock.Text;
            if (txtCodStock2.Text != "")
            {
                sql = sql + "," + txtCodStock2.Text;
                double Importe = fun.ToDouble(txtTotalVehiculo.Text);
                sql = sql + "," + Importe.ToString().Replace(",", ".");
            }
            sql = sql + "," + ImporteEfectivo.ToString().Replace(",", ".");
            sql = sql + ")";

            if (txtCodStock2.Text != "")
            {
                string sql2 = "Update StockAuto set ";
                sql2 = sql2 + " FechaBaja =" + "'" + txtFecha.Text + "'";
                sql2 = sql2 + " where CodStock =" + txtCodStock2.Text;
                SqlCommand Comand3 = new SqlCommand();
                Comand3.Connection = con;
                Comand3.Transaction = Transaccion;
                Comand3.CommandText = sql2;
                Comand3.ExecuteNonQuery();
            }

            SqlCommand Comand = new SqlCommand();
            Comand.Connection = con;
            Comand.Transaction = Transaccion;
            Comand.CommandText = sql;
            Comand.ExecuteNonQuery();

            sql = "select max(CodCompra) as CodCompra from Compra";
            SqlCommand Comand2 = new SqlCommand();
            Comand2.Connection = con;
            Comand2.Transaction = Transaccion;
            Comand2.CommandText = sql;
            Int32 CodCompra = Convert.ToInt32(Comand2.ExecuteScalar());
            return CodCompra;
        }

        private void GrabarMovimiento(SqlConnection con, SqlTransaction Transaccion, Int32 CodCompra)
        {
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            string Descripcion = "COMPRA DE AUTO " + txtPatente.Text;
            Clases.cFunciones fun = new Clases.cFunciones();
            Double Importe = 0;
            double ValorCompra = fun.ToDouble(txtTotal.Text);
            if (txtEfectivo.Text != "")
                Importe = fun.ToDouble(txtEfectivo.Text);
            Clases.cMovimiento mov = new Clases.cMovimiento();
            mov.RegistrarMovimientoDescripcionTransaccion(con, Transaccion, -1, Principal.CodUsuarioLogueado, (-1) * Importe, 0, 0, ValorCompra, 0, Fecha, Descripcion, CodCompra);

            if (txtTotalVehiculo.Text != "")
            {
                double TotalAuto = fun.ToDouble(txtCostoxAuto.Text);
                Descripcion = "SALIDA AUTO " + txtPatente2.Text;
                mov.RegistrarMovimientoDescripcionTransaccion(con, Transaccion, -1, Principal.CodUsuarioLogueado, 0, 0, 0, -1 * TotalAuto, 0, Fecha, Descripcion, CodCompra);
            }

        }

        private void txtPatente_Leave(object sender, EventArgs e)
        {
            txtPatente.Text = txtPatente.Text.ToUpper();
        }

        private void txtDescripcion_Leave(object sender, EventArgs e)
        {
            txtDescripcion.Text = txtDescripcion.Text.ToUpper();
        }

        private void GrabarMovimientoGastoRecepcion(SqlConnection con, SqlTransaction Transaccion, Int32 CodCompra)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtTotalGastosRecepcion.Text != "")
            {
                if (txtTotalGastosRecepcion.Text != "0")
                {
                    DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                    double Importe = fun.ToDouble(txtTotalGastosRecepcion.Text);
                    string Descripcion = "INGRESO GASTOS DE TRANSFERECIA " + txtPatente.Text;
                    Clases.cMovimiento mov = new Clases.cMovimiento();
                    mov.RegistrarMovimientoDescripcionTransaccion(con, Transaccion, -1, Principal.CodUsuarioLogueado, Importe, 0, 0, 0, 0, Fecha, Descripcion, CodCompra);
                }
            }

        }

        private void txtImporteCheque_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtImporteCheque.Text = fun.FormatoEnteroMiles(txtImporteCheque.Text);
        }

        private void txtImporteGastoRecepcion_Leave_1(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtImporteGastoRecepcion.Text = fun.FormatoEnteroMiles(txtImporteGastoRecepcion.Text);
        }

        private void txtEfectivoaPagar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtEfectivoaPagar_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            txtEfectivoaPagar.Text = fun.FormatoEnteroMiles(txtEfectivoaPagar.Text);
            txtTotalEfectivosaPagar.Text = txtEfectivoaPagar.Text;
        }

        private void bnAgregarGastosRecepcion_Click_1(object sender, EventArgs e)
        {
            Principal.CampoIdSecundario = "Codigo";
            Principal.CampoNombreSecundario = "Descripcion";
            Principal.NombreTablaSecundario = "CategoriaGastoRecepcion";
            FrmAltaBasica form = new FrmAltaBasica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private string GetSqlVenta()
        {
            string sql = "";   
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            Int32 CodAutoVendido = Convert.ToInt32(txtCodAuto2.Text);
            
            Int32 CodStock = Convert.ToInt32(txtCodStock2.Text);
            double ImporteVenta = 0;            
            Int32 CodCliente = 0;
            if (txtCodCLiente.Text != "")
                CodCliente = Convert.ToInt32(txtCodCLiente.Text);
  
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtImporteVehiculo2.Text != "")
                ImporteVenta = fun.ToDouble(txtImporteVehiculo2.Text);
            
            //Principal.CodUsuarioLogueado 
            sql = "insert into Venta(Fecha,CodUsuario,CodCliente";
            sql = sql + ",CodAutoVendido,ImporteVenta,CodStock,ImporteAutoPartePago)";
            sql = sql + "values(" + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + Principal.CodUsuarioLogueado.ToString();
            sql = sql + "," + CodCliente.ToString();
            sql = sql + "," + CodAutoVendido.ToString();
            sql = sql + "," + ImporteVenta.ToString().Replace(",", ".");
            sql = sql + "," + CodStock.ToString();
            sql = sql + "," + ImporteVenta.ToString().Replace(",", ".");
            sql = sql + ")";
            return sql;
        }

        private void GrabarVenta(SqlConnection con, SqlTransaction Transaccion)
        {
            if (txtTotalVehiculo.Text != "")
            {
                string sql = GetSqlVenta();
                SqlCommand comandVenta = new SqlCommand();
                comandVenta.Connection = con;
                comandVenta.Transaction = Transaccion;
                comandVenta.CommandText = sql;
                comandVenta.ExecuteNonQuery();
            }
        }
    }
}
