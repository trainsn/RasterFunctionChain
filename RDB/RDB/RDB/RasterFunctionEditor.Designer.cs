namespace RDB
{
    partial class RasterFunctionEditor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miXML = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadXML = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportXML = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.miFinish = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clipFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slopeFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aspectFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hillshadeFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nDVIFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panSharpenFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convolutionFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.miApply = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miXML});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(400, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miXML
            // 
            this.miXML.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadXML,
            this.miExportXML});
            this.miXML.Name = "miXML";
            this.miXML.Size = new System.Drawing.Size(54, 24);
            this.miXML.Text = "XML";
            // 
            // miLoadXML
            // 
            this.miLoadXML.Name = "miLoadXML";
            this.miLoadXML.Size = new System.Drawing.Size(159, 24);
            this.miLoadXML.Text = "LoadXML";
            this.miLoadXML.Click += new System.EventHandler(this.miLoadXML_Click);
            // 
            // miExportXML
            // 
            this.miExportXML.Name = "miExportXML";
            this.miExportXML.Size = new System.Drawing.Size(159, 24);
            this.miExportXML.Text = "ExportXML";
            this.miExportXML.Click += new System.EventHandler(this.miExportXML_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFinish,
            this.miApply});
            this.menuStrip2.Location = new System.Drawing.Point(0, 415);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip2.Size = new System.Drawing.Size(400, 28);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // miFinish
            // 
            this.miFinish.Name = "miFinish";
            this.miFinish.Size = new System.Drawing.Size(62, 24);
            this.miFinish.Text = "Finish";
            this.miFinish.Click += new System.EventHandler(this.miFinish_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuItem,
            this.miDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(127, 52);
            // 
            // insertToolStripMenuItem
            // 
            this.insertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clipFunctionToolStripMenuItem,
            this.slopeFunctionToolStripMenuItem,
            this.aspectFunctionToolStripMenuItem,
            this.hillshadeFunctionToolStripMenuItem,
            this.stretchFunctionToolStripMenuItem,
            this.nDVIFunctionToolStripMenuItem,
            this.panSharpenFunctionToolStripMenuItem,
            this.convolutionFunctionToolStripMenuItem});
            this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            this.insertToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.insertToolStripMenuItem.Text = "Insert";
            // 
            // clipFunctionToolStripMenuItem
            // 
            this.clipFunctionToolStripMenuItem.Name = "clipFunctionToolStripMenuItem";
            this.clipFunctionToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.clipFunctionToolStripMenuItem.Text = "ClipFunction";
            this.clipFunctionToolStripMenuItem.Click += new System.EventHandler(this.clipFunctionToolStripMenuItem_Click);
            // 
            // slopeFunctionToolStripMenuItem
            // 
            this.slopeFunctionToolStripMenuItem.Name = "slopeFunctionToolStripMenuItem";
            this.slopeFunctionToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.slopeFunctionToolStripMenuItem.Text = "SlopeFunction";
            this.slopeFunctionToolStripMenuItem.Click += new System.EventHandler(this.slopeFunctionToolStripMenuItem_Click);
            // 
            // aspectFunctionToolStripMenuItem
            // 
            this.aspectFunctionToolStripMenuItem.Name = "aspectFunctionToolStripMenuItem";
            this.aspectFunctionToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.aspectFunctionToolStripMenuItem.Text = "AspectFunction";
            this.aspectFunctionToolStripMenuItem.Click += new System.EventHandler(this.aspectFunctionToolStripMenuItem_Click);
            // 
            // hillshadeFunctionToolStripMenuItem
            // 
            this.hillshadeFunctionToolStripMenuItem.Name = "hillshadeFunctionToolStripMenuItem";
            this.hillshadeFunctionToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.hillshadeFunctionToolStripMenuItem.Text = "HillshadeFunction";
            this.hillshadeFunctionToolStripMenuItem.Click += new System.EventHandler(this.hillshadeFunctionToolStripMenuItem_Click);
            // 
            // stretchFunctionToolStripMenuItem
            // 
            this.stretchFunctionToolStripMenuItem.Name = "stretchFunctionToolStripMenuItem";
            this.stretchFunctionToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.stretchFunctionToolStripMenuItem.Text = "StretchFunction";
            this.stretchFunctionToolStripMenuItem.Click += new System.EventHandler(this.stretchFunctionToolStripMenuItem_Click);
            // 
            // nDVIFunctionToolStripMenuItem
            // 
            this.nDVIFunctionToolStripMenuItem.Name = "nDVIFunctionToolStripMenuItem";
            this.nDVIFunctionToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.nDVIFunctionToolStripMenuItem.Text = "NDVIFunction";
            this.nDVIFunctionToolStripMenuItem.Click += new System.EventHandler(this.nDVIFunctionToolStripMenuItem_Click);
            // 
            // panSharpenFunctionToolStripMenuItem
            // 
            this.panSharpenFunctionToolStripMenuItem.Name = "panSharpenFunctionToolStripMenuItem";
            this.panSharpenFunctionToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.panSharpenFunctionToolStripMenuItem.Text = "PanSharpenFunction";
            this.panSharpenFunctionToolStripMenuItem.Click += new System.EventHandler(this.panSharpenFunctionToolStripMenuItem_Click);
            // 
            // convolutionFunctionToolStripMenuItem
            // 
            this.convolutionFunctionToolStripMenuItem.Name = "convolutionFunctionToolStripMenuItem";
            this.convolutionFunctionToolStripMenuItem.Size = new System.Drawing.Size(230, 24);
            this.convolutionFunctionToolStripMenuItem.Text = "ConvolutionFunction";
            this.convolutionFunctionToolStripMenuItem.Click += new System.EventHandler(this.convolutionFunctionToolStripMenuItem_Click);
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(126, 24);
            this.miDelete.Text = "Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(400, 387);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            // 
            // miApply
            // 
            this.miApply.Name = "miApply";
            this.miApply.Size = new System.Drawing.Size(64, 24);
            this.miApply.Text = "Apply";
            this.miApply.Click += new System.EventHandler(this.miApply_Click);
            // 
            // RasterFunctionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 443);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RasterFunctionEditor";
            this.Text = "RasterFunctionEditor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miXML;
        private System.Windows.Forms.ToolStripMenuItem miLoadXML;
        private System.Windows.Forms.ToolStripMenuItem miExportXML;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem miFinish;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clipFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slopeFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aspectFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hillshadeFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stretchFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nDVIFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panSharpenFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convolutionFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem miApply;
    }
}