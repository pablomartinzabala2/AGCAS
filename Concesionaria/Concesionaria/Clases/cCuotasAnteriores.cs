using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Concesionaria.Clases
{
    public  class cCuotasAnteriores
    {
        public Int32 GetMaxCodGrupo()
        {
            string sql = "select max(CodGrupo) as CodGrupo";
            sql = sql + " from CuotasAnteriores";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            Int32 CodGrupo = 1;
            if (trdo.Rows.Count >0)
                if (trdo.Rows[0]["CodGrupo"].ToString() != "")
                {
                    CodGrupo = Convert.ToInt32(trdo.Rows[0]["CodGrupo"].ToString());
                    CodGrupo = CodGrupo + 1;
                }
            return CodGrupo;
        }

        public DataTable GetCuotasAnterioresxPatente(string Patente)
        {
            string sql = "select * from CuotasAnteriores";
            sql = sql + " where Patente=" + "'" + Patente + "'" ;
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetCuotasAnterioresxCodGrupo(Int32 CodGrupo)
        {
            string sql = "select * from CuotasAnteriores";
            sql = sql + " where CodGrupo=" + CodGrupo.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable GetCuotasxCodVenta(Int32 CodGrupo)
        {
            string sql = "select CodGrupo,Cuota,FechaVencimiento,Importe,FechaPago,ImportePagado from CuotasAnteriores where CodGrupo =" + CodGrupo.ToString();
            DataTable tventa = cDb.ExecuteDataTable(sql);
            return tventa;
        }

        public double GetSaldoDeudaCuotas(Int32 CodGrupo)
        {
            double Deuda = 0;
            double Importe = 0;
            double Saldo = 0;
            string sql = "select sum(Importe) as Importe from cuotasanteriores";
            sql = sql + " where FechaPago is null";
            sql = sql + " and CodGrupo =" + CodGrupo.ToString();
            DataTable tDeuda = cDb.ExecuteDataTable(sql);
            if (tDeuda.Rows.Count > 0)
            {
                if (tDeuda.Rows[0]["Importe"].ToString() != "")
                    Importe = Convert.ToDouble(tDeuda.Rows[0]["Importe"].ToString());
            }

            string sql2 = "select sum(Saldo),0 as Saldo from cuotasanteriores";
            sql2 = sql2 + " where CodGrupo =" + CodGrupo.ToString();
            DataTable tSaldo = cDb.ExecuteDataTable(sql2);


            if (tSaldo.Rows.Count > 0)
            {
                if (tSaldo.Rows[0]["Saldo"].ToString() != "")
                    Saldo = Convert.ToDouble(tSaldo.Rows[0]["Saldo"].ToString());
            }

            Deuda = Importe + Saldo;
            return Deuda;
        }

        public Double GetMontoCuotasImpagas()
        {
            cFunciones fun = new cFunciones();
            string sql = "select isnull(sum(Importe),0) as Importe from CuotasAnteriores";
            sql = sql + " where FechaPago is null";
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
            string sql = "select isnull(sum(ImporteSinInteres),0) as ImporteSinInteres from cuotasanteriores";
            sql = sql + " where FechaPago is null";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            Double Importe = 0;
            if (trdo.Rows.Count > 0)
            {
                Importe = Convert.ToDouble(trdo.Rows[0]["ImporteSinInteres"].ToString());
            }
            return Importe;
        }

        public Boolean GrabarCuota(Int32 CodGrupo, Int32 Cuota, DateTime FechaPago, double ImportePagado, double Saldo, Int32 CodUsuario, string Patente, Double Punitorio)
        {
            string sql = "update cuotasanteriores";
            sql = sql + " set ImportePagado =" + ImportePagado.ToString().Replace(",", ".");
            sql = sql + ",FechaPago=" + "'" + FechaPago.ToShortDateString() + "'";
            sql = sql + ",Saldo=" + Saldo.ToString().Replace(",", ".");
            sql = sql + " where CodGrupo =" + CodGrupo.ToString();
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
                sqlMov = sqlMov + "values(" + "'" + FechaPago.ToShortDateString() + "'";
                sqlMov = sqlMov + "," + CodUsuario.ToString();
                sqlMov = sqlMov + "," + ImportePagado.ToString();
                sqlMov = sqlMov + ",0,0,0";
                sqlMov = sqlMov + "," + "'" + Descripcion + "'";
                sqlMov = sqlMov + ")";

                Command2.Transaction = Transaccion;
                Command2.CommandText = sqlMov;
                Command2.ExecuteNonQuery();
                if (Punitorio >0)
                {
                    string sqlPuni ="";
                    sqlPuni ="Insert into PunitorioCuotasAnteriores(CodGrupo,Cuota,Importe,Fecha)";
                    sqlPuni = sqlPuni + " Values(" + CodGrupo.ToString ();
                    sqlPuni = sqlPuni + "," + Cuota.ToString ();
                    sqlPuni = sqlPuni + "," + Punitorio.ToString ().Replace (",",".");
                    sqlPuni = sqlPuni + "," + "'" + FechaPago.ToShortDateString () + "'";
                    sqlPuni = sqlPuni + ")";
                    SqlCommand puni = new SqlCommand ();
                    puni.Transaction = Transaccion ;
                    puni.Connection = con;
                    puni.CommandText = sqlPuni;
                    puni.ExecuteNonQuery ();

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
  
   
       public Boolean AnularCuota(Int32 CodGrupo, Int32 Cuota, double ImportePagado, Int32 CodUsuario, string Patente,Double Punitorio)
        {
            string sql = "";
            sql = "Update cuotasanteriores set FechaPago =null";
            sql = sql + ", ImportePagado =null";
            sql = sql + ", Saldo =Importe";
            sql = sql + " where CodGrupo =" + CodGrupo.ToString();
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
            Descripcin = Descripcin + " PATENTE " + Patente.ToString();
            try
            {
                Comand.Transaction = Transaccion;
                Comand.CommandText = sql;
                Comand.ExecuteNonQuery();
                ImportePagado = ImportePagado + Punitorio;
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
                    string sql3 = "Delete from PunitorioCuotasAnteriores";
                    sql3 = sql3 + " where CodGrupo=" + CodGrupo.ToString();
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
            sql = sql + "c.Apellido,c.Nombre,c.Telefono";
            sql = sql + ",c.Patente ";
            sql = sql + ",c.Descripcion,c.CodGrupo ";
            sql = sql + " from CuotasAnteriores c";
            sql = sql + " where c.FechaVencimiento >=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and c.FechaVencimiento <= " + "'" + FechaHasta.ToShortDateString() + "'";
            if (SoloImpago == true)
                sql = sql + " and isnull(Saldo,Importe) <> 0";
            if (Patente != "")
                sql = sql + " and c.Patente like " + "'%" + Patente +"%'";
            if (Apellido != "")
                sql = sql + " and c.Apellido like" + "'%" + Apellido + "%'" ;
            sql = sql + " order by c.FechaVencimiento asc";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            return trdo;
        }

        public void PagarSaldoCuota(Int32 CodGrupo, Int32 Cuota, double Importe)
        {
            string sql = "Update CuotasAnteriores set ImportePagado = ImportePagado +" + Importe.ToString().Replace(",", ".");
            sql = sql + ", Saldo = Saldo -" + Importe.ToString().Replace(",", ".");
            sql = sql + " where CodGrupo =" + CodGrupo.ToString();
            sql = sql + " and Cuota=" + Cuota.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public Double GetGanaciaCobroCuotas(DateTime FechaDesde, DateTime FechaHasta)
        {
            double Ganancia = 0;
            string sql = " select (sum(Importe) - sum(ImporteSinInteres) - sum(Saldo)) as Ganancia";
            sql = sql + " from CuotasAnteriores";
            sql = sql + " where fechaPago is not null";
            sql = sql + " and FechaVencimiento>=" + "'" + FechaDesde.ToShortDateString() + "'";
            sql = sql + " and FechaVencimiento<=" + "'" + FechaHasta.ToShortDateString() + "'";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["Ganancia"].ToString() != "")
                    Ganancia = Convert.ToDouble(trdo.Rows[0]["Ganancia"].ToString());
            return Ganancia;
        }

        public DataTable GetCuotasAnterioresAdeudades(string Patente, string Apellido,DateTime Fecha,int ConDeuda)
        {
            string sql = "";
            if (Patente != "")
            {
                sql = "select * from CuotasAnteriores ";
                sql = sql + " where Patente like " + "'%" + Patente + "%'";
                sql = sql + " and Saldo >0 ";
                if (ConDeuda == 1)
                sql = sql + " and FechaVencimiento <" + "'" + Fecha.ToShortDateString() + "'";
            }
            else
            {
                sql = "select * from CuotasAnteriores ";
                sql = sql + " where Apellido like " + "'%" + Apellido + "%'";
                sql = sql + " and Saldo >0 ";
                if (ConDeuda == 1)
                sql = sql + " and FechaVencimiento <" + "'" + Fecha.ToShortDateString() + "'";
            }
            return cDb.ExecuteDataTable(sql);
        }
    }
}
