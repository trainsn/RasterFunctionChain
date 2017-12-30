using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class convolutionFunction : Form
    {
        XmlNode m_xmlNode;
        IRaster m_raster;
        string inputRaster = null, outputRaster = null;
        int type = -1;
        
        public convolutionFunction(XmlNode node)
        {
            InitializeComponent();
            m_xmlNode = node;
            initial();
        }

        public convolutionFunction(IRaster raster, XmlNode node)
        {
            InitializeComponent();
            m_xmlNode = node;
            m_raster = raster;
            initial();
        }

        private void initial(){
            XmlNodeList xnl0 = m_xmlNode.ChildNodes;            
            foreach (XmlNode xn2 in xnl0)
            {
                if (xn2 is XmlComment)
                    continue;
                switch (xn2.Name)
                {
                    case "Raster":
                        inputRaster = xn2.InnerText;
                        break;
                    case "Type":
                        type = Convert.ToInt16(xn2.InnerText);
                        break;
                    case "Output":
                        outputRaster =xn2.InnerText;
                        break;
                }
            }

           cmb_FilterLayer.Items.Add("test");
            if (cmb_FilterLayer.Items.Count > 0) cmb_FilterLayer.SelectedIndex = 0;

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlElement xe = (XmlElement)m_xmlNode;
            xe.SetAttribute("name", "Convolution Function");
            xe.SetAttribute("description", "Performs filtering on the pixel values in a raster, which can be used for sharpening an image, blurring an image, detecting edges within an image, or other kernel-based enhancements.");
            xe.GetElementsByTagName("Type").Item(0).InnerText = cmb_FliterMethod.SelectedIndex.ToString();
            xe.GetElementsByTagName("Output").Item(0).InnerText = "_conv";
            m_xmlNode = (XmlNode)xe;
            this.Close();
        }

        public XmlNode GetXMLNode()
        {
            return m_xmlNode;
        }

        public IRaster GetRaster()
        {
            return m_raster;
        }

        public IRaster Init()         
        {
            try
            {
                IConvolutionFunctionArguments rasterFunctionArguments = (IConvolutionFunctionArguments)new ConvolutionFunctionArguments();

                //设置输入栅格数据
                rasterFunctionArguments.Raster = m_raster;
                rasterFunctionArguments.Type = (esriRasterFilterTypeEnum)type;
                //创建Raster Function对象
                IRasterFunction rasterFunction = new ConvolutionFunction();
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                functionRasterDatasetName.FullName = @"D:\\RDB" + "\\" + outputRaster;
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                functionRasterDataset.Init(rasterFunction, rasterFunctionArguments);

                IRasterDataset rasData = functionRasterDataset as IRasterDataset;
                IRasterLayer pRstLayer = new RasterLayerClass();
                pRstLayer.CreateFromDataset(rasData);

                IRaster iRaster = pRstLayer.Raster;
                return iRaster;
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
