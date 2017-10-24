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

        public FormAutoSet(Device.Devices dev)
        {
            InitializeComponent();
            devicesAll = dev;
        }


        // 开始自动控温流程
        private void button_start_Click(object sender, EventArgs e)
        {
            lock(devicesAll.stepLocker)
            {
                devicesAll.controlFlowList.Clear();
                // 将实验流程数据写入 Devices 类中
                for(int i = 0;i<BList.Count;i++)
                {
                    devicesAll.controlFlowList.Add(new Device.Devices.StateFlow() { flowState = Device.Devices.State.TempDown, stateChanged = true, stateTemp = BList[i]._tempt, advanceState = BList[i]._isAdvance });
                    devicesAll.controlFlowList.Add(new Device.Devices.StateFlow() { flowState = Device.Devices.State.TempControl, stateChanged = true, stateTemp = BList[i]._tempt, advanceState = BList[i]._isAdvance });
                    devicesAll.controlFlowList.Add(new Device.Devices.StateFlow() { flowState = Device.Devices.State.TempStable, stateChanged = true, stateTemp = BList[i]._tempt, advanceState = BList[i]._isAdvance });
                    devicesAll.controlFlowList.Add(new Device.Devices.StateFlow() { flowState = Device.Devices.State.Measure, stateChanged = true, stateTemp = BList[i]._tempt, advanceState = BList[i]._isAdvance });
                }
                // 第一项设置为升温
                if(devicesAll.controlFlowList.Count !=0 )
                    devicesAll.controlFlowList.First().flowState = Device.Devices.State.TempUp;
            }
            
            // 开始实验流程
            this.DialogResult = DialogResult.OK;
        }


        // 取消操作，关闭窗口
        private void button_cancel_Click(object sender, EventArgs e)
        {
            // 取消操作
            this.DialogResult = DialogResult.Cancel;
        }


        // 添加温度点
        private void button_add_Click(object sender, EventArgs e)
        {
            float tp;
            if (float.TryParse(this.textBox_tp.Text,  out tp))
            {
                // 判断温度点是否已经存在于 BList 中
                foreach (TemptState st in BList)
                {
                    if (float.Parse(st.Tempt) == tp)
                    {
                        MessageBox.Show("温度点已经存在！");
                        textBox_tp.Text = "";
                        return;
                    }
                }

                // 向 BList 中添加新数据
                BList.Add(new TemptState(-1, tp, false));
                BList.Sort(null, ListSortDirection.Descending);
                // 计算编号
                for(int i = 0;i<BList.Count;i++)
                {
                    BList[i]._index = i + 1;
                }

                // 删除列表中的温度设定值
                textBox_tp.Text = "";
            }
            else
            {
                MessageBox.Show("温度点格式不正确，请检查!");
            }
        }


        // 删除温度点
        private void button_delet_Click(object sender, EventArgs e)
        {
            for(int i = dataGridView1.Rows.Count; i>0;i--)
            {
                if (dataGridView1.Rows[i -1].Selected == true)
                    BList.RemoveAt(i-1);
            }
            // 计算编号
            for (int i = 0; i < BList.Count; i++)
            {
                BList[i]._index = i + 1;
            }
        }


        // 窗体载入函数
        private void FormAutoSet_Load(object sender, EventArgs e)
        {
            index.DataPropertyName = "Index";
            temp.DataPropertyName = "Tempt";
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = BList;

            foreach(var st in devicesAll.controlFlowList)
            {
                if(st.flowState == Device.Devices.State.Measure)
                {
                    if(st.advanceState == false)
                    {
                        // 基本设置
                        BList.Add(new TemptState(BList.Count + 1, st.stateTemp, false));
                    }
                    else
                    {
                        // 高级设置
                        // wghou
                    }
                    
                }
            }

            // 排序
            BList.Sort(null, ListSortDirection.Descending);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            this.textBox_tp.Text += "9";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.textBox_tp.Text += "8";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textBox_tp.Text += "7";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox_tp.Text += "6";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.textBox_tp.Text += "5";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.textBox_tp.Text += "4";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox_tp.Text += "3";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox_tp.Text += "2";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox_tp.Text += "1";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            // 不能出现 00 
            if ((this.textBox_tp.Text.Length == 1) && this.textBox_tp.Text == "0")
                return;

            this.textBox_tp.Text += "0";
        }

        private void buttonp_Click(object sender, EventArgs e)
        {
            float val;
            if(float.TryParse(this.textBox_tp.Text + ".", out val))
                this.textBox_tp.Text += ".";
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if(this.textBox_tp.Text.Length != 0)
                this.textBox_tp.Text = this.textBox_tp.Text.Remove(this.textBox_tp.Text.Length - 1);
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
        public string Index { get { return _index.ToString(); } }

        public float _tempt;
        /// <summary>
        /// 温度值
        /// </summary>
        public string Tempt { get { return _tempt.ToString(); } }

        /// <summary>
        /// 是否是高级设置
        /// </summary>
        public bool _isAdvance = false;
        public bool IsAdvance { get { return _isAdvance; } }

        // 禁用该构造函数
        private TemptState() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index"></param>
        /// <param name="tempt"></param>
        /// <param name="isAdvance"></param>
        public TemptState(int index, float tempt, bool isAdvance) { _index = index; _tempt = tempt; _isAdvance = isAdvance; }

        public int CompareTo(Object obj)
        {
            if (obj == null) return 1;
            TemptState otherState = obj as TemptState;
            if(_tempt > otherState._tempt) { return 1; }
            else
            {
                if(_tempt == otherState._tempt) { return 0; }
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
