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
    class RasterFunction
    {
        public string name;
        public string description;

        public static ILayer Convolution(ILayer layer, int type, string outputRaster)
        {
            try
            {               
                IRasterLayer rstLayer = layer as IRasterLayer;
                IRaster raster = rstLayer.Raster;

                IConvolutionFunctionArguments rasterFunctionArguments = (IConvolutionFunctionArguments)new ConvolutionFunctionArguments();

                //设置输入栅格数据
                rasterFunctionArguments.Raster = raster;
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

                ILayer iLayer = pRstLayer as ILayer;
                return iLayer;
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static IRasterLayer Pansharpening(ILayer sigleLayer, ILayer multiLayer, int type, string outputRaster)
        {
            try
            {
                IRaster2 panRaster2 = ((IRasterLayer)sigleLayer).Raster as IRaster2;
                IRaster2 multiRaster2 = ((IRasterLayer)multiLayer).Raster as IRaster2;
                IRasterDataset panDataset = panRaster2.RasterDataset;
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
                pansharpenFilter.PutWeights(0.1, 0.167, 0.167, 0.5);

                //将全色锐化过滤器设置于多光谱栅格对象上
                IPixelOperation pixeOperation = (IPixelOperation)multiRaster;
                pixeOperation.PixelFilter = (IPixelFilter)pansharpenFilter;

                //保存结果数据集，并加载显示
                //加载显示裁剪结果图像
                IRasterLayer panSharpenLayer = new RasterLayerClass();
                panSharpenLayer.CreateFromRaster(multiRaster);
                panSharpenLayer.Name = "panSharpen_Result";
                panSharpenLayer.SpatialReference = ((IGeoDataset)multiRaster).SpatialReference;
                return panSharpenLayer;
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
