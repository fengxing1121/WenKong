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
    public partial class FormFinish : Form
    {
        Timer timer1 = new Timer();
        int timeCount = 600;
        public FormFinish(int timeSecond = 600)
        {
            InitializeComponent();
            timeCount = timeSecond;
        }

        private void FormFinish_Load(object sender, EventArgs e)
        {
            this.timer1.Interval = 1000;
            this.timer1.Tick += Timer1_Tick;
            this.timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timeCount--;
            label_time.Text = (timeCount/60).ToString("0") + " 分钟  " + (timeCount % 60).ToString("0") + " 秒后将关闭计算机！";
            if(timeCount < 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button_shutdown_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
