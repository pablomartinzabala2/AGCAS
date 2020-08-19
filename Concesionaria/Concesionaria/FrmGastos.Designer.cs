namespace Concesionaria
{
    partial class FrmGastos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAgregarCategoriaGasto = new System.Windows.Forms.Button();
            this.Grilla = new System.Windows.Forms.DataGridView();
            this.btnEliminarGasto = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.btnAgregarGasto = new System.Windows.Forms.Button();
            this.txtImporteGasto = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.CmbCategoriaGasto = new System.Windows.Forms.ComboBox();
            this.txtCodStock = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCodAuto = new System.Windows.Forms.TextBox();
            this.cmbCiudad = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKms = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMarca = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPatente = new System.Windows.Forms.TextBox();
            this.lblPatente = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grilla)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnAgregarCategoriaGasto);
            this.groupBox1.Controls.Add(this.Grilla);
            this.groupBox1.Controls.Add(this.btnEliminarGasto);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.btnAgregarGasto);
            this.groupBox1.Controls.Add(this.txtImporteGasto);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.CmbCategoriaGasto);
            this.groupBox1.Controls.Add(this.txtCodStock);
            this.groupBox1.Controls.Add(this.txtImporte);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtCodAuto);
            this.groupBox1.Controls.Add(this.cmbCiudad);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtKms);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtAnio);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbMarca);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPatente);
            this.groupBox1.Controls.Add(this.lblPatente);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(783, 427);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del vehículo";
            // 
            // btnAgregarCategoriaGasto
            // 
            this.btnAgregarCategoriaGasto.Image = global::Concesionaria.Properties.Resources.page_add;
            this.btnAgregarCategoriaGasto.Location = new System.Drawing.Point(395, 148);
            this.btnAgregarCategoriaGasto.Name = "btnAgregarCategoriaGasto";
            this.btnAgregarCategoriaGasto.Size = new System.Drawing.Size(40, 28);
            this.btnAgregarCategoriaGasto.TabIndex = 29;
            this.btnAgregarCategoriaGasto.UseVisualStyleBackColor = true;
            this.btnAgregarCategoriaGasto.Click += new System.EventHandler(this.btnAgregarCategoriaGasto_Click);
            // 
            // Grilla
            // 
            this.Grilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grilla.Location = new System.Drawing.Point(30, 193);
            this.Grilla.Name = "Grilla";
            this.Grilla.ReadOnly = true;
            this.Grilla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grilla.Size = new System.Drawing.Size(747, 215);
            this.Grilla.TabIndex = 28;
            // 
            // btnEliminarGasto
            // 
            this.btnEliminarGasto.Image = global::Concesionaria.Properties.Resources.cancel;
            this.btnEliminarGasto.Location = new System.Drawing.Point(699, 148);
            this.btnEliminarGasto.Name = "btnEliminarGasto";
            this.btnEliminarGasto.Size = new System.Drawing.Size(40, 28);
            this.btnEliminarGasto.TabIndex = 26;
            this.btnEliminarGasto.UseVisualStyleBackColor = true;
            this.btnEliminarGasto.Click += new System.EventHandler(this.btnEliminarGasto_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(441, 154);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 17);
            this.label17.TabIndex = 27;
            this.label17.Text = "Importe";
            // 
            // btnAgregarGasto
            // 
            this.btnAgregarGasto.Image = global::Concesionaria.Properties.Resources.add;
            this.btnAgregarGasto.Location = new System.Drawing.Point(653, 148);
            this.btnAgregarGasto.Name = "btnAgregarGasto";
            this.btnAgregarGasto.Size = new System.Drawing.Size(40, 28);
            this.btnAgregarGasto.TabIndex = 25;
            this.btnAgregarGasto.UseVisualStyleBackColor = true;
            this.btnAgregarGasto.Click += new System.EventHandler(this.btnAgregarGasto_Click);
            // 
            // txtImporteGasto
            // 
            this.txtImporteGasto.Location = new System.Drawing.Point(510, 151);
            this.txtImporteGasto.Name = "txtImporteGasto";
            this.txtImporteGasto.Size = new System.Drawing.Size(131, 23);
            this.txtImporteGasto.TabIndex = 24;
            this.txtImporteGasto.Leave += new System.EventHandler(this.txtImporteGasto_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(26, 154);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 17);
            this.label16.TabIndex = 22;
            this.label16.Text = "Gasto";
            // 
            // CmbCategoriaGasto
            // 
            this.CmbCategoriaGasto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbCategoriaGasto.FormattingEnabled = true;
            this.CmbCategoriaGasto.Location = new System.Drawing.Point(143, 151);
            this.CmbCategoriaGasto.Name = "CmbCategoriaGasto";
            this.CmbCategoriaGasto.Size = new System.Drawing.Size(246, 24);
            this.CmbCategoriaGasto.TabIndex = 21;
            // 
            // txtCodStock
            // 
            this.txtCodStock.Enabled = false;
            this.txtCodStock.Location = new System.Drawing.Point(336, 29);
            this.txtCodStock.Name = "txtCodStock";
            this.txtCodStock.Size = new System.Drawing.Size(54, 23);
            this.txtCodStock.TabIndex = 17;
            this.txtCodStock.Visible = false;
            // 
            // txtImporte
            // 
            this.txtImporte.Enabled = false;
            this.txtImporte.Location = new System.Drawing.Point(515, 89);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(112, 23);
            this.txtImporte.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(405, 92);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 17);
            this.label15.TabIndex = 15;
            this.label15.Text = "Importe";
            // 
            // txtCodAuto
            // 
            this.txtCodAuto.Enabled = false;
            this.txtCodAuto.Location = new System.Drawing.Point(276, 28);
            this.txtCodAuto.Name = "txtCodAuto";
            this.txtCodAuto.Size = new System.Drawing.Size(54, 23);
            this.txtCodAuto.TabIndex = 14;
            this.txtCodAuto.Visible = false;
            // 
            // cmbCiudad
            // 
            this.cmbCiudad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCiudad.Enabled = false;
            this.cmbCiudad.FormattingEnabled = true;
            this.cmbCiudad.Location = new System.Drawing.Point(143, 114);
            this.cmbCiudad.Name = "cmbCiudad";
            this.cmbCiudad.Size = new System.Drawing.Size(246, 24);
            this.cmbCiudad.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Radicación";
            // 
            // txtKms
            // 
            this.txtKms.Location = new System.Drawing.Point(515, 61);
            this.txtKms.Name = "txtKms";
            this.txtKms.Size = new System.Drawing.Size(100, 23);
            this.txtKms.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(405, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Kms";
            // 
            // txtAnio
            // 
            this.txtAnio.Enabled = false;
            this.txtAnio.Location = new System.Drawing.Point(143, 86);
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(100, 23);
            this.txtAnio.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Año";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Location = new System.Drawing.Point(515, 32);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(262, 23);
            this.txtDescripcion.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Descripción";
            // 
            // cmbMarca
            // 
            this.cmbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarca.Enabled = false;
            this.cmbMarca.FormattingEnabled = true;
            this.cmbMarca.Location = new System.Drawing.Point(143, 57);
            this.cmbMarca.Name = "cmbMarca";
            this.cmbMarca.Size = new System.Drawing.Size(246, 24);
            this.cmbMarca.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Marca";
            // 
            // txtPatente
            // 
            this.txtPatente.Location = new System.Drawing.Point(143, 29);
            this.txtPatente.Name = "txtPatente";
            this.txtPatente.Size = new System.Drawing.Size(127, 23);
            this.txtPatente.TabIndex = 1;
            this.txtPatente.TextChanged += new System.EventHandler(this.txtPatente_TextChanged);
            // 
            // lblPatente
            // 
            this.lblPatente.Location = new System.Drawing.Point(26, 32);
            this.lblPatente.Name = "lblPatente";
            this.lblPatente.Size = new System.Drawing.Size(80, 30);
            this.lblPatente.TabIndex = 0;
            this.lblPatente.Text = "Patente";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(405, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 17);
            this.label6.TabIndex = 30;
            this.label6.Text = "Fecha";
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.SystemColors.Control;
            this.txtFecha.Location = new System.Drawing.Point(515, 122);
            this.txtFecha.Mask = "00/00/0000";
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(100, 23);
            this.txtFecha.TabIndex = 47;
            this.txtFecha.ValidatingType = typeof(System.DateTime);
            // 
            // FrmGastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(814, 451);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGastos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmGastos";
            this.Load += new System.EventHandler(this.FrmGastos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grilla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCodStock;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCodAuto;
        private System.Windows.Forms.ComboBox cmbCiudad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKms;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAnio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbMarca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPatente;
        private System.Windows.Forms.Label lblPatente;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox CmbCategoriaGasto;
        private System.Windows.Forms.Button btnEliminarGasto;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnAgregarGasto;
        private System.Windows.Forms.TextBox txtImporteGasto;
        private System.Windows.Forms.DataGridView Grilla;
        private System.Windows.Forms.Button btnAgregarCategoriaGasto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox txtFecha;
    }
}