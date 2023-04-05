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
    public partial class Principal : Form
    {
        //nombre del campo id
        public static string CampoIdSecundario;
        //nombre del campo descripcion
        public static  string CampoNombreSecundario;
        //nombre de la tabla donde se realiza el grabado
        public static  string NombreTablaSecundario;
        public static string NombreLabelSecundario;
        //valor del id que genera al insertar
        public static string CampoIdSecundarioGenerado;
        public static Int32 CodUsuarioLogueado;
        public static string NombreUsuarioLogueado;
        public static string  CodigoPrincipalAbm;
        public static string CodigoSenia;
        private int childFormNumber = 0;
        public static string OpcionesdeBusqueda;
        public static string TablaPrincipal;
        public static string OpcionesColumnasGrilla;
        public static string ColumnasVisibles;
        public static string ColumnasAncho;
        public static string Comodin;

        public Principal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            FrmAbmCliente childForm = new FrmAbmCliente();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de altas, bajas y modificación de clientes";
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            //FrmAbmAuto childForm = new FrmAbmAuto();
            FrmStockAuto childForm =new FrmStockAuto();
            childForm.MdiParent = this;
            childForm.Text = "Listado de vehículos";
            childForm.Show();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            Principal.CodigoSenia = null;
            FrmVenta childForm = new FrmVenta();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de ventas";
            childForm.Show();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
                    }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void mnuCompraAuto_Click(object sender, EventArgs e)
        {
            FrmAutos childForm = new FrmAutos();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void mnuAgregarCosto_Click(object sender, EventArgs e)
        {
            FrmCosto childForm = new FrmCosto();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de Costos";
            childForm.Show();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            //CodUsuarioLogueado = "1";
            Clases.cAlarma alarma = new Clases.cAlarma();
            DataTable trdo = alarma.GetAlarmasxFecha(DateTime.Now);
            if (trdo.Rows.Count > 0)
            {
                FrmAlertas frm = new FrmAlertas();
                frm.ShowDialog();
            }
            VerificarPablo();
        }

        public void VerificarPablo()
        {
            if (Principal.NombreUsuarioLogueado == "PabloZ" || Principal.NombreUsuarioLogueado.ToUpper () == "SERGIO")
            {
                btnAnularVenta.Visible = true;
                BtnCopia.Visible = true;
            }
                
        }
        private void mnuInformeCuentas_Click(object sender, EventArgs e)
        {
            FrmInformeCuentas childForm = new FrmInformeCuentas();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de resumen de cuentas";
            childForm.Show();          
        }

        private void mnuCobroCuotas_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            FrmCobroCuotas childForm = new FrmCobroCuotas();
            childForm.MdiParent = this;
            childForm.Text = "Cobro de documentos";
            childForm.Show(); 
        }

        private void mnuInformeDeudas_Click(object sender, EventArgs e)
        {
            FrmInformeDeuda childForm = new FrmInformeDeuda();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show(); 
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            FrmVenta childForm = new FrmVenta();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de ventas";
            childForm.Show(); 
        }

        private void mnuMarcas_Click(object sender, EventArgs e)
        {
            FrmAbmMarca childForm = new FrmAbmMarca();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de Marcas";
            childForm.Show();
        }

        private void mnuCobroPrendas_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            FrmCobroPrenda childForm = new FrmCobroPrenda();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de cobro de prendas";
            childForm.Show();
        }

        private void mnucobroDeChequesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            FrmCobroCheque childForm = new FrmCobroCheque();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de cobro de cheques";
            childForm.Show();
        }

        private void cobroDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            FrmCobroDocumentos childForm = new FrmCobroDocumentos();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de cobro de cobranzas";
            childForm.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmListadoVentas childForm = new FrmListadoVentas();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de consulta de ventas";
            childForm.Show();
        }

        private void registrarEfectivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRegistrarEfectivo childForm = new FrmRegistrarEfectivo();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de registro de efectivo";
            childForm.Show();
        }

        private void ciompraDeAutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAutos childForm = new FrmAutos();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de compra de auto";
            childForm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FrmAutos childForm = new FrmAutos();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de compra de auto";
            childForm.Show();
        }

        

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmGastos childForm = new FrmGastos();
            childForm.Text = "Formulario de Gastos";
            childForm.Show();
        }

        private void borrarTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBorrarTablas childForm = new FrmBorrarTablas();
            childForm.Text = "Formulario de borrardo de talbas";
            childForm.Show();
        }

        private void prendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoPrenda childForm = new FrmListadoPrenda();
            childForm.Text = "Formulario de listado de prendas";
            childForm.Show();
        }

        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStockAuto childForm = new FrmStockAuto();
            childForm.Text = "Formulario de listado de stock";
            childForm.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FrmStockAuto childForm = new FrmStockAuto();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de listado de stock";
            childForm.Show();
        }

        private void cobranzasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoCobranza childForm = new FrmListadoCobranza();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de listado de Cobranzas";
            childForm.Show();
        }

        private void ventasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmListadoVentas childForm = new FrmListadoVentas();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de listado de Ventas";
            childForm.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FrmListadoMovimientos childForm = new FrmListadoMovimientos();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de Listado de Movimientos";
            childForm.Show();
        }

        private void chcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoCheques childForm = new FrmListadoCheques();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de Listado de Cheques";
            childForm.Show();
        }

        private void menuPrendas_Click(object sender, EventArgs e)
        {
            FrmListadoPrenda childForm = new FrmListadoPrenda();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de Listado de Prendas";
            childForm.Show();
        }

        private void MenuPrestamo_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            FrmRegistrarPrestamo childForm = new FrmRegistrarPrestamo();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de registro de Préstamo";
            childForm.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {   
            Principal.CodigoPrincipalAbm = null;
            FrmListadoPrestamo childForm = new FrmListadoPrestamo();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de listados de Préstamo";
            childForm.Show();
        }

        private void interesesPagadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            FrmListadoInteresesPagados childForm = new FrmListadoInteresesPagados();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de listados de Intereses pagados";
            childForm.Show();
        }

        private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            Principal.CodigoPrincipalAbm = null;
            FrmListadoComisiones childForm = new FrmListadoComisiones();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de listados de Comisiones";
            childForm.Show();
        }

        private void gastpsToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            Principal.CodigoPrincipalAbm = null;
            FrmListadoGastos childForm = new FrmListadoGastos();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de listados de Gastos";
            childForm.Show();
        }

        private void chequesAPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            Principal.CodigoPrincipalAbm = null;
            FrmListadoChequesaPagar childForm = new FrmListadoChequesaPagar();
            childForm.Text = "Formulario de listados de Cheques a Pagar";
            childForm.Show();
        }

        private void registrarDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            Principal.CodigoPrincipalAbm = null;
            FrmDocumentosAnteriores childForm = new FrmDocumentosAnteriores();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de documentos anteriores";
            childForm.Show();
        }

        private void cobroDeDocumentosAnterioresToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            Principal.CodigoPrincipalAbm = null;
            FrmCobroDocumentosAnteriores childForm = new FrmCobroDocumentosAnteriores();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de cobro de documentos anteriores";
            childForm.Show();
        }

        private void documentosAnteriroesToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            Principal.CodigoPrincipalAbm = null;
            FrmListadoDocumentosAnteriores childForm = new FrmListadoDocumentosAnteriores();
            childForm.Text = "Formulario de Listado de documentos anteriores";
            childForm.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registrarGastosGeneralesToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            Principal.CodigoPrincipalAbm = null;
            FrmGastosGenerales childForm = new FrmGastosGenerales();
            childForm.MdiParent = this;
            childForm.Text = "Formulario gastos generales";
            childForm.Show();
        }

        private void gastosGeneralesToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            Principal.CodigoPrincipalAbm = null;
            FrmListadoGastosGenerales childForm = new FrmListadoGastosGenerales();
            childForm.MdiParent = this;
            childForm.Text = "Formulario gastos generales";
            childForm.Show();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {  
            Principal.Comodin = "";
            Principal.CodigoPrincipalAbm = null;
            FrmListadoAlertas form = new FrmListadoAlertas();
            form.Show();
        }

        private void alertasToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            Principal.CodigoPrincipalAbm = null;
            FrmListadoAlertas childForm = new FrmListadoAlertas();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de listado de alertas";
            childForm.Show();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            FrmAbmBarrio childForm = new FrmAbmBarrio();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de barrio";
            childForm.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            FrmAbmCiudad childForm = new FrmAbmCiudad();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de Ciudad";
            childForm.Show();
        }

        private void registrarCobranzasGeneralesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRegistrarCobranzaGeneral childForm = new FrmRegistrarCobranzaGeneral();
            childForm.MdiParent = this; 
            childForm.Text = "Formulario de Cobranzas Generales";
            childForm.Show();


        }

        private void cobranzasGeneralesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoCobranzasGenerales childForm = new FrmListadoCobranzasGenerales();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de Listado de Cobranzas Generales";
            childForm.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            FrmAbmBanco childForm = new FrmAbmBanco();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de Bancos";
            childForm.Show();
        }

        private void efectivosAPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoEfectivosaPagar childForm = new FrmListadoEfectivosaPagar();
            childForm.MdiParent = this;
            childForm.Text = "Listado de efectivos a pagar";
            childForm.Show();
            
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            FrmRegistrarAnotacion childForm = new FrmRegistrarAnotacion();
            //childForm.Text = "Listado de efectivos a pagar";
            childForm.Show();
        }

        private void anotacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoAnotaciones childForm = new FrmListadoAnotaciones();
            childForm.MdiParent = this;
            //childForm.Text = "Listado de anotaciones";
            childForm.Show();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            FrmInformeDeuda childForm = new FrmInformeDeuda();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de Listado de documentos";
            childForm.Show();
        }

        private void rentabilidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmResumenGanancia childForm = new FrmResumenGanancia();
            childForm.MdiParent = this;
            //childForm.Text = "Formulario de Listado de documentos";
            childForm.Show();
        }

        private void btnAnularVenta_Click(object sender, EventArgs e)
        {
            FrmAnularVenta frm = new FrmAnularVenta();
            frm.ShowDialog();
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            FrmListadoMovimientos frm = new FrmListadoMovimientos();
            frm.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmListadoVentas frm = new FrmListadoVentas();
            frm.ShowDialog();
        }

        private void BtnCopia_Click(object sender, EventArgs e)
        {
            FrmCopia fm = new FrmCopia();
            fm.Show();
        }

        private void preVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoPreVenta frm = new FrmListadoPreVenta();
            frm.MdiParent = this;
            frm.Show();
        }

        private void categoriaDeGastosDeRecepciónToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmAbmCategoriaGastoRecepcion frm = new FrmAbmCategoriaGastoRecepcion();
            frm.MdiParent = this;
            frm.Show();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            FrmCopia frm = new FrmCopia();
            frm.MdiParent = this;
            frm.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbmAuto frm = new FrmAbmAuto();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuControlOperaciones_Click(object sender, EventArgs e)
        {
            FrmControl frm = new FrmControl();
            frm.MdiParent = this;
            frm.Show();
        }

        private void crearAlertaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.Comodin = "";
            Principal.CodigoPrincipalAbm = null;
            FrmRegistrarAlarma form = new FrmRegistrarAlarma();
            form.Show();
        }

        private void btnConsultaAgenda_Click(object sender, EventArgs e)
        {
            FrmConsultaAgenda frm = new FrmConsultaAgenda();
            frm.Show();
        }

        private void controlDeOperacionesGeneralesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmControlGeneralOperaciones frm = new FrmControlGeneralOperaciones();
            frm.Show();
        }

        private void tarjetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbmTarjeta frm = new FrmAbmTarjeta();
            frm.Show();
        }

        private void ventasPorTarjetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoTarjeta frm = new FrmListadoTarjeta();
            frm.Show();
        }

        private void ingresoDeChequeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIngresoCheque frm = new FrmIngresoCheque();
            frm.Show();
        }

        private void registrarPrestamoACobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPrestamoCobrar frm = new FrmPrestamoCobrar();
            frm.Show();
        }

        private void prestamoACobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoPrestamoCobrar frm = new FrmListadoPrestamoCobrar();
            frm.Show();
        }
    }
}
