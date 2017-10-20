using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;
using System.IO;

namespace Device
{
    /// <summary>
    /// 主槽控温设备
    /// </summary>
    public class Devices
    {
        #region Members
        /// <summary>
        /// relay 继电器设备
        /// </summary>
        public RelayDevice ryDevice = new RelayDevice();
        /// <summary>
        /// 主槽温控设备
        /// </summary>
        public TempDevice tpDeviceM = new TempDevice();
        /// <summary>
        /// 辅槽控温设备
        /// </summary>
        public TempDevice tpDeviceS = new TempDevice();
        /// <summary>
        /// 传感器设备
        /// </summary>
        public SensorDevice srDevice = new SensorDevice();
        /// <summary>
        /// 定时器，用于每隔一段时间从设备读取温度显示值，并在主界面中更新
        /// </summary>
        public Timer tpTemperatureUpdateTimer = new Timer();

        #endregion

        #region Public Methods
        /// <summary>
        /// （通过配置文件）配置设备参数
        /// </summary>
        /// <param name="configFilePath">配置文件路径</param>
        /// <returns></returns>
        public bool Configure(string configFilePath = @"./config.ini")
        {
            // 配置成功标志位
            bool confOK = true;


            // 如果配置文件不存在，则新建
            if(!File.Exists(configFilePath))
            {
                // 主槽控温设备
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceM", "COM", "COM1");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceM", "FlucThr", "0.01");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceM", "TempThr", "0.01");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceM", "ReadInterval", "1000");

