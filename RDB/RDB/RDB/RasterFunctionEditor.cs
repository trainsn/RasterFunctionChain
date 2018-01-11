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
        //成员变量的设置
        IRaster m_raster;   //传入获取的栅格
        string filename = "";   //记录导入的Xml文件路径
        DataTable dataTable = InitialTable();   //静态变量DataTable的初始化
        int m_hitRowindex;  //记录单击时的索引
        int m_doublehitRowindex;  //记录双击时的索引
        int m_checkedindex = -1;  //用来确定函数的中间步骤
        XmlDocument doc = new XmlDocument(); //用于存储Xml文件
        public bool IsFinished = false;   //决定是否对栅格进行函数操作

        //构造函数，将栅格传入
        public RasterFunctionEditor(IRaster raster)
        {
            InitializeComponent();
            DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
            check.HeaderText = "IsChecked";
            //check.DataPropertyName = "IsChecked";
            dataGridView1.Columns.Add(check);
            dataGridView1.DataSource = dataTable;
            //使得列的布局可以完全充满表
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //使得DatagridView的默认行消失
            dataGridView1.AllowUserToAddRows = false;
            m_raster = raster;
            InitialDoc();
        }

        //对于Doc的初始化函数
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

        //对于表格的初始化 将表格作为静态变量
        static DataTable  InitialTable()
        {
            DataTable dataTable = new DataTable();
            DataColumn dataColumn = new DataColumn();//创建列

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "RasterName";//设置第一列
            dataColumn.DataType = System.Type.GetType("System.String");
            dataTable.Columns.Add(dataColumn);

            dataColumn = new DataColumn();
            dataColumn.ColumnName = "Description";//设置第二列
            dataColumn.DataType = System.Type.GetType("System.String");
            dataTable.Columns.Add(dataColumn);

            return dataTable;
        }

        //对于DatagridView的单击函数
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //右键时的情况
                if (e.Button == MouseButtons.Right)
                {
                    miDelete.Enabled = true;
                    DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                    this.contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
                    m_hitRowindex = hit.RowIndex;
                    //如果没有行的话 则不能进行删除操作
                    if (m_hitRowindex < 0)
                    {
                        miDelete.Enabled = false;
                    }
                }
                //左键时的情况 这里用于处理中间结果
                else
                {
                    DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                    if (hit.ColumnIndex == 0)
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            dataGridView1.Rows[i].Cells[0].Value = false;
                        }
                        //对于第一列的Checked状态进行更新
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

        //对于DatagridView的双击函数
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
                            //过滤XML文件中的注释
                            if (xn1 is XmlComment)
                                continue;
                            //else if (xn1.
                            XmlElement xe = (XmlElement)xn1;
                            //利用根名来找到函数根节点
                            if (xe.Name == "Function")
                            {
                                if (i == m_doublehitRowindex)
                                {
                                    //对于不同函数名打开不同窗口
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

        //实现裁剪函数的插入
        private void clipFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //对于裁剪函数对应的XmlNode进行初始化
                XmlElement xmlnode1 = doc.CreateElement("Function");
                xmlnode1.SetAttribute("name", "clipFunction");
                xmlnode1.SetAttribute("description", "A raster clip function.");
                xmlnode1.AppendChild(doc.CreateElement("inputSHPPath", ""));

                //打开窗口，待窗口函数执行结束后函数再继续执行
                clipFunction clipFunction = new clipFunction(xmlnode1);
                clipFunction.ShowDialog();
                if (clipFunction.isFinished)
                {
                    //得到XML中的Element
                    XmlNode node = clipFunction.GetXMLNode();
                    XmlElement temp = node as XmlElement;
                    //对DataTable进行修改
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = temp.GetAttribute("name");
                    dataRow[1] = temp.GetAttribute("description");
                    //更正m_hitRowIndex
                    m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                    dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                    //对doc进行修改
                    InsertDoc(doc, node, m_hitRowindex);
                }           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //实现坡度函数的插入
        private void slopeFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //对于坡度函数对应的XmlNode进行初始化赋值
                XmlElement xmlEleSlope = doc.CreateElement("Function");
                xmlEleSlope.SetAttribute("name", "Slope");
                xmlEleSlope.SetAttribute("description", "Slope represents the rate of change of elevation for each digital elevation model (DEM) cell. It's the first derivative of a DEM.");
                XmlElement xmlEleZfactor = doc.CreateElement("Zfactor");               
                xmlEleZfactor.InnerText = (1.0/11111).ToString();
                xmlEleSlope.AppendChild(xmlEleZfactor);

                //打开窗口，待窗口函数执行结束后函数再继续执行
                slopeFunction slopeFunction = new slopeFunction(xmlEleSlope);
                slopeFunction.ShowDialog();
                if (slopeFunction.isFinished)
                {
                    //得到XML中的Element
                    XmlNode node = slopeFunction.GetXMLNode();
                    XmlElement temp = node as XmlElement;
                    //对DataTable进行修改
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = temp.GetAttribute("name");
                    dataRow[1] = temp.GetAttribute("description");
                    m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                    dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                    //对doc进行修改
                    InsertDoc(doc, node, m_hitRowindex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //实现坡向函数的插入
        private void aspectFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //对于坡向函数对应的XmlNode进行初始化赋值
                XmlElement xmlEleSlope = doc.CreateElement("Function");
                xmlEleSlope.SetAttribute("name", "Aspect");
                xmlEleSlope.SetAttribute("description", "Aspect identifies the downslope direction of the maximum rate of change in value from each cell to its neighbors. Aspect can be thought of as the slope direction. The values of the output raster will be the compass direction of the aspect.");

                aspectFunction aspectFunction = new aspectFunction(xmlEleSlope);
                //得到XML中的Element
                XmlNode node = aspectFunction.GetXMLNode();             
                XmlElement temp = node as XmlElement;
                DataRow dataRow = dataTable.NewRow();
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

        //实现山体阴影函数的插入
        private void hillshadeFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //对于山体阴影函数所对应的XmlNode进行初始化赋值
                XmlElement xmlnode1 = doc.CreateElement("Function");
                xmlnode1.SetAttribute("name", "hillshadeFunction");
                xmlnode1.SetAttribute("description", "A raster hillshade function.");
                XmlNode node1 = doc.CreateElement("Azimuth");
                node1.InnerText = "50";
                xmlnode1.AppendChild(node1);
                XmlNode node2 = doc.CreateElement("ZFactor");
                node2.InnerText = (1/11111.0).ToString();
                xmlnode1.AppendChild(node2);

                //打开窗口，待窗口函数执行结束后函数再继续执行
                hillshadeFunction hillshadeFunction = new hillshadeFunction(xmlnode1);
                hillshadeFunction.ShowDialog();
                if (hillshadeFunction.isFinished)
                {
                    //得到XML中的Element
                    XmlNode node = hillshadeFunction.GetXMLNode();

                    XmlElement temp = node as XmlElement;
                    //更新DataTable
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = temp.GetAttribute("name");
                    dataRow[1] = temp.GetAttribute("description");
                    m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                    dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                    //对doc进行修改
                    InsertDoc(doc, node, m_hitRowindex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //对于拉伸函数的插入
        private void stretchFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //对于拉伸函数所对应的XmlNode进行初始化赋值
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

                //打开窗口，待窗口函数执行结束后函数再继续执行
                stretchFunction stretchFunction = new stretchFunction(stretchnode);
                stretchFunction.ShowDialog();
                if (stretchFunction.IsFinished)
                {
                    //得到XML中的Element
                    XmlNode node = stretchFunction.GetXMLNode();

                    XmlElement temp = node as XmlElement;
                    //对DataTable进行更新
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = temp.GetAttribute("name");
                    dataRow[1] = temp.GetAttribute("description");
                    m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                    dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                    //对doc进行修改
                    InsertDoc(doc, node, m_hitRowindex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //对于NDVI函数的插入
        private void nDVIFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //对于NDVI函数所对应的XmlNode进行初始化赋值
                XmlElement ndvinode = doc.CreateElement("Function");
                ndvinode.SetAttribute("name", "ndviFunction");
                ndvinode.SetAttribute("description", "A raster NDVI function.");
                XmlElement xmlnode1 = doc.CreateElement("InfraredBandID");
                xmlnode1.InnerText = "3";
                XmlElement xmlnode2 = doc.CreateElement("VisibleBandID");
                xmlnode2.InnerText = "2";
                ndvinode.AppendChild(xmlnode1);
                ndvinode.AppendChild(xmlnode2);

                //打开窗口，待窗口函数执行结束后函数再继续执行
                ndviFunction NDVIFunction = new ndviFunction(m_raster, ndvinode);
                NDVIFunction.ShowDialog();

                if (NDVIFunction.IsFinished)
                {
                    //得到XML中的Element
                    XmlNode node = NDVIFunction.GetXMLNode();
                    XmlElement temp = node as XmlElement;
                    //对于DataTable进行修改
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = temp.GetAttribute("name");
                    dataRow[1] = temp.GetAttribute("description");
                    m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                    dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                    //对doc进行修改
                    InsertDoc(doc, node, m_hitRowindex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //对于融合函数的插入
        private void panSharpenFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //对于融合函数所对应的XmlNode的初始化赋值
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

                //打开窗口，待窗口函数执行结束后函数再继续执行
                panSharpenFunction panSharpenFunction = new panSharpenFunction(xelKey);
                panSharpenFunction.ShowDialog();
                if (panSharpenFunction.IsFinished)
                {
                    //得到XML中的Element
                    XmlNode node = panSharpenFunction.GetXMLNode();
                    XmlElement temp = node as XmlElement;
                    //对于DataTable进行修改
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = temp.GetAttribute("name");
                    dataRow[1] = temp.GetAttribute("description");
                    m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                    dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                    //对doc进行修改
                    InsertDoc(doc, node, m_hitRowindex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //对于卷积函数的插入
        private void convolutionFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //对于卷积函数对应的XmlNode进行初始化赋值
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

                //打开窗口，待窗口函数执行结束后函数再继续执行
                convolutionFunction convolutionFunction = new convolutionFunction(xelKey);
                convolutionFunction.ShowDialog();
                if (convolutionFunction.IsFinished)
                {
                    //得到XML中的Element
                    XmlNode node = convolutionFunction.GetXMLNode();
                    XmlElement temp = node as XmlElement;
                    //对于DataTable进行修改
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = temp.GetAttribute("name");
                    dataRow[1] = temp.GetAttribute("description");
                    m_hitRowindex = (m_hitRowindex < 0) ? dataTable.Rows.Count : m_hitRowindex;
                    dataTable.Rows.InsertAt(dataRow, m_hitRowindex);
                    //对doc进行修改
                    InsertDoc(doc, node, m_hitRowindex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //将选中行在DataTable和Doc中删去
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
                //保证每次加入时dataTable能刷新
                dataTable.Clear();
                //存储XML文件信息
                doc.Load(filename);
                XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");
                XmlNodeList xnl = xn.ChildNodes;

                foreach (XmlNode xn1 in xnl)
                {
                    //跳过文件中的注释
                    if (xn1 is XmlComment)
                        continue;
                    //else if (xn1.
                    XmlElement xe = (XmlElement)xn1;
                    if (xe.Name == "Function")
                    {
                        //将函数按照顺序加入DataTable中
                        DataRow dataRow = dataTable.NewRow();
                        dataRow[0] = xe.GetAttribute("name");
                        dataRow[1] = xe.GetAttribute("description");
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }        
        }
        
        //将XML文件输出
        private void miExportXML_Click(object sender, EventArgs e)
        {
            doc.Save("test.xml");
            MessageBox.Show("保存成功");
        }

        //对于document进行添加
        public void InsertDoc(XmlDocument doc, XmlNode node, int index)
        {
            try
            {
                XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");
                XmlNodeList xnl = xn.ChildNodes;
                int i = 0;
                foreach (XmlNode xn1 in xnl)
                {
                    if (xn1 is XmlComment)
                        continue;

                    XmlElement xe = (XmlElement)xn1;
                    if (xe.Name == "Function")
                    {
                        if (i == index-1)
                        {
                            xn.InsertAfter(node, xn1);
                            break;
                        }
                        i++;
                    }
                }
                if (index == 0)
                {
                    xn.InsertAfter(node, xnl.Item(1));
                }

            }
            catch (Exception ex)
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
                int i=0;
                foreach (XmlNode xn1 in xnl)
                {
                    if (xn1 is XmlComment)
                        continue;
                    XmlElement xe = (XmlElement)xn1;
                    if (xe.Name == "Function")
                    {
                        if (i == index)
                        {
                            xn.ReplaceChild(node, xn1);
                            break;
                        }
                        i++;
                    }
                }
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
                int i = 0;
                foreach (XmlNode xn1 in xnl)
                {
                    if (xn1 is XmlComment)
                        continue;
                    XmlElement xe = (XmlElement)xn1;
                    if (xe.Name == "Function")
                    {
                        if (i == index)
                        {
                            xn.RemoveChild(xn1);
                            break;
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //对函数模板进行应用
        private void miApply_Click(object sender, EventArgs e)
        {
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("编辑器不能为空");
            }
            else
            {
                //确定Checked的位置
                if (m_checkedindex == -1)
                {
                    m_checkedindex = dataTable.Rows.Count - 1;
                }
                //按照doc的顺序对函数进行遍历执行
                XmlNode xn = doc.SelectSingleNode("XmlRasterFunctionTemplate");
                XmlNodeList xnl = xn.ChildNodes;
                int i = 0;
                foreach (XmlNode xn1 in xnl)
                {
                    if (xn1 is XmlComment)
                        continue;
                    //else if (xn1.
                    if (i == (m_checkedindex + 1))
                        break;
                    XmlElement xe = (XmlElement)xn1;
                    if (xe.Name == "Function")
                    {
                        switch (xe.GetAttribute("name"))
                        {
                            case "Pansharpening":
                                {
                                    panSharpenFunction panSharpenFunction = new panSharpenFunction(m_raster, xn1);
                                    panSharpenFunction.Init();
                                    m_raster = panSharpenFunction.GetRaster();
                                    break;
                                }
                            case "Convolution":
                                {
                                    convolutionFunction convolutionFunction = new convolutionFunction(m_raster, xn1);
                                    convolutionFunction.Init();
                                    m_raster = convolutionFunction.GetRaster();
                                    break;
                                }
                            case "Slope":
                                {
                                    slopeFunction slopeFunction = new slopeFunction(m_raster, xn1);
                                    slopeFunction.Init();
                                    m_raster = slopeFunction.GetRaster();
                                    break;
                                }
                            case "Aspect":
                                {
                                    aspectFunction aspectFunction = new aspectFunction(m_raster, xn1);
                                    aspectFunction.Init();
                                    m_raster = aspectFunction.GetRaster();
                                    break;
                                }
                            case "stretchFunction":
                                {
                                    stretchFunction stretchFunction = new stretchFunction(m_raster, xn1);
                                    stretchFunction.Init();
                                    m_raster = stretchFunction.GetRaster();
                                    break;
                                }
                            case "ndviFunction":
                                {
                                    ndviFunction ndviFunction = new ndviFunction(m_raster, xn1);
                                    ndviFunction.Init();
                                    m_raster = ndviFunction.GetRaster();
                                    break;
                                }
                            case "clipFunction":
                                {
                                    clipFunction clipFunction = new clipFunction(m_raster, xn1);
                                    clipFunction.Init();
                                    m_raster = clipFunction.GetRaster();
                                    break;
                                }
                            case "hillshadeFunction":
                                {
                                    hillshadeFunction hillshadeFunction = new hillshadeFunction(m_raster, xn1);
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

        //将模板操作后的栅格返回给主窗口
        private void miFinish_Click(object sender, EventArgs e)
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

        //返回成员变量m_raster
        public IRaster GetRaster()
        {
            return m_raster;
        }  

    }
}
