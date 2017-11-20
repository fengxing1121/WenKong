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

        // 用于定时 1 秒
        Timer tm1Sec;
        int tm1_count = 0;
        UInt64 total = 0;

        public FormChart(Device.Devices devAll, Device.TempDevice dev)
        {
            InitializeComponent();
            tpDevice = dev;
            deviceAll = devAll;
            mDrawChart = new DrawChart(dev,TempPic.Height, TempPic.Width, 11, 7);

            tm1Sec = new Timer();
            tm1Sec.Interval = 1000;
            tm1Sec.Tick += Tm1Sec_Tick;
            tm1_count = devAll.tpDeviceM.readTempIntervalSec - 1;
            if(tm1_count != 0)
                tm1Sec.Start();
        }

        private void Tm1Sec_Tick(object sender, EventArgs e)
        {
            if(--tm1_count == 0)
                this.tm1Sec.Stop();
            this.total++;
            UInt64 timeHour = total / 360;
            UInt64 timeMin = (total - timeHour * 360) / 60;
            UInt64 timeSec = total % 60;
            this.label1.Text = "控温时间： " + (timeHour).ToString("0") + " 小时" + (timeMin).ToString("0") + " 分" + (timeSec).ToString("0") + "秒";
        }

        private void TemperatureChart_Load(object sender, EventArgs e)
        {
            TempPic.Image = mDrawChart.Draw();
            deviceAll.TpTemperatureUpdateTimerEvent += DeviceAll_TpTemperatureUpdateTimerEvent;

            total = deviceAll.timeStart;
            UInt64 timeHour = total / 360;
            UInt64 timeMin = (total - timeHour * 360) / 60;
            UInt64 timeSec = total % 60;
            this.label1.Text = "控温时间： " + (timeHour).ToString("0") + " 小时" + (timeMin).ToString("0") + " 分" + (timeSec).ToString("0") + "秒";
        }

        private void DeviceAll_TpTemperatureUpdateTimerEvent()
        {
            // 只要是定时器函数执行了，不管有没有正确的从下位机读取到参数，都会重新绘制
            // 也就是说，不处理错误
            this.BeginInvoke(new EventHandler(delegate
            {
                // 用于每隔一秒计数一次
                this.tm1Sec.Start();

                TempPic.Image = mDrawChart.Draw();

                total = deviceAll.timeStart;
                //System.Diagnostics.Debug.WriteLine("当前时间" + total.ToString());
                
                UInt64 timeHour = total / 3600;
                UInt64 timeMin = (total - timeHour * 3600) / 60;
                UInt64 timeSec = total % 60;
                this.label1.Text = "控温时间： " + (timeHour).ToString("0") + " 小时" + (timeMin).ToString("0") + " 分" + (timeSec).ToString("0") + "秒";

                float val = 0.0f;
                if(deviceAll.tpDeviceM.GetFluc((5*60/deviceAll.tpDeviceM.readTempIntervalSec),out val))
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
