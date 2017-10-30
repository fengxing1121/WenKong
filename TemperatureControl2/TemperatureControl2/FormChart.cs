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

        private void DeviceAll_TpTemperatureUpdateTimerEvent(Device.TempProtocol.Err_t err)
        {
            if (err == Device.TempProtocol.Err_t.NoError)
            {
                this.BeginInvoke(new EventHandler(delegate
                {
                    TempPic.Image = mDrawChart.Draw();
                }));
            }
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
