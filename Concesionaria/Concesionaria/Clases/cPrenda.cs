using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;

namespace Concesionaria.Clases
{
    public class cPrenda
    {
        public DataTable GetPrendaxPatente(string Patente)
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
            string sql = "select p.*,a.Descripcion,cli.Nombre,cli.Apellido from Prenda p, Auto a,Venta v,Cliente cli ";
            sql = sql + " where p.CodAuto = a.CodAuto  ";
            sql = sql + " and v.CodVenta = p.CodVenta ";
            sql = sql + " and v.CodCliente = cli.CodCliente";
            sql = sql + " and a.CodAuto =" + CodAuto.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetPrendaxCodigo(Int32 CodPrenda)
        {
            string sql = "select p.*,ep.Descripcion";
            sql = sql + ",(select au.Patente from auto au where au.CodAuto = p.CodAuto) as Patente";
            sql = sql + ",(select nombre from Cliente cli where cli.CodCliente = p.CodCliente) as Nombre";
            sql = sql + ",(select apellido from Cliente cli where cli.CodCliente = p.CodCliente) as Apellido";
            sql = sql + " from Prenda p,EntidadPrendaria ep";
            sql = sql + " where p.CodEntidad = ep.CodEntidad ";
            sql = sql + " and p.CodPrenda = " + CodPrenda.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void RegistrarPagoPrenda(Int32 CodPrenda, DateTime Fecha,double ImportePagar,double Diferencia)
        {
            string sql = "Update Prenda "; 
            sql = sql + " set FechaPago =" + "'" + Fecha.ToShortDateString () + "'" ;
            sql = sql + ", Diferencia =" + Diferencia.ToString().Replace(",", ".");
            sql = sql + ", Saldo = 0 ";
            sql = sql + ",ImportePagado=" + ImportePagar.ToString().Replace(",", ".");
            sql = sql + " where CodPrenda=" + CodPrenda.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public void AnularPagoPrenda(Int32 CodPrenda)
        {
            string sql = "Update Prenda set FechaPago =null";
            sql = sql + " ,Saldo = Importe,ImportePagado = null,Diferencia =0";
            sql = sql + " where CodPrenda=" + CodPrenda.ToString () ;
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetPrendasxFecha(DateTime FechaDesde, DateTime FechaHasta,int SoloImpagos,string Patente,string Apellido)
        {
            string sql = "";
            sql = "select a.Patente,a.Descripcion,cli.Apellido,p.Importe,p.Fecha,p.FechaPago,p.CodPrenda,p.ImportePagado,p.Diferencia";
            sql = sql + " from Prenda p, auto a,Cliente cli";
            sql = sql + " where p.CodAuto = a.CodAuto";
            sql = sql + " and p.CodCliente = cli.CodCliente";
            sql = sql + " and p.Fecha >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and p.Fecha <=" + "'" + FechaHasta.ToShortDateString() + "'";
            if (SoloImpagos == 1)
                sql = sql + " and p.FechaPago is null";
            if (Patente != "")
                sql = sql + " and a.Patente like" + "'%" + Patente  + "%'";
            if (Apellido !="")
                sql = sql + " and cli.Apellido like" + "'%" + Apellido   + "%'";
            sql = sql + " order by p.Fecha Desc";
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetPrendaxCodVenta(Int32 CodVenta)
        {
            string sql = "select p.*,ep.Descripcion";
            sql = sql + " from Prenda p,EntidadPrendaria ep";
            sql = sql + " where p.CodEntidad = ep.CodEntidad";
            sql = sql + " and p.CodVenta =" + CodVenta.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public double GetTotalPrenda()
        {
            double Importe = 0;
            string sql = "select sum(p.Saldo) as Importe";
            sql = sql + " from Prenda p,auto a ";
            sql = sql + " where p.CodAuto = a.CodAuto";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            }
            return Importe;
        }

        public double ImportePagado(Int32 CodVenta)
        {
            double Importe = 0;
            string sql = "select isnull(sum(ImportePagado),0) as Importe ";
            sql = sql + " from Prenda ";
            sql = sql + " where FechaPago is not null";
            sql = sql + " and CodVenta=" + CodVenta.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
        }

        public DataTable GetPrendasAdeudadas(string Patente, string Apellido, DateTime Fecha, int ConDeuda)
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
                sql = "select * from Prenda p,Venta v,Cliente cli,Auto a";
                sql = sql + " where p.CodVenta=v.CodVenta";
                sql = sql + " and v.CodCliente = cli.CodCliente ";
                sql = sql + " and v.CodAutoVendido = a.CodAuto";
                sql = sql + " and p.FechaPago is null";
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
                sql = "select * from Prenda p,Venta v,Cliente cli,Auto a";
                sql = sql + " where p.CodVenta=v.CodVenta";
                sql = sql + " and v.CodCliente = cli.CodCliente ";
                sql = sql + " and v.CodAutoVendido = a.CodAuto";
                sql = sql + " and p.FechaPago is null";

                if (ListaCliente != "()")
                    sql = sql + " and v.CodCliente in " + ListaCliente.ToString();
                else
                    sql = sql + " and v.CodCliente=-1";
            }
            return cDb.ExecuteDataTable(sql);
        }

        public double ImporteAdeudado(Int32 CodVenta)
        {
            double Importe = 0;
            string sql = "select isnull(sum(Saldo),0) as Importe ";
            sql = sql + " from Prenda ";
            //sql = sql + " where FechaPago is  null";
            sql = sql + " where CodVenta=" + CodVenta.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
        }

        
    }
}
