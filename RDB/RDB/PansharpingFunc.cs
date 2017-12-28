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
    public partial class PansharpingFunc : Form
    {
        public string name;
        public string description;

        public PansharpingFunc(IMap map)
        {
            InitializeComponent();
            this.name = "Pansharpening";
            this.description = "Enhances the spatial resolution of a multiband image by fusing it with a higher-resolution panchromatic image.";

            ILayer layer;
            int count = map.LayerCount;
            if (count > 0)
            {
                //遍历地图的所有图层，获取图层的名字加入下拉框
                for (int i = 0; i < count; i++)
                {
                    layer = map.get_Layer(i);
                    cmb_PanSharpenSigleLayer.Items.Add(layer.Name);
                    cmb_PanSharpenMultiLayer.Items.Add(layer.Name);
                }
            }
            if (cmb_PanSharpenSigleLayer.Items.Count > 0) cmb_PanSharpenSigleLayer.SelectedIndex = 0;
            if (cmb_PanSharpenMultiLayer.Items.Count > 0) cmb_PanSharpenMultiLayer.SelectedIndex = 0;

            //添加滤波方法
            cmb_PanMethod.Items.Add("Brovey");
            cmb_PanMethod.Items.Add("Esri");
            cmb_PanMethod.Items.Add("Gram-schmidt");
            cmb_PanMethod.Items.Add("IHS");
            cmb_PanMethod.Items.Add("Simple Mean");
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

            XmlElement xelPanImage = doc.CreateElement("PanImage");
            xelPanImage.InnerText = cmb_PanSharpenSigleLayer.SelectedItem.ToString();
            xelKey.AppendChild(xelPanImage);

            XmlElement xelMSImage = doc.CreateElement("MSImage");
            xelMSImage.InnerText = cmb_PanSharpenMultiLayer.SelectedItem.ToString();
            xelKey.AppendChild(xelMSImage);

            XmlElement xelType = doc.CreateElement("PansharpeningType");
            xelType.InnerText = cmb_PanMethod.SelectedIndex.ToString();
            xelKey.AppendChild(xelType);

            XmlElement xelRed = doc.CreateElement("Red");
            xelRed.InnerText =txb_red.Text;
            xelKey.AppendChild(xelRed);

            XmlElement xelGreen = doc.CreateElement("Green");
            xelGreen.InnerText = txb_green.Text;
            xelKey.AppendChild(xelGreen);

            XmlElement xelBlue = doc.CreateElement("Blue");
            xelBlue.InnerText = txb_blue.Text;
            xelKey.AppendChild(xelBlue);

            XmlElement xelIR = doc.CreateElement("Infra");
            xelIR.InnerText = txb_IR.Text;
            xelKey.AppendChild(xelIR);

            XmlElement xelSave = doc.CreateElement("Save");
            xelSave.InnerText = "1";
            xelKey.AppendChild(xelSave);

            XmlElement xelOutput = doc.CreateElement("Output");
            xelOutput.InnerText = cmb_PanSharpenMultiLayer.SelectedItem.ToString() + "_Result";
            xelKey.AppendChild(xelOutput);

            root.AppendChild(xelKey);
            doc.Save(@"D:\\RDB\\Raster_Function_Template.xml");
            this.Close();
        }


    }
}
