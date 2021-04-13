using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Concesionaria.Clases
{
    public static  class cConexion
    {
        public static  string Cadenacon()
        {
            //*****CASA**********
            string cadena = "Data Source=DESKTOP-BI5616B\\SQLEXPRESS;Initial Catalog=CONCESIONARIA;Integrated Security=True";
          //  string cadena = "Data Source=DESKTOP-QKECIIE;Initial Catalog=COPIACONCESIONARIA;Integrated Security=True";
          //     string cadena = "Data Source=DESKTOP-QKECIIE;Initial Catalog=CONCESIONARIA;Integrated Security=True";
           // string cadena = "Data Source=DESKTOP-QKECIIE;Initial Catalog=COPIACONCESIONARIA;Integrated Security=True";
           // string cadena = "Data Source=ALLINONE;Initial Catalog=AUTOMOTORES;Integrated Security=True";
          //  string cadena = "Data Source=ALLINONE;Initial Catalog=AUTOMOTORES;User ID=sa;Password=123";
           
            return cadena;
        }
    }
}
