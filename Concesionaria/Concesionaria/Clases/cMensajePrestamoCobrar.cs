﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public class cMensajePrestamoCobrar
    {
        public void InsertarMensaje(string Mensaje, DateTime Fecha, Int32 CodPrestamo)
        {
            string sql = "Insert into MensajesPrestamosCobrar";
            sql = sql + "(Mensaje,Fecha,CodPrestamo)";
            sql = sql + "values(" + "'" + Mensaje + "'";
            sql = sql + "," + "'" + Fecha.ToShortDateString() + "'";
            sql = sql + "," + CodPrestamo.ToString();
            sql = sql + ")";
            cDb.ExecutarNonQuery(sql);
        }

        public DataTable GetMensajesxCodPrestamo(Int32 CodPrestamo)
        {
            string sql = "select Fecha,Mensaje from MensajesPrestamosCobrar";
            sql = sql + " where CodPrestamo =" + CodPrestamo.ToString();
            sql = sql + " order by Fecha Desc";
            return cDb.ExecuteDataTable(sql);
        }
    }
}
