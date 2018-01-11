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
    public partial class SelectBandsForm : Form
    {
        //存储要绘制对比直方图的栅格图层对象
        private IRasterLayer m_rstlayer;
        //存储栅格图层的波段总数
        private int m_bandnum;
        //存储栅格图层的选择的波段
        private int[] m_selband;

        public SelectBandsForm()
        {
            InitializeComponent();
        }
        public SelectBandsForm(IRasterLayer rstlayer)
        {
            InitializeComponent();
            m_rstlayer = rstlayer;

            //波段选择窗体的实现
            if (rstlayer != null)
            {
                IRaster2 raster2 = rstlayer.Raster as IRaster2;
                IRasterDataset rstDataset = raster2.RasterDataset;
                IRasterBandCollection rstBandColl = rstDataset as IRasterBandCollection;
                m_bandnum = rstBandColl.Count;
                for (int i = 1; i <= m_bandnum; i++)
                {
                    cklb_CompareHistogram.Items.Add("波段" + i);
                }
               
            }


        }
        //点击绘制对比直方图，进行多直方图对比绘制
        private void btn_DrawCompareHistogram_Click(object sender, EventArgs e)
        {
            //检测有多少个选择的波段
            int k = 0;
            for (int i = 0; i < cklb_CompareHistogram.Items.Count; i++)
            {
                if (cklb_CompareHistogram.GetItemChecked(i)) k++;
            }
            if (k == 0){ MessageBox.Show("请选择至少一个波段！"); return;}
            m_selband = new int[k];
            //把选择的波段索引存储在整数数组里
            int j = 0;
            for (int i = 0; i < cklb_CompareHistogram.Items.Count; i++)
            {
                if (cklb_CompareHistogram.GetItemChecked(i))
                {
                    m_selband[j] = i;
                    j++;
                }
            }
            //创建对比直方图绘制窗体对象，并展示出来
            HistogramCompareForm HistogramCompare = new HistogramCompareForm(m_rstlayer, m_selband);
            HistogramCompare.ShowDialog();
        }

    }
}
