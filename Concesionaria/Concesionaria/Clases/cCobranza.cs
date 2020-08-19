using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data ;
namespace Concesionaria.Clases
{
    public  class cCobranza
    {
        public DataTable GetCobranzaxPatente(string Patente)
        {
            Int32 CodAuto = -1;
            cAuto auto = new cAuto();
            DataTable tauto = auto.GetAutoxPatente(Patente);
            if (tauto.Rows.Count > 0)
            {
                if (tauto.Rows[0]["CodAuto"].ToString() != "")
                {
                    CodAuto = Convert.ToInt32(tauto.Rows[0]["CodAuto"].ToString());
                }
            }
            string sql = "select c.CodCobranza,c.CodVenta,c.Importe,c.Fecha,c.FechaPago,Cli.Apellido,Cli.Nombre,A.Descripcion,c.ImportePagado,c.Saldo";
            sql = sql + ",( select isnull(sum(pun.Importe),0)  from PunitorioCobranza pun where pun.CodCobranza =c.CodCobranza) as Punitorio ";
            sql = sql + " from Cobranza c,Venta v,Cliente Cli,Auto a";
            sql = sql + " where c.CodVenta = v.CodVenta ";
            sql = sql + " and v.CodCliente = cli.CodCliente ";
            sql = sql + " and c.CodAuto = a.CodAuto ";
            sql = sql + " and c.CodAuto =" + CodAuto.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public void RegistrarCobranza(Int32 CodCobranza,string FechaPago,double ImportePagado,double Saldo)
        {
            string sql = "Update Cobranza ";
            sql = sql + " set FechaPago=" + "'" + FechaPago + "'";
            sql = sql + ",ImportePagado = " + ImportePagado.ToString().Replace(",", ".");
            sql = sql + ",Saldo =" + Saldo.ToString ().Replace (",",".") ;
            sql = sql + " where CodCobranza =" + CodCobranza.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public void AnularCobranza(Int32 CodCobranza)
        {
            string sql = "Update Cobranza ";
            sql = sql + " set FechaPago=null,ImportePagado=0";
            sql = sql + ", Saldo = Importe";
            sql = sql + " where CodCobranza =" + CodCobranza.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetCobranzaxFecha(DateTime FechaDesde, DateTime FechaHasta,string Patente,string Apellido,int SoloImpago )
        {
            string sql = "";
            sql = " select c.Importe,";
            sql = sql + "(select aa.patente from auto aa where aa.CodAuto = c.CodAuto) as Patente,";
            sql = sql + "(select aa.Descripcion from auto aa where aa.CodAuto = c.CodAuto) as Descripción";
            sql = sql + ",c.Fecha,c.FechaPago,";
            sql = sql + "(select cli.apellido from cliente cli where cli.CodCliente = c.CodCliente ) as Apellido";
            sql = sql + ",c.FechaCompromiso,c.Saldo";
            sql = sql + " from Cobranza c, auto aut,Cliente Cli";
            sql = sql + " where c.CodAuto = aut.CodAuto";
            sql = sql + " and c.CodCliente = Cli.CodCliente";
            sql = sql + " and c.Fecha >=" + "'" + FechaDesde  +"'";
            sql = sql + " and c.Fecha <=" + "'" + FechaHasta + "'";
            if (Patente != "")
                sql = sql + " and aut.Patente like" + "'%" + Patente  +"%'";
            if (Apellido != "")
                sql = sql + " and cli.Apellido like" + "'%" + Apellido  + "%'" ;
            if (SoloImpago == 1)
                sql = sql + " and c.Saldo>0";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            return trdo;
        }

        public double GetTotalDeudaCobranzas()
        {
            string sql = "select sum(isnull(Saldo,0)) as total from cobranza";
            double Total = 0;
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count >0)
                if (trdo.Rows[0]["Total"].ToString ()!="") 
                    Total = Convert.ToDouble (trdo.Rows[0]["Total"].ToString ()); 
            return Total;
        }

        public void PagarSaldo(Int32 CodCobranza,DateTime Fecha,double Importe)
        {
            string sql = "update cobranza set ";
            sql = sql + " ImportePagado = ImportePagado + " + Importe.ToString ().Replace (",",".");
            sql = sql + " ,FechaPago=" + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + ",Saldo = Saldo - " + Importe.ToString().Replace(",", ".");
            sql = sql + " where CodCobranza=" + CodCobranza.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetCobranzaxCodVenta(Int32 CodVenta)
        {
            string sql = "select * from Cobranza where CodVenta =" + CodVenta.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public double GetImportePagado(Int32 CodVenta)
        {
            double Importe =0;
            string sql = "select ImportePagado";
             sql = sql + " from Cobranza ";
            sql = sql + " where FechaPago is not null";
            sql = sql + " and CodVenta=" + CodVenta.ToString ();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["ImportePagado"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["ImportePagado"].ToString());
            return Importe;
        }

        public DataTable GetCobranzasAdeudadas(string Patente, string Apellido, DateTime Fecha, int ConDeuda)
        {
            int b = 0;
            Int32 CodAuto = 0;
            string ListaCodAuto = "(";
            string sql = "";
            if (Patente != "")
            {
                b = 1;
                Clases.cAuto auto = new Clases.cAuto();
                DataTable trdo = auto.GetAutoxContenidoPatente(Patente);
                if (trdo.Rows.Count > 0)
                {
                    if (trdo.Rows[0]["CodAuto"].ToString() != "")
                    {
                        for (int i = 0; i < trdo.Rows.Count; i++)
                        {
                            b = 1;
                            CodAuto = Convert.ToInt32(trdo.Rows[i]["CodAuto"].ToString());
                            if (ListaCodAuto == "(")
                                ListaCodAuto = ListaCodAuto + CodAuto.ToString();
                            else
                                ListaCodAuto = ListaCodAuto + "," + CodAuto.ToString();
                        }
                        ListaCodAuto = ListaCodAuto + ")";
                    }


                }
                sql = "select * from Cobranza c,Venta v,Cliente cli,Auto a";
                sql = sql + " where c.CodVenta=v.CodVenta";
                sql = sql + " and v.CodCliente = cli.CodCliente ";
                sql = sql + " and v.CodAutoVendido = a.CodAuto";
                sql = sql + " and c.Saldo >0 ";
                if (ConDeuda == 1)
                    sql = sql + " and c.FechaCompromiso <" + "'" + Fecha.ToShortDateString() + "'";
                if (ListaCodAuto != "(")
                    sql = sql + " and v.CodAutoVendido in " + ListaCodAuto.ToString();
                else
                    sql = sql + " and v.CodAutoVendido=-1";
            }
            if (b == 0)
            {
                string ListaCliente = "(";
                cCliente cli = new cCliente();
                DataTable trdo = cli.GetClientexApellido(Apellido);
                for (int i = 0; i < trdo.Rows.Count; i++)
                {
                    if (ListaCliente == "(")
                    {
                        ListaCliente = ListaCliente + trdo.Rows[i]["CodCliente"].ToString();
                    }
                    else
                    {
                        ListaCliente = ListaCliente + "," + trdo.Rows[i]["CodCliente"].ToString();
                    }
                }
                ListaCliente = ListaCliente + ")";
                if (ListaCliente =="()")
                    ListaCliente ="(-1)";
                sql = "select * from Cobranza c,Venta v,Cliente cli,Auto a";
                sql = sql + " where c.CodVenta=v.CodVenta";
                sql = sql + " and v.CodCliente = cli.CodCliente ";
                sql = sql + " and v.CodAutoVendido = a.CodAuto";
                sql = sql + " and c.Saldo >0 ";
                if (ConDeuda == 1)
                 sql = sql + " and c.FechaCompromiso <" + "'" + Fecha.ToShortDateString() + "'";
                if (ListaCliente != "(")
                    sql = sql + " and v.CodCliente in " + ListaCliente.ToString();
                else
                    sql = sql + " and v.CodAutoVendido=-1";
            }
            return cDb.ExecuteDataTable(sql);
        }

        public Double GetSaldoCobranza(Int32 CodVenta)
        {
            Double Saldo = 0;
            string sql = "Select isnull(Sum(Saldo),0) as Saldo";
            sql = sql + " from Cobranza where CodVenta=" + CodVenta.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["Saldo"].ToString() != "")
                {
                    Saldo = Convert.ToDouble(trdo.Rows[0]["Saldo"].ToString());
                }
            }
            return Saldo;
        }
       
    }
}
