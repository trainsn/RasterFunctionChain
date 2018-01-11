using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

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
using System.Xml;
using System.IO;

namespace RDB
{
    public partial class clipFunction : Form
    {  
        
        private IRaster m_raster;
        private XmlNode m_xmlnode;
        private string m_input_shppath;
        public bool isFinished = false;

       
        public clipFunction()
        {
            InitializeComponent(); 
        }

        //具有参数XmlNode的构造函数
        public clipFunction(XmlNode xmlnode)
        {
            InitializeComponent();
            //接收传入的XmlNode
            this.m_xmlnode = xmlnode;
            //遍历子节点寻找名字为"inputSHPPath"的节点，并在textbox中显示该参数值
            foreach (XmlNode xnl1 in m_xmlnode.ChildNodes)
            {
                //跳过
                if (xnl1 is XmlComment)
                    continue;
               //找到该节点
                XmlElement xe = (XmlElement)xnl1;
                if (xe.Name == "inputSHPPath")
                { 
                    //在textbox中显示该参数值
                    txb_inputfeatherlayer.Text = xnl1.InnerText;
                }
               
            }

        }
        //具有参数IRaster和XmlNode的构造函数
        public clipFunction(IRaster raster, XmlNode xmlnode)
        {
            InitializeComponent();
            //接收传入的IRaster和XmlNode
            this.m_raster = raster;
            this.m_xmlnode = xmlnode;
  
        }
        //主窗体调用该函数得到XmlNode
        public XmlNode GetXMLNode()
        {
            return this.m_xmlnode;
        }
        //主窗体调用该函数得到IRaster
        public IRaster GetRaster()
        {
            return this.m_raster;
        }

         //设置并保存输入参数，更新XMLNode
        private void btn_inputclick2_Click(object sender, EventArgs e)
        {
            
            //遍历子节点寻找并修改成新输入的参数
            foreach (XmlNode xnl1 in  m_xmlnode.ChildNodes)
                {
                    //跳过
                    if (xnl1 is XmlComment)
                        continue;

                    XmlElement xe = (XmlElement)xnl1;
                    if (xe.Name  == "inputSHPPath")
                    {
                        //修改节点属性
                        xe.InnerText = txb_inputfeatherlayer.Text;
                    }
                 }
            //判断用户已经做出完成修改操作，将该布尔值置真
            isFinished = true;
            this.Close();
          
        }


