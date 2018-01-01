using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RDB
{
   public partial class aspectFunction
    {
        XmlNode m_xmlNode;
        IRaster m_raster;
        public aspectFunction() 
        {
            m_raster = null;
            m_xmlNode = null;
        }
        public aspectFunction(XmlNode node)
        {
            m_raster = null;
            m_xmlNode = node;
        }
        public aspectFunction(IRaster raster, XmlNode node)
        {
            m_raster = raster;
            m_xmlNode = node;
        }

        public void Init()
        {
            
            IRaster2 raster2 = m_raster as IRaster2;

            IRasterFunction rasterFunction = new AspectFunction();
            IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
            IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
            functionRasterDatasetName.FullName = @"D:\RDB";
            functionRasterDataset.FullName = (IName)functionRasterDatasetName;
            functionRasterDataset.Init(rasterFunction, raster2);

            IRasterDataset rstDataset = functionRasterDataset as IRasterDataset;
            m_raster = rstDataset.CreateDefaultRaster();
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
