using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Concesionaria.Clases
{
    public class cCuota
    {
        public Double  GetMontoCuotasImpagas()
        {
            cFunciones fun = new cFunciones ();
            string sql = "select isnull(sum(Saldo),0) as Importe from Cuotas";
               // sql = sql + " where FechaPago is null";
                DataTable trdo = cDb.ExecuteDataTable(sql);
                Double Importe = 0;    
            if (trdo.Rows.Count > 0)
                {
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
                }
            return Importe;
        }

        public Double GetMontoCuotasImpagasSinInteres()
        {
            cFunciones fun = new cFunciones();
            string sql = "select isnull(sum(ImporteSinInteres),0) as ImporteSinInteres from Cuotas";
            sql = sql + " where FechaPago is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            Double Importe = 0;
            if (trdo.Rows.Count > 0)
            {
                Importe = Convert.ToDouble(trdo.Rows[0]["ImporteSinInteres"].ToString());
            }
            return Importe;
        }

        public DataTable GetCuotasxCodVenta(Int32 CodVenta)
        {
            string sql ="select CodVenta,Cuota,FechaVencimiento,Importe,FechaPago,ImportePagado,Saldo from cuotas where CodVenta =" + CodVenta.ToString ();
            DataTable tventa = cDb.ExecuteDataTable(sql);
            return tventa;
        }

        public Boolean  GrabarCuota(Int32 CodVenta, Int32 Cuota, DateTime FechaPago, double ImportePagado,double Saldo,Int32 CodUsuario,string Patente,Double Punitorio)
        {
            string sql ="update Cuotas";
            sql = sql + " set ImportePagado =" + ImportePagado.ToString ().Replace (",",".");
            sql = sql + ",FechaPago=" + "'" + FechaPago.ToShortDateString() + "'";
            sql = sql + ",Saldo=" + Saldo.ToString().Replace(",", ".");
            sql = sql + " where CodVenta =" + CodVenta.ToString();
            sql = sql + " and Cuota =" + Cuota.ToString();
            //cDb.ExecutarNonQuery(sql); grabo la cuota
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Clases.cConexion.Cadenacon();
            con.Open();
            SqlTransaction Transaccion;
            Transaccion = con.BeginTransaction();
            SqlCommand Comand = new SqlCommand();
            SqlCommand Command2 = new SqlCommand();
            Comand.Connection = con;
            Command2.Connection = con;
            string Descripcion = "PAGO DE CUOTA " + Cuota.ToString();
            Descripcion = Descripcion + " PATENTE " + Patente.ToString();
            try
            {
                Comand.Transaction = Transaccion;
                Comand.CommandText = sql;
                Comand.ExecuteNonQuery();
                //grabo el movimiento
                string sqlMov = "Insert into Movimiento(Fecha,CodUsuario,ImporteEfectivo";
                sqlMov = sqlMov + ",ImporteDocumento,ImportePrenda,ImporteAuto,Descripcion)";
                sqlMov = sqlMov + "values(" + "'" + FechaPago.ToShortDateString () + "'";
                sqlMov = sqlMov + "," + CodUsuario.ToString();
                sqlMov = sqlMov + "," + ImportePagado.ToString();
                sqlMov = sqlMov + ",0,0,0";
                sqlMov = sqlMov + "," + "'" + Descripcion + "'";
                sqlMov = sqlMov + ")";

                Command2.Transaction = Transaccion;
                Command2.CommandText = sqlMov;
                Command2.ExecuteNonQuery();
                if (Punitorio > 0)
                {
                    string sqlPunit = "";
                    sqlPunit = "Insert into PunitorioCuotas(";
                    sqlPunit = sqlPunit + "CodVenta,Cuota,Importe,Fecha)";
                    sqlPunit = sqlPunit + " Values(" + CodVenta.ToString();
                    sqlPunit = sqlPunit + "," + Cuota.ToString();
                    sqlPunit = sqlPunit + "," + Punitorio.ToString().Replace(",", ".");
                    sqlPunit = sqlPunit + "," + "'" + FechaPago.ToShortDateString() + "'";
                    sqlPunit = sqlPunit + ")";
                    SqlCommand comandPuni = new SqlCommand();
                    comandPuni.Connection = con;
                    comandPuni.Transaction = Transaccion;
                    comandPuni.CommandText = sqlPunit;
                    comandPuni.ExecuteNonQuery();

                    Descripcion = "COBRO DE PUNITORIO, PATENTE " + Patente;
                    string sqlMovPun = "Insert into Movimiento(Fecha,CodUsuario,ImporteEfectivo";
                    sqlMovPun = sqlMovPun + ",ImporteDocumento,ImportePrenda,ImporteAuto,Descripcion)";
                    sqlMovPun = sqlMovPun + "values(" + "'" + FechaPago.ToShortDateString() + "'";
                    sqlMovPun = sqlMovPun + "," + CodUsuario.ToString();
                    sqlMovPun = sqlMovPun + "," + Punitorio.ToString().Replace(",", ".");
                    sqlMovPun = sqlMovPun + ",0,0,0";
                    sqlMovPun = sqlMovPun + "," + "'" + Descripcion + "'";
                    sqlMovPun = sqlMovPun + ")";
                    SqlCommand movPun = new SqlCommand();
                    movPun.Transaction = Transaccion;
                    movPun.Connection = con;
                    movPun.CommandText = sqlMovPun;
                    movPun.ExecuteNonQuery();
                }
                Transaccion.Commit();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                return false;
            }
            
        }

        public double GetSaldoDeudaCuotas(Int32 CodVenta)
        {
            double Deuda =0;
            double Importe =0;
            double Saldo =0;
            string sql = "select sum(Importe) as Importe from Cuotas";
            sql = sql + " where FechaPago is null";
            sql = sql + " and CodVenta =" + CodVenta.ToString();
            DataTable tDeuda = cDb.ExecuteDataTable(sql);
            if (tDeuda.Rows.Count > 0)
            {
                if (tDeuda.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(tDeuda.Rows[0]["Importe"].ToString());
            }

            string sql2 = "select sum(Saldo),0 as Saldo from Cuotas";
            sql2 = sql2 + " where CodVenta =" + CodVenta.ToString ();
            DataTable tSaldo = cDb.ExecuteDataTable(sql2);


            if (tSaldo.Rows.Count > 0)
            {
                if (tSaldo.Rows[0]["Saldo"].ToString() != "")
                    Saldo = Convert.ToDouble(tSaldo.Rows[0]["Saldo"].ToString());
            }

            Deuda = Importe + Saldo;
            return Deuda;
        }

        public Boolean  AnularCuota(Int32 CodVenta, Int32 Cuota,double ImportePagado,Int32 CodUsuario,string Patente, Double Punitorio)
        {
            string sql = "";
            sql = "Update Cuotas set FechaPago =null";
            sql = sql + ", ImportePagado =null";
            sql = sql + ", Saldo =Importe";
            sql = sql + " where CodVenta =" + CodVenta.ToString () ;
            sql = sql + " and Cuota =" + Cuota.ToString();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = Clases.cConexion.Cadenacon();
            con.Open();
            SqlTransaction Transaccion;
            Transaccion = con.BeginTransaction();
            SqlCommand Comand = new SqlCommand();
            SqlCommand Command2 = new SqlCommand();
            Comand.Connection = con;
            Command2.Connection = con;
            string Descripcin = "ANULACION DE CUOTA " + Cuota.ToString();
            Descripcin = Descripcin + " PATENTE " + Patente.ToString() ;
            try
            {
                ImportePagado = ImportePagado + Punitorio;
                Comand.Transaction = Transaccion;
                Comand.CommandText = sql;
                Comand.ExecuteNonQuery();
                //grabo el movimiento
                string sql2 = "Insert into Movimiento(Fecha,CodUsuario,ImporteEfectivo";
                sql2 = sql2 + ",ImporteDocumento,ImportePrenda,ImporteAuto,Descripcion)";
                sql2 = sql2 + "values(" + "'" + DateTime.Now.ToShortDateString() + "'";
                sql2 = sql2 + "," + CodUsuario.ToString();
                sql2 = sql2 + "," + ImportePagado.ToString();
                sql2 = sql2 + ",0,0,0";
                sql2 = sql2 + "," + "'" + Descripcin + "'";
                sql2 = sql2 + ")";
                Command2.Transaction = Transaccion;
                Command2.CommandText = sql2;
                Command2.ExecuteNonQuery();
                if (Punitorio != 0)
                {
                    string sql3 = "Delete from PunitorioCuotas";
                    sql3 = sql3 + " where CodVenta=" + CodVenta.ToString ();
                    sql3 = sql3 + " and Cuota =" + Cuota.ToString();
                    SqlCommand sqlcomand3 = new SqlCommand();
                    sqlcomand3.Connection = con; 
                    sqlcomand3.Transaction = Transaccion;
                    sqlcomand3.CommandText = sql3;
                    sqlcomand3.ExecuteNonQuery(); 
                }
                Transaccion.Commit();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                return false;
            }
        }

        public DataTable GetDeuda(DateTime FechaDesde, DateTime FechaHasta, Boolean SoloImpago,string Patente,string Apellido)
        {
            string sql = "";
            sql = "select c.Cuota,c.Importe,c.ImportePagado,c.FechaVencimiento as Fecha  ,c.Saldo, ";
            sql = sql + "cli.Apellido,Cli.Nombre,cli.Telefono,cli.Celular";
            sql = sql + ",(select aa.Patente from auto aa where aa.CodAuto = v.CodAutoVendido) as Patente ";
            sql = sql + ",(select aa.Descripcion from auto aa where aa.CodAuto = v.CodAutoVendido) as Descripción ";
            sql = sql + " from Cuotas c, Venta v,Cliente cli,auto au";
            sql = sql + " where c.CodVenta = v.CodVenta";
            sql = sql + " and v.CodCliente = cli.CodCliente";
            sql = sql + " and v.CodAutoVendido = au.CodAuto";
            sql = sql + " and FechaVencimiento >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and FechaVencimiento <= " + "'" + FechaHasta.ToShortDateString() + "'";
            if (SoloImpago ==true)
                sql = sql + " and isnull(Saldo,0) <> 0";
                //sql = sql + " and isnull(Saldo,Importe) <> 0";
            if (Patente != "")
                sql = sql + " and au.Patente like " + "'%" + Patente + "%'";
            if (Apellido != "")
                sql = sql + " and cli.Apellido like" + "'%" + Apellido + "%'" ;
            sql = sql + " order by FechaVencimiento desc";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            return trdo;
        }

        public void PagarSaldoCuota(Int32 CodVenta, Int32 Cuota, double Importe)
        {
            string sql = "Update Cuotas set ImportePagado = ImportePagado +" + Importe.ToString ().Replace (",",".");
            sql = sql + ", Saldo = Saldo -" + Importe.ToString().Replace(",", ".");
            sql = sql + " where CodVenta =" + CodVenta.ToString () ;
            sql = sql + " and Cuota=" + Cuota.ToString () ;
            cDb.ExecutarNonQuery(sql);
        }

        public Double GetGanaciaCobroCuotas(DateTime FechaDesde, DateTime FechaHasta)
        {
            double Ganancia=0;
           // string sql = " select (sum(Importe) - sum(ImporteSinInteres) - sum(Saldo)) as Ganancia";
            string sql = " select (sum(Importe) - sum(ImporteSinInteres)) as Ganancia";
            sql = sql + " from Cuotas";
            sql = sql + " where fechaPago is not null";
            sql = sql + " and FechaVencimiento>=" + "'" + FechaDesde.ToShortDateString () + "'";
            sql = sql + " and FechaVencimiento<=" + "'" + FechaHasta.ToShortDateString() + "'";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Ganancia"].ToString() != "")
                    Ganancia = Convert.ToDouble(trdo.Rows[0]["Ganancia"].ToString());
            return Ganancia;
        }

        public double ImportePagado(Int32 CodVenta)
        {
            double Importe =0;
            string sql = "select isnull(sum(ImportePagado),0) as Importe ";
            sql = sql + " from Cuotas ";
            sql = sql + " where CodVenta=" + CodVenta.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(trdo.Rows[0]["Importe"].ToString());
            return Importe;
        }

        public DataTable GetCuotasAdeudadas(string Patente, string Apellido, DateTime Fecha,int ConDeuda)
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
                sql = "select * ";
                sql = sql + " from Cuotas c,Venta v,Cliente cli,Auto a";
                sql = sql + " where c.CodVenta=v.CodVenta";
                sql = sql + " and v.CodCliente = cli.CodCliente ";
                sql = sql + " and v.CodAutoVendido = a.CodAuto";
                sql = sql + " and c.Saldo >0 ";
                if(ConDeuda ==1)
                    sql = sql + " and c.FechaVencimiento <" + "'" + Fecha.ToShortDateString() + "'";
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

                 sql = "select * ";
                sql = sql + " from Cuotas c,Venta v,Cliente cli,Auto a";
                sql = sql + " where c.CodVenta=v.CodVenta";
                sql = sql + " and v.CodCliente = cli.CodCliente ";
                sql = sql + " and v.CodAutoVendido = a.CodAuto";
                sql = sql + " and c.Saldo >0 ";
                if (ConDeuda ==1)
                    sql = sql + " and c.FechaVencimiento <" + "'" + Fecha.ToShortDateString() + "'";
                sql = sql + " and v.CodCliente in " + ListaCliente.ToString();
            }
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetCuotasAdeudadasxCodVenta(Int32 CodVenta)
        {
            string sql = "select CodVenta,Cuota,ImporteSinInteres from Cuotas";
            sql = sql + " where Saldo = Importe";
            sql = sql + " and CodVenta=" + CodVenta.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void  GrabarCuotaTransaccion(SqlConnection con,SqlTransaction tran,Int32 CodVenta, Int32 Cuota, DateTime FechaPago, double ImportePagado, double Saldo)
        {
            string sql = "update Cuotas";
            sql = sql + " set ImportePagado =" + ImportePagado.ToString().Replace(",", ".");
            sql = sql + ",FechaPago=" + "'" + FechaPago.ToShortDateString() + "'";
            sql = sql + ",Saldo=" + Saldo.ToString().Replace(",", ".");
            sql = sql + ",Importe=ImporteSinInteres";
            sql = sql + " where CodVenta =" + CodVenta.ToString();
            sql = sql + " and Cuota =" + Cuota.ToString();
            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            comand.Transaction = tran;
            comand.CommandText = sql;
            comand.ExecuteNonQuery();
            
            

        }

        public Double GetSaldoCuotas(Int32 CodVenta)
        {
            Double Saldo = 0;
            string sql = "Select isnull(Sum(Saldo),0) as Saldo";
            sql = sql + " from Cuotas where CodVenta=" + CodVenta.ToString();
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
