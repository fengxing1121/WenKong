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

        // 窗体构造函数
        public FormSetting(Device.TempDevice dev)
        {
            InitializeComponent();

            // 设备对象
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
            this.tpDev.ParamUpdatedFromDeviceEvent += TpDev_ParamUpdatedFromDeviceEvent;
            this.tpDev.ParamUpdatedToDeviceEvent += TpDev_ParamUpdatedToDeviceEvent;

            // 从硬件设备读取参数
            TempGetParamHandler getTempParam = new TempGetParamHandler(this.tpDev.UpdateParamFromDevice);
            getTempParam.BeginInvoke(null, null);

            
        }

        private void TpDev_ParamUpdatedToDeviceEvent(bool st, Device.TempProtocol.Err_t err)
        {
            if(st == false)
            {
                this.Invoke(new EventHandler(delegate
                {
                    MessageBox.Show("向温控设备更新参数失败! \r错误状态：" + err.ToString());
                }));
            }
            else
            {
                this.Invoke(new EventHandler(delegate
                {
                    MessageBox.Show("向温控设备更新参数成功!");
                }));
            }
        }

        private void TpDev_ParamUpdatedFromDeviceEvent(bool st, Device.TempProtocol.Err_t err)
        {
            if (st == true)
            {
                this.Invoke(new EventHandler(delegate
                {
                    // 从 TempDevice.tpParam 中读取参数值
                    for (int i = 0; i < 9; i++)
                    {
                        tpParam[i].Text = tpDev.tpParam[i].ToString();
                    }
                    MessageBox.Show("从温控设备读取参数成功!");
                }));

            }
            else
            {
                this.Invoke(new EventHandler(delegate
                {
                    MessageBox.Show("从温控设备读取参数失败! \r错误状态：" + err.ToString());
                }));
            }
        }

        /// <summary>
        /// 读取温控设备参数 - 委托 - 用于开辟新的线程读取设备参数
        /// </summary>
        /// <param name="cmd"></param>
        private delegate void TempGetParamHandler();

        private void BntRead_Click(object sender, EventArgs e)
        {
            // 从硬件设备读取参数
            TempGetParamHandler getTempParam = new TempGetParamHandler(this.tpDev.UpdateParamFromDevice);
            getTempParam.BeginInvoke(null, null);
        }


        /// <summary>
        /// 温控设备参数设置 - 委托 - 用于开辟新的线程设置参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="val"></param>
        private delegate void TempSetParamHandler();

        // 参数设置按键
        private void BntUpdate_Click(object sender, EventArgs e)
        {
            // 设置温控设备参数
            for(int i = 0;i<9;i++)
            {
                float newVal = 0.0f;

                if(float.TryParse(this.tpParam[i].Text, out newVal) != true)
                {
                    // 参数数据格式错误哦
                    MessageBox.Show("参数 " + tpDev.tpParamNames[i] +" 格式错误！");
                    return;
                }

                if(Math.Abs(newVal - tpDev.tpParam[i]) > 10e-5)
                {
                    tpDev.tpParam[i] = newVal;
                }
            }

            // 向硬件设备更新参数
            TempSetParamHandler setTempParam = new TempSetParamHandler(this.tpDev.UpdateParamToDevice);
                    setTempParam.BeginInvoke(null, null);
        }

        private void FormSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.tpDev.ParamUpdatedFromDeviceEvent -= TpDev_ParamUpdatedFromDeviceEvent;
            this.tpDev.ParamUpdatedToDeviceEvent -= TpDev_ParamUpdatedToDeviceEvent;
        }
    }
}
