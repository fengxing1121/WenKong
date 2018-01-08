using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TemperatureControl2
{
    public partial class FormAutoSet : Form
    {
        // 设备
        Device.Devices devicesAll;
        /// <summary>
        /// 自动控制流程的温度点 - 仅用于显示
        /// </summary>
        BindingList<ParamShow> BList = new BindingList<ParamShow>();
        /// <summary>
        /// 温度点参数列表
        /// </summary>
        List<TempParam> paramList = new List<TempParam>();

        /// <summary>
        /// 是否已开始自动控温流程
        /// </summary>
        bool flowStart = false;


        private TextBox tx = null;
        private TextBox[] paramMTextBox = new TextBox[7];
        private TextBox[] paramSTextBox = new TextBox[7];
        private float[] paramMCache = new float[7];
        private float[] paramSCache = new float[7];

        public FormAutoSet(Device.Devices dev)
        {
            InitializeComponent();
            devicesAll = dev;
            paramMTextBox[0] = textBox_tpSetM;
            paramMTextBox[1] = textBox_tpAdjustM;
            paramMTextBox[2] = textBox_advanceM;
            paramMTextBox[3] = textBox_fuzzyM;
            paramMTextBox[4] = textBox_ratioM;
            paramMTextBox[5] = textBox_integM;
            paramMTextBox[6] = textBox_powerM;

            paramSTextBox[0] = textBox_tpSetS;
            paramSTextBox[1] = textBox_tpAdjustS;
            paramSTextBox[2] = textBox_advanceS;
            paramSTextBox[3] = textBox_fuzzyS;
            paramSTextBox[4] = textBox_ratioS;
            paramSTextBox[5] = textBox_integS;
            paramSTextBox[6] = textBox_powerS;

        }

        private void updateDataGridView()
        {
            // 排序
            if(devicesAll.sort == "ascend")
            {
                // 升序
                paramList.Sort();
            }
            else
            {
                // 降序
                paramList.Sort();
                paramList.Reverse();
            }

            // 将温度点数值显示在 DataGridView 中
            BList.Clear();
            for (int i = 0; i < paramList.Count; i++)
            {
                ParamShow ps1 = new ParamShow();
                ps1._index = i + 1;
                ps1.tpName = "主槽";
                if(paramList[i].finished == true)
                    ps1.editSave = "已测量";
                else
                    ps1.editSave = "未测量";
                paramList[i].paramM.CopyTo(ps1.param, 0);
                BList.Add(ps1);
                ParamShow ps2 = new ParamShow();
                ps2._index = i + 1;
                ps2.tpName = "辅槽";
                ps2.editSave = "参数设置";
                paramList[i].paramS.CopyTo(ps2.param, 0);
                BList.Add(ps2);
            }
            dataGridView1.ClearSelection();
        }

        // 窗体载入函数
        private void FormAutoSet_Load(object sender, EventArgs e)
        {
            index.DataPropertyName = "Index";
            tpName.DataPropertyName = "TpName";
            tpSet.DataPropertyName = "TemptSet";
            tpAdjust.DataPropertyName = "TempAdjust";
            advance.DataPropertyName = "Advance";
            fuzzy.DataPropertyName = "Fuzzy";
            ratio.DataPropertyName = "Ratio";
            integration.DataPropertyName = "Integration";
            power.DataPropertyName = "Power";
            edit.DataPropertyName = "Edit";


            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = BList;

#if true
            lock(this.devicesAll.stepLocker)
            {
                // 判断温控列表中是否有温度点
                if (devicesAll.temperaturePointList.Count != 0)
                {
                    // 将 devicesAll.temperaturePointList 中未测量完成的温度点
                    foreach (var st in devicesAll.temperaturePointList)
                    {
                        TempParam ts = new TempParam();
                        st.paramM.CopyTo(ts.paramM, 0);
                        st.paramS.CopyTo(ts.paramS, 0);
                        ts.finished = st.finished;
                        paramList.Add(ts);
                    }
                }

                // 判断是否已开始自动控温流程
                if (this.devicesAll.autoStart == true)
                {
                    // 正在自动控温
                    this.checkBox_start.Checked = true;
                    this.checkBox_start.Text = "停止";
                    flowStart = true;

                    // 判断是否已设定完成实验测量后关机
                    this.checkBox_shutDown.Checked = devicesAll.shutDownComputer;
                    this.checkBox_shutDown.Enabled = false;
                }
                else
                {
                    // 未开始自动控温
                    this.checkBox_start.Checked = false;
                    this.checkBox_start.Text = "开始";
                    flowStart = false;

                    // 判断是否已设定完成实验测量后关机
                    this.checkBox_shutDown.Checked = true;
                    this.checkBox_shutDown.Enabled = true;
                }
            }
            
#endif

#if false
            FileStream fm = File.Open(@"./params.cache", FileMode.Open, FileAccess.Read);
            
            if(fm != null)
            {
                fm.Close();
                string[] lines = File.ReadAllLines(@"./params.cache", Encoding.UTF8);
                for(int i =0;i<lines.Length;i++)
                {
                    // 主槽参数
                    string line1 = lines[i];
                    TempParam ts = new TempParam();
                    i++;
                    string[] parmM = line1.Split(' ');
                    if(parmM.Length == 7)
                    {
                        float vl;
                        if (float.TryParse(parmM[0], out vl)) ts.paramM[0] = vl;
                        else break;
                        if (float.TryParse(parmM[1], out vl)) ts.paramM[1] = vl;
                        else break;
                        if (float.TryParse(parmM[2], out vl)) ts.paramM[2] = vl;
                        else break;
                        if (float.TryParse(parmM[3], out vl)) ts.paramM[3] = vl;
                        else break;
                        if (float.TryParse(parmM[4], out vl)) ts.paramM[4] = vl;
                        else break;
                        if (float.TryParse(parmM[5], out vl)) ts.paramM[5] = vl;
                        else break;
                        if (float.TryParse(parmM[6], out vl)) ts.paramM[6] = vl;
                        else break;
                    }
                    else
                    {
                        break;
                    }

                    // 辅槽参数
                    if(i>=lines.Length) { break; }
                    string line2 = lines[i];
                    string[] parmS = line2.Split(' ');
                    if (parmS.Length == 7)
                    {
                        float vl;
                        if (float.TryParse(parmS[0], out vl)) ts.paramS[0] = vl;
                        else break;
                        if (float.TryParse(parmS[1], out vl)) ts.paramS[1] = vl;
                        else break;
                        if (float.TryParse(parmS[2], out vl)) ts.paramS[2] = vl;
                        else break;
                        if (float.TryParse(parmS[3], out vl)) ts.paramS[3] = vl;
                        else break;
                        if (float.TryParse(parmS[4], out vl)) ts.paramS[4] = vl;
                        else break;
                        if (float.TryParse(parmS[5], out vl)) ts.paramS[5] = vl;
                        else break;
                        if (float.TryParse(parmS[6], out vl)) ts.paramS[6] = vl;
                        else break;
                    }
                    else
                    {
                        break;
                    }

                    // 装入列表中
                    paramList.Add(ts);
                }
                
            }
#endif

            // 更新表格显示
            updateDataGridView();

            // 输入窗口的默认数值
            devicesAll.tpDeviceM.tpParam.CopyTo(paramMCache, 0);
            devicesAll.tpDeviceS.tpParam.CopyTo(paramSCache, 0);
            for (int i = 1;i<7;i++)
            {
                if(i<3)
                {
                    paramMTextBox[i].Text = paramMCache[i].ToString("0.000");
                    paramSTextBox[i].Text = paramSCache[i].ToString("0.000");
                } 
                else
                {
                    paramMTextBox[i].Text = paramMCache[i].ToString("0");
                    paramSTextBox[i].Text = paramSCache[i].ToString("0");
                }
            }

        }


        /// <summary>
        /// 开始 / 暂停自动控温流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_start_Click(object sender, EventArgs e)
        {
            if(this.checkBox_start.Checked == true)
            {
                // 如果列表中没有温度点，则不开始控温流程
                if (paramList.Count == 0)
                {
                    flowStart = false;
                    this.checkBox_start.Checked = false;
                    this.checkBox_start.Text = "开始";
                    return;
                }


                // 将温度点数据保存到缓存中
                try
                {
                    // 清空原有文件
                    FileStream fs = File.Open(@"./params.cache", FileMode.Truncate, FileAccess.ReadWrite);
                    if(fs!=null) fs.Close();

                    StreamWriter sw = new StreamWriter(@"./params.cache", true, Encoding.UTF8);
                    for (int i = 0; i < paramList.Count; i++)
                    {
                        for (int j = 0; j < paramList[i].paramM.Length - 1; j++)
                        {
                            sw.Write(paramList[i].paramM[j].ToString() + " ");
                        }
                        sw.Write(paramList[i].paramM[paramList[i].paramM.Length - 1].ToString());
                        sw.WriteLine();
                        for (int j = 0; j < paramList[i].paramM.Length - 1; j++)
                        {
                            sw.Write(paramList[i].paramS[j].ToString() + " ");
                        }
                        sw.Write(paramList[i].paramS[paramList[i].paramM.Length - 1].ToString());
                        sw.WriteLine();
                    }
                    sw.Flush();
                    fs.Close();
                }
                catch(Exception ex)
                {
                    Utils.Logger.Sys("存储温度点缓存时，发生异常！");
                    MessageBox.Show("存储温度点缓存时，发生异常！");
                    return;
                }

                // 开始自动控温流程
                lock (devicesAll.stepLocker)
                {
                    // 清空原有的温度点数据
                    devicesAll.temperaturePointList.Clear();
                    // 将实验流程数据写入 Devices 类中
                    for (int i = 0; i < paramList.Count; i++)
                    {
                        // deviceAll.controlFlowList 中的 StateFlow.flowState 必须设置为 Undefine
                        // 只保存温度点，不再保存
                        Device.Devices.TemperaturePoint tp = new Device.Devices.TemperaturePoint();
                        paramList[i].paramM.CopyTo(tp.paramM, 0);
                        paramList[i].paramS.CopyTo(tp.paramS, 0);
                        tp.finished = paramList[i].finished;
                        devicesAll.temperaturePointList.Add(tp);
                    }

                    // 设置当前控温流程状态 - 开始
                    this.devicesAll.currentState.flowState = Device.Devices.State.Start;
                    this.devicesAll.currentState.stateChanged = true;

                    // 自动控温流程标志位置位
                    this.devicesAll.autoStart = true;

                    // 实验完成后是否关闭计算机
                    devicesAll.shutDownComputer = this.checkBox_shutDown.Checked;
                    this.checkBox_shutDown.Enabled = false;
                }

                Utils.Logger.Op("点击自动控温设置界面 开始 按键，开始执行自动控温流程...");
                Utils.Logger.Sys("点击自动控温设置界面 开始 按键，开始执行自动控温流程...");

                Utils.Logger.Op("设定的温度点有：");
                Utils.Logger.Sys("设定的温度点有：");

                foreach (var st in paramList)
                {
                    Utils.Logger.Op(st.paramM[0].ToString("0.0000"));
                    Utils.Logger.Sys(st.paramM[0].ToString("0.0000"));
                    Utils.Logger.Op("是否已测量：" + st.finished.ToString());
                    Utils.Logger.Sys("是否已测量：" + st.finished.ToString());
                }

                // 自动控温流程已开始
                flowStart = true;
                checkBox_start.Text = "停止";

            }
            else
            {
                // 暂停自动控温流程
                lock(this.devicesAll.stepLocker)
                {
                    // 控温流程当前工作状态 - 停止
                    this.devicesAll.currentState.flowState = Device.Devices.State.Stop;
                    this.devicesAll.currentState.stateChanged = true;

                    if (devicesAll.temperaturePointList.Count != 0)
                    {
                        paramList.Clear();
                        // 将 devicesAll.temperaturePointList 中未测量完成的温度点
                        foreach (var st in devicesAll.temperaturePointList)
                        {
                            TempParam ts = new TempParam();
                            st.paramM.CopyTo(ts.paramM, 0);
                            st.paramS.CopyTo(ts.paramS, 0);
                            ts.finished = st.finished;
                            paramList.Add(ts);
                        }
                    }

                    // 取消 - 实验完成后不关闭计算机
                    devicesAll.shutDownComputer = false;
                    this.checkBox_shutDown.Enabled = true;
                }

                // 更新表格显示
                updateDataGridView();

                // 自动控温流程已停止
                flowStart = false;
                checkBox_start.Text = "开始";
            }

            return;
        }

#if false
        // 开始自动控温流程
        // wghou
        // 已经不用的
        private void button_start_Click(object sender, EventArgs e)
        {
            lock(devicesAll.stepLocker)
            {
                devicesAll.temperaturePointList.Clear();
                // 将实验流程数据写入 Devices 类中
                for(int i = 0;i< paramList.Count;i++)
                {
                    // deviceAll.controlFlowList 中的 StateFlow.flowState 必须设置为 Undefine
                    // 只保存温度点，不再保存
                    Device.Devices.TemperaturePoint tp = new Device.Devices.TemperaturePoint();
                    paramList[i].paramM.CopyTo(tp.paramM, 0);
                    paramList[i].paramS.CopyTo(tp.paramS, 0);
                    devicesAll.temperaturePointList.Add(tp);
                }
            }

            Utils.Logger.Op("点击自动控温设置界面 开始 按键，开始执行自动控温流程...");
            Utils.Logger.Sys("点击自动控温设置界面 开始 按键，开始执行自动控温流程...");

            Utils.Logger.Op("设定的温度点有：");
            Utils.Logger.Sys("设定的温度点有：");

            foreach (var st in paramList)
            {
                Utils.Logger.Op(st.paramM[0].ToString("0.0000"));
                Utils.Logger.Sys(st.paramM[0].ToString("0.0000"));
            }

            // 将温度点及参数写入缓存文件
            FileStream stm = File.Open(@"./params.cache", FileMode.OpenOrCreate, FileAccess.Write);
            stm.Seek(0, SeekOrigin.Begin);
            stm.SetLength(0);
            stm.Close();

            StreamWriter stmW = new StreamWriter(@"./params.cache", false,Encoding.UTF8);

            for(int i = 0;i<paramList.Count;i++)
            {
                //
                stmW.WriteLine(paramList[i].paramM[0].ToString("0.0000") + " " + paramList[i].paramM[1].ToString("0.0000") + " " +
                    paramList[i].paramM[2].ToString("0") + " " + paramList[i].paramM[3].ToString("0") + " " +
                    paramList[i].paramM[4].ToString("0") + " " + paramList[i].paramM[5].ToString("0") + " " +
                    paramList[i].paramM[6].ToString("0"));
                stmW.WriteLine(paramList[i].paramS[0].ToString("0.0000") + " " + paramList[i].paramS[1].ToString("0.0000") + " " +
                    paramList[i].paramS[2].ToString("0") + " " + paramList[i].paramS[3].ToString("0") + " " +
                    paramList[i].paramS[4].ToString("0") + " " + paramList[i].paramS[5].ToString("0") + " " +
                    paramList[i].paramS[6].ToString("0"));
            }
            stmW.Flush();
            stmW.Close();

            // 开始实验流程
            this.DialogResult = DialogResult.OK;
        }
#endif


        // 取消操作，关闭窗口
        private void button_cancel_Click(object sender, EventArgs e)
        {
            Utils.Logger.Op("点击自动控温设置界面 取消 按键，取消了自动控温流程设置...");
            Utils.Logger.Sys("点击自动控温设置界面 取消 按键，取消了自动控温流程设置...");

            // 取消操作
            this.Dispose();
        }


        // 添加温度点
        private void button_add_Click(object sender, EventArgs e)
        {
            // 如果已经开始自动控温流程，则无法编辑
            if (flowStart)
                return;


            float valuef = 0.0f;
            // 主槽参数
            for(int i = 0;i<7;i++)
            {
                if(float.TryParse(this.paramMTextBox[i].Text, out valuef))
                {
                    paramMCache[i] = valuef;
                }
                else
                {
                    MessageBox.Show("温度点参数格式不正确，请检查!");
                    return;
                }
            }
            if(paramMCache[0] > devicesAll.tempMaxValue || paramMCache[0] < devicesAll.tempMinValue)
            {
                MessageBox.Show("温度点超出界限 ( "+ devicesAll.tempMinValue .ToString("0.0000")+ " - "+ devicesAll.tempMaxValue.ToString("0.0000")+ " )，请检查!");
                return;
            }


            // 辅槽参数
            for (int i = 0; i < 7; i++)
            {
                if (float.TryParse(this.paramSTextBox[i].Text, out valuef))
                {
                    paramSCache[i] = valuef;
                }
                else
                {
                    MessageBox.Show("温度点参数格式不正确，请检查!");
                    return;
                }
            }
            if (paramSCache[0] > devicesAll.tempMaxValue || paramSCache[0] < devicesAll.tempMinValue)
            {
                MessageBox.Show("温度点超出界限 ( " + devicesAll.tempMinValue.ToString("0.0000") + " - " + devicesAll.tempMaxValue.ToString("0.0000") + " )，请检查!");
                return;
            }

            // 判断温度点是否已经存在于 BList 中
            foreach (TempParam st in paramList)
            {
                if (st.paramM[0] == paramMCache[0])
                {
                    MessageBox.Show("温度点已经存在！");
                    textBox_tpSetM.Text = "";
                    return;
                }
            }



            // 添加温度点
            TempParam ts = new TempParam();
            paramMCache.CopyTo(ts.paramM, 0);
            paramSCache.CopyTo(ts.paramS, 0);
            // 添加到 
            paramList.Add(ts);

            // 更新表格显示
            updateDataGridView();

            // 删除列表中的温度设定值
            textBox_tpSetM.Text = "";
            textBox_tpSetS.Text = "";

            Utils.Logger.Op("添加温度设定点: " + ts.paramM[0].ToString("0.0000"));
            Utils.Logger.Sys("添加温度设定点: " + ts.paramM[0].ToString("0.0000"));

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 删除文本框中的文本
            if (tx != null)
            {
                tx.Text = "";
            }


            // 如果已经开始自动控温流程，则无法编辑
            if (flowStart)
                return;

            // 删除选中的温度点
            for (int i = BList.Count; i > 0; i--)
            {
                if (dataGridView1.Rows[i - 1].Selected == true)
                {
                    Utils.Logger.Op("删除了温度设定点: " + BList[i - 1].TemptSet);
                    Utils.Logger.Sys("删除了温度设定点: " + BList[i - 1].TemptSet);

                    if(i%2 == 0)
                    {
                        BList.RemoveAt(i - 1);
                        BList.RemoveAt(i - 2);
                        paramList.RemoveAt((i / 2) - 1);
                        i--;
                    }
                    else
                    {
                        BList.RemoveAt(i);
                        BList.RemoveAt(i - 1);
                        paramList.RemoveAt(i / 2);
                    }

                    break;
                }
            }

            // 更新表格显示
            updateDataGridView();

        }


        // 选中表格行时，取消编辑文本
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
                tx = null;
            }
        }

        // 重新编辑按键
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 如果已经开始自动控温流程，则无法编辑
            if (flowStart)
                return;


            if (dataGridView1.Columns[e.ColumnIndex].Name == "edit")
            {
                if(e.RowIndex % 2 == 1)
                {
                    // 获取该行的参数
                    paramList[e.RowIndex / 2].paramM.CopyTo(paramMCache, 0);
                    paramList[e.RowIndex / 2].paramS.CopyTo(paramSCache, 0);
                    // 将参数写入到文本框中
                    for (int i = 0; i < 7; i++)
                    {
                        if (i < 3)
                        {
                            paramMTextBox[i].Text = paramMCache[i].ToString("0.000");
                            paramSTextBox[i].Text = paramSCache[i].ToString("0.000");
                        }
                        else
                        {
                            paramMTextBox[i].Text = paramMCache[i].ToString("0");
                            paramSTextBox[i].Text = paramSCache[i].ToString("0");
                        }
                    }
                    // 从 paramList 中删除该项
                    paramList.RemoveAt(e.RowIndex / 2);
                    // 重新显示
                    updateDataGridView();
                }
                else
                {
                    paramList[e.RowIndex / 2].finished = !paramList[e.RowIndex / 2].finished;
                    updateDataGridView();
                }
                
            }
        }


        ////////////////////////////////////////////////////////////
        //// 表格单元合并 / 重新绘制
        ////////////////////////////////////////////////////////////
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 合并序号列
            if (dataGridView1.Columns[e.ColumnIndex].Name == "index" && e.RowIndex >= 0)
            {
                Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor);
                Pen gridLinePen = new Pen(gridBrush);
                SolidBrush backBrush = new SolidBrush(e.CellStyle.BackColor);
                SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);

                e.Graphics.FillRectangle(backBrush, e.CellBounds);

                // 绘制单元格相互间隔的区分线条，dataGridView自己会处理左侧和上册边缘的线条，因此，只需绘制下边框和右边框
                // dataGridView 控件绘制单元格时，不绘制左边和上边框，共用左单元格的右边框，上一单元格的下边框

                // 不是最后一行且单元格的值不为 null
                if (e.RowIndex < this.dataGridView1.RowCount - 1 && this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value != null)
                {
                    // 若与下一单元格值不同
                    if (e.Value.ToString() != this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString())
                    {
                        //下边缘的线
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
                        e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        //绘制值
                        if (e.Value != null)
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
                                Brushes.Crimson, e.CellBounds.X + 2,
                                e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                    }
                    //若与下一单元格值相同 
                    else
                    {
                        //背景颜色
                        //e.CellStyle.BackColor = Color.LightPink;   //仅在CellFormatting方法中可用
                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
                        this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
                        //只读（以免双击单元格时显示值）
                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                        this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].ReadOnly = true;
                    }
                }
                //最后一行或单元格的值为null
                else
                {
                    //下边缘的线
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
                        e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

                    //绘制值
                    if (e.Value != null)
                    {
                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
                            Brushes.Crimson, e.CellBounds.X + 2,
                            e.CellBounds.Y + 2, StringFormat.GenericDefault);
                    }
                }

                //右侧的线
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                    e.CellBounds.Top, e.CellBounds.Right - 1,
                    e.CellBounds.Bottom - 1);

                //设置处理事件完成（关键点），只有设置为ture,才能显示出想要的结果。
                e.Handled = true;
            }

