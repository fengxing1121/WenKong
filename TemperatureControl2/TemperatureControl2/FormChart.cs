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
        public FormChart( Device.TempDevice dev)
        {
            InitializeComponent();
            tpDevice = dev;
            mDrawChart = new DrawChart(dev,TempPic.Height, TempPic.Width, 11, 7);
        }

        private void TemperatureChart_Load(object sender, EventArgs e)
        {
            TempPic.Image = mDrawChart.Draw();
            
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {

        }

        private void TemperatureChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            mDrawChart.Dispose();
        }
    }
}