        //clip操作的执行函数（底层）
        public void UnderInit()
        {
            //遍历子节点，找到该对应子节点，读取相应路径参数
            //该路径是作为Clip Features的文件路径
            foreach (XmlNode xnl1 in m_xmlnode.ChildNodes)
            {
                if (xnl1 is XmlComment)
                    continue;
                XmlElement xe = (XmlElement)xnl1;
                if (xe.Name == "inputSHPPath")
                {
                   m_input_shppath= xe.InnerText;
                }
              
            }
            //裁剪的实现
            try
            {

                //获取矢量文件的路径和文件名字
                string fileN = m_input_shppath;
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
                //将传入的栅格转化为IRaster2接口
                IRaster2 raster2 = m_raster as IRaster2;
                //获取IRasterDataset类型数据集
                IRasterDataset rstDataset = raster2.RasterDataset;
                //调用CreateDefaultRaster方法产生可用于裁剪操作的栅格
                IRaster raster = rstDataset.CreateDefaultRaster();

                //将矢量数据中的每一个IFeature几何形状添加到clipGeometry
                for (int i = 0; i < featureClass.FeatureCount(null); i++)
                {
                    feature = featureClass.GetFeature(i);
                    clipGeometry = feature.Shape;
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
                // IWorkspace rstWs = wsf.OpenFromFile(@"D:\RDB", 0);
                m_raster = raster;
                //保存输出
                //ISaveAs saveas = (ISaveAs)raster;
                //saveas.SaveAs("clip_result.tif", rstWs, "TIFF");

            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
        //clip操作的执行函数（IRasterFunction）
        public void Init()
        {
            //遍历子节点，找到该对应子节点，读取相应路径参数
            //该路径是作为Clip Features的文件路径
            foreach (XmlNode xnl1 in m_xmlnode.ChildNodes)
            {
                if (xnl1 is XmlComment)
                    continue;
                XmlElement xe = (XmlElement)xnl1;
                if (xe.Name == "inputSHPPath")
                {
                    m_input_shppath = xe.InnerText;
                }

            }
            //裁剪的实现
            try
            {
                //获取矢量文件的路径和文件名字
                string fileN = m_input_shppath;
                FileInfo fileInfo = new FileInfo(fileN);
                string filePath = fileInfo.DirectoryName;
                string fileName = fileInfo.Name;

                //根据选择的矢量文件的路径打开工作空间
                IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
                IWorkspace wp = wsf.OpenFromFile(filePath, 0);
                IFeatureWorkspace fw = (IFeatureWorkspace)wp;
                IFeatureClass featureClass = fw.OpenFeatureClass(fileName);
                //单个geometry的临时存储
                IGeometry clipGeometry;
                IFeature feature;

                //将传入的栅格转化为IRaster2接口
                IRaster2 raster2 = m_raster as IRaster2;
                //获取IRasterDataset类型数据集
                IRasterDataset rstDataset = raster2.RasterDataset;
                //调用CreateDefaultRaster方法产生可用于裁剪操作的栅格数据
                IRaster raster = rstDataset.CreateDefaultRaster();

                //对于具有多个feature的shp需要IGeometryCollection进行合并存储
                 IGeoDataset geoDataset = featureClass as IGeoDataset;
                  IGeometry geometryBag = new GeometryBagClass();
                 geometryBag.SpatialReference = geoDataset.SpatialReference;
                IGeometryCollection geometryCollection = geometryBag as IGeometryCollection;
                //将矢量数据中的每一个IFeature几何形状添加到geometryCollection中
                for (int i = 0; i < featureClass.FeatureCount(null); i++)
               {
                    feature = featureClass.GetFeature(i);
                    clipGeometry = feature.Shape;
                    geometryCollection.AddGeometry(clipGeometry);
                   
               }
                //将geometryBag中的所有geometry合并为一个geometry
                 ITopologicalOperator unionedPolygon = new PolygonClass();
                unionedPolygon.ConstructUnion(geometryBag as IEnumGeometry);
                //最后得到一个用于裁剪的geometry
                IPolygon uniPoly = (IPolygon)unionedPolygon;

                // 创建一个IClipFunctionArguments
                IClipFunctionArguments clipFunctionArguments = (IClipFunctionArguments)new ClipFunctionArguments();
                //设置输入数据
                clipFunctionArguments.Raster = m_raster;
                //设置函数参数
                //设置裁剪类型：clip outside 保留内部
                clipFunctionArguments.ClippingType = esriRasterClippingType.esriRasterClippingOutside;
                clipFunctionArguments.ClippingGeometry = (IGeometry)uniPoly;
                
                //创建一个IRasterFunction
                IRasterFunction rasterFunction = new ClipFunction();
                //创建IFunctionRasterDataset以调用Init方法执行函数
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                //设置IFunctionRasterDataset的相关参数
                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                functionRasterDatasetName.FullName = @"D:\RDB";
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                //调用Init执行
                functionRasterDataset.Init(rasterFunction, clipFunctionArguments);
                //所得结果转为IRasterDataset
                IRasterDataset rasData = functionRasterDataset as IRasterDataset;
               
                //修改m_raster
                m_raster = rasData.CreateDefaultRaster();
                

            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //为textbox的点击设置响应函数，点击该框弹出选择文件对话框以输入Cilp Features
        private void txb_inputfeatherlayer_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //打开文件选择对话框，设置对话框属性
                OpenFileDialog openFileDialog = new OpenFileDialog();
                //只显示后缀名为shp的文件，防止后续程序运行出错
                openFileDialog.Filter = "Shp file (*.shp)|*.shp";
                openFileDialog.Title="Choose Clip Features File";
                openFileDialog.Multiselect=false;
                string fileName="";
                //如果对话框已成功选择文件，将文件路径信息填写到输入框里
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    txb_inputfeatherlayer .Text=fileName;
                    m_input_shppath =fileName ;
                }
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        //点击Apply按钮，窗口关闭
        //private void btn_applyclick_Click(object sender, EventArgs e)
        //{
          //  this.Close();
       // }

      

    }
}