                // 辅槽控温设备
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceS", "COM", "COM2");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceS", "FlucThr", "0.01");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceS", "TempThr", "0.01");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceS", "ReadInterval", "1000");

                // 继电器设备
                Utils.IniReadWrite.INIWriteValue(configFilePath, "relayDevice", "COM", "COM3");

                // 传感器设备
                // wghou
                // code

            }


            // 配置参数
            // 主槽控温设备
            confOK &= tpDeviceM.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceM", "COM", "COM1"));
            // 这里做的不好，没有把命令统一一下
            // 有待改进吧
            // 设置波动度阈值
            tpDeviceM.tpParam[7] = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceM", "FlucThr", "0.01"));
            // 设置温度阈值
            tpDeviceM.tpParam[8] = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceM", "TempThr", "0.01"));
            // 温度读取时间间隔
            tpDeviceM.readTempInterval = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceM", "ReadInterval", "1000"));


            // 辅槽控温设备
            confOK &= tpDeviceS.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceS", "COM", "COM2"));
            // 这里做的不好，没有把命令统一一下
            // 有待改进吧
            // 设置波动度阈值
            tpDeviceS.tpParam[7] = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceS", "FlucThr", "0.01"));
            // 设置温度阈值
            tpDeviceS.tpParam[8] = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceS", "TempThr", "0.01"));
            // 温度读取时间间隔
            tpDeviceS.readTempInterval = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceS", "ReadInterval", "1000"));


            // 继电器设备
            confOK &= ryDevice.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "relayDevice", "COM", "COM3"));


            // 传感器设备
            // wghou
            // code
            


            // 返回初始化状态
            return confOK;
        }

        /// <summary>
        /// 设备自检，只有设备初始工作状态正确，才能启动软件，进行实验
        /// </summary>
        /// <returns>设备状态是否正确</returns>
        public bool DeviceSelfCheck()
        {
            // 自检成功标志位
            bool checkOK = true;


            // 设备自检
            // wghou
            // code


            // 返回自检状态
            return checkOK;
        }
        #endregion

        /// <summary>
        /// 主槽控温设备工作状态
        /// </summary>
        public bool tpMainStart = false;
        /// <summary>
        /// 辅槽温控设备工作状态
        /// </summary>
        public bool tpSubStart = false;

        
        // 构造函数
        public Devices()
        {
            tpTemperatureUpdateTimer.Elapsed += TpTemperatureUpdateTimer_Elapsed;
            tpTemperatureUpdateTimer.AutoReset = true;
            tpTemperatureUpdateTimer.Interval = 2000;
        }

        // 自定义定时器事件，当执行完定时器触发函数后，触发该事件
        public delegate void TpTemperatureUpdateTimerEventHandler(bool err);
        public event TpTemperatureUpdateTimerEventHandler TpTemperatureUpdateTimerEvent;

        // 定时器触发函数 - 定时结束时执行该函数
        private void TpTemperatureUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(tpMainStart == true)
            {
                float val1 = 0.0f,val2 = 0.0f;
                //tpDeviceM.GetTemperatureShow( out val1);
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

            // 自动运行时调度设备控制状态
            controlFlowSchedule(false);
        }


        /// <summary>
        /// 设置主槽控温读取温度定时器
        /// </summary>
        /// <param name="st"></param>
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



        /// <summary>
        /// 设置主槽控温读取温度定时器
        /// </summary>
        /// <param name="st"></param>
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


        #region 自动控制部分
        /// <summary>
        /// 系统所处于的工作状态
        /// </summary>
        public struct StateFlow
        {
            /// <summary>
            /// 当前的工作状态
            /// </summary>
            public State flowState;
            /// <summary>
            /// 是否是新改变了工作状态
            /// </summary>
            public bool stateChanged;
            /// <summary>
            /// 当前工作状态下的应达到的温度值
            /// </summary>
            public float stateTemp;
            /// <summary>
            /// 当前工作状态的持续时间
            /// </summary>
            public int stateTime;
        }


        /// <summary>
        /// 系统工作状态，对应不同的继电器通断组合
        /// </summary>
        public enum State
        {
            /// <summary>
            /// 升温状态
            /// </summary>
            TempUp = 0,
            /// <summary>
            /// 降温状态
            /// </summary>
            TempDown,
            /// <summary>
            /// 控温状态
            /// </summary>
            TempControl,
            /// <summary>
            /// 稳定状态
            /// </summary>
            TempStable,
            /// <summary>
            /// 测量状态
            /// </summary>
            Measure,
            /// <summary>
            /// 流程结束状态
            /// </summary>
            Finish,
            /// <summary>
            /// 首次开始状态
            /// </summary>
            Start,
            /// <summary>
            /// 暂停状态
            /// </summary>
            Pause,
            /// <summary>
            /// 手动工作状态，不一定会用得到
            /// </summary>
            Mannual
        }


        /// <summary>
        /// 系统自动状态下，完成整个实验所需的状态序列
        /// </summary>
        public List<StateFlow> controlFlowList = new List<StateFlow>();
        /// <summary>
        /// 当前工作状态
        /// </summary>
        public StateFlow currentState;


        /// <summary>
        /// 温控系统状态调度函数，用于自动状态下系统设备的状态调度
        /// </summary>
        /// <param name="auto">是否进行自动流程控制</param>
        private void controlFlowSchedule( bool auto)
        {
            // 如果 xx 则不执行状态调度

            if (!auto)
                return;

            // 调试信息
            Debug.WriteLine("当前工作状态： " + currentState.flowState.ToString());

            // 判断当前工作状态，执行不同的操作流程
            switch(currentState.flowState)
            {
                case State.TempUp:
                    // 升温
                    TempUpStep();
                    break;

                case State.TempDown:
                    // 降温
                    TempDownStep();
                    break;

                case State.TempControl:
                    // 控温
                    TempControlStep();
                    break;

                case State.TempStable:
                    // 稳定
                    TempStableStep();
                    break;

                case State.Measure:
                    // 测量
                    MeasureStep();
                    break;

                case State.Finish:
                    // 完成
                    FinishStep();
                    break;

                case State.Start:
                    // 开始
                    StartStep();
                    break;

                case State.Pause:
                    // 暂停
                    PauseStep();
                    break;

                case State.Mannual:
                    // 手动状态，不知道会不会使用
                    MannualStep();
                    break;

                default:
                    // 通常不会出现该情况
                    Debug.WriteLine("控温流程 case: default ");
                    break;
            }

            // 这里是否应处理相应的错误信息
            // wghou
            // code

        }


        /// <summary>
        /// 自动 - 升温
        /// </summary>
        private void TempUpStep()
        {
            // 判断自动流程是否首次进入该状态
            if(currentState.stateChanged == true)
            {
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 - 快加热
                bool[] st = { true, true, true, false, false, false, false, false, false };
                st.CopyTo(ryDevice.ryStatusToSet, 0);
                // 将继电器状态写入下位机
                ryDevice.UpdateStatusToDevice();

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;
            }
            
            // 继续执行相应的操作

            // 温度是不是应该在一定的范围内就可以啊
            // wghou
            if(tpDeviceM.temperatures.Last() > currentState.stateTemp)
            {
                // 如果主槽中温度高于设定值，则进入下一个状态 - 控温

                // 首先，确保此时控制列表队列不能为空
                Trace.Assert(controlFlowList.Count != 0);

                // 读取列表中的第一个状态相，并将其从列表中移除
                currentState = controlFlowList.First();
                controlFlowList.RemoveAt(0);

                // 确保该状态为控温
                Trace.Assert(currentState.flowState == State.TempControl);

                // 该状态置为首次改变
                currentState.stateChanged = true;
            }


            // 是否还有其他相关操作
            // wghou
            // code
            // 故障判断 - 温度不升高 等

        }


        /// <summary>
        /// 自动 - 降温
        /// </summary>
        private void TempDownStep()
        {
            // 判断自动流程是否首次进入该状态
            if (currentState.stateChanged == true)
            {
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 4 5 6 7 - 到达温度点关闭 6 和 7
                bool[] st = { true, true, true, true, true, true, true, false, false };
                st.CopyTo(ryDevice.ryStatusToSet, 0);
                // 将继电器状态写入下位机
                ryDevice.UpdateStatusToDevice();

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;
            }

            // 继续执行相应的操作

            // 降温的时候，是不是温度下降到一定范围就可以进入下一个状态了
            if(tpDeviceM.temperatures.Last() < currentState.stateTemp)
            {
                // 进入下一个状态，下一个状态应该是 控温

                // 首先，确保此时控制列表队列不能为空
                Trace.Assert(controlFlowList.Count != 0);

                // 读取列表中的第一个状态项目，并将其从列表中移除
                currentState = controlFlowList.First();
                controlFlowList.RemoveAt(0);

                // 确保该状态为控温
                Trace.Assert(currentState.flowState == State.TempControl);

                // 该状态置为首次改变
                currentState.stateChanged = true;
            }

            // 是否还有其他相关操作
            // wghou
            // code
            // 故障判断 - 温度不下降 / 温度持续上升 等
        }


        /// <summary>
        /// 自动 - 控温
        /// </summary>
        private void TempControlStep()
        {
            // 判断自动流程是否首次进入该状态
            if (currentState.stateChanged == true)
            {
                // 首次进入该状态，应改变相应的继电器状态
                //  1 2 3 4 5 
                bool[] st = { true, true, true, true, true, false, false, false, false };
                st.CopyTo(ryDevice.ryStatusToSet, 0);
                // 将继电器状态写入下位机
                ryDevice.UpdateStatusToDevice();

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;
            }

            // 继续执行相应的操作

            // 控温需要做什么？？什么条件下进入下一个状态？？
            if(true)
            {
                // 进入下一个状态，下一个状态应该是 稳定

                // 首先，确保此时控制列表队列不能为空
                Trace.Assert(controlFlowList.Count != 0);

                // 读取列表中的第一个状态相，并将其从列表中移除
                currentState = controlFlowList.First();
                controlFlowList.RemoveAt(0);

                // 确保该状态为控温
                Trace.Assert(currentState.flowState == State.TempStable);

                // 该状态置为首次改变
                currentState.stateChanged = true;
            }

            // 是否还有其他相关操作
            // wghou
            // code
            // 故障判断 - 温度波动过大 / 温度持续下降 等
        }


        /// <summary>
        /// 自动 - 稳定
        /// </summary>
        private void TempStableStep()
        {
            // 判断自动流程是否首次进入该状态
            if (currentState.stateChanged == true)
            {
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 4 5 - 电桥 - 温度波动 <= 0.0005 C / 3 min
                bool[] st = { true, true, true, true, true, false, false, false, false };
                st.CopyTo(ryDevice.ryStatusToSet, 0);
                // 将继电器状态写入下位机
                ryDevice.UpdateStatusToDevice();

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;
            }

            // 继续执行相应的操作

            // 稳定状态下，是不是要不断判断温度值是否达到要求，判断波动度是否达到要求
            float fluc = 1;
            tpDeviceM.GetFluc(90,out fluc);
            if(fluc < 0.0005f)
            {
                // 温度稳定度达到了要求，进入下一个状态 - 测量

                // 首先，确保此时控制列表队列不能为空
                Trace.Assert(controlFlowList.Count != 0);

                // 读取列表中的第一个状态相，并将其从列表中移除
                currentState = controlFlowList.First();
                controlFlowList.RemoveAt(0);

                // 确保该状态为控温
                Trace.Assert(currentState.flowState == State.Measure);

                // 该状态置为首次改变
                currentState.stateChanged = true;
            }

            // 是否还有其他相关操作
            // wghou
            // code
            // 故障判断 - 温度波动过大 / 温度持续下降 等
        }


        /// <summary>
        /// 自动 - 测量
        /// </summary>
        private void MeasureStep()
        {
            // 判断自动流程是否首次进入该状态
            if (currentState.stateChanged == true)
            {
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 4 5 - 电导率测量 - 海水取样
                bool[] st = { true, true, true, false, false, false, false, false, false };
                st.CopyTo(ryDevice.ryStatusToSet, 0);
                // 将继电器状态写入下位机
                ryDevice.UpdateStatusToDevice();

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;
            }

            // 继续执行相应的操作

            // 这里是不是读取相应的参数并记录就完成任务了

            // 进入下一个状态

            // 首先，判断此时控制列表队列是否为空
            if( controlFlowList.Count != 0)
            {
                // 控制状态序列不为空，说明实验还没有结束

                // 读取列表中的第一个状态相，并将其从列表中移除
                currentState = controlFlowList.First();
                controlFlowList.RemoveAt(0);

                // 确保该状态为控温
                Trace.Assert(currentState.flowState == State.TempDown);

                // 该状态置为首次改变
                currentState.stateChanged = true;
            }
            else
            {
                // 控制状态序列为空，说明实验已经结束了
                currentState = new StateFlow() { flowState = State.Finish, stateChanged = true, stateTemp = 0.0f, stateTime = 0 };

            }



            // 是否还有其他相关操作
            // wghou
            // code
            // 是否也有故障判断
        }

        /// <summary>
        /// 自动 - 实验完成
        /// </summary>
        private void FinishStep()
        {
            // 完成测量

            // 执行相应的操作
            // wghou
            // code

        }


        /// <summary>
        /// 自动 - 首次开始
        /// </summary>
        private void StartStep()
        {

        }

        /// <summary>
        /// 自动 - 暂停
        /// </summary>
        private void PauseStep()
        {

        }


        /// <summary>
        /// 手动状态
        /// </summary>
        private void MannualStep()
        {

        }



        #endregion
        // end
    }

}
