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
    public partial class FormAlarm : Form
    {
        // 显示时间
        private int errTime = 0;
        /// <summary>
        /// 警示信息
        /// </summary>
        public string errMessage = string.Empty;

        public EventHandler shutdownSystem;

        /// <summary>
        /// 创建警示窗口
        /// </summary>
        /// <param name="msg">警示信息</param>
        /// <param name="time">持续多久后关闭系统</param>
        public FormAlarm(string msg, int time = 600)
        {
            InitializeComponent();

            this.Name = "FormAlarm";

            // 默认十分钟后关闭程序
            if(time == 0 || time<0)
            {
                errTime = 10 * 60;
            }
            else
            {
                errTime = time;
            }

            // 存储并显示警示信息
            errMessage = msg;
            this.label_errMessage.Text = msg;

            // 初始化剩余时间
            this.label_errTime.Text = (errTime / 60).ToString("0") + " 分钟 " + (errTime % 60).ToString("0") + "秒后将自动关闭程序！";
            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            // 时间减一
            errTime--;

            // 显示警示信息
            this.label_errMessage.Text = errMessage;
            // 显示剩余时间
            this.label_errTime.Text = (errTime / 60).ToString("0") + " 分钟 " + (errTime % 60).ToString("0") + "秒后将自动关闭程序！";

            // 最后一分钟，始终将窗口置于最前
            if (errTime < 60)
                this.BringToFront();

            // 时间结束则返回请求关闭系统
            if(errTime == 0 || errTime < 0)
            {
                // 通知关闭系统
                shutdownSystem(null, null);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
