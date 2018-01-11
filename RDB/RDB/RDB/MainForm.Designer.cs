namespace RDB
{
    partial class MainForm
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
            //Ensures that any ESRI libraries that have been used are unloaded in the correct order. 
            //Failure to do this may result in random crashes on exit due to the operating system unloading 
            //the libraries in the incorrect order. 
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();

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
            System.Windows.Forms.TabControl tbc_ImageAnalysis;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbP_DataManagement = new System.Windows.Forms.TabPage();
            this.gb_ImportRasterImage = new System.Windows.Forms.GroupBox();
            this.btn_ImportRstDataset = new System.Windows.Forms.Button();
            this.txb_ImportRstDataset = new System.Windows.Forms.TextBox();
            this.cmb_ImportRstCatalog = new System.Windows.Forms.ComboBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.gb_LoadRasterImage = new System.Windows.Forms.GroupBox();
            this.btn_LoadRstDataset = new System.Windows.Forms.Button();
            this.cmb_LoadRstDataset = new System.Windows.Forms.ComboBox();
            this.cmb_LoadRstCatalog = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gb_CreateRasterCatalog = new System.Windows.Forms.GroupBox();
            this.btn_NewRstCatalog = new System.Windows.Forms.Button();
            this.txb_NewRstCatalog = new System.Windows.Forms.TextBox();
            this.tbp_ImageProcess = new System.Windows.Forms.TabPage();
            this.grb_RGBLayer = new System.Windows.Forms.GroupBox();
            this.btn_RGB = new System.Windows.Forms.Button();
            this.cmb_GBand = new System.Windows.Forms.ComboBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.cmb_BBand = new System.Windows.Forms.ComboBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.cmb_RBand = new System.Windows.Forms.ComboBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.cmb_RGBLayer = new System.Windows.Forms.ComboBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.grb_RenderLayer = new System.Windows.Forms.GroupBox();
            this.btn_Render = new System.Windows.Forms.Button();
            this.pb_ToColor = new System.Windows.Forms.PictureBox();
            this.pb_ColorBar = new System.Windows.Forms.PictureBox();
            this.pb_FromColor = new System.Windows.Forms.PictureBox();
            this.cmb_RenderBand = new System.Windows.Forms.ComboBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.cmb_RenderLayer = new System.Windows.Forms.ComboBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.grb_StretchLayer = new System.Windows.Forms.GroupBox();
            this.btn_Stretch = new System.Windows.Forms.Button();
            this.cmb_StretchMethod = new System.Windows.Forms.ComboBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.cmb_StretchBand = new System.Windows.Forms.ComboBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.cmb_StretchLayer = new System.Windows.Forms.ComboBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.grb_DrawHisLayer = new System.Windows.Forms.GroupBox();
            this.btn_MultiBandHis = new System.Windows.Forms.Button();
            this.btn_SingleBandHis = new System.Windows.Forms.Button();
            this.cmb_DrawHisBand = new System.Windows.Forms.ComboBox();
            this.cmb_DrawHisLayer = new System.Windows.Forms.ComboBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.grb_NDVILayer = new System.Windows.Forms.GroupBox();
            this.btn_NDVI_Bottom = new System.Windows.Forms.Button();
            this.btn_CalculateNDVI_ = new System.Windows.Forms.Button();
            this.cmb_NDVILayer = new System.Windows.Forms.ComboBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.grb_StatisticsLayer = new System.Windows.Forms.GroupBox();
            this.btn_Statistics_Bottom = new System.Windows.Forms.Button();
            this.btn_Statistics = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.cmb_StatisticsBand = new System.Windows.Forms.ComboBox();
            this.cmb_StatistiicsLayer = new System.Windows.Forms.ComboBox();
            this.tbp_ImageAnalysis = new System.Windows.Forms.TabPage();
            this.grb_Transform = new System.Windows.Forms.GroupBox();
            this.txb_TransformAngle = new System.Windows.Forms.TextBox();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.cmb_TransformMethod = new System.Windows.Forms.ComboBox();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.btn_Transform = new System.Windows.Forms.Button();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.cmb_TransformLayer = new System.Windows.Forms.ComboBox();
            this.grb_Filter = new System.Windows.Forms.GroupBox();
            this.cmb_FliterMethod = new System.Windows.Forms.ComboBox();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.btn_Filter = new System.Windows.Forms.Button();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.cmb_FliterLayer = new System.Windows.Forms.ComboBox();
            this.grb_Mosaic = new System.Windows.Forms.GroupBox();
            this.btn_Mosaic = new System.Windows.Forms.Button();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.cmb_MosaicCatalog = new System.Windows.Forms.ComboBox();
            this.grb_PanSharpen = new System.Windows.Forms.GroupBox();
            this.cmb_PanSharpenMultiLayer = new System.Windows.Forms.ComboBox();
            this.btn_PanSharpen = new System.Windows.Forms.Button();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.cmb_PanSharpenSigleLayer = new System.Windows.Forms.ComboBox();
            this.grb_ClipFeature = new System.Windows.Forms.GroupBox();
            this.btn_Clip = new System.Windows.Forms.Button();
            this.txb_ClipFeature = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.cmb_ClipFeatureLayer = new System.Windows.Forms.ComboBox();
            this.grb_ImageClass = new System.Windows.Forms.GroupBox();
            this.btn_After_Classify = new System.Windows.Forms.Button();
            this.btn_Classify = new System.Windows.Forms.Button();
            this.txb_ClassifyNumber = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.cmb_ClassifyLayer = new System.Windows.Forms.ComboBox();
            this.tbp_SpatialAnalysis = new System.Windows.Forms.TabPage();
            this.grb_contour_Voronoi = new System.Windows.Forms.GroupBox();
            this.btn_Voronoi = new System.Windows.Forms.Button();
            this.textBox40 = new System.Windows.Forms.TextBox();
            this.cmb_TinVoronoi = new System.Windows.Forms.ComboBox();
            this.btn_tinContour = new System.Windows.Forms.Button();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.cmb_tinContour = new System.Windows.Forms.ComboBox();
            this.btn_DEMContour = new System.Windows.Forms.Button();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.cmb_DEMContour = new System.Windows.Forms.ComboBox();
            this.grb_CreateTIN = new System.Windows.Forms.GroupBox();
            this.btn_CreateTINAuto = new System.Windows.Forms.Button();
            this.btm_CreateTin = new System.Windows.Forms.Button();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.cmb_CreateTINLayer = new System.Windows.Forms.ComboBox();
            this.grb_Extraction = new System.Windows.Forms.GroupBox();
            this.btn_Extraction = new System.Windows.Forms.Button();
            this.textBox39 = new System.Windows.Forms.TextBox();
            this.cmb_Extraction = new System.Windows.Forms.ComboBox();
            this.grb_Neighborhood = new System.Windows.Forms.GroupBox();
            this.btn_Neighborhood = new System.Windows.Forms.Button();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.cmb_NeighborhoodMethod = new System.Windows.Forms.ComboBox();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.cmb_NeighborhoodLayer = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Visibility = new System.Windows.Forms.Button();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.cmb_VisibilityLayer = new System.Windows.Forms.ComboBox();
            this.btn_LineOfSight = new System.Windows.Forms.Button();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.cmb_LineOfSightLayer = new System.Windows.Forms.ComboBox();
            this.grb_HillShade_Slope_Aspect = new System.Windows.Forms.GroupBox();
            this.btn_Aspect = new System.Windows.Forms.Button();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.cmb_Aspect = new System.Windows.Forms.ComboBox();
            this.btn_Slope = new System.Windows.Forms.Button();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.cmb_Slope = new System.Windows.Forms.ComboBox();
            this.btn_HillShade = new System.Windows.Forms.Button();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.cmb_HillShade = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_ConnectDB = new System.Windows.Forms.ToolStripMenuItem();
            this.miRasterFunctionEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarXY = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.colorDialog3 = new System.Windows.Forms.ColorDialog();
            this.colorDialog4 = new System.Windows.Forms.ColorDialog();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_ZoomToLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_DeleteLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditRasterFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl2 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            tbc_ImageAnalysis = new System.Windows.Forms.TabControl();
            tbc_ImageAnalysis.SuspendLayout();
            this.tbP_DataManagement.SuspendLayout();
            this.gb_ImportRasterImage.SuspendLayout();
            this.gb_LoadRasterImage.SuspendLayout();
            this.gb_CreateRasterCatalog.SuspendLayout();
            this.tbp_ImageProcess.SuspendLayout();
            this.grb_RGBLayer.SuspendLayout();
            this.grb_RenderLayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ToColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ColorBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_FromColor)).BeginInit();
            this.grb_StretchLayer.SuspendLayout();
            this.grb_DrawHisLayer.SuspendLayout();
            this.grb_NDVILayer.SuspendLayout();
            this.grb_StatisticsLayer.SuspendLayout();
            this.tbp_ImageAnalysis.SuspendLayout();
            this.grb_Transform.SuspendLayout();
            this.grb_Filter.SuspendLayout();
            this.grb_Mosaic.SuspendLayout();
            this.grb_PanSharpen.SuspendLayout();
            this.grb_ClipFeature.SuspendLayout();
            this.grb_ImageClass.SuspendLayout();
            this.tbp_SpatialAnalysis.SuspendLayout();
            this.grb_contour_Voronoi.SuspendLayout();
            this.grb_CreateTIN.SuspendLayout();
            this.grb_Extraction.SuspendLayout();
            this.grb_Neighborhood.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grb_HillShade_Slope_Aspect.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbc_ImageAnalysis
            // 
            tbc_ImageAnalysis.Controls.Add(this.tbP_DataManagement);
            tbc_ImageAnalysis.Controls.Add(this.tbp_ImageProcess);
            tbc_ImageAnalysis.Controls.Add(this.tbp_ImageAnalysis);
            tbc_ImageAnalysis.Controls.Add(this.tbp_SpatialAnalysis);
            tbc_ImageAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            tbc_ImageAnalysis.Location = new System.Drawing.Point(0, 0);
            tbc_ImageAnalysis.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            tbc_ImageAnalysis.Multiline = true;
            tbc_ImageAnalysis.Name = "tbc_ImageAnalysis";
            tbc_ImageAnalysis.SelectedIndex = 0;
            tbc_ImageAnalysis.Size = new System.Drawing.Size(290, 560);
            tbc_ImageAnalysis.TabIndex = 8;
            tbc_ImageAnalysis.Tag = "图像分析";
            // 
            // tbP_DataManagement
            // 
            this.tbP_DataManagement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbP_DataManagement.Controls.Add(this.gb_ImportRasterImage);
            this.tbP_DataManagement.Controls.Add(this.gb_LoadRasterImage);
            this.tbP_DataManagement.Controls.Add(this.gb_CreateRasterCatalog);
            this.tbP_DataManagement.Location = new System.Drawing.Point(4, 46);
            this.tbP_DataManagement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbP_DataManagement.Name = "tbP_DataManagement";
            this.tbP_DataManagement.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbP_DataManagement.Size = new System.Drawing.Size(282, 510);
            this.tbP_DataManagement.TabIndex = 0;
            this.tbP_DataManagement.Text = "数据管理";
            this.tbP_DataManagement.UseVisualStyleBackColor = true;
            // 
            // gb_ImportRasterImage
            // 
            this.gb_ImportRasterImage.Controls.Add(this.btn_ImportRstDataset);
            this.gb_ImportRasterImage.Controls.Add(this.txb_ImportRstDataset);
            this.gb_ImportRasterImage.Controls.Add(this.cmb_ImportRstCatalog);
            this.gb_ImportRasterImage.Controls.Add(this.textBox4);
            this.gb_ImportRasterImage.Controls.Add(this.textBox3);
            this.gb_ImportRasterImage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.gb_ImportRasterImage.Location = new System.Drawing.Point(5, 299);
            this.gb_ImportRasterImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_ImportRasterImage.Name = "gb_ImportRasterImage";
            this.gb_ImportRasterImage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_ImportRasterImage.Size = new System.Drawing.Size(313, 128);
            this.gb_ImportRasterImage.TabIndex = 2;
            this.gb_ImportRasterImage.TabStop = false;
            this.gb_ImportRasterImage.Text = "导入栅格图像";
            // 
            // btn_ImportRstDataset
            // 
            this.btn_ImportRstDataset.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btn_ImportRstDataset.Location = new System.Drawing.Point(135, 100);
            this.btn_ImportRstDataset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ImportRstDataset.Name = "btn_ImportRstDataset";
            this.btn_ImportRstDataset.Size = new System.Drawing.Size(80, 25);
            this.btn_ImportRstDataset.TabIndex = 12;
            this.btn_ImportRstDataset.Text = "导入";
            this.btn_ImportRstDataset.UseVisualStyleBackColor = true;
            this.btn_ImportRstDataset.Click += new System.EventHandler(this.btn_ImportRstDataset_Click);
            // 
            // txb_ImportRstDataset
            // 
            this.txb_ImportRstDataset.Location = new System.Drawing.Point(96, 65);
            this.txb_ImportRstDataset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_ImportRstDataset.Name = "txb_ImportRstDataset";
            this.txb_ImportRstDataset.Size = new System.Drawing.Size(201, 25);
            this.txb_ImportRstDataset.TabIndex = 3;
            this.txb_ImportRstDataset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txb_ImportRstDataset_MouseDown);
            // 
            // cmb_ImportRstCatalog
            // 
            this.cmb_ImportRstCatalog.FormattingEnabled = true;
            this.cmb_ImportRstCatalog.Location = new System.Drawing.Point(96, 32);
            this.cmb_ImportRstCatalog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_ImportRstCatalog.Name = "cmb_ImportRstCatalog";
            this.cmb_ImportRstCatalog.Size = new System.Drawing.Size(201, 23);
            this.cmb_ImportRstCatalog.TabIndex = 11;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Window;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(11, 65);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(72, 18);
            this.textBox4.TabIndex = 3;
            this.textBox4.Text = "栅格图像";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Window;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(11, 32);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(72, 18);
            this.textBox3.TabIndex = 2;
            this.textBox3.Text = "栅格目录";
            // 
            // gb_LoadRasterImage
            // 
            this.gb_LoadRasterImage.Controls.Add(this.btn_LoadRstDataset);
            this.gb_LoadRasterImage.Controls.Add(this.cmb_LoadRstDataset);
            this.gb_LoadRasterImage.Controls.Add(this.cmb_LoadRstCatalog);
            this.gb_LoadRasterImage.Controls.Add(this.textBox2);
            this.gb_LoadRasterImage.Controls.Add(this.textBox1);
            this.gb_LoadRasterImage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.gb_LoadRasterImage.Location = new System.Drawing.Point(5, 148);
            this.gb_LoadRasterImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_LoadRasterImage.Name = "gb_LoadRasterImage";
            this.gb_LoadRasterImage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_LoadRasterImage.Size = new System.Drawing.Size(313, 134);
            this.gb_LoadRasterImage.TabIndex = 1;
            this.gb_LoadRasterImage.TabStop = false;
            this.gb_LoadRasterImage.Text = "加载栅格图像";
            // 
            // btn_LoadRstDataset
            // 
            this.btn_LoadRstDataset.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btn_LoadRstDataset.Location = new System.Drawing.Point(135, 92);
            this.btn_LoadRstDataset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_LoadRstDataset.Name = "btn_LoadRstDataset";
            this.btn_LoadRstDataset.Size = new System.Drawing.Size(80, 26);
            this.btn_LoadRstDataset.TabIndex = 12;
            this.btn_LoadRstDataset.Text = "加载";
            this.btn_LoadRstDataset.UseVisualStyleBackColor = true;
            this.btn_LoadRstDataset.Click += new System.EventHandler(this.btn_LoadRstDataset_Click);
            // 
            // cmb_LoadRstDataset
            // 
            this.cmb_LoadRstDataset.FormattingEnabled = true;
            this.cmb_LoadRstDataset.Location = new System.Drawing.Point(96, 62);
            this.cmb_LoadRstDataset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_LoadRstDataset.Name = "cmb_LoadRstDataset";
            this.cmb_LoadRstDataset.Size = new System.Drawing.Size(201, 23);
            this.cmb_LoadRstDataset.TabIndex = 10;
            // 
            // cmb_LoadRstCatalog
            // 
            this.cmb_LoadRstCatalog.FormattingEnabled = true;
            this.cmb_LoadRstCatalog.Location = new System.Drawing.Point(96, 30);
            this.cmb_LoadRstCatalog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_LoadRstCatalog.Name = "cmb_LoadRstCatalog";
            this.cmb_LoadRstCatalog.Size = new System.Drawing.Size(201, 23);
            this.cmb_LoadRstCatalog.TabIndex = 9;
            this.cmb_LoadRstCatalog.SelectedIndexChanged += new System.EventHandler(this.cmb_LoadRstCatalog_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Window;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(11, 62);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(72, 18);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "栅格图像";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(11, 32);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(72, 18);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "栅格目录";
            // 
            // gb_CreateRasterCatalog
            // 
            this.gb_CreateRasterCatalog.Controls.Add(this.btn_NewRstCatalog);
            this.gb_CreateRasterCatalog.Controls.Add(this.txb_NewRstCatalog);
            this.gb_CreateRasterCatalog.ForeColor = System.Drawing.SystemColors.Highlight;
            this.gb_CreateRasterCatalog.Location = new System.Drawing.Point(5, 12);
            this.gb_CreateRasterCatalog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_CreateRasterCatalog.Name = "gb_CreateRasterCatalog";
            this.gb_CreateRasterCatalog.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gb_CreateRasterCatalog.Size = new System.Drawing.Size(313, 111);
            this.gb_CreateRasterCatalog.TabIndex = 0;
            this.gb_CreateRasterCatalog.TabStop = false;
            this.gb_CreateRasterCatalog.Text = "创建栅格目录";
            // 
            // btn_NewRstCatalog
            // 
            this.btn_NewRstCatalog.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btn_NewRstCatalog.Location = new System.Drawing.Point(205, 44);
            this.btn_NewRstCatalog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_NewRstCatalog.Name = "btn_NewRstCatalog";
            this.btn_NewRstCatalog.Size = new System.Drawing.Size(69, 26);
            this.btn_NewRstCatalog.TabIndex = 1;
            this.btn_NewRstCatalog.Text = "创建";
            this.btn_NewRstCatalog.UseVisualStyleBackColor = true;
            this.btn_NewRstCatalog.Click += new System.EventHandler(this.btn_NewRstCatalog_Click);
            // 
            // txb_NewRstCatalog
            // 
            this.txb_NewRstCatalog.Location = new System.Drawing.Point(11, 48);
            this.txb_NewRstCatalog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_NewRstCatalog.Name = "txb_NewRstCatalog";
            this.txb_NewRstCatalog.Size = new System.Drawing.Size(171, 25);
            this.txb_NewRstCatalog.TabIndex = 0;
            // 
            // tbp_ImageProcess
            // 
            this.tbp_ImageProcess.Controls.Add(this.grb_RGBLayer);
            this.tbp_ImageProcess.Controls.Add(this.grb_RenderLayer);
            this.tbp_ImageProcess.Controls.Add(this.grb_StretchLayer);
            this.tbp_ImageProcess.Controls.Add(this.grb_DrawHisLayer);
            this.tbp_ImageProcess.Controls.Add(this.grb_NDVILayer);
            this.tbp_ImageProcess.Controls.Add(this.grb_StatisticsLayer);
            this.tbp_ImageProcess.Location = new System.Drawing.Point(4, 25);
            this.tbp_ImageProcess.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbp_ImageProcess.Name = "tbp_ImageProcess";
            this.tbp_ImageProcess.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbp_ImageProcess.Size = new System.Drawing.Size(353, 643);
            this.tbp_ImageProcess.TabIndex = 1;
            this.tbp_ImageProcess.Text = "图像处理";
            this.tbp_ImageProcess.UseVisualStyleBackColor = true;
            // 
            // grb_RGBLayer
            // 
            this.grb_RGBLayer.Controls.Add(this.btn_RGB);
            this.grb_RGBLayer.Controls.Add(this.cmb_GBand);
            this.grb_RGBLayer.Controls.Add(this.textBox18);
            this.grb_RGBLayer.Controls.Add(this.cmb_BBand);
            this.grb_RGBLayer.Controls.Add(this.textBox17);
            this.grb_RGBLayer.Controls.Add(this.cmb_RBand);
            this.grb_RGBLayer.Controls.Add(this.textBox16);
            this.grb_RGBLayer.Controls.Add(this.cmb_RGBLayer);
            this.grb_RGBLayer.Controls.Add(this.textBox15);
            this.grb_RGBLayer.Location = new System.Drawing.Point(7, 464);
            this.grb_RGBLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_RGBLayer.Name = "grb_RGBLayer";
            this.grb_RGBLayer.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_RGBLayer.Size = new System.Drawing.Size(312, 85);
            this.grb_RGBLayer.TabIndex = 5;
            this.grb_RGBLayer.TabStop = false;
            this.grb_RGBLayer.Text = "多波段假彩色合成";
            // 
            // btn_RGB
            // 
            this.btn_RGB.Location = new System.Drawing.Point(216, 20);
            this.btn_RGB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_RGB.Name = "btn_RGB";
            this.btn_RGB.Size = new System.Drawing.Size(69, 24);
            this.btn_RGB.TabIndex = 16;
            this.btn_RGB.Text = "合成";
            this.btn_RGB.UseVisualStyleBackColor = true;
            this.btn_RGB.Click += new System.EventHandler(this.btn_RGB_Click);
            // 
            // cmb_GBand
            // 
            this.cmb_GBand.FormattingEnabled = true;
            this.cmb_GBand.Location = new System.Drawing.Point(108, 54);
            this.cmb_GBand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_GBand.Name = "cmb_GBand";
            this.cmb_GBand.Size = new System.Drawing.Size(47, 23);
            this.cmb_GBand.TabIndex = 20;
            // 
            // textBox18
            // 
            this.textBox18.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox18.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox18.Location = new System.Drawing.Point(88, 56);
            this.textBox18.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new System.Drawing.Size(16, 18);
            this.textBox18.TabIndex = 19;
            this.textBox18.Text = "G";
            // 
            // cmb_BBand
            // 
            this.cmb_BBand.FormattingEnabled = true;
            this.cmb_BBand.Location = new System.Drawing.Point(184, 52);
            this.cmb_BBand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_BBand.Name = "cmb_BBand";
            this.cmb_BBand.Size = new System.Drawing.Size(47, 23);
            this.cmb_BBand.TabIndex = 18;
            // 
            // textBox17
            // 
            this.textBox17.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox17.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox17.Location = new System.Drawing.Point(161, 58);
            this.textBox17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox17.Name = "textBox17";
            this.textBox17.ReadOnly = true;
            this.textBox17.Size = new System.Drawing.Size(16, 18);
            this.textBox17.TabIndex = 17;
            this.textBox17.Text = "B";
            // 
            // cmb_RBand
            // 
            this.cmb_RBand.FormattingEnabled = true;
            this.cmb_RBand.Location = new System.Drawing.Point(27, 55);
            this.cmb_RBand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_RBand.Name = "cmb_RBand";
            this.cmb_RBand.Size = new System.Drawing.Size(47, 23);
            this.cmb_RBand.TabIndex = 16;
            // 
            // textBox16
            // 
            this.textBox16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox16.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox16.Location = new System.Drawing.Point(8, 56);
            this.textBox16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new System.Drawing.Size(16, 18);
            this.textBox16.TabIndex = 15;
            this.textBox16.Text = "R";
            // 
            // cmb_RGBLayer
            // 
            this.cmb_RGBLayer.FormattingEnabled = true;
            this.cmb_RGBLayer.Location = new System.Drawing.Point(56, 25);
            this.cmb_RGBLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_RGBLayer.Name = "cmb_RGBLayer";
            this.cmb_RGBLayer.Size = new System.Drawing.Size(115, 23);
            this.cmb_RGBLayer.TabIndex = 11;
            this.cmb_RGBLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_RGBLayer_SelectedIndexChanged);
            // 
            // textBox15
            // 
            this.textBox15.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox15.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox15.Location = new System.Drawing.Point(8, 28);
            this.textBox15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new System.Drawing.Size(37, 18);
            this.textBox15.TabIndex = 14;
            this.textBox15.Text = "图层";
            // 
            // grb_RenderLayer
            // 
            this.grb_RenderLayer.Controls.Add(this.btn_Render);
            this.grb_RenderLayer.Controls.Add(this.pb_ToColor);
            this.grb_RenderLayer.Controls.Add(this.pb_ColorBar);
            this.grb_RenderLayer.Controls.Add(this.pb_FromColor);
            this.grb_RenderLayer.Controls.Add(this.cmb_RenderBand);
            this.grb_RenderLayer.Controls.Add(this.textBox14);
            this.grb_RenderLayer.Controls.Add(this.cmb_RenderLayer);
            this.grb_RenderLayer.Controls.Add(this.textBox13);
            this.grb_RenderLayer.Location = new System.Drawing.Point(5, 370);
            this.grb_RenderLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_RenderLayer.Name = "grb_RenderLayer";
            this.grb_RenderLayer.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_RenderLayer.Size = new System.Drawing.Size(315, 90);
            this.grb_RenderLayer.TabIndex = 4;
            this.grb_RenderLayer.TabStop = false;
            this.grb_RenderLayer.Text = "单波段伪彩色渲染";
            // 
            // btn_Render
            // 
            this.btn_Render.Location = new System.Drawing.Point(217, 50);
            this.btn_Render.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Render.Name = "btn_Render";
            this.btn_Render.Size = new System.Drawing.Size(69, 25);
            this.btn_Render.TabIndex = 6;
            this.btn_Render.Text = "渲染";
            this.btn_Render.UseVisualStyleBackColor = true;
            this.btn_Render.Click += new System.EventHandler(this.btn_Render_Click);
            // 
            // pb_ToColor
            // 
            this.pb_ToColor.Location = new System.Drawing.Point(152, 50);
            this.pb_ToColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_ToColor.Name = "pb_ToColor";
            this.pb_ToColor.Size = new System.Drawing.Size(41, 25);
            this.pb_ToColor.TabIndex = 15;
            this.pb_ToColor.TabStop = false;
            this.pb_ToColor.Click += new System.EventHandler(this.pb_ToColor_Click);
            // 
            // pb_ColorBar
            // 
            this.pb_ColorBar.Location = new System.Drawing.Point(51, 50);
            this.pb_ColorBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_ColorBar.Name = "pb_ColorBar";
            this.pb_ColorBar.Size = new System.Drawing.Size(96, 25);
            this.pb_ColorBar.TabIndex = 14;
            this.pb_ColorBar.TabStop = false;
            // 
            // pb_FromColor
            // 
            this.pb_FromColor.Location = new System.Drawing.Point(9, 50);
            this.pb_FromColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_FromColor.Name = "pb_FromColor";
            this.pb_FromColor.Size = new System.Drawing.Size(37, 25);
            this.pb_FromColor.TabIndex = 9;
            this.pb_FromColor.TabStop = false;
            this.pb_FromColor.Click += new System.EventHandler(this.pb_FromColor_Click);
            // 
            // cmb_RenderBand
            // 
            this.cmb_RenderBand.FormattingEnabled = true;
            this.cmb_RenderBand.Location = new System.Drawing.Point(205, 19);
            this.cmb_RenderBand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_RenderBand.Name = "cmb_RenderBand";
            this.cmb_RenderBand.Size = new System.Drawing.Size(63, 23);
            this.cmb_RenderBand.TabIndex = 13;
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox14.Location = new System.Drawing.Point(163, 22);
            this.textBox14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(37, 18);
            this.textBox14.TabIndex = 12;
            this.textBox14.Text = "波段";
            // 
            // cmb_RenderLayer
            // 
            this.cmb_RenderLayer.FormattingEnabled = true;
            this.cmb_RenderLayer.Location = new System.Drawing.Point(59, 20);
            this.cmb_RenderLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_RenderLayer.Name = "cmb_RenderLayer";
            this.cmb_RenderLayer.Size = new System.Drawing.Size(63, 23);
            this.cmb_RenderLayer.TabIndex = 11;
            this.cmb_RenderLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_RenderLayer_SelectedIndexChanged);
            // 
            // textBox13
            // 
            this.textBox13.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox13.Location = new System.Drawing.Point(9, 21);
            this.textBox13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(37, 18);
            this.textBox13.TabIndex = 11;
            this.textBox13.Text = "图层";
            // 
            // grb_StretchLayer
            // 
            this.grb_StretchLayer.Controls.Add(this.btn_Stretch);
            this.grb_StretchLayer.Controls.Add(this.cmb_StretchMethod);
            this.grb_StretchLayer.Controls.Add(this.textBox12);
            this.grb_StretchLayer.Controls.Add(this.cmb_StretchBand);
            this.grb_StretchLayer.Controls.Add(this.textBox11);
            this.grb_StretchLayer.Controls.Add(this.cmb_StretchLayer);
            this.grb_StretchLayer.Controls.Add(this.textBox10);
            this.grb_StretchLayer.Location = new System.Drawing.Point(5, 252);
            this.grb_StretchLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_StretchLayer.Name = "grb_StretchLayer";
            this.grb_StretchLayer.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_StretchLayer.Size = new System.Drawing.Size(315, 114);
            this.grb_StretchLayer.TabIndex = 3;
            this.grb_StretchLayer.TabStop = false;
            this.grb_StretchLayer.Text = "单波段灰度增强";
            // 
            // btn_Stretch
            // 
            this.btn_Stretch.Location = new System.Drawing.Point(219, 50);
            this.btn_Stretch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Stretch.Name = "btn_Stretch";
            this.btn_Stretch.Size = new System.Drawing.Size(69, 25);
            this.btn_Stretch.TabIndex = 8;
            this.btn_Stretch.Text = "增强";
            this.btn_Stretch.UseVisualStyleBackColor = true;
            this.btn_Stretch.Click += new System.EventHandler(this.btn_Stretch_Click);
            // 
            // cmb_StretchMethod
            // 
            this.cmb_StretchMethod.FormattingEnabled = true;
            this.cmb_StretchMethod.Location = new System.Drawing.Point(59, 82);
            this.cmb_StretchMethod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_StretchMethod.Name = "cmb_StretchMethod";
            this.cmb_StretchMethod.Size = new System.Drawing.Size(115, 23);
            this.cmb_StretchMethod.TabIndex = 10;
            // 
            // textBox12
            // 
            this.textBox12.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox12.Location = new System.Drawing.Point(11, 84);
            this.textBox12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(37, 18);
            this.textBox12.TabIndex = 9;
            this.textBox12.Text = "方法";
            // 
            // cmb_StretchBand
            // 
            this.cmb_StretchBand.FormattingEnabled = true;
            this.cmb_StretchBand.Location = new System.Drawing.Point(59, 54);
            this.cmb_StretchBand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_StretchBand.Name = "cmb_StretchBand";
            this.cmb_StretchBand.Size = new System.Drawing.Size(115, 23);
            this.cmb_StretchBand.TabIndex = 8;
            // 
            // textBox11
            // 
            this.textBox11.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox11.Location = new System.Drawing.Point(11, 56);
            this.textBox11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(37, 18);
            this.textBox11.TabIndex = 7;
            this.textBox11.Text = "波段";
            // 
            // cmb_StretchLayer
            // 
            this.cmb_StretchLayer.FormattingEnabled = true;
            this.cmb_StretchLayer.Location = new System.Drawing.Point(59, 26);
            this.cmb_StretchLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_StretchLayer.Name = "cmb_StretchLayer";
            this.cmb_StretchLayer.Size = new System.Drawing.Size(115, 23);
            this.cmb_StretchLayer.TabIndex = 6;
            this.cmb_StretchLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_StretchLayer_SelectedIndexChanged);
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Location = new System.Drawing.Point(11, 29);
            this.textBox10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(37, 18);
            this.textBox10.TabIndex = 6;
            this.textBox10.Text = "图层";
            // 
            // grb_DrawHisLayer
            // 
            this.grb_DrawHisLayer.Controls.Add(this.btn_MultiBandHis);
            this.grb_DrawHisLayer.Controls.Add(this.btn_SingleBandHis);
            this.grb_DrawHisLayer.Controls.Add(this.cmb_DrawHisBand);
            this.grb_DrawHisLayer.Controls.Add(this.cmb_DrawHisLayer);
            this.grb_DrawHisLayer.Controls.Add(this.textBox9);
            this.grb_DrawHisLayer.Controls.Add(this.textBox8);
            this.grb_DrawHisLayer.Location = new System.Drawing.Point(5, 169);
            this.grb_DrawHisLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_DrawHisLayer.Name = "grb_DrawHisLayer";
            this.grb_DrawHisLayer.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_DrawHisLayer.Size = new System.Drawing.Size(315, 80);
            this.grb_DrawHisLayer.TabIndex = 2;
            this.grb_DrawHisLayer.TabStop = false;
            this.grb_DrawHisLayer.Text = "直方图绘制";
            // 
            // btn_MultiBandHis
            // 
            this.btn_MultiBandHis.Location = new System.Drawing.Point(219, 49);
            this.btn_MultiBandHis.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_MultiBandHis.Name = "btn_MultiBandHis";
            this.btn_MultiBandHis.Size = new System.Drawing.Size(69, 25);
            this.btn_MultiBandHis.TabIndex = 7;
            this.btn_MultiBandHis.Text = "多波段";
            this.btn_MultiBandHis.UseVisualStyleBackColor = true;
            this.btn_MultiBandHis.Click += new System.EventHandler(this.btn_MultiBandHis_Click);
            // 
            // btn_SingleBandHis
            // 
            this.btn_SingleBandHis.Location = new System.Drawing.Point(219, 20);
            this.btn_SingleBandHis.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_SingleBandHis.Name = "btn_SingleBandHis";
            this.btn_SingleBandHis.Size = new System.Drawing.Size(69, 22);
            this.btn_SingleBandHis.TabIndex = 6;
            this.btn_SingleBandHis.Text = "单波段";
            this.btn_SingleBandHis.UseVisualStyleBackColor = true;
            this.btn_SingleBandHis.Click += new System.EventHandler(this.btn_SingleBandHis_Click);
            // 
            // cmb_DrawHisBand
            // 
            this.cmb_DrawHisBand.FormattingEnabled = true;
            this.cmb_DrawHisBand.Location = new System.Drawing.Point(59, 51);
            this.cmb_DrawHisBand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_DrawHisBand.Name = "cmb_DrawHisBand";
            this.cmb_DrawHisBand.Size = new System.Drawing.Size(115, 23);
            this.cmb_DrawHisBand.TabIndex = 5;
            // 
            // cmb_DrawHisLayer
            // 
            this.cmb_DrawHisLayer.FormattingEnabled = true;
            this.cmb_DrawHisLayer.Location = new System.Drawing.Point(59, 21);
            this.cmb_DrawHisLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_DrawHisLayer.Name = "cmb_DrawHisLayer";
            this.cmb_DrawHisLayer.Size = new System.Drawing.Size(115, 23);
            this.cmb_DrawHisLayer.TabIndex = 4;
            this.cmb_DrawHisLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_DrawHisLayer_SelectedIndexChanged);
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Location = new System.Drawing.Point(11, 51);
            this.textBox9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(37, 18);
            this.textBox9.TabIndex = 4;
            this.textBox9.Text = "波段";
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Location = new System.Drawing.Point(11, 24);
            this.textBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(37, 18);
            this.textBox8.TabIndex = 4;
            this.textBox8.Text = "图层";
            // 
            // grb_NDVILayer
            // 
            this.grb_NDVILayer.Controls.Add(this.btn_NDVI_Bottom);
            this.grb_NDVILayer.Controls.Add(this.btn_CalculateNDVI_);
            this.grb_NDVILayer.Controls.Add(this.cmb_NDVILayer);
            this.grb_NDVILayer.Controls.Add(this.textBox7);
            this.grb_NDVILayer.Location = new System.Drawing.Point(5, 99);
            this.grb_NDVILayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_NDVILayer.Name = "grb_NDVILayer";
            this.grb_NDVILayer.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_NDVILayer.Size = new System.Drawing.Size(315, 66);
            this.grb_NDVILayer.TabIndex = 1;
            this.grb_NDVILayer.TabStop = false;
            this.grb_NDVILayer.Text = "NDVI指数计算";
            // 
            // btn_NDVI_Bottom
            // 
            this.btn_NDVI_Bottom.Location = new System.Drawing.Point(251, 28);
            this.btn_NDVI_Bottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_NDVI_Bottom.Name = "btn_NDVI_Bottom";
            this.btn_NDVI_Bottom.Size = new System.Drawing.Size(56, 26);
            this.btn_NDVI_Bottom.TabIndex = 7;
            this.btn_NDVI_Bottom.Text = "底层";
            this.btn_NDVI_Bottom.UseVisualStyleBackColor = true;
            this.btn_NDVI_Bottom.Click += new System.EventHandler(this.btn_NDVI_Bottom_Click);
            // 
            // btn_CalculateNDVI_
            // 
            this.btn_CalculateNDVI_.Location = new System.Drawing.Point(192, 26);
            this.btn_CalculateNDVI_.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CalculateNDVI_.Name = "btn_CalculateNDVI_";
            this.btn_CalculateNDVI_.Size = new System.Drawing.Size(56, 26);
            this.btn_CalculateNDVI_.TabIndex = 6;
            this.btn_CalculateNDVI_.Text = "计算";
            this.btn_CalculateNDVI_.UseVisualStyleBackColor = true;
            this.btn_CalculateNDVI_.Click += new System.EventHandler(this.btn_CalculateNDVI__Click);
            // 
            // cmb_NDVILayer
            // 
            this.cmb_NDVILayer.FormattingEnabled = true;
            this.cmb_NDVILayer.Location = new System.Drawing.Point(59, 30);
            this.cmb_NDVILayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_NDVILayer.Name = "cmb_NDVILayer";
            this.cmb_NDVILayer.Size = new System.Drawing.Size(115, 23);
            this.cmb_NDVILayer.TabIndex = 4;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Location = new System.Drawing.Point(11, 32);
            this.textBox7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(37, 18);
            this.textBox7.TabIndex = 4;
            this.textBox7.Text = "图层";
            // 
            // grb_StatisticsLayer
            // 
            this.grb_StatisticsLayer.Controls.Add(this.btn_Statistics_Bottom);
            this.grb_StatisticsLayer.Controls.Add(this.btn_Statistics);
            this.grb_StatisticsLayer.Controls.Add(this.textBox6);
            this.grb_StatisticsLayer.Controls.Add(this.textBox5);
            this.grb_StatisticsLayer.Controls.Add(this.cmb_StatisticsBand);
            this.grb_StatisticsLayer.Controls.Add(this.cmb_StatistiicsLayer);
            this.grb_StatisticsLayer.Location = new System.Drawing.Point(5, 11);
            this.grb_StatisticsLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_StatisticsLayer.Name = "grb_StatisticsLayer";
            this.grb_StatisticsLayer.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_StatisticsLayer.Size = new System.Drawing.Size(315, 85);
            this.grb_StatisticsLayer.TabIndex = 0;
            this.grb_StatisticsLayer.TabStop = false;
            this.grb_StatisticsLayer.Text = "波段信息统计";
            // 
            // btn_Statistics_Bottom
            // 
            this.btn_Statistics_Bottom.Location = new System.Drawing.Point(251, 32);
            this.btn_Statistics_Bottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Statistics_Bottom.Name = "btn_Statistics_Bottom";
            this.btn_Statistics_Bottom.Size = new System.Drawing.Size(56, 29);
            this.btn_Statistics_Bottom.TabIndex = 22;
            this.btn_Statistics_Bottom.Text = "底层";
            this.btn_Statistics_Bottom.UseVisualStyleBackColor = true;
            this.btn_Statistics_Bottom.Click += new System.EventHandler(this.btn_Statistics_Bottom_Click);
            // 
            // btn_Statistics
            // 
            this.btn_Statistics.Location = new System.Drawing.Point(192, 32);
            this.btn_Statistics.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Statistics.Name = "btn_Statistics";
            this.btn_Statistics.Size = new System.Drawing.Size(56, 29);
            this.btn_Statistics.TabIndex = 21;
            this.btn_Statistics.Text = "统计";
            this.btn_Statistics.UseVisualStyleBackColor = true;
            this.btn_Statistics.Click += new System.EventHandler(this.btn_Statistics_Click);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Location = new System.Drawing.Point(11, 52);
            this.textBox6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(37, 18);
            this.textBox6.TabIndex = 3;
            this.textBox6.Text = "波段";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Location = new System.Drawing.Point(11, 24);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(37, 18);
            this.textBox5.TabIndex = 2;
            this.textBox5.Text = "图层";
            // 
            // cmb_StatisticsBand
            // 
            this.cmb_StatisticsBand.FormattingEnabled = true;
            this.cmb_StatisticsBand.Location = new System.Drawing.Point(59, 52);
            this.cmb_StatisticsBand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_StatisticsBand.Name = "cmb_StatisticsBand";
            this.cmb_StatisticsBand.Size = new System.Drawing.Size(115, 23);
            this.cmb_StatisticsBand.TabIndex = 1;
            // 
            // cmb_StatistiicsLayer
            // 
            this.cmb_StatistiicsLayer.FormattingEnabled = true;
            this.cmb_StatistiicsLayer.Location = new System.Drawing.Point(59, 21);
            this.cmb_StatistiicsLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_StatistiicsLayer.Name = "cmb_StatistiicsLayer";
            this.cmb_StatistiicsLayer.Size = new System.Drawing.Size(115, 23);
            this.cmb_StatistiicsLayer.TabIndex = 0;
            this.cmb_StatistiicsLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_StatistiicsLayer_SelectedIndexChanged);
            // 
            // tbp_ImageAnalysis
            // 
            this.tbp_ImageAnalysis.Controls.Add(this.grb_Transform);
            this.tbp_ImageAnalysis.Controls.Add(this.grb_Filter);
            this.tbp_ImageAnalysis.Controls.Add(this.grb_Mosaic);
            this.tbp_ImageAnalysis.Controls.Add(this.grb_PanSharpen);
            this.tbp_ImageAnalysis.Controls.Add(this.grb_ClipFeature);
            this.tbp_ImageAnalysis.Controls.Add(this.grb_ImageClass);
            this.tbp_ImageAnalysis.Location = new System.Drawing.Point(4, 25);
            this.tbp_ImageAnalysis.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbp_ImageAnalysis.Name = "tbp_ImageAnalysis";
            this.tbp_ImageAnalysis.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbp_ImageAnalysis.Size = new System.Drawing.Size(353, 643);
            this.tbp_ImageAnalysis.TabIndex = 2;
            this.tbp_ImageAnalysis.Text = "图像分析";
            this.tbp_ImageAnalysis.UseVisualStyleBackColor = true;
            // 
            // grb_Transform
            // 
            this.grb_Transform.Controls.Add(this.txb_TransformAngle);
            this.grb_Transform.Controls.Add(this.textBox30);
            this.grb_Transform.Controls.Add(this.cmb_TransformMethod);
            this.grb_Transform.Controls.Add(this.textBox28);
            this.grb_Transform.Controls.Add(this.btn_Transform);
            this.grb_Transform.Controls.Add(this.textBox29);
            this.grb_Transform.Controls.Add(this.cmb_TransformLayer);
            this.grb_Transform.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_Transform.Location = new System.Drawing.Point(8, 360);
            this.grb_Transform.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Transform.Name = "grb_Transform";
            this.grb_Transform.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Transform.Size = new System.Drawing.Size(309, 95);
            this.grb_Transform.TabIndex = 17;
            this.grb_Transform.TabStop = false;
            this.grb_Transform.Text = "图像变换";
            // 
            // txb_TransformAngle
            // 
            this.txb_TransformAngle.Location = new System.Drawing.Point(224, 29);
            this.txb_TransformAngle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_TransformAngle.Name = "txb_TransformAngle";
            this.txb_TransformAngle.Size = new System.Drawing.Size(57, 25);
            this.txb_TransformAngle.TabIndex = 14;
            // 
            // textBox30
            // 
            this.textBox30.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox30.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox30.Location = new System.Drawing.Point(184, 32);
            this.textBox30.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox30.Name = "textBox30";
            this.textBox30.ReadOnly = true;
            this.textBox30.Size = new System.Drawing.Size(47, 18);
            this.textBox30.TabIndex = 16;
            this.textBox30.Text = "角度";
            // 
            // cmb_TransformMethod
            // 
            this.cmb_TransformMethod.FormattingEnabled = true;
            this.cmb_TransformMethod.Location = new System.Drawing.Point(56, 60);
            this.cmb_TransformMethod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_TransformMethod.Name = "cmb_TransformMethod";
            this.cmb_TransformMethod.Size = new System.Drawing.Size(116, 23);
            this.cmb_TransformMethod.TabIndex = 15;
            // 
            // textBox28
            // 
            this.textBox28.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox28.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox28.Location = new System.Drawing.Point(5, 60);
            this.textBox28.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox28.Name = "textBox28";
            this.textBox28.ReadOnly = true;
            this.textBox28.Size = new System.Drawing.Size(47, 18);
            this.textBox28.TabIndex = 14;
            this.textBox28.Text = "方式";
            // 
            // btn_Transform
            // 
            this.btn_Transform.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Transform.Location = new System.Drawing.Point(184, 58);
            this.btn_Transform.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Transform.Name = "btn_Transform";
            this.btn_Transform.Size = new System.Drawing.Size(88, 28);
            this.btn_Transform.TabIndex = 13;
            this.btn_Transform.Text = "变换";
            this.btn_Transform.UseVisualStyleBackColor = true;
            this.btn_Transform.Click += new System.EventHandler(this.btn_Transform_Click);
            // 
            // textBox29
            // 
            this.textBox29.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox29.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox29.Location = new System.Drawing.Point(5, 34);
            this.textBox29.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox29.Name = "textBox29";
            this.textBox29.ReadOnly = true;
            this.textBox29.Size = new System.Drawing.Size(47, 18);
            this.textBox29.TabIndex = 10;
            this.textBox29.Text = "图层";
            // 
            // cmb_TransformLayer
            // 
            this.cmb_TransformLayer.FormattingEnabled = true;
            this.cmb_TransformLayer.Location = new System.Drawing.Point(56, 30);
            this.cmb_TransformLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_TransformLayer.Name = "cmb_TransformLayer";
            this.cmb_TransformLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_TransformLayer.TabIndex = 0;
            // 
            // grb_Filter
            // 
            this.grb_Filter.Controls.Add(this.cmb_FliterMethod);
            this.grb_Filter.Controls.Add(this.textBox27);
            this.grb_Filter.Controls.Add(this.btn_Filter);
            this.grb_Filter.Controls.Add(this.textBox23);
            this.grb_Filter.Controls.Add(this.cmb_FliterLayer);
            this.grb_Filter.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_Filter.Location = new System.Drawing.Point(8, 256);
            this.grb_Filter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Filter.Name = "grb_Filter";
            this.grb_Filter.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Filter.Size = new System.Drawing.Size(309, 89);
            this.grb_Filter.TabIndex = 16;
            this.grb_Filter.TabStop = false;
            this.grb_Filter.Text = "卷积运算";
            // 
            // cmb_FliterMethod
            // 
            this.cmb_FliterMethod.FormattingEnabled = true;
            this.cmb_FliterMethod.Location = new System.Drawing.Point(56, 60);
            this.cmb_FliterMethod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_FliterMethod.Name = "cmb_FliterMethod";
            this.cmb_FliterMethod.Size = new System.Drawing.Size(116, 23);
            this.cmb_FliterMethod.TabIndex = 15;
            // 
            // textBox27
            // 
            this.textBox27.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox27.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox27.Location = new System.Drawing.Point(5, 60);
            this.textBox27.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox27.Name = "textBox27";
            this.textBox27.ReadOnly = true;
            this.textBox27.Size = new System.Drawing.Size(47, 18);
            this.textBox27.TabIndex = 14;
            this.textBox27.Text = "方式";
            // 
            // btn_Filter
            // 
            this.btn_Filter.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Filter.Location = new System.Drawing.Point(192, 41);
            this.btn_Filter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Filter.Name = "btn_Filter";
            this.btn_Filter.Size = new System.Drawing.Size(67, 28);
            this.btn_Filter.TabIndex = 13;
            this.btn_Filter.Text = "滤波";
            this.btn_Filter.UseVisualStyleBackColor = true;
            this.btn_Filter.Click += new System.EventHandler(this.btn_Filter_Click);
            // 
            // textBox23
            // 
            this.textBox23.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox23.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox23.Location = new System.Drawing.Point(5, 34);
            this.textBox23.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox23.Name = "textBox23";
            this.textBox23.ReadOnly = true;
            this.textBox23.Size = new System.Drawing.Size(47, 18);
            this.textBox23.TabIndex = 10;
            this.textBox23.Text = "图层";
            // 
            // cmb_FliterLayer
            // 
            this.cmb_FliterLayer.FormattingEnabled = true;
            this.cmb_FliterLayer.Location = new System.Drawing.Point(56, 30);
            this.cmb_FliterLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_FliterLayer.Name = "cmb_FliterLayer";
            this.cmb_FliterLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_FliterLayer.TabIndex = 0;
            // 
            // grb_Mosaic
            // 
            this.grb_Mosaic.Controls.Add(this.btn_Mosaic);
            this.grb_Mosaic.Controls.Add(this.textBox26);
            this.grb_Mosaic.Controls.Add(this.cmb_MosaicCatalog);
            this.grb_Mosaic.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_Mosaic.Location = new System.Drawing.Point(8, 182);
            this.grb_Mosaic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Mosaic.Name = "grb_Mosaic";
            this.grb_Mosaic.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Mosaic.Size = new System.Drawing.Size(309, 64);
            this.grb_Mosaic.TabIndex = 15;
            this.grb_Mosaic.TabStop = false;
            this.grb_Mosaic.Text = "图像镶嵌";
            // 
            // btn_Mosaic
            // 
            this.btn_Mosaic.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Mosaic.Location = new System.Drawing.Point(205, 22);
            this.btn_Mosaic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Mosaic.Name = "btn_Mosaic";
            this.btn_Mosaic.Size = new System.Drawing.Size(55, 30);
            this.btn_Mosaic.TabIndex = 13;
            this.btn_Mosaic.Text = "镶嵌";
            this.btn_Mosaic.UseVisualStyleBackColor = true;
            this.btn_Mosaic.Click += new System.EventHandler(this.btn_Mosaic_Click);
            // 
            // textBox26
            // 
            this.textBox26.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox26.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox26.Location = new System.Drawing.Point(5, 34);
            this.textBox26.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox26.Name = "textBox26";
            this.textBox26.ReadOnly = true;
            this.textBox26.Size = new System.Drawing.Size(64, 18);
            this.textBox26.TabIndex = 10;
            this.textBox26.Text = "栅格目录";
            // 
            // cmb_MosaicCatalog
            // 
            this.cmb_MosaicCatalog.FormattingEnabled = true;
            this.cmb_MosaicCatalog.Location = new System.Drawing.Point(80, 31);
            this.cmb_MosaicCatalog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_MosaicCatalog.Name = "cmb_MosaicCatalog";
            this.cmb_MosaicCatalog.Size = new System.Drawing.Size(116, 23);
            this.cmb_MosaicCatalog.TabIndex = 0;
            // 
            // grb_PanSharpen
            // 
            this.grb_PanSharpen.Controls.Add(this.cmb_PanSharpenMultiLayer);
            this.grb_PanSharpen.Controls.Add(this.btn_PanSharpen);
            this.grb_PanSharpen.Controls.Add(this.textBox24);
            this.grb_PanSharpen.Controls.Add(this.textBox25);
            this.grb_PanSharpen.Controls.Add(this.cmb_PanSharpenSigleLayer);
            this.grb_PanSharpen.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_PanSharpen.Location = new System.Drawing.Point(8, 90);
            this.grb_PanSharpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_PanSharpen.Name = "grb_PanSharpen";
            this.grb_PanSharpen.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_PanSharpen.Size = new System.Drawing.Size(309, 82);
            this.grb_PanSharpen.TabIndex = 14;
            this.grb_PanSharpen.TabStop = false;
            this.grb_PanSharpen.Text = "全色锐化（融合）";
            // 
            // cmb_PanSharpenMultiLayer
            // 
            this.cmb_PanSharpenMultiLayer.FormattingEnabled = true;
            this.cmb_PanSharpenMultiLayer.Location = new System.Drawing.Point(56, 50);
            this.cmb_PanSharpenMultiLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_PanSharpenMultiLayer.Name = "cmb_PanSharpenMultiLayer";
            this.cmb_PanSharpenMultiLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_PanSharpenMultiLayer.TabIndex = 14;
            // 
            // btn_PanSharpen
            // 
            this.btn_PanSharpen.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_PanSharpen.Location = new System.Drawing.Point(192, 30);
            this.btn_PanSharpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_PanSharpen.Name = "btn_PanSharpen";
            this.btn_PanSharpen.Size = new System.Drawing.Size(67, 28);
            this.btn_PanSharpen.TabIndex = 13;
            this.btn_PanSharpen.Text = "融合";
            this.btn_PanSharpen.UseVisualStyleBackColor = true;
            this.btn_PanSharpen.Click += new System.EventHandler(this.btn_PanSharpen_Click);
            // 
            // textBox24
            // 
            this.textBox24.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox24.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox24.Location = new System.Drawing.Point(5, 52);
            this.textBox24.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox24.Name = "textBox24";
            this.textBox24.ReadOnly = true;
            this.textBox24.Size = new System.Drawing.Size(52, 18);
            this.textBox24.TabIndex = 11;
            this.textBox24.Text = "多波段";
            // 
            // textBox25
            // 
            this.textBox25.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox25.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox25.Location = new System.Drawing.Point(5, 24);
            this.textBox25.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox25.Name = "textBox25";
            this.textBox25.ReadOnly = true;
            this.textBox25.Size = new System.Drawing.Size(47, 18);
            this.textBox25.TabIndex = 10;
            this.textBox25.Text = "单波段";
            // 
            // cmb_PanSharpenSigleLayer
            // 
            this.cmb_PanSharpenSigleLayer.FormattingEnabled = true;
            this.cmb_PanSharpenSigleLayer.Location = new System.Drawing.Point(56, 21);
            this.cmb_PanSharpenSigleLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_PanSharpenSigleLayer.Name = "cmb_PanSharpenSigleLayer";
            this.cmb_PanSharpenSigleLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_PanSharpenSigleLayer.TabIndex = 0;
            // 
            // grb_ClipFeature
            // 
            this.grb_ClipFeature.Controls.Add(this.btn_Clip);
            this.grb_ClipFeature.Controls.Add(this.txb_ClipFeature);
            this.grb_ClipFeature.Controls.Add(this.textBox22);
            this.grb_ClipFeature.Controls.Add(this.textBox21);
            this.grb_ClipFeature.Controls.Add(this.cmb_ClipFeatureLayer);
            this.grb_ClipFeature.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_ClipFeature.Location = new System.Drawing.Point(8, 4);
            this.grb_ClipFeature.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_ClipFeature.Name = "grb_ClipFeature";
            this.grb_ClipFeature.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_ClipFeature.Size = new System.Drawing.Size(309, 82);
            this.grb_ClipFeature.TabIndex = 1;
            this.grb_ClipFeature.TabStop = false;
            this.grb_ClipFeature.Text = "图像剪裁";
            // 
            // btn_Clip
            // 
            this.btn_Clip.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Clip.Location = new System.Drawing.Point(192, 30);
            this.btn_Clip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Clip.Name = "btn_Clip";
            this.btn_Clip.Size = new System.Drawing.Size(67, 28);
            this.btn_Clip.TabIndex = 13;
            this.btn_Clip.Text = "剪裁";
            this.btn_Clip.UseVisualStyleBackColor = true;
            this.btn_Clip.Click += new System.EventHandler(this.btn_Clip_Click);
            // 
            // txb_ClipFeature
            // 
            this.txb_ClipFeature.Location = new System.Drawing.Point(56, 49);
            this.txb_ClipFeature.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_ClipFeature.Name = "txb_ClipFeature";
            this.txb_ClipFeature.Size = new System.Drawing.Size(116, 25);
            this.txb_ClipFeature.TabIndex = 12;
            this.txb_ClipFeature.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txb_ClipFeature_MouseDown);
            // 
            // textBox22
            // 
            this.textBox22.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox22.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox22.Location = new System.Drawing.Point(13, 52);
            this.textBox22.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox22.Name = "textBox22";
            this.textBox22.ReadOnly = true;
            this.textBox22.Size = new System.Drawing.Size(37, 18);
            this.textBox22.TabIndex = 11;
            this.textBox22.Text = "矢量";
            // 
            // textBox21
            // 
            this.textBox21.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox21.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox21.Location = new System.Drawing.Point(13, 24);
            this.textBox21.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox21.Name = "textBox21";
            this.textBox21.ReadOnly = true;
            this.textBox21.Size = new System.Drawing.Size(37, 18);
            this.textBox21.TabIndex = 10;
            this.textBox21.Text = "图层";
            // 
            // cmb_ClipFeatureLayer
            // 
            this.cmb_ClipFeatureLayer.FormattingEnabled = true;
            this.cmb_ClipFeatureLayer.Location = new System.Drawing.Point(56, 21);
            this.cmb_ClipFeatureLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_ClipFeatureLayer.Name = "cmb_ClipFeatureLayer";
            this.cmb_ClipFeatureLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_ClipFeatureLayer.TabIndex = 0;
            // 
            // grb_ImageClass
            // 
            this.grb_ImageClass.Controls.Add(this.btn_After_Classify);
            this.grb_ImageClass.Controls.Add(this.btn_Classify);
            this.grb_ImageClass.Controls.Add(this.txb_ClassifyNumber);
            this.grb_ImageClass.Controls.Add(this.textBox20);
            this.grb_ImageClass.Controls.Add(this.textBox19);
            this.grb_ImageClass.Controls.Add(this.cmb_ClassifyLayer);
            this.grb_ImageClass.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_ImageClass.Location = new System.Drawing.Point(8, 468);
            this.grb_ImageClass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_ImageClass.Name = "grb_ImageClass";
            this.grb_ImageClass.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_ImageClass.Size = new System.Drawing.Size(309, 90);
            this.grb_ImageClass.TabIndex = 0;
            this.grb_ImageClass.TabStop = false;
            this.grb_ImageClass.Text = "图像分类";
            // 
            // btn_After_Classify
            // 
            this.btn_After_Classify.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_After_Classify.Location = new System.Drawing.Point(192, 48);
            this.btn_After_Classify.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_After_Classify.Name = "btn_After_Classify";
            this.btn_After_Classify.Size = new System.Drawing.Size(103, 22);
            this.btn_After_Classify.TabIndex = 9;
            this.btn_After_Classify.Text = "分类后处理";
            this.btn_After_Classify.UseVisualStyleBackColor = true;
            this.btn_After_Classify.Click += new System.EventHandler(this.btn_After_Classify_Click);
            // 
            // btn_Classify
            // 
            this.btn_Classify.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Classify.Location = new System.Drawing.Point(109, 48);
            this.btn_Classify.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Classify.Name = "btn_Classify";
            this.btn_Classify.Size = new System.Drawing.Size(67, 22);
            this.btn_Classify.TabIndex = 8;
            this.btn_Classify.Text = "分类";
            this.btn_Classify.UseVisualStyleBackColor = true;
            this.btn_Classify.Click += new System.EventHandler(this.btn_Classify_Click);
            // 
            // txb_ClassifyNumber
            // 
            this.txb_ClassifyNumber.Location = new System.Drawing.Point(235, 20);
            this.txb_ClassifyNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_ClassifyNumber.Name = "txb_ClassifyNumber";
            this.txb_ClassifyNumber.Size = new System.Drawing.Size(57, 25);
            this.txb_ClassifyNumber.TabIndex = 7;
            // 
            // textBox20
            // 
            this.textBox20.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox20.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox20.Location = new System.Drawing.Point(192, 25);
            this.textBox20.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox20.Name = "textBox20";
            this.textBox20.ReadOnly = true;
            this.textBox20.Size = new System.Drawing.Size(37, 18);
            this.textBox20.TabIndex = 6;
            this.textBox20.Text = "数量";
            // 
            // textBox19
            // 
            this.textBox19.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox19.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox19.Location = new System.Drawing.Point(13, 25);
            this.textBox19.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox19.Name = "textBox19";
            this.textBox19.ReadOnly = true;
            this.textBox19.Size = new System.Drawing.Size(37, 18);
            this.textBox19.TabIndex = 5;
            this.textBox19.Text = "图层";
            // 
            // cmb_ClassifyLayer
            // 
            this.cmb_ClassifyLayer.FormattingEnabled = true;
            this.cmb_ClassifyLayer.Location = new System.Drawing.Point(56, 21);
            this.cmb_ClassifyLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_ClassifyLayer.Name = "cmb_ClassifyLayer";
            this.cmb_ClassifyLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_ClassifyLayer.TabIndex = 0;
            // 
            // tbp_SpatialAnalysis
            // 
            this.tbp_SpatialAnalysis.Controls.Add(this.grb_contour_Voronoi);
            this.tbp_SpatialAnalysis.Controls.Add(this.grb_CreateTIN);
            this.tbp_SpatialAnalysis.Controls.Add(this.grb_Extraction);
            this.tbp_SpatialAnalysis.Controls.Add(this.grb_Neighborhood);
            this.tbp_SpatialAnalysis.Controls.Add(this.groupBox2);
            this.tbp_SpatialAnalysis.Controls.Add(this.grb_HillShade_Slope_Aspect);
            this.tbp_SpatialAnalysis.Location = new System.Drawing.Point(4, 25);
            this.tbp_SpatialAnalysis.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbp_SpatialAnalysis.Name = "tbp_SpatialAnalysis";
            this.tbp_SpatialAnalysis.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbp_SpatialAnalysis.Size = new System.Drawing.Size(353, 643);
            this.tbp_SpatialAnalysis.TabIndex = 3;
            this.tbp_SpatialAnalysis.Text = "空间分析";
            this.tbp_SpatialAnalysis.UseVisualStyleBackColor = true;
            // 
            // grb_contour_Voronoi
            // 
            this.grb_contour_Voronoi.Controls.Add(this.btn_Voronoi);
            this.grb_contour_Voronoi.Controls.Add(this.textBox40);
            this.grb_contour_Voronoi.Controls.Add(this.cmb_TinVoronoi);
            this.grb_contour_Voronoi.Controls.Add(this.btn_tinContour);
            this.grb_contour_Voronoi.Controls.Add(this.textBox41);
            this.grb_contour_Voronoi.Controls.Add(this.cmb_tinContour);
            this.grb_contour_Voronoi.Controls.Add(this.btn_DEMContour);
            this.grb_contour_Voronoi.Controls.Add(this.textBox42);
            this.grb_contour_Voronoi.Controls.Add(this.cmb_DEMContour);
            this.grb_contour_Voronoi.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_contour_Voronoi.Location = new System.Drawing.Point(5, 440);
            this.grb_contour_Voronoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_contour_Voronoi.Name = "grb_contour_Voronoi";
            this.grb_contour_Voronoi.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_contour_Voronoi.Size = new System.Drawing.Size(309, 124);
            this.grb_contour_Voronoi.TabIndex = 20;
            this.grb_contour_Voronoi.TabStop = false;
            this.grb_contour_Voronoi.Text = "等高线/泰森多边形";
            // 
            // btn_Voronoi
            // 
            this.btn_Voronoi.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Voronoi.Location = new System.Drawing.Point(191, 86);
            this.btn_Voronoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Voronoi.Name = "btn_Voronoi";
            this.btn_Voronoi.Size = new System.Drawing.Size(108, 28);
            this.btn_Voronoi.TabIndex = 19;
            this.btn_Voronoi.Text = "泰森多边形";
            this.btn_Voronoi.UseVisualStyleBackColor = true;
            this.btn_Voronoi.Click += new System.EventHandler(this.btn_Voronoi_Click);
            // 
            // textBox40
            // 
            this.textBox40.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox40.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox40.Location = new System.Drawing.Point(13, 92);
            this.textBox40.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox40.Name = "textBox40";
            this.textBox40.ReadOnly = true;
            this.textBox40.Size = new System.Drawing.Size(37, 18);
            this.textBox40.TabIndex = 18;
            this.textBox40.Text = "TIN";
            // 
            // cmb_TinVoronoi
            // 
            this.cmb_TinVoronoi.FormattingEnabled = true;
            this.cmb_TinVoronoi.Location = new System.Drawing.Point(56, 90);
            this.cmb_TinVoronoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_TinVoronoi.Name = "cmb_TinVoronoi";
            this.cmb_TinVoronoi.Size = new System.Drawing.Size(116, 23);
            this.cmb_TinVoronoi.TabIndex = 17;
            // 
            // btn_tinContour
            // 
            this.btn_tinContour.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_tinContour.Location = new System.Drawing.Point(191, 52);
            this.btn_tinContour.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_tinContour.Name = "btn_tinContour";
            this.btn_tinContour.Size = new System.Drawing.Size(108, 28);
            this.btn_tinContour.TabIndex = 16;
            this.btn_tinContour.Text = "生成等高线";
            this.btn_tinContour.UseVisualStyleBackColor = true;
            this.btn_tinContour.Click += new System.EventHandler(this.btn_tinContour_Click);
            // 
            // textBox41
            // 
            this.textBox41.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox41.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox41.Location = new System.Drawing.Point(13, 58);
            this.textBox41.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox41.Name = "textBox41";
            this.textBox41.ReadOnly = true;
            this.textBox41.Size = new System.Drawing.Size(37, 18);
            this.textBox41.TabIndex = 15;
            this.textBox41.Text = "TIN";
            // 
            // cmb_tinContour
            // 
            this.cmb_tinContour.FormattingEnabled = true;
            this.cmb_tinContour.Location = new System.Drawing.Point(56, 55);
            this.cmb_tinContour.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_tinContour.Name = "cmb_tinContour";
            this.cmb_tinContour.Size = new System.Drawing.Size(116, 23);
            this.cmb_tinContour.TabIndex = 14;
            // 
            // btn_DEMContour
            // 
            this.btn_DEMContour.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_DEMContour.Location = new System.Drawing.Point(191, 18);
            this.btn_DEMContour.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_DEMContour.Name = "btn_DEMContour";
            this.btn_DEMContour.Size = new System.Drawing.Size(108, 28);
            this.btn_DEMContour.TabIndex = 13;
            this.btn_DEMContour.Text = "生成等高线";
            this.btn_DEMContour.UseVisualStyleBackColor = true;
            this.btn_DEMContour.Click += new System.EventHandler(this.btn_DEMContour_Click);
            // 
            // textBox42
            // 
            this.textBox42.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox42.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox42.Location = new System.Drawing.Point(13, 24);
            this.textBox42.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox42.Name = "textBox42";
            this.textBox42.ReadOnly = true;
            this.textBox42.Size = new System.Drawing.Size(37, 18);
            this.textBox42.TabIndex = 10;
            this.textBox42.Text = "DEM";
            // 
            // cmb_DEMContour
            // 
            this.cmb_DEMContour.FormattingEnabled = true;
            this.cmb_DEMContour.Location = new System.Drawing.Point(56, 21);
            this.cmb_DEMContour.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_DEMContour.Name = "cmb_DEMContour";
            this.cmb_DEMContour.Size = new System.Drawing.Size(116, 23);
            this.cmb_DEMContour.TabIndex = 0;
            // 
            // grb_CreateTIN
            // 
            this.grb_CreateTIN.Controls.Add(this.btn_CreateTINAuto);
            this.grb_CreateTIN.Controls.Add(this.btm_CreateTin);
            this.grb_CreateTIN.Controls.Add(this.textBox38);
            this.grb_CreateTIN.Controls.Add(this.cmb_CreateTINLayer);
            this.grb_CreateTIN.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_CreateTIN.Location = new System.Drawing.Point(5, 376);
            this.grb_CreateTIN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_CreateTIN.Name = "grb_CreateTIN";
            this.grb_CreateTIN.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_CreateTIN.Size = new System.Drawing.Size(309, 60);
            this.grb_CreateTIN.TabIndex = 23;
            this.grb_CreateTIN.TabStop = false;
            this.grb_CreateTIN.Text = "构建TIN";
            // 
            // btn_CreateTINAuto
            // 
            this.btn_CreateTINAuto.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_CreateTINAuto.Location = new System.Drawing.Point(243, 18);
            this.btn_CreateTINAuto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_CreateTINAuto.Name = "btn_CreateTINAuto";
            this.btn_CreateTINAuto.Size = new System.Drawing.Size(56, 28);
            this.btn_CreateTINAuto.TabIndex = 17;
            this.btn_CreateTINAuto.Text = "采样";
            this.btn_CreateTINAuto.UseVisualStyleBackColor = true;
            this.btn_CreateTINAuto.Click += new System.EventHandler(this.btn_CreateTINAuto_Click);
            // 
            // btm_CreateTin
            // 
            this.btm_CreateTin.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btm_CreateTin.Location = new System.Drawing.Point(183, 18);
            this.btm_CreateTin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btm_CreateTin.Name = "btm_CreateTin";
            this.btm_CreateTin.Size = new System.Drawing.Size(56, 28);
            this.btm_CreateTin.TabIndex = 16;
            this.btm_CreateTin.Text = "手绘";
            this.btm_CreateTin.UseVisualStyleBackColor = true;
            this.btm_CreateTin.Click += new System.EventHandler(this.btm_CreateTin_Click);
            // 
            // textBox38
            // 
            this.textBox38.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox38.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox38.Location = new System.Drawing.Point(13, 24);
            this.textBox38.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox38.Name = "textBox38";
            this.textBox38.ReadOnly = true;
            this.textBox38.Size = new System.Drawing.Size(37, 18);
            this.textBox38.TabIndex = 10;
            this.textBox38.Text = "DEM";
            // 
            // cmb_CreateTINLayer
            // 
            this.cmb_CreateTINLayer.FormattingEnabled = true;
            this.cmb_CreateTINLayer.Location = new System.Drawing.Point(56, 21);
            this.cmb_CreateTINLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_CreateTINLayer.Name = "cmb_CreateTINLayer";
            this.cmb_CreateTINLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_CreateTINLayer.TabIndex = 0;
            // 
            // grb_Extraction
            // 
            this.grb_Extraction.Controls.Add(this.btn_Extraction);
            this.grb_Extraction.Controls.Add(this.textBox39);
            this.grb_Extraction.Controls.Add(this.cmb_Extraction);
            this.grb_Extraction.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_Extraction.Location = new System.Drawing.Point(5, 312);
            this.grb_Extraction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Extraction.Name = "grb_Extraction";
            this.grb_Extraction.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Extraction.Size = new System.Drawing.Size(309, 60);
            this.grb_Extraction.TabIndex = 22;
            this.grb_Extraction.TabStop = false;
            this.grb_Extraction.Text = "剪裁分析";
            // 
            // btn_Extraction
            // 
            this.btn_Extraction.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Extraction.Location = new System.Drawing.Point(191, 18);
            this.btn_Extraction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Extraction.Name = "btn_Extraction";
            this.btn_Extraction.Size = new System.Drawing.Size(93, 28);
            this.btn_Extraction.TabIndex = 16;
            this.btn_Extraction.Text = "裁剪分析";
            this.btn_Extraction.UseVisualStyleBackColor = true;
            this.btn_Extraction.Click += new System.EventHandler(this.btn_Extraction_Click);
            // 
            // textBox39
            // 
            this.textBox39.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox39.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox39.Location = new System.Drawing.Point(13, 24);
            this.textBox39.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox39.Name = "textBox39";
            this.textBox39.ReadOnly = true;
            this.textBox39.Size = new System.Drawing.Size(37, 18);
            this.textBox39.TabIndex = 10;
            this.textBox39.Text = "栅格";
            // 
            // cmb_Extraction
            // 
            this.cmb_Extraction.FormattingEnabled = true;
            this.cmb_Extraction.Location = new System.Drawing.Point(56, 21);
            this.cmb_Extraction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Extraction.Name = "cmb_Extraction";
            this.cmb_Extraction.Size = new System.Drawing.Size(116, 23);
            this.cmb_Extraction.TabIndex = 0;
            // 
            // grb_Neighborhood
            // 
            this.grb_Neighborhood.Controls.Add(this.btn_Neighborhood);
            this.grb_Neighborhood.Controls.Add(this.textBox34);
            this.grb_Neighborhood.Controls.Add(this.cmb_NeighborhoodMethod);
            this.grb_Neighborhood.Controls.Add(this.textBox37);
            this.grb_Neighborhood.Controls.Add(this.cmb_NeighborhoodLayer);
            this.grb_Neighborhood.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_Neighborhood.Location = new System.Drawing.Point(5, 220);
            this.grb_Neighborhood.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Neighborhood.Name = "grb_Neighborhood";
            this.grb_Neighborhood.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_Neighborhood.Size = new System.Drawing.Size(309, 88);
            this.grb_Neighborhood.TabIndex = 21;
            this.grb_Neighborhood.TabStop = false;
            this.grb_Neighborhood.Text = "邻域分析";
            // 
            // btn_Neighborhood
            // 
            this.btn_Neighborhood.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Neighborhood.Location = new System.Drawing.Point(191, 35);
            this.btn_Neighborhood.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Neighborhood.Name = "btn_Neighborhood";
            this.btn_Neighborhood.Size = new System.Drawing.Size(93, 28);
            this.btn_Neighborhood.TabIndex = 16;
            this.btn_Neighborhood.Text = "邻域分析";
            this.btn_Neighborhood.UseVisualStyleBackColor = true;
            this.btn_Neighborhood.Click += new System.EventHandler(this.btn_Neighborhood_Click);
            // 
            // textBox34
            // 
            this.textBox34.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox34.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox34.Location = new System.Drawing.Point(13, 58);
            this.textBox34.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox34.Name = "textBox34";
            this.textBox34.ReadOnly = true;
            this.textBox34.Size = new System.Drawing.Size(37, 18);
            this.textBox34.TabIndex = 15;
            this.textBox34.Text = "方式";
            // 
            // cmb_NeighborhoodMethod
            // 
            this.cmb_NeighborhoodMethod.FormattingEnabled = true;
            this.cmb_NeighborhoodMethod.Location = new System.Drawing.Point(56, 55);
            this.cmb_NeighborhoodMethod.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_NeighborhoodMethod.Name = "cmb_NeighborhoodMethod";
            this.cmb_NeighborhoodMethod.Size = new System.Drawing.Size(116, 23);
            this.cmb_NeighborhoodMethod.TabIndex = 14;
            // 
            // textBox37
            // 
            this.textBox37.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox37.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox37.Location = new System.Drawing.Point(13, 24);
            this.textBox37.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox37.Name = "textBox37";
            this.textBox37.ReadOnly = true;
            this.textBox37.Size = new System.Drawing.Size(37, 18);
            this.textBox37.TabIndex = 10;
            this.textBox37.Text = "栅格";
            // 
            // cmb_NeighborhoodLayer
            // 
            this.cmb_NeighborhoodLayer.FormattingEnabled = true;
            this.cmb_NeighborhoodLayer.Location = new System.Drawing.Point(56, 21);
            this.cmb_NeighborhoodLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_NeighborhoodLayer.Name = "cmb_NeighborhoodLayer";
            this.cmb_NeighborhoodLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_NeighborhoodLayer.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Visibility);
            this.groupBox2.Controls.Add(this.textBox35);
            this.groupBox2.Controls.Add(this.cmb_VisibilityLayer);
            this.groupBox2.Controls.Add(this.btn_LineOfSight);
            this.groupBox2.Controls.Add(this.textBox36);
            this.groupBox2.Controls.Add(this.cmb_LineOfSightLayer);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox2.Location = new System.Drawing.Point(5, 129);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(309, 88);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "通视/视域";
            // 
            // btn_Visibility
            // 
            this.btn_Visibility.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Visibility.Location = new System.Drawing.Point(191, 52);
            this.btn_Visibility.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Visibility.Name = "btn_Visibility";
            this.btn_Visibility.Size = new System.Drawing.Size(93, 28);
            this.btn_Visibility.TabIndex = 16;
            this.btn_Visibility.Text = "视域分析";
            this.btn_Visibility.UseVisualStyleBackColor = true;
            this.btn_Visibility.Click += new System.EventHandler(this.btn_Visibility_Click);
            // 
            // textBox35
            // 
            this.textBox35.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox35.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox35.Location = new System.Drawing.Point(13, 58);
            this.textBox35.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox35.Name = "textBox35";
            this.textBox35.ReadOnly = true;
            this.textBox35.Size = new System.Drawing.Size(37, 18);
            this.textBox35.TabIndex = 15;
            this.textBox35.Text = "DEM";
            // 
            // cmb_VisibilityLayer
            // 
            this.cmb_VisibilityLayer.FormattingEnabled = true;
            this.cmb_VisibilityLayer.Location = new System.Drawing.Point(56, 55);
            this.cmb_VisibilityLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_VisibilityLayer.Name = "cmb_VisibilityLayer";
            this.cmb_VisibilityLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_VisibilityLayer.TabIndex = 14;
            // 
            // btn_LineOfSight
            // 
            this.btn_LineOfSight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_LineOfSight.Location = new System.Drawing.Point(191, 18);
            this.btn_LineOfSight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_LineOfSight.Name = "btn_LineOfSight";
            this.btn_LineOfSight.Size = new System.Drawing.Size(93, 28);
            this.btn_LineOfSight.TabIndex = 13;
            this.btn_LineOfSight.Text = "通视分析";
            this.btn_LineOfSight.UseVisualStyleBackColor = true;
            this.btn_LineOfSight.Click += new System.EventHandler(this.btn_LineOfSight_Click);
            // 
            // textBox36
            // 
            this.textBox36.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox36.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox36.Location = new System.Drawing.Point(13, 24);
            this.textBox36.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox36.Name = "textBox36";
            this.textBox36.ReadOnly = true;
            this.textBox36.Size = new System.Drawing.Size(37, 18);
            this.textBox36.TabIndex = 10;
            this.textBox36.Text = "DEM";
            // 
            // cmb_LineOfSightLayer
            // 
            this.cmb_LineOfSightLayer.FormattingEnabled = true;
            this.cmb_LineOfSightLayer.Location = new System.Drawing.Point(56, 21);
            this.cmb_LineOfSightLayer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_LineOfSightLayer.Name = "cmb_LineOfSightLayer";
            this.cmb_LineOfSightLayer.Size = new System.Drawing.Size(116, 23);
            this.cmb_LineOfSightLayer.TabIndex = 0;
            // 
            // grb_HillShade_Slope_Aspect
            // 
            this.grb_HillShade_Slope_Aspect.Controls.Add(this.btn_Aspect);
            this.grb_HillShade_Slope_Aspect.Controls.Add(this.textBox32);
            this.grb_HillShade_Slope_Aspect.Controls.Add(this.cmb_Aspect);
            this.grb_HillShade_Slope_Aspect.Controls.Add(this.btn_Slope);
            this.grb_HillShade_Slope_Aspect.Controls.Add(this.textBox31);
            this.grb_HillShade_Slope_Aspect.Controls.Add(this.cmb_Slope);
            this.grb_HillShade_Slope_Aspect.Controls.Add(this.btn_HillShade);
            this.grb_HillShade_Slope_Aspect.Controls.Add(this.textBox33);
            this.grb_HillShade_Slope_Aspect.Controls.Add(this.cmb_HillShade);
            this.grb_HillShade_Slope_Aspect.ForeColor = System.Drawing.SystemColors.Highlight;
            this.grb_HillShade_Slope_Aspect.Location = new System.Drawing.Point(5, 8);
            this.grb_HillShade_Slope_Aspect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_HillShade_Slope_Aspect.Name = "grb_HillShade_Slope_Aspect";
            this.grb_HillShade_Slope_Aspect.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grb_HillShade_Slope_Aspect.Size = new System.Drawing.Size(309, 118);
            this.grb_HillShade_Slope_Aspect.TabIndex = 2;
            this.grb_HillShade_Slope_Aspect.TabStop = false;
            this.grb_HillShade_Slope_Aspect.Text = "山体阴影/坡度/坡向";
            // 
            // btn_Aspect
            // 
            this.btn_Aspect.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Aspect.Location = new System.Drawing.Point(191, 86);
            this.btn_Aspect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Aspect.Name = "btn_Aspect";
            this.btn_Aspect.Size = new System.Drawing.Size(93, 28);
            this.btn_Aspect.TabIndex = 19;
            this.btn_Aspect.Text = "坡向函数";
            this.btn_Aspect.UseVisualStyleBackColor = true;
            this.btn_Aspect.Click += new System.EventHandler(this.btn_Aspect_Click);
            // 
            // textBox32
            // 
            this.textBox32.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox32.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox32.Location = new System.Drawing.Point(13, 92);
            this.textBox32.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox32.Name = "textBox32";
            this.textBox32.ReadOnly = true;
            this.textBox32.Size = new System.Drawing.Size(37, 18);
            this.textBox32.TabIndex = 18;
            this.textBox32.Text = "DEM";
            // 
            // cmb_Aspect
            // 
            this.cmb_Aspect.FormattingEnabled = true;
            this.cmb_Aspect.Location = new System.Drawing.Point(56, 90);
            this.cmb_Aspect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Aspect.Name = "cmb_Aspect";
            this.cmb_Aspect.Size = new System.Drawing.Size(116, 23);
            this.cmb_Aspect.TabIndex = 17;
            // 
            // btn_Slope
            // 
            this.btn_Slope.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Slope.Location = new System.Drawing.Point(191, 52);
            this.btn_Slope.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Slope.Name = "btn_Slope";
            this.btn_Slope.Size = new System.Drawing.Size(93, 28);
            this.btn_Slope.TabIndex = 16;
            this.btn_Slope.Text = "坡度函数";
            this.btn_Slope.UseVisualStyleBackColor = true;
            this.btn_Slope.Click += new System.EventHandler(this.btn_Slope_Click);
            // 
            // textBox31
            // 
            this.textBox31.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox31.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox31.Location = new System.Drawing.Point(13, 58);
            this.textBox31.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox31.Name = "textBox31";
            this.textBox31.ReadOnly = true;
            this.textBox31.Size = new System.Drawing.Size(37, 18);
            this.textBox31.TabIndex = 15;
            this.textBox31.Text = "DEM";
            // 
            // cmb_Slope
            // 
            this.cmb_Slope.FormattingEnabled = true;
            this.cmb_Slope.Location = new System.Drawing.Point(56, 55);
            this.cmb_Slope.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Slope.Name = "cmb_Slope";
            this.cmb_Slope.Size = new System.Drawing.Size(116, 23);
            this.cmb_Slope.TabIndex = 14;
            // 
            // btn_HillShade
            // 
            this.btn_HillShade.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_HillShade.Location = new System.Drawing.Point(191, 18);
            this.btn_HillShade.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_HillShade.Name = "btn_HillShade";
            this.btn_HillShade.Size = new System.Drawing.Size(93, 28);
            this.btn_HillShade.TabIndex = 13;
            this.btn_HillShade.Text = "山体阴影";
            this.btn_HillShade.UseVisualStyleBackColor = true;
            this.btn_HillShade.Click += new System.EventHandler(this.btn_HillShade_Click);
            // 
            // textBox33
            // 
            this.textBox33.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox33.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox33.Location = new System.Drawing.Point(13, 24);
            this.textBox33.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox33.Name = "textBox33";
            this.textBox33.ReadOnly = true;
            this.textBox33.Size = new System.Drawing.Size(37, 18);
            this.textBox33.TabIndex = 10;
            this.textBox33.Text = "DEM";
            // 
            // cmb_HillShade
            // 
            this.cmb_HillShade.FormattingEnabled = true;
            this.cmb_HillShade.Location = new System.Drawing.Point(56, 21);
            this.cmb_HillShade.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_HillShade.Name = "cmb_HillShade";
            this.cmb_HillShade.Size = new System.Drawing.Size(116, 23);
            this.cmb_HillShade.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.Btn_ConnectDB,
            this.miRasterFunctionEditor});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1125, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewDoc,
            this.menuOpenDoc,
            this.menuSaveDoc,
            this.menuSaveAs,
            this.menuSeparator,
            this.menuExitApp});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(46, 24);
            this.menuFile.Text = "File";
            // 
            // menuNewDoc
            // 
            this.menuNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuNewDoc.Image")));
            this.menuNewDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuNewDoc.Name = "menuNewDoc";
            this.menuNewDoc.Size = new System.Drawing.Size(210, 24);
            this.menuNewDoc.Text = "New Document";
            this.menuNewDoc.Click += new System.EventHandler(this.menuNewDoc_Click);
            // 
            // menuOpenDoc
            // 
            this.menuOpenDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuOpenDoc.Image")));
            this.menuOpenDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuOpenDoc.Name = "menuOpenDoc";
            this.menuOpenDoc.Size = new System.Drawing.Size(210, 24);
            this.menuOpenDoc.Text = "Open Document...";
            this.menuOpenDoc.Click += new System.EventHandler(this.menuOpenDoc_Click);
            // 
            // menuSaveDoc
            // 
            this.menuSaveDoc.Image = ((System.Drawing.Image)(resources.GetObject("menuSaveDoc.Image")));
            this.menuSaveDoc.ImageTransparentColor = System.Drawing.Color.White;
            this.menuSaveDoc.Name = "menuSaveDoc";
            this.menuSaveDoc.Size = new System.Drawing.Size(210, 24);
            this.menuSaveDoc.Text = "SaveDocument";
            this.menuSaveDoc.Click += new System.EventHandler(this.menuSaveDoc_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(210, 24);
            this.menuSaveAs.Text = "Save As...";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuSeparator
            // 
            this.menuSeparator.Name = "menuSeparator";
            this.menuSeparator.Size = new System.Drawing.Size(207, 6);
            // 
            // menuExitApp
            // 
            this.menuExitApp.Name = "menuExitApp";
            this.menuExitApp.Size = new System.Drawing.Size(210, 24);
            this.menuExitApp.Text = "Exit";
            this.menuExitApp.Click += new System.EventHandler(this.menuExitApp_Click);
            // 
            // Btn_ConnectDB
            // 
            this.Btn_ConnectDB.Name = "Btn_ConnectDB";
            this.Btn_ConnectDB.Size = new System.Drawing.Size(96, 24);
            this.Btn_ConnectDB.Text = "连接数据库";
            this.Btn_ConnectDB.Click += new System.EventHandler(this.Btn_ConnectDB_Click);
            // 
            // miRasterFunctionEditor
            // 
            this.miRasterFunctionEditor.Name = "miRasterFunctionEditor";
            this.miRasterFunctionEditor.Size = new System.Drawing.Size(126, 24);
            this.miRasterFunctionEditor.Text = "栅格函数编辑器";
            this.miRasterFunctionEditor.Click += new System.EventHandler(this.miRasterFunctionEditor_Click);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 28);
            this.axToolbarControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1125, 28);
            this.axToolbarControl1.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 56);
            this.splitter1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 585);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarXY});
            this.statusStrip1.Location = new System.Drawing.Point(3, 616);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(3, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1122, 25);
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusBar1";
            // 
            // statusBarXY
            // 
            this.statusBarXY.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.statusBarXY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBarXY.Name = "statusBarXY";
            this.statusBarXY.Size = new System.Drawing.Size(71, 20);
            this.statusBarXY.Text = "Test 123";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(466, 278);
            this.axLicenseControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_ZoomToLayer,
            this.TSMI_DeleteLayer,
            this.tsmiEditRasterFunction});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 76);
            // 
            // TSMI_ZoomToLayer
            // 
            this.TSMI_ZoomToLayer.Name = "TSMI_ZoomToLayer";
            this.TSMI_ZoomToLayer.Size = new System.Drawing.Size(183, 24);
            this.TSMI_ZoomToLayer.Text = "缩放至当前图层";
            this.TSMI_ZoomToLayer.Click += new System.EventHandler(this.TSMI_ZoomToLayer_Click);
            // 
            // TSMI_DeleteLayer
            // 
            this.TSMI_DeleteLayer.Name = "TSMI_DeleteLayer";
            this.TSMI_DeleteLayer.Size = new System.Drawing.Size(183, 24);
            this.TSMI_DeleteLayer.Text = "删除当前图层";
            this.TSMI_DeleteLayer.Click += new System.EventHandler(this.TSMI_DeleteLayer_Click);
            // 
            // tsmiEditRasterFunction
            // 
            this.tsmiEditRasterFunction.Name = "tsmiEditRasterFunction";
            this.tsmiEditRasterFunction.Size = new System.Drawing.Size(183, 24);
            this.tsmiEditRasterFunction.Text = "编辑栅格函数";
            this.tsmiEditRasterFunction.Click += new System.EventHandler(this.tsmiEditRasterFunction_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 56);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(tbc_ImageAnalysis);
            this.splitContainer1.Size = new System.Drawing.Size(1122, 560);
            this.splitContainer1.SplitterDistance = 828;
            this.splitContainer1.TabIndex = 9;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.axMapControl1);
            this.splitContainer2.Size = new System.Drawing.Size(828, 560);
            this.splitContainer2.SplitterDistance = 210;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.axMapControl2);
            this.splitContainer3.Size = new System.Drawing.Size(210, 560);
            this.splitContainer3.SplitterDistance = 276;
            this.splitContainer3.TabIndex = 0;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(210, 276);
            this.axTOCControl1.TabIndex = 0;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            // 
            // axMapControl2
            // 
            this.axMapControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl2.Location = new System.Drawing.Point(0, 0);
            this.axMapControl2.Name = "axMapControl2";
            this.axMapControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl2.OcxState")));
            this.axMapControl2.Size = new System.Drawing.Size(210, 280);
            this.axMapControl2.TabIndex = 0;
            this.axMapControl2.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl2_OnMouseDown);
            this.axMapControl2.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl2_OnMouseMove);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(614, 560);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 641);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "ArcEngine Controls Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            tbc_ImageAnalysis.ResumeLayout(false);
            this.tbP_DataManagement.ResumeLayout(false);
            this.gb_ImportRasterImage.ResumeLayout(false);
            this.gb_ImportRasterImage.PerformLayout();
            this.gb_LoadRasterImage.ResumeLayout(false);
            this.gb_LoadRasterImage.PerformLayout();
            this.gb_CreateRasterCatalog.ResumeLayout(false);
            this.gb_CreateRasterCatalog.PerformLayout();
            this.tbp_ImageProcess.ResumeLayout(false);
            this.grb_RGBLayer.ResumeLayout(false);
            this.grb_RGBLayer.PerformLayout();
            this.grb_RenderLayer.ResumeLayout(false);
            this.grb_RenderLayer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ToColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_ColorBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_FromColor)).EndInit();
            this.grb_StretchLayer.ResumeLayout(false);
            this.grb_StretchLayer.PerformLayout();
            this.grb_DrawHisLayer.ResumeLayout(false);
            this.grb_DrawHisLayer.PerformLayout();
            this.grb_NDVILayer.ResumeLayout(false);
            this.grb_NDVILayer.PerformLayout();
            this.grb_StatisticsLayer.ResumeLayout(false);
            this.grb_StatisticsLayer.PerformLayout();
            this.tbp_ImageAnalysis.ResumeLayout(false);
            this.grb_Transform.ResumeLayout(false);
            this.grb_Transform.PerformLayout();
            this.grb_Filter.ResumeLayout(false);
            this.grb_Filter.PerformLayout();
            this.grb_Mosaic.ResumeLayout(false);
            this.grb_Mosaic.PerformLayout();
            this.grb_PanSharpen.ResumeLayout(false);
            this.grb_PanSharpen.PerformLayout();
            this.grb_ClipFeature.ResumeLayout(false);
            this.grb_ClipFeature.PerformLayout();
            this.grb_ImageClass.ResumeLayout(false);
            this.grb_ImageClass.PerformLayout();
            this.tbp_SpatialAnalysis.ResumeLayout(false);
            this.grb_contour_Voronoi.ResumeLayout(false);
            this.grb_contour_Voronoi.PerformLayout();
            this.grb_CreateTIN.ResumeLayout(false);
            this.grb_CreateTIN.PerformLayout();
            this.grb_Extraction.ResumeLayout(false);
            this.grb_Extraction.PerformLayout();
            this.grb_Neighborhood.ResumeLayout(false);
            this.grb_Neighborhood.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grb_HillShade_Slope_Aspect.ResumeLayout(false);
            this.grb_HillShade_Slope_Aspect.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuNewDoc;
        private System.Windows.Forms.ToolStripMenuItem menuOpenDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveDoc;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuExitApp;
        private System.Windows.Forms.ToolStripSeparator menuSeparator;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarXY;
        private System.Windows.Forms.ToolStripMenuItem Btn_ConnectDB;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.ColorDialog colorDialog3;
        private System.Windows.Forms.ColorDialog colorDialog4;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMI_ZoomToLayer;
        private System.Windows.Forms.ToolStripMenuItem TSMI_DeleteLayer;
        private System.Windows.Forms.TabPage tbp_ImageAnalysis;
        private System.Windows.Forms.TabPage tbp_ImageProcess;
        private System.Windows.Forms.GroupBox grb_RGBLayer;
        private System.Windows.Forms.Button btn_RGB;
        private System.Windows.Forms.ComboBox cmb_GBand;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.ComboBox cmb_BBand;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.ComboBox cmb_RBand;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.ComboBox cmb_RGBLayer;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.GroupBox grb_RenderLayer;
        private System.Windows.Forms.Button btn_Render;
        private System.Windows.Forms.PictureBox pb_ToColor;
        private System.Windows.Forms.PictureBox pb_ColorBar;
        private System.Windows.Forms.PictureBox pb_FromColor;
        private System.Windows.Forms.ComboBox cmb_RenderBand;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.ComboBox cmb_RenderLayer;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.GroupBox grb_StretchLayer;
        private System.Windows.Forms.Button btn_Stretch;
        private System.Windows.Forms.ComboBox cmb_StretchMethod;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.ComboBox cmb_StretchBand;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.ComboBox cmb_StretchLayer;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.GroupBox grb_DrawHisLayer;
        private System.Windows.Forms.Button btn_MultiBandHis;
        private System.Windows.Forms.Button btn_SingleBandHis;
        private System.Windows.Forms.ComboBox cmb_DrawHisBand;
        private System.Windows.Forms.ComboBox cmb_DrawHisLayer;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.GroupBox grb_NDVILayer;
        private System.Windows.Forms.Button btn_CalculateNDVI_;
        private System.Windows.Forms.ComboBox cmb_NDVILayer;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.GroupBox grb_StatisticsLayer;
        private System.Windows.Forms.Button btn_Statistics;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox cmb_StatisticsBand;
        private System.Windows.Forms.ComboBox cmb_StatistiicsLayer;
        private System.Windows.Forms.TabPage tbP_DataManagement;
        private System.Windows.Forms.GroupBox gb_ImportRasterImage;
        private System.Windows.Forms.Button btn_ImportRstDataset;
        private System.Windows.Forms.TextBox txb_ImportRstDataset;
        private System.Windows.Forms.ComboBox cmb_ImportRstCatalog;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox gb_LoadRasterImage;
        private System.Windows.Forms.Button btn_LoadRstDataset;
        private System.Windows.Forms.ComboBox cmb_LoadRstDataset;
        private System.Windows.Forms.ComboBox cmb_LoadRstCatalog;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox gb_CreateRasterCatalog;
        private System.Windows.Forms.Button btn_NewRstCatalog;
        private System.Windows.Forms.TextBox txb_NewRstCatalog;
        private System.Windows.Forms.TabPage tbp_SpatialAnalysis;
        private System.Windows.Forms.GroupBox grb_ImageClass;
        private System.Windows.Forms.TextBox txb_ClassifyNumber;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.ComboBox cmb_ClassifyLayer;
        private System.Windows.Forms.Button btn_Classify;
        private System.Windows.Forms.Button btn_After_Classify;
        private System.Windows.Forms.GroupBox grb_ClipFeature;
        private System.Windows.Forms.Button btn_Clip;
        private System.Windows.Forms.TextBox txb_ClipFeature;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.ComboBox cmb_ClipFeatureLayer;
        private System.Windows.Forms.GroupBox grb_PanSharpen;
        private System.Windows.Forms.ComboBox cmb_PanSharpenMultiLayer;
        private System.Windows.Forms.Button btn_PanSharpen;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.ComboBox cmb_PanSharpenSigleLayer;
        private System.Windows.Forms.GroupBox grb_Mosaic;
        private System.Windows.Forms.Button btn_Mosaic;
        private System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.ComboBox cmb_MosaicCatalog;
        private System.Windows.Forms.GroupBox grb_Transform;
        private System.Windows.Forms.TextBox txb_TransformAngle;
        private System.Windows.Forms.TextBox textBox30;
        private System.Windows.Forms.ComboBox cmb_TransformMethod;
        private System.Windows.Forms.TextBox textBox28;
        private System.Windows.Forms.Button btn_Transform;
        private System.Windows.Forms.TextBox textBox29;
        private System.Windows.Forms.ComboBox cmb_TransformLayer;
        private System.Windows.Forms.GroupBox grb_Filter;
        private System.Windows.Forms.ComboBox cmb_FliterMethod;
        private System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.Button btn_Filter;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.ComboBox cmb_FliterLayer;
        private System.Windows.Forms.GroupBox grb_HillShade_Slope_Aspect;
        private System.Windows.Forms.Button btn_Aspect;
        private System.Windows.Forms.TextBox textBox32;
        private System.Windows.Forms.ComboBox cmb_Aspect;
        private System.Windows.Forms.Button btn_Slope;
        private System.Windows.Forms.TextBox textBox31;
        private System.Windows.Forms.ComboBox cmb_Slope;
        private System.Windows.Forms.Button btn_HillShade;
        private System.Windows.Forms.TextBox textBox33;
        private System.Windows.Forms.ComboBox cmb_HillShade;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Visibility;
        private System.Windows.Forms.TextBox textBox35;
        private System.Windows.Forms.ComboBox cmb_VisibilityLayer;
        private System.Windows.Forms.Button btn_LineOfSight;
        private System.Windows.Forms.TextBox textBox36;
        private System.Windows.Forms.ComboBox cmb_LineOfSightLayer;
        private System.Windows.Forms.GroupBox grb_Neighborhood;
        private System.Windows.Forms.Button btn_Neighborhood;
        private System.Windows.Forms.TextBox textBox34;
        private System.Windows.Forms.ComboBox cmb_NeighborhoodMethod;
        private System.Windows.Forms.TextBox textBox37;
        private System.Windows.Forms.ComboBox cmb_NeighborhoodLayer;
        private System.Windows.Forms.GroupBox grb_Extraction;
        private System.Windows.Forms.Button btn_Extraction;
        private System.Windows.Forms.TextBox textBox39;
        private System.Windows.Forms.ComboBox cmb_Extraction;
        private System.Windows.Forms.GroupBox grb_contour_Voronoi;
        private System.Windows.Forms.Button btn_Voronoi;
        private System.Windows.Forms.TextBox textBox40;
        private System.Windows.Forms.ComboBox cmb_TinVoronoi;
        private System.Windows.Forms.Button btn_tinContour;
        private System.Windows.Forms.TextBox textBox41;
        private System.Windows.Forms.ComboBox cmb_tinContour;
        private System.Windows.Forms.Button btn_DEMContour;
        private System.Windows.Forms.TextBox textBox42;
        private System.Windows.Forms.ComboBox cmb_DEMContour;
        private System.Windows.Forms.GroupBox grb_CreateTIN;
        private System.Windows.Forms.Button btn_CreateTINAuto;
        private System.Windows.Forms.Button btm_CreateTin;
        private System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.ComboBox cmb_CreateTINLayer;
        private System.Windows.Forms.Button btn_Statistics_Bottom;
        private System.Windows.Forms.Button btn_NDVI_Bottom;
        private System.Windows.Forms.ToolStripMenuItem miRasterFunctionEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditRasterFunction;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl2;
    }
}

