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
using System.Windows.Forms;
using System.Xml;

namespace RDB
{
    public partial class stretchFunction : Form
    {
        IRaster m_raster;//栅格
        int StretchType=0;//拉伸类型
        double MaxPercent=1;//最大百分比
        double MinPercent=0;//最小百分比
        double NumberOfStandardDeviations=2;//标准差n值
        public bool IsFinished = false;//判断用户是否进行设置
        XmlNode m_xmlNode;//XML结点

        //空构造函数
        //public stretchFunction()
        //{
        //    InitializeComponent();
        //    //加入可选拉伸选项
        //    cmb_StretchType.Items.Add("None");
        //    cmb_StretchType.Items.Add("StandardDeviation");
        //    cmb_StretchType.Items.Add("HistogramEqualization");
        //    cmb_StretchType.Items.Add("MinimumMaximum");
        //    cmb_StretchType.Items.Add("PercentMinimumMaximum");
        //    cmb_StretchType.Items.Add("Gaussian");
        //    cmb_StretchType.Items.Add("HistogramSpecification");
        //}

        //构造函数1，通过传入XMLNode结点,获取其内部信息对类进行初始化
        public stretchFunction(XmlNode in_xmlNode)
        {
            InitializeComponent();
            m_xmlNode = in_xmlNode;
            //处理xml，获取其中装载的拉伸类型，最大最小百分比以及标准差n值信息。           
            XmlNodeList childlist = m_xmlNode.ChildNodes;
            foreach (XmlNode node in childlist)
            {
                if (node is XmlComment)
                    continue;
                XmlElement element = node as XmlElement;
                if (element.Name == "StretchType") StretchType = int.Parse(element.InnerText);
                if (element.Name == "MaxPercent") MaxPercent = double.Parse(element.InnerText);
                if (element.Name == "MinPercent") MinPercent = double.Parse(element.InnerText);
                if (element.Name == "NumberOfStandardDeviations") NumberOfStandardDeviations = double.Parse(element.InnerText);
            }
            //加入可选拉伸类型
                cmb_StretchType.Items.Add("None");
                cmb_StretchType.Items.Add("StandardDeviation");
                cmb_StretchType.Items.Add("HistogramEqualization");
                cmb_StretchType.Items.Add("MinimumMaximum");
                cmb_StretchType.Items.Add("PercentMinimumMaximum");
                cmb_StretchType.Items.Add("Gaussian");
                cmb_StretchType.Items.Add("HistogramSpecification");
                //由于在StretchFunction参数设置中，拉伸类型并非按顺序123排列，需要进行特殊处理。
                if (StretchType == 0)
                    cmb_StretchType.SelectedIndex = StretchType;
                else
                    cmb_StretchType.SelectedIndex = StretchType - 2;

                stxb_max.Visible = false;
                stxb_min.Visible = false;
                txb_max.Visible = false;
                txb_min.Visible = false;
                stxb_num.Visible = false;
                txb_NumberOfStandardDeviations.Visible = false;
                //如果选择的是按标准差拉伸，则将标准差拉伸相关组件显示
                if (cmb_StretchType.SelectedItem.ToString() == "StandardDeviation")
                {
                    stxb_num.Visible = true;
                    txb_NumberOfStandardDeviations.Visible = true;
                }
                else if (cmb_StretchType.SelectedItem.ToString() == "PercentMinimumMaximum")
                {
                    //如果选择的是按最大最小百分比拉伸，则将最大最小百分比拉伸相关组件显示
                    stxb_max.Visible = true;
                    stxb_min.Visible = true;
                    txb_max.Visible = true;
                    txb_min.Visible = true;
                    
                }
                txb_NumberOfStandardDeviations.Text = NumberOfStandardDeviations.ToString();
                txb_max.Text = MaxPercent.ToString();
                txb_min.Text = MinPercent.ToString();
        }
        //构造函数2，传入Raster和XMlNode，对类进行初始化
        public stretchFunction(IRaster raster, XmlNode in_xmlNode)
        {
            InitializeComponent();
            IRaster2 raster2 = raster as IRaster2;
            IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
            m_raster = rasterDataset.CreateFullRaster();
            m_xmlNode = in_xmlNode;
            //处理xml，获取其中装载的拉伸类型，最大最小百分比以及标准差n值信息。 
            XmlNodeList childlist = m_xmlNode.ChildNodes;
            foreach (XmlNode node in childlist)
            {
                if (node is XmlComment)
                    continue;
                XmlElement element = node as XmlElement;
                if (element.Name == "StretchType") StretchType = int.Parse(element.InnerText);
                if (element.Name == "MaxPercent") MaxPercent = double.Parse(element.InnerText);
                if (element.Name == "MinPercent") MinPercent = double.Parse(element.InnerText);
                if (element.Name == "NumberOfStandardDeviations") NumberOfStandardDeviations = double.Parse(element.InnerText);
            }
            //加入可选拉伸类型
            cmb_StretchType.Items.Add("None");
            cmb_StretchType.Items.Add("StandardDeviation");
            cmb_StretchType.Items.Add("HistogramEqualization");
            cmb_StretchType.Items.Add("MinimumMaximum");
            cmb_StretchType.Items.Add("PercentMinimumMaximum");
            cmb_StretchType.Items.Add("Gaussian");
            cmb_StretchType.Items.Add("HistogramSpecification");
            //由于在StretchFunction参数设置中，拉伸类型并非按顺序123排列，需要进行特殊处理。
            if (StretchType == 0)
                cmb_StretchType.SelectedIndex = StretchType;
            else
                cmb_StretchType.SelectedIndex = StretchType - 2;

            stxb_max.Visible = false;
            stxb_min.Visible = false;
            txb_max.Visible = false;
            txb_min.Visible = false;
            stxb_num.Visible = false;
            txb_NumberOfStandardDeviations.Visible = false;
            //如果选择的是按标准差拉伸，则将标准差拉伸相关组件显示
            if (cmb_StretchType.SelectedItem.ToString() == "StandardDeviation")
            {
                stxb_num.Visible = true;
                txb_NumberOfStandardDeviations.Visible = true;
               
            }
            else if (cmb_StretchType.SelectedItem.ToString() == "PercentMinimumMaximum")
            {
                //如果选择的是按最大最小百分比拉伸，则将最大最小百分比拉伸相关组件显示
                stxb_max.Visible = true;
                stxb_min.Visible = true;
                txb_max.Visible = true;
                txb_min.Visible = true;
                
            }
            txb_NumberOfStandardDeviations.Text = NumberOfStandardDeviations.ToString();
            txb_max.Text = MaxPercent.ToString();
            txb_min.Text = MinPercent.ToString();

           
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
            //如果选择的是按标准差拉伸，则将标准差拉伸相关组件显示
            if (cmb_StretchType.SelectedItem.ToString() == "StandardDeviation")
            {
                stxb_num.Visible = true;
                txb_NumberOfStandardDeviations.Visible = true;
            }
            else if (cmb_StretchType.SelectedItem.ToString() == "PercentMinimumMaximum")
            {
                //如果选择的是按最大最小百分比拉伸，则将最大最小百分比拉伸相关组件显示。
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
                //由于在StretchFunction参数设置中，拉伸类型并非按顺序123排列，需要进行特殊处理。
                if(cmb_StretchType.SelectedIndex==0)
                   StretchType = 0;
                else
                   StretchType = (cmb_StretchType.SelectedIndex + 2);
                //若选择标准差拉伸，则设置N值参数。
                if (cmb_StretchType.SelectedItem.ToString() == "StandardDeviation")
                {
                    NumberOfStandardDeviations = double.Parse(txb_NumberOfStandardDeviations.Text.ToString());
                }
                else if (cmb_StretchType.SelectedItem.ToString() == "PercentMinimumMaximum")
                {
                    //若选择按最大最小百分比拉伸，则设置最大最小百分比参数
                    MinPercent = double.Parse(txb_min.Text.ToString());
                    MaxPercent = double.Parse(txb_max.Text.ToString());
                }
                //修改xmlNode，更改其中装载的拉伸类型，最大最小百分比以及标准差n值信息。 
                XmlNodeList childlist = m_xmlNode.ChildNodes;
                foreach (XmlNode node in childlist)
                {
                    if (node is XmlComment)
                        continue;
                    XmlElement element = node as XmlElement;
                    if (element.Name == "StretchType") element.InnerText = StretchType.ToString();
                    if (element.Name == "MaxPercent")  element.InnerText=MaxPercent.ToString();
                    if (element.Name == "MinPercent")  element.InnerText=MinPercent.ToString();
                    if (element.Name == "NumberOfStandardDeviations") element.InnerText=NumberOfStandardDeviations.ToString();
                }
                IsFinished = true;//用户点击了确定
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
            if (m_raster == null)//判断是否有栅格
            {
                MessageBox.Show("当前类内并无栅格");
                return null;
            }
            else
            {
                return m_raster;
            }
        }
        //返回XMLNode
        public XmlNode GetXMLNode()
        {
            return m_xmlNode;
        }
        //执行函数
        public void Init()
        {
            try
            {
                //创建Stretch栅格函数参数对象
                IStretchFunctionArguments stretchFunctionArguments = (IStretchFunctionArguments)new StretchFunctionArguments();
                //设置要处理的栅格
                stretchFunctionArguments.Raster = m_raster;
                //设置拉伸类型
                stretchFunctionArguments.StretchType = (esriRasterStretchType)StretchType;

                if (cmb_StretchType.SelectedItem.ToString() == "StandardDeviation")//如果是按标准差拉伸，需要设置参数N
                {
                    //设置参数N
                    stretchFunctionArguments.NumberOfStandardDeviations = NumberOfStandardDeviations;
                }
                else if (cmb_StretchType.SelectedItem.ToString() == "PercentMinimumMaximum")//如果是按最大最小百分比拉伸，需要设置最大最小百分比
                {
                    //设置最大最小百分比
                    stretchFunctionArguments.MinPercent = MinPercent;
                    stretchFunctionArguments.MaxPercent = MaxPercent;
                }
                //创建栅格函数
                IRasterFunction rasterFunction = new StretchFunction();
                //创建栅格函数数据集对象
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                //设置栅格函数数据集名称对象，设置其临时文件存储地点
                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                functionRasterDatasetName.FullName = @"D:\RDB";
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                //执行栅格函数
                functionRasterDataset.Init(rasterFunction, stretchFunctionArguments);
                //将执行结果存入成员变量中
                IRasterDataset2 rstDatasetr2 = (IRasterDataset2)functionRasterDataset;
                m_raster = rstDatasetr2.CreateFullRaster();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //测试用
        ////可以通过构造函数传参数
        //public stretchFunction(IRaster raster, int in_StretchType = 0, double in_MaxPercent = 0, double in_MinPercent = 0, double in_NumberOfStandardDeviations = 0)
        //{
        //    InitializeComponent();
        //    StretchType = in_StretchType;
        //    MaxPercent = in_MaxPercent;
        //    MinPercent = in_MinPercent;
        //    NumberOfStandardDeviations = in_NumberOfStandardDeviations;
        //    IRaster2 raster2 = m_raster as IRaster2;
        //    IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
        //    m_raster = rasterDataset.CreateFullRaster();

        //    cmb_StretchType.Items.Add("None");
        //    cmb_StretchType.Items.Add("StandardDeviation");
        //    cmb_StretchType.Items.Add("HistogramEqualization");
        //    cmb_StretchType.Items.Add("MinimumMaximum");
        //    cmb_StretchType.Items.Add("PercentMinimumMaximum");
        //    cmb_StretchType.Items.Add("Gaussian");
        //    cmb_StretchType.Items.Add("HistogramSpecification");
        //    if (in_StretchType == 0)
        //        cmb_StretchType.SelectedIndex = StretchType;
        //    else
        //        cmb_StretchType.SelectedIndex = StretchType - 2;

        //    txb_max.Text = MaxPercent.ToString();
        //    txb_min.Text = MinPercent.ToString();
        //    txb_NumberOfStandardDeviations.Text = NumberOfStandardDeviations.ToString();
        //}
    }
}
