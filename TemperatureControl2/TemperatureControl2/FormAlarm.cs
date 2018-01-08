using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace TemperatureControl2
{
    public partial class FormAlarm : Form
    {
        // 显示时间
        private int errTime = 0;
        // wghou
        // 直接 new 出来的资源，窗体不会在关闭时释放，而是整个系统在关闭时释放，所以，感觉要慎用!!!
        //private Timer timer1 = new Timer();
        /// <summary>
        /// 警示信息统计
        /// </summary>
        public Dictionary<Device.Devices.FaultCode, int> errCount = new Dictionary<Device.Devices.FaultCode, int>();

        public event EventHandler shutdownSystem;

        /// <summary>
        /// 创建警示窗口
        /// </summary>
        /// <param name="msg">警示信息</param>
        /// <param name="time">持续多久后关闭系统</param>
        public FormAlarm(int time = 600)
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

            // 初始化剩余时间
            this.label_errTime.Text = (errTime / 60).ToString("0") + " 分钟 " + (errTime % 60).ToString("0") + "秒后将自动关闭程序！";
            timer1.Tick += Timer1_Tick;
            timer1.Interval = 1000;
            timer1.Start();


        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine("timer for error message and try to shut down PC.");
            
            // 时间减一
            errTime--;

            string errMsg = string.Empty;
            // 显示警示信息
            foreach(var item in errCount)
            {
                switch(item.Key)
                {
                    case Device.Devices.FaultCode.TempError:
                        if (item.Value != 0)
                            errMsg = "温控设备读取温度值失败!   " + item.Value.ToString() + " 次";
                        break;

                    case Device.Devices.FaultCode.TempParamSetError:
                        if (item.Value != 0)
                            errMsg += "\r\n温控设备写入参数值失败!   " + item.Value.ToString() + " 次";
                        break;

                    case Device.Devices.FaultCode.RelayError:
                        if (item.Value != 0)
                            errMsg += "\r\n继电器设备写入状态失败!   " + item.Value.ToString() + " 次";
                        break;

                    case Device.Devices.FaultCode.SensorError:
                        if (item.Value != 0)
                            errMsg += "\r\n传感器设备读取数值失败!   " + item.Value.ToString() + " 次";
                        break;

                    case Device.Devices.FaultCode.TempNotDown:
                        if (item.Value != 0)
                            errMsg += "\r\n温度不下降警报!   " + item.Value.ToString() + " 次";
                        break;

                    case Device.Devices.FaultCode.TempNotUp:
                        if (item.Value != 0)
                            errMsg += "\r\n温度不上升警报!   " + item.Value.ToString() + " 次";
                        break;

                    case Device.Devices.FaultCode.TempFlucLarge:
                        if (item.Value != 0)
                            errMsg += "\r\n温度波动过大警报!   " + item.Value.ToString() + " 次";
                        break;

                    case Device.Devices.FaultCode.TempBasis:
                        if (item.Value != 0)
                            errMsg += "\r\n温度偏离设定点过大警报!   " + item.Value.ToString() + " 次";
                        break;

                    case Device.Devices.FaultCode.CodeError:
                        if (item.Value != 0)
                            errMsg += "\r\n代码错误!   " + item.Value.ToString() + " 次";
                        break;

                    case Device.Devices.FaultCode.TempOutRange:
                        if (item.Value != 0)
                            errMsg += "\r\n代码错误!   " + item.Value.ToString() + " 次";
                        break;

                    default:
                        break;
                }
            }
            this.textBox_errMessage.Text = errMsg;
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

            Console.WriteLine("formAlarm.timer");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Utils.Logger.Sys("用户点击关闭了报警窗口");
            this.Close();
        }
    }
}
