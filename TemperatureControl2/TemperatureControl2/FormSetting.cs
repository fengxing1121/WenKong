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
            TempGetSetParamHandler getTempParam = new TempGetSetParamHandler(this.tpDev.UpdateParamFromDevice);
            getTempParam.BeginInvoke(null, null);
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
            // 从下位机读取参数失败
            else
            {
                this.Invoke(new EventHandler(delegate
                {
                    MessageBox.Show("从温控设备读取参数失败! \r错误状态：" + err.ToString());
                }));
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
                this.Invoke(new EventHandler(delegate
                {
                    MessageBox.Show("向温控设备更新参数成功!");
                }));
            }
            // 向下位机写入参数失败
            else
            {
                this.Invoke(new EventHandler(delegate
                {
                    MessageBox.Show("向温控设备更新参数失败! \r错误状态：" + err.ToString());
                }));
            }
        }
        // end

    }
}
