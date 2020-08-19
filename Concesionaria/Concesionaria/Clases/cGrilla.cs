using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms ;
namespace Concesionaria.Clases
{
    public static  class cGrilla
    {
        public static void EliminarFila(DataGridView Grilla, string CampoBorrar, string ValorBorrar)
        {
            int i,j;
            string Columna = "";
            DataTable trdo = new DataTable();
            for (i = 0;i< Grilla.Columns.Count ; i++)
            {
                Columna = Grilla.Columns[i].Name;
                trdo.Columns.Add(Columna); 
            }

            for (i = 0; i < Grilla.Rows.Count-1; i++)
            {
                DataRow r;
                r = trdo.NewRow();
                for (j = 0; j < Grilla.Columns.Count; j++)
                {
                    r[j] = Grilla.Rows[i].Cells[j].Value.ToString();   
                }
                trdo.Rows.Add(r);
            }

            for (i = 0; i < trdo.Rows.Count; i++)
            {
                if (trdo.Rows[i][CampoBorrar].ToString().ToUpper() == ValorBorrar.ToUpper())
                {
                    trdo.Rows[i].Delete();
                    i = trdo.Rows.Count;
                }
            }
            Grilla.DataSource = trdo;
            
        }

        public static void EliminarFilaxdosFiltros(DataGridView Grilla, string CampoBorrar, string ValorBorrar, string CampoBorrar2, string ValorBorrar2)
        {
            int i, j;
            string Columna = "";
            DataTable trdo = new DataTable();
            for (i = 0; i < Grilla.Columns.Count; i++)
            {
                Columna = Grilla.Columns[i].Name;
                trdo.Columns.Add(Columna);
            }

            for (i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                DataRow r;
                r = trdo.NewRow();
                for (j = 0; j < Grilla.Columns.Count; j++)
                {
                    r[j] = Grilla.Rows[i].Cells[j].Value.ToString();
                }
                trdo.Rows.Add(r);
            }

            for (i = 0; i < trdo.Rows.Count; i++)
            {
                if (trdo.Rows[i][CampoBorrar].ToString().ToUpper() == ValorBorrar.ToUpper() && trdo.Rows[i][CampoBorrar2].ToString().ToUpper() == ValorBorrar2.ToUpper())
                {
                    trdo.Rows[i].Delete();
                    i = trdo.Rows.Count;
                }
            }
            Grilla.DataSource = trdo;

        }

        public static void FormatoColumnasMiles(DataGridView Grilla, string Columna)
        {
            cFunciones fun = new cFunciones();
            string Valor = "";
            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                Valor = Grilla.Rows[i].Cells[Columna].Value.ToString();
                Valor = fun.ParteEntera(Valor);
                Grilla.Rows[i].Cells[Columna].Value = fun.FormatoEnteroMiles(Valor);
            }
        }
    }
}
