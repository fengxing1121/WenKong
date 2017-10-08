using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Device;
using Logs;

namespace TemperatureControl
{
    public partial class FormSetting : Form
    {
        ///////////////////////////////////////
        #region 用户自定义成员变量部分 by wghou
        private TempDevice _dev;

        #endregion


        public FormSetting()
        {
            InitializeComponent();
        }

        public FormSetting(ref TempDevice device)
        {
            InitializeComponent();
            _dev = device;
        }


        private void FormSetting_Shown(object sender, EventArgs e)
        {
            // 第一次显示窗体
            Logger.Op(this.Text + "参数设置...");
        }


        private void button_return_Click(object sender, EventArgs e)
        {

        }

        private void button_apply_Click(object sender, EventArgs e)
        {

        }

        private void button_read_Click(object sender, EventArgs e)
        {

        }

    }
}
