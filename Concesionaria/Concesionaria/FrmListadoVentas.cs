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
    public partial class FrmListadoVentas : Form
    {
        public FrmListadoVentas()
        {
            InitializeComponent();
            DateTime fecha = DateTime.Now;
            DateTime fecha1 = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha1.ToShortDateString();
            txtFechaHasta.Text = fecha.ToShortDateString();
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
            string Apellido = null;
            if (txtApellido.Text !="")
                Apellido = txtApellido.Text ;
            Clases.cVenta objVenta = new Clases.cVenta();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            DataTable trdo = objVenta.GetVentasxFecha(FechaDesde, FechaHasta, txtPatente.Text.Trim (),Apellido);
            Clases.cPreVenta objPreVenta = new Clases.cPreVenta();

            DataTable trdo2 = objPreVenta.GetPreVentasxFecha(FechaDesde, FechaHasta, txtPatente.Text.Trim(), Apellido);
            //le agre[g
            string Dato = "";
            Int32 PosPintar = 0;
            PosPintar = trdo.Rows.Count;
            for (int i = 0; i < trdo2.Rows.Count; i++)
            {
                DataRow fila;
                fila = trdo.NewRow();
                for (int j=0;j< trdo2.Columns.Count ;j++)
                {
                    Dato = trdo2.Rows[i][j].ToString();
                    fila[j] = Dato; 
                }
                trdo.Rows.Add(fila);
            }

            

            Int32 Cant = trdo.Rows.Count ;
            txtCantidadVentas.Text = Cant.ToString();
            trdo = fun.TablaaMiles(trdo, "ImporteVenta");
            trdo = fun.TablaaMiles(trdo, "ImporteEfectivo");
            trdo = fun.TablaaMiles(trdo, "ImporteAutoPartePago");
            trdo = fun.TablaaMiles(trdo, "ImporteCredito");
            trdo = fun.TablaaMiles(trdo, "ImportePrenda");
            trdo = fun.TablaaMiles(trdo, "Cheque");
            trdo = fun.TablaaMiles(trdo, "ImporteCobranza");
            trdo = fun.TablaaMiles(trdo, "Ganancia");
            txtTotal.Text = fun.TotalizarColumna(trdo, "Ganancia").ToString();
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            Grilla.DataSource = trdo;
            //
            Clases.cVenta ven = new Clases.cVenta();
            //pinto las ventas sin saldo
            for (int k = 0; k < Grilla.Rows.Count - 1; k++)
            {
                Int32 CodVenta = Convert.ToInt32(Grilla.Rows[k].Cells[0].Value.ToString());
                if (k < PosPintar)
                {
                    double ImporteDocumento = 0;
                    double Prenda = 0;
                    double Cheque = 0;
                    double Cobranza = 0;
                    if (Grilla.Rows[k].Cells[10].Value.ToString() != "")
                    {
                        ImporteDocumento = GetSaldoCuotaxCodVenta(CodVenta);
                    }
                    
                    if (Grilla.Rows[k].Cells[11].Value.ToString() != "")
                    {
                        Prenda = GetSaldoPrendaxCodVenta(CodVenta);
                    }

                    if (Grilla.Rows[k].Cells[12].Value.ToString() != "")
                    {
                        Cheque = GetSaldoChequexCodVenta(CodVenta);
                    }

                    if (Grilla.Rows[k].Cells[13].Value.ToString() != "")
                    {
                        Cobranza = GetSaldoCobranza(CodVenta);
                    }
                    double Suma = (ImporteDocumento + Prenda + Cheque + Cobranza);
                    if (Suma == 0)
                    {
                        //pinto
                        Grilla.Rows[k].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    int TieneDeuda = ven.TieneDeuda(CodVenta);
                    if (TieneDeuda == 1)
                        Grilla.Rows[k].DefaultCellStyle.BackColor = Color.LightCyan;


                    
                }
            }
            //
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[2].HeaderText = "Descripción";
            Grilla.Columns[7].HeaderText = "Total";
            Grilla.Columns[8].HeaderText = "Efectivo";
            Grilla.Columns[9].HeaderText = "Vehículo";
            Grilla.Columns[10].HeaderText = "Documentos";
            Grilla.Columns[11].HeaderText = "Prenda";
            Grilla.Columns[13].HeaderText = "Cobranza";
            Grilla.Columns[15].Visible = false;
            
            Grilla.Columns[1].Width = 105;
            Grilla.Columns[5].Visible = false;
            Grilla.Columns[3].HeaderText = "Parte Pago";
            Grilla.Columns[7].Width = 80;
            Grilla.Columns[8].Width = 80;
            Grilla.Columns[9].Width = 80;
            Grilla.Columns[10].Width = 80;
            Grilla.Columns[11].Width = 80;
            Grilla.Columns[12].Width = 80;
            Grilla.Columns[13].Width = 80;
            for (int k = 0; k < Grilla.Rows.Count - 1; k++)
            {
                if (k>=PosPintar)
                    Grilla.Rows[k].DefaultCellStyle.BackColor = Color.LightGray;
            }

           
                    
        }

        private void btnAbrirVenta_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje());
                return;
            }

            if (Grilla.CurrentRow.DefaultCellStyle.BackColor == Color.LightGray)
            {
                string CodPreVenta = Grilla.CurrentRow.Cells[0].Value.ToString();
                Principal.CodigoPrincipalAbm = null;
                Principal.CodigoSenia = CodPreVenta;
                FrmVenta form = new FrmVenta();
                form.ShowDialog();
            }
            else
            {
                string CodVenta = Grilla.CurrentRow.Cells[0].Value.ToString();
                Principal.CodigoPrincipalAbm = CodVenta;
                Principal.CodigoSenia = null;
                FrmVenta form = new FrmVenta();
                form.ShowDialog();
            }
            

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {  
            if (Grilla.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un registro", Clases.cMensaje.Mensaje());
                return;
            }
            string CodVenta = Grilla.CurrentRow.Cells[0].Value.ToString();
            Principal.CodigoPrincipalAbm = CodVenta;
            FrmVistaPrevia form = new FrmVistaPrevia();
            form.Show();
        }

        private void BtnVerGanancia_Click(object sender, EventArgs e)
        {
            if (Grilla.Columns[14].Visible == false)
            {
                Grilla.Columns[14].Visible = true;
                Grilla.Columns[2].Width = 100;
                lblGanancia.Visible = true;
                txtTotal.Visible = true; 
            }
            else
            {
                Grilla.Columns[14].Visible = false;
                Grilla.Columns[2].Width = 200;
                lblGanancia.Visible = false;
                txtTotal.Visible = false;
            }
             
        }

        private void FrmListadoVentas_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnReporte2_Click(object sender, EventArgs e)
        {
            Clases.cDb.ExecutarNonQuery("delete from ReporteAuto");
            string sql = "";
            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                string Modelo = (Grilla.Rows[i].Cells[1].Value.ToString());
                string Descripcion = (Grilla.Rows[i].Cells[2].Value.ToString());
                Int32 CodCliente = Convert.ToInt32(Grilla.Rows[i].Cells[15].Value.ToString());
                string Cliente = GetCliente(CodCliente, "NOMBRE");
                string Telefono = GetCliente(CodCliente, "TELEFONO");
                string Celular = GetCliente(CodCliente, "CELULAR");
                string Fecha = (Grilla.Rows[i].Cells[6].Value.ToString());
                if (Fecha.Length > 11)
                    Fecha = Fecha.Substring(0, 10);
                sql = "insert into ReporteAuto(Marca,Descripcion,Extra1,Extra2,Extra3,Extra4)";
                sql = sql + "values (" + "'" + Modelo + "'";
                sql = sql + "," + "'" + Descripcion + "'";
                sql = sql + "," + "'" + Cliente + "'";
                sql = sql + "," + "'" + Telefono + "'";
                sql = sql + "," + "'" + Celular + "'";
                sql = sql + "," + "'" + Fecha + "'";
                sql = sql + ")";
                Clases.cDb.ExecutarNonQuery(sql);
            }
            FrmReporteVenta frm = new FrmReporteVenta();
            frm.Show();
        }

        private string GetCliente(Int32 CodCLiente, string Campo)
        {
            string Dato = "";
            Clases.cCliente cliente = new Clases.cCliente();
            DataTable trdo = cliente.GetClientesxCodigo(CodCLiente);
            if (trdo.Rows.Count > 0)
            {
                if (Campo == "NOMBRE")
                {
                    Dato = trdo.Rows[0]["Nombre"].ToString();
                    Dato = Dato + " " + trdo.Rows[0]["Apellido"].ToString();
                }
                if (Campo == "TELEFONO")
                {
                   Dato = trdo.Rows[0]["Telefono"].ToString();                
                }

                if (Campo == "CELULAR")
                {
                  Dato =  trdo.Rows[0]["Celular"].ToString();
                }
            }
            return Dato;
        }

        private double GetSaldoPrendaxCodVenta(Int32 CodVenta)
        {
            double Saldo = 0;
            Clases.cPrenda prenda = new Clases.cPrenda();
            DataTable tPrenda = prenda.GetPrendaxCodVenta(CodVenta);
            if (tPrenda.Rows.Count > 0)
                if (tPrenda.Rows[0]["Saldo"].ToString() != "")
                {
                    Saldo = Convert.ToDouble(tPrenda.Rows[0]["Saldo"].ToString()); 
                }
            return Saldo;
        }

        private double GetSaldoCuotaxCodVenta(Int32 CodVenta)
        {
            Clases.cCuota cuota = new Clases.cCuota();
            double Saldo = 0;
            Saldo  = cuota.GetSaldoDeudaCuotas(CodVenta);
            return Saldo; 
        }

        private double GetSaldoChequexCodVenta(Int32 CodVenta)
        {
            int ban = 0;
            Clases.cCheque cheque = new Clases.cCheque();
            DataTable tcheque = cheque.GetChequexCodVenta(CodVenta);
            if (tcheque.Rows.Count > 0)
            {
                for (int i = 0; i < tcheque.Rows.Count; i++)
                {
                    if (tcheque.Rows[i]["FechaPago"].ToString() == "")
                    {
                        ban = 1;
                    }
                }
            }
            if (ban == 1)
                return 1;
            else
                return 0;
        }

        private double GetSaldoCobranza(Int32 CodVenta)
        {
            double Saldo = 0;
            Clases.cCobranza cob = new Clases.cCobranza();
            DataTable tcob = cob.GetCobranzaxCodVenta(CodVenta);
            if (tcob.Rows.Count >0)
                if (tcob.Rows[0]["Saldo"].ToString() != "")
                {
                    Saldo = Convert.ToDouble(tcob.Rows[0]["Saldo"].ToString());
                }
            return Saldo;
        }
    }
}
