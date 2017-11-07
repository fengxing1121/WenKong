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
    public partial class FormChart : Form
    {
        DrawChart mDrawChart;
        Device.TempDevice tpDevice;
        Device.Devices deviceAll;
        public FormChart(Device.Devices devAll, Device.TempDevice dev)
        {
            InitializeComponent();
            tpDevice = dev;
            deviceAll = devAll;
            mDrawChart = new DrawChart(dev,TempPic.Height, TempPic.Width, 11, 7);
        }

        private void TemperatureChart_Load(object sender, EventArgs e)
        {
            TempPic.Image = mDrawChart.Draw();
            deviceAll.TpTemperatureUpdateTimerEvent += DeviceAll_TpTemperatureUpdateTimerEvent;
        }

        private void DeviceAll_TpTemperatureUpdateTimerEvent()
        {
            // 只要是定时器函数执行了，不管有没有正确的从下位机读取到参数，都会重新绘制
            // 也就是说，不处理错误
            this.BeginInvoke(new EventHandler(delegate
            {
                TempPic.Image = mDrawChart.Draw();
                if (deviceAll.currentState.flowState == Device.Devices.State.TempControl || deviceAll.currentState.flowState == Device.Devices.State.TempStable)
                {
                    int timeSec = deviceAll.currentState.stateTime * deviceAll.tpDeviceM.readTempInterval / 1000;
                    this.label1.Text = "控温时间： " + (timeSec/60).ToString("0") + " 分" + (timeSec%60).ToString("0") + "秒";
                }
                else
                {
                    this.label1.Text = "控温时间： null";
                }
                float val = 0.0f;
                if(deviceAll.tpDeviceM.GetFluc((5*60*1000/deviceAll.tpDeviceM.readTempInterval),out val))
                {
                    this.label2.Text = "5分钟波动度： " + val.ToString("0.0000");
                }
                else
                {
                    this.label2.Text = "5分钟波动度： null";
                }

            }));
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TemperatureChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            mDrawChart.Dispose();
            deviceAll.TpTemperatureUpdateTimerEvent -= DeviceAll_TpTemperatureUpdateTimerEvent;
        }
    }
}
