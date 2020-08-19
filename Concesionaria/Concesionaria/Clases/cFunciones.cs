using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data; 

namespace Concesionaria.Clases
{
    public class cFunciones
    {
        public void LlenarCombo(ComboBox Combo,string Tabla,string Texto,string Value)
        {
            string Cadena = cConexion.Cadenacon ();
            string sql = "";
            sql = "select * from " + Tabla;
            sql = sql + " order by " + "'" + Texto + "'" ;
            DataTable trdo = SqlHelper.ExecuteDataset(Cadena, CommandType.Text, sql).Tables[0];
            DataTable trdo2 = new DataTable();
            trdo2.Columns.Add(Value);
            trdo2.Columns.Add(Texto);
            
            DataRow r = trdo2.NewRow() ;
            r[0] = 0;
            r[1] = "---Seleccionar---";
            trdo2.Rows.Add(r);
            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                DataRow r2 = trdo2.NewRow();
                r2[Value] = trdo.Rows[i][Value];
                r2[Texto] = trdo.Rows[i][Texto];
                trdo2.Rows.Add(r2);
            }
            Combo.DataSource = trdo2;
            Combo.ValueMember = Value;
            Combo.DisplayMember = Texto;
            Combo.SelectedIndex = 0;
        }

        public void LlenarComboDatatable(ComboBox Combo, DataTable trdo, string Texto, string Value)
        { 
            DataTable trdo2 = new DataTable();
            trdo2.Columns.Add(Value);
            trdo2.Columns.Add(Texto);

            DataRow r = trdo2.NewRow();
            r[0] = 0;
            r[1] = "---Seleccionar---";
            trdo2.Rows.Add(r);
            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                DataRow r2 = trdo2.NewRow();
                r2[Value] = trdo.Rows[i][Value];
                r2[Texto] = trdo.Rows[i][Texto];
                trdo2.Rows.Add(r2);
            }
            Combo.DataSource = trdo2;
            Combo.ValueMember = Value;
            Combo.DisplayMember = Texto;
            Combo.SelectedIndex = 0;
        }
        
        public double ToDouble(string Nro)
        {
            if (Nro == "")
                return 0;
            Nro = Nro.Replace(".", "");
            Nro = Nro.Replace(",", ".");
            return Convert.ToDouble(Nro);
        }

        public Boolean SoloNumerosDecimales(KeyPressEventArgs e)
        {
            Boolean op = false;
            if (char.IsDigit(e.KeyChar))
            {
                op = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                op = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                op = false;
            }
            else
            {
                op = true;
            }
            return op;
        }

        public Boolean SoloNumerosEnteros(KeyPressEventArgs e)
        {
            Boolean op = false;
            if (char.IsDigit(e.KeyChar))
            {
                op = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                op = false;
            }
            else
            {
                op = true;
            }
            return op;
        }

        public Boolean SoloLetras(KeyPressEventArgs e)
        {
            Boolean op = false;
            if (char.IsLetter(e.KeyChar))
            {
                op = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                op = false;
            }
            else if (char.IsSeparator(e.KeyChar))
            {
                op = false;
            }
            else
            {
                op = true;
            }
            return op;
        }

        public DataTable CrearTabla(string Lista)
        {
            DataTable Tabla = new DataTable();
            string[] Columnas = Lista.Split(';');
            foreach (string Col in Columnas)
            {
                Tabla.Columns.Add(Col); 
            }
            return Tabla;
        }

        public DataTable AgregarFilas(DataTable Tabla, string Lista)
        {
            
            string[] Datos = Lista.Split(';');
            DataRow r;
            r = Tabla.NewRow();
            int i = 0;
            foreach (string Dato in Datos)
            {
                r[i] = Dato;
                i++;
            }
            Tabla.Rows.Add(r);
            return Tabla;
        }

        public string FormatoEnteroMiles(string Nro)
        {
            Nro = Nro.Replace(".", "");
            int n = 0;
            if (Nro != "")
            {
                n = Convert.ToInt32(Nro);
                Nro = n.ToString("N0");
            }
            return Nro;
        }

        public void SoloEnteroConPunto(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar.ToString() == ",")
                e.Handled = true;
        }

        public string TransformarEntero(string Nro)
        {
            //se usa cuando regresa de la base
            Nro = Nro.Replace(",", ".");
            string[] vec = Nro.Split('.');
            return vec[0];
        }

        public Boolean ValidarFecha(string Fecha)
        {
            DateTime fec;
            try
            {
                fec = Convert.ToDateTime(Fecha);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable TablaaMiles(DataTable tabla, string Columna)
        {
            int i = 0;
            int j =0;
            string Valor ="";
            string Col ="";
            DataTable tabla2 = new DataTable();
            for (i = 0; i < tabla.Columns.Count; i++)
            {
                tabla2.Columns.Add(tabla.Columns[i].ColumnName.ToString ());  
            }

            for (i=0;i< tabla.Rows.Count ;i++)
            {
                DataRow r = tabla2.NewRow();
                for (j=0;j< tabla.Columns.Count ;j++)
                {
                    Valor = tabla.Rows[i][j].ToString ();
                    Col = tabla.Columns[j].ColumnName;
                    if (Col ==Columna)
                    {
                        Valor =FormatoEnteroMiles(ParteEntera (Valor)); 
                    }
                    r[j] = Valor;
                }
                tabla2.Rows.Add (r);
            }
            return tabla2;
        }

        public DataTable TablaaFechas(DataTable tabla, string Columna)
        {
            int i = 0;
            int j = 0;
            string Valor = "";
            string Col = "";
            DataTable tabla2 = new DataTable();
            for (i = 0; i < tabla.Columns.Count; i++)
            {
                tabla2.Columns.Add(tabla.Columns[i].ColumnName.ToString());
            }

            for (i = 0; i < tabla.Rows.Count; i++)
            {
                DataRow r = tabla2.NewRow();
                for (j = 0; j < tabla.Columns.Count; j++)
                {
                    Valor = tabla.Rows[i][j].ToString();
                    Col = tabla.Columns[j].ColumnName;
                    if (Col == Columna)
                    {
                        Valor = FormatoEnteroMiles(ParteEntera(Valor));
                        if (Valor.Length > 10)
                            Valor = Valor.Substring(0, 10);
                    }
                    r[j] = Valor;
                }
                tabla2.Rows.Add(r);
            }
            return tabla2;
        }

        public void GuardarNuevoGenerico(Control c, string Tabla)
        {
            string nombre = "";
            string Campo = "";
            string values = "";
            string  sql = "insert into " + Tabla;
            sql = sql + "(";
            int ban = 0;
            foreach (Control con in c.Controls)
            {
                if (con is GroupBox)
                {
                    
                    foreach (Control g in con.Controls)
                    {
                       // MessageBox.Show(g.Name);
                        
                        nombre = g.Name;
                        //txtM_ para las mascaras
                        if (nombre.Substring(0, 5) == "txtM_")
                        {
                            MaskedTextBox CajaTextoMascara = (MaskedTextBox)g;
                            string[] vec = nombre.Split('_');
                            if (ban == 0)
                            {
                                sql = sql + vec[1].ToString();
                                if (CajaTextoMascara.Text != "")
                                    values = "Values(" + "'" + CajaTextoMascara.Text + "'";
                                else
                                    values = "Values(null";
                                ban = 1;
                            }
                            else
                            {
                                sql = sql + "," + vec[1].ToString();
                                if (CajaTextoMascara.Text != "")
                                    values = values + "," + "'" + CajaTextoMascara.Text + "'";
                                else
                                    values = values + ",null";
                            }


                        }
                        if (nombre.Substring(0, 4) == "txt_")
                        {
                            TextBox CajaTexto = (TextBox)g;
                            string[] vec = nombre.Split('_');
                            if (ban == 0)
                            {
                                sql = sql + vec[1].ToString();
                                if (CajaTexto.Text != "")
                                    values = "Values(" + "'" + CajaTexto.Text + "'";
                                else
                                    values = "Values(null"; 
                                ban = 1;
                            }
                            else
                            {
                                sql = sql + "," + vec[1].ToString();
                                if (CajaTexto.Text != "")
                                    values = values + "," + "'" + CajaTexto.Text + "'";
                                else
                                    values = values + ",null";
                            }
                                
                            
                        }

                        if (nombre.Substring(0, 4) == "cmb_")
                        {
                            ComboBox Combo = (ComboBox)g;
                            string[] vec = nombre.Split('_');
                            if (ban == 0)
                            {
                                sql = sql + vec[1].ToString();
                                if (Combo.SelectedIndex > 0)
                                    values = "Values("+ Combo.SelectedValue.ToString();
                                else
                                    values = "(null";
                                ban = 1;
                            }
                            else
                            {
                                sql = sql + "," + vec[1].ToString();
                                if (Combo.SelectedIndex > 0)
                                    values = values + "," + Combo.SelectedValue.ToString();
                                else
                                    values = values + ",null";
                            }
                                
                            
                        }
                    }
                }
            }
            sql = sql + ")";
            values = values + ")";
                sql = sql + values ;
            
            cDb.ExecutarNonQuery(sql);
        }

        public void ModificarGenerico(Control c, string Tabla,string NombreClavePrimario,string ValorClavePrimaria)
        {
            string nombre = "";
            string Campo = "";
            string values = "";
            string sql = "Update " + Tabla;
            sql = sql + " set ";
            int ban = 0;
            foreach (Control con in c.Controls)
            {
                if (con is GroupBox)
                {

                    foreach (Control g in con.Controls)
                    {
                        // MessageBox.Show(g.Name);

                        nombre = g.Name;
                        //txtM_ para las mascaras
                        if (nombre.Substring(0, 5) == "txtM_")
                        {
                            MaskedTextBox CajaTextoMascara = (MaskedTextBox)g;
                            string[] vec = nombre.Split('_');
                            if (ban == 0)
                                sql = sql + vec[1].ToString() + "=" + "'" + CajaTextoMascara.Text + "'";
                            else
                                sql = sql + "," + vec[1].ToString() + "=" + "'" + CajaTextoMascara.Text + "'";
                            ban = 1;
                        }
                        if (nombre.Substring(0, 4) == "txt_")
                        {
                            TextBox CajaTexto = (TextBox)g;
                            string[] vec = nombre.Split('_');
                            if (ban ==0)
                                sql = sql + vec[1].ToString () + "=" + "'" + CajaTexto.Text + "'";
                            else
                                sql = sql + "," + vec[1].ToString() + "=" + "'" + CajaTexto.Text + "'";
                            ban = 1;
                        }

                        if (nombre.Substring(0, 4) == "cmb_")
                        {
                            ComboBox Combo = (ComboBox)g;
                            string[] vec = nombre.Split('_');
                            if (ban == 0)
                                if (Combo.SelectedIndex > 0)
                                    sql = sql + vec[1].ToString() + "=" + Combo.SelectedValue.ToString();
                                else
                                    sql = sql + vec[1].ToString() + "=null";

                            else
                                if (Combo.SelectedIndex > 0)
                                    sql = sql + "," + vec[1].ToString() + "=" + Combo.SelectedValue.ToString();
                                else
                                    sql = sql + "," + vec[1].ToString() + "=null";
                            ban = 1;
                        }
                    }
                }
            }
            sql = sql + " where " + NombreClavePrimario + "=" + ValorClavePrimaria.ToString ();
            cDb.ExecutarNonQuery(sql);
        }

        public void LimpiarGenerico(Control c)
        {
            string nombre = "";
            foreach (Control con in c.Controls)
            {
                if (con is GroupBox)
                {

                    foreach (Control g in con.Controls)
                    {
                        // MessageBox.Show(g.Name);

                        nombre = g.Name;
                        //txtM_ para las mascaras
                        if (nombre.Substring(0, 5) == "txtM_")
                        {
                            MaskedTextBox CajaTextoMascara = (MaskedTextBox)g;
                            CajaTextoMascara.Text = "";
                        }
                        if (nombre.Substring(0, 4) == "txt_")
                        {
                            TextBox CajaTexto = (TextBox)g;
                            CajaTexto.Text ="";
                        }

                        

                        if (nombre.Substring(0, 4) == "cmb_")
                        {
                            ComboBox Combo = (ComboBox)g;
                            string[] vec = nombre.Split('_');
                            Combo.SelectedIndex = 0;
                        }
                    }
                }
            }
           
        }

        public void CargarControles(Control c, string Tabla, string CampoClavePrimaria, string ValorClavePrimaria)
        {
            string nombre;
            string sql = "select * from " + Tabla;
            sql = sql + " where " + CampoClavePrimaria + "=" + ValorClavePrimaria.ToString();
            DataTable trdo = cDb.ExecuteDataTable(sql);
            if (trdo.Rows.Count == 0)
            {
                return;
            }

            foreach (Control con in c.Controls)
            {
                if (con is GroupBox)
                {

                    foreach (Control g in con.Controls)
                    {
                        nombre = g.Name;
                        if (nombre.Substring(0, 4) == "txt_")
                        {
                            TextBox CajaTexto = (TextBox)g;
                            string[] vec = nombre.Split('_');
                            CajaTexto.Text = trdo.Rows[0][vec[1].ToString()].ToString();
                        }

                        if (nombre.Substring(0, 4) == "cmb_")
                        {
                            ComboBox Combo = (ComboBox)g;
                            string[] vec = nombre.Split('_');
                            if (trdo.Rows[0][vec[1]].ToString() != "")
                            {
                                Combo.SelectedValue = trdo.Rows[0][vec[1]].ToString();
                            }
                            else
                            {
                                Combo.SelectedIndex = 0;
                            }

                        }

                        nombre = g.Name;
                        
                        //txtM_ para las mascaras
                        if (nombre.Substring(0, 5) == "txtM_")
                        {
                            string[] vec = nombre.Split('_');
                            MaskedTextBox CajaTextoMascara = (MaskedTextBox)g;
                            CajaTextoMascara.Text = trdo.Rows[0][vec[1]].ToString();
                        }
                    }
                }
            }
        }

        public void EliminarGenerico(string Tabla, string CampoClave, string ValorClave)
        {
            string sql = "delete from " + Tabla;
            sql = sql + " where " + CampoClave + "=" + ValorClave.ToString();
            cDb.ExecutarNonQuery(sql);
        }

        public double CalcularTotalGrilla(DataGridView Grilla, string Campo)
        {
             
        
            string sImporte = "";
            Clases.cFunciones fun = new Clases.cFunciones();
            double Total = 0;
            //Importe = fun.ToDouble(txtImporteVehiculoCompra.Text);
            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                sImporte = Grilla.Rows[i].Cells[Campo].Value.ToString();
                Total = Total + Convert.ToDouble (sImporte.Replace (".",""));
            }
            return Total;
        }

        public string  ParteEntera(string Nro)
        {
            Nro = Nro.Replace(",", ".");
            string[] vec = Nro.Split('.');
            return vec[0]; 
        }

        public double TotalizarColumna(DataTable trdo, string Columna)
        {
            double Total = 0;
            Clases.cFunciones fun = new Clases.cFunciones ();
            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                if (trdo.Rows[i][Columna].ToString() != "")
                    Total = Total + Convert.ToDouble(trdo.Rows[i][Columna].ToString());
            }
            return Total;
        }

        public double TotalizarColumnaCondicion(DataTable trdo, string Columna,string ColumnaCondicion,string ValorCondicion)
        {
            double Total = 0;
            Clases.cFunciones fun = new Clases.cFunciones();
            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                if (trdo.Rows[i][ColumnaCondicion].ToString() == ValorCondicion)
                if (trdo.Rows[i][Columna].ToString() != "")
                    Total = Total + Convert.ToDouble(trdo.Rows[i][Columna].ToString());
            }
            return Total;
        }

        public string SepararDecimales(string sImporte)
        {
            //recibe un nro en formato 1000,00 y retorna en 1.000
            sImporte = sImporte.Replace(",", ".");
            string[] vec = sImporte.Split('.');
            sImporte = vec[0];
            sImporte = FormatoEnteroMiles(sImporte);
            return sImporte;
 
        }

        public DataTable EliminarFila(DataTable Trdo, string Columna, string Valor)
        {
            for (int i = 0; i < Trdo.Rows.Count; i++)
            {
                if (Trdo.Rows[i][Columna].ToString() == Valor)
                {
                    Trdo.Rows[i].Delete();
                    Trdo.AcceptChanges();
                    i = Trdo.Rows.Count;
                }
            }
            return Trdo;
        }

        public Boolean Buscar(DataTable Trdo, string Columna, string Valor)
        {
            Boolean Encontro = false;
            for (int i = 0; i < Trdo.Rows.Count; i++)
            {
                if (Trdo.Rows[i][Columna].ToString() == Valor)
                {
                    Encontro = true;
                }
            }
            return Encontro;
        }
    }
}
