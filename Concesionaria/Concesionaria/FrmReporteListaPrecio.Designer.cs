namespace Concesionaria
{
    partial class FrmReporteListaPrecio
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReporteAutoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CONCESIONARIADataSet = new Concesionaria.CONCESIONARIADataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReporteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ReporteTableAdapter = new Concesionaria.CONCESIONARIADataSetTableAdapters.ReporteTableAdapter();
            this.ReporteAutoTableAdapter = new Concesionaria.CONCESIONARIADataSetTableAdapters.ReporteAutoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteAutoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CONCESIONARIADataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReporteAutoBindingSource
            // 
            this.ReporteAutoBindingSource.DataMember = "ReporteAuto";
            this.ReporteAutoBindingSource.DataSource = this.CONCESIONARIADataSet;
            // 
            // CONCESIONARIADataSet
            // 
            this.CONCESIONARIADataSet.DataSetName = "CONCESIONARIADataSet";
            this.CONCESIONARIADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ReporteAutoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Concesionaria.ReporteListado.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(977, 649);
            this.reportViewer1.TabIndex = 0;
            // 
            // ReporteBindingSource
            // 
            this.ReporteBindingSource.DataMember = "Reporte";
            this.ReporteBindingSource.DataSource = this.CONCESIONARIADataSet;
            // 
            // ReporteTableAdapter
            // 
            this.ReporteTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteAutoTableAdapter
            // 
            this.ReporteAutoTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReporteListaPrecio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 673);
            this.Controls.Add(this.reportViewer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReporteListaPrecio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReporteListaPrecio";
            this.Load += new System.EventHandler(this.FrmReporteListaPrecio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReporteAutoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CONCESIONARIADataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ReporteBindingSource;
        private CONCESIONARIADataSet CONCESIONARIADataSet;
        private CONCESIONARIADataSetTableAdapters.ReporteTableAdapter ReporteTableAdapter;
        private System.Windows.Forms.BindingSource ReporteAutoBindingSource;
        private CONCESIONARIADataSetTableAdapters.ReporteAutoTableAdapter ReporteAutoTableAdapter;
    }
}