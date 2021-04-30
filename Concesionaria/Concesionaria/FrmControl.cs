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
    public partial class FrmControl : Form
    {
        public FrmControl()
        {
            InitializeComponent();
        }

        private void FrmControl_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            lblVencidas.BackColor = Color.LightGreen;  
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int ConDeuda = 0;
            if (ChkVencida.Checked == true)
                ConDeuda = 1;
            Clases.cFunciones fun = new Clases.cFunciones();
            DataTable tResul = fun.CrearTabla("Codigo;Tipo;Cuota;Patente;Descripcion;Apellido;Telefono;Celular;Importe;Saldo;Vencimiento");
            if (txtPatente.Text == "" && txtApellido.Text == "")
            {
                Mensaje("Debe ingresar un criterio de búsqueda");
                return;
            }
            DateTime Fecha = Convert.ToDateTime (txtFecha.Text);
            string Valor = "";
            cCuota cuota = new cCuota();
            DataTable trdo = cuota.GetCuotasAdeudadas(txtPatente.Text, txtApellido.Text, Fecha,ConDeuda); 
            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                Valor = trdo.Rows[i]["CodVenta"].ToString();
                Valor = Valor + ";" + "Cuotas";
                Valor = Valor + ";" + trdo.Rows[i]["Cuota"].ToString();
                Valor = Valor + ";" +   trdo.Rows[i]["Patente"].ToString();
                Valor = Valor +";" +  trdo.Rows[i]["Descripcion"].ToString();
                Valor = Valor + ";" + trdo.Rows[i]["Apellido"].ToString();
                Valor = Valor + ";" + trdo.Rows[i]["Telefono"].ToString();
                Valor = Valor + ";" + trdo.Rows[i]["Celular"].ToString();
                Valor = Valor + ";" +  trdo.Rows[i]["Importe"].ToString();
                Valor = Valor + ";" + trdo.Rows[i]["Saldo"].ToString();
                Valor = Valor + ";" + trdo.Rows[i]["FechaVencimiento"].ToString();
                tResul = fun.AgregarFilas(tResul, Valor);

            }
            cCuotasAnteriores cuotasAnt = new cCuotasAnteriores();
            DataTable tcuotasAnt = cuotasAnt.GetCuotasAnterioresAdeudades(txtPatente.Text, txtApellido.Text, Fecha, ConDeuda);
            for (int i = 0; i < tcuotasAnt.Rows.Count; i++)
            {
                Valor = tcuotasAnt.Rows[i]["CodGrupo"].ToString();
                Valor = Valor + ";" + "Doc. Anteriores";
                Valor = Valor + ";" + tcuotasAnt.Rows[i]["Cuota"].ToString();
                Valor = Valor + ";" + tcuotasAnt.Rows[i]["Patente"].ToString();
                Valor = Valor + ";" + tcuotasAnt.Rows[i]["Descripcion"].ToString();
                Valor = Valor + ";" + tcuotasAnt.Rows[i]["Apellido"].ToString();
                Valor = Valor + ";" + tcuotasAnt.Rows[i]["Telefono"].ToString();
                Valor = Valor + ";" + tcuotasAnt.Rows[i]["Telefono"].ToString();
                Valor = Valor + ";" + tcuotasAnt.Rows[i]["Importe"].ToString();
                Valor = Valor + ";" + tcuotasAnt.Rows[i]["Saldo"].ToString();
                Valor = Valor + ";" + tcuotasAnt.Rows[i]["FechaVencimiento"].ToString();
                tResul = fun.AgregarFilas(tResul, Valor);

            }
            cCobranza cob = new cCobranza();
            DataTable tcob = cob.GetCobranzasAdeudadas(txtPatente.Text, txtApellido.Text, Fecha, ConDeuda);
            for (int i = 0; i < tcob.Rows.Count; i++)
            {
                Valor = tcob.Rows[i]["CodCobranza"].ToString();
                Valor = Valor + ";" + "Cobranza";
                Valor = Valor + ";1";
                Valor = Valor + ";" + tcob.Rows[i]["Patente"].ToString();
                Valor = Valor + ";" + tcob.Rows[i]["Descripcion"].ToString();
                Valor = Valor + ";" + tcob.Rows[i]["Apellido"].ToString();
                Valor = Valor + ";" + tcob.Rows[i]["Telefono"].ToString();
                Valor = Valor + ";" + tcob.Rows[i]["Celular"].ToString();
                Valor = Valor + ";" + tcob.Rows[i]["Importe"].ToString();
                Valor = Valor + ";" + tcob.Rows[i]["Saldo"].ToString();
                Valor = Valor + ";" + tcob.Rows[i]["FechaCompromiso"].ToString();
                tResul = fun.AgregarFilas(tResul, Valor);
            }
            cCheque cheque = new cCheque();
            DataTable tcheque = cheque.GetChequesAdeudados(txtPatente.Text, txtApellido.Text, Fecha, ConDeuda);
            for (int i = 0; i < tcheque.Rows.Count; i++)
            {
                Valor = tcheque.Rows[i]["CodVenta"].ToString();
                Valor = Valor + ";" + "Cheque";
                Valor = Valor + ";1";
                Valor = Valor + ";" + tcheque.Rows[i]["Patente"].ToString();
                Valor = Valor + ";" + tcheque.Rows[i]["Descripcion"].ToString();
                Valor = Valor + ";" + tcheque.Rows[i]["Apellido"].ToString();
                Valor = Valor + ";" + tcheque.Rows[i]["Telefono"].ToString();
                Valor = Valor + ";" + tcheque.Rows[i]["Celular"].ToString();
                Valor = Valor + ";" + tcheque.Rows[i]["Importe"].ToString();
                Valor = Valor + ";" + tcheque.Rows[i]["Importe"].ToString();
                Valor = Valor + ";" + tcheque.Rows[i]["FechaVencimiento"].ToString();
                tResul = fun.AgregarFilas(tResul, Valor);
            }
            cPrenda prenda = new cPrenda();
            DataTable tPrenda = prenda.GetPrendasAdeudadas(txtPatente.Text, txtApellido.Text, Fecha, ConDeuda);
            for (int i = 0; i < tPrenda.Rows.Count; i++)
            {
                Valor = tPrenda.Rows[i]["CodPrenda"].ToString();
                Valor = Valor + ";" + "Prenda";
                Valor = Valor + ";1";
                Valor = Valor + ";" + tPrenda.Rows[i]["Patente"].ToString();
                Valor = Valor + ";" + tPrenda.Rows[i]["Descripcion"].ToString();
                Valor = Valor + ";" + tPrenda.Rows[i]["Apellido"].ToString();
                Valor = Valor + ";" + tPrenda.Rows[i]["Telefono"].ToString();
                Valor = Valor + ";" + tPrenda.Rows[i]["Celular"].ToString();
                Valor = Valor + ";" + tPrenda.Rows[i]["Importe"].ToString();
                Valor = Valor + ";" + tPrenda.Rows[i]["Importe"].ToString();
                Valor = Valor + ";";
                tResul = fun.AgregarFilas(tResul, Valor);
            }
            cCobranzaGeneral cobGen = new cCobranzaGeneral();
           // if (txtApellido.Text != "")
           // {
                DataTable tCobGen = cobGen.GetDedudaCobranzaGeneral(txtApellido.Text, txtPatente.Text);
                for (int i = 0; i < tCobGen.Rows.Count; i++)
                {
                Valor = tCobGen.Rows[i]["CodCobranza"].ToString();
                Valor = Valor + ";" + "Cobranza General";
                Valor = Valor + ";1";
                Valor = Valor + ";" + tCobGen.Rows[i]["Patente"].ToString();
                Valor = Valor + ";" + tCobGen.Rows[i]["Descripcion"].ToString();
                Valor = Valor + ";" + tCobGen.Rows[i]["Cliente"].ToString();
                Valor = Valor + ";" + tCobGen.Rows[i]["Telefono"].ToString();
                Valor = Valor + ";";
                Valor = Valor + ";" + tCobGen.Rows[i]["Importe"].ToString();
                Valor = Valor + ";" + tCobGen.Rows[i]["Saldo"].ToString();
                Valor = Valor + ";";
                tResul = fun.AgregarFilas(tResul, Valor);
                }
            //}
                //DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
                DateTime FechaDesde = Fecha.AddDays(-200);
                DateTime FechaHasta = Fecha.AddDays(10);
                Clases.cAlarma alarma = new Clases.cAlarma();
                DataTable talarma = alarma.GetAlertasxRangoFecha(FechaDesde , FechaHasta , "", txtPatente.Text, txtApellido.Text); 
            for (int i=0;i< talarma.Rows.Count ;i++)
            {
                Valor = talarma.Rows[i]["CodAlarma"].ToString();
                Valor = Valor + ";" + "Alerta";
                Valor = Valor + ";1";
                Valor = Valor + ";" + talarma.Rows[i]["Patente"].ToString();
                Valor = Valor + ";" + talarma.Rows[i]["Nombre"].ToString();
                Valor = Valor + ";" + talarma.Rows[i]["Cliente"].ToString();
                Valor = Valor + ";" + "";
                Valor = Valor + ";";
                Valor = Valor + ";" + "0";
                Valor = Valor + ";" + "0";
                Valor = Valor + ";";
                tResul = fun.AgregarFilas(tResul, Valor);
            }
            Double TotalImporte = 0;
            Double TotalSaldo = 0;
            TotalImporte = fun.TotalizarColumna(tResul, "Importe");
            TotalSaldo = fun.TotalizarColumna(tResul, "Saldo");
            Valor = ";" + "Total";
            Valor = Valor + ";;;;;;";
            Valor = Valor + ";" + TotalImporte.ToString();
            Valor = Valor + ";" + TotalSaldo.ToString();
            Valor = Valor + ";";
            tResul = fun.AgregarFilas(tResul, Valor);
            tResul = fun.TablaaMiles(tResul, "Importe");
            tResul = fun.TablaaMiles(tResul, "Saldo");
            Grilla.DataSource = tResul;
            Grilla.Columns[0].Visible = false;
            Pintar();
            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                if (i == (Grilla.Rows.Count - 2))
                    Grilla.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
            }
        }

        private void btnCobroPrenda_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un registro para continuar");
                return;
            }
            string Tipo = Grilla.CurrentRow.Cells[1].Value.ToString();
            switch (Tipo)
            {
                case "Cuotas":
                    string patente = Grilla.CurrentRow.Cells[3].Value.ToString();
            Principal.CodigoPrincipalAbm = patente.ToString();
            FrmCobroCuotas form = new FrmCobroCuotas();
            form.ShowDialog();
             break;
                case "Cobranza":
             string Patente = Grilla.CurrentRow.Cells[3].Value.ToString();
            Principal.CodigoPrincipalAbm = Patente;
            FrmCobroDocumentos cobro = new FrmCobroDocumentos();
            cobro.ShowDialog();
             break;
                case "Cheque":
                    Int32 CodVenta = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            Clases.cVenta venta = new Clases.cVenta();
            DataTable trdo = venta.GetVentaxCodigo(CodVenta);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodAutoVendido"].ToString() != "")
                {
                    string CodAuto = trdo.Rows[0]["CodAutoVendido"].ToString();
                    Principal.CodigoPrincipalAbm = CodAuto;
                    FrmCobroCheque frm = new FrmCobroCheque();
                    frm.ShowDialog();
                }
            }
             break;
                case "Doc. Anteriores":
                    string CodGrupo = Grilla.CurrentRow.Cells[0].Value.ToString();
            Principal.CodigoPrincipalAbm = CodGrupo.ToString();
            FrmCobroDocumentosAnteriores FrmDocAnt = new FrmCobroDocumentosAnteriores();
            FrmDocAnt.ShowDialog();
             break;
                case "Prenda":
             Int32 CodPrenda = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            Principal.CodigoPrincipalAbm = CodPrenda.ToString();
            FrmCobroPrenda frmPrenda = new FrmCobroPrenda();
            frmPrenda.ShowDialog();
             break;
                case "Cobranza General":
                     Int32 CodCobranza = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
                     Principal.CodigoPrincipalAbm = CodCobranza.ToString();
                     FrmRegistrarCobroCobranzasGenerales FrmCob = new FrmRegistrarCobroCobranzasGenerales();
                    FrmCob.ShowDialog();
             break;
            }
        }

        private void Pintar()
        {
            string Tipo = "";
            DateTime? Vencimiento;
            DateTime Fecha = Convert.ToDateTime (txtFecha.Text);
            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                Tipo = Grilla.Rows[i].Cells[1].Value.ToString(); Grilla.Rows[i].Cells[10].Value.ToString();
                if (Grilla.Rows[i].Cells[10].Value.ToString() != "")
                    Vencimiento = Convert.ToDateTime(Grilla.Rows[i].Cells[10].Value.ToString());
                else
                    Vencimiento = null;
                if (Vencimiento !=null)
                {
                    if (Convert.ToDateTime (Vencimiento) < Fecha)
                    {
                        Grilla.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
        }
       
    }
}
