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
    public partial class ConvolutionFunc : Form
    {
        public string name;
        public string description;     
        
        public ConvolutionFunc(IMap map)
        {
            InitializeComponent();
            this.name = "Convolution";
            this.description = "Performs filtering on the pixel values in a raster, which can be used for sharpening an image, blurring an image, detecting edges within an image, or other kernel-based enhancements.";

            ILayer layer;
            int count = map.LayerCount;
            if (count > 0)
            {
                //遍历地图的所有图层，获取图层的名字加入下拉框
                for (int i = 0; i < count; i++)
                {
                    layer = map.get_Layer(i);
                    cmb_FliterLayer.Items.Add(layer.Name);
                }
            }
            if (cmb_FliterLayer.Items.Count > 0) cmb_FliterLayer.SelectedIndex = 0;

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
            XmlDocument doc = new XmlDocument();
            doc.Load(@"D:\\RDB\\Raster_Function_Template.xml");
            XmlNode root = doc.SelectSingleNode("XmlRasterFunctionTemplate");

            XmlElement xelKey = doc.CreateElement("Function");
            XmlAttribute xelName = doc.CreateAttribute("name");
            xelName.InnerText = name;
            xelKey.SetAttributeNode(xelName);

            XmlAttribute xelDesp = doc.CreateAttribute("description");
            xelDesp.InnerText = description;
            xelKey.SetAttributeNode(xelDesp);
            
            XmlElement xelRaster = doc.CreateElement("Raster");
            xelRaster.InnerText = cmb_FliterLayer.SelectedItem.ToString();
            xelKey.AppendChild(xelRaster);

            XmlElement xelType = doc.CreateElement("Type");
            xelType.InnerText = cmb_FliterMethod.SelectedIndex.ToString();
            xelKey.AppendChild(xelType);

            XmlElement xelSave = doc.CreateElement("Save");
            xelSave.InnerText = "1";
            xelKey.AppendChild(xelSave);

            XmlElement xelOutput = doc.CreateElement("Output");
            xelOutput.InnerText = cmb_FliterLayer.SelectedItem.ToString()+"_conv";
            xelKey.AppendChild(xelOutput);

            root.AppendChild(xelKey);
            doc.Save(@"D:\\RDB\\Raster_Function_Template.xml");
            this.Close();
        }

    }
}
