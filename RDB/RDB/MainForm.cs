using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;


using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Analyst3D;

namespace RDB
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
            //色带初始化
            m_FromColor = Color.Red;  //初始化色块颜色（左）
            m_ToColor = Color.Blue; //初始化色块颜色（右）

            RefreshColors(m_FromColor, m_ToColor);
            //添加单波段灰度增强方法
            cmb_StretchMethod.Items.Add("默认拉伸");
            cmb_StretchMethod.Items.Add("标准差拉伸");
            cmb_StretchMethod.Items.Add("最大最小值拉伸");
            cmb_StretchMethod.Items.Add("百分比最大最小拉伸");
            cmb_StretchMethod.Items.Add("直方图拉伸");
            cmb_StretchMethod.Items.Add("直方图匹配");
            cmb_StretchMethod.SelectedIndex = 0;
            //添加图像变化方法
            cmb_TransformMethod.Items.Add("翻转");
            cmb_TransformMethod.Items.Add("镜像");
            cmb_TransformMethod.Items.Add("剪裁");
            cmb_TransformMethod.Items.Add("旋转");
            cmb_TransformMethod.SelectedIndex = 0;
            //添加滤波方法
            cmb_FliterMethod.Items.Add("LineDetectionHorizontal");
            cmb_FliterMethod.Items.Add("LineDetectionVertical");
            cmb_FliterMethod.Items.Add("LineDetectionLeftDiagonal");
            cmb_FliterMethod.Items.Add("LineDetectionRightDiagonal");
            cmb_FliterMethod.Items.Add("GradientNorth");
            cmb_FliterMethod.Items.Add("GradientWest");
            cmb_FliterMethod.Items.Add("GradientEast");
            cmb_FliterMethod.Items.Add("GradientSouth");
            cmb_FliterMethod.Items.Add("GradientNorthEast");
            cmb_FliterMethod.Items.Add("GradientNorthWest");
            cmb_FliterMethod.Items.Add("SmoothArithmeticMean");
            cmb_FliterMethod.Items.Add("Smoothing3x3");
            cmb_FliterMethod.Items.Add("Smoothing5x5");
            cmb_FliterMethod.Items.Add("Sharpening3x3");
            cmb_FliterMethod.Items.Add("Sharpening5x5");
            cmb_FliterMethod.Items.Add("Laplacian3x3");
            cmb_FliterMethod.Items.Add("Laplacian5x5");
            cmb_FliterMethod.Items.Add("SobelHorizontal");
            cmb_FliterMethod.Items.Add("SobelVertical");
            cmb_FliterMethod.Items.Add("Sharpen");
            cmb_FliterMethod.Items.Add("Sharpen2");
            cmb_FliterMethod.Items.Add("PointSpread");
            cmb_FliterMethod.SelectedIndex = 0;
            //添加领域分析方法
            cmb_NeighborhoodMethod.Items.Add("3x3LowPass");
            cmb_NeighborhoodMethod.Items.Add("3x3HighPass");
            cmb_NeighborhoodMethod.Items.Add("Majority");
            cmb_NeighborhoodMethod.Items.Add("Maximum");
            cmb_NeighborhoodMethod.Items.Add("Mean");
            cmb_NeighborhoodMethod.Items.Add("Median");
            cmb_NeighborhoodMethod.Items.Add("Minimum");
            cmb_NeighborhoodMethod.Items.Add("Minority");
            cmb_NeighborhoodMethod.Items.Add("Range");
            cmb_NeighborhoodMethod.Items.Add("Std");
            cmb_NeighborhoodMethod.Items.Add("Sum");
            cmb_NeighborhoodMethod.Items.Add("Variety");
            cmb_NeighborhoodMethod.Items.Add("Length");
            cmb_NeighborhoodMethod.SelectedIndex = 0;
        }
        #endregion

  
        private IWorkspace workspace = null;//工作空间，即GeoDataBase
        private ILayer TOCRightLayer;//用于存储TOC右键选中图层
        private Color m_FromColor = Color.Red;  //初始化色块颜色（左）
        private Color m_ToColor = Color.Blue; //初始化色块颜色（右）
        private ColorDialog cd_FromColor=new ColorDialog();
        private ColorDialog cd_ToColor=new ColorDialog();
        bool fClip = false;//矩形剪裁
        bool fExtraction = false;//多边形剪裁
        bool fLineOfSight = false;//通视分析
        bool fVisibility = false;//视域分析
        bool fTIN = false;//手绘构建TIN
        ITinEdit TinEdit=new TinClass();//TIN

        private void MainForm_Load(object sender, EventArgs e)
        {
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }

        //连接SDE数据库和初始化加载下拉框内容
        private void Btn_ConnectDB_Click(object sender, EventArgs e)
        {
            try
            {
                //SDE连接数据库参数设置
                IPropertySet propertySet = new PropertySet();
                propertySet.SetProperty("SERVER", "es27");
                propertySet.SetProperty("INSTANCE", "sde:oracle11g:es27/orcl");
                propertySet.SetProperty("DATABASE", "sde");
                propertySet.SetProperty("USER", "sde");
                propertySet.SetProperty("PASSWORD", "123");
                propertySet.SetProperty("VERSION", "sde.DEFAULT");
                propertySet.SetProperty("AUTHENTICATION_MORE", "DBMS");

                //指定SDE工作空间factory
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
                workspace = workspaceFactory.Open(propertySet, 0);

                //if (workspace != null) MessageBox.Show(workspace.Type.ToString());

                //清除栅格目录下拉框里面的选项
                cmb_LoadRstCatalog.Items.Clear();
                cmb_LoadRstCatalog.Items.Add("非栅格目录（工作空间）");
                cmb_ImportRstCatalog.Items.Clear();
                cmb_ImportRstCatalog.Items.Add("非栅格目录（工作空间）");
                //获取数据库中的栅格目录，除去SDE前缀
                IEnumDatasetName enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTRasterCatalog);
                IDatasetName datasetName = enumDatasetName.Next();
                while (datasetName != null)
                {
                    cmb_LoadRstCatalog.Items.Add(datasetName.Name.Substring(datasetName.Name.LastIndexOf('.') + 1));
                    cmb_ImportRstCatalog.Items.Add(datasetName.Name.Substring(datasetName.Name.LastIndexOf('.') + 1));
                    datasetName = enumDatasetName.Next();
                }
                //设置下拉框默认选项为非栅格目录（工作空间）
                if (cmb_LoadRstCatalog.Items.Count > 0)
                {
                    cmb_LoadRstCatalog.SelectedIndex = 0;
                }
                if (cmb_ImportRstCatalog.Items.Count > 0)
                {
                    cmb_ImportRstCatalog.SelectedIndex = 0;
                }
            }
            catch (System.Exception ex)//处理异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IFields CreateFields(string rasterFldName, string shapeFldName, ISpatialReference rasterSF,ISpatialReference shapeSF)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = fields as IFieldsEdit;

            IField field;
            IFieldEdit fieldEdit;

            //添加OID字段，注意字段type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = "ObjectID";
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(field);

            //添加name字段，注意字段type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = "NAME";
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            fieldsEdit.AddField(field);

            //添加raster字段，注意字段type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = rasterFldName;
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeRaster;

            //IRasterDef接口定义栅格字段
            IRasterDef rasterDef =new RasterDefClass();
            rasterDef.SpatialReference=rasterSF;
            ((IFieldEdit2)fieldEdit).RasterDef = rasterDef;
            fieldsEdit.AddField(field);

            //添加shape字段，注意字段type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = shapeFldName;
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;

            //IGeometryDef和IGeometryDefEdit即可偶定义和编辑几何字段
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = geometryDef as IGeometryDefEdit;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            geometryDefEdit.SpatialReference_2 = shapeSF;
            ((IFieldEdit2)fieldEdit).GeometryDef_2 = geometryDef;
            fieldsEdit.AddField(field);

            //添加xml（元数据）字段，注意字段Type
            field = new FieldClass();
            fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = "METADATA";
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeBlob;
            fieldsEdit.AddField(field);

            return fields;
            
        }

        //创建栅格目录
        private void btn_NewRstCatalog_Click(object sender, EventArgs e)
        {
            if (txb_NewRstCatalog.Text.Trim() == "")
            {
                MessageBox.Show("请输入栅格目录名称！");
            }
            else
            {
                string rasCatalogName = txb_NewRstCatalog.Text.Trim();
                IRasterWorkspaceEx rasterWorkspaceEx = workspace as IRasterWorkspaceEx;
                //定义空间参考，采用WGS84投影
                ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference spatialReference = spatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
                spatialReference.SetDomain(-180, 180, -90, 90);
                //判断栅格目录是否存在
                IEnumDatasetName enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTRasterCatalog);
                IDatasetName datasetName = enumDatasetName.Next();
                bool isExit = false;
                //循环遍历判断是否已存在该栅格目录
                while (datasetName != null)
                {
                    if (datasetName.Name.Substring(datasetName.Name.LastIndexOf('.') + 1) == rasCatalogName)
                    {
                        isExit = true;
                        MessageBox.Show("栅格目录已经存在！");
                        txb_NewRstCatalog.Focus();
                        return;
                    }
                    datasetName = enumDatasetName.Next();
                }
                //若不存在，则建立新的栅格目录
                if (isExit == false)
                {
                    //创建栅格目录字段集
                    IFields fields = CreateFields("RASTER", "SHAPE", spatialReference, spatialReference);

                    rasterWorkspaceEx.CreateRasterCatalog(rasCatalogName, fields, "SHAPE", "RASTER", "DEFAULTS");
                    //将新建的栅格目录添加到下拉列表中，并设置当前栅格目录
                    cmb_LoadRstCatalog.Items.Add(rasCatalogName);
                    cmb_LoadRstCatalog.SelectedIndex = cmb_LoadRstCatalog.Items.Count - 1;
                    cmb_ImportRstCatalog.Items.Add(rasCatalogName);
                    cmb_ImportRstCatalog.SelectedIndex = cmb_ImportRstCatalog.Items.Count - 1;
                    cmb_LoadRstDataset.Items.Clear();
                    cmb_LoadRstDataset.Text = "";
                }
                MessageBox.Show("栅格目录创建成功！");
            }
        }

        //当选择的栅格目录发生变化，则相应的栅格图像列表也发生变化
        private void cmb_LoadRstCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rstCatalogName = cmb_LoadRstCatalog.SelectedItem.ToString();
            IEnumDatasetName enumDatasetName;
            IDatasetName datasetName;
            if (cmb_LoadRstCatalog.SelectedIndex == 0)
            {
                //清除栅格图像下拉框中的选项
                cmb_LoadRstDataset.Items.Clear();
                //获取非栅格目录（工作空间）中的栅格图像
                enumDatasetName = workspace.get_DatasetNames(esriDatasetType.esriDTRasterDataset);
                datasetName = enumDatasetName.Next();
                while (datasetName != null)
                {
                    cmb_LoadRstDataset.Items.Add(datasetName.Name.Substring(datasetName.Name.LastIndexOf('.') + 1));
                    datasetName = enumDatasetName.Next();
                }
                //设置下拉框默认选项为非栅格目录（工作空间）
                if (cmb_LoadRstDataset.Items.Count > 0)
                {
                    cmb_LoadRstDataset.SelectedIndex = 0;
                }
            }
            else
            {
                //接口转换IRasterWorkspaceEx
                IRasterWorkspaceEx workspaceEx = (IRasterWorkspaceEx)workspace;
                //获取栅格目录
                IRasterCatalog rasterCatalog = workspaceEx.OpenRasterCatalog(rstCatalogName);
                //接口转换IFeatureClass
                IFeatureClass featureClass = (IFeatureClass)rasterCatalog;
                //接口转换ITable
                ITable pTable = featureClass as ITable;
                //执行查询获取指针
                ICursor cursor = pTable.Search(null, true) as ICursor;
                IRow pRow = null;
                //清除下拉框的选项
                cmb_LoadRstDataset.Items.Clear();
                cmb_LoadRstDataset.Text = "";
                while ((pRow = cursor.NextRow()) != null)
                {
                    int idxName = pRow.Fields.FindField("NAME");
                    cmb_LoadRstDataset.Items.Add(pRow.get_Value(idxName).ToString());
                }
                //设置默认选项
                if (cmb_LoadRstDataset.Items.Count > 0)
                {
                    cmb_LoadRstDataset.SelectedIndex = 0;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);

            }
        }

        //根据选定的栅格目录和栅格图像来加载相应的图像
        private void btn_LoadRstDataset_Click(object sender, EventArgs e)
        {
           
            if (cmb_LoadRstCatalog.SelectedIndex == 0)
            {
                string rstDatasetName = cmb_LoadRstDataset.SelectedItem.ToString();
                //接口转换IRasterWorkspaceEx
                IRasterWorkspaceEx workspaceEx = (IRasterWorkspaceEx)workspace;
                //获取栅格数据集                
                IRasterDataset rasterDataset = workspaceEx.OpenRasterDataset(rstDatasetName);
                //利用栅格目录项创建栅格图层
                IRasterLayer rasterLayer = new RasterLayerClass();
                rasterLayer.CreateFromDataset(rasterDataset);
                rasterLayer.Name = rstDatasetName;
                ILayer layer = rasterLayer as ILayer;
                //将图层加载到MapControl中，并缩放到当前图层
                axMapControl1.AddLayer(layer);
                axMapControl1.ActiveView.Extent = layer.AreaOfInterest;
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();
                iniCmbItems();
            }
            else
            {
                string rstCatalogName = cmb_LoadRstCatalog.SelectedItem.ToString();
                string rstDatasetName = cmb_LoadRstDataset.SelectedItem.ToString();
                //接口转换IRasterWorkspaceEx
                IRasterWorkspaceEx workspaceEx = (IRasterWorkspaceEx)workspace;
                //获取栅格目录
                IRasterCatalog rasterCatalog = workspaceEx.OpenRasterCatalog(rstCatalogName);
                //接口转换IFeatureClass
                IFeatureClass featureClass = (IFeatureClass)rasterCatalog;
                //接口转换ITable
                ITable pTable = featureClass as ITable;
                //查询条件过滤器QueryFilterClass
                IQueryFilter qf = new QueryFilterClass();
                qf.SubFields = "OBJECTID";
                qf.WhereClause = "NAME='" + rstDatasetName + "'";
                //执行查询获取指针
                ICursor cursor = pTable.Search(qf, true) as ICursor;
                IRow pRow = null;
                int rstOID = 0;
                //判断读取第一条记录
                if ((pRow = cursor.NextRow()) != null)
                {
                    int idxfld = pRow.Fields.FindField("OBJECTID");
                    rstOID = int.Parse(pRow.get_Value(idxfld).ToString());
                    //获取检索到的栅格目录项
                    IRasterCatalogItem rasterCatalogItem = (IRasterCatalogItem)featureClass.GetFeature(rstOID);
                    //利用栅格目录项创建栅格图层
                    IRasterLayer rasterLayer = new RasterLayerClass();
                    rasterLayer.CreateFromDataset(rasterCatalogItem.RasterDataset);
                    rasterLayer.Name = rstDatasetName;
                    ILayer layer = rasterLayer as ILayer;
                    //将图层加载到MapControl中，并缩放到当前图层
                    axMapControl1.AddLayer(layer);
                    axMapControl1.ActiveView.Extent = layer.AreaOfInterest;
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                }
                iniCmbItems();
                //释放内存空间
                System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
            }
         
        }

        private void txb_ImportRstDataset_MouseDown(object sender, MouseEventArgs e)
        {
            //打开文件选择对话框，设置对话框属性
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imag file (*.img)|*img|Tiff file(*.tif)|*.tif|Openflight file (*.flt)|*.flt";
            openFileDialog.Title = "打开影像数据";
            openFileDialog.Multiselect = false;
            string filename = "";
            //如果对话框已成功选择文件，将文件路径信息填写到输入框内
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
                txb_ImportRstDataset.Text = filename;
            }
        }

        private void btn_ImportRstDataset_Click(object sender, EventArgs e)
        {
            //获取栅格图像的路径和文件名字
            string fileName = txb_ImportRstDataset.Text;
            if (fileName == "")
            {
                MessageBox.Show("文件名不能为空！");
                return;
            }

            FileInfo fileInfo = new FileInfo(fileName);
            string filePath = fileInfo.DirectoryName;
            string file = fileInfo.Name;
            string strOutName = file.Substring(0, file.LastIndexOf("."));

            //根据路径和文件名字获取栅格数据集
            if (cmb_ImportRstCatalog.SelectedIndex == 0)
            {
                //判断是否有重名现象
                IWorkspace2 workspace2 = workspace as IWorkspace2;
                //如果名称已存在
                if (workspace2.get_NameExists(esriDatasetType.esriDTRasterDataset, strOutName))
                {
                    DialogResult result;
                    result = MessageBox.Show(this, "名为 " + strOutName + " 的栅格文件在数据库中已存在！" + "\n是否覆盖", "相同文件名",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
                    //如果选择确认删除，则覆盖原栅格数据
                    if (result == DialogResult.Yes)
                    {
                        IRasterWorkspaceEx rstWorkspaceEx = workspace as IRasterWorkspaceEx;
                        IDataset datasetDel = rstWorkspaceEx.OpenRasterDataset(strOutName) as IDataset;
                        //调用IDataset接口的Delete接口实现已存在栅格数据集的删除
                        datasetDel.Delete();
                        datasetDel = null;
                    }
                    else if(result == DialogResult.No)
                    {
                        MessageBox.Show("工作空间已存在同名栅格数据集，不覆盖不能导入！");
                        return;                    
                    }   
                }
                //根据选择的栅格图像的路径打开栅格工作空间
                IWorkspaceFactory rstWorkspaceFactoryImport = new RasterWorkspaceFactoryClass();
                IRasterWorkspace rstWorkspaceImport = (IRasterWorkspace)rstWorkspaceFactoryImport.OpenFromFile(filePath, 0);
                IRasterDataset rstDatasetImport = null;
                //检测选择的文件路径是不是有效的栅格工作空间
                if (!(rstWorkspaceImport is IRasterWorkspace))
                {
                    MessageBox.Show("文件路径不是有效的栅格工作空间！");
                    return;
                }
                //根据选择的栅格图像名字获取栅格数据集
                rstDatasetImport = rstWorkspaceImport.OpenRasterDataset(file);
                //用IRasterDataset接口的CreateDefaultRaster方法创建空白的栅格对象
                IRaster raster = rstDatasetImport.CreateDefaultRaster();
                //IRasterProps是和栅格属性定义有关的接口
                IRasterProps rasterProp = raster as IRasterProps;
                //IRasterStorageDef接口和栅格存储参数相关
                IRasterStorageDef storageDef = new RasterStorageDefClass();
                //指定压缩类型
                storageDef.CompressionType = esriRasterCompressionType.esriRasterCompressionLZ77;
                //设置CellSize
                IPnt pnt = new PntClass();
                pnt.SetCoords(rasterProp.MeanCellSize().X, rasterProp.MeanCellSize().Y);
                storageDef.CellSize = pnt;
                //设置栅格数据集的原点，在最左上角一点的位置
                IPoint origin = new PointClass();
                origin.PutCoords(rasterProp.Extent.XMin,rasterProp.Extent.YMax);
                storageDef.Origin = origin;
                //接口转换为和栅格存储有关的ISaveAs2
                ISaveAs2 saveAs2 = (ISaveAs2)rstDatasetImport;

                //接口转化为和栅格存储属性定义相关的IRasterStorageDef2
                IRasterStorageDef2 rasterStorageDef2 = (IRasterStorageDef2)storageDef;
                //指定压缩质量，瓦片高度和宽度
                rasterStorageDef2.CompressionQuality = 100;
                rasterStorageDef2.Tiled = true;
                rasterStorageDef2.TileHeight = 128;
                rasterStorageDef2.TileWidth = 128;
                //最后调用ISaveAs2接口的SaveAsRasterDataset方法实现栅格数据集的存储
                //指定存储名字，工作空间，存储格式和相关存储属性
                saveAs2.SaveAsRasterDataset(strOutName, workspace, "GRID", rasterStorageDef2);
                //显示导入成功的消息
                MessageBox.Show("导入成功");
            }
            else
            {
                string rasterCatalogName = cmb_ImportRstCatalog.Text;
                //打开栅格工作空间
                IWorkspaceFactory pRasterWsFac = new RasterWorkspaceFactoryClass();
                IWorkspace pWs = pRasterWsFac.OpenFromFile(filePath, 0);
                if (!(pWs is IRasterWorkspace))
                {
                    MessageBox.Show("文件路径不是有效的栅格工作空间！");
                    return;
                }
                IRasterWorkspace pRasterWs = pWs as IRasterWorkspace;
                //获取栅格数据集
                IRasterDataset pRasterDs = pRasterWs.OpenRasterDataset(file);
                //创建栅格对象
                IRaster raster = pRasterDs.CreateDefaultRaster();
                IRasterProps rasterProp = raster as IRasterProps;
                //设置栅格存储参数
                IRasterStorageDef storageDef = new RasterStorageDefClass();
                storageDef.CompressionType = esriRasterCompressionType.esriRasterCompressionLZ77;
                //设置CellSize
                IPnt pnt = new PntClass();
                pnt.SetCoords(rasterProp.MeanCellSize().X, rasterProp.MeanCellSize().Y);
                storageDef.CellSize = pnt;
                //设置栅格数据集的原点，在最左上角一点位置。
                IPoint origin = new PointClass();
                origin.PutCoords(rasterProp.Extent.XMin, rasterProp.Extent.YMax);
                storageDef.Origin = origin;
               
                //在Raster Catalog中添加栅格
                //打开对应的Raster Catalog
                IRasterCatalog pRasterCatalog = ((IRasterWorkspaceEx)workspace).OpenRasterCatalog(rasterCatalogName);
                //将需要导入的Raster Catalog转化成为Feature Class
                IFeatureClass pFeatureClass = (IFeatureClass)pRasterCatalog;
                //名字所在列的索引号
                int nameIndex = pRasterCatalog.NameFieldIndex;
                //栅格数据所在列的索引号
                int rasterIndex = pRasterCatalog.RasterFieldIndex;
                IFeatureBuffer pBuffer = null;
                IFeatureCursor pFeatureCursor = pFeatureClass.Insert(false);
                //创建IRasterValue接口的对象——RasterBuffer对象的rasterIndex需要使用
                IRasterValue pRasterValue = new RasterValueClass();
                //设置IRasterValue的RasterDataset
                pRasterValue.RasterDataset = (IRasterDataset)pRasterDs;
                //存储参数设定
                pRasterValue.RasterStorageDef = storageDef;
                pBuffer = pFeatureClass.CreateFeatureBuffer();
                //设置RasterBuffer对象的rasterIndex和nameIndex
                pBuffer.set_Value(rasterIndex, pRasterValue);
                pBuffer.set_Value(nameIndex, strOutName);
                //通过cursor实现feature的Insert操作
                pFeatureCursor.InsertFeature(pBuffer);
                pFeatureCursor.Flush();
                //释放内粗资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pBuffer);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pRasterValue);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                //显示成功信息
                MessageBox.Show("导入成功！");
            }
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            try
            {
                //获取当前鼠标点击位置的相关信息
                esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap basicMap = null;
                ILayer layer = null;
                object unk = null;
                object data = null;
                //将以上定义的接口对象作为引用传入函数中，获取多个返回值
                this.axTOCControl1.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);
                //如若是鼠标右击且点击位置为图层，则弹出右击功能框
                if (e.button == 2)
                {
                    if (itemType == esriTOCControlItem.esriTOCControlItemLayer)
                    {
                        //设置TOC选择图层
                        this.TOCRightLayer = layer;
                        this.contextMenuStrip1.Show(axTOCControl1, e.x, e.y);
                    }
                }
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //右键选项，删除图层
        private void TSMI_DeleteLayer_Click(object sender, EventArgs e)
        {
            try
            {
                //删除当前图层
                axMapControl1.Map.DeleteLayer(TOCRightLayer);
                //刷新当前页面
                axMapControl1.ActiveView.Refresh();
                //更新波段信息统计的图层和波段下拉框选择内容
                iniCmbItems();
            }
            catch(System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //右键选项，PAN
        private void TSMI_ZoomToLayer_Click(object sender, EventArgs e)
        {
            try
            {
                //缩放到当前图层
                axMapControl1.ActiveView.Extent = TOCRightLayer.AreaOfInterest;
                //刷新页面显示
                axMapControl1.ActiveView.Refresh();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //当加载图层时候，初始化tab页面里的图层和波段下拉框的选项内容
        private void iniCmbItems()
        {
            try
            {
                //清除波段信息统计图层下拉框的选项内容
                cmb_StatistiicsLayer.Items.Clear();
                //清除NDVI指数计算图层下拉框的选项内容
                cmb_NDVILayer.Items.Clear();
                //清除直方图绘制图层下拉框的选项内容
                cmb_DrawHisLayer.Items.Clear();
                //清除单波段灰度增强的图层下拉框的选项内容
                cmb_StretchLayer.Items.Clear();
                //清除单波段伪彩色渲染的图层下拉框的选项内容
                cmb_RenderLayer.Items.Clear();
                //清除多波段假彩色合成的图层下拉框的选项内容
                cmb_RGBLayer.Items.Clear();
                //清除图像分类的下拉框选项内容
                cmb_ClassifyLayer.Items.Clear();
                //清除图像融合的下拉框选项内容
                cmb_PanSharpenSigleLayer.Items.Clear();
                cmb_PanSharpenMultiLayer.Items.Clear();
                //清除卷积运算的下拉框选项内容
                cmb_FliterLayer.Items.Clear();
                //清除图像变换的下拉框选项内容
                cmb_TransformLayer.Items.Clear();
                //清除山体阴影的下拉框选项内容
                cmb_HillShade.Items.Clear();
                //清除坡度函数的下拉框选项内容
                cmb_Slope.Items.Clear();
                //清除坡向函数的下拉框选项内容
                cmb_Aspect.Items.Clear();
                //清除领域分析的下拉框选项内容
                cmb_NeighborhoodLayer.Items.Clear();
                //清除裁剪分析的下拉框选项内容
                cmb_NeighborhoodLayer.Items.Clear();
                //清除通视分析的下拉框选项内容
                cmb_LineOfSightLayer.Items.Clear();
                //清除视域分析的下拉框选项内容
                cmb_VisibilityLayer.Items.Clear();
                //清除创建TIN的下拉框选项内容
                cmb_CreateTINLayer.Items.Clear();
                //清除通过DEM创建等高线的下拉框选项内容
                cmb_DEMContour.Items.Clear();
                //清除通过TIN创建等高线下拉框选项内容
                cmb_tinContour.Items.Clear();
                //清除通过TIN创建泰森多边形下拉框选项内容
                cmb_TinVoronoi.Items.Clear();

                ILayer layer = null;
                IMap map = axMapControl1.Map;
                int count = map.LayerCount;
                if (count > 0)
                {
                    //遍历地图的所有图层，获取图层的名字加入下拉框
                    for (int i = 0; i < count; i++)
                    {
                        layer = map.get_Layer(i);
                        //波段信息统计的图层下拉框
                        cmb_StatistiicsLayer.Items.Add(layer.Name);
                        //NDVI指数计算的图层下拉框
                        cmb_NDVILayer.Items.Add(layer.Name);
                        //直方图绘制的图层下拉框
                        cmb_DrawHisLayer.Items.Add(layer.Name);
                        //单薄段灰度增强的图层下拉框
                        cmb_StretchLayer.Items.Add(layer.Name);
                        //单波段伪彩色渲染的图层下拉框
                        cmb_RenderLayer.Items.Add(layer.Name);
                        //多波段假彩色合成的图层下拉框
                        cmb_RGBLayer.Items.Add(layer.Name);
                        //图像分类的图层下拉框
                        cmb_ClassifyLayer.Items.Add(layer.Name);
                        //图像剪裁的图层下拉框
                        cmb_ClipFeatureLayer.Items.Add(layer.Name);
                        //图像融合的图层下拉框
                        cmb_PanSharpenMultiLayer.Items.Add(layer.Name);
                        cmb_PanSharpenSigleLayer.Items.Add(layer.Name);
                        //图像卷积的图层下拉框
                        cmb_FliterLayer.Items.Add(layer.Name);
                        //图像变换的图层下拉框
                        cmb_TransformLayer.Items.Add(layer.Name);
                        //邻域分析的图层下拉框
                        cmb_NeighborhoodLayer.Items.Add(layer.Name);
                        //裁剪分析的图层下拉框
                        cmb_Extraction.Items.Add(layer.Name);

                        ///////////////////////////////////////////
                        ////////////以下都是要加DEM的图层//////////
                        ///////////////////////////////////////////
                        //山体阴影的图层下拉框
                        cmb_HillShade.Items.Add(layer.Name);
                        //坡度函数的图层下拉框
                        cmb_Slope.Items.Add(layer.Name);
                        //坡向函数的图层下拉框
                        cmb_Aspect.Items.Add(layer.Name);
                        //通视分析的下拉框选项内容
                        cmb_LineOfSightLayer.Items.Add(layer.Name);
                        //视域分析的下拉框选项内容
                        cmb_VisibilityLayer.Items.Add(layer.Name);
                        //创建TIN的下拉框选项内容
                        cmb_CreateTINLayer.Items.Add(layer.Name);
                        //通过DEM创建等高线的下拉框选项内容
                        cmb_DEMContour.Items.Add(layer.Name);

                        ///////////////////////////////////////////
                        ////////////以下都是要加TIN的图层//////////
                        ///////////////////////////////////////////
                        //通过TIN创建等高线下拉框
                        cmb_tinContour.Items.Add(layer.Name);
                        //通过TIN创建泰森多边形下拉框
                        cmb_TinVoronoi.Items.Add(layer.Name);


                    }
                    //设置下拉框默认选项为第一个图层
                    if (cmb_StatistiicsLayer.Items.Count > 0) cmb_StatistiicsLayer.SelectedIndex = 0;
                    if (cmb_NDVILayer.Items.Count > 0) cmb_NDVILayer.SelectedIndex = 0;
                    if (cmb_DrawHisLayer.Items.Count > 0) cmb_DrawHisLayer.SelectedIndex = 0;
                    if (cmb_StretchLayer.Items.Count > 0) cmb_StretchLayer.SelectedIndex = 0;
                    if (cmb_RenderLayer.Items.Count > 0) cmb_RenderLayer.SelectedIndex = 0;
                    if (cmb_RGBLayer.Items.Count > 0) cmb_RGBLayer.SelectedIndex = 0;
                    if (cmb_ClassifyLayer.Items.Count > 0) cmb_ClassifyLayer.SelectedIndex = 0;
                    if (cmb_ClipFeatureLayer.Items.Count > 0) cmb_ClipFeatureLayer.SelectedIndex = 0;
                    if (cmb_FliterLayer.Items.Count > 0) cmb_FliterLayer.SelectedIndex = 0;
                    if (cmb_TransformLayer.Items.Count > 0) cmb_TransformLayer.SelectedIndex = 0;
                    if (cmb_PanSharpenMultiLayer.Items.Count > 0) cmb_PanSharpenMultiLayer.SelectedIndex = 0;
                    if (cmb_PanSharpenSigleLayer.Items.Count > 0) cmb_PanSharpenSigleLayer.SelectedIndex = 0;
                    if (cmb_HillShade.Items.Count > 0) cmb_HillShade.SelectedIndex = 0;
                    if (cmb_Slope.Items.Count > 0) cmb_Slope.SelectedIndex = 0;
                    if (cmb_Aspect.Items.Count > 0) cmb_Aspect.SelectedIndex = 0;
                    if (cmb_NeighborhoodLayer.Items.Count > 0) cmb_NeighborhoodLayer.SelectedIndex = 0;
                    if (cmb_Extraction.Items.Count > 0) cmb_Extraction.SelectedIndex = 0;
                    if (cmb_LineOfSightLayer.Items.Count > 0) cmb_LineOfSightLayer.SelectedIndex = 0;
                    if (cmb_VisibilityLayer.Items.Count > 0) cmb_VisibilityLayer.SelectedIndex = 0;
                    if (cmb_CreateTINLayer.Items.Count > 0) cmb_CreateTINLayer.SelectedIndex = 0;
                    if (cmb_DEMContour.Items.Count > 0) cmb_DEMContour.SelectedIndex = 0;
                    if (cmb_tinContour.Items.Count > 0) cmb_tinContour.SelectedIndex = 0;
                    if (cmb_TinVoronoi.Items.Count > 0) cmb_TinVoronoi.SelectedIndex = 0;
                    //清楚波段信息统计波段下拉框的选项内容
                    cmb_StatisticsBand.Items.Clear();
                    //清除直方图绘制的波段下拉框的选项内容
                    cmb_DrawHisBand.Items.Clear();
                    //清除单波段灰度增强的波段下拉框的选项内容
                    cmb_StretchBand.Items.Clear();
                    //清除单波段伪彩色渲染的波段下拉框的选项内容
                    cmb_RenderBand.Items.Clear();
                    //清除多波段假彩色合成的波段下拉框选项的内容
                    cmb_RBand.Items.Clear();
                    cmb_GBand.Items.Clear();
                    cmb_BBand.Items.Clear();
                    //获取第一个图层的栅格波段
                    ILayer player = map.get_Layer(0);
                    if (player is IRasterLayer)
                    {
                        IRasterLayer rstLayer = player as IRasterLayer;
                        IRaster2 raster2 = rstLayer.Raster as IRaster2;
                        IRasterDataset rstDataset = raster2.RasterDataset;
                        IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
                        //波段总数
                        int bandCount = rstLayer.BandCount;
                        //添加所有波段的选项
                        cmb_StatisticsBand.Items.Add("全部波段");
                        //遍历图层的所有波段，获取波段名字加入下拉框
                        for (int i = 0; i < bandCount; i++)
                        {
                            int bandIdx = i + 1;//设置波段序号
                            //添加波段信息统计的波段下拉框的选项内容
                            cmb_StatisticsBand.Items.Add("波段" + bandIdx);
                            //添加直方图绘制的波段下拉框选项
                            cmb_DrawHisBand.Items.Add("波段" + bandIdx);
                            //添加单波段灰度增强的波段下拉框的选项内容
                            cmb_StretchBand.Items.Add("波段" + bandIdx);
                            //添加单波段伪彩色渲染的波段下拉框选项内容
                            cmb_RenderBand.Items.Add("波段" + bandIdx);
                            //添加多波段假彩色合成的波段下拉框选项内容
                            cmb_RBand.Items.Add("波段" + bandIdx);
                            cmb_GBand.Items.Add("波段" + bandIdx);
                            cmb_BBand.Items.Add("波段" + bandIdx);
                        }
                        //设置下拉框默认选项
                        if (cmb_StatisticsBand.Items.Count > 0) cmb_StatisticsBand.SelectedIndex = 0;
                        if (cmb_DrawHisBand.Items.Count > 0) cmb_DrawHisBand.SelectedIndex = 0;
                        if (cmb_StretchBand.Items.Count > 0) cmb_StretchBand.SelectedIndex = 0;
                        if (cmb_RenderBand.Items.Count > 0) cmb_RenderBand.SelectedIndex = 0;
                        if (cmb_RBand.Items.Count > 0) cmb_RBand.SelectedIndex = 0;
                        if (cmb_BBand.Items.Count > 0) cmb_BBand.SelectedIndex = 0;
                        if (cmb_GBand.Items.Count > 0) cmb_GBand.SelectedIndex = 0;
                    }
                }
            }
            catch(System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public ILayer GetLayerByName(string sLayerName)
        {
            //判断图层名称或者地图对象是否为空，若为空，则返回空
            if (sLayerName == "" || axMapControl1 == null)
            {
                return null;
            }
            //对地图对象中的所有图层进行遍历，若某一图层的名称与指定图层名称相同，则返回该图层
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                if (axMapControl1.get_Layer(i).Name == sLayerName)
                {
                    return axMapControl1.get_Layer(i);
                }
            }
            //若地图中所有图层名称与指定名称都不匹配，则返回空
            return null;
        }

        //当遥感图像处理分析的图层下拉框的选择项发生变化，则相应的波段下拉框的选项也会发生变化
        private void selectedIndexChangeFunction(ComboBox cmbLayer, ComboBox cmbBand, string type)
        {
            try
            {
                ILayer layer = GetLayerByName(cmbLayer.SelectedItem.ToString());
                if (layer is IRasterLayer)
                {
                    cmbBand.Items.Clear();
                    //cmbBand.SelectedIndex = 0;
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    IRasterDataset rstDataset = raster2.RasterDataset;
                    IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
                    //波段总数
                    int bandCount = rstLayer.BandCount;
                    //添加所有波段的选项
                    if (type == "statistics") cmbBand.Items.Add("全部波段");
                    //遍历图层的所有波段，获取波段名字加入下拉框
                    for (int i = 0; i < bandCount; i++)
                    {
                        int bandIdx = i + 1;//设置波段序号
                        cmbBand.Items.Add("波段" + bandIdx);
                    }
                    cmbBand.SelectedIndex = 0;
                }
            }
            catch(System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //当波段信息统计的图层下拉框的选择项发生变化，则相应的波段下拉框的选项也会发生变化
        private void cmb_StatistiicsLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                selectedIndexChangeFunction(cmb_StatistiicsLayer, cmb_StatisticsBand, "statistics");
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        //当直方图绘制的图层下拉框的选择项发生变化，则相应的波段下拉框的选项也会发生变化
        private void cmb_DrawHisLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedIndexChangeFunction(cmb_DrawHisLayer, cmb_DrawHisBand, null);
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //当单波段灰度增强的图层下拉框的选择项发生变化，则相应的波段下拉框的选项也会发生变化
        private void cmb_StretchLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedIndexChangeFunction(cmb_StretchLayer, cmb_StretchBand, null);
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //当单波段伪彩色渲染的图层下拉框的选择项发生变化，则相应的波段下拉框的选项也会发生变化
        private void cmb_RenderLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedIndexChangeFunction(cmb_RenderLayer, cmb_RenderBand, null);
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //当多波段假彩色合成的图层下拉框的选择项发生变化，则相应的波段下拉框的选项也会发生变化
        private void cmb_RGBLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedIndexChangeFunction(cmb_RGBLayer, cmb_RBand, null);
                selectedIndexChangeFunction(cmb_RGBLayer, cmb_GBand, null);
                selectedIndexChangeFunction(cmb_RGBLayer, cmb_BBand, null);
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //点击统计按钮后，进行波段信息统计
        private void btn_Statistics_Click(object sender, EventArgs e)
        {
            try
            {
                //获取选择的图层和波段，转换接口
                ILayer layer = GetLayerByName(cmb_StatistiicsLayer.SelectedItem.ToString());
                IRasterLayer rstLayer = null;
                if (layer is IRasterLayer) rstLayer = layer as IRasterLayer;
                else
                {
                    MessageBox.Show("所选择的图层并非栅格图层，无法进行操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //IRasterLayer rstLayer= GetLayerByName(cmb_NDVILayer.SelectedItem.ToString()) as IRasterLayer;
                IRaster2 raster2 = rstLayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;

                int index = cmb_StatisticsBand.SelectedIndex;
                if (cmb_StatisticsBand.SelectedItem.ToString()=="全部波段")
                {
                    string StatRes = "";
                    for (int i = 0; i < rstBandColl.Count; i++)
                    {
                        IRasterBand rstBand = rstBandColl.Item(i);
                        //判断该波段是否已经存在统计数据
                        bool hasStat = false;
                        rstBand.HasStatistics(out hasStat);
                        ////如果不存在统计数据，就进行波段信息统计
                        if (null == rstBand.Statistics || !hasStat)
                        {
                            IRasterBandEdit rasterBandEdit = rstBand as IRasterBandEdit;
                            rasterBandEdit.ComputeStatsHistogram(0);
                        }
                        //获取统计结果
                        rstBand.ComputeStatsAndHist();
                        IRasterStatistics rstStat = rstBand.Statistics;
                        StatRes += "第" + (i + 1) + "波段：" + "  平均值为:" + rstStat.Mean + "  最大值为：" + rstStat.Maximum + "  最小值为:" + rstStat.Minimum + "  标准差为:" + rstStat.StandardDeviation + "\r\n";                                        
                    }
                    //提示框输出统计结果
                    MessageBox.Show(StatRes, "统计结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int bandnum;
                    if (cmb_StatisticsBand.Items.Count > rstBandColl.Count) bandnum = index - 1;
                    else bandnum = index;
                    //获取波段
                    IRasterBand rstBand = rstBandColl.Item(bandnum);
                    //判断该波段是否已经存在统计数据
                    bool hasStat = false;
                    rstBand.HasStatistics(out hasStat);
                    ////如果不存在统计数据，就进行波段信息统计
                    if (null == rstBand.Statistics || !hasStat)
                    {
                        IRasterBandEdit rasterBandEidt = rstBand as IRasterBandEdit;
                        rasterBandEidt.ComputeStatsHistogram(0);
                    }
                    //获取统计结果
                    rstBand.ComputeStatsAndHist();
                    IRasterStatistics rstStat = rstBand.Statistics;
                    String bandStatRes = null;
                    bandStatRes += "第" + (bandnum + 1) + "波段：" + "  平均值为:" + rstStat.Mean + "  最大值为：" + rstStat.Maximum + "  最小值为:" + rstStat.Minimum + "  标准差为:" + rstStat.StandardDeviation + "\r\n";
                    MessageBox.Show(bandStatRes, "统计结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                //IRasterBand rstBand = rstBandColl.Item(1);
                //获取当前选中的栅格图层的栅格波段

                //如果选择全部波段，则遍历该图层全部波段，并统计信息

            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //计算NDVI指数
        private void btn_CalculateNDVI__Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选择的图层和波段，转换接口
                ILayer layer = GetLayerByName(cmb_NDVILayer.SelectedItem.ToString());
                IRasterLayer rstLayer=null;
                if (layer is IRasterLayer) rstLayer = layer as IRasterLayer;
                else
                {
                    MessageBox.Show("所选择的图层并非栅格图层，无法进行操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //IRasterLayer rstLayer= GetLayerByName(cmb_NDVILayer.SelectedItem.ToString()) as IRasterLayer;
                IRaster2 raster2 = rstLayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;

                //获取红波段和近红外波段，转换IGeoDataset接口
                if (rstBandColl.Count < 4)
                {
                    MessageBox.Show("该图层不可计算NDVI","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    //获取第四波段
                    IRasterBand rstBand4 = rstBandColl.Item(3);
                    //获取第三波段
                    IRasterBand rstBand3 = rstBandColl.Item(2);
                    //分别将第四、第三波段转换成IGeoDataset接口
                    IGeoDataset geoDataset4 = rstBand4 as IGeoDataset;
                    IGeoDataset geoDataset3 = rstBand3 as IGeoDataset;

                    //创建一个用于栅格运算的类RasterMathOpsClass
                    IMathOp mathOp = new RasterMathOpsClass();
                    //利用IGeoDataset和Math计算NDVI获得结果IGeoDataset
                    //band4-band3
                    IGeoDataset upDataset = mathOp.Minus(geoDataset4, geoDataset3);
                    //band4+band3
                    IGeoDataset downDataset = mathOp.Plus(geoDataset4, geoDataset3);
                    //分子分母转为float型
                    IGeoDataset fltUpDataset = mathOp.Float(upDataset);
                    IGeoDataset fltDownDataset = mathOp.Float(downDataset);
                    //相除得到NDVI
                    IGeoDataset resultDataset = mathOp.Divide(fltUpDataset, fltDownDataset);

                    //将结果保存到一个RasterLayer中，命名为NDVI
                    IRaster resRaster = resultDataset as IRaster;
                    IRasterLayer resLayer = new RasterLayerClass();
                    resLayer.CreateFromRaster(resRaster);
                    resLayer.SpatialReference = geoDataset4.SpatialReference;
                    resLayer.Name = "NDVI";
                    //将此单波段图像用灰度显示，并按照最大最小值拉伸
                    IRasterStretchColorRampRenderer grayStretch = null;
                    if (resLayer.Renderer is IRasterStretchColorRampRenderer) grayStretch = resLayer.Renderer as IRasterStretchColorRampRenderer;
                    else grayStretch = new RasterStretchColorRampRendererClass();
                    IRasterStretch2 rstStr2 = grayStretch as IRasterStretch2;
                    rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;//设置拉伸模式为最大最小值拉伸
                    resLayer.Renderer = grayStretch as IRasterRenderer;
                    resLayer.Renderer.Update();

                    //添加NDVI图层显示，并刷新视图
                    axMapControl1.AddLayer(resLayer);
                    axMapControl1.ActiveView.Extent = resLayer.AreaOfInterest;
                    axMapControl1.ActiveView.Refresh();
                    this.axTOCControl1.Update();
                }
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再修改鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
                
            }
        }

        private void btn_SingleBandHis_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单机时修改鼠标光标形状
            try
            {
                //获取选择的图层和波段，转换接口
                ILayer layer = GetLayerByName(cmb_DrawHisLayer.SelectedItem.ToString());
                IRasterLayer rstLayer = null;
                if (layer is IRasterLayer) rstLayer = layer as IRasterLayer;
                else
                {
                    MessageBox.Show("所选择的图层并非栅格图层，无法进行操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //IRasterLayer rstLayer= GetLayerByName(cmb_NDVILayer.SelectedItem.ToString()) as IRasterLayer;
                IRaster2 raster2 = rstLayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
                IRasterBand rasterBand = rstBandColl.Item(cmb_DrawHisBand.SelectedIndex);

                //计算该波段的Histogram
                bool hasStat = false;
                rasterBand.HasStatistics(out hasStat);
                if (null == rasterBand.Statistics || !hasStat || rasterBand.Histogram == null)
                {
                    //转换IRasterBandEdit2接口，调用ComputeStatsHistogram方法进行波段信息统计和直方图绘制
                    IRasterBandEdit rasterBandEdit = rasterBand as IRasterBandEdit;
                    rasterBandEdit.ComputeStatsHistogram(0);
                }

                //获取每个像元值的统计个数
                double[] histo = rasterBand.Histogram.Counts as double[];

                //获取统计结果
                IRasterStatistics rasterStatistics = rasterBand.Statistics;

                //创建直方图窗体，并将像元统计、最小值、最大值作为参数传入
                HistogramForm histogramForm = new HistogramForm(histo, rasterStatistics.Minimum, rasterStatistics.Maximum);
                histogramForm.ShowDialog();
            }
            catch (Exception ex)//处理异常，输出异常错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后将鼠标光标设置为默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //点击绘制多波段的对比直方图，弹出波段复选窗口
        private void btn_MultiBandHis_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取当前选中的图层的index
                int indexLayer = cmb_DrawHisLayer.SelectedIndex;
                //获取MapControl中的map相关图层
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    IRasterLayer rasterLayer = layer as IRasterLayer;
                    SelectBandsForm SelectBands = new SelectBandsForm(rasterLayer);
                    SelectBands.ShowDialog();
                }
            }
            catch(Exception ex)//异常处理，输出异常错误信息
            {
                MessageBox.Show(ex.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成为默认格式
            {
                this.Cursor = Cursors.Default;
            }
        }
        //单波段灰度增强
        private void btn_Stretch_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选择的图层和波段对象
                if (axMapControl1.LayerCount == 0) {MessageBox.Show("当前控件中并无图层，无法继续操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return;}
                if (cmb_StretchBand.Items.ToString() == "") { MessageBox.Show("未选择波段，无法继续操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (cmb_StretchMethod.SelectedItem.ToString()  == "") { MessageBox.Show("未指定方法，无法继续操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                ILayer layer = GetLayerByName(cmb_StretchLayer.SelectedItem.ToString());
                if (layer is IRasterLayer)
                {
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    IRasterDataset rstDataset = raster2.RasterDataset;
                    IRasterRenderer rstRenderer=rstLayer.Renderer;
                    //IRaster resRaster = rstBand as IRaster;
                    //IRasterLayer resLayer = new RasterLayerClass();
                    //resLayer.CreateFromRaster(resRaster);
                    //resLayer.SpatialReference = ((IGeoDataset)rstBand).SpatialReference;
                    //resLayer.Name = cmb_StretchLayer.SelectedItem.ToString()+cmb_StretchBand.SelectedItem.ToString()+cmb_StretchMethod.SelectedItem.ToString();

                    ///////////////////////////////////////////
                    //波段信息在这里到底怎么用咧？？？？？？///
                    ///////////////////////////////////////////
                    
                    IRasterStretchColorRampRenderer grayRenderer = new RasterStretchColorRampRendererClass();
                    grayRenderer.BandIndex = cmb_StretchBand.SelectedIndex;
                    IRasterStretch2 rstStr2 = grayRenderer as IRasterStretch2;
                    rstRenderer.Raster = (IRaster)raster2;
                                  
                    //判断拉伸方式
                    switch (cmb_StretchMethod.SelectedIndex)
                    {
                        case 0://默认拉伸
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_DefaultFromSource;
                            break;
                        case 1://标准差拉伸
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations;
                            break;
                        case 2://最大最小值拉伸
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;
                            break;
                        case 3://百分比最大最小值拉伸
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_PercentMinimumMaximum;
                            break;
                        case 4://直方图均衡
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize;
                            break;
                        case 5://直方图匹配
                            rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramSpecification;
                            break;
                        default:
                            break;
                    }
                    //设置不应用反色
                    rstStr2.Invert = false;
                    //应用拉伸渲染
                    rstRenderer = grayRenderer as IRasterRenderer;
                    rstRenderer.Update();
                    rstLayer.Renderer = rstRenderer;
                }
                //刷新控件
                this.axMapControl1.ActiveView.Refresh();
                this.axTOCControl1.Update();

            }
            catch (Exception ex)//处理异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //根据两端颜色和大小产生AlgorithmicColorRamp
        private IAlgorithmicColorRamp GetAlgorithmicColorRamp(Color FromColor, Color ToColor, int size)
        {
            try
            {
                //实例化接口
                IAlgorithmicColorRamp algorithmicColorRamp = new AlgorithmicColorRampClass();
                IColor toColor=new RgbColorClass();
                IColor fromColor = new RgbColorClass();
                toColor.RGB = ToColor.B * 65536 + ToColor.G * 256 + ToColor.R;
                fromColor.RGB = FromColor.B * 65536 + FromColor.G * 256 + FromColor.R;
                //toColor.RGB = ToColor;
                //设置起始颜色，终止颜色，算法类型，尺寸大小
                algorithmicColorRamp.ToColor = toColor ;
                algorithmicColorRamp.FromColor = fromColor;
                algorithmicColorRamp.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
                algorithmicColorRamp.Size = size;

                //调用IAlgorithmicColorRamp接口的CreateRamp函数创建色带
                bool bResult;
                algorithmicColorRamp.CreateRamp(out bResult);
                if (bResult)
                {
                    return algorithmicColorRamp;
                }
                return null;
            }
            catch (System.Exception e)//捕捉异常
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //通过传入的起始和终止颜色创建包含色带的Bitmap图像并返回
        private Bitmap CreateColorRamp(Color FromColor, Color ToColor)
        {
            try
            {
                //获取色带
                IAlgorithmicColorRamp algorithmicColorRamp = GetAlgorithmicColorRamp(FromColor, ToColor, pb_ColorBar.Size.Width);
                //创建新的bitmap
                Bitmap bmpColorRamp = new Bitmap(pb_ColorBar.Size.Width, pb_ColorBar.Size.Height);
                //获取graphics对象
                Graphics graphic = Graphics.FromImage(bmpColorRamp);
                //用GDI+的方法逐一填充颜色到显示色带
                IColor color = null;
                for (int i = 0; i < pb_ColorBar.Size.Width; i++)
                {
                    //获取当前颜色
                    color = algorithmicColorRamp.get_Color(i);
                    if (color == null) continue;
                    IRgbColor rgbColor = new RgbColorClass();
                    rgbColor.RGB = color.RGB;
                    Color customColor = Color.FromArgb(rgbColor.Red, rgbColor.Green, rgbColor.Blue);
                    SolidBrush solidBrush = new SolidBrush(customColor);
                    //绘制
                    graphic.FillRectangle(solidBrush, i, 0, 1, pb_ColorBar.Size.Height);

                }
                //删除graphics对象
                graphic.Dispose();
                return bmpColorRamp;
            }
            catch (System.Exception ex)//捕获异常，并输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
     
        //刷新颜色控件
        private void RefreshColors(Color FromColor, Color ToColor)//重新绘制起始颜色、终止颜色，并根据这两种颜色绘制色带
        {
            try
            {
                //初始化FromColor
                //创建Bitmap
                Bitmap bmpFromColor = new Bitmap(pb_FromColor.Size.Width, pb_FromColor.Size.Height);
                //创建graphics对象
                Graphics graphicFC = Graphics.FromImage(bmpFromColor);
                SolidBrush solidBrushFC = new SolidBrush(FromColor);
                //绘制起始颜色，左下到右上
                graphicFC.FillRectangle(solidBrushFC, 0, 0, pb_FromColor.Size.Width, pb_FromColor.Size.Height);
                //更新图像
                this.pb_FromColor.Image = bmpFromColor;
                //删除graphics对象
                graphicFC.Dispose();

                //初始化ToColor
                //创建bitmap
                Bitmap bmpToColor = new Bitmap(pb_ToColor.Size.Width, pb_ToColor.Size.Height);
                //创建graphics对象
                Graphics graphicTC = Graphics.FromImage(bmpToColor);
                SolidBrush solidBrushTC = new SolidBrush(ToColor);
                //绘制终止颜色，左下到右上
                graphicTC.FillRectangle(solidBrushTC, 0, 0, pb_ToColor.Size.Width, pb_ToColor.Size.Height);
                //更新图像
                this.pb_ToColor.Image = bmpToColor;
                //删除graphics对象
                graphicTC.Dispose();

                //初始化色带
                Bitmap stretchRamp = CreateColorRamp(FromColor, ToColor);
                //更新图像
                this.pb_ColorBar.Image = stretchRamp;
            }
            catch (System.Exception ex)//捕获异常，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //添加起始颜色的鼠标点击事件
        private void pb_FromColor_Click(object sender, EventArgs e)//显示ColorDialog选项框，选择并获取色带起始颜色
        {
            try
            {
                if (this.cd_FromColor.ShowDialog() == DialogResult.OK)
                {
                    m_FromColor = this.cd_FromColor.Color;//设置起始颜色
                    RefreshColors(m_FromColor, m_ToColor);//刷新颜色控件
                }
            }
            catch (System.Exception ex)//捕获异常，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //添加终止颜色的鼠标点击事件
        private void pb_ToColor_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cd_ToColor.ShowDialog() == DialogResult.OK)
                {
                    m_ToColor = this.cd_ToColor.Color;//设置终止颜色
                    RefreshColors(m_FromColor, m_ToColor);//刷新颜色控件
                }
            }
            catch (System.Exception ex)//捕获异常，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //单波段伪彩色增强
        private void btn_Render_Click(object sender, EventArgs e)
        {
            try
            {
                //获取当前选择的图层和波段
                ILayer layer = GetLayerByName(cmb_RenderLayer.SelectedItem.ToString());
                IRasterLayer rstLayer = layer as IRasterLayer;
                IRaster raster = rstLayer.Raster;

                //设置IRasterRenderer
                IRasterStretchColorRampRenderer stretchRenderer = new RasterStretchColorRampRendererClass();
                IRasterRenderer rasterRenderer = (IRasterRenderer)stretchRenderer;
                rasterRenderer.Raster = raster;
                //获取并设置设置渲染应用波段
                stretchRenderer.BandIndex = cmb_RenderBand.SelectedIndex;

                //设置拉伸类型
                IRasterStretch2 rstStretch2 = rasterRenderer as IRasterStretch2;
                rstStretch2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize;//设置拉伸方式为直方图均衡化
                //获取色带，256层
                IAlgorithmicColorRamp algorithmicColorRamp = GetAlgorithmicColorRamp(m_FromColor, m_ToColor, 256);
                IColorRamp colorRamp = algorithmicColorRamp as IColorRamp;
                //设置拉伸渲染的色带
                stretchRenderer.ColorRamp = colorRamp;

                //设置TOC中的图例
                ILegendInfo legendInfo = stretchRenderer as ILegendInfo;
                ILegendGroup legendGroup = legendInfo.get_LegendGroup(0);
                for (int i = 0; i < legendGroup.ClassCount; i++)
                {
                    ILegendClass legendClass = legendGroup.get_Class(i);
                    legendClass.Symbol = new ColorRampSymbolClass();
                    IColorRampSymbol colorRampSymbol = legendClass.Symbol as IColorRampSymbol;
                    colorRampSymbol.ColorRamp = colorRamp;
                    colorRampSymbol.ColorRampInLegendGroup = colorRamp;
                    colorRampSymbol.LegendClassIndex = i;
                    legendClass.Symbol = colorRampSymbol as ISymbol;
                }
                //应用渲染设置
                rasterRenderer.Update();
                rstLayer.Renderer = rasterRenderer;
                rstLayer.Renderer.Update();
                //刷新控件
                this.axMapControl1.ActiveView.Refresh();
                this.axTOCControl1.Update();

            }
            catch (System.Exception ex)//捕获异常，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //多波段假彩色合成
        private void btn_RGB_Click(object sender, EventArgs e)
        {
            try
            {
                ILayer layer = GetLayerByName(cmb_RGBLayer.SelectedItem.ToString());
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取波段渲染信息
                    IRasterRenderer rstRenderer = rstLayer.Renderer;
                    //创建RGB合成渲染类
                    IRasterRGBRenderer rgbRenderer = null;
                    if (rstRenderer is IRasterRGBRenderer)
                    {
                        rgbRenderer = rstRenderer as IRasterRGBRenderer;
                    }
                    else
                    {
                        rgbRenderer=new RasterRGBRendererClass();
                    }
                        //获取并设置RGB对应波段
                        rgbRenderer.RedBandIndex = cmb_RBand.SelectedIndex;
                        rgbRenderer.GreenBandIndex = cmb_GBand.SelectedIndex;
                        rgbRenderer.BlueBandIndex = cmb_BBand.SelectedIndex;
                        //更新渲染类
                        rstRenderer = rgbRenderer as IRasterRenderer;
                        rstRenderer.Update();
                        //将RGB渲染参数赋值给图层渲染器
                        rstLayer.Renderer = rstRenderer;
                    //更新控件
                    this.axMapControl1.ActiveView.Refresh();
                    this.axTOCControl1.Update();
                }
            }
            catch(System.Exception ex)//处理异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //栅格图像唯一值渲染
        public IRasterRenderer UniqueValueRenderer(ESRI.ArcGIS.Geodatabase.IRasterDataset rasterDataset)
        {
            try
            {
                //获得栅格图像属性表及其大小
                IRaster2 raster = (IRaster2)rasterDataset.CreateDefaultRaster();
                ITable rasterTable = raster.AttributeTable;
                if (rasterTable == null) { return null; }
                int tableRows = rasterTable.RowCount(null);
                //为每一个属性的唯一值创建和设置一个唯一颜色
                IRandomColorRamp colorRamp = new RandomColorRampClass();
                //设置随机色带的属性参数
                colorRamp.Size = tableRows;
                colorRamp.Seed = 100;
                //调用createRamp方法来创建色带
                bool createColorRamp;
                colorRamp.CreateRamp(out createColorRamp);
                if (createColorRamp == false) { return null; }
                //创建一个唯一值渲染器
                IRasterUniqueValueRenderer uvRenderer = new RasterUniqueValueRendererClass();
                IRasterRenderer rasterRenderer = (IRasterRenderer)uvRenderer;
                //设置渲染器的栅格数据对象（属性）
                rasterRenderer.Raster = rasterDataset.CreateDefaultRaster();
                rasterRenderer.Update();
                //设置渲染器的属性值
                uvRenderer.HeadingCount = 1;
                uvRenderer.set_Heading(0, "ALL Data Value");
                uvRenderer.set_ClassCount(0, tableRows);
                uvRenderer.Field = "Value";//或者表中的其他字段
                //遍历属性表格，分别设置唯一值颜色
                IRow row;
                //创建简单填充符号接口的对象，用户每一个类别的像素颜色填充
                ISimpleFillSymbol fillSymbol;
                for (int i = 0; i < tableRows; i++)
                {
                    row = rasterTable.GetRow(i);
                    //为某一个特定的类别添加值
                    uvRenderer.AddValue(0, i, Convert.ToByte(row.get_Value(1)));
                    //为某一个特定的类别设置标签
                    uvRenderer.set_Label(0, i, Convert.ToString(row.get_Value(1)));
                    //实例化创建一个简单填充符号类的对象
                    fillSymbol = new SimpleFillSymbolClass();
                    fillSymbol.Color = colorRamp.get_Color(i);
                    //为某一特定的类别设置渲染符号
                    uvRenderer.set_Symbol(0, i, (ISymbol)fillSymbol);
                }
                return rasterRenderer;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        //点击分类按钮，实现栅格图像的分类操作
        private void btn_Classify_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取填入的分类的数量
                string numStr = txb_ClassifyNumber.Text;
                int num = int.Parse(numStr);
                //获取选中的图层
                int indexLayer = cmb_ClassifyLayer.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    if (rstLayer.BandCount > 1)
                    {
                        //获取图层raster并转换成IRaster2接口
                        IRaster2 raster2 = rstLayer.Raster as IRaster2;
                        //获取该raster的RasterDataset
                        IRasterDataset rstDataset = raster2.RasterDataset;
                        //转换IGeoDataset接口
                        IGeoDataset geoDataset = rstDataset as IGeoDataset;
                        //创建多元操作组件类对象
                        IMultivariateOp mulop = new RasterMultivariateOpClass();
                        //设定结果文件保存路径
                        string signatureFilePath = "D:\\RDB";
                        string fullPath = signatureFilePath + "\\" + "classify_signature";
                        string treePath = signatureFilePath + "\\" + "signature_treediagram";
                        //获取用户输入的分类数量
                        int NumClass = num;
                        //执行isocluster聚类方法提取多维属性空间中的像素单元的特征值
                        mulop.IsoCluster(geoDataset, fullPath, NumClass, 20, 20, 10);
                        //分类结果数据集
                        IGeoDataset outdataset;
                        //定义Missing的类型
                        object missing = Type.Missing;
                        //利用最大似然法进行遥感图像非监督分类
                        outdataset = mulop.MLClassify(geoDataset, fullPath, false, esriGeoAnalysisAPrioriEnum.esriGeoAnalysisAPrioriEqual, missing, missing);
                        //定义输出结果栅格
                        IRaster2 outraster1;
                        outraster1 = (IRaster2)outdataset;
                        //保存结果栅格数据，加载栅格数据图层显示，进行唯一值渲染
                        IRasterRenderer rasterrender = UniqueValueRenderer(outraster1.RasterDataset);
                        //在栅格图层中加载显示
                        IRasterLayer rasterLayer = new RasterLayerClass();
                        rasterLayer.CreateFromDataset(outraster1.RasterDataset);
                        rasterLayer.Name = "MLClassify";
                        //设置栅格渲染器对象
                        if (rasterrender != null)
                        {
                            rasterLayer.Renderer = rasterrender;
                        }
                        //将渲染好的栅格图像加载到map中
                        if (rasterLayer != null)
                        {
                            //更新控件
                            axMapControl1.Map.AddLayer(rasterLayer);
                        }
                        //classprobility
                        IGeoDataset outdataset2;
                        outdataset2 = mulop.ClassProbability(geoDataset, fullPath, esriGeoAnalysisAPrioriEnum.esriGeoAnalysisAPrioriSample, missing, missing);
                        IRaster2 outraster2 = (IRaster2)outdataset2;
                        IRasterLayer rasterLayer2 = new RasterLayerClass();
                        rasterLayer2.CreateFromDataset(outraster2.RasterDataset);
                        rasterLayer2.Name = "ClassProbility";
                        ILayer iLayer2 = rasterLayer2 as ILayer;
                        axMapControl1.Map.AddLayer(iLayer2);

                        axMapControl1.ActiveView.Refresh();
                        axTOCControl1.Update();
                        iniCmbItems();
                        //Dendrogram
                        mulop.Dendrogram(fullPath, treePath, true, Type.Missing);

                    }
                }


            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后在将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //点击混淆矩阵按钮，查看混淆矩阵结果数值
        private void btn_After_Classify_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选中的图层
                int indexLayer = cmb_ClassifyLayer.SelectedIndex;
                ILayer layer = this.axMapControl1.get_Layer(indexLayer);
                if (layer is IRasterLayer)
                {
                    //转换成IRasterLayer接口
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    //获取图层raster并转换成IRaster2接口
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    //获取该raster的RasterDataset
                    IRasterDataset rstDataset = raster2.RasterDataset;
                    //转换IGeoDataset接口
                    IGeoDataset geoDataset = rstDataset as IGeoDataset;
                    //Create the RasterGeneralizeOP object
                    IGeneralizeOp generalizeOp = new ESRI.ArcGIS.SpatialAnalyst.RasterGeneralizeOpClass();
                    //Declare the output raster object
                    IGeoDataset outdataset1 = generalizeOp.Aggregate(geoDataset, 4, ESRI.ArcGIS.GeoAnalyst.esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMean, true, true);
                    IRaster2 outraster1 = (IRaster2)outdataset1;
                    IRasterLayer rasterLayer1 = new RasterLayerClass();
                    rasterLayer1.CreateFromDataset(outraster1.RasterDataset);
                    rasterLayer1.Name = "Aggregate";
                    ILayer iLayer1 = rasterLayer1 as ILayer;
                    axMapControl1.Map.AddLayer(iLayer1);

                    IGeoDataset outdataset2 = generalizeOp.BoundaryClean(geoDataset, ESRI.ArcGIS.SpatialAnalyst.esriGeoAnalysisSortEnum.esriGeoAnalysisSortAscending, true);
                    IRaster2 outraster2 = (IRaster2)outdataset2;
                    IRasterLayer rasterLayer2 = new RasterLayerClass();
                    rasterLayer2.CreateFromDataset(outraster2.RasterDataset);
                    rasterLayer2.Name = "BoundaryClean";
                    ILayer iLayer2 = rasterLayer2 as ILayer;
                    axMapControl1.Map.AddLayer(iLayer2);

                    //Majority filter
                    IGeoDataset outdataset3 = generalizeOp.MajorityFilter(geoDataset, true, false);
                    IRaster2 outraster3 = (IRaster2)outdataset3;
                    IRasterLayer rasterLayer3 = new RasterLayerClass();
                    rasterLayer3.CreateFromDataset(outraster3.RasterDataset);
                    rasterLayer3.Name = "MajorityFilter";
                    ILayer iLayer3 = rasterLayer3 as ILayer;
                    axMapControl1.Map.AddLayer(iLayer3);
                    //refresh the active view
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    iniCmbItems();

                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后在将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //弹出文件选择对话框，点击选择用于剪裁的矢量文件
        private void txb_ClipFeature_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //打开文件选择对话框，设置对话框属性
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Shp file (*.shp)|*.shp";
                openFileDialog.Title="选择矢量文件";
                openFileDialog.Multiselect=false;
                string fileName="";
                //如果对话框已成功选择文件，将文件路径信息填写到输入框里
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    txb_ClipFeature.Text = fileName;
                }
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //点击进行矢量文件对栅格文件的裁剪
        private void btn_Clip_Click(object sender, EventArgs e)
        {
            try
            {
                //获取选择的栅格图层、栅格对象
                ILayer layer = GetLayerByName(cmb_ClipFeatureLayer.SelectedItem.ToString());
                IRasterLayer rstLayer = layer as IRasterLayer;
                IRaster2 raster2 = rstLayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;

                IRaster raster = rstDataset.CreateDefaultRaster();
                
                //获取矢量文件的路径和文件名字
                string fileN = txb_ClipFeature.Text;
                FileInfo fileInfo = new FileInfo(fileN);
                string filePath = fileInfo.DirectoryName;
                string fileName = fileInfo.Name;

                //根据选择的矢量文件的路径打开工作空间
                IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
                IWorkspace wp = wsf.OpenFromFile(filePath, 0);
                IFeatureWorkspace fw = (IFeatureWorkspace)wp;
                IFeatureClass featureClass = fw.OpenFeatureClass(fileName);

                //构造一个剪裁过滤器
                IClipFilter2 clipRaster = new ClipFilterClass();
                clipRaster.ClippingType = esriRasterClippingType.esriRasterClippingOutside;
                //将矢量数据的几何属性加到过滤器中
                IGeometry clipGeometry;
                IFeature feature;

                //将矢量数据中的每一个IFeature几何形状添加到clipGeometry
                for (int i = 0; i < featureClass.FeatureCount(null); i++)
                {
                    feature = featureClass.GetFeature(i);
                    clipGeometry=feature.Shape;
                    clipRaster.Add(clipGeometry);
                }
                //将这个过滤器作用于栅格图像
                IPixelOperation pixelOp = (IPixelOperation)raster;
                pixelOp.PixelFilter = (IPixelFilter)clipRaster;

                //如果输入的栅格中并不包含NoData和曾经使用过的最大像素深度，则输出文件的像素深度和NoData赋值
                IRasterProps rasterProps = (IRasterProps)raster;
                rasterProps.NoDataValue = 0;
                rasterProps.PixelType = rstPixelType.PT_USHORT;
                //存储剪裁结果栅格图像
                IWorkspace rstWs = wsf.OpenFromFile(@"D:\RDB", 0);
                //保存输出
                ISaveAs saveas = (ISaveAs)raster;
                saveas.SaveAs("clip_result.tif", rstWs, "TIFF");

                //加载显示裁剪结果图像
                IRasterLayer clipLayer = new RasterLayerClass();
                clipLayer.CreateFromRaster(raster);
                clipLayer.Name = "Clip_Result";
                clipLayer.SpatialReference = ((IGeoDataset)raster).SpatialReference;

                //添加到控件中
                axMapControl1.AddLayer(clipLayer);
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();
                //更新combobox里面的选项，图层和波段的
                iniCmbItems();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //点击按钮实现高空间分辨率单波段图像和低空间分辨率多波段图像的融合操作
        private void btn_PanSharpen_Click(object sender, EventArgs e)
        {
            try {
                ILayer sigleLayer = GetLayerByName(cmb_PanSharpenSigleLayer.SelectedItem.ToString());
                ILayer multiLayer = GetLayerByName(cmb_PanSharpenMultiLayer.SelectedItem.ToString());
                IRaster2 panRaster2 = ((IRasterLayer)sigleLayer).Raster as IRaster2;
                IRaster2 multiRaster2 = ((IRasterLayer)multiLayer).Raster as IRaster2;
                IRasterDataset panDataset = panRaster2.RasterDataset;
                IRasterDataset multiDataset = multiRaster2.RasterDataset;
                //默认波段顺序，RGB和近红外
                //创建全色和多光谱栅格数据集的full栅格对象
                IRaster panRaster = ((IRasterDataset2)panDataset).CreateFullRaster();
                IRaster multiRaster = ((IRasterDataset2)multiDataset).CreateFullRaster();
                //设置红外波段
                IRasterBandCollection rasterbandCol = (IRasterBandCollection)multiRaster;
                IRasterBandCollection infredRaster = new RasterClass();
                infredRaster.AppendBand(rasterbandCol.Item(3));

                //设置全色波段的属性
                IRasterProps panSharpenRasterProps = (IRasterProps)multiRaster;
                IRasterProps panRasterProps = (IRasterProps)panRaster;
                panSharpenRasterProps.Width = panRasterProps.Width;
                panSharpenRasterProps.Height = panRasterProps.Height;
                panSharpenRasterProps.Extent = panRasterProps.Extent;
                multiRaster.ResampleMethod = rstResamplingTypes.RSP_BilinearInterpolationPlus;

                //创建全色锐化过滤器和设置其参数
                IPansharpeningFilter pansharpenFilter = new PansharpeningFilterClass();
                pansharpenFilter.InfraredImage = (IRaster)infredRaster;
                pansharpenFilter.PanImage = (IRaster)panRaster;
                pansharpenFilter.PansharpeningType = esriPansharpeningType.esriPansharpeningESRI;
                pansharpenFilter.PutWeights(0.1, 0.167, 0.167, 0.5);

                //将全色锐化过滤器设置于多光谱栅格对象上
                IPixelOperation pixeOperation = (IPixelOperation)multiRaster;
                pixeOperation.PixelFilter = (IPixelFilter)pansharpenFilter;

                //保存结果数据集，并加载显示
                //加载显示裁剪结果图像
                IRasterLayer panSharpenLayer = new RasterLayerClass();
                panSharpenLayer.CreateFromRaster(multiRaster);
                panSharpenLayer.Name = "panSharpen_Result";
                panSharpenLayer.SpatialReference = ((IGeoDataset)multiRaster).SpatialReference;

                //添加到控件中
                axMapControl1.AddLayer(panSharpenLayer);
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();

                //更新combobox里面的选项（图层和波段的）
                iniCmbItems();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                     
        }

        //点击镶嵌按钮，对选中的栅格目录的遥感影响进行镶嵌处理
        private void btn_Mosaic_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //定义待用的字符串变量，表示原栅格数据文件夹路径、结果栅格数据保存路径
                //特别地，先创建一个个人地理空间数据库，再在其中创建一个栅格目录
                string inputFolder = @"D:\RDB\mosaic";
                string outputFolder = @"D:\RDB";
                //string outputName = "mosaic.tif";
                string tempRasterCatalog = "temp_rc";
                string tempPGDB = "temp.mdb";
                string tempPGDBPath = outputFolder + "\\" + tempPGDB;
                string tempRasterCatalogPath = tempPGDBPath + "\\" + tempRasterCatalog;

                //使用geoprocessing来创建地理数据库、栅格目录以及加载目录到栅格目录中
                Geoprocessor geoprocessor = new Geoprocessor();
                //在临时文件夹中创建个人地理数据库
                CreatePersonalGDB createPersonalGDB = new CreatePersonalGDB();
                createPersonalGDB.out_folder_path = outputFolder;
                createPersonalGDB.out_name = tempPGDB;
                //调用geopeocessor的execute方法执行创见个人地理数据库
                geoprocessor.Execute(createPersonalGDB, null);
                
                //在新创建的个人地理数据库中创建一个非托管的栅格目录
                CreateRasterCatalog createRasterCatalog = new CreateRasterCatalog();
                //设置创建的栅格目录的输出路径、输出名字和栅格托管类型
                createRasterCatalog.out_path = tempPGDBPath;
                createRasterCatalog.out_name = tempRasterCatalog;
                createRasterCatalog.raster_management_type = "unmanaged";
                //调用geoprocessor的execute方法执行创建栅格目录
                geoprocessor.Execute(createRasterCatalog, null);

                //把用于镶嵌的原始栅格数据加载到新创建的非托管的栅格目录中
                WorkspaceToRasterCatalog wsToRasterCatalog = new WorkspaceToRasterCatalog();
                //设置加载栅格数据的栅格目录路径、栅格数据路径、加载的类型（是否包含子文件夹）
                wsToRasterCatalog.in_raster_catalog = tempRasterCatalogPath;
                wsToRasterCatalog.in_workspace = inputFolder;
                wsToRasterCatalog.include_subdirectories = "INCLUDE_SUBDIRECTORIES";
                //调用geoprocessor的execute方法执行加载栅格数据到栅格目录中
                geoprocessor.Execute(wsToRasterCatalog, null);

                //打开刚刚创建的个人地理数据库，以获取栅格目录对象
                IWorkspaceFactory wsf = new AccessWorkspaceFactoryClass();
                IWorkspace mworkspace = wsf.OpenFromFile(tempPGDBPath, 0);
                IRasterWorkspaceEx rasterWorkspaceEx = mworkspace as IRasterWorkspaceEx;
                IRasterCatalog rstCatalog = rasterWorkspaceEx.OpenRasterCatalog(tempRasterCatalog);

                //定义一个影像镶嵌对象
                IMosaicRaster mosaicRaster = new MosaicRasterClass();
                //把栅格目录中所有的影像镶嵌成为一个栅格图像
                mosaicRaster.RasterCatalog = rstCatalog;

                //设置镶嵌的颜色映射表模式和像素运算类型
                mosaicRaster.MosaicColormapMode = rstMosaicColormapMode.MM_FIRST;
                mosaicRaster.MosaicOperatorType = rstMosaicOperatorType.MT_MAX;
                
                //打开输出结果数据集保存路径的工作空间
                IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactory();
                string savefile = @"D:\RDB\";
                IWorkspace workspace_save = workspaceFactory.OpenFromFile(outputFolder,0);
                string filename = @"mosaic_result.tif";

                //转换ISavsAs接口，实现结果数据集保存，可以保存为Tif或其他格式
                ISaveAs saveas = (ISaveAs)mosaicRaster;
                //通过saves方法实现镶嵌栅格图像的保存
                saveas.SaveAs(filename, workspace_save, "TIFF");
                //根据保存文件的存储文件夹路径获取栅格工作空间
                IWorkspaceFactory pRasterWsFac = new RasterWorkspaceFactoryClass();
                IWorkspace pWs = pRasterWsFac.OpenFromFile(savefile, 0);
                IRasterDataset pRasterDs = null;
                IRasterWorkspace pRasterWs;
                pRasterWs = pWs as IRasterWorkspace;
                pRasterDs = pRasterWs.OpenRasterDataset(filename);

                IRaster praster = pRasterDs.CreateDefaultRaster();
                IRasterLayer pLayer = new RasterLayerClass();
                pLayer.CreateFromRaster(praster);
                pLayer.Name = "mosaic_result";
                //更新控件
                axMapControl1.AddLayer(pLayer);
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();

            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }

        }
        //点击变换按钮，对选中的图层实施图像变换操作
        private void btn_Transform_Click(object sender, EventArgs e)
        {
            try
            {
                ILayer layer = GetLayerByName(cmb_FliterLayer.SelectedItem.ToString());
                IRasterLayer rstLayer = layer as IRasterLayer;
                IRaster2 raster2 = rstLayer.Raster as IRaster2;
                IGeoDataset geoDataset = raster2.RasterDataset as IGeoDataset;
                int angle=0;
                if(txb_TransformAngle.Text!="") angle= int.Parse(txb_TransformAngle.Text);
                //创建栅格图像变换操作接口的对象
                ITransformationOp transop = new RasterTransformationOpClass();
                //定义输出地理数据集的对象
                IGeoDataset outdataset;

                switch (cmb_TransformMethod.SelectedIndex)
                {
                    case 0://翻转
                        outdataset = transop.Flip(geoDataset);
                        break;
                    case 1://镜像
                        outdataset = transop.Mirror(geoDataset);
                        break;
                    case 2://剪裁
                        fClip = true;
                        MessageBox.Show("请使用鼠标在图上绘制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    case 3://旋转
                        object missing = Type.Missing;
                        outdataset = transop.Rotate(geoDataset, esriGeoAnalysisResampleEnum.esriGeoAnalysisResampleNearest, angle, ref missing);
                        break;
                    default:
                        return;

                }
                //通过图像变化结果获取栅格数据集，进而创建栅格图层加以显示
                IRasterDataset rasterdataset;
                IRaster outraster;
                //获取结果数据集
                rasterdataset = (IRasterDataset)outdataset;
                outraster = rasterdataset.CreateDefaultRaster();
                IRaster resRaster = outraster as IRaster;
                IRasterLayer resLayer = new RasterLayerClass();
                resLayer.CreateFromRaster(resRaster);
                resLayer.Name = "Transformation";
                resLayer.SpatialReference = outdataset.SpatialReference;

                //加载显示栅格图层
                axMapControl1.AddLayer(resLayer);
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
        ////处于剪裁状态时候，鼠标在地图上的按下事件即为绘制剪裁矩形相应
        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            try
            {
                //如果是处于裁剪状态
                if (fClip == true)
                {
                    //获取屏幕显示有关的接口对象（与ActiveView有关）
                    IScreenDisplay screenDisplay = axMapControl1.ActiveView.ScreenDisplay;
                    //rubberband橡皮筋接口用来绘制图形
                    IRubberBand rubberBand = new RubberEnvelopeClass();
                    //获取绘制的几何图形IGeometry接口
                    IGeometry geometry = rubberBand.TrackNew(screenDisplay, null);
                    //用IEnvelop接口获取几何图形的包络范围矩形
                    IEnvelope cutEnv = null;
                    cutEnv = geometry.Envelope;
                    //获取选中的图层
                    ILayer layer = GetLayerByName(cmb_TransformLayer.SelectedItem.ToString());
                    if (layer is IRasterLayer)
                    {
                        ITransformationOp transop = new RasterTransformationOpClass();
                        IRasterLayer rstLayer = layer as IRasterLayer;
                        IRaster2 raster2 = rstLayer.Raster as IRaster2;
                        IGeoDataset geoDataset = raster2.RasterDataset as IGeoDataset;

                        //创建结果地理数据集的对象
                        IGeoDataset outdataset;
                        //执行剪裁操作
                        outdataset = transop.Clip(geoDataset, cutEnv);

                        //通过图像变化结果获取栅格数据集，进而创建栅格图层加以显示
                        IRasterDataset rasterdataset;
                        IRaster outraster;
                        //获取结果数据集
                        rasterdataset = (IRasterDataset)outdataset;
                        outraster = rasterdataset.CreateDefaultRaster();
                        IRaster resRaster = outraster as IRaster;
                        IRasterLayer resLayer = new RasterLayerClass();
                        resLayer.CreateFromRaster(resRaster);
                        resLayer.Name = "Transformation";
                        resLayer.SpatialReference = outdataset.SpatialReference;

                        //加载显示栅格图层
                        axMapControl1.AddLayer(resLayer);
                        axMapControl1.ActiveView.Refresh();
                        axTOCControl1.Update();
                    }
                }
                //如果是extraction剪裁状态
                else if (fExtraction == true)
                {
                    //获取选择的图层栅格数据
                    ILayer layer = GetLayerByName(cmb_Extraction.SelectedItem.ToString());
                    //获取栅格图层的栅格对象
                    if (layer is IRasterLayer)
                    {
                        IRasterLayer rasterLayer = layer as IRasterLayer;
                        IRaster2 raster2 = rasterLayer.Raster as IRaster2;
                        IGeoDataset geoDataset = raster2.RasterDataset as IGeoDataset;
                        //鼠标点击屏幕绘制多边形
                        IPolygon pPolygon = axMapControl1.TrackPolygon() as IPolygon;
                        //创建栅格数据裁剪分析操作相关的类对象
                        IExtractionOp pExtractionOp = new ESRI.ArcGIS.SpatialAnalyst.RasterExtractionOpClass();
                        //执行裁剪分析操作得到结果数据集
                        IGeoDataset pGeoOutput = pExtractionOp.Polygon(geoDataset, pPolygon, true);

                        //加载显示裁剪分析结果
                        IRasterLayer resLayer = new RasterLayerClass();
                        //根据结果栅格数据集创建和赋值栅格图层
                        resLayer.CreateFromRaster((IRaster)pGeoOutput);
                        //图层名字
                        resLayer.Name = "Extraction";
                        //更新控件
                        axMapControl1.Map.AddLayer(resLayer);
                        axMapControl1.ActiveView.Extent = resLayer.AreaOfInterest;
                        axMapControl1.ActiveView.Refresh();
                        axTOCControl1.Update();
                        //更新combobox里面的选项（图层和波段的）
                        iniCmbItems();
                    }
                    fExtraction = false;
                }
                //如果是处于通视分析的状态
                else if (fLineOfSight == true)
                {
                    ILayer layer = GetLayerByName(cmb_LineOfSightLayer.SelectedItem.ToString());
                    if (layer is IRasterLayer)
                    {
                        //获取需要进行通视分析的栅格数据对象
                        IRasterLayer pRasterLayer = (IRasterLayer)layer;
                        //创建栅格表面分析的对象并设置处理栅格数据对象
                        IRasterSurface pRasterSurface = new RasterSurfaceClass();
                        pRasterSurface.PutRaster(pRasterLayer.Raster, 0);
                        //接口转换ISurface
                        ISurface pSurface = pRasterSurface as ISurface;
                        //地图上跟踪绘制直线，得到几何对象
                        IPolyline pPolyline = axMapControl1.TrackLine() as IPolyline;
                        IPoint pPoint = null;
                        IPolyline pVPolyline = null;
                        IPolyline pInPolyline = null;
                        //设置参数
                        object pRef = 0.13;
                        bool pBool = true;
                        //获取Dem的高程
                        double pZ1 = pSurface.GetElevation(pPolyline.FromPoint);
                        double pZ2 = pSurface.GetElevation(pPolyline.ToPoint);
                        //创建IPoint对象，赋值高程和xy值
                        IPoint pPoint1 = new PointClass();
                        pPoint1.X = pPolyline.FromPoint.X;
                        pPoint1.Y = pPolyline.FromPoint.Y;
                        pPoint1.Z = pZ1;
                        IPoint pPoint2 = new PointClass();
                        pPoint2.X = pPolyline.ToPoint.X;
                        pPoint2.Y = pPolyline.ToPoint.Y;
                        pPoint2.Z = pZ2;
                        //调用Isurface接口的getlineofsight方法得到通视范围
                        pSurface.GetLineOfSight(pPoint1, pPoint2, out pPoint, out pVPolyline, out pInPolyline, out pBool, false, false,ref pRef);
                        if (pVPolyline != null)
                        {
                            //如果可视范围不为null,则进行渲染和显示
                            IElement pLineElementV = new LineElementClass();
                            pLineElementV.Geometry = pVPolyline;
                            ILineSymbol pLinesymbolV = new SimpleLineSymbolClass();
                            pLinesymbolV.Width = 2;
                            IRgbColor pColorV = new RgbColorClass();
                            pColorV.Green = 255;
                            pLinesymbolV.Color = pColorV;
                            ILineElement pLineV = pLineElementV as ILineElement;
                            pLineV.Symbol = pLinesymbolV;
                            axMapControl1.ActiveView.GraphicsContainer.AddElement(pLineElementV, 0);
                        }
                        if (pInPolyline != null)
                        {
                            //如果不可视范围不为null，则进行渲染和显示
                            IElement pLineElementIn = new LineElementClass();
                            pLineElementIn.Geometry = pInPolyline;
                            ILineSymbol pLinesymbolIn = new SimpleLineSymbolClass();
                            pLinesymbolIn.Width = 2;
                            IRgbColor pColorIn = new RgbColorClass();
                            pColorIn.Red = 255;
                            pLinesymbolIn.Color = pColorIn;
                            ILineElement pLineIn = pLineElementIn as ILineElement;
                            pLineIn.Symbol = pLinesymbolIn;
                            axMapControl1.ActiveView.GraphicsContainer.AddElement(pLineElementIn,1);
                        }
                        //对试图范围进行局部刷新
                        axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    }
                    fLineOfSight = false;
                }
                //如果是处于视域分析的状态
                else if (fVisibility == true)
                {
                    ILayer layer = GetLayerByName(cmb_VisibilityLayer.SelectedItem.ToString());
                    //获取栅格图层的栅格对象
                    if (layer is IRasterLayer)
                    {
                        IRasterLayer rasterLayer = layer as IRasterLayer;
                        IRaster2 raster2 = rasterLayer.Raster as IRaster2;
                        IGeoDataset geoDataset = raster2.RasterDataset as IGeoDataset;
                        //工作控件
                        IFeatureWorkspace fcw = (IFeatureWorkspace)workspace;
                        //创建要素类的字段集合
                        IFields fields = new FieldsClass();
                        IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
                        //添加OID字段
                        IField oidField = new FieldClass();                      
                        IFieldEdit oidFieldEdit= (IFieldEdit)oidField;
                        oidFieldEdit.Name_2 ="0ID";
                        oidFieldEdit.Type_2= esriFieldType.esriFieldTypeOID;
                        fieldsEdit.AddField(oidField);
                        // 创建几何字段
                        IGeometryDef geometryDef = new GeometryDefClass();
                        IGeometryDefEdit geometryDefEdit= (IGeometryDefEdit) geometryDef ;
                        geometryDefEdit.GeometryType_2 =esriGeometryType.esriGeometryPoint ;
                        ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                        ISpatialReference spatialReference=spatialReferenceFactory.CreateProjectedCoordinateSystem ((int)esriSRProjCSType.esriSRProjCS_NAD1983UTM_20N) ;
                        ISpatialReferenceResolution spatialReferenceResolution=(ISpatialReferenceResolution) spatialReference ;
                        spatialReferenceResolution.ConstructFromHorizon();
                        spatialReferenceResolution.SetDefaultXYResolution();
                        ISpatialReferenceTolerance spatialReferenceTolerance =(ISpatialReferenceTolerance) spatialReference ;
                        spatialReferenceTolerance.SetDefaultXYTolerance();
                        geometryDefEdit.SpatialReference_2 = spatialReference ;
                        // 添加几何字段
                        IField geometryField= new FieldClass();
                        IFieldEdit geometryFieldEdit= (IFieldEdit) geometryField ;
                        geometryFieldEdit.Name_2 = "Shape";
                        geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                        geometryFieldEdit.GeometryDef_2 = geometryDef;
                        fieldsEdit.AddField(geometryField);
                        //创建name字段
                        IField nameField = new FieldClass();
                        IFieldEdit nameFieldEdit = (IFieldEdit)nameField;
                        nameFieldEdit.Name_2 = "Name";
                        nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                        nameFieldEdit.Length_2 = 20;
                        fieldsEdit.AddField(nameField);
                        //利用IFieldChecker创建验证字段集合
                        IFieldChecker fieldChecker = new FieldCheckerClass();
                        IEnumFieldError enumFieldError = null;
                        IFields validatedFields = null;
                        fieldChecker.ValidateWorkspace = (IWorkspace)fcw;
                        fieldChecker.Validate(fields, out enumFieldError, out validatedFields);
                        //创建要素类
                        Random rd = new Random();
                        int i = rd.Next();
                        IFeatureClass featureClass = fcw.CreateFeatureClass("visibility_featureclass"  + (i%10000), validatedFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

                        //鼠标点击屏幕绘制点
                        IPoint pt;
                        pt = axMapControl1.ToMapPoint(e.x, e.y);
                        //创建要素
                        IFeature feature = featureClass.CreateFeature();
                        feature.Shape = pt;
                        //应用适当的子类型要要素中
                        ISubtypes subtypes = (ISubtypes)featureClass;
                        IRowSubtypes rowSubtypes = (IRowSubtypes)feature;
                        if (subtypes.HasSubtype) rowSubtypes.SubtypeCode = 3;
                        //初始化要素的所有默认设置
                        rowSubtypes.InitDefaultValues();
                        //实现保存
                        feature.Store();
                        //IfeatureClass转换IGeoDataset
                        IGeoDataset geodataset = (IGeoDataset)featureClass;
                        //创建栅格数据表而分析操作相关的类对象
                        ISurfaceOp surfaceOp = new RasterSurfaceOpClass();
                        //执行视域分析操作得到结果数据集
                        IGeoDataset pGeoOutput = surfaceOp.Visibility(geoDataset, geodataset, esriGeoAnalysisVisibilityEnum.esriGeoAnalysisVisibilityObservers);
                        //删除刚刚创建的要素类
                        IDataset dataset = featureClass as IDataset;
                        dataset.Delete();

                        //加载和显示结果

                        IRasterLayer resLayer = new RasterLayerClass();
                        resLayer.CreateFromRaster((IRaster)pGeoOutput);
                        resLayer.Name = "Visibility";
                        resLayer.SpatialReference = pGeoOutput.SpatialReference;

                        //加载显示栅格图层
                        axMapControl1.AddLayer(resLayer);
                        axMapControl1.ActiveView.Refresh();
                        axTOCControl1.Update();
                    }
                    fVisibility = false;
                }
                else if (fTIN == true)
                {
                    //获取选中的dem图层和raster数据
                    ILayer layer = GetLayerByName(cmb_CreateTINLayer.SelectedItem.ToString());
                    //获取栅格图层的栅格对象
                    if (layer is IRasterLayer)
                    {
                        //获取需要进行构建TIN的栅格数据对象
                        IRasterLayer pRasterLayer = (IRasterLayer)layer;
                        //为构建TIN添加Point结点
                        IPoint Point = new PointClass();
                        Point = axMapControl1.ToMapPoint(e.x, e.y);
                        IRasterSurface rasterSurface = new RasterSurfaceClass();
                        rasterSurface.PutRaster(pRasterLayer.Raster, 0);
                        ISurface surface = rasterSurface as ISurface;
                        Point.Z = surface.GetElevation(Point);

                        //添加point到TINedit中
                        TinEdit.AddPointZ(Point, 0);

                        //高亮画点
                        //获取map和activeView
                        IGraphicsContainer pGra = (IGraphicsContainer)axMapControl1.Map;
                        IActiveView pActiview = (IActiveView)pGra;

                        //创建和设置element
                        IMarkerElement pMarkerElement = new MarkerElementClass();
                        //创建和设置symbol
                        IMarkerSymbol pMarkSym = new SimpleMarkerSymbolClass();
                        IRgbColor rgbColor = new RgbColorClass();
                        rgbColor.Red = 255;
                        rgbColor.Green = 0;
                        rgbColor.Blue = 0;
                        pMarkSym.Color = rgbColor;
                        pMarkSym.Size = 5;
                        pMarkerElement.Symbol = pMarkSym;
                        //设置Geometry
                        IElement pElement;
                        pElement = pMarkerElement as IElement;
                        pElement.Geometry = Point;
                        //添加点marker,刷新activeview
                        pGra.AddElement(pElement, 0);
                        pActiview.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                    }
                }
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }      
        }
        //点击滤波操作，对选中的图层进行滤波操作
        private void btn_Filter_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选中的图层
                ILayer layer = GetLayerByName(cmb_FliterLayer.SelectedItem.ToString());
                IRasterLayer rstLayer = layer as IRasterLayer;
                IRaster raster = rstLayer.Raster;

                IConvolutionFunctionArguments rasterFunctionArguments = (IConvolutionFunctionArguments)new ConvolutionFunctionArguments();

                //设置输入栅格数据
                rasterFunctionArguments.Raster = raster;
                rasterFunctionArguments.Type = (esriRasterFilterTypeEnum)cmb_FliterMethod.SelectedIndex;
                //创建Raster Function对象
                IRasterFunction rasterFunction = new ConvolutionFunction();
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                functionRasterDatasetName.FullName = @"D:\RDB"+"\\"+cmb_FliterMethod.SelectedItem.ToString();
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                functionRasterDataset.Init(rasterFunction, rasterFunctionArguments);

                IRasterDataset rasData = functionRasterDataset as IRasterDataset;
                IRasterLayer pRstLayer = new RasterLayerClass();
                pRstLayer.CreateFromDataset(rasData);

                ILayer iLayer = pRstLayer as ILayer;
                axMapControl1.AddLayer(iLayer);
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();

                //更新combobox里面的选项（图层的和波段的）
                iniCmbItems();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txb_ImportRstDataset_TextChanged(object sender, EventArgs e)
        {

        }
        //点击山体阴影函数，对选定的DEM数据进行山体阴影函数处理
        private void btn_HillShade_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
               //获取选择的图层栅格数据
                ILayer layer = GetLayerByName(cmb_HillShade.SelectedItem.ToString());
                //获取栅格图层的栅格对象
                if (layer is IRasterLayer)
                {
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    IHillshadeFunctionArguments hillshadeFunctionArugments = (IHillshadeFunctionArguments)new HillshadeFunctionArguments();
                    hillshadeFunctionArugments.Azimuth = 50;
                    hillshadeFunctionArugments.ZFactor = 1 / 11111.0;
                    hillshadeFunctionArugments.DEM = raster2;

                    IRasterFunction rasterFunction = new HillshadeFunction();
                    IFunctionRasterDataset functionRasterDataset= new FunctionRasterDataset();
                    IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                    functionRasterDatasetName.FullName = @"D:\RDB" + "\\" + cmb_HillShade.SelectedItem.ToString()+"HillShade";
                    functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                    functionRasterDataset.Init(rasterFunction, hillshadeFunctionArugments);

                    IRasterDataset rasData = functionRasterDataset as IRasterDataset;
                    IRasterLayer pRstLayer = new RasterLayerClass();
                    pRstLayer.CreateFromDataset(rasData);

                    ILayer iLayer = pRstLayer as ILayer;
                    axMapControl1.AddLayer(iLayer);
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    //更新下拉框选项内容
                    iniCmbItems();
                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //点击坡度函数按钮，对选中的dem数据进行坡度计算
        private void btn_Slope_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选择的图层栅格数据
                ILayer layer = GetLayerByName(cmb_Slope.SelectedItem.ToString());
                //获取栅格图层的栅格对象
                if (layer is IRasterLayer)
                {
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    ISlopeFunctionArguments slopeFunctionArugments = (ISlopeFunctionArguments)new SlopeFunctionArguments();
                    slopeFunctionArugments.DEM = raster2;
                    slopeFunctionArugments.ZFactor = 1 / 11111.0;
                    IRasterFunction rasterFunction = new SlopeFunction();
                    IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                    IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                    functionRasterDatasetName.FullName = @"D:\RDB" + "\\" + cmb_Slope.SelectedItem.ToString() + "Slope";
                    functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                    functionRasterDataset.Init(rasterFunction, slopeFunctionArugments);

                    IRasterDataset rasData = functionRasterDataset as IRasterDataset;
                    IRasterLayer pRstLayer = new RasterLayerClass();
                    pRstLayer.CreateFromDataset(rasData);

                    ILayer iLayer = pRstLayer as ILayer;
                    axMapControl1.AddLayer(iLayer);
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    //更新下拉框选项内容
                    iniCmbItems();
                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //点击坡向函数按钮，对选中的dem进行坡向函数处理分析
        private void btn_Aspect_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选择的图层栅格数据
                ILayer layer = GetLayerByName(cmb_Aspect.SelectedItem.ToString());
                //获取栅格图层的栅格对象
                if (layer is IRasterLayer)
                {
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;

                    IRasterFunction rasterFunction = new AspectFunction();
                    IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                    IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                    functionRasterDatasetName.FullName = @"D:\RDB" + "\\" + cmb_Aspect.SelectedItem.ToString() + "Aspect";
                    functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                    functionRasterDataset.Init(rasterFunction, raster2);

                    IRasterDataset rasData = functionRasterDataset as IRasterDataset;
                    IRasterLayer pRstLayer = new RasterLayerClass();
                    pRstLayer.CreateFromDataset(rasData);

                    ILayer iLayer = pRstLayer as ILayer;
                    axMapControl1.AddLayer(iLayer);
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    //更新下拉框选项内容
                    iniCmbItems();
                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //点击邻域分析按钮，对选中的DEM数据进行领域分析
        private void btn_Neighborhood_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选择的图层栅格数据
                ILayer layer = GetLayerByName(cmb_Aspect.SelectedItem.ToString());
                int indexMethod = cmb_NeighborhoodMethod.SelectedIndex;
                //获取栅格图层的栅格对象
                if (layer is IRasterLayer)
                {
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    IGeoDataset geoDataset = raster2.RasterDataset as IGeoDataset;

                    //创建栅格数据邻域分析操作相关的类对象
                    INeighborhoodOp pNeighborhoodOP = new RasterNeighborhoodOpClass();
                    //创建栅格数据邻域分析参数对象
                    IRasterNeighborhood pRasterNeighborhood = new RasterNeighborhoodClass();
                    //设置矩形邻域分析范围
                    pRasterNeighborhood.SetRectangle(3, 3, esriGeoAnalysisUnitsEnum.esriUnitsCells);
                    IGeoDataset pGeoOutput = null;
                    //执行邻域分析操作得到结果数据集
                    switch (indexMethod)
                    {
                        case 0:
                            pGeoOutput = pNeighborhoodOP.Filter(geoDataset, esriGeoAnalysisFilterEnum.esriGeoAnalysisFilter3x3HighPass, true);
                            break;
                        case 1:
                            pGeoOutput = pNeighborhoodOP.Filter(geoDataset, esriGeoAnalysisFilterEnum.esriGeoAnalysisFilter3x3LowPass, true);
                            break;
                        case 2:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMajority, pRasterNeighborhood, true);
                            break;
                        case 3:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMaximum, pRasterNeighborhood, true);
                            break;
                        case 4:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMean, pRasterNeighborhood, true);
                            break;
                        case 5:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMedian, pRasterNeighborhood, true);
                            break;
                        case 6:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMinimum, pRasterNeighborhood, true);
                            break;
                        case 7:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsMinority, pRasterNeighborhood, true);
                            break;
                        case 8:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsRange, pRasterNeighborhood, true);
                            break;
                        case 9:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsStd, pRasterNeighborhood, true);
                            break;
                        case 10:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsSum, pRasterNeighborhood, true);
                            break;
                        case 11:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsVariety, pRasterNeighborhood, true);
                            break;
                        case 12:
                            pGeoOutput = pNeighborhoodOP.BlockStatistics(geoDataset, esriGeoAnalysisStatisticsEnum.esriGeoAnalysisStatsLength, pRasterNeighborhood, true);
                            break;
                        default:
                            break;
                    }
                    //加载显示邻域分析处理后的栅格图像
                    IRasterLayer resultRstLayer = new RasterLayerClass();
                    resultRstLayer.CreateFromRaster((IRaster)pGeoOutput);
                    ILayer resultLayer = (ILayer)resultRstLayer;
                    axMapControl1.Map.AddLayer(resultRstLayer);
                    //刷新视图显示
                    axMapControl1.ActiveView.Extent = resultRstLayer.AreaOfInterest;
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    //更新下拉框选项内容
                    iniCmbItems();
                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btn_Extraction_Click(object sender, EventArgs e)
        {
            fExtraction = true;
            MessageBox.Show("请使用鼠标在图上绘制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        //点击通视分析按钮，进行通视分析的函数
        private void btn_LineOfSight_Click(object sender, EventArgs e)
        {
            fLineOfSight = true;
            MessageBox.Show("请使用鼠标在图上绘制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        //点击视域分析按钮，进行视域分析的函数
        private void btn_Visibility_Click(object sender, EventArgs e)
        {
            fVisibility = true;
            MessageBox.Show("请使用鼠标在图上绘制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;            
        }
        //点击手绘构建TIN按钮，进行鼠标绘制构建TIN
        private void btm_CreateTin_Click(object sender, EventArgs e)
        {
            fTIN = true;
            ILayer layer = GetLayerByName(cmb_CreateTINLayer.SelectedItem.ToString());
            if (layer is IRasterLayer)
            {
                //获取需要构建TIN的栅格数据对象
                IRasterLayer rasterLayer = layer as IRasterLayer;
                //实例化新的空的TIN 
                TinEdit = new TinClass();
                //用一个Envelope初始化TIN，envelope的空间参考也成为TIN的空间参考
                IEnvelope Env = rasterLayer.AreaOfInterest;
                //根据envelop初始化tin
                TinEdit.InitNew(Env);
                //获取map，清除element的标记marker
                IGraphicsContainer pGra = (IGraphicsContainer)axMapControl1.Map;
                pGra.DeleteAllElements();
            }
            MessageBox.Show("请使用鼠标在图上绘制", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;    
        }
        //点击由DEM生成等高线按钮，对选中的DEM生成等高线
        private void btn_DEMContour_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选择的图层栅格数据
                ILayer layer = GetLayerByName(cmb_DEMContour.SelectedItem.ToString());          
                //获取栅格图层的栅格对象
                if (layer is IRasterLayer)
                {
                    IRasterLayer rstLayer = layer as IRasterLayer;
                    IRaster2 raster2 = rstLayer.Raster as IRaster2;
                    IGeoDataset geoDataset = raster2.RasterDataset as IGeoDataset;
                    //创建表面操作处理对象
                    ISurfaceOp2 sfop = new RasterSurfaceOpClass();
                    //根据设置的等高线起点高度、高度间距等对dem数据构建等高线
                    IGeoDataset outputGeoDataset = sfop.Contour(geoDataset, 100, Type.Missing, Type.Missing);

                    //加载显示获得的等高线featureclass
                    IFeatureClass featureClass = outputGeoDataset as IFeatureClass;
                    IFeatureLayer featureLayer = new FeatureLayerClass();
                    featureLayer.Name = "DEM_Contour";
                    featureLayer.FeatureClass = featureClass;
                    axMapControl1.Map.AddLayer(featureLayer);
                    axMapControl1.ActiveView.Extent = featureLayer.AreaOfInterest;
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    //更新下拉框选项内容
                    iniCmbItems();
                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //点击采样方法构建TIN的按钮，根据选中的DEM进行采样构建TIN
        private void btn_CreateTINAuto_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时，修改鼠标光标形状
            try
            {
                //获取选择的图层栅格数据
                ILayer layer = GetLayerByName(cmb_CreateTINLayer.SelectedItem.ToString());
                //判断图层
                if (layer is IRasterLayer)
                {
                    //获取map，清除所有element的标记marker
                    IGraphicsContainer pGra = (IGraphicsContainer)axMapControl1.Map;
                    pGra.DeleteAllElements();

                    //获取需要进行构建TIN的栅格数据对象
                    IRasterLayer pRasterLayer = (IRasterLayer)layer;
                    IRaster2 iRaster = pRasterLayer.Raster as IRaster2;
                    //转IGeodataset接口，获取extent范围
                    IGeoDataset pGeoData = iRaster as IGeoDataset;
                    IEnvelope pExtent = pGeoData.Extent;
                    //转换IRasterBandCollection接口，获取band波段
                    IRasterBandCollection pRasBC = iRaster as IRasterBandCollection;
                    IRasterBand pRasBand = pRasBC.Item(0);
                    //转换band接口为IRawPixels和IRasterProps，获取高度和宽度等信息
                    IRawPixels pRawPixels = pRasBand as IRawPixels;
                    IRasterProps pProps = pRawPixels as IRasterProps;
                    int iWid = pProps.Width;
                    int iHei = pProps.Height;
                    double w = iWid / 1000.0f;
                    double h = iHei / 1000.0f;
                    //设置像素快的高度和宽度
                    IPnt pBlockSize = new DblPntClass();
                    bool IterationFlag;
                    if (w < 1 && h < 1)//横纵都小于1000像素
                    {
                        pBlockSize.X = iWid;
                        pBlockSize.Y = iHei;
                        IterationFlag = false;
                    }
                    else
                    {
                        pBlockSize.X = 1001.0f;
                        pBlockSize.Y = 1001.0f;
                        IterationFlag = true;
                    }
                    //获取单元格尺寸大小
                    double cellsize = 0.0f;//栅格大小
                    IPnt pPnt1 = pProps.MeanCellSize();//栅格平均大小
                    cellsize = pPnt1.X;
                    //创建ITinEdit对象，用以构建TIN，并初始化extent
                    ITinEdit pTinEdit = new TinClass() as ITinEdit;
                    pTinEdit.InitNew(pExtent);
                    //创建ISpatialReference对象，并设置
                    ISpatialReference pSpatial = pGeoData.SpatialReference;
                    pExtent.SpatialReference = pSpatial;
                    IPnt pOrigin = new DblPntClass();
                    //像素块原点信息
                    IPnt pPixelBlockOrigin = new DblPntClass();
                    //栅格左上角像素中心坐标
                    double bX = pBlockSize.X;
                    double bY = pBlockSize.Y;
                    pBlockSize.SetCoords(bX, bY);
                    //获取IPixelBlock对象
                    IPixelBlock pPixelBlock = pRawPixels.CreatePixelBlock(pBlockSize);
                    object nodata = pProps.NoDataValue;//无值标记
                    //转换ITinAdvanced2接口，获取NodeCount
                    ITinAdvanced2 pTinNodeCount = pTinEdit as ITinAdvanced2;
                    int nodeCount = pTinNodeCount.NodeCount;
                    object vtMissing = Type.Missing;
                    object vPixels = null;//格子
                    if (!IterationFlag)//当为一个处理单元格子时
                    {
                        //原点坐标信息
                        pPixelBlockOrigin.SetCoords(0.0f, 0.0f);
                        //读取像素块
                        pRawPixels.Read(pPixelBlockOrigin, pPixelBlock);
                        //获取像素数组
                        vPixels = pPixelBlock.get_SafeArray(0);
                        double xMin = pExtent.XMin;
                        double yMax = pExtent.YMax;
                        pOrigin.X = xMin + cellsize / 2;
                        pOrigin.Y = yMax - cellsize / 2;
                        bX = pOrigin.X;
                        bY = pOrigin.Y;
                        //采集和添加结点
                        pTinEdit.AddFromPixelBlock(bX, bY, cellsize, cellsize, nodata, vPixels, 20.0f, ref vtMissing, out vtMissing);
                    }
                    else//当有多个处理单元格时，依次循环处理每个单元格
                    {
                        //遍历每个像素块，进行AddFromPixelBlock添加结点
                        int i = 0, j = 0, count = 0;
                        int FirstGoNodeCout = 0;
                        while (nodeCount != FirstGoNodeCout)
                        {
                            count++;
                            nodeCount = pTinNodeCount.NodeCount;
                            //依次循环处理
                            for (i = 0; i < h + 1; i++)
                            {
                                for (j = 0; j < w + 1; j++)
                                {
                                    double bX1, bY1, xMin1, yMax1;
                                    //像素块高度宽度
                                    bX1 = pBlockSize.X;
                                    bY1 = pBlockSize.Y;
                                    pPixelBlockOrigin.SetCoords(j * bX1, i * bY1);
                                    //读取像素块
                                    pRawPixels.Read(pPixelBlockOrigin, pPixelBlock);
                                    //获取像素数组
                                    vPixels = pPixelBlock.get_SafeArray(0);
                                    //原点坐标信息
                                    xMin1 = pExtent.XMin;
                                    yMax1 = pExtent.YMax;
                                    bX1 = pBlockSize.X;
                                    bY1 = pBlockSize.Y;
                                    pOrigin.X = xMin1 + j * bX1 * cellsize + cellsize / 2.0f;
                                    pOrigin.Y = yMax1 + i * bY1 * cellsize - cellsize / 2.0f;
                                    bX1 = pOrigin.X;
                                    bY1 = pOrigin.Y;
                                    //调用AddFromPixelBlock添加结点
                                    pTinEdit.AddFromPixelBlock(bX1, bY1, cellsize, cellsize, nodata, vPixels, 20.0f, ref vtMissing, out vtMissing);
                                    FirstGoNodeCout = pTinNodeCount.NodeCount;
                                }
                            }
                        }
                    }
                    //创建TIN图层
                    ITinLayer pTinLayer = new TinLayerClass();
                    pTinLayer.Name = "TIN";
                    //设置TIN数据
                    pTinLayer.Dataset = (ITinAdvanced2)pTinEdit;
                    //刷新视图
                    axMapControl1.Map.AddLayer(pTinLayer as ILayer);
                    axMapControl1.ActiveView.Extent = pTinLayer.AreaOfInterest;
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();
                    //更新combobox里面的选项（图层和波段的）
                    iniCmbItems();
                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //点击生成等高线按钮，对选中的TIN生成等高线
        private void btn_tinContour_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选中的TIN图层
                ILayer layer = GetLayerByName(cmb_tinContour.SelectedItem.ToString());
                //判断TIN图层
                if (layer is ITinLayer)
                {
                    //获取TIN对象
                    ITinLayer tinLayer = layer as ITinLayer;
                    ITin tin = tinLayer.Dataset;
                    ITinSurface tinSurface = tin as ITinSurface;
                    //实例化要素描述对象来获取构建要素类必须的字段集合
                    IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
                    IObjectClassDescription ocDescription = (IObjectClassDescription)fcDescription;
                    IFields fields = ocDescription.RequiredFields;
                    //找到shape形状字段，设置集合类型和投影坐标系统
                    int shapeFieldIndex = fields.FindField(fcDescription.ShapeFieldName);
                    IField field = fields.get_Field(shapeFieldIndex);
                    IGeometryDef geometryDef = field.GeometryDef;
                    IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                    //设置几何类型
                    geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                    ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                    ISpatialReference spatialReference = spatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_NAD1983UTM_20N);
                    ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
                    spatialReferenceResolution.ConstructFromHorizon();
                    spatialReferenceResolution.SetDefaultXYResolution();
                    ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
                    spatialReferenceTolerance.SetDefaultXYTolerance();
                    //设置坐标系统
                    geometryDefEdit.SpatialReference_2 = spatialReference;

                    //转换工作空间接口
                    IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                    //创建要素类
                    IFeatureClass featureClass = featureWorkspace.CreateFeatureClass("TinContour", fields, ocDescription.InstanceCLSID, ocDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple, fcDescription.ShapeFieldName, "");

                    //调用ITinSurface的Contour方法生成等高线
                    tinSurface.Contour(0, 100, featureClass, "Height", 0);

                    IFeatureLayer featureLayer = new FeatureLayerClass();
                    featureLayer.FeatureClass = featureClass;
                    featureLayer.Name = "TinContour";

                    axMapControl1.Map.AddLayer(featureLayer);
                    axMapControl1.ActiveView.Extent = featureLayer.AreaOfInterest;
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();

                    iniCmbItems();
                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }
        }
        //点击生成泰森多边形的按钮，对选中的TIN生成泰森多边形
        private void btn_Voronoi_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;//单击时修改鼠标光标形状
            try
            {
                //获取选中的TIN图层
                ILayer layer = GetLayerByName(cmb_TinVoronoi.SelectedItem.ToString());
                //判断TIN图层
                if (layer is ITinLayer)
                {
                    //获取TIN对象
                    ITinLayer tinLayer = layer as ITinLayer;
                    ITin tin = tinLayer.Dataset;
                    ITinNodeCollection tinNodeCollection = tin as ITinNodeCollection;

                    //实例化要素描述对象来获取构建要素类必须的字段集合
                    IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
                    IObjectClassDescription ocDescription = (IObjectClassDescription)fcDescription;
                    IFields fields = ocDescription.RequiredFields;
                    //找到shape形状字段，设置集合类型和投影坐标系统
                    int shapeFieldIndex = fields.FindField(fcDescription.ShapeFieldName);
                    IField field = fields.get_Field(shapeFieldIndex);
                    IGeometryDef geometryDef = field.GeometryDef;
                    IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                    //设置几何类型
                    geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                    ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                    ISpatialReference spatialReference = spatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_NAD1983UTM_20N);
                    ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
                    spatialReferenceResolution.ConstructFromHorizon();
                    spatialReferenceResolution.SetDefaultXYResolution();
                    ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
                    spatialReferenceTolerance.SetDefaultXYTolerance();
                    //设置坐标系统
                    geometryDefEdit.SpatialReference_2 = spatialReference;

                    //转换工作空间接口
                    IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                    //保存到指定路径
                    IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
                    IWorkspace wp = wsf.OpenFromFile("D://RDB", 0);
                    IFeatureWorkspace fw = (IFeatureWorkspace)wp;
                    //创建要素类
                    IFeatureClass featureClass = featureWorkspace.CreateFeatureClass("TinVoronoi", fields, ocDescription.InstanceCLSID, ocDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple, fcDescription.ShapeFieldName, "");

                    //调用ITinSurface的Contour方法生成等高线
                    tinNodeCollection.ConvertToVoronoiRegions(featureClass,null,null,"","");

                    IFeatureLayer featureLayer = new FeatureLayerClass();
                    featureLayer.FeatureClass = featureClass;
                    featureLayer.Name = "TinVoronoi";

                    axMapControl1.Map.AddLayer(featureLayer);
                    axMapControl1.ActiveView.Extent = featureLayer.AreaOfInterest;
                    axMapControl1.ActiveView.Refresh();
                    axTOCControl1.Update();

                    iniCmbItems();
                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally//最后再将鼠标光标设置成默认形状
            {
                this.Cursor = Cursors.Default;
            }

        }
        //结束TIN选点控制时触发函数
        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            try
            {
                if (fTIN == true)
                {
                    //还原fTIN标记变量的值
                    fTIN = false;
                    ITinLayer tinLayer = new TinLayerClass();
                    tinLayer.Name = "TIN";
                    tinLayer.Dataset = (ITinAdvanced2)TinEdit;

                    axMapControl1.Map.AddLayer(tinLayer);
                    axMapControl1.ActiveView.Extent = tinLayer.AreaOfInterest;
                    axMapControl1.ActiveView.Refresh();
                    axMapControl1.Update();

                    iniCmbItems();
                    
                }
            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //点击波段信息统计底层按钮后，从底层实现波段信息统计
        private void btn_Statistics_Bottom_Click(object sender, EventArgs e)
        {
            try
            {
                //获取选择的图层和波段，转换接口
                ILayer layer = GetLayerByName(cmb_StatistiicsLayer.SelectedItem.ToString());
                IRasterLayer rstLayer = null;
                if (layer is IRasterLayer) rstLayer = layer as IRasterLayer;
                else
                {
                    MessageBox.Show("所选择的图层并非栅格图层，无法进行操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //IRasterLayer rstLayer= GetLayerByName(cmb_NDVILayer.SelectedItem.ToString()) as IRasterLayer;
                IRaster2 raster2 = rstLayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;

                int index = cmb_StatisticsBand.SelectedIndex;
                if (cmb_StatisticsBand.SelectedItem.ToString() == "全部波段")
                {
                    string StatRes = null;
                    IRasterProps pRasterProps = raster2 as IRasterProps;
                    int Height = pRasterProps.Height;
                    int Width = pRasterProps.Width;
                    //定义并初始化数组，用户存储栅格内所有像元像素值
                    double[,] PixelValue = new double[Height, Width];
                    for (int k = 0; k < rstBandColl.Count; k++)
                    {
                        PixelValue = GetPixelValue(rstLayer, k);
                        double sum = 0;
                        int count = 0;
                        double max = PixelValue[0, 0];
                        double min = PixelValue[0, 0];
                        for (int i = 0; i < Height; i++)
                        {
                            for (int j = 0; j < Width; j++)
                            {
                                if (PixelValue[i, j] > max) max = PixelValue[i, j];
                                if (PixelValue[i, j] < min) min = PixelValue[i, j];
                                sum = sum + PixelValue[i, j];
                                count++;
                            }
                        }
                        double mean = sum / count;
                        double stdsum = 0;
                        for (int i = 0; i < Height; i++)
                        {
                            for (int j = 0; j < Width; j++)
                            {
                                stdsum = stdsum + (PixelValue[i, j] - mean) * (PixelValue[i, j] - mean);
                            }
                        }
                        double std = Math.Sqrt(stdsum / (count - 1));
                        StatRes = StatRes+ "第" + (k + 1) + "波段：" + "  平均值为:" + mean.ToString() + "  最大值为：" + max.ToString() + "  最小值为:" + min.ToString() + "  标准差为:" + std.ToString() + "\r\n";
                    }
                    //提示框输出统计结果
                    MessageBox.Show(StatRes, "统计结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int bandnum;
                    if (cmb_StatisticsBand.Items.Count > rstBandColl.Count) bandnum = index - 1;
                    else bandnum = index;
                    IRasterProps pRasterProps = raster2 as IRasterProps;
                    int Height = pRasterProps.Height;
                    int Width = pRasterProps.Width;
                    //定义并初始化数组，用户存储栅格内所有像元像素值
                    double[,] PixelValue = new double[Height, Width];
                    PixelValue = GetPixelValue(rstLayer, bandnum);

                    double sum = 0;
                    int count = 0;
                    double max = PixelValue[0, 0];
                    double min = PixelValue[0, 0];
                    for (int i = 0; i < Height; i++)
                    {
                        for (int j = 0; j < Width; j++)
                        {
                            if (PixelValue[i, j] > max) max = PixelValue[i, j];
                            if (PixelValue[i, j] < min) min = PixelValue[i, j];
                            sum = sum + PixelValue[i, j];
                            count++;
                        }
                    }
                    double mean = sum / count;
                    double stdsum=0;
                    for (int i = 0; i < Height; i++)
                    {
                        for (int j = 0; j < Width; j++)
                        {
                            stdsum = stdsum + (PixelValue[i, j] - mean) * (PixelValue[i, j] - mean);
                        }
                    }
                    double std = Math.Sqrt(stdsum / (count-1));
                    string StatRes = "第" + (bandnum + 1) + "波段：" + "  平均值为:" + mean.ToString() + "  最大值为：" + max.ToString() + "  最小值为:" + min.ToString() + "  标准差为:" + std.ToString() + "\r\n";
                    MessageBox.Show(StatRes, "统计结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //点击NDVI计算底层按钮后，从底层实现NDVI计算
        private void btn_NDVI_Bottom_Click(object sender, EventArgs e)
        {
            try
            {
                //获取选择的图层和波段，转换接口
                ILayer layer = GetLayerByName(cmb_NDVILayer.SelectedItem.ToString());
                IRasterLayer rstLayer = null;
                if (layer is IRasterLayer) rstLayer = layer as IRasterLayer;
                else
                {
                    MessageBox.Show("所选择的图层并非栅格图层，无法进行操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //IRasterLayer rstLayer= GetLayerByName(cmb_NDVILayer.SelectedItem.ToString()) as IRasterLayer;
                IRaster2 raster2 = rstLayer.Raster as IRaster2;
                IGeoDataset geoDataset = raster2.RasterDataset as IGeoDataset;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
                if (rstBandColl.Count < 3)
                {
                    MessageBox.Show("所选择的图层的栅格图层不满足NDVI计算条件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IRasterProps pRasterProps = raster2 as IRasterProps;
                int Height = pRasterProps.Height;
                int Width = pRasterProps.Width;
                double cellsizex = pRasterProps.MeanCellSize().X;
                
                double cellsizey = pRasterProps.MeanCellSize().Y;
                rstPixelType pixelType = pRasterProps.PixelType;
                ISpatialReference spatialReference = pRasterProps.SpatialReference;
                //MessageBox.Show(spatialReference.Name.ToString());
                IWorkspaceFactory pRasterWsFac = new RasterWorkspaceFactoryClass();
                IWorkspace pWs = pRasterWsFac.OpenFromFile(@"D://RDB", 0);
                IRasterWorkspace2 pRasterWs;
                pRasterWs = pWs as IRasterWorkspace2;
                IPoint origin = new PointClass();
                origin.PutCoords(pRasterProps.Extent.XMin, pRasterProps.Extent.YMin);
                //RasterWorkspace rasterWorkspace = (RasterWorkspace)workspace;
                ISpatialReference sr = new UnknownCoordinateSystemClass();
                IRasterDataset2 resultDataset = pRasterWs.CreateRasterDataset("NDVI.tif", "TIFF", origin, Width, Height, cellsizex, cellsizey, 1, rstPixelType.PT_DOUBLE, sr) as IRasterDataset2;
                IRaster resultRaster = resultDataset.CreateFullRaster();
                IRasterCursor resultRasterCursor = ((IRaster2)resultRaster).CreateCursorEx(null);

                IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
                IRaster2 raster = rasterDataset.CreateFullRaster() as IRaster2;
                IRasterCursor rasterCursor = raster.CreateCursorEx(null);

                IPixelBlock3 resultPixelBlock = null;
                IPixelBlock3 tempPixelBlock = null;
                IRasterEdit resultRasterEdit = resultRaster as IRasterEdit;

                long blockWidth = 0;
                long blockHeight = 0;
               // System.Array pixels;
                do
                {
                     resultPixelBlock = resultRasterCursor.PixelBlock as IPixelBlock3;
                     tempPixelBlock = rasterCursor.PixelBlock as IPixelBlock3;

                     System.Array pixels3 = (System.Array)tempPixelBlock.get_PixelData(2);
                     System.Array pixels4 = (System.Array)tempPixelBlock.get_PixelData(3);
                     //MessageBox.Show(pixels3.GetValue(0, 0).GetType().ToString());
                     blockHeight = resultPixelBlock.Height;
                     blockWidth = resultPixelBlock.Width;
                     System.Array resultPixels = (System.Array)resultPixelBlock.get_PixelData(0);
                     //MessageBox.Show(resultPixels.GetValue(0, 0).GetType().ToString());
                     for (int i = 0; i < blockHeight; i++)
                     {
                         for (int j = 0; j < blockWidth; j++)
                         {
                             double up = double.Parse(pixels4.GetValue(j, i).ToString()) - double.Parse(pixels3.GetValue(j, i).ToString());
                             double down = double.Parse(pixels4.GetValue(j, i).ToString()) + double.Parse(pixels3.GetValue(j, i).ToString());
                             if (down != 0)
                             {
                                 resultPixels.SetValue((up/down) , j, i);
                             }
                             else
                             {
                                 resultPixels.SetValue((0.0), j, i);
                             }
                         }
                     }
                     resultPixelBlock.set_PixelData(0, (System.Array)resultPixels);
                     resultRasterEdit.Write(resultRasterCursor.TopLeft, (IPixelBlock)resultPixelBlock);
                     resultRasterEdit.Refresh();

                } while (resultRasterCursor.Next() == true && rasterCursor.Next() == true);

                IRasterDataset pRasterDs = pRasterWs.OpenRasterDataset("NDVI.tif");
                IRaster praster = pRasterDs.CreateDefaultRaster();

                IRasterLayer resLayer = new RasterLayerClass();
                resLayer.CreateFromRaster(praster);
                //resLayer.SpatialReference = ((IGeoDataset)pRasterDs).SpatialReference;
                resLayer.Name = "NDVI";
                ////将此单波段图像用灰度显示，并按照最大最小值拉伸
                IRasterStretchColorRampRenderer grayStretch = null;
                if (resLayer.Renderer is IRasterStretchColorRampRenderer) grayStretch = resLayer.Renderer as IRasterStretchColorRampRenderer;
                else grayStretch = new RasterStretchColorRampRendererClass();
                IRasterStretch2 rstStr2 = grayStretch as IRasterStretch2;
                rstStr2.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;//设置拉伸模式为最大最小值拉伸
                resLayer.Renderer = grayStretch as IRasterRenderer;
                resLayer.Renderer.Update();

                //添加NDVI图层显示，并刷新视图
                axMapControl1.AddLayer(resLayer);
                axMapControl1.ActiveView.Extent = resLayer.AreaOfInterest;
                axMapControl1.ActiveView.Refresh();
                this.axTOCControl1.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //获取指定图层指定波段的栅格图像信息,返回一个二维数组
        private double[,] GetPixelValue(IRasterLayer pRasterLayer,int bandIndex)
        {
            IRaster2 raster2 = pRasterLayer.Raster as IRaster2;
            IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
            IRaster2 raster = rasterDataset.CreateFullRaster() as IRaster2;

            IRasterProps pRasterProps = raster as IRasterProps;
            int Height = pRasterProps.Height;
            int Width = pRasterProps.Width;
            //定义并初始化数组，用户存储栅格内所有像元像素值
            double[,] PixelValue = new double[Height, Width];
            System.Array pixels;
            double[] PixelArray = new double[256];

            IRasterCursor rasterCursor = raster.CreateCursorEx(null);
            long blockWidth = 0;
            long blockHeight = 0;
            IPixelBlock3 pixelBlock3 = null;
            do
            {
                pixelBlock3 = rasterCursor.PixelBlock as IPixelBlock3;
                int left = (int)rasterCursor.TopLeft.X;
                int top = (int)rasterCursor.TopLeft.Y;

                blockHeight = pixelBlock3.Height;
                blockWidth = pixelBlock3.Width;
                //MessageBox.Show(pixelBlock3.Planes.ToString());
                pixels = (System.Array)pixelBlock3.get_PixelData(bandIndex);
                for (int i = 0; i < blockHeight; i++)
                {
                    for (int j = 0; j < blockWidth; j++)
                    {
                        PixelValue[top + i, left + j] = Convert.ToDouble(pixels.GetValue(j, i));
                    }
                }
            } while (rasterCursor.Next() == true);

            return PixelValue;
        }

        private void Btn_ReadXML_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"D:\\RDB\\Raster_Function_Template.xml");

                XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");

                XmlNodeList xnl = xn.ChildNodes;

                foreach (XmlNode xn1 in xnl)
                {
                    if (xn1 is XmlComment)
                        continue;
                    //else if (xn1.
                    XmlElement xe = (XmlElement)xn1;
                    if (xe.Name == "Function")
                    {
                        if (xe.GetAttribute("name").Equals("Convolution"))
                        {                            
                            XmlNodeList xnl0 = xe.ChildNodes;
                            string inputRaster = null, outputRaster = null;
                            int type = -1;
                            bool save = false;
                            foreach (XmlNode xn2 in xnl0){
                                if (xn2 is XmlComment)
                                    continue;
                                switch (xn2.Name)
                                {
                                    case "Raster":
                                        inputRaster = xn2.InnerText;
                                        break;
                                    case  "Type":
                                        type = Convert.ToInt16(xn2.InnerText);
                                        break;
                                    case "Save":
                                        save = Convert.ToInt16(xn2.InnerText) == 0 ? false : true;
                                        break;
                                    case "Output":
                                        outputRaster = xnl0.Item(3).InnerText;
                                        break;                                  
                                }
                            }
                            ILayer layer = GetLayerByName(inputRaster);
                            ILayer outLayer = RasterFunction.Convolution(layer, type, outputRaster);
                            axMapControl1.AddLayer(outLayer);
                            axMapControl1.ActiveView.Refresh();
                            axTOCControl1.Update();

                            //更新combobox里面的选项（图层的和波段的）
                            iniCmbItems();                        
                        }
                        else if (xe.GetAttribute("name").Equals("Pansharpening"))
                        {
                            XmlNodeList xnl0 = xe.ChildNodes;
                            double red=0, green=0, blue=0, infra=0;
                            string panImage = null, MSImage = null, outputRaster = null;
                            int type = -1;
                            bool save;
                            foreach (XmlNode xn2 in xnl0)
                            {
                                if (xn2 is XmlComment)
                                    continue;
                                switch (xn2.Name)
                                {
                                    case "Red":
                                        red = Convert.ToDouble(xn2.InnerText);
                                        break;
                                    case "Green":
                                        green = Convert.ToDouble(xn2.InnerText);
                                        break;
                                    case "Blue":
                                        blue = Convert.ToDouble(xn2.InnerText); ;
                                        break;
                                    case "Infra":
                                        infra = Convert.ToDouble(xn2.InnerText);
                                        break;
                                    case "PanImage":
                                        panImage = xn2.InnerText;
                                        break;
                                    case "MSImage":
                                        MSImage = xn2.InnerText;
                                        break;
                                    case "Type":
                                        type = Convert.ToInt16(xn2.InnerText);
                                        break;
                                    case "Save":
                                        save = Convert.ToInt16(xn2.InnerText) == 0 ? false : true;
                                        break;
                                    case "Output":
                                        outputRaster = xnl0.Item(3).InnerText;
                                        break;
                                }
                            }
                            ILayer singleLayer = GetLayerByName(panImage);
                            ILayer multiLayer = GetLayerByName(MSImage);

                            ILayer panSharpenLayer = RasterFunction.Pansharpening(singleLayer, multiLayer, type, outputRaster);
                            //添加到控件中
                            axMapControl1.AddLayer(panSharpenLayer);
                            axMapControl1.ActiveView.Refresh();
                            axTOCControl1.Update();

                            //更新combobox里面的选项（图层和波段的）
                            iniCmbItems();                       
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            
        }

        private void convolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            convolutionFunction conv = new convolutionFunction(null);
            conv.ShowDialog();
        }

        private void pansharpingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panSharpenFunction pansharping = new panSharpenFunction(null);
            pansharping.ShowDialog();
        }

        private void btn_Filter_Bottom_Click(object sender, EventArgs e)
        {
            try
            {
                //获取选择的图层和波段，转换接口
                ILayer layer = GetLayerByName(cmb_NDVILayer.SelectedItem.ToString());
                IRasterLayer rstLayer = null;
                if (layer is IRasterLayer) rstLayer = layer as IRasterLayer;
                else
                {
                    MessageBox.Show("所选择的图层并非栅格图层，无法进行操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IRaster2 raster2 = rstLayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
                if (rstBandColl.Count > 1)
                {
                    MessageBox.Show("暂不支持多波段的滤波计算", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IRasterProps pRasterProps = raster2 as IRasterProps;
                int Height = pRasterProps.Height;
                int Width = pRasterProps.Width;
                double cellsizex = pRasterProps.MeanCellSize().X;

                double cellsizey = pRasterProps.MeanCellSize().Y;
                rstPixelType pixelType = pRasterProps.PixelType;
                ISpatialReference spatialReference = pRasterProps.SpatialReference;
                //MessageBox.Show(spatialReference.Name.ToString());
                IWorkspaceFactory pRasterWsFac = new RasterWorkspaceFactoryClass();
                IWorkspace pWs = pRasterWsFac.OpenFromFile(@"D://RDB", 0);
                IRasterWorkspace2 pRasterWs;
                pRasterWs = pWs as IRasterWorkspace2;
                IPoint origin = new PointClass();
                origin.PutCoords(pRasterProps.Extent.XMin, pRasterProps.Extent.YMin);
                //RasterWorkspace rasterWorkspace = (RasterWorkspace)workspace;
                ISpatialReference sr = new UnknownCoordinateSystemClass();
                IRasterDataset2 resultDataset = pRasterWs.CreateRasterDataset(cmb_FliterLayer.SelectedItem.ToString() + "_"+ cmb_FliterMethod.SelectedItem.ToString() +  ".tif", "TIFF", origin, Width, Height, cellsizex, cellsizey, 1, rstPixelType.PT_DOUBLE, sr) as IRasterDataset2;
                IRaster resultRaster = resultDataset.CreateFullRaster();
                IRasterCursor resultRasterCursor = ((IRaster2)resultRaster).CreateCursorEx(null);

                IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
                IRaster2 raster = rasterDataset.CreateFullRaster() as IRaster2;
                IRasterCursor rasterCursor = raster.CreateCursorEx(null);

                IPixelBlock3 resultPixelBlock = null;
                IPixelBlock3 tempPixelBlock = null;
                IRasterEdit resultRasterEdit = resultRaster as IRasterEdit;

                long blockWidth = 0;
                long blockHeight = 0;
                // System.Array pixels;
                double[,] kernal = new double[3, 3]{{0,0,0},
                                                        {0,1,0},
                                                        {0,0,0}};
                switch (cmb_FliterMethod.SelectedItem.ToString()){
                    case "LineDetectionHorizontal":
                        kernal = new double[3, 3]{{-1,-1,-1},
                                                        {2,2,2},
                                                        {-1,-1,-1}};
                        break;
                    case "LineDetectionVertical":
                        kernal = new double[3, 3]{{-1,2,-1},
                                                        {-1,2,-1},
                                                        {-1,2,-1}};
                        break;
                    case "Laplacian3x3":
                        kernal = new double[3, 3]{{0,-1,0},
                                                        {-1,4,-1},
                                                        {0,-1,0}};
                        break;
                    case "Smoothing3x3":
                        kernal = new double[3, 3]{{1,2,1},
                                                        {2,4,2},
                                                        {1,2,1}};
                        break;
                    case "Sharpening3x3":
                        kernal = new double[3, 3]{{-1,-1,-1},
                                                        {-1,9,-1},
                                                        {-1,-1,-1}};
                        break;
                }
                do
                {
                    resultPixelBlock = resultRasterCursor.PixelBlock as IPixelBlock3;
                    tempPixelBlock = rasterCursor.PixelBlock as IPixelBlock3;

                    System.Array pixels = (System.Array)tempPixelBlock.get_PixelData(0);
                    //MessageBox.Show(pixels3.GetValue(0, 0).GetType().ToString());
                    blockHeight = resultPixelBlock.Height;
                    blockWidth = resultPixelBlock.Width;
                    System.Array resultPixels = (System.Array)resultPixelBlock.get_PixelData(0);
                    //MessageBox.Show(resultPixels.GetValue(0, 0).GetType().ToString());
                    for (int i = 0; i < blockHeight; i++)
                    {
                        for (int j = 0; j < blockWidth; j++)
                        {
                            double sum = 0;
                            for (int ki = -1; ki <= 1; ki++)
                            {
                                for (int kj = -1; kj <= 1; kj++)
                                {
                                    long idxi = (i + ki) < 0?0:i + ki >= blockHeight?blockHeight-1:(i+ki);
                                    long idxj = (j + kj) < 0?0:j + kj >= blockWidth?blockWidth-1:(j+kj);
                                    double raw = double.Parse(pixels.GetValue(idxj, idxi).ToString());
                                    sum += raw * kernal[ki + 1, kj + 1];                                  
                                }
                            }
                            resultPixels.SetValue(sum, j, i);
                        }
                    }
                    resultPixelBlock.set_PixelData(0, (System.Array)resultPixels);
                    resultRasterEdit.Write(resultRasterCursor.TopLeft, (IPixelBlock)resultPixelBlock);
                    resultRasterEdit.Refresh();
                } while (resultRasterCursor.Next() == true && rasterCursor.Next() == true);
                IRasterDataset pRasterDs = pRasterWs.OpenRasterDataset(cmb_FliterLayer.SelectedItem.ToString() + "_" + cmb_FliterMethod.SelectedItem.ToString() + ".tif");
                IRaster praster = pRasterDs.CreateDefaultRaster();

                IRasterLayer resLayer = new RasterLayerClass();
                resLayer.CreateFromRaster(praster);
                resLayer.Name = cmb_FliterLayer.SelectedItem.ToString() + "_" + cmb_FliterMethod.SelectedItem.ToString();

                //添加图层显示，并刷新视图
                axMapControl1.AddLayer(resLayer);
                axMapControl1.ActiveView.Extent = resLayer.AreaOfInterest;
                axMapControl1.ActiveView.Refresh();
                this.axTOCControl1.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }      
    }
}