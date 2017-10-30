using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TemperatureControl2
{
    public partial class FormSetting : Form
    {
        /// <summary>
        /// 用于存放所有对象
        /// </summary>
        private Device.TempDevice tpDev;

        /// <summary>
        /// 存放参数控件数组
        /// </summary>
        private TextBox[] tpParam = new TextBox[9];

        private TextBox tx = null;

        // 窗体构造函数
        public FormSetting(Device.TempDevice dev)
        {
            InitializeComponent();

            // 温控设备对象
            tpDev = dev;

            // 温控设备参数控件
            tpParam[0] = TxtTempSet;
            tpParam[1] = TxtTempCorrect;
            tpParam[2] = TxtLeadAdjust;
            tpParam[3] = TxtFuzzy;
            tpParam[4] = TxtRatio;
            tpParam[5] = TxtIntegral;
            tpParam[6] = TxtPower;
            tpParam[7] = TxtFlucThr;
            tpParam[8] = TxtTempThr;

        }


        // 窗体加载事件 - 处理函数
        // 从 TempDevice.tpParam 中读取参数值
        // 在 TempDevice 初始化的过程中，已经将硬件中的参数读取到了 tpParam 中，因此，不再直接从硬件中读取参数
        private void FormSetting_Load(object sender, EventArgs e)
        {
            // 注册温控设备参数更新 / 设置事件处理函数
            this.tpDev.ParamUpdatedFromDeviceEvent += TpDev_ParamUpdatedFromDeviceEvent;
            this.tpDev.ParamUpdatedToDeviceEvent += TpDev_ParamUpdatedToDeviceEvent;

            // 从硬件设备读取参数
            //TempGetSetParamHandler getTempParam = new TempGetSetParamHandler(this.tpDev.UpdateParamFromDevice);
            //getTempParam.BeginInvoke(null, null);
            for(int i = 0;i<tpParam.Length;i++)
            {
                tpParam[i].Text = tpDev.tpParam[i].ToString(tpDev.tpParamFormat[i]);
            }
        }


        // 窗体关闭事件 - 处理函数
        // 设置窗口关闭时，注销相应的事件
        private void FormSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注销温控设备参数更新 / 设置事件处理函数
            this.tpDev.ParamUpdatedFromDeviceEvent -= TpDev_ParamUpdatedFromDeviceEvent;
            this.tpDev.ParamUpdatedToDeviceEvent -= TpDev_ParamUpdatedToDeviceEvent;
        }


        /// <summary>读取温控设备参数 - 委托 - 用于开辟新的线程读取设备参数 </summary>
        private delegate void TempGetSetParamHandler();

        // 按键 click 事件 - 处理函数
        // 参数读取按键
        private void BntRead_Click(object sender, EventArgs e)
        {
            // 从硬件设备读取参数
            TempGetSetParamHandler getTempParam = new TempGetSetParamHandler(this.tpDev.UpdateParamFromDevice);
            getTempParam.BeginInvoke(null, null);

            Utils.Logger.Op("点击 查询参数 按键，从 " + tpDev.tpDeviceName + " 中读取温控设备的参数!");
            Utils.Logger.Sys("点击 查询参数 按键，从 " + tpDev.tpDeviceName + " 中读取温控设备的参数!");
        }


        // 按键 click 事件 - 处理函数
        // 参数设置按键
        private void BntUpdate_Click(object sender, EventArgs e)
        {
            // 设置温控设备参数
            for (int i = 0; i < 9; i++)
            {
                float newVal = 0.0f;

                if (float.TryParse(this.tpParam[i].Text, out newVal) != true)
                {
                    // 参数数据格式错误哦
                    MessageBox.Show("参数 " + tpDev.tpParamNames[i] + " 格式错误！");
                    return;
                }

                // 将参数写入参数设置缓存
                tpDev.tpParamToSet[i] = newVal;
            }

            // 向硬件设备更新参数
            TempGetSetParamHandler setTempParam = new TempGetSetParamHandler(this.tpDev.UpdateParamToDevice);
            setTempParam.BeginInvoke(null, null);

            Utils.Logger.Op("点击 更新参数 按键，向 " + tpDev.tpDeviceName + " 中写入温控设备的参数!");
            Utils.Logger.Sys("点击 更新参数 按键，向 " + tpDev.tpDeviceName + " 中写入温控设备的参数!");
        }


        /// <summary>
        /// 从设备读取参数完成事件处理函数
        /// </summary>
        /// <param name="st"></param>
        /// <param name="err"></param>
        private void TpDev_ParamUpdatedFromDeviceEvent(Device.TempProtocol.Err_t err)
        {
            // 从下位机读取参数成功
            if (err == Device.TempProtocol.Err_t.NoError)
            {
                this.BeginInvoke(new EventHandler(delegate
                {
                    // 从 TempDevice.tpParam 中读取参数值
                    for (int i = 0; i < tpParam.Length; i++)
                    {
                        tpParam[i].Text = tpDev.tpParam[i].ToString(tpDev.tpParamFormat[i]);
                    }
                    MessageBox.Show("从温控设备读取参数成功!");
                }));

            }
            // 从下位机读取参数失败
            else
            {
                this.BeginInvoke(new EventHandler(delegate
                {
                    MessageBox.Show("从温控设备读取参数失败! \r错误状态：" + err.ToString());
                }));

                Utils.Logger.Sys("从 " + tpDev.tpDeviceName + " 中读取温控设备的参数失败  ErrorCode: !" + err.ToString());
            }
        }


        /// <summary>
        /// 向下位机写入参数完成事件处理函数
        /// </summary>
        /// <param name="st">参数是否写入成功</param>
        /// <param name="err">错误代码</param>
        private void TpDev_ParamUpdatedToDeviceEvent(Device.TempProtocol.Err_t err)
        {
            // 向下位机写入参数成功
            if (err == Device.TempProtocol.Err_t.NoError)
            {
                this.BeginInvoke(new EventHandler(delegate
                {
                    MessageBox.Show("向温控设备更新参数成功!");
                }));
            }
            // 向下位机写入参数失败
            else
            {
                this.BeginInvoke(new EventHandler(delegate
                {
                    MessageBox.Show("向温控设备更新参数失败! \r错误状态：" + err.ToString());
                }));
            }

            Utils.Logger.Sys("向 " + tpDev.tpDeviceName + " 中写入温控设备的参数失败  ErrorCode: !" + err.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region 焦点
        // 焦点
        private void TxtTempSet_Enter(object sender, EventArgs e)
        {
            if(tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.TxtTempSet;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void TxtTempCorrect_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.TxtTempCorrect;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void TxtLeadAdjust_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.TxtLeadAdjust;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void TxtFuzzy_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.TxtFuzzy;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void TxtRatio_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.TxtRatio;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void TxtIntegral_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.TxtIntegral;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void TxtPower_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.TxtPower;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void TxtFlucThr_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.TxtFlucThr;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void TxtTempThr_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.TxtTempThr;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }
        #endregion


        private void button9_Click(object sender, EventArgs e)
        {
            if(tx != null)
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
                if(tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "1";
                }
                else if(tx.Text.Length == 2 && tx.Text == "-0")
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
                if(tx.Text.Length == 2 && tx.Text == "-0")
                {

                }
                else if(tx.Text.Length != 1|| tx.Text == "-" || int.Parse(tx.Text) !=0 )
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
                if(tx.Text == "")
                {
                    tx.Text = "-";
                }
                else if(tx.Text[0] == '-')
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
                if(!tx.Text.Contains("."))
                {
                    if(tx.Text.Length == 0)
                    {
                        tx.Text = "0.";
                    }
                    else if(tx.Text.Length == 1 && tx.Text == "-")
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
            if (tx != null)
            {
                tx.Text = "";
            }
            //else
            //{
            //    MessageBox.Show("请先选定设定项!");
            //}
        }

        // end

    }
}
