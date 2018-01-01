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
        double red = 0, green = 0, blue = 0, infra = 0;
        string panImage = null, MSImage = null, outputRaster = null;
        int type = -1;
        
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
                    case "PansharpeningType":
                        type = Convert.ToInt16(xn2.InnerText);
                        break;
                    case "Output":
                        outputRaster = xnl0.Item(3).InnerText;
                        break;
                }
            }
            cmb_PanSharpenMultiLayer.Items.Add("test");
            if (cmb_PanSharpenMultiLayer.Items.Count > 0) cmb_PanSharpenMultiLayer.SelectedIndex = 0;

            //添加滤波方法
            cmb_PanMethod.Items.Add("Brovey");
            cmb_PanMethod.Items.Add("Esri");
            cmb_PanMethod.Items.Add("Gram-schmidt");
            cmb_PanMethod.Items.Add("IHS");
            cmb_PanMethod.Items.Add("Simple Mean");

            txb_red.Text = red.ToString();
            txb_green.Text = green.ToString();
            txb_blue.Text = blue.ToString();
            txb_IR.Text = infra.ToString();
            txb_single.Text = panImage;
            cmb_PanMethod.SelectedIndex = type;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlElement xe = (XmlElement)m_xmlNode;
            xe.SetAttribute("name", "Pansharpening");
            xe.SetAttribute("description", "Enhances the spatial resolution of a multiband image by fusing it with a higher-resolution panchromatic image.");
            xe.GetElementsByTagName("PanImage").Item(0).InnerText = txb_single.Text;
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

        public void Init()
        {
            try
            {
                //根据选择的矢量文件的路径打开工作空间
                string fileN = panImage;
                FileInfo fileInfo = new FileInfo(fileN);
                string filePath = fileInfo.DirectoryName;
                string fileName = fileInfo.Name;

                IWorkspaceFactory wsf = new RasterWorkspaceFactory();
                IWorkspace wp = wsf.OpenFromFile(filePath, 0);
                IRasterWorkspace rw = (IRasterWorkspace)wp;
                IRasterDataset panDataset = rw.OpenRasterDataset(fileName);

                IRaster2 multiRaster2 = m_raster as IRaster2;                
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
                pansharpenFilter.PutWeights(red,green,blue,infra);

                //将全色锐化过滤器设置于多光谱栅格对象上
                IPixelOperation pixeOperation = (IPixelOperation)multiRaster;
                pixeOperation.PixelFilter = (IPixelFilter)pansharpenFilter;

                //保存结果数据集，并加载显示
                //加载显示裁剪结果图像
                /*IRasterLayer panSharpenLayer = new RasterLayerClass();
                panSharpenLayer.CreateFromRaster(multiRaster);
                panSharpenLayer.Name = "panSharpen_Result";
                panSharpenLayer.SpatialReference = ((IGeoDataset)multiRaster).SpatialReference;
                return panSharpenLayer;*/
                m_raster= multiRaster;
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txb_single_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //打开文件选择对话框，设置对话框属性
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Imag file (*.img)|*img|Tiff file(*.tif)|*.tif|Openflight file (*.flt)|*.flt";
                openFileDialog.Title = "打开影像数据";
                openFileDialog.Multiselect = false;
                string fileName = "";
                //如果对话框已成功选择文件，将文件路径信息填写到输入框里
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    txb_single.Text = fileName;
                }
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
