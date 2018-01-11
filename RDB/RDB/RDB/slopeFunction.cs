using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RDB
{
    public partial class slopeFunction : Form
    {
        XmlNode m_xmlNode;
        IRaster m_raster;
        public bool isFinished = false;

        public slopeFunction()
        {
            InitializeComponent();
        }
        public slopeFunction(XmlNode node)
        {
            InitializeComponent();
            m_xmlNode = node;
            XmlNode Zfactor = m_xmlNode.SelectSingleNode("Zfactor");
            txb_SlopeZfactor.Text = Zfactor.InnerText;
        }

        public slopeFunction( IRaster raster,XmlNode node)
        {
            InitializeComponent();
            m_xmlNode = node;
            m_raster = raster;
            XmlNode Zfactor = m_xmlNode.SelectSingleNode("Zfactor");
            txb_SlopeZfactor.Text = Zfactor.InnerText;
        }
        private void btn_SlopeOK_Click(object sender, EventArgs e)
        {
            if (txb_SlopeZfactor.Text != null) 
            {
                //XmlDocument xmlDoc = new XmlDocument();
                //XmlElement xmlEleSlope = xmlDoc.CreateElement("Function");
                //xmlEleSlope.SetAttribute("name", "Slope");
                //xmlEleSlope.SetAttribute("description", "Slope represents the rate of change of elevation for each digital elevation model (DEM) cell. It's the first derivative of a DEM.");
                //XmlElement xmlEleZfactor = xmlDoc.CreateElement("Zfactor");
                //xmlEleSlope.AppendChild(xmlEleZfactor);

                XmlNode Zfactor = m_xmlNode.SelectSingleNode("Zfactor");
                Zfactor.InnerText = txb_SlopeZfactor.Text;
                isFinished = true;
                this.Close();
            }
            else
            {
                //MessageBox message = new MessageBox("please input the Zfactor");
                MessageBox.Show("please input the Zfactor");
                return;
            }
        }

        public void Init()
        {
           
            XmlNode Zfactor = m_xmlNode.SelectSingleNode("Zfactor");
            double Zfactorvalue;
            if (Zfactor != null)
            {
                Zfactorvalue = double.Parse(Zfactor.InnerText);
                IRaster2 raster2 = m_raster as IRaster2;
                ISlopeFunctionArguments slopeFunctionArugments = (ISlopeFunctionArguments)new SlopeFunctionArguments();
                slopeFunctionArugments.DEM = raster2;
                slopeFunctionArugments.ZFactor = Zfactorvalue;
                IRasterFunction rasterFunction = new SlopeFunction();
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                functionRasterDatasetName.FullName = @"D:\RDB";
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                functionRasterDataset.Init(rasterFunction, slopeFunctionArugments);

                IRasterDataset rstDataset = functionRasterDataset as IRasterDataset;
                m_raster = rstDataset.CreateDefaultRaster();
            }
            else 
            {
                MessageBox.Show(" no Zfactor","ERROR");
          
            }
        }

        public XmlNode GetXMLNode() 
        {
            return m_xmlNode;
        }

        public IRaster GetRaster() 
        {
            return m_raster;
        }

        private void slopeFunction_Load(object sender, EventArgs e)
        {

        }
    }
}
