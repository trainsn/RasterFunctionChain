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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RDB
{
    public partial class ndviFunction : Form
    {
        IRaster m_raster;
        int InfraredBandID;
        int VisibleBandID;
        XmlNode m_xmlNode;

        public ndviFunction()
        {
            InitializeComponent();
            //创建默认xmlNode
            XmlDocument doc = new XmlDocument();
            XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");

            XmlElement ndvinode = doc.CreateElement("Function");
            xn.AppendChild(ndvinode);
            ndvinode.SetAttribute("name", "ndviFunction");
            ndvinode.SetAttribute("description", "A raster NDVI function.");

            XmlElement xmlnode2 = doc.CreateElement("InfraredBandID", "3");
            XmlElement xmlnode3 = doc.CreateElement("VisibleBandID", "2");

            ndvinode.AppendChild(xmlnode2);
            ndvinode.AppendChild(xmlnode3);

            m_xmlNode = ndvinode;
        }
        //可以通过构造函数传参
        public ndviFunction(IRaster raster, int in_infraredBandindex = 3, int in_redBandindex = 2)
        {
            InitializeComponent();
            m_raster = raster;
            InfraredBandID = in_infraredBandindex;
            VisibleBandID = in_redBandindex;
            IRaster2 raster2 = (IRaster2)m_raster;
            IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
            m_raster = rasterDataset.CreateFullRaster();

            IRasterBandCollection rstBandColl = raster as IRasterBandCollection;
            for (int i = 0; i < rstBandColl.Count; i++)
            {
                cmb_infraredBandindex.Items.Add(i + 1);
                cmb_redBandindex.Items.Add(i + 1);
            }
            if (rstBandColl.Count >= 4)
            {
                //设置波段选择的默认值
                cmb_infraredBandindex.SelectedIndex = InfraredBandID;
                cmb_redBandindex.SelectedIndex = VisibleBandID;
            }
        }
        public ndviFunction(XmlNode in_xmlNode)
        {
            m_xmlNode = in_xmlNode;
            //处理xml
            XmlNodeList childlist = m_xmlNode.ChildNodes;
            foreach (XmlNode node in childlist)
            {
                if (node is XmlComment)
                    continue;
                XmlElement element = node as XmlElement;
                if (element.Name == "InfraredBandID") InfraredBandID = int.Parse(element.InnerText);
                if (element.Name == "VisibleBandID") VisibleBandID = int.Parse(element.InnerText);
            }
            cmb_infraredBandindex.SelectedIndex = InfraredBandID;
            cmb_redBandindex.SelectedIndex = VisibleBandID;
        }
        public ndviFunction(IRaster raster, XmlNode in_xmlNode)
        {
            InitializeComponent();
            IRaster2 raster2 = m_raster as IRaster2;
            IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
            m_raster = rasterDataset.CreateFullRaster();
            m_xmlNode = in_xmlNode;
            //处理xml
            XmlNodeList childlist = m_xmlNode.ChildNodes;
            foreach (XmlNode node in childlist)
            {
                if (node is XmlComment)
                    continue;
                XmlElement element = node as XmlElement;
                if (element.Name == "InfraredBandID") InfraredBandID = int.Parse(element.Value);
                if (element.Name == "VisibleBandID") VisibleBandID = int.Parse(element.Value);
            }
            cmb_infraredBandindex.SelectedIndex = InfraredBandID;
            cmb_redBandindex.SelectedIndex = VisibleBandID;
        }
        //设置参数
        private void btn_init_Click(object sender, EventArgs e)
        {
            try
            {
                InfraredBandID = cmb_infraredBandindex.SelectedIndex;
                VisibleBandID = cmb_redBandindex.SelectedIndex;
                //修改xml
                XmlNodeList childlist = m_xmlNode.ChildNodes;
                foreach (XmlNode node in childlist)
                {
                    if (node is XmlComment)
                        continue;
                    XmlElement element = node as XmlElement;
                    if (element.Name == "InfraredBandID") element.Value=InfraredBandID.ToString();
                    if (element.Name == "VisibleBandID")  element.Value=VisibleBandID.ToString();
                }
                this.Close();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //返回结果
        public IRaster GetRaster()
        {
            return m_raster;
        }
        //返回XMLNode
        public XmlNode GetXMLNode()
        {
            return m_xmlNode;
        }

        //执行栅格函数
        public void init()
        {
            try
            {
                INDVIFunctionArguments ndviFunctionArguments = (INDVIFunctionArguments)new NDVIFunctionArguments();
                ndviFunctionArguments.InfraredBandID = InfraredBandID;
                ndviFunctionArguments.VisibleBandID = VisibleBandID;
                ndviFunctionArguments.Raster = m_raster;

                IRasterFunction rasterFunction = new NDVIFunction();
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();

                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                functionRasterDatasetName.FullName = @"F:\RDB" + "\\" + "NDVI";
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                functionRasterDataset.Init(rasterFunction, ndviFunctionArguments);

                IRasterDataset2 rstDatasetr2 = (IRasterDataset2)functionRasterDataset;
                m_raster = rstDatasetr2.CreateFullRaster();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
