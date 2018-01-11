using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
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

namespace RDB
{
    public partial class HistogramCompareForm : Form
    {
        //存储要绘制对比直方图的栅格图层对象
        private IRasterLayer m_rstlayer;
        //存储选择的波段（数组）
        private int[] m_selband;

        public HistogramCompareForm()
        {
            InitializeComponent();
        }
        public HistogramCompareForm(IRasterLayer rstlayer ,int[] selband)
        {
            //初始化窗体的基本组件
            InitializeComponent();
            //成员变量赋值
            m_rstlayer = rstlayer;
            m_selband = selband;
        }

        //对比直方图窗体的绘制函数paint函数
        private void HistogramCompareForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;// 获取Graphics对象
            Pen pen = new Pen(Brushes.Black, 1); //实例化细度为1的黑色画笔
            Pen pennoteline = new Pen(Brushes.Red, 1);   //实例化细度为1的蓝色画笔，用来做线注记

            // 绘制坐标轴
            g.DrawLine(pen, 50, 240, 320, 240);
            g.DrawLine(pen, 320, 240, 316, 236);
            g.DrawLine(pen, 320, 240, 316, 244);
            g.DrawLine(pen, 50, 240, 50, 30);
            g.DrawLine(pen, 50, 30, 46, 34);
            g.DrawLine(pen, 50, 30, 54, 34);
            g.DrawString("灰度值", new Font("宋体", 9), Brushes.Black, new PointF(318, 243));
            g.DrawString("像元数", new Font("宋体", 9), Brushes.Black, new PointF(10, 20));

            //绘制并标识X轴的坐标刻度
            g.DrawLine(pen, 100, 240, 100, 242);
            g.DrawLine(pen, 150, 240, 150, 242);
            g.DrawLine(pen, 200, 240, 200, 242);
            g.DrawLine(pen, 250, 240, 250, 242);
            g.DrawLine(pen, 300, 240, 300, 242);

            //绘制X轴上的刻度值
            g.DrawString("0", new Font("宋体", 8), Brushes.Black, new Point(46, 242));
            g.DrawString("51", new Font("宋体", 8), Brushes.Black, new Point(92, 242));
            g.DrawString("102", new Font("宋体", 8), Brushes.Black, new Point(139, 242));
            g.DrawString("153", new Font("宋体", 8), Brushes.Black, new Point(189, 242));
            g.DrawString("204", new Font("宋体", 8), Brushes.Black, new Point(239, 242));
            g.DrawString("255", new Font("宋体", 8), Brushes.Black, new Point(289, 242));

            //绘制并标识Y轴的坐标刻度
            g.DrawLine(pen, 48, 40, 50, 40);
            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(34, 234));

            //计算所有波段的最大像元数，绘制并标识每个波段最大灰度值，同时将该点与每个波段统计图的峰值连线，并标记波段信息
            int MaxBandCount = GetMaxBandCount();
            for (int i = 0; i < m_selband.Length; i++)
            {
                int j = m_selband[i];
                //调用绘制直方图的函数，进行绘制
                DrawHisto(j, MaxBandCount, g);
            }

        }

        //绘制直方图的函数，传入参数为波段索引、最大像元数、绘图对象
        private void DrawHisto(int index, int maxst, Graphics g)
        {
            try
            {
                //实现绘制直方图的函数
                IRaster2 raster2 = m_rstlayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandcoll = rstDataset as IRasterBandCollection;
                IRasterBand rstBand = rstBandcoll.Item(index);
                IRasterStatistics rstStatistics = rstBand.Statistics;
                double[] pixelCounts = rstBand.Histogram.Counts as double[];
                //计算最大像元数（y轴）
                int maxCount = (int)pixelCounts[0];
                for (int i = 0; i < pixelCounts.Length; i++)
                {
                    if (maxCount < pixelCounts[i]) maxCount = (int)pixelCounts[i];
                }

                Pen pen = new Pen(Brushes.Black, 1);//实例化细度为1的黑色画笔

                Color color = new Color();
                switch (index)
                {
                    case 0:
                        color = Color.Red;
                        break;
                    case 1:
                        color = Color.Orange;
                        break;
                    case 2:
                        color = Color.Yellow;
                        break;
                    case 3:
                        color = Color.Green;
                        break;
                    case 4:
                        color = Color.Blue;
                        break;
                    case 5:
                        color = Color.Purple;
                        break;
                    case 6:
                        color = Color.Peru;
                        break;
                }
                //画笔颜色赋值
                pen.Color = color;

                //不同波段用不同颜色的画笔绘制直方图

                //绘制并标识最大灰度值
                g.DrawString(maxCount.ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(9, 34));

                //绘制直方图
                double xTemp = 0;
                double yTemp = 0;
                double yMax = 0;
                for (int i = 0; i < pixelCounts.Length; i++)
                {
                    xTemp = i * 1.0 / pixelCounts.Length * (300 - 50);//计算横向位置
                    yTemp = 200.0 * pixelCounts[i] / maxst;//计算纵向长度并绘制
                    if (yTemp > yMax) yMax = yTemp;
                    g.DrawLine(pen, 50 + (int)xTemp, 240, 50 + (int)xTemp, 240 - (int)yTemp);
                }

                //本来准备加一条线以表示最大值，但是感觉加上了也没什么用还混淆了界面QAQ
                // g.DrawLine(pen, 50,240-(int)yMax ,320, 240-(int)yMax);

                //释放资源
                pen.Dispose();
            }
            catch (Exception ex)//处理异常，输出异常信息
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int GetMaxBandCount()
        {
            int[] max = new int[m_selband.Length];
            for (int i = 0; i < m_selband.Length; i++)
            {
                IRaster2 raster2 = m_rstlayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
                IRasterBand rasterBand = rstBandColl.Item(i);                       
                //获取每个像元值的统计个数
                double[] histo = rasterBand.Histogram.Counts as double[];
                //计算最大像元数
                int maxCount = (int)histo[0];
                for (int j = 0; j < histo.Length; j++)
                {
                    if (maxCount < histo[j])
                    {
                        maxCount = (int)histo[j];
                    }
                }
            //记录下该波段的最大像元数
                max[i] = maxCount;
            }
            //计算所有波段的最大像元数
            int maxst = max[0];
            for (int k = 0; k < max.Length; k++)
            {
                if (maxst < max[k]) maxst = (int)max[k];
            }
            return maxst;
        }

    }
}
