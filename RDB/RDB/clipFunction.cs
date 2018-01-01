using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private string m_input_shppath="D:";

       
        public clipFunction()
        {
            InitializeComponent();
            XmlDocument Doc = new XmlDocument();
            XmlDeclaration dec = Doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            Doc.AppendChild(dec);

            XmlElement xmlnode1= Doc.CreateElement("Function");
            Doc.AppendChild(xmlnode1);
            xmlnode1.SetAttribute("name", "clipFunction");
            xmlnode1.SetAttribute("description", "A raster clip function.");
           xmlnode1.AppendChild(Doc.CreateElement("inputSHPPath",m_input_shppath));
            

            //新建m_xmlnode
            m_xmlnode = xmlnode1;

        }

  
      
     
        //接收栅格函数链中传递过来的参数
        public clipFunction(XmlNode xmlnode)
        {
            InitializeComponent();
            this.m_xmlnode = xmlnode;

        }

        public clipFunction(IRaster raster, XmlNode xmlnode)
        {
            InitializeComponent();
            this.m_raster = raster;
            this.m_xmlnode = xmlnode;
  
        }

        public XmlNode GetXMLNode()
        {
            return this.m_xmlnode;
        }

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
                    if (xnl1 is XmlComment)
                        continue;
            XmlElement xe = (XmlElement)xnl1;
            if (xe.Name == "inputSHPPath")
                    {
                        xe.InnerText = txb_inputfeatherlayer.Text;
                    }
                 }
          this.Close();
        }

     

        //执行山体阴影操作
        public void init()
        {
        //遍历子节点读取参数
            
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

                //将矢量数据中的每一个IFeature几何形状添加到clipGeometry
                for (int i = 0; i < featureClass.FeatureCount(null); i++)
                {
                    feature = featureClass.GetFeature(i);
                    clipGeometry = feature.Shape;
                    clipRaster.Add(clipGeometry);
                }
                //将这个过滤器作用于栅格图像
                IPixelOperation pixelOp = (IPixelOperation)m_raster;
                pixelOp.PixelFilter = (IPixelFilter)clipRaster;

                //如果输入的栅格中并不包含NoData和曾经使用过的最大像素深度，则输出文件的像素深度和NoData赋值
                IRasterProps rasterProps = (IRasterProps)m_raster;
                rasterProps.NoDataValue = 0;
                rasterProps.PixelType = rstPixelType.PT_USHORT;
                //存储剪裁结果栅格图像
                IWorkspace rstWs = wsf.OpenFromFile(@"F:\RDB", 0);
                //保存输出
                ISaveAs saveas = (ISaveAs)m_raster;
                saveas.SaveAs("clip_result.tif", rstWs, "TIFF");

            }
            catch (System.Exception ex)//捕获异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void txb_inputfeatherlayer_MouseDown(object sender, MouseEventArgs e)
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
                    txb_inputfeatherlayer .Text=fileName;
                    m_input_shppath =fileName ;
                }
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

      

    }
}



