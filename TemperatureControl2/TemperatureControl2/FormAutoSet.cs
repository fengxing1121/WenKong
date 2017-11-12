using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TemperatureControl2
{
    public partial class FormAutoSet : Form
    {
        // 设备
        Device.Devices devicesAll;
        // 自动控制流程的温度点
        BindingCollection<TemptState> BList = new BindingCollection<TemptState>();

        private TextBox tx = null;
        private TextBox[] paramTextBox = new TextBox[9];
        private float[] paramCache = new float[9];

        public FormAutoSet(Device.Devices dev)
        {
            InitializeComponent();
            devicesAll = dev;
            paramTextBox[0] = textBox_tpSet;
            paramTextBox[1] = textBox_tpAdjust;
            paramTextBox[2] = textBox_advance;
            paramTextBox[3] = textBox_fuzzy;
            paramTextBox[4] = textBox_ratio;
            paramTextBox[5] = textBox_integ;
            paramTextBox[6] = textBox_power;
            paramTextBox[7] = null;
            paramTextBox[8] = null;
        }


        // 窗体载入函数
        private void FormAutoSet_Load(object sender, EventArgs e)
        {
            index.DataPropertyName = "Index";
            tpSet.DataPropertyName = "TemptSet";
            tpAdjust.DataPropertyName = "TempAdjust";
            advance.DataPropertyName = "Advance";
            fuzzy.DataPropertyName = "Fuzzy";
            ratio.DataPropertyName = "Ratio";
            integration.DataPropertyName = "Integration";
            power.DataPropertyName = "Power";

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = BList;

            foreach (var st in devicesAll.temperaturePointList)
            {
                TemptState ts = new TemptState();
                st.paramS.CopyTo(ts.paramM, 0);
                BList.Add(ts);
            }

            // 排序
            BList.Sort(null, ListSortDirection.Ascending);

            devicesAll.tpDeviceM.tpParam.CopyTo(paramCache, 0);

            // 默认选中
            tx = textBox_tpSet;
            tx.BackColor = System.Drawing.SystemColors.Window;

            for(int i = 1;i<7;i++)
            {
                if(i<3)
                    paramTextBox[i].Text = paramCache[i].ToString("0.000");
                else
                    paramTextBox[i].Text = paramCache[i].ToString("0");
            }
        }


        // 开始自动控温流程
        private void button_start_Click(object sender, EventArgs e)
        {
            lock(devicesAll.stepLocker)
            {
                devicesAll.temperaturePointList.Clear();
                // 将实验流程数据写入 Devices 类中
                for(int i = 0;i<BList.Count;i++)
                {
                    // deviceAll.controlFlowList 中的 StateFlow.flowState 必须设置为 Undefine
                    // 只保存温度点，不再保存
                    Device.Devices.TemperaturePoint tp = new Device.Devices.TemperaturePoint();
                    BList[i].paramM.CopyTo(tp.paramM, 0);
                    BList[i].paramM.CopyTo(tp.paramS, 0);
                    devicesAll.temperaturePointList.Add(tp);
                }
            }

            Utils.Logger.Op("点击自动控温设置界面 开始 按键，开始执行自动控温流程...");
            Utils.Logger.Sys("点击自动控温设置界面 开始 按键，开始执行自动控温流程...");

            Utils.Logger.Op("设定的温度点有：");
            Utils.Logger.Sys("设定的温度点有：");

            foreach (var st in BList)
            {
                Utils.Logger.Op(st.TemptSet);
                Utils.Logger.Sys(st.TemptSet);
            }

            // 开始实验流程
            this.DialogResult = DialogResult.OK;
        }


        // 取消操作，关闭窗口
        private void button_cancel_Click(object sender, EventArgs e)
        {
            Utils.Logger.Op("点击自动控温设置界面 取消 按键，取消了自动控温流程设置...");
            Utils.Logger.Sys("点击自动控温设置界面 取消 按键，取消了自动控温流程设置...");

            // 取消操作
            this.DialogResult = DialogResult.Cancel;
        }


        // 添加温度点
        private void button_add_Click(object sender, EventArgs e)
        {
            float[] pArray = new float[9];
            float valuef;
            for(int i = 0;i<7;i++)
            {
                if(float.TryParse(this.paramTextBox[i].Text, out valuef))
                {
                    pArray[i] = valuef;
                }
                else
                {
                    MessageBox.Show("温度点格式不正确，请检查!");
                    return;
                }
            }
            pArray[7] = paramCache[7];
            pArray[8] = paramCache[8];

            // 判断温度点是否已经存在于 BList 中
            foreach (TemptState st in BList)
            {
                if (float.Parse(st.TemptSet) == pArray[0])
                {
                    MessageBox.Show("温度点已经存在！");
                    textBox_tpSet.Text = "";
                    return;
                }
            }

            // 添加温度点
            TemptState ts = new TemptState();
            ts._index = -1;
            pArray.CopyTo(ts.paramM, 0);
            pArray.CopyTo(ts.paramS, 0);
            // 向 BList 中添加新数据
            BList.Add(ts);
            BList.Sort(null, ListSortDirection.Ascending);
            // 计算编号
            for (int i = 0; i < BList.Count; i++)
            {
                BList[i]._index = i + 1;
            }
            dataGridView1.ClearSelection();

            // 删除列表中的温度设定值
            textBox_tpSet.Text = "";

            Utils.Logger.Op("添加温度设定点: " + ts.TemptSet);
            Utils.Logger.Sys("添加温度设定点: " + ts.TemptSet);

        }


        // 删除温度点
        private void button_delet_Click(object sender, EventArgs e)
        {
            for(int i = dataGridView1.Rows.Count; i>0;i--)
            {
                if (dataGridView1.Rows[i -1].Selected == true)
                {
                    BList.RemoveAt(i-1);
                }
                    
            }
            
        }


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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // 删除选中的温度点
            for(int i = BList.Count; i>0;i--)
            {
                if (dataGridView1.Rows[i - 1].Selected == true)
                {
                    Utils.Logger.Op("删除了温度设定点: " + BList[i - 1].TemptSet);
                    Utils.Logger.Sys("删除了温度设定点: " + BList[i - 1].TemptSet);

                    BList.RemoveAt(i - 1);
                }
            }

            // 计算编号
            for (int i = 0; i < BList.Count; i++)
            {
                BList[i]._index = i + 1;
            }

            // 删除文本框中的文本
            if (tx != null)
            {
                tx.Text = "";
            }
        }


        // 编辑文本时，取消表格行的选中
        private void textBox_tpSet_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_tpSet;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_tpAdjust_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_tpAdjust;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_advance_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_advance;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_fuzzy_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_fuzzy;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_ratio_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_ratio;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_integ_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_integ;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
        }

        private void textBox_power_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox_power;
            tx.BackColor = System.Drawing.SystemColors.Window;
            dataGridView1.ClearSelection();
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
    }


    /// <summary>
    /// 温度点设定状态
    /// </summary>
    class TemptState : IComparable
    {
        public int _index;
        /// <summary>
        /// 编号
        /// </summary>
        public string Index { get { return _index.ToString("0"); } }

        /// <summary>
        /// 主槽温控设备的参数值
        /// </summary>
        public float[] paramM = new float[9];
        /// <summary>
        /// 辅槽控温设备的参数值
        /// </summary>
        public float[] paramS = new float[9];
        /// <summary>
        /// 温度值
        /// </summary>
        public string TemptSet { get { return paramM[0].ToString("0.0000"); } }
        /// <summary>
        /// 温度修订值
        /// </summary>
        public string TempAdjust { get { return paramM[1].ToString("0.0000"); } }
        /// <summary>
        /// 超前调整值
        /// </summary>
        public string Advance { get { return paramM[2].ToString("0.000"); } }
        /// <summary>
        /// 模糊系数
        /// </summary>
        public string Fuzzy { get { return paramM[3].ToString("0"); } }
        /// <summary>
        /// 比例系数
        /// </summary>
        public string Ratio { get { return paramM[4].ToString("0"); } }
        /// <summary>
        /// 积分系数
        /// </summary>
        public string Integration { get { return paramM[5].ToString("0"); } }
        /// <summary>
        /// 功率系数
        /// </summary>
        public string Power { get { return paramM[6].ToString("0"); } }
        /// <summary>
        /// 波动度阈值
        /// </summary>
        public string FlucThr { get { return paramM[7].ToString("0.000"); } }
        /// <summary>
        /// 温度阈值
        /// </summary>
        public string TempThr { get { return paramM[8].ToString("0.000"); } }


        public int CompareTo(Object obj)
        {
            if (obj == null) return 1;
            TemptState otherState = obj as TemptState;
            if(paramM[0] > otherState.paramM[0]) { return 1; }
            else
            {
                if(paramM[0] == otherState.paramM[0]) { return 0; }
                else { return -1; }
            }
        }
    }


    #region BindingList Sort
    /// <summary>
    /// 支持排序的 BindingList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindingCollection<T> : BindingList<T>
    {
        private bool isSorted = false;
        private PropertyDescriptor sortProperty = null;
        private ListSortDirection sortDirection = ListSortDirection.Ascending;

        protected override bool IsSortedCore
        {
            get { return isSorted; }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirection; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortProperty; }
        }

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            List<T> items = this.Items as List<T>;

            if (items != null)
            {
                ObjectPropertyCompare<T> pc = new ObjectPropertyCompare<T>(property, direction);
                items.Sort(pc);
                isSorted = true;
            }
            else
            {
                isSorted = false;
            }

            sortProperty = property;
            sortDirection = direction;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            isSorted = false;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        //排序
        public void Sort(PropertyDescriptor property, ListSortDirection direction)
        {
            this.ApplySortCore(property, direction);
        }
    }

    class ObjectPropertyCompare<T> : System.Collections.Generic.IComparer<T>
    {
        private PropertyDescriptor property;
        private ListSortDirection direction;

        public ObjectPropertyCompare(PropertyDescriptor property, ListSortDirection direction)
        {
            this.property = property;
            this.direction = direction;
        }

        #region IComparer<T>
        /// <summary>
        /// 比较方法
        /// </summary>
        /// <param name="x">相对属性x</param>
        /// <param name="y">相对属性y</param>
        /// <returns></returns>
        public int Compare(T x, T y)
        {
            //object xValue = x.GetType().GetProperty(property.Name).GetValue(x, null);
            //object yValue = y.GetType().GetProperty(property.Name).GetValue(y, null);
            object xValue = x;
            object yValue = y;

            int returnValue;

            if (xValue is IComparable)
            {
                returnValue = ((IComparable)xValue).CompareTo(yValue);
            }
            else if (xValue.Equals(yValue))
            {
                returnValue = 0;
            }
            else
            {
                returnValue = xValue.ToString().CompareTo(yValue.ToString());
            }

            if (direction == ListSortDirection.Ascending)
            {
                return returnValue;
            }
            else
            {
                return returnValue * -1;
            }
        }

        public bool Equals(T xWord, T yWord)
        {
            return xWord.Equals(yWord);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        #endregion

        #endregion
    }
}
