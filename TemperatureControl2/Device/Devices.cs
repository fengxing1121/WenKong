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
        struct StateFlow
        {
            public State flowState;
            public bool stateChanged;
            public float stateTemp;
            public int stateTime;
        }

        enum State
        {
            TempUp = 0,
            TempControl,
            TempStable,
            Measure,
            TempDown,
            Finish,
            Start
        }

        List<StateFlow> controlFlowList = new List<StateFlow>();
        StateFlow currentState;

        void controlFlowTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            switch(currentState.flowState)
            {
                // 温度上升
                case State.TempUp:
                    {
                        // 状态初次改变
                        if(currentState.stateChanged == true)
                        {
                            currentState.stateChanged = false;
                            // 设置继电器状态
                            // wghou
                            // code

                            // 读取温度数据
                            // wghou
                            // code
                        }
                        // 状态持续中
                        else
                        {
                            // 读取温度数据
                            // wghou
                            // code


                            // 如果温度达到设定温度值，则进入下一个状态
                            // wghou
                            // code
                            currentState = controlFlowList[0]; // 下一个状态
                        }
                    }
                    break;


                case State.TempControl:

                    break;
            }
        }


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
                Debug.WriteLine("温度显示值： " + val1.ToString());
            }

            // 触发事件 - 错误状态为 false - 没有发生错误
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
