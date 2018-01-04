using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace RDB
{
    public partial class RasterFunctionEditor : Form
    {
        IRaster m_raster;
        string filename = "";
        DataTable dataTable = InitialTable();
        int m_hitRowindex;
        int m_doublehitRowindex;
        int m_checkedindex = -1;
        XmlDocument doc = new XmlDocument();
        public bool IsFinished = false;

        public RasterFunctionEditor()
        {
            InitializeComponent();
            InitialDoc();
        }
        public RasterFunctionEditor(IRaster raster)
        {
            InitializeComponent();
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            check.HeaderText = "IsChecked";
            //check.DataPropertyName = "IsChecked";
            dataGridView1.Columns.Add(check);
            dataGridView1.DataSource = dataTable;
            //使得列完全充满表
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //使得默认dataGridView默认输入的部分消失
            dataGridView1.AllowUserToAddRows = false;
            m_raster = raster;
            InitialDoc();
        }

        public void InitialDoc()
        {
            XmlElement root = doc.CreateElement("XmlRasterFunctionTemplate");
            doc.AppendChild(root);
            XmlElement name = doc.CreateElement("Name");
            name.InnerText = "Raster Funciton Template";
            root.AppendChild(name);
            XmlElement descrip = doc.CreateElement("Description");
            descrip.InnerText = "A raster function template.";
            root.AppendChild(descrip);
        }
        //对于表格的初始化
        static DataTable  InitialTable()
        {
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = new DataColumn();//创建列

            //dataColumn = new DataColumn();
            //dataColumn.ColumnName = "number";//设置第一列
            //dataColumn.DataType = System.Type.GetType("System.String");
            //dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "RasterName";//设置第二列
            dataColumn.DataType = System.Type.GetType("System.String");
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Description";//设置第三列
            dataColumn.DataType = System.Type.GetType("System.String");
            dataTable.Columns.Add(dataColumn);

            return dataTable;
        }

        //右键出现contextmenu 最好要判断什么时候能出现右键
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    //确保只有点在cell中才会出现框，并记录hitRowindex
                    DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                    this.contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
                    m_hitRowindex = hit.RowIndex;                   
                }
                else
                {
                    DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                    if (hit.ColumnIndex == 0)
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            dataGridView1.Rows[i].Cells[0].Value = false;
                        }
                        dataGridView1.Rows[hit.RowIndex].Cells[0].Value = true;
                        m_checkedindex = hit.RowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.ColumnIndex != 0)
                {
                    if (hit.Type == DataGridViewHitTestType.Cell)
                    {
                        m_doublehitRowindex = hit.RowIndex;
                        XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");
                        XmlNodeList xnl = xn.ChildNodes;
                        int i = 0;
                        foreach (XmlNode xn1 in xnl)
                        {
                            if (xn1 is XmlComment)
                                continue;
                            //else if (xn1.
                            XmlElement xe = (XmlElement)xn1;
                            if (xe.Name == "Function")
                            {
                                if (i == m_doublehitRowindex)
                                {
                                    switch (xe.GetAttribute("name"))
                                    {
                                        case "Pansharpening":
                                            {
                                                panSharpenFunction panSharpenFunction = new panSharpenFunction(xn1);
                                                panSharpenFunction.ShowDialog();
                                                EditDoc(doc, panSharpenFunction.GetXMLNode(), m_doublehitRowindex);
                                                break;
                                            }
                                        case "Convolution":
                                            {
                                                convolutionFunction convolutionFunction = new convolutionFunction(xn1);
                                                convolutionFunction.ShowDialog();
                                                EditDoc(doc, convolutionFunction.GetXMLNode(), m_doublehitRowindex);
                                                break;
                                            }
                                        case "Slope":
                                            {
                                                slopeFunction slopeFunction = new slopeFunction(xn1);
                                                slopeFunction.ShowDialog();
                                                EditDoc(doc, slopeFunction.GetXMLNode(), m_doublehitRowindex);
                                                break;
                                            }
                                        case "Aspect":
                                            {
                                                break;
                                            }
                                        case "stretchFunction":
                                            {
                                                stretchFunction stretchFunction = new stretchFunction(xn1);
                                                stretchFunction.ShowDialog();
                                                EditDoc(doc, stretchFunction.GetXMLNode(), m_doublehitRowindex);
                                                break;
                                            }
                                        case "ndviFunction":
                                            {
                                                ndviFunction ndviFunction = new ndviFunction(m_raster,xn1);
                                                ndviFunction.ShowDialog();
                                                EditDoc(doc, ndviFunction.GetXMLNode(), m_doublehitRowindex);
                                                break;
                                            }
                                        case "clipFunction":
                                            {
                                                clipFunction clipFunction = new clipFunction(xn1);
                                                clipFunction.ShowDialog();
                                                EditDoc(doc, clipFunction.GetXMLNode(), m_doublehitRowindex);
                                                break;
                                            }
                                        case "hillshadeFunction":
                                            {
                                                hillshadeFunction hillshadeFunction = new hillshadeFunction(xn1);
                                                hillshadeFunction.ShowDialog();
                                                EditDoc(doc, hillshadeFunction.GetXMLNode(), m_doublehitRowindex);
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                }
                                i++;
                            }
                        }
                    }
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clipFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");

                XmlElement xmlnode1 = doc.CreateElement("Function");
                //xn.AppendChild(xmlnode1);
                xmlnode1.SetAttribute("name", "clipFunction");
                xmlnode1.SetAttribute("description", "A raster clip function.");
                //doc.AppendChild(xmlnode1);

                xmlnode1.AppendChild(doc.CreateElement("inputSHPPath", ""));

                //窗口打开
                clipFunction clipFunction = new clipFunction(xmlnode1);
                clipFunction.ShowDialog();

                //得到XML中的Element
                XmlNode node = clipFunction.GetXMLNode();               
                XmlElement temp = node as XmlElement;
                DataRow dataRow = dataTable.NewRow();
                //dataRow[0] = dataTable.Columns.Count;
                //现在是会报错 因为没有传出来temp
                dataRow[0] = temp.GetAttribute("name");
                dataRow[1] = temp.GetAttribute("description");
                m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                dataTable.Rows.InsertAt(dataRow,m_hitRowindex);
                //对doc进行修改
                InsertDoc(doc, node, m_hitRowindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void slopeFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlElement xmlEleSlope = doc.CreateElement("Function");

                xmlEleSlope.SetAttribute("name", "Slope");
                xmlEleSlope.SetAttribute("description", "Slope represents the rate of change of elevation for each digital elevation model (DEM) cell. It's the first derivative of a DEM.");
                XmlElement xmlEleZfactor = doc.CreateElement("Zfactor");               
                xmlEleZfactor.InnerText = (1.0/11111).ToString();
                xmlEleSlope.AppendChild(xmlEleZfactor);

                //窗口打开
                slopeFunction slopeFunction = new slopeFunction(xmlEleSlope);
                slopeFunction.ShowDialog();
                //得到XML中的Element
                XmlNode node = slopeFunction.GetXMLNode();
                
                XmlElement temp = node as XmlElement;
                DataRow dataRow = dataTable.NewRow();       
                //dataRow[0] = dataTable.Columns.Count;
                
                dataRow[0] = temp.GetAttribute("name");
                dataRow[1] = temp.GetAttribute("description");
                m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                //对doc进行修改
                InsertDoc(doc, node, m_hitRowindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void aspectFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlElement xmlEleSlope = doc.CreateElement("Function");
                xmlEleSlope.SetAttribute("name", "Aspect");
                xmlEleSlope.SetAttribute("description", "Aspect identifies the downslope direction of the maximum rate of change in value from each cell to its neighbors. Aspect can be thought of as the slope direction. The values of the output raster will be the compass direction of the aspect.");

                //窗口打开
                aspectFunction aspectFunction = new aspectFunction(xmlEleSlope);
                //aspectFunction.Show();
                //得到XML中的Element
                XmlNode node = aspectFunction.GetXMLNode();
                
                XmlElement temp = node as XmlElement;
                DataRow dataRow = dataTable.NewRow();
                //dataRow[0] = dataTable.Columns.Count;
                //现在是会报错 因为没有传出来temp
                dataRow[0] = temp.GetAttribute("name");
                dataRow[1] = temp.GetAttribute("description");
                m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                //对doc进行修改
                InsertDoc(doc, node, m_hitRowindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void hillshadeFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlElement xmlnode1 = doc.CreateElement("Function");
                //xn.AppendChild(xmlnode1);
                xmlnode1.SetAttribute("name", "hillshadeFunction");
                xmlnode1.SetAttribute("description", "A raster hillshade function.");

                XmlNode node1 = doc.CreateElement("Azimuth");
                node1.InnerText = "50";
                xmlnode1.AppendChild(node1);
                XmlNode node2 = doc.CreateElement("ZFactor");
                node2.InnerText = (1/11111.0).ToString();
                xmlnode1.AppendChild(node2);

                //窗口打开
                hillshadeFunction hillshadeFunction = new hillshadeFunction(xmlnode1);
                hillshadeFunction.ShowDialog();
                //得到XML中的Element
                XmlNode node = hillshadeFunction.GetXMLNode();  
                
                XmlElement temp = node as XmlElement;
                DataRow dataRow = dataTable.NewRow();
                //dataRow[0] = dataTable.Columns.Count;
                //现在是会报错 因为没有传出来temp
                dataRow[0] = temp.GetAttribute("name");
                dataRow[1] = temp.GetAttribute("description");
                m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                //对doc进行修改
                InsertDoc(doc, node, m_hitRowindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stretchFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                XmlElement stretchnode = doc.CreateElement("Function");
                stretchnode.SetAttribute("name", "stretchFunction");
                stretchnode.SetAttribute("description", "A raster Stretch function.");

                XmlElement xmlnode1 = doc.CreateElement("StretchType");
                xmlnode1.InnerText = "0";
                XmlElement xmlnode2 = doc.CreateElement("MaxPercent");
                xmlnode2.InnerText = "1";
                XmlElement xmlnode3 = doc.CreateElement("MinPercent");
                xmlnode3.InnerText = "0";
                XmlElement xmlnode4 = doc.CreateElement("NumberOfStandardDeviations");
                xmlnode4.InnerText = "2";
                stretchnode.AppendChild(xmlnode1);
                stretchnode.AppendChild(xmlnode2);
                stretchnode.AppendChild(xmlnode3);
                stretchnode.AppendChild(xmlnode4);

                //窗口打开
                stretchFunction stretchFunction = new stretchFunction(stretchnode);
                stretchFunction.ShowDialog();
                //得到XML中的Element
                XmlNode node = stretchFunction.GetXMLNode();
                
                XmlElement temp = node as XmlElement;
                DataRow dataRow = dataTable.NewRow();
                //dataRow[0] = dataTable.Columns.Count;
                //现在是会报错 因为没有传出来temp
                dataRow[0] = temp.GetAttribute("name");
                dataRow[1] = temp.GetAttribute("description");
                m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                //对doc进行修改
                InsertDoc(doc, node, m_hitRowindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void nDVIFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");

                XmlElement ndvinode = doc.CreateElement("Function");
                //xn.AppendChild(ndvinode);
                ndvinode.SetAttribute("name", "ndviFunction");
                ndvinode.SetAttribute("description", "A raster NDVI function.");
                XmlElement xmlnode1 = doc.CreateElement("InfraredBandID");
                xmlnode1.InnerText = "3";
                XmlElement xmlnode2 = doc.CreateElement("VisibleBandID");
                xmlnode2.InnerText = "2";
                ndvinode.AppendChild(xmlnode1);
                ndvinode.AppendChild(xmlnode2);
                //窗口打开

                ndviFunction NDVIFunction = new ndviFunction(m_raster, ndvinode);
                NDVIFunction.ShowDialog();

                //得到XML中的Element
                XmlNode node = NDVIFunction.GetXMLNode();
                
                XmlElement temp = node as XmlElement;
                DataRow dataRow = dataTable.NewRow();
                //dataRow[0] = dataTable.Columns.Count;
                //现在是会报错 因为没有传出来temp
                dataRow[0] = temp.GetAttribute("name");
                dataRow[1] = temp.GetAttribute("description");
                m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                //对doc进行修改
                InsertDoc(doc, node, m_hitRowindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void panSharpenFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlElement xelKey = doc.CreateElement("Function");
                XmlAttribute xelName = doc.CreateAttribute("name");
                xelName.InnerText = "Pansharpening";
                xelKey.SetAttributeNode(xelName);

                XmlAttribute xelDesp = doc.CreateAttribute("description");
                xelDesp.InnerText = "Enhances the spatial resolution of a multiband image by fusing it with a higher-resolution panchromatic image.";
                xelKey.SetAttributeNode(xelDesp);

                XmlElement xelPanImage = doc.CreateElement("PanImage");
                xelPanImage.InnerText = "default";
                xelKey.AppendChild(xelPanImage);

                XmlElement xelType = doc.CreateElement("PansharpeningType");
                xelType.InnerText = "0";
                xelKey.AppendChild(xelType);

                XmlElement xelRed = doc.CreateElement("Red");
                xelRed.InnerText = "0.166";
                xelKey.AppendChild(xelRed);

                XmlElement xelGreen = doc.CreateElement("Green");
                xelGreen.InnerText = "0.166";
                xelKey.AppendChild(xelGreen);

                XmlElement xelBlue = doc.CreateElement("Blue");
                xelBlue.InnerText = "0.166";
                xelKey.AppendChild(xelBlue);

                XmlElement xelIR = doc.CreateElement("Infra");
                xelIR.InnerText = "0.5";
                xelKey.AppendChild(xelIR);

                XmlElement xelOutput = doc.CreateElement("Output");
                xelOutput.InnerText = "_pansharpen";
                xelKey.AppendChild(xelOutput);

                //窗口打开
                panSharpenFunction panSharpenFunction = new panSharpenFunction(xelKey);
                panSharpenFunction.ShowDialog();
                //得到XML中的Element
                XmlNode node = panSharpenFunction.GetXMLNode();

                XmlElement temp = node as XmlElement;
                DataRow dataRow = dataTable.NewRow();
                //dataRow[0] = dataTable.Columns.Count;
               
                dataRow[0] = temp.GetAttribute("name");
                dataRow[1] = temp.GetAttribute("description");
                m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                //对doc进行修改
                InsertDoc(doc, node, m_hitRowindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void convolutionFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                XmlElement xelKey = doc.CreateElement("Function");
                XmlAttribute xelName = doc.CreateAttribute("name");
                xelName.InnerText = "Convolution";
                xelKey.SetAttributeNode(xelName);

                XmlAttribute xelDesp = doc.CreateAttribute("description");
                xelDesp.InnerText = "Performs filtering on the pixel values in a raster, which can be used for sharpening an image, blurring an image, detecting edges within an image, or other kernel-based enhancements.";
                xelKey.SetAttributeNode(xelDesp);

                XmlElement xelType = doc.CreateElement("Type");
                xelType.InnerText = "0";
                xelKey.AppendChild(xelType);

                XmlElement xelOutput = doc.CreateElement("Output");
                xelOutput.InnerText = "_conv";
                xelKey.AppendChild(xelOutput); 
                
                //窗口打开
                convolutionFunction convolutionFunction = new convolutionFunction(xelKey);
                convolutionFunction.ShowDialog();
                //得到XML中的Element
                XmlNode node = convolutionFunction.GetXMLNode();

                XmlElement temp = node as XmlElement;
                DataRow dataRow = dataTable.NewRow();
                //dataRow[0] = dataTable.Columns.Count;
                //现在是会报错 因为没有传出来temp
                dataRow[0] = temp.GetAttribute("name");
                dataRow[1] = temp.GetAttribute("description");
                m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                //对doc进行修改
                InsertDoc(doc, node, m_hitRowindex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(m_hitRowindex);
            DeleteDoc(doc, m_hitRowindex);
        }

        //导入XML文件
        private void miLoadXML_Click(object sender, EventArgs e)
        {
            try
            {
                //保证每次加入时dataTable能刷新
                dataTable.Clear();
                //将XML文件读入
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Rft.XML (*.xml)|*.xml";
                openFileDialog.Title = "打开rft.xml文件";
                openFileDialog.Multiselect = false;
                filename = "";
                //如果对话框已成功选择文件，将文件路径信息填写到输入框内
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog.FileName;
                }
                //存储XML文件信息
                doc.Load(filename);
                XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");
                XmlNodeList xnl = xn.ChildNodes;

               // int i = 1;
                foreach (XmlNode xn1 in xnl)
                {
                    if (xn1 is XmlComment)
                        continue;
                    //else if (xn1.
                    XmlElement xe = (XmlElement)xn1;
                    if (xe.Name == "Function")
                    {
                        //RasterFunction func = new RasterFunction();
                        //func.name = xe.GetAttribute("name");
                        //func.description = xe.GetAttribute("description");
                        DataRow dataRow = dataTable.NewRow();
                        //dataRow[0] = i;
                        dataRow[0] = xe.GetAttribute("name");
                        dataRow[1] = xe.GetAttribute("description");
                        dataTable.Rows.Add(dataRow);
                        //i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }        
        }
        
        //最简单的save函数
        private void miExportXML_Click(object sender, EventArgs e)
        {
            doc.Save("test.xml");
        }

        //对于doc进行loop，依次进行Init()函数
        private void miFinish_Click(object sender, EventArgs e)
        {
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("编辑器不能为空");
            }
            else
            {
                if (m_checkedindex == -1)
                {
                    m_checkedindex = dataTable.Rows.Count-1;
                }
                XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");
                XmlNodeList xnl = xn.ChildNodes;
                int i = 0;
                foreach (XmlNode xn1 in xnl)
                {
                    if (xn1 is XmlComment)
                        continue;
                    //else if (xn1.
                    if (i == (m_checkedindex+1))
                        break;
                    XmlElement xe = (XmlElement)xn1;
                    if (xe.Name == "Function")
                    {
                         switch (xe.GetAttribute("name"))
                         {
                             case "Pansharpening":
                                 {
                                     panSharpenFunction panSharpenFunction = new panSharpenFunction(m_raster,xn1);
                                     panSharpenFunction.Init();
                                     m_raster = panSharpenFunction.GetRaster();
                                     break;
                                 }
                             case "Convolution":
                                 {
                                     convolutionFunction convolutionFunction = new convolutionFunction(m_raster,xn1);
                                     convolutionFunction.underInit();
                                     m_raster = convolutionFunction.GetRaster();
                                     break;
                                 }
                             case "Slope":
                                 {
                                     slopeFunction slopeFunction = new slopeFunction(m_raster,xn1);
                                     slopeFunction.Init();
                                     m_raster = slopeFunction.GetRaster();
                                     break;
                                 }
                             case "Aspect":
                                 {
                                     aspectFunction aspectFunction = new aspectFunction(m_raster,xn1);
                                     aspectFunction.Init();
                                     m_raster = aspectFunction.GetRaster();
                                     break;
                                 }
                             case "stretchFunction":
                                 {
                                     stretchFunction stretchFunction = new stretchFunction(m_raster,xn1);
                                     stretchFunction.Init();
                                     m_raster = stretchFunction.GetRaster();
                                     break;
                                 }
                             case "ndviFunction":
                                 {
                                     ndviFunction ndviFunction = new ndviFunction(m_raster,xn1);
                                     ndviFunction.Init();
                                     //ndviFunction.UnderInit();
                                     m_raster = ndviFunction.GetRaster();
                                     break;
                                 }
                             case "clipFunction":
                                 {
                                     clipFunction clipFunction = new clipFunction(m_raster,xn1);
                                     clipFunction.Init();
                                     m_raster = clipFunction.GetRaster();
                                     break;
                                 }
                             case "hillshadeFunction":
                                 {
                                     hillshadeFunction hillshadeFunction = new hillshadeFunction(m_raster,xn1);
                                     hillshadeFunction.Init();
                                     m_raster = hillshadeFunction.GetRaster();
                                     break;
                                 }
                             default:
                                 break;
                         }
                         i++;
                    }
                }
            }
            IsFinished = true;
        }

        //对于document进行添加
        public void InsertDoc(XmlDocument doc, XmlNode node,int index)
        {
            try
            {
                //如果是对于doc进行第一次新建，则
                //if (doc.SelectSingleNode("XmlRasterFunctionTemplate") == null)
                //{
                //    XmlElement root = doc.CreateElement("XmlRasterFunctionTemplate");
                //    doc.AppendChild(root);
                //    XmlElement name = doc.CreateElement("Name");
                //    name.InnerText = "Raster Funciton Template";
                //    XmlElement descrip = doc.CreateElement("Description");
                //    descrip.InnerText = "A raster function template.";
                //    root.AppendChild(node);
                //}
               // else
                //{
                    XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");
                    XmlNodeList xnl = xn.ChildNodes;
                    xn.InsertAfter(node, xnl.Item(1 + index));
               // }
            }
                catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //对于document进行编辑
        public void EditDoc(XmlDocument doc, XmlNode node, int index)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");
                XmlNodeList xnl = xn.ChildNodes;
                xn.ReplaceChild(node,xnl.Item(2 + index));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //对于document进行删除
        public void DeleteDoc(XmlDocument doc, int index)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");
                XmlNodeList xnl = xn.ChildNodes;
                xn.RemoveChild(xnl.Item(2 + index));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }          
        }
        public IRaster GetRaster()
        {
            return m_raster;
        }

        private void miApply_Click(object sender, EventArgs e)
        {
            if (IsFinished)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("请先确定编辑器内容");
            }
        }
    }
}
