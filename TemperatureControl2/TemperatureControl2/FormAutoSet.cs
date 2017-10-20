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
        Device.Devices devicesAll;
        public FormAutoSet( Device.Devices dev)
        {
            InitializeComponent();
            devicesAll = dev;
        }


        private void button_start_Click(object sender, EventArgs e)
        {
            // 将实验流程数据写入 Devices 类中

            // 开始实验流程
            this.DialogResult = DialogResult.OK;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {

            // 取消操作
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
