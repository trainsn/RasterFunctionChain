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

namespace RDB
{
    public partial class hillshadeFunction : Form
    {   
        private IRaster m_raster;
        private XmlNode m_xmlnode;
        private double  m_azimuth=50;
        private double m_zfactor=1/11111.0;
        public bool isFinished = false; 
        public hillshadeFunction()
        {
            InitializeComponent();
        }
     
        //接收栅格函数链中传递过来的参数
        public hillshadeFunction(XmlNode xmlnode)
        {
            InitializeComponent();
            this.m_xmlnode = xmlnode;
            foreach (XmlNode xnl1 in m_xmlnode.ChildNodes)
            {
                if (xnl1 is XmlComment)
                    continue;

                XmlElement xe = (XmlElement)xnl1;
                if (xe.Name == "Azimuth")
                {
                    txb_inputazimuth.Text =xe.InnerText;
                 
                }
                if (xe.Name == "ZFactor")
                {
                
                    txb_inputzfactor.Text = xe.InnerText;
                   
                }
            }


        }

        public hillshadeFunction(IRaster raster, XmlNode xmlnode)
        {
            InitializeComponent();
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
        private void btn_inputclick_Click(object sender, EventArgs e)
        {
            m_azimuth = double.Parse(txb_inputazimuth.Text);
            m_zfactor = double.Parse(txb_inputzfactor.Text);


          
            //遍历子节点寻找并修改成新输入的参数
            foreach (XmlNode xnl1 in  m_xmlnode.ChildNodes)
                {
                    if (xnl1 is XmlComment)
                        continue;

                XmlElement xe = (XmlElement)xnl1;
                //找到Azimuth节点，修改其参数
                    if (xe.Name == "Azimuth")
                    {
                        xe.InnerText = m_azimuth.ToString();
                    }
                    //找到ZFactor节点，修改其参数
                    if (xe.Name == "ZFactor")
                    {
                        xe.InnerText = m_zfactor.ToString();
                    }
                    }

            isFinished = true;
            this.Close();
        }

        //执行山体阴影操作
        public void Init()
        {
            //遍历子节点读取参数 
            foreach (XmlNode xnl1 in m_xmlnode.ChildNodes)
            {
                if (xnl1 is XmlComment)
                    continue;

                XmlElement xe = (XmlElement)xnl1;
                //找到Azimuth节点，读取其参数
                if (xe.Name == "Azimuth")
                {
                   m_azimuth= double.Parse(xe.InnerText);
                }
                //找到ZFactor节点，读取其参数
                if (xe.Name == "ZFactor")
                {
                    m_zfactor = double.Parse(xe.InnerText);
                }
            }
            //山体阴影的实现
            try
            {
                //将m_raster转为IRaster接口实行栅格相关操作
                IRaster2 raster2 = m_raster as IRaster2;
                //使用IHillshadeFunctionArguments接口进行山体阴影操作
                IHillshadeFunctionArguments hillshadeFunctionArugments = (IHillshadeFunctionArguments)new HillshadeFunctionArguments();
                //设置生成山体阴影所需要的参数Azimuth和ZFactor的值
                hillshadeFunctionArugments.Azimuth = m_azimuth;
                hillshadeFunctionArugments.ZFactor = m_zfactor;
                //设置数据源为该raster
                hillshadeFunctionArugments.DEM = raster2;
                //创建一个IRasterFunction
                IRasterFunction rasterFunction = new HillshadeFunction();
                //创建IFunctionRasterDataset以调用Init方法执行函数
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                //设置IFunctionRasterDataset的相关参数
                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                functionRasterDatasetName.FullName = @"D:\RDB";
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                //调用Init执行
                functionRasterDataset.Init(rasterFunction, hillshadeFunctionArugments);
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

    }
}
