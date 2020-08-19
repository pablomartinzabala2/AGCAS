using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Concesionaria.Clases
{
    public  class cCheque
    {
        public Double GetTotalChequesaCobrar()
        {
            Double Importe =0;
            string sql = "select sum(Importe) as Importe";
            sql = sql + " from Cheque ";
            sql = sql + " where FechaPago is null" ;
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
        }

        public DataTable GetChequesxPatente(string Patente)
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
            string sql = "select ch.CodVenta,ch.NroCheque,ch.Importe,ch.FechaPago,ch.FechaVencimiento";
            sql = sql + ",a.Descripcion,c.Nombre,c.Apellido";
            sql = sql + " from Cheque ch,Auto a,Cliente c,venta v"; 
            sql = sql + " where ch.CodVenta = v.CodVenta ";
            sql = sql + " and v.CodAutoVendido = a.CodAuto";
            sql = sql + " and ch.CodCliente = c.CodCliente";
            sql = sql + " and v.CodAutoVendido =" + CodAuto.ToString();
            
            return cDb.ExecuteDataTable(sql);
        }

        public void RegistrarCobroCheque(string Fecha,Int32 CodVenta,string NroCheque)
        {
            string sql = "Update Cheque set FechaPago = " + "'" + Fecha + "'";
            sql = sql + " where CodVenta =" + CodVenta.ToString();
            sql = sql + " and NroCheque =" + "'" + NroCheque  + "'" ;
            cDb.ExecutarNonQuery(sql);
        }

        public void AnularCobroCheque(Int32 CodVenta, string NroCheque)
        {
            string sql = "Update Cheque set FechaPago =null";
            sql = sql + " Where CodVenta =" + CodVenta.ToString();
            sql = sql + " and NroCheque =" + "'" + NroCheque  +"'";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetChequesxFecha(DateTime FechaDesde, DateTime FechaHasta,int Impagos,Int32? CodBanco,string NroCheque)
        {
            string sql = "select c.Fecha,c.FechaPago,c.FechaVencimiento";
            sql = sql + ",c.NroCheque,c.Importe,";
            sql = sql + "(select b.Nombre from banco b where b.CodBanco = c.CodBanco) as Banco";
            sql = sql + ",v.CodAutoVendido as CodAuto";
            sql = sql + ",au.Patente,c.EntregadoA as Entregado ";
            sql = sql + " from Cheque c,Venta v,auto au ";
            sql = sql + " where c.CodVenta = v.CodVenta";
            sql = sql + " and v.CodAutoVendido= au.CodAuto";
            sql = sql + " and c.Fecha>=" + "'" + FechaDesde + "'" ;
            sql = sql + " and c.Fecha <=" + "'" + FechaHasta + "'";
            if (Impagos == 1)
                sql = sql + " and c.FechaPago is  null";
            if (CodBanco != null)
                sql = sql + " and c.CodBanco = " + CodBanco.ToString ();
            if (NroCheque != "")
                sql = sql + " and c.NroCheque like " + "'%" + NroCheque + "%'" ;
            sql = sql + " order by c.Fecha Desc";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetChequexCodVenta(Int32 CodVenta)
        {
            string Sql = "select * from Cheque where CodVenta=" + CodVenta.ToString ();
            Sql = Sql + " and CodPrenda =null";
            return cDb.ExecuteDataTable(Sql);
        }

        public double ImportePagado(Int32 CodVenta)
        {
            double Importe = 0;
            string sql = "select isnull(sum(Importe),0) as Importe ";
            sql = sql + " from Cheque ";
            sql = sql + " where FechaPago is not null";
            sql = sql + " and CodVenta=" + CodVenta.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
        }

        public DataTable GetChequesAdeudados(string Patente, string Apellido, DateTime Fecha,int ConDeuda )
        {
            int b = 0;
            Int32 CodAuto = 0;
            string sql = "";
            string ListaCodAuto = "(";
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
                sql = sql + " select *";
                sql = sql + " from Cheque c,venta v, auto a,cliente cli";
                sql = sql + " where c.CodVenta = v.CodVenta ";
                sql = sql + " and v.CodAutoVendido = a.CodAuto";
                sql = sql + " and v.CodCliente = cli.CodCliente";
                if (ConDeuda == 1)
                    sql = sql + " and c.FechaVencimiento <" + "'" + Fecha.ToShortDateString () + "'" ;
                sql = sql + " and c.FechaPago is null";
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
                if (ListaCliente == "()")
                    ListaCliente = "(-1)";
                sql = sql + " select *";
                sql = sql + " from Cheque c,venta v, auto a,cliente cli";
                sql = sql + " where c.CodVenta = v.CodVenta ";
                sql = sql + " and v.CodAutoVendido = a.CodAuto";
                sql = sql + " and v.CodCliente = cli.CodCliente";
                if (ConDeuda == 1)
                    sql = sql + " and c.FechaVencimiento <" + "'" + Fecha.ToShortDateString() + "'";
                sql = sql + " and c.FechaPago is null";
                if (ListaCliente != "(")
                    sql = sql + " and v.CodCliente in " + ListaCliente.ToString();
                else
                    sql = sql + " and v.CodCliente=-1";
            }
            return cDb.ExecuteDataTable(sql);
        }

        public Double GetSaldoCheque(Int32 CodVenta)
        {
            Double Importe = 0;
            string sql = "Select isnull(Sum(Importe),0) as Importe";
            sql = sql + " from Cheque where CodVenta=" + CodVenta.ToString();
            sql = sql + " and FechaPago is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["Importe"].ToString() != "")
                {
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
                }
            }
            return Importe;
        }

        public void GrabarEntrega(string  NroCheque, string EntregadoA)
        {
            string sql = "update Cheque ";
            sql = sql + " set EntregadoA=" + "'" + EntregadoA + "'";
            sql = sql + " where NroCheque=" + "'" + NroCheque.ToString() + "'";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetChequexCodPrenda(Int32 CodPrenda)
        {
            string sql = "select c.NroCheque,c.Importe,c.FechaVencimiento";
            sql = sql + ",c.CodBanco,b.Nombre";
            sql = sql + " from Cheque c,Banco b";
            sql = sql + " where c.CodBanco= b.CodBanco ";
            sql = sql + " and c.CodPrenda=" + CodPrenda.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void BorrarChequexCodPrenda(Int32 CodPrenda)
        {
            string sql = "Delete from Cheque";
            sql = sql + " where CodPrenda=" + CodPrenda.ToString();
            cDb.ExecutarNonQuery(sql);
        }
    }
}
