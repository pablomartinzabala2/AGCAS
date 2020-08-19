namespace Concesionaria
{
    partial class FrmCobroPrenda
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
            this.label7 = new System.Windows.Forms.Label();
            this.txtDiferencia = new System.Windows.Forms.TextBox();
            this.txtCodVenta = new System.Windows.Forms.TextBox();
            this.txtImporteaPagar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAnular = new System.Windows.Forms.Button();
            this.txtFecha = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.Grilla = new System.Windows.Forms.DataGridView();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtPatente = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtImporteCheque = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.txtCheque = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.BtnAgregarCheque = new System.Windows.Forms.Button();
            this.CmbBanco = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.txtFechaVencimiento = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.GrillaCheques = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalCheque = new System.Windows.Forms.TextBox();
            this.txtCodCliente = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grilla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaCheques)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCodCliente);
            this.groupBox1.Controls.Add(this.txtTotalCheque);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.GrillaCheques);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.CmbBanco);
            this.groupBox1.Controls.Add(this.label49);
            this.groupBox1.Controls.Add(this.label48);
            this.groupBox1.Controls.Add(this.txtFechaVencimiento);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.BtnAgregarCheque);
            this.groupBox1.Controls.Add(this.txtImporteCheque);
            this.groupBox1.Controls.Add(this.label47);
            this.groupBox1.Controls.Add(this.txtCheque);
            this.groupBox1.Controls.Add(this.label46);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDiferencia);
            this.groupBox1.Controls.Add(this.txtCodVenta);
            this.groupBox1.Controls.Add(this.txtImporteaPagar);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtImporte);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnAnular);
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnGrabar);
            this.groupBox1.Controls.Add(this.Grilla);
            this.groupBox1.Controls.Add(this.txtApellido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtPatente);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 470);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de la prenda";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(362, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 17);
            this.label7.TabIndex = 54;
            this.label7.Text = "Diferencia";
            // 
            // txtDiferencia
            // 
            this.txtDiferencia.BackColor = System.Drawing.SystemColors.Control;
            this.txtDiferencia.Location = new System.Drawing.Point(477, 122);
            this.txtDiferencia.Name = "txtDiferencia";
            this.txtDiferencia.ReadOnly = true;
            this.txtDiferencia.Size = new System.Drawing.Size(207, 23);
            this.txtDiferencia.TabIndex = 53;
            // 
            // txtCodVenta
            // 
            this.txtCodVenta.BackColor = System.Drawing.SystemColors.Control;
            this.txtCodVenta.Location = new System.Drawing.Point(130, 130);
            this.txtCodVenta.Name = "txtCodVenta";
            this.txtCodVenta.Size = new System.Drawing.Size(161, 23);
            this.txtCodVenta.TabIndex = 52;
            this.txtCodVenta.Visible = false;
            // 
            // txtImporteaPagar
            // 
            this.txtImporteaPagar.BackColor = System.Drawing.SystemColors.Control;
            this.txtImporteaPagar.Location = new System.Drawing.Point(477, 151);
            this.txtImporteaPagar.Name = "txtImporteaPagar";
            this.txtImporteaPagar.Size = new System.Drawing.Size(207, 23);
            this.txtImporteaPagar.TabIndex = 51;
            this.txtImporteaPagar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImporteaPagar_KeyPress_1);
            this.txtImporteaPagar.Leave += new System.EventHandler(this.txtImporteaPagar_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 17);
            this.label6.TabIndex = 50;
            this.label6.Text = "Importe a Pagar";
            // 
            // txtImporte
            // 
            this.txtImporte.BackColor = System.Drawing.SystemColors.Control;
            this.txtImporte.Location = new System.Drawing.Point(477, 93);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.ReadOnly = true;
            this.txtImporte.Size = new System.Drawing.Size(207, 23);
            this.txtImporte.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(365, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 17);
            this.label5.TabIndex = 48;
            this.label5.Text = "Importe";
            // 
            // btnAnular
            // 
            this.btnAnular.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnular.Location = new System.Drawing.Point(297, 407);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(75, 36);
            this.btnAnular.TabIndex = 47;
            this.btnAnular.Text = "Anular";
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.SystemColors.Control;
            this.txtFecha.Location = new System.Drawing.Point(130, 101);
            this.txtFecha.Mask = "00/00/0000";
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(66, 23);
            this.txtFecha.TabIndex = 46;
            this.txtFecha.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 45;
            this.label4.Text = "Fecha";
            // 
            // btnGrabar
            // 
            this.btnGrabar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrabar.Location = new System.Drawing.Point(368, 407);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 36);
            this.btnGrabar.TabIndex = 44;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // Grilla
            // 
            this.Grilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grilla.Location = new System.Drawing.Point(22, 130);
            this.Grilla.Name = "Grilla";
            this.Grilla.ReadOnly = true;
            this.Grilla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grilla.Size = new System.Drawing.Size(79, 26);
            this.Grilla.TabIndex = 43;
            this.Grilla.Visible = false;
            // 
            // txtApellido
            // 
            this.txtApellido.BackColor = System.Drawing.SystemColors.Control;
            this.txtApellido.Location = new System.Drawing.Point(477, 64);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.ReadOnly = true;
            this.txtApellido.Size = new System.Drawing.Size(207, 23);
            this.txtApellido.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 41;
            this.label3.Text = "Apellido";
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.SystemColors.Control;
            this.txtNombre.Location = new System.Drawing.Point(477, 35);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(207, 23);
            this.txtNombre.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(362, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 39;
            this.label2.Text = "Nombre";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescripcion.Location = new System.Drawing.Point(130, 68);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(207, 23);
            this.txtDescripcion.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 37;
            this.label1.Text = "Descripción";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Enabled = false;
            this.btnBuscar.Image = global::Concesionaria.Properties.Resources.zoom2;
            this.btnBuscar.Location = new System.Drawing.Point(297, 33);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 28);
            this.btnBuscar.TabIndex = 36;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtPatente
            // 
            this.txtPatente.BackColor = System.Drawing.SystemColors.Control;
            this.txtPatente.Location = new System.Drawing.Point(130, 33);
            this.txtPatente.Name = "txtPatente";
            this.txtPatente.ReadOnly = true;
            this.txtPatente.Size = new System.Drawing.Size(161, 23);
            this.txtPatente.TabIndex = 35;
            this.txtPatente.TextChanged += new System.EventHandler(this.txtPatente_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(19, 35);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 17);
            this.label16.TabIndex = 34;
            this.label16.Text = "Patente";
            // 
            // txtImporteCheque
            // 
            this.txtImporteCheque.BackColor = System.Drawing.SystemColors.Control;
            this.txtImporteCheque.Location = new System.Drawing.Point(87, 216);
            this.txtImporteCheque.Name = "txtImporteCheque";
            this.txtImporteCheque.Size = new System.Drawing.Size(170, 23);
            this.txtImporteCheque.TabIndex = 58;
            this.txtImporteCheque.Leave += new System.EventHandler(this.txtImporteCheque_Leave);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(-139, 213);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(55, 17);
            this.label47.TabIndex = 57;
            this.label47.Text = "Importe";
            // 
            // txtCheque
            // 
            this.txtCheque.Location = new System.Drawing.Point(373, 216);
            this.txtCheque.Name = "txtCheque";
            this.txtCheque.Size = new System.Drawing.Size(147, 23);
            this.txtCheque.TabIndex = 56;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(263, 216);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(109, 17);
            this.label46.TabIndex = 55;
            this.label46.Text = "Número cheque";
            // 
            // button2
            // 
            this.button2.Image = global::Concesionaria.Properties.Resources.cancel;
            this.button2.Location = new System.Drawing.Point(577, 243);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 28);
            this.button2.TabIndex = 60;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BtnAgregarCheque
            // 
            this.BtnAgregarCheque.Image = global::Concesionaria.Properties.Resources.add;
            this.BtnAgregarCheque.Location = new System.Drawing.Point(540, 243);
            this.BtnAgregarCheque.Name = "BtnAgregarCheque";
            this.BtnAgregarCheque.Size = new System.Drawing.Size(40, 28);
            this.BtnAgregarCheque.TabIndex = 59;
            this.BtnAgregarCheque.UseVisualStyleBackColor = true;
            this.BtnAgregarCheque.Click += new System.EventHandler(this.BtnAgregarCheque_Click);
            // 
            // CmbBanco
            // 
            this.CmbBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbBanco.FormattingEnabled = true;
            this.CmbBanco.Location = new System.Drawing.Point(87, 247);
            this.CmbBanco.Name = "CmbBanco";
            this.CmbBanco.Size = new System.Drawing.Size(246, 24);
            this.CmbBanco.TabIndex = 64;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(6, 249);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(48, 17);
            this.label49.TabIndex = 63;
            this.label49.Text = "Banco";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(365, 247);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(85, 17);
            this.label48.TabIndex = 62;
            this.label48.Text = "Vencimiento";
            // 
            // txtFechaVencimiento
            // 
            this.txtFechaVencimiento.BackColor = System.Drawing.SystemColors.Control;
            this.txtFechaVencimiento.Location = new System.Drawing.Point(456, 248);
            this.txtFechaVencimiento.Mask = "00/00/0000";
            this.txtFechaVencimiento.Name = "txtFechaVencimiento";
            this.txtFechaVencimiento.Size = new System.Drawing.Size(78, 23);
            this.txtFechaVencimiento.TabIndex = 61;
            this.txtFechaVencimiento.ValidatingType = typeof(System.DateTime);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(-3, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 17);
            this.label8.TabIndex = 65;
            this.label8.Text = "Importe";
            // 
            // GrillaCheques
            // 
            this.GrillaCheques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaCheques.Location = new System.Drawing.Point(10, 277);
            this.GrillaCheques.Name = "GrillaCheques";
            this.GrillaCheques.ReadOnly = true;
            this.GrillaCheques.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GrillaCheques.Size = new System.Drawing.Size(674, 85);
            this.GrillaCheques.TabIndex = 66;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(443, 377);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 17);
            this.label9.TabIndex = 67;
            this.label9.Text = "TotalCheque";
            // 
            // txtTotalCheque
            // 
            this.txtTotalCheque.BackColor = System.Drawing.SystemColors.Control;
            this.txtTotalCheque.Location = new System.Drawing.Point(538, 374);
            this.txtTotalCheque.Name = "txtTotalCheque";
            this.txtTotalCheque.Size = new System.Drawing.Size(146, 23);
            this.txtTotalCheque.TabIndex = 68;
            // 
            // txtCodCliente
            // 
            this.txtCodCliente.BackColor = System.Drawing.SystemColors.Control;
            this.txtCodCliente.Location = new System.Drawing.Point(130, 159);
            this.txtCodCliente.Name = "txtCodCliente";
            this.txtCodCliente.Size = new System.Drawing.Size(66, 23);
            this.txtCodCliente.TabIndex = 69;
            this.txtCodCliente.Visible = false;
            // 
            // FrmCobroPrenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(744, 467);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCobroPrenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cobro de prenda";
            this.Load += new System.EventHandler(this.FrmCobroPrenda_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grilla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaCheques)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtPatente;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox txtFecha;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView Grilla;
        private System.Windows.Forms.TextBox txtImporteaPagar;
        private System.Windows.Forms.TextBox txtCodVenta;
        private System.Windows.Forms.TextBox txtDiferencia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtImporteCheque;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txtCheque;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button BtnAgregarCheque;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CmbBanco;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.MaskedTextBox txtFechaVencimiento;
        private System.Windows.Forms.DataGridView GrillaCheques;
        private System.Windows.Forms.TextBox txtTotalCheque;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCodCliente;
    }
}