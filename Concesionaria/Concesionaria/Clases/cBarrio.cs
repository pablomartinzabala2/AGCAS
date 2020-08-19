using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;

namespace Concesionaria.Clases
{
    public class cBarrio
    {
        public DataTable Getbarrios()
        {
            string sql = "select * from barrio order by nombre";
            return cDb.ExecuteDataTable(sql);
        }
    }
}
