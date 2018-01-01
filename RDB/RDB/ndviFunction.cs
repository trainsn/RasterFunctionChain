using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
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
    public partial class ndviFunction : Form
    {
        IRaster m_raster;//栅格
        int InfraredBandID;//近红外波段
        int VisibleBandID;//红波段
        XmlNode m_xmlNode;//XML结点
        public bool IsFinished = false;//判断用户是否进行设置
        //空构造函数
        //public ndviFunction()
        //{
        //    InitializeComponent();
        //}

        //构造函数1，通过传入XMLNode结点,获取其内部信息对类进行初始化
        public ndviFunction(XmlNode in_xmlNode)
        {
            try
            {
                m_xmlNode = in_xmlNode;
                //处理xml，获取其中装载的近红外波段和红波段数据
                XmlNodeList childlist = m_xmlNode.ChildNodes;
                foreach (XmlNode node in childlist)
                {
                    if (node is XmlComment)
                        continue;
                    XmlElement element = node as XmlElement;
                    if (element.Name == "InfraredBandID") InfraredBandID = int.Parse(element.InnerText);
                    if (element.Name == "VisibleBandID") VisibleBandID = int.Parse(element.InnerText);
                }
                //默认情况下，显示三个波段
                for (int i = 0; i < 3; i++)
                {
                    cmb_infraredBandindex.Items.Add(i + 1);
                    cmb_redBandindex.Items.Add(i + 1);
                }
                //判断传入的参数是否小于3
                if (InfraredBandID <= 2 || VisibleBandID <= 2)
                {
                    //设置默认选项为传入的参数
                    cmb_infraredBandindex.SelectedIndex = InfraredBandID;
                    cmb_redBandindex.SelectedIndex = VisibleBandID;
                }
                else
                {
                    MessageBox.Show("波段数与输入数不匹配！" + "\n" + "请重新选择!");
                    //设置默认选项为第一波段
                    cmb_infraredBandindex.SelectedIndex = 0;
                    cmb_redBandindex.SelectedIndex = 0;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public ndviFunction(IRaster raster, XmlNode in_xmlNode)
        {
            try
            {
                InitializeComponent();

                IRaster2 raster2 = raster as IRaster2;
                IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
                m_raster = rasterDataset.CreateFullRaster();
                m_xmlNode = in_xmlNode;
                //处理xml，获取其中装载的近红外波段和红波段数据
                XmlNodeList childlist = m_xmlNode.ChildNodes;
                foreach (XmlNode node in childlist)
                {
                    if (node is XmlComment)//如果是评论，则跳过
                        continue;
                    XmlElement element = node as XmlElement;
                    if (element.Name == "InfraredBandID") InfraredBandID = int.Parse(element.InnerText);
                    if (element.Name == "VisibleBandID") VisibleBandID = int.Parse(element.InnerText);
                }
                //获取栅格波段信息，将其添加到下拉框中
                IRasterBandCollection rstBandColl = m_raster as IRasterBandCollection;
                for (int i = 0; i < rstBandColl.Count; i++)
                {
                    cmb_infraredBandindex.Items.Add(i + 1);
                    cmb_redBandindex.Items.Add(i + 1);
                }
                //如果输入的参数波段数大于栅格本身的波段数，则弹出警告，并重置选择波段为第一波段
                if (InfraredBandID > rstBandColl.Count || VisibleBandID > rstBandColl.Count)
                {
                    MessageBox.Show("波段数与输入数不匹配！"+"\n"+ "请重新选择!");
                    cmb_infraredBandindex.SelectedIndex = 0;
                    cmb_redBandindex.SelectedIndex = 0;
                }
                else
                {
                    //否则，则将输入的参数作为下拉框的默认选项
                    cmb_infraredBandindex.SelectedIndex = InfraredBandID;
                    cmb_redBandindex.SelectedIndex = VisibleBandID;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        //设置参数
        private void btn_init_Click(object sender, EventArgs e)
        {
            try
            {
                //获取用户选择的近红外波段和红波段ID
                InfraredBandID = cmb_infraredBandindex.SelectedIndex;
                VisibleBandID = cmb_redBandindex.SelectedIndex;
                //修改xml，设置近红外波段和红波段ID
                XmlNodeList childlist = m_xmlNode.ChildNodes;
                foreach (XmlNode node in childlist)
                {
                    if (node is XmlComment)
                        continue;
                    XmlElement element = node as XmlElement;
                    if (element.Name == "InfraredBandID") element.InnerText=InfraredBandID.ToString();
                    if (element.Name == "VisibleBandID")  element.InnerText=VisibleBandID.ToString();
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
            if (m_raster == null)
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

        //执行栅格函数
        public void Init()
        {
            try
            {
                //创建NDVI栅格函数参数对象
                INDVIFunctionArguments ndviFunctionArguments = (INDVIFunctionArguments)new NDVIFunctionArguments();
                //设置近红外波段信息
                ndviFunctionArguments.InfraredBandID = InfraredBandID;
                //设置红波段信息
                ndviFunctionArguments.VisibleBandID = VisibleBandID;
                //设置要处理的栅格
                ndviFunctionArguments.Raster = m_raster;
                //创建栅格函数
                IRasterFunction rasterFunction = new NDVIFunction();
                //创建栅格函数数据集对象
                IFunctionRasterDataset functionRasterDataset = new FunctionRasterDataset();
                //设置栅格函数数据集名称对象，设置其临时文件存储地点
                IFunctionRasterDatasetName functionRasterDatasetName = (IFunctionRasterDatasetName)new FunctionRasterDatasetNameClass();
                functionRasterDatasetName.FullName = @"D:\RDB";
                functionRasterDataset.FullName = (IName)functionRasterDatasetName;
                //执行栅格函数
                functionRasterDataset.Init(rasterFunction, ndviFunctionArguments);
                //将执行结果存入成员变量中
                IRasterDataset2 rstDatasetr2 = (IRasterDataset2)functionRasterDataset;
                m_raster = rstDatasetr2.CreateFullRaster();
            }
            catch (System.Exception ex)//异常处理，输出错误信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void UnderInit()
        {
            //接口转换
            IRaster2 raster2 = m_raster as IRaster2;
            //获取栅格的属性
            IRasterProps pRasterProps = raster2 as IRasterProps;
            //获取栅格的高度和宽度
            int Height = pRasterProps.Height;
            int Width = pRasterProps.Width;
            //获取像元的大小
            double cellsizex = pRasterProps.MeanCellSize().X;
            double cellsizey = pRasterProps.MeanCellSize().Y;
            //创建工作空间，存储NDVI计算结果
            IWorkspaceFactory pRasterWsFac = new RasterWorkspaceFactoryClass();
            IWorkspace pWs = pRasterWsFac.OpenFromFile(@"F:/RDB", 0);
            IRasterWorkspace2 pRasterWs;
            pRasterWs = pWs as IRasterWorkspace2;
            //设置原点与原栅格相同
            IPoint origin = new PointClass();
            origin.PutCoords(pRasterProps.Extent.XMin, pRasterProps.Extent.YMin);
            //定义空间参考系为未知参考系
            ISpatialReference sr = new UnknownCoordinateSystemClass();
            //创建栅格
            IRasterDataset2 resultDataset = pRasterWs.CreateRasterDataset("NDVI.tif", "TIFF", origin, Width, Height, cellsizex, cellsizey, 1, rstPixelType.PT_DOUBLE, sr) as IRasterDataset2;
            //对创建的栅格创建栅格指针
            IRaster resultRaster = resultDataset.CreateFullRaster();
            IRasterCursor resultRasterCursor = ((IRaster2)resultRaster).CreateCursorEx(null);
            //对原始栅格创建栅格指针
            IRasterCursor rasterCursor = raster2.CreateCursorEx(null);
            //创建两个像素块，用以同时遍历两个栅格
            IPixelBlock3 resultPixelBlock = null;
            IPixelBlock3 tempPixelBlock = null;
            //创建IRasterEidt以便对结果栅格进行修改，填入计算结果
            IRasterEdit resultRasterEdit = resultRaster as IRasterEdit;
            //初始化像素块宽度与高度
            long blockWidth = 0;
            long blockHeight = 0;

            do
            {
                //对两个像素块进行更新
                resultPixelBlock = resultRasterCursor.PixelBlock as IPixelBlock3;
                tempPixelBlock = rasterCursor.PixelBlock as IPixelBlock3;
                //获取原始栅格中近红外波段以及红波段的数据
                System.Array pixels3 = (System.Array)tempPixelBlock.get_PixelData(VisibleBandID);
                System.Array pixels4 = (System.Array)tempPixelBlock.get_PixelData(InfraredBandID);
                //获取结果栅格像素块的高度和宽度，并以此进行遍历
                blockHeight = resultPixelBlock.Height;
                blockWidth = resultPixelBlock.Width;
                //获取结果栅格像素块中第一波段的值
                System.Array resultPixels = (System.Array)resultPixelBlock.get_PixelData(0);
                //根据NDVI公式，将计算结果写入到结果栅格像素块中
                for (int i = 0; i < blockHeight; i++)
                {
                    for (int j = 0; j < blockWidth; j++)
                    {
                        double up = double.Parse(pixels4.GetValue(j, i).ToString()) - double.Parse(pixels3.GetValue(j, i).ToString());
                        double down = double.Parse(pixels4.GetValue(j, i).ToString()) + double.Parse(pixels3.GetValue(j, i).ToString());
                        if (down != 0)
                        {
                            resultPixels.SetValue((up / down), j, i);
                        }
                        else
                        {
                            resultPixels.SetValue((0.0), j, i);
                        }
                    }
                }
                //将结算结果设置到结果栅格图像对应的像素块中，根据其位置进行更新后刷新
                resultPixelBlock.set_PixelData(0, (System.Array)resultPixels);
                resultRasterEdit.Write(resultRasterCursor.TopLeft, (IPixelBlock)resultPixelBlock);
                resultRasterEdit.Refresh();

            } while (resultRasterCursor.Next() == true && rasterCursor.Next() == true);//遍历完一块像素块之后遍历下一块，直到栅格遍历结束。
            //将结果栅格存入到成员变量m_raster中
            m_raster = resultRaster;

            //我在这里无法删除这个文件怎么办！！！！！！！！！
            //System.IO.File.Delete("F:/RDB/NDVI.tif");
            
        }
        //可以通过构造函数传参
        //public ndviFunction(IRaster raster, int in_infraredBandindex = 3, int in_redBandindex = 2)
        //{
        //    InitializeComponent();
        //    m_raster = raster;
        //    InfraredBandID = in_infraredBandindex;
        //    VisibleBandID = in_redBandindex;
        //    IRaster2 raster2 = (IRaster2)m_raster;
        //    IRasterDataset2 rasterDataset = raster2.RasterDataset as IRasterDataset2;
        //    m_raster = rasterDataset.CreateFullRaster();

        //    IRasterBandCollection rstBandColl = raster as IRasterBandCollection;
        //    for (int i = 0; i < rstBandColl.Count; i++)
        //    {
        //        cmb_infraredBandindex.Items.Add(i + 1);
        //        cmb_redBandindex.Items.Add(i + 1);
        //    }
        //    if (rstBandColl.Count >= 4)
        //    {
        //        //设置波段选择的默认值
        //        cmb_infraredBandindex.SelectedIndex = InfraredBandID;
        //        cmb_redBandindex.SelectedIndex = VisibleBandID;
        //    }
        //}
    }
}
