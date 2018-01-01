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
    public partial class stretchFunction : Form
    {
        IRaster m_raster;
        int StretchType;
        double MaxPercent;
        double MinPercent;
        double NumberOfStandardDeviations;
        XmlNode m_xmlNode;

        public stretchFunction()
        {
            InitializeComponent();
            cmb_StretchType.Items.Add("None");
            cmb_StretchType.Items.Add("StandardDeviation");
            cmb_StretchType.Items.Add("HistogramEqualization");
            cmb_StretchType.Items.Add("MinimumMaximum");
            cmb_StretchType.Items.Add("PercentMinimumMaximum");
            cmb_StretchType.Items.Add("Gaussian");
            cmb_StretchType.Items.Add("HistogramSpecification");

            //创建默认xmlNode
            XmlDocument doc = new XmlDocument();
            XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");

            XmlElement stretchnode = doc.CreateElement("Function");
            xn.AppendChild(stretchnode);
            stretchnode.SetAttribute("name", "stretchFunction");
            stretchnode.SetAttribute("description", "A raster Stretch function.");

            XmlElement xmlnode2 = doc.CreateElement("StretchType", "0");
            XmlElement xmlnode3 = doc.CreateElement("MaxPercent", "1");
            XmlElement xmlnode4 = doc.CreateElement("MinPercent", "0");
            XmlElement xmlnode5 = doc.CreateElement("NumberOfStandardDeviations", "2");

            stretchnode.AppendChild(xmlnode2);
            stretchnode.AppendChild(xmlnode3);
            stretchnode.AppendChild(xmlnode4);
            stretchnode.AppendChild(xmlnode5);
            m_xmlNode = stretchnode;
            
        }
        //可以通过构造函数传参数
        public stretchFunction(IRaster raster, int in_StretchType = 0, double in_MaxPercent = 0, double in_MinPercent = 0, double in_NumberOfStandardDeviations=0)
        {
            InitializeComponent();
            StretchType = in_StretchType;
            MaxPercent = in_MaxPercent;
            MinPercent = in_MinPercent;
            NumberOfStandardDeviations = in_NumberOfStandardDeviations;
            IRaster2 raster2 = m_raster as IRaster2;
            IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
            m_raster = rasterDataset.CreateFullRaster();

            cmb_StretchType.Items.Add("None");
            cmb_StretchType.Items.Add("StandardDeviation");
            cmb_StretchType.Items.Add("HistogramEqualization");
            cmb_StretchType.Items.Add("MinimumMaximum");
            cmb_StretchType.Items.Add("PercentMinimumMaximum");
            cmb_StretchType.Items.Add("Gaussian");
            cmb_StretchType.Items.Add("HistogramSpecification");
            if (in_StretchType==0)
                cmb_StretchType.SelectedIndex = StretchType;
            else
                cmb_StretchType.SelectedIndex = StretchType-2;

            txb_max.Text = MaxPercent.ToString();
            txb_min.Text = MinPercent.ToString();
            txb_NumberOfStandardDeviations.Text = NumberOfStandardDeviations.ToString();
        }
        public stretchFunction(XmlNode in_xmlNode)
        {
            m_xmlNode = in_xmlNode;
            //处理xml
            XmlNodeList childlist = m_xmlNode.ChildNodes;
            foreach (XmlNode node in childlist)
            {
                if (node is XmlComment)
                    continue;
                XmlElement element = node as XmlElement;
                if (element.Name == "StretchType") StretchType = int.Parse(element.Value);
                if (element.Name == "MaxPercent") MaxPercent = int.Parse(element.Value);
                if (element.Name == "MinPercent") MinPercent = int.Parse(element.Value);
                if (element.Name == "NumberOfStandardDeviations") NumberOfStandardDeviations = int.Parse(element.Value);
            }
            txb_max.Text = MaxPercent.ToString();
            txb_min.Text = MinPercent.ToString();
            txb_NumberOfStandardDeviations.Text = NumberOfStandardDeviations.ToString();
        }
        public stretchFunction(IRaster raster, XmlNode in_xmlNode)
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
                if (element.Name == "StretchType") StretchType = int.Parse(element.Value);
                if (element.Name == "MaxPercent") MaxPercent = int.Parse(element.Value);
                if (element.Name == "MinPercent") MinPercent = int.Parse(element.Value);
                if (element.Name == "NumberOfStandardDeviations") NumberOfStandardDeviations = int.Parse(element.Value);
            }
            txb_max.Text = MaxPercent.ToString();
            txb_min.Text = MinPercent.ToString();
            txb_NumberOfStandardDeviations.Text = NumberOfStandardDeviations.ToString();
        }
        //窗体内组件变换
        private void cmb_StretchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            stxb_max.Visible = false;
            stxb_min.Visible = false;
            txb_max.Visible = false;
            txb_min.Visible = false;
            stxb_num.Visible = false;
            txb_NumberOfStandardDeviations.Visible = false;
            if (cmb_StretchType.SelectedItem.ToString() == "StandardDeviation")
            {
                stxb_num.Visible = true;
                txb_NumberOfStandardDeviations.Visible = true;
            }
            else if (cmb_StretchType.SelectedItem.ToString() == "PercentMinimumMaximum")
            {
                stxb_max.Visible = true;
                stxb_min.Visible = true;
                txb_max.Visible = true;
                txb_min.Visible = true;
            }
        }
        //定义参数
        private void btn_Stretch_Click(object sender, EventArgs e)
        {
            try
            {

                if(cmb_StretchType.SelectedIndex==0)
                   StretchType = 0;
                else
                   StretchType = (cmb_StretchType.SelectedIndex + 2);

                if (cmb_StretchType.SelectedItem.ToString() == "StandardDeviation")
                {
                    NumberOfStandardDeviations = int.Parse(txb_NumberOfStandardDeviations.Text.ToString());
                }
                else if (cmb_StretchType.SelectedItem.ToString() == "PercentMinimumMaximum")
                {
                    MinPercent = double.Parse(txb_min.Text.ToString());
                    MaxPercent = double.Parse(txb_max.Text.ToString());
                }
                //修改xmlNode
                XmlNodeList childlist = m_xmlNode.ChildNodes;
                foreach (XmlNode node in childlist)
                {
                    if (node is XmlComment)
                        continue;
                    XmlElement element = node as XmlElement;
                    if (element.Name == "StretchType") element.Value = StretchType.ToString();
                    if (element.Name == "MaxPercent")  element.Value=MaxPercent.ToString();
                    if (element.Name == "MinPercent")  element.Value=MinPercent.ToString();
                    if (element.Name == "NumberOfStandardDeviations") element.Value=NumberOfStandardDeviations.ToString();
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
        //执行函数
        public void init()
        {
            try
            {

                IStretchFunctionArguments stretchFunctionArguments = (IStretchFunctionArguments)new StretchFunctionArguments();
                stretchFunctionArguments.Raster = m_raster;
               
                stretchFunctionArguments.StretchType = (esriRasterStretchType)StretchType;

                if (cmb_StretchType.SelectedItem.ToString() == "StandardDeviation")
                {
                    stretchFunctionArguments.NumberOfStandardDeviations = NumberOfStandardDeviations;
                }
                else if (cmb_StretchType.SelectedItem.ToString() == "PercentMinimumMaximum")
                {
                    stretchFunctionArguments.MinPercent = MinPercent;
                    stretchFunctionArguments.MaxPercent = MaxPercent;
                }

                IRasterFunction rasterFunction = new StretchFunction();
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();

                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                functionRasterDatasetName.FullName = @"F:\RDB" + "\\" + "Stretch";
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                functionRasterDataset.Init(rasterFunction, stretchFunctionArguments);

                IRasterDataset2 rstDatasetr2 = (IRasterDataset2)functionRasterDataset;
                m_raster = rstDatasetr2.CreateFullRaster();
                this.Close();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
