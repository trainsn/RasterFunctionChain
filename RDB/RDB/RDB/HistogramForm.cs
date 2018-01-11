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
    public partial class HistogramForm : Form
    {
        private int maxCount;//图像的最大灰度值计数
        private double[] pixelCounts;//图像灰度值计数数组
        private double minPixel;//最小像元值
        private double maxPixel;//最大像元值
        private int[] xGraduations;//X轴刻度线

        public HistogramForm()
        {  
            InitializeComponent();
        }

        public HistogramForm(double[] pixelCounts,double minPixel,double maxPixel)
        {
            //窗体基本组件的初始化
            InitializeComponent();
            this.pixelCounts = pixelCounts;
            this.minPixel = minPixel;
            this.maxPixel = maxPixel;

            //计算X轴刻度线数值
            xGraduations = new int[6];
            for (int i = 0; i < 6; i++)
            {
                xGraduations[i] = (int)((maxPixel - minPixel) / 5 * i + minPixel);
            }

            //计算最大像元数（y轴）
            maxCount = (int)pixelCounts[0];
            for (int i = 0; i < pixelCounts.Length; i++)
            {
                if (maxCount < pixelCounts[i]) maxCount = (int)pixelCounts[i];
            }
        }

        private void HistogramForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;//获取Graphics对象
            Pen pen = new Pen(Brushes.Black, 1);//实例化细度为1的黑色画笔

            //绘制坐标轴
            g.DrawLine(pen, 50, 240, 320, 240);
            g.DrawLine(pen, 320, 240, 316, 236);
            g.DrawLine(pen, 320, 240, 316, 244);
            g.DrawString("灰度值", new Font("宋体", 9), Brushes.Black, new PointF(318, 243));

            g.DrawLine(pen, 50, 240, 50, 30);
            g.DrawLine(pen, 50, 30, 46, 34);
            g.DrawLine(pen, 50, 30, 54, 34);
            g.DrawString("像元数", new Font("宋体", 9), Brushes.Black, new PointF(10, 20));

            //绘制并标识X轴的坐标刻度
            g.DrawLine(pen, 100, 240, 100, 242);
            g.DrawLine(pen, 150, 240, 150, 242);
            g.DrawLine(pen, 200, 240, 200, 242);
            g.DrawLine(pen, 250, 240, 250, 242);
            g.DrawLine(pen, 300, 240, 300, 242);

            //绘制X轴上的刻度值
            g.DrawString(xGraduations[0].ToString(), new Font("宋体", 8), Brushes.Black, new Point(46, 242));
            g.DrawString(xGraduations[1].ToString(), new Font("宋体", 8), Brushes.Black, new Point(92, 242));
            g.DrawString(xGraduations[2].ToString(), new Font("宋体", 8), Brushes.Black, new Point(139, 242));
            g.DrawString(xGraduations[3].ToString(), new Font("宋体", 8), Brushes.Black, new Point(189, 242));
            g.DrawString(xGraduations[4].ToString(), new Font("宋体", 8), Brushes.Black, new Point(239, 242));
            g.DrawString(xGraduations[5].ToString(), new Font("宋体", 8), Brushes.Black, new Point(289, 242));

            //绘制并标识Y轴的坐标刻度
            g.DrawLine(pen, 48, 40, 50, 40);
            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(34, 234));

            //绘制并标识最大灰度值
            g.DrawString(maxCount.ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(9, 34));

            //绘制直方图
            double xTemp = 0;
            double yTemp = 0;

            for (int i = 0; i < pixelCounts.Length; i++)
            {
                xTemp = i * 1.0 / pixelCounts.Length * (300 - 50);//计算横向位置
                yTemp = 200.0 * pixelCounts[i] / maxCount;//计算纵向长度并绘制
                g.DrawLine(pen, 50 + (int)xTemp, 240, 50 + (int)xTemp, 240 - (int)yTemp);
            }
            //释放资源
            pen.Dispose();
        }
    }
}
