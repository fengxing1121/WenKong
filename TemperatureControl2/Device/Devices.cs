using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;

namespace Device
{
    /// <summary>
    /// 主槽控温设备
    /// </summary>
    public class Devices
    {
        /// <summary>
        /// relay 继电器设备
        /// </summary>
        public RelayDevice ryDevice = new RelayDevice();

        public TempDevice tpDeviceM = new TempDevice();
        public TempDevice tpDeviceS = new TempDevice();

        // 定时器设备
        public bool tpMainStart = false;
        public bool tpSubStart = false;
        public Timer tpTemperatureUpdateTimer = new Timer();

        public Devices()
        {
            tpTemperatureUpdateTimer.Elapsed += TpTemperatureUpdateTimer_Elapsed;
            tpTemperatureUpdateTimer.AutoReset = true;
            tpTemperatureUpdateTimer.Interval = 2000;
        }

        public delegate void TpTemperatureUpdateTimerEventHandler(bool err);
        public event TpTemperatureUpdateTimerEventHandler TpTemperatureUpdateTimerEvent;

        // 定时器触发函数
        private void TpTemperatureUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(tpMainStart == true)
            {
                float val1,val2;
                tpDeviceM.GetTemperatureShow( out val1);
                tpDeviceM.GetPowerShow(out val2);
                Debug.WriteLine("温度显示值： " + val1.ToString());
            }

            if(tpSubStart == true)
            {
                float val1, val2;
                tpDeviceS.GetTemperatureShow(out val1);
                tpDeviceS.GetPowerShow(out val2);
            }

            TpTemperatureUpdateTimerEvent(false);
        }


        // 设置主槽控温读取温度定时器
        public void startTemperatureUpdateM( bool st)
        {
            tpMainStart = st;

            if(st == true)
            {
                // 打开定时器
                if (tpTemperatureUpdateTimer.Enabled == false)
                    tpTemperatureUpdateTimer.Start();
            }
            else
            {
                // 如果辅槽控温也关闭了，则关闭定时器
                if (tpSubStart == false)
                    tpTemperatureUpdateTimer.Stop();
            }
        }

        // 设置主槽控温读取温度定时器
        public void startTemperatureUpdateS(bool st)
        {
            tpSubStart = st;

            if (st == true)
            {
                // 打开定时器
                if (tpTemperatureUpdateTimer.Enabled == false)
                    tpTemperatureUpdateTimer.Start();
            }
            else
            {
                // 如果辅槽控温也关闭了，则关闭定时器
                if (tpMainStart == false)
                    tpTemperatureUpdateTimer.Stop();
            }

        }
    }

}
