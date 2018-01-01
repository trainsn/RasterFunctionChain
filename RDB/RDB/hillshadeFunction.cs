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
       
        public hillshadeFunction()
        {
            InitializeComponent();
            XmlDocument Doc = new XmlDocument();
            XmlDeclaration dec = Doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            Doc.AppendChild(dec);

            XmlElement xmlnode1 = Doc.CreateElement("Function");
            Doc.AppendChild(xmlnode1);
            xmlnode1.SetAttribute("name", "hillshadeFunction");
            xmlnode1.SetAttribute("description", "A raster hillshade function.");

            xmlnode1.AppendChild(Doc.CreateElement("Azimuth", m_azimuth.ToString()));
            xmlnode1.AppendChild(Doc.CreateElement("ZFactor", m_zfactor.ToString()));

            //新建m_xmlnode
            m_xmlnode = xmlnode1;
           
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

        public XmlNode GetXMLNode()
        {
            return this.m_xmlnode;
        }

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
                    if (xe.Name == "Azimuth")
                    {
                        xe.InnerText = m_azimuth.ToString();
                    }
                    if (xe.Name == "ZFactor")
                    {
                        xe.InnerText = m_zfactor.ToString();
                    }
                    }


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
                if (xe.Name == "Azimuth")
                {
                   m_azimuth= double.Parse(xe.InnerText);
                }
                if (xe.Name == "ZFactor")
                {
                    m_zfactor = double.Parse(xe.InnerText);
                }
            }
            //山体阴影的实现
            try
            {

                IRaster2 raster2 = m_raster as IRaster2;
                IHillshadeFunctionArguments hillshadeFunctionArugments = (IHillshadeFunctionArguments)new HillshadeFunctionArguments();
                hillshadeFunctionArugments.Azimuth = m_azimuth;
                hillshadeFunctionArugments.ZFactor = m_zfactor;
                hillshadeFunctionArugments.DEM = raster2;

                IRasterFunction rasterFunction = new HillshadeFunction();
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                //随机生成文件名
                //Random ran = new Random();
                //int rannum = ran.Next(1000);
                functionRasterDatasetName.FullName = @"D:\RDB";
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                functionRasterDataset.Init(rasterFunction, hillshadeFunctionArugments);

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
