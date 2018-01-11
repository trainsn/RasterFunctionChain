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
        int type = 0;
        public bool IsFinished = false;
        
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
            cmb_FliterMethod.SelectedIndex = type;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlElement xe = (XmlElement)m_xmlNode;
            xe.SetAttribute("name", "Convolution");
            xe.SetAttribute("description", "Performs filtering on the pixel values in a raster, which can be used for sharpening an image, blurring an image, detecting edges within an image, or other kernel-based enhancements.");
            xe.GetElementsByTagName("Type").Item(0).InnerText = cmb_FliterMethod.SelectedIndex.ToString();
            xe.GetElementsByTagName("Output").Item(0).InnerText = "_conv";
            m_xmlNode = (XmlNode)xe;
            IsFinished = true;

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
                m_raster = ((IRasterDataset2)rasData).CreateFullRaster();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void underInit() {
            IRaster2 raster2 = m_raster as IRaster2;
            IRasterDataset rstDataset = raster2.RasterDataset;
            IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
            if (rstBandColl.Count > 1)
            {
                MessageBox.Show("暂不支持多波段的滤波计算", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IRasterProps pRasterProps = raster2 as IRasterProps;
            int Height = pRasterProps.Height;
            int Width = pRasterProps.Width;
            double cellsizex = pRasterProps.MeanCellSize().X;

            double cellsizey = pRasterProps.MeanCellSize().Y;
            rstPixelType pixelType = pRasterProps.PixelType;
            ISpatialReference spatialReference = pRasterProps.SpatialReference;
            //MessageBox.Show(spatialReference.Name.ToString());
            IWorkspaceFactory pRasterWsFac = new RasterWorkspaceFactoryClass();
            IWorkspace pWs = pRasterWsFac.OpenFromFile(@"D://RDB", 0);
            IRasterWorkspace2 pRasterWs;
            pRasterWs = pWs as IRasterWorkspace2;
            IPoint origin = new PointClass();
            origin.PutCoords(pRasterProps.Extent.XMin, pRasterProps.Extent.YMin);
            //RasterWorkspace rasterWorkspace = (RasterWorkspace)workspace;
            ISpatialReference sr = new UnknownCoordinateSystemClass();
            IRasterDataset2 resultDataset = pRasterWs.CreateRasterDataset("raster" + "_" + cmb_FliterMethod.SelectedItem.ToString() + ".tif", "TIFF", origin, Width, Height, cellsizex, cellsizey, 1, rstPixelType.PT_DOUBLE, sr) as IRasterDataset2;
            IRaster resultRaster = resultDataset.CreateFullRaster();
            IRasterCursor resultRasterCursor = ((IRaster2)resultRaster).CreateCursorEx(null);

            IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
            IRaster2 raster = rasterDataset.CreateFullRaster() as IRaster2;
            IRasterCursor rasterCursor = raster.CreateCursorEx(null);

            IPixelBlock3 resultPixelBlock = null;
            IPixelBlock3 tempPixelBlock = null;
            IRasterEdit resultRasterEdit = resultRaster as IRasterEdit;

            long blockWidth = 0;
            long blockHeight = 0;
            // System.Array pixels;
            double[,] kernal = new double[3, 3]{{0,0,0},
                                                        {0,1,0},
                                                        {0,0,0}};
            switch (cmb_FliterMethod.SelectedItem.ToString())
            {
                case "LineDetectionHorizontal":
                    kernal = new double[3, 3]{{-1,-1,-1},
                                                        {2,2,2},
                                                        {-1,-1,-1}};
                    break;
                case "LineDetectionVertical":
                    kernal = new double[3, 3]{{-1,2,-1},
                                                        {-1,2,-1},
                                                        {-1,2,-1}};
                    break;
                case "Laplacian3x3":
                    kernal = new double[3, 3]{{0,-1,0},
                                                        {-1,4,-1},
                                                        {0,-1,0}};
                    break;
                case "Smoothing3x3":
                    kernal = new double[3, 3]{{1,2,1},
                                                        {2,4,2},
                                                        {1,2,1}};
                    break;
                case "Sharpening3x3":
                    kernal = new double[3, 3]{{-1,-1,-1},
                                                        {-1,9,-1},
                                                        {-1,-1,-1}};
                    break;
            }
            do
            {
                resultPixelBlock = resultRasterCursor.PixelBlock as IPixelBlock3;
                tempPixelBlock = rasterCursor.PixelBlock as IPixelBlock3;

                System.Array pixels = (System.Array)tempPixelBlock.get_PixelData(0);
                //MessageBox.Show(pixels3.GetValue(0, 0).GetType().ToString());
                blockHeight = resultPixelBlock.Height;
                blockWidth = resultPixelBlock.Width;
                System.Array resultPixels = (System.Array)resultPixelBlock.get_PixelData(0);
                //MessageBox.Show(resultPixels.GetValue(0, 0).GetType().ToString());
                for (int i = 0; i < blockHeight; i++)
                {
                    for (int j = 0; j < blockWidth; j++)
                    {
                        double sum = 0;
                        for (int ki = -1; ki <= 1; ki++)
                        {
                            for (int kj = -1; kj <= 1; kj++)
                            {
                                long idxi = (i + ki) < 0 ? 0 : i + ki >= blockHeight ? blockHeight - 1 : (i + ki);
                                long idxj = (j + kj) < 0 ? 0 : j + kj >= blockWidth ? blockWidth - 1 : (j + kj);
                                double raw = double.Parse(pixels.GetValue(idxj, idxi).ToString());
                                sum += raw * kernal[ki + 1, kj + 1];
                            }
                        }
                        resultPixels.SetValue(sum, j, i);
                    }
                }
                resultPixelBlock.set_PixelData(0, (System.Array)resultPixels);
                resultRasterEdit.Write(resultRasterCursor.TopLeft, (IPixelBlock)resultPixelBlock);
                resultRasterEdit.Refresh();
            } while (resultRasterCursor.Next() == true && rasterCursor.Next() == true);
            IRasterDataset pRasterDs = pRasterWs.OpenRasterDataset("raster" + "_" + cmb_FliterMethod.SelectedItem.ToString() + ".tif");
            IRaster praster = ((IRasterDataset2)pRasterDs).CreateFullRaster();

            //IRasterLayer resLayer = new RasterLayerClass();
            //resLayer.CreateFromRaster(praster);
            m_raster = praster;
        }
    }
}
