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
            //  CASTAÑO
            // string cadena = "Data Source=ALLINONE;Initial Catalog=AUTOMOTORES;Integrated Security=True";
            //  string cadena = "Data Source=ALLINONE;Initial Catalog=AUTOMOTORES;User ID=sa;Password=123";
            //casa coriente
            //string cadena = "Data Source=DESKTOP-BI5616B\\SQLEXPRESS;Initial Catalog=AgenciaCorriente;Integrated Security=True";
            //corrientes
            // string cadena = "Data Source=DESKTOP-QMPK26U\\SQLEXPRESS;Initial Catalog=AgenciaCorriente;Integrated Security=True";
            //SistemadeTaller.Properties.Settings.Default.TALLERConnectionString;
            string cadena = Concesionaria.Properties.Settings.Default.CONCESIONARIAConnectionString; 
            return cadena;
        }
    }
}