#if false
            // 合并编辑列
            else if(dataGridView1.Columns[e.ColumnIndex].Name == "edit" && e.RowIndex >= 0)
            {
                Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor);
                Pen gridLinePen = new Pen(gridBrush);
                SolidBrush backBrush = new SolidBrush(e.CellStyle.BackColor);
                SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);

                e.Graphics.FillRectangle(backBrush, e.CellBounds);

                // 绘制单元格相互间隔的区分线条，dataGridView自己会处理左侧和上册边缘的线条，因此，只需绘制下边框和右边框
                // dataGridView 控件绘制单元格时，不绘制左边和上边框，共用左单元格的右边框，上一单元格的下边框

                // 不是最后一行且单元格的值不为 null
                if (e.RowIndex < this.dataGridView1.RowCount - 1 && this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value != null)
                {
                    // 若与下一单元格值不同
                    if (this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() != this.dataGridView1.Rows[e.RowIndex + 1].Cells[0].Value.ToString())
                    {
                        //下边缘的线
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
                        e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        //绘制值
                        if (e.Value != null)
                        {
                            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
                                Brushes.Crimson, e.CellBounds.X + 2,
                                e.CellBounds.Y + 2, StringFormat.GenericDefault);
                        }
                    }
                    //若与下一单元格值相同 
                    else
                    {
                        //背景颜色
                        //e.CellStyle.BackColor = Color.LightPink;   //仅在CellFormatting方法中可用
                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
                        this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
                        //只读（以免双击单元格时显示值）
                        this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                        this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].ReadOnly = true;
                    }
                }
                //最后一行或单元格的值为null
                else
                {
                    //下边缘的线
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
                        e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

                    //绘制值
                    if (e.Value != null)
                    {
                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
                            Brushes.Crimson, e.CellBounds.X + 2,
                            e.CellBounds.Y + 2, StringFormat.GenericDefault);
                    }
                }

                //右侧的线
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                    e.CellBounds.Top, e.CellBounds.Right - 1,
                    e.CellBounds.Bottom - 1);

                //设置处理事件完成（关键点），只有设置为ture,才能显示出想要的结果。
                e.Handled = true;
            }
