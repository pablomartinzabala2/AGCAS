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
    public partial class FrmVistaPrevia : Form
    {
        public FrmVistaPrevia()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmVistaPrevia_Load(object sender, EventArgs e)
        {
            if (Principal.CodigoPrincipalAbm != null)
            {
                CargarDatos(Convert.ToInt32(Principal.CodigoPrincipalAbm));
                BuscarGastosTransferencia(Convert.ToInt32(Principal.CodigoPrincipalAbm));
            }
        }

        private void CargarDatos(Int32 CodVenta)
        {
            GetAutoPartePago(CodVenta);
            Clases.cCliente cliente = new Clases.cCliente();
            Clases.cVenta venta = new Clases.cVenta();
            DataTable trdo = venta.GetVentaxCodigo(CodVenta);
            
            if (trdo.Rows.Count > 0)
            {
                Int32 CodCliente = Convert.ToInt32(trdo.Rows[0]["CodCliente"].ToString());
                DataTable tcli = cliente.GetClientesxCodigo(CodCliente);
                if (tcli.Rows.Count > 0)
                {
                    string nombre = tcli.Rows[0]["Nombre"].ToString();
                    nombre = nombre + " " + tcli.Rows[0]["Apellido"].ToString();
                    string Direccion = tcli.Rows[0]["Calle"].ToString();
                    Direccion = Direccion + " " + tcli.Rows[0]["Numero"].ToString();
                    txtDireccion.Text = Direccion;
                    txtTelefono.Text = tcli.Rows[0]["Telefono"].ToString();
                    txtNombre.Text = nombre;
                }
            }
            txtEfectivo.Text = trdo.Rows[0]["ImporteEfectivo"].ToString();
            txtDocumentos.Text = trdo.Rows[0]["ImporteCredito"].ToString(); 
            Int32 CodAuto = Convert.ToInt32(trdo.Rows[0]["CodAutoVendido"].ToString());
            Clases.cAuto auto = new Clases.cAuto();
            DataTable tauto = auto.GetAutoxCodigo(CodAuto);
            {
                if (tauto.Rows.Count > 0)
                {
                    string Descrip = tauto.Rows[0]["Descripcion"].ToString();
                    Descrip = Descrip + " AÑO " + tauto.Rows[0]["Anio"].ToString();
                    Descrip = Descrip + " DOMINIO " + tauto.Rows[0]["Patente"].ToString();
                    Descrip = Descrip + " MOTOR N º" + tauto.Rows[0]["Motor"].ToString();
                    Descrip = Descrip + " CHASIS N º" + tauto.Rows[0]["Chasis"].ToString();
                    txtAuto.Text = Descrip; 
                }
            }
            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtEfectivo.Text != "0" && txtEfectivo.Text != "")
            {
                txtEfectivo.Text = fun.SepararDecimales(txtEfectivo.Text);
                txtEfectivo.Text = fun.FormatoEnteroMiles(txtEfectivo.Text);
            }

            if (txtDocumentos.Text != "0" && txtDocumentos.Text != "")
            {
                txtDocumentos.Text = fun.SepararDecimales(txtDocumentos.Text);
                txtDocumentos.Text = fun.FormatoEnteroMiles(txtDocumentos.Text);
            }
            Clases.cPrenda prenda = new Clases.cPrenda();
            DataTable trdoPrenda = prenda.GetPrendaxCodVenta(Convert.ToInt32(Principal.CodigoPrincipalAbm));
            if (trdoPrenda.Rows.Count > 0)
            {
                string Importe = trdoPrenda.Rows[0]["Importe"].ToString();
                Importe = fun.SepararDecimales(Importe);
                Importe = fun.FormatoEnteroMiles(Importe);
                txtImportePrenda.Text = Importe;
            }
        }

        private void GrabarDatos()
        {
            string NombreCliente = txtNombre.Text;
            string DniCliente = txtDni.Text;
            string DireccionCliente = txtDireccion.Text;
            string Texto1 = "Entre la firma CASTAÑO AUTOMOTORES con domicilio";
            Texto1 = Texto1 + " en Av Colon 4747 de la ciudad de Córdoba";
            Texto1 = Texto1 + " adelante VENDEDOR Y La Sr/a NombreCliente";
            Texto1 = Texto1 + " DNI DniCliente Con domicilio en CALLE DireccionCliente TELEFONO TelefonoCliente";
            Texto1 = Texto1 + " en adelante COMPRADOR convienen en celebrar el siguiente boleto";
            Texto1 = Texto1 + " de compra venta, sujeto a las siguientes Cláusulas ";
            Texto1 = Texto1.Replace("NombreCliente", NombreCliente);
            Texto1 = Texto1.Replace("DniCliente", DniCliente);
            Texto1 = Texto1.Replace("TelefonoCliente", txtTelefono.Text);
            Texto1 = Texto1.Replace("DireccionCliente", DireccionCliente);

            string texto2 = "Un vehículo NombreAuto";
            texto2 = texto2.Replace("NombreAuto", txtAuto.Text);

            string texto3 = "El precio se establece en PESOS " + txtPrecio.Text;
            string Texto4 = "mas transfencia, por un valor de $" + txtGastosTransferencia.Text;
            if (txtTarjetaAzul.Text != "")
                Texto4 = Texto4 + " con " + txtTarjetaAzul.Text + " cedulas azules";

            string texto5 = GetFormasPago();
    
            string sql = "delete from reporte";
            Clases.cDb.ExecutarNonQuery(sql);
            sql = "Insert into reporte(Parte1,Parte2,Parte3,Parte4,Parte6) values (" + "'" + Texto1 + "'";
            sql = sql + "," + "'" + texto2 +"'";
            sql = sql + "," + "'" + texto3 + "'";
            sql = sql + "," + "'" + Texto4 + "'";
            sql = sql + "," + "'" + texto5 + "'";
            sql = sql + ")";
            Clases.cDb.ExecutarNonQuery(sql);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GrabarDatos();

            FrmReporte form = new FrmReporte();
            form.Show();
        }

        private void BuscarGastosTransferencia(Int32 CodVenta)
        {
            Clases.cGastoTransferencia gasto = new Clases.cGastoTransferencia();
            DataTable trdo = gasto.GetGastoTransferenciaxCodVenta(CodVenta);
            if (trdo.Rows.Count > 0)
            {
                for (int i = 0; i < trdo.Rows.Count; i++)
                {
                    string Codigo = trdo.Rows[i]["CodGastoTranasferencia"].ToString();
                    string Descripcion = trdo.Rows[i]["Descripcion"].ToString();
                    string Importe = trdo.Rows[i]["Importe"].ToString();
                    AgregarGasto(Codigo, Descripcion, Importe, "Transferencia");
                }
            }
            //AgregarGasto(CmbGastosTransferencia.SelectedValue.ToString(), Descripcion, txtImporteGastoTransferencia.Text, "Transferencia");
        }

        private void AgregarGasto(string Codigo, string Descripcion, string Importe, string Tipo)
        {
            for (int i = 0; i < GrillaGastos.Rows.Count - 1; i++)
            {
                if (GrillaGastos.Rows[i].Cells[0].Value.ToString() == Codigo.ToString() && GrillaGastos.Rows[i].Cells[2].Value.ToString() == Tipo)
                {
                    MessageBox.Show("Ya se ha ingresado el gasto", Clases.cMensaje.Mensaje());
                    return;
                }
            }
            DataTable tListado = new DataTable();
            tListado.Columns.Add("Codigo");
            tListado.Columns.Add("Descripcion");
            tListado.Columns.Add("Tipo");
            tListado.Columns.Add("Importe");
            for (int i = 0; i < GrillaGastos.Rows.Count - 1; i++)
            {
                string sCodigo = GrillaGastos.Rows[i].Cells[0].Value.ToString();
                string sDescripcion = GrillaGastos.Rows[i].Cells[1].Value.ToString();
                string sTipo = GrillaGastos.Rows[i].Cells[2].Value.ToString();
                string sImporte = GrillaGastos.Rows[i].Cells[3].Value.ToString();
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
            GrillaGastos.DataSource = tListado;
            Clases.cFunciones fun = new Clases.cFunciones();
          // txtTotalGasto.Text = fun.CalcularTotalGrilla(GrillaGastos, "Importe").ToString();
         //   if (txtTotalGasto.Text != "")
        //    {

        //        txtTotalGasto.Text = fun.FormatoEnteroMiles(txtTotalGasto.Text);
        //    }
            //GrillaGastos.Columns[0].Visible = false;
            //GrillaGastos.Columns[2].Visible = false;
            //txtImporteGastoTransferencia.Text = "";
            //txtImporteGastoRecepcion.Text = "";
            //GrillaGastos.Columns[1].Width = 250;

            //txtTotalGastosRecepcion.Text = fun.CalcularTotalGrilla(GrillaGastosRecepcion, "Importe").ToString();
            //if (txtTotalGastosRecepcion.Text != "")
            //{
            //    txtTotalGastosRecepcion.Text = fun.FormatoEnteroMiles(txtTotalGastosRecepcion.Text);
            //}

            //double TotalVenta = 0;
            //double PrecioVenta = 0;
            //double TotalGastos = 0;
            //double TotalGastosRecepcion = 0;

            //if (txtTotalVenta.Text != "")
            //{
            //    PrecioVenta = fun.ToDouble(txtPrecioVenta.Text);
            //}

            //if (txtTotalGasto.Text != "")
            //{
            //    TotalGastos = fun.ToDouble(txtTotalGasto.Text);
            //}

            //if (txtTotalGastosRecepcion.Text != "")
            //{
            //    TotalGastosRecepcion = fun.ToDouble(txtTotalGastosRecepcion.Text);
            //}

            //TotalVenta = PrecioVenta + TotalGastos + TotalGastosRecepcion;
            //txtTotalVenta.Text = TotalVenta.ToString();
            //txtTotalVenta.Text = fun.FormatoEnteroMiles(txtTotalVenta.Text);
            ////CalcularSubTotal(); 
        }

        private string GetFormasPago()
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            string texto = "";
            int b = 0;
            if (txtSenia.Text != "")
            {
                texto = "Seña adelanto " + txtSenia.Text;
                b = 1;
            }
            if (txtEfectivo.Text != "")
            {
                if (b == 0)
                    texto = " efectivo en este acto " + txtEfectivo.Text;
                else
                    texto = texto + ",efectivo en este acto " + txtEfectivo.Text;
                b = 1;
            }
            if (txtDocumentos.Text != "")
            {
                if (b == 0)
                    texto = " Documento de " + txtDocumentos.Text;
                else
                    texto = texto + ", Documento de " + txtDocumentos.Text;
            }

            if (txtAutoPartePago.Text != "")
                texto = texto + txtAutoPartePago.Text;

            // busco si hubo prenda
            Clases.cPrenda prenda = new Clases.cPrenda();
            DataTable trdo = prenda.GetPrendaxCodVenta(Convert.ToInt32(Principal.CodigoPrincipalAbm));
            if (trdo.Rows.Count > 0)
            {
                string Importe = trdo.Rows[0]["Importe"].ToString();
                Importe = fun.SepararDecimales(Importe);
                Importe = fun.FormatoEnteroMiles(Importe);
                string Descripcion = trdo.Rows[0]["Descripcion"].ToString();
                texto = texto + ",crédito prendario a cargo de " + Descripcion;
                texto = texto + ", por un valor de " + Importe;
            }

            texto = texto + ",sobre el cual se aplicaran los siguientes descuentos:";	
			if (txtRentas.Text !="")
				texto = texto +", rentas :" + txtRentas.Text ;
			 
			if (txtMunicipalidad.Text !="")
				texto = texto +", municipalidad :" + txtMunicipalidad.Text ;
			 
			if (txtMultas.Text !="")
				texto = texto +", multas :" + txtMultas.Text ;	

            if (txtMultas.Text !="")
				texto = texto +", rentas :" + txtMultas.Text ;
	 
             if (txtVerificacion.Text !="")  
				texto = texto +", verificación :" + txtVerificacion.Text ;
             
             if (txtFirmasyForm.Text !="")  
				texto = texto +", firmas y form. :" + txtFirmasyForm.Text ;
             
             if (txtCancelacionPrenda.Text !="")  
				texto = texto +", cancelac prenda. :" + txtCancelacionPrenda.Text ;
             
            if (txtOtros.Text !="")  
				texto = texto +", cancelac prenda. :" + txtOtros.Text ;
																					

            return texto;
        }

        public void GetAutoPartePago(Int32 CodVenta)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cVenta obj = new Clases.cVenta();
            DataTable trdo = obj.GetAutosPartePago(CodVenta);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodAuto"].ToString()!="")
                {
                    string sImporte = trdo.Rows[0]["Importe"].ToString();
                    if (sImporte != "" && sImporte !="0")
                    {
                        sImporte = fun.SepararDecimales(sImporte);
                        sImporte = fun.FormatoEnteroMiles(sImporte);
                    }
                    Int32 CodAuto = Convert.ToInt32(trdo.Rows[0]["CodAuto"].ToString());
                    Clases.cAuto auto = new Clases.cAuto();
                    DataTable tauto = auto.GetAutoxCodigo(CodAuto);
                    {
                        if (tauto.Rows.Count > 0)
                        {
                            string Descrip = " Un vehículo" + tauto.Rows[0]["Marca"].ToString() + " " + tauto.Rows[0]["Descripcion"].ToString();
                            Descrip = Descrip + " MOTOR N º" + tauto.Rows[0]["Motor"].ToString();
                            Descrip = Descrip + " CHASIS N º" + tauto.Rows[0]["Chasis"].ToString();
                            Descrip = Descrip + " AÑO " + tauto.Rows[0]["Anio"].ToString();
                            Descrip = Descrip + " DOMINIO " + tauto.Rows[0]["Patente"].ToString();
                            Descrip = Descrip + " valuado en " + sImporte;
                            txtAutoPartePago.Text = Descrip;

                        }
                    }
                }
            }
        }

        private void txtVerificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtFirmasyForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtRentas_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtMunicipalidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtCancelacionPrenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtOtros_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtMultas_KeyPress(object sender, KeyPressEventArgs e)
        {
             Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtVerificacion_Leave(object sender, EventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones ();
            fun.FormatoEnteroMiles (txtVerificacion.Text);
        }

        private void txtFirmasyForm_Leave(object sender, EventArgs e)
        {  
            Clases.cFunciones fun = new Clases.cFunciones ();
            fun.FormatoEnteroMiles (txtFirmasyForm.Text);
        }

        private void txtRentas_Leave(object sender, EventArgs e)
        {   
            Clases.cFunciones fun = new Clases.cFunciones ();
            fun.FormatoEnteroMiles (txtRentas.Text);
        }

        private void txtMunicipalidad_Leave(object sender, EventArgs e)
        {   
            Clases.cFunciones fun = new Clases.cFunciones ();
            fun.FormatoEnteroMiles (txtMunicipalidad.Text);
        }

        private void txtCancelacionPrenda_Leave(object sender, EventArgs e)
        {   
            Clases.cFunciones fun = new Clases.cFunciones ();
            fun.FormatoEnteroMiles (txtCancelacionPrenda.Text);
        }

        private void txtMultas_Leave(object sender, EventArgs e)
        {     
            Clases.cFunciones fun = new Clases.cFunciones ();
            fun.FormatoEnteroMiles (txtMultas.Text);
        }

        private void txtOtros_Leave(object sender, EventArgs e)
        {  
            Clases.cFunciones fun = new Clases.cFunciones ();
            fun.FormatoEnteroMiles (txtOtros.Text);
        }
    }
}
