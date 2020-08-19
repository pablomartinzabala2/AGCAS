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
    public partial class FrmResumenGanancia : Form
    {
        public FrmResumenGanancia()
        {
            InitializeComponent();
        }

        private void FrmResumenGanancia_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            DateTime fecha1 = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha1.ToShortDateString();
            txtFechaHasta.Text = fecha.ToShortDateString();
        }

        private void GetGanancia()
        {

            Clases.cFunciones fun = new Clases.cFunciones();
            Clases.cVenta objVenta = new Clases.cVenta();
            Clases.cPunitorioCobranza objPunitorioCobranza= new Clases.cPunitorioCobranza();
            Clases.cPunitorioCuota objPunitorioCuota = new Clases.cPunitorioCuota();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            DataTable trdo = objVenta.GetVentasxFecha(FechaDesde, FechaHasta, "",null);
            Clases.cPreVenta objPreVenta = new Clases.cPreVenta();
            DataTable trdo2 = objPreVenta.GetPreVentasxFecha(FechaDesde, FechaHasta, "", null);
            //le agre[g
            string Dato = "";
            Int32 PosPintar = 0;
            PosPintar = trdo.Rows.Count;
            for (int i = 0; i < trdo2.Rows.Count; i++)
            {
                DataRow fila;
                fila = trdo.NewRow();
                for (int j = 0; j < trdo2.Columns.Count; j++)
                {
                    Dato = trdo2.Rows[i][j].ToString();
                    fila[j] = Dato;
                }
                trdo.Rows.Add(fila);
            }
             Clases.cPunitorioCuotasAnteriores objPunDocAnt = new Clases.cPunitorioCuotasAnteriores();
            Int32 Cant = trdo.Rows.Count;
            txtCantidad.Text = Cant.ToString();
            trdo = fun.TablaaMiles(trdo, "ImporteVenta");
            trdo = fun.TablaaMiles(trdo, "ImporteEfectivo");
            trdo = fun.TablaaMiles(trdo, "ImporteAutoPartePago");
            trdo = fun.TablaaMiles(trdo, "ImporteCredito");
            trdo = fun.TablaaMiles(trdo, "ImportePrenda");
            trdo = fun.TablaaMiles(trdo, "Cheque");
            trdo = fun.TablaaMiles(trdo, "ImporteCobranza");
            trdo = fun.TablaaMiles(trdo, "Ganancia");
            double Utilidad = fun.TotalizarColumna(trdo, "Ganancia");
            Double ImportePunitorioCobranza = objPunitorioCobranza.GetImportexFecha(FechaDesde, FechaHasta);
            Double ImportePunitorioCuota = objPunitorioCuota.GetImportexFecha(FechaDesde, FechaHasta);
            Double ImportePunitorioCuotasAnterioes = objPunDocAnt.GetImportexFecha(FechaDesde, FechaHasta);
            Double TotalPunitorio = ImportePunitorioCobranza + ImportePunitorioCuota + ImportePunitorioCuotasAnterioes;
            
            txtUtilidad.Text = fun.TotalizarColumna(trdo, "Ganancia").ToString();
            txtUtilidad.Text = fun.FormatoEnteroMiles(txtUtilidad.Text);

            Clases.cGastosNegocio gastos = new Clases.cGastosNegocio();
            double TotalGastos = gastos.GetGastosNegocioxFecha(FechaDesde, FechaHasta);
            txtGastos.Text = fun.SepararDecimales(TotalGastos.ToString ());
            txtGastos.Text = fun.FormatoEnteroMiles(txtGastos.Text);
             
            Clases.cPagoIntereses objpago = new Clases.cPagoIntereses();
            double TotalPago = objpago.GetResumenPagosInteresesxFecha(FechaDesde, FechaHasta);
            txtInteresesPagados.Text = fun.SepararDecimales(TotalPago.ToString());
            txtInteresesPagados.Text = fun.FormatoEnteroMiles(txtInteresesPagados.Text);
            double InteresesGanados = 0;
            Clases.cCuotasAnteriores cuotasAnt = new Clases.cCuotasAnteriores();
            Clases.cCuota cuota = new Clases.cCuota();
            InteresesGanados = cuota.GetGanaciaCobroCuotas(FechaDesde, FechaHasta);
            InteresesGanados = InteresesGanados + cuotasAnt.GetGanaciaCobroCuotas(FechaDesde, FechaHasta);
            txtInteresesGanados.Text = InteresesGanados.ToString();
            txtInteresesGanados.Text = fun.FormatoEnteroMiles(txtInteresesGanados.Text);
            double Ganancia = Utilidad + TotalPunitorio  - TotalPago - TotalGastos + InteresesGanados;
            txtResultado.Text = fun.SepararDecimales(Ganancia.ToString());
            txtResultado.Text = fun.FormatoEnteroMiles(txtResultado.Text);
            txtPunitorio.Text = TotalPunitorio.ToString();
            txtPunitorio.Text = fun.FormatoEnteroMiles(txtPunitorio.Text); 
        }

        private void btnBuscar_Click(object sender, EventArgs e)
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
            GetGanancia();
        }
    }
}
