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
    public partial class panSharpenFunction : Form
    {
        XmlNode m_xmlNode;
        IRaster m_raster;
        
        public panSharpenFunction(XmlNode node)
        {
            InitializeComponent();
            m_xmlNode = node;
            initial();
        }

        public panSharpenFunction(IRaster raster, XmlNode node)
        {
            InitializeComponent();
            m_xmlNode = node;
            m_raster = raster;
            initial();
        }

        private void initial()
        {           
            XmlNodeList xnl0 = m_xmlNode.ChildNodes;
            double red=0, green=0, blue=0, infra=0;
            string panImage = null, MSImage = null, outputRaster = null;
            int type = -1;
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
                    case "Output":
                        outputRaster = xnl0.Item(3).InnerText;
                        break;
                }
            }
            cmb_PanSharpenMultiLayer.Items.Add(MSImage);
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
            XmlElement xe = (XmlElement)m_xmlNode;
            xe.SetAttribute("name", "Pansharpening Function");
            xe.SetAttribute("description", "Enhances the spatial resolution of a multiband image by fusing it with a higher-resolution panchromatic image.");
            xe.GetElementsByTagName("PanImage").Item(0).InnerText = cmb_PanSharpenSigleLayer.SelectedItem.ToString();
            xe.GetElementsByTagName("MSImage").Item(0).InnerText = cmb_PanSharpenMultiLayer.SelectedItem.ToString();
            xe.GetElementsByTagName("PansharpeningType").Item(0).InnerText = cmb_PanMethod.SelectedIndex.ToString();
            xe.GetElementsByTagName("Red").Item(0).InnerText = txb_red.Text;
            xe.GetElementsByTagName("Green").Item(0).InnerText = txb_green.Text;
            xe.GetElementsByTagName("Blue").Item(0).InnerText = txb_blue.Text;
            xe.GetElementsByTagName("Infra").Item(0).InnerText = txb_IR.Text;
            xe.GetElementsByTagName("Output").Item(0).InnerText = cmb_PanSharpenMultiLayer.SelectedItem.ToString() + "_Result";
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
    }
}
