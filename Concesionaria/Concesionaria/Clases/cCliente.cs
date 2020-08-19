using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
namespace Concesionaria.Clases
{
    public class cCliente
    {
        public DataTable GetClientesxNroDoc(Int32 CodTipoDoc, string NroDocumento)
        {
            string sql ="select * from cliente";
            sql = sql + " where NroDocumento =" + "'" + NroDocumento + "'";
            sql = sql + " and CodTipoDoc=" + CodTipoDoc.ToString ();
            return cDb.ExecuteDataTable(sql);
        }

        public void InsertarCliente(Int32? CodTipoDoc,string NroDocumento,
            string Nombre,string Apellido,string Telefono,string Celular,
            string Calle, string Altura, Int32? CodBarrio
            )
        {
            string sql = "Insert into Cliente(CodTipoDoc,NroDocumento,Nombre,Apellido";
            sql = sql + ",Telefono,Celular, Calle,  Numero, CodBarrio)";
            sql = sql + "Values(";
            if (CodTipoDoc == null)
                sql = sql + "null";
            else
                sql = sql + CodTipoDoc.ToString();
            sql = sql + "," + "'" + NroDocumento  + "'" ;
            sql = sql + "," + "'" + Nombre + "'";
            sql = sql + "," + "'" + Apellido + "'";
            sql = sql + "," + "'" + Telefono + "'";
            sql = sql + "," + "'" + Celular + "'";
            sql = sql + "," + "'" + Calle + "'";
            sql = sql + "," + "'" + Altura  + "'";
            if (CodBarrio == null)
                sql = sql + ",null";
            else
                sql = sql + "," + CodBarrio.ToString();
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public string  GetSqlInsertarCliente(Int32? CodTipoDoc, string NroDocumento,
            string Nombre, string Apellido, string Telefono, string Celular,
            string Calle, string Altura, Int32? CodBarrio
            )
        {
            string sql = "Insert into Cliente(CodTipoDoc,NroDocumento,Nombre,Apellido";
            sql = sql + ",Telefono,Celular, Calle,  Numero, CodBarrio)";
            sql = sql + "Values(";
            if (CodTipoDoc == null)
                sql = sql + "null";
            else
                sql = sql + CodTipoDoc.ToString();
            sql = sql + "," + "'" + NroDocumento + "'";
            sql = sql + "," + "'" + Nombre + "'";
            sql = sql + "," + "'" + Apellido + "'";
            sql = sql + "," + "'" + Telefono + "'";
            sql = sql + "," + "'" + Celular + "'";
            sql = sql + "," + "'" + Calle + "'";
            sql = sql + "," + "'" + Altura + "'";
            if (CodBarrio == null)
                sql = sql + ",null";
            else
                sql = sql + "," + CodBarrio.ToString();
            sql = sql + ")";
            return sql;
        }

        public DataTable GetClientexNroDoc(Int32? CodTipoDoc, string NroDocumento)
        {
            string sql = "select * from cliente";
            sql = sql + " where CodTipoDoc =" + CodTipoDoc.ToString();
            sql = sql + " and NroDocumento =" + "'" + NroDocumento + "'";
            DataTable trdo = cDb.ExecuteDataTable (sql);
            return trdo ;
        }

        public void ModificarCliente(Int32 CodCliente, Int32? CodTipoDoc, string NroDocumento,
            string Nombre, string Apellido, string Telefono, string Celular,
            string Calle, string Numero, Int32? CodBarrio)
        {
            string sql = "Update Cliente ";
            
            sql = sql + "set NroDocumento =" + "'" + NroDocumento + "'";
            if (CodTipoDoc == null)
                sql = sql + ",CodTipoDoc =null";
            else
                sql = sql + ",CodTipoDoc=" + CodTipoDoc.ToString();
            sql = sql + ",Nombre =" + "'" + Nombre + "'";
            sql = sql + ",Apellido=" + "'" + Apellido + "'";
            sql = sql + ",Telefono=" + "'" + Telefono + "'";
            sql = sql + ",Celular=" + "'" + Celular + "'";
            sql = sql + ",Calle=" + "'" + Calle + "'";
            sql = sql + ",Numero=" + "'" + Numero + "'";
            if (CodBarrio == null)
                sql = sql + ",CodBarrio =null";
            else
                sql = sql + ",CodBarrio =" + CodBarrio.ToString();
            sql = sql + " where CodCliente=" + CodCliente.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public string GetSqlModificarCliente(Int32 CodCliente, Int32? CodTipoDoc, string NroDocumento,
            string Nombre, string Apellido, string Telefono, string Celular,
            string Calle, string Numero, Int32? CodBarrio)
        {
            string sql = "Update Cliente ";

            sql = sql + "set NroDocumento =" + "'" + NroDocumento + "'";
            if (CodTipoDoc == null)
                sql = sql + ",CodTipoDoc =null";
            else
                sql = sql + ",CodTipoDoc=" + CodTipoDoc.ToString();
            sql = sql + ",Nombre =" + "'" + Nombre + "'";
            sql = sql + ",Apellido=" + "'" + Apellido + "'";
            sql = sql + ",Telefono=" + "'" + Telefono + "'";
            sql = sql + ",Celular=" + "'" + Celular + "'";
            sql = sql + ",Calle=" + "'" + Calle + "'";
            sql = sql + ",Numero=" + "'" + Numero + "'";
            if (CodBarrio == null)
                sql = sql + ",CodBarrio =null";
            else
                sql = sql + ",CodBarrio =" + CodBarrio.ToString();
            sql = sql + " where CodCliente=" + CodCliente.ToString();
            return sql;
        }

        public Int32 GetMaxCliente()
        {
            string sql = "select max(CodCliente) as CodCliente from Cliente";
            return Convert.ToInt32 (cDb.ExecuteScalar(sql, "CodCliente"));
        }

        public DataTable GetClientesxCodigo(Int32 CodCLiente)
        {
            string sql = "select * from cliente where CodCLiente =" + CodCLiente.ToString();
            return cDb.ExecuteDataTable(sql);
        }

        public DataTable BuscarCliente(string NroDocumento, string Apellido, string Nombre)
        {
            int UsoWhere = 0;
            string sql = "select CodCliente, NroDocumento as Documento ,Apellido,Nombre,Telefono as Teléfono,Celular";
            sql = sql + " from Cliente ";
            if (NroDocumento != "")
            {
                sql = sql + " Where NroDocumento =" + "'" + NroDocumento + "'";
                UsoWhere = 1;
            }

            if (Apellido != "")
            {
                if (UsoWhere == 1)
                {
                    sql = sql + " and Apellido =" + "'" + Apellido + "'";
                }
                else{
                    sql = sql + " where Apellido =" + "'" + Apellido + "'";
                }
                UsoWhere = 1;
            }

            if (Nombre != "")
            {
                if (UsoWhere == 1)
                {
                    sql = sql + " and Nombre =" + "'" + Nombre + "'";
                }
                else
                {
                    sql = sql + " where Nombre =" + "'" + Nombre + "'";
                }
                UsoWhere = 1;
            }
            return cDb.ExecuteDataTable(sql);
        }

        public Boolean PuedeBorrar(Int32 CodCliente)
        {
            Boolean Borra = true  ;
            string sql = "select * from venta where CodCliente =" + CodCliente.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["CodVenta"].ToString() != "")
                    Borra = false;
            return Borra;
        }

        public void InsertarClienteTransaccion(SqlConnection con, SqlTransaction Transaccion, Int32? CodTipoDoc, string NroDocumento,
            string Nombre, string Apellido, string Telefono, string Celular,
            string Calle, string Altura, Int32? CodBarrio
            )
        {
            string sql = "Insert into Cliente(CodTipoDoc,NroDocumento,Nombre,Apellido";
            sql = sql + ",Telefono,Celular, Calle,  Numero, CodBarrio)";
            sql = sql + "Values(";
            if (CodTipoDoc == null)
                sql = sql + "null";
            else
                sql = sql + CodTipoDoc.ToString();
            sql = sql + "," + "'" + NroDocumento + "'";
            sql = sql + "," + "'" + Nombre + "'";
            sql = sql + "," + "'" + Apellido + "'";
            sql = sql + "," + "'" + Telefono + "'";
            sql = sql + "," + "'" + Celular + "'";
            sql = sql + "," + "'" + Calle + "'";
            sql = sql + "," + "'" + Altura + "'";
            if (CodBarrio == null)
                sql = sql + ",null";
            else
                sql = sql + "," + CodBarrio.ToString();
            sql = sql + ")";
            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            comand.Transaction = Transaccion;
            comand.CommandText = sql;
            comand.ExecuteNonQuery();
            //cDb.ExecutarNonQuery(sql);
        }

        public Int32 GetMaxClientetTransaccion(SqlConnection con,SqlTransaction Transaccion)
        {
            string sql = "select max(CodCliente) as CodCliente from Cliente";
            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            comand.Transaction = Transaccion;
            comand.CommandText = sql;
            Int32 Codigo = Convert.ToInt32(comand.ExecuteScalar());
            return Codigo;
        }

        public void ModificarClientetTransaccion(SqlConnection con,SqlTransaction Transaccion,Int32 CodCliente, Int32? CodTipoDoc, string NroDocumento,
           string Nombre, string Apellido, string Telefono, string Celular,
           string Calle, string Numero, Int32? CodBarrio)
        {
            string sql = "Update Cliente ";

            sql = sql + "set NroDocumento =" + "'" + NroDocumento + "'";
            if (CodTipoDoc == null)
                sql = sql + ",CodTipoDoc =null";
            else
                sql = sql + ",CodTipoDoc=" + CodTipoDoc.ToString();
            sql = sql + ",Nombre =" + "'" + Nombre + "'";
            sql = sql + ",Apellido=" + "'" + Apellido + "'";
            sql = sql + ",Telefono=" + "'" + Telefono + "'";
            sql = sql + ",Celular=" + "'" + Celular + "'";
            sql = sql + ",Calle=" + "'" + Calle + "'";
            sql = sql + ",Numero=" + "'" + Numero + "'";
            if (CodBarrio == null)
                sql = sql + ",CodBarrio =null";
            else
                sql = sql + ",CodBarrio =" + CodBarrio.ToString();
            sql = sql + " where CodCliente=" + CodCliente.ToString();
            SqlCommand comand = new SqlCommand();
            comand.Connection = con;
            comand.Transaction = Transaccion;
            comand.CommandText = sql;
            comand.ExecuteNonQuery();
            //cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetClientexApellido(string Ape)
        {
            string sql = "select * from Cliente where Apellido like " + "'%" + Ape + "%'";
            return cDb.ExecuteDataTable(sql);
        }

        
    }
}