#endif
        }

        private void button10_Click(object sender, EventArgs e)
        {
            float val = 0.0f;
            if (!float.TryParse(textBox_tpSetM.Text, out val))
                return;

            
        }


        ///////////////////////////////////////////////////////////////
        // 键盘操作
        private void button9_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "9";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-9";
                }
                else
                {
                    tx.Text += "9";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "8";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-8";
                }
                else
                {
                    tx.Text += "8";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "7";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-7";
                }
                else
                {
                    tx.Text += "7";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "6";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-6";
                }
                else
                {
                    tx.Text += "6";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "5";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-5";
                }
                else
                {
                    tx.Text += "5";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "4";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-4";
                }
                else
                {
                    tx.Text += "4";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "3";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-3";
                }
                else
                {
                    tx.Text += "3";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "2";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-2";
                }
                else
                {
                    tx.Text += "2";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "1";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-1";
                }
                else
                {
                    tx.Text += "1";
                }

            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 2 && tx.Text == "-0")
                {

                }
                else if (tx.Text.Length != 1 || tx.Text == "-" || int.Parse(tx.Text) != 0)
                {
                    tx.Text += "0";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void buttonNegtive_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text == "")
                {
                    tx.Text = "-";
                }
                else if (tx.Text[0] == '-')
                {
                    tx.Text = tx.Text.Remove(0, 1);
                }
                else
                {
                    tx.Text = tx.Text.Insert(0, "-");
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void buttonPoint_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (!tx.Text.Contains("."))
                {
                    if (tx.Text.Length == 0)
                    {
                        tx.Text = "0.";
                    }
                    else if (tx.Text.Length == 1 && tx.Text == "-")
                    {
                        tx.Text = "-0.";
                    }
                    else
                    {
                        tx.Text += ".";
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length > 0)
                    tx.Text = tx.Text.Substring(0, tx.Text.Length - 1);
            }
            //else
            //{
            //    MessageBox.Show("请先选定设定项!");
            //}
        }

        // 编辑文本时，取消表格行的选中
        private void textBox_tpSet_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_tpSetM;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_tpAdjust_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_tpAdjustM;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_advance_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_advanceM;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_fuzzy_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_fuzzyM;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_ratio_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_ratioM;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_integ_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_integM;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_power_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_powerM;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        //////////////////////////////////////////////////////////////
        private void textBox_tpSetS_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_tpSetS;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_tpAdjustS_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_tpAdjustS;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_advanceS_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_advanceS;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_fuzzyS_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_fuzzyS;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_ratioS_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_ratioS;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_integS_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_integS;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_powerS_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_powerS;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }
    }

    internal class ParamShow
    {
        public int _index;
        public string tpName;
        public string editSave;
        /// <summary>
        /// 温控设备的参数值
        /// </summary>
        public float[] param = new float[7];


        /// <summary>
        /// 编号
        /// </summary>
        public string Index { get { return _index.ToString("0"); } }
        /// <summary>
        /// 主槽 / 辅槽 （名称）
        /// </summary>
        public string TpName { get { return tpName; } }
        /// <summary>
        /// 温度值
        /// </summary>
        public string TemptSet { get { return param[0].ToString("0.0000"); } }
        /// <summary>
        /// 温度修订值
        /// </summary>
        public string TempAdjust { get { return param[1].ToString("0.0000"); } }
        /// <summary>
        /// 超前调整值
        /// </summary>
        public string Advance { get { return param[2].ToString("0.000"); } }
        /// <summary>
        /// 模糊系数
        /// </summary>
        public string Fuzzy { get { return param[3].ToString("0"); } }
        /// <summary>
        /// 比例系数
        /// </summary>
        public string Ratio { get { return param[4].ToString("0"); } }
        /// <summary>
        /// 积分系数
        /// </summary>
        public string Integration { get { return param[5].ToString("0"); } }
        /// <summary>
        /// 功率系数
        /// </summary>
        public string Power { get { return param[6].ToString("0"); } }
        /// <summary>
        /// 波动度阈值
        /// </summary>
        public string FlucThr { get { return param[7].ToString("0.000"); } }
        /// <summary>
        /// 温度阈值
        /// </summary>
        public string TempThr { get { return param[8].ToString("0.000"); } }

        public string Edit { get { return editSave; } }
    }

    /// <summary>
    /// 温度点设定状态
    /// </summary>
    internal class TempParam : IComparable
    {
        public bool finished = false;
        /// <summary>
        /// 主槽温控设备的参数值
        /// </summary>
        public float[] paramM = new float[7];
        /// <summary>
        /// 辅槽温控设备的参数值
        /// </summary>
        public float[] paramS = new float[7];

        /// <summary>
        /// 比较函数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(Object obj)
        {
            if (obj == null) return 1;
            TempParam otherState = obj as TempParam;
            if (paramM[0] > otherState.paramM[0]) { return 1; }
            else
            {
                if (paramM[0] == otherState.paramM[0]) { return 0; }
                else { return -1; }
            }
        }
    }

}
