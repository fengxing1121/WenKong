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
        #region 设备部分及定时器部分
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
        // 自定义定时器事件，当执行完定时器触发函数后，触发该事件
        public delegate void TpTemperatureUpdateTimerEventHandler(TempProtocol.Err_t err);
        /// <summary>
        /// 温度更新定时器函数中读取温度事件 - 通知主界面更新温度等数据
        /// </summary>
        public event TpTemperatureUpdateTimerEventHandler TpTemperatureUpdateTimerEvent;
        #endregion Members



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
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceM", "ReadInterval", "2000");

                // 辅槽控温设备
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceS", "COM", "COM2");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceS", "FlucThr", "0.01");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceS", "TempThr", "0.01");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "tempDeviceS", "ReadInterval", "2000");

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
            tpDeviceM.readTempInterval = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceM", "ReadInterval", "2000"));
            Debug.WriteLineIf(!confOK, "配置主槽控温设备失败! 端口号: " + tpDeviceM.tpDevicePortName);
            Debug.WriteLineIf(confOK, "配置主槽控温设备成功! 端口号: " + tpDeviceM.tpDevicePortName);
            if (!confOK)
                Utils.Logger.Sys("配置主槽控温设备失败! 端口号: " + tpDeviceM.tpDevicePortName);


            // 辅槽控温设备
            confOK &= tpDeviceS.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceS", "COM", "COM2"));
            // 这里做的不好，没有把命令统一一下
            // 有待改进吧
            // 设置波动度阈值
            tpDeviceS.tpParam[7] = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceS", "FlucThr", "0.01"));
            // 设置温度阈值
            tpDeviceS.tpParam[8] = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceS", "TempThr", "0.01"));
            // 温度读取时间间隔
            tpDeviceS.readTempInterval = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "tempDeviceS", "ReadInterval", "2000"));
            Debug.WriteLineIf(!confOK, "配置辅槽控温设备失败! 端口号: " + tpDeviceS.tpDevicePortName);
            Debug.WriteLineIf(confOK, "配置辅槽控温设备成功! 端口号: " + tpDeviceS.tpDevicePortName);
            if (!confOK)
                Utils.Logger.Sys("配置辅槽控温设备失败! 端口号: " + tpDeviceS.tpDevicePortName);


            // 继电器设备
            confOK &= ryDevice.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "relayDevice", "COM", "COM3"));
            Debug.WriteLineIf(!confOK, "配置继电器设备失败! 端口号: " + ryDevice.ryDevicePortName);
            Debug.WriteLineIf(confOK, "配置继电器设备成功! 端口号: " + ryDevice.ryDevicePortName);
            if (!confOK)
                Utils.Logger.Sys("配置继电器设备失败! 端口号: " + ryDevice.ryDevicePortName);

            // 传感器设备
            // wghou
            // code


            Debug.WriteLineIf(confOK, "设备串口配置成功!");
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

            // 主槽温控设备自检
            if (tpDeviceM.SelfCheck() != TempProtocol.Err_t.NoError)
            {
                checkOK = false;
                Debug.WriteLine("主槽控温设备自检失败!");
                Utils.Logger.Sys("主槽控温设备自检失败!");
            }
            else
            {
                Debug.WriteLine("主槽控温设备自检成功!");
            }

                
            // 辅槽温控设备自检
            if (tpDeviceS.SelfCheck() != TempProtocol.Err_t.NoError)
            {
                checkOK = false;
                Debug.WriteLine("辅槽控温设备自检失败!");
                Utils.Logger.Sys("辅槽控温设备自检失败!");
            }
            else
            {
                Debug.WriteLine("辅槽控温设备自检成功!");
            }


            // 继电器设备自检
            if (ryDevice.SelfCheck() != RelayProtocol.Err_r.NoError)
            {
                checkOK = false;
                Debug.WriteLine("继电器设备自检失败!");
                Utils.Logger.Sys("继电器设备自检失败!");
            }
            else
            {
                Debug.WriteLine("继电器设备自检成功!");
            }


            // 传感器设备自检
            // wghou
            // code

            Debug.WriteLineIf(checkOK, "设备自检完成!");
            // 返回自检状态
            return checkOK;
        }


        // 构造函数
        public Devices()
        {
            tpTemperatureUpdateTimer.Elapsed += TpTemperatureUpdateTimer_Elapsed;
            tpTemperatureUpdateTimer.AutoReset = true;
            // 默认值，其他地方还会再修改
            tpTemperatureUpdateTimer.Interval = 2000;
        }
        #endregion Public Methods



        #region Private Method
        // 定时器触发函数 - 定时结束时执行该函数
        private void TpTemperatureUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
#if DEBUG
            tpTemperatureUpdateTimer.Stop();
#endif

            // 读取主槽控温表温度值 / 功率系数
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;

            // 读取主槽温度
            float val = 0.0f;
            err = tpDeviceM.GetTemperatureShow( out val);
            if(err != TempProtocol.Err_t.NoError)
            {
                // 如果发生错误，则直接触发事件，向主界面报错，并暂停流程控制
                Debug.WriteLine("读取主槽温度时发生错误，errorCode: " + err.ToString());
                Utils.Logger.Sys("读取主槽温度时发生错误，errorCode: " + err.ToString());
                goto End;
                //TpTemperatureUpdateTimerEvent(err);
                //return;
            }

            // 读取主槽功率系数
            err = tpDeviceM.GetPowerShow(out val);
            if (err != TempProtocol.Err_t.NoError)
            {
                // 如果发生错误，则直接触发事件，向主界面报错，并暂停流程控制
                Debug.WriteLine("读取主槽功率时发生错误，errorCode: " + err.ToString());
                Utils.Logger.Sys("读取主槽功率时发生错误，errorCode: " + err.ToString());
                goto End;
                //TpTemperatureUpdateTimerEvent(err);
                //return;
            }

            // 读取辅槽控温表温度值 / 功率系数
            err = tpDeviceS.GetTemperatureShow(out val);
            if (err != TempProtocol.Err_t.NoError)
            {
                // 如果发生错误，则直接触发事件，向主界面报错，并暂停流程控制
                Debug.WriteLine("读取辅槽温度时发生错误，errorCode: " + err.ToString());
                Utils.Logger.Sys("读取辅槽温度时发生错误，errorCode: " + err.ToString());
                goto End;
                //TpTemperatureUpdateTimerEvent(err);
                //return;
            }

            // 读取主槽功率系数
            err = tpDeviceS.GetPowerShow(out val);
            if (err != TempProtocol.Err_t.NoError)
            {
                // 如果发生错误，则直接触发事件，向主界面报错，并暂停流程控制
                Debug.WriteLine("读取辅槽温度时发生错误，errorCode: " + err.ToString());
                Utils.Logger.Sys("读取辅槽温度时发生错误，errorCode: " + err.ToString());
                goto End;
                //TpTemperatureUpdateTimerEvent(err);
                //return;
            }

            End:
            // 触发事件 - 此时应该没有错误发生
            Debug.WriteLineIf(err == TempProtocol.Err_t.NoError, "从主 / 辅槽温控设备读取温度显示值 / 功率系数完成!");
            TpTemperatureUpdateTimerEvent(err);

            // 如果温度读取发生了错误，则再执行自动控温流程
            if (err != TempProtocol.Err_t.NoError)
                return;

            // 自动运行时调度设备控制状态
            //Debug.WriteLineIf(autoStart, "执行自动运行状态调度...");
            lock(stepLocker)
            {
                controlFlowSchedule(autoStart);
            }

#if DEBUG
            tpTemperatureUpdateTimer.Start();
#endif
        }
        #endregion
        #endregion



        #region 自动控制部分

        #region Public Members
        /// <summary>
        /// 系统所处于的工作状态
        /// </summary>
        public class StateFlow
        {
            /// <summary>
            /// 当前的工作状态
            /// </summary>
            public State flowState = State.Idle;
            /// <summary>
            /// 是否是新改变了工作状态
            /// </summary>
            public bool stateChanged = true;
            /// <summary>
            /// 当前工作状态下的应达到的温度值
            /// </summary>
            public float stateTemp = 0.0f;
            /// <summary>
            /// 当前工作状态的工作时长计时，定时器每循环一次，自动 +1
            /// </summary>
            public int stateTime = 0;
            /// <summary>
            /// 是否为高级状态 - 即是否修改控温参数
            /// </summary>
            public bool advanceState = false;
            /// <summary>
            /// 主槽控温表参数 - 9个
            /// </summary>
            public float[] paramM = new float[9];
            /// <summary>
            /// 辅槽控温表参数 - 9个
            /// </summary>
            public float[] paramS = new float[9];
        }


        /// <summary>
        /// 系统工作状态，对应不同的继电器通断组合
        /// </summary>
        public enum State : int
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
            /// 实验完成
            /// </summary>
            Finish,
            /// <summary>
            /// 流程空闲 / 暂停
            /// </summary>
            Idle
        }

        /// <summary>
        /// 自动控制各流程的名称
        /// </summary>
        public string[] StateName = { "升温", "降温", "控温", "稳定","测量", "完成", "开始", "暂停", "手动" };
        /// <summary>
        /// 系统自动状态下，完成整个实验所需的状态序列
        /// </summary>
        public List<StateFlow> controlFlowList = new List<StateFlow>();
        /// <summary>
        /// 当前工作状态
        /// </summary>
        public StateFlow currentState = new StateFlow() { flowState = State.Idle, stateTemp = 20.0f };
        /// <summary>
        /// 是否开始自动运行
        /// </summary>
        public bool autoStart = false;
        /// <summary>
        /// 锁 - 保证自动控制流程步骤执行的时候，相应资源（例如 currentState autoStart controlFlowList etc. ）不会被访问
        /// </summary>
        public Object stepLocker = new Object();
        
        // 
        public delegate void FlowControlStateChangedEventHandler(State st);
        /// <summary>
        /// 自动操作流程中，进入某一状态时触发的事件
        /// </summary>
        public event FlowControlStateChangedEventHandler FlowControlStateChangedEvent;
        /// <summary>
        /// 自动控制流程执行完毕事件 - 通知主界面完成相应的处理
        /// </summary>
        public event FlowControlStateChangedEventHandler FlowControlFinishEvent;
        /// <summary>
        /// 满足稳定状态后，电导率测量事件 - 通知主界面完成相应的提示
        /// </summary>
        public event FlowControlStateChangedEventHandler FlowControlMeasureEvent;
        #endregion



        #region 故障判断部分
        public enum FaultCode : int
        {
            /// <summary>
            /// 温度不讲
            /// </summary>
            TempNotDown = 0,
            /// <summary>
            /// 温度持续下降
            /// </summary>
            TempContinueDown,
            /// <summary>
            /// 温度波动过大
            /// </summary>
            TempFlucLarge,
            /// <summary>
            /// 温度持续上升
            /// </summary>
            TempContinueUp
        }
        //
        public delegate void FlowControlFaultOccurEventHandler(FaultCode fCode);
        /// <summary>
        /// 自动控温流程中，判断发生故障事件 - 通知主界面完成相应的处理
        /// </summary>
        public event FlowControlFaultOccurEventHandler FlowControlFaultOccurEvent;
        /// <summary>
        /// 故障判断，如果发生故障，则返回 true
        /// </summary>
        /// <param name="fCode"></param>
        /// <returns></returns>
        private bool FaultCheck(int count, FaultCode fCode)
        {
            bool fault = false;
            switch(fCode)
            {
                case FaultCode.TempNotDown:
                    {
                        // 判断温度不下降
                        // 如果 tpDeviceM.temperatures 中存储的温度值过少，即系统运行时间太短，则不检测 
                        //int count = 1 * 60 * 1000 / tpDeviceM.readTempInterval;
                        if (tpDeviceM.temperatures.Count > count)
                        {
                            // 如果当前温度大于 count 个之前的温度值，说明温度没有下降
                            if (tpDeviceM.temperatures.Last() > tpDeviceM.temperatures[tpDeviceM.temperatures.Count - count])
                                fault = true;
                            else
                                fault = false;
                        }
                        else
                        {
                            fault = false;
                        }
                        break;
                    }

                case FaultCode.TempContinueDown:
                    {
                        // 判断温度持续下降
                        // 如果 tpDeviceM.temperatures 中存储的温度值过少，即系统运行时间太短，则不检测 
                        //int count = 1 * 10 * 1000 / tpDeviceM.readTempInterval;
                        if (tpDeviceM.temperatures.Count > count)
                        {
                            // 如果count 个之前的温度值比当前温度大 Device.TempDevice.tpParam[8] (温度阈值)，说明温度持续下降
                            if (tpDeviceM.temperatures[tpDeviceM.temperatures.Count - count] - tpDeviceM.temperatures.Last() > tpDeviceM.tpParam[8])
                                fault = true;
                            else
                                fault = false;
                        }
                        else
                        {
                            fault = false;
                        }
                        break;
                    }

                case FaultCode.TempFlucLarge:
                    {
                        // 判断温度波动大
                        // 如果 tpDeviceM.temperatures 中存储的温度值过少，即系统运行时间太短，则不检测 
                        //int count = 1 * 10 * 1000 / tpDeviceM.readTempInterval;
                        float fluc = 0.0f;
                        // 如果获取波动度大于 Device.TempDevice.tpParam[8] (波动度阈值)，说明波动度过大
                        if (tpDeviceM.GetFluc(count, out fluc) && fluc > tpDeviceM.tpParam[7])
                        {
                            fault = true;
                        }
                        else
                        {
                            fault = false;
                        }
                        break;
                    }

                case FaultCode.TempContinueUp:
                    {
                        // 温度持续上升
                        // 如果 tpDeviceM.temperatures 中存储的温度值过少，即系统运行时间太短，则不检测 
                        //int count = 1 * 10 * 1000 / tpDeviceM.readTempInterval;
                        if (tpDeviceM.temperatures.Count > count)
                        {
                            // 如果count 个之前的温度值比当前温度大 Device.TempDevice.tpParam[8] (温度阈值)，说明温度持续下降
                            if (tpDeviceM.temperatures.Last() - tpDeviceM.temperatures[tpDeviceM.temperatures.Count - count]  > tpDeviceM.tpParam[8])
                                fault = true;
                            else
                                fault = false;
                        }
                        else
                        {
                            fault = false;
                        }
                        break;
                    }

                default:
                    fault = false;
                    break;
            }
            return fault;
        }
        #endregion 故障判断部分



        #region Private Methods
        /// <summary>
        /// 温控系统状态调度函数，用于自动状态下系统设备的状态调度
        /// </summary>
        /// <param name="auto">是否进行自动流程控制</param>
        private void controlFlowSchedule( bool auto)
        {
            // 如果没有开启自动状态，则不执行状态调度
            if (!auto)
                return;

            // 当前状态工作时间计时
            currentState.stateTime++;

            // 判断当前工作状态，执行不同的操作流程
            switch (currentState.flowState)
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

                case State.Idle:
                    // 空闲
                    IdleStep();
                    break;

                default:
                    // 通常不会出现该情况
                    Debug.WriteLine("控温流程 case: default ");
                    Utils.Logger.Sys("异常：控温流程 case: default");
                    break;
            }


            // 在多长的时间段内检测故障
            // 还是担心的那些事情，如果刚刚进入某个状态，特别容易报错
            int count = 1 * 60 * 1000 / tpDeviceM.readTempInterval;
            // 主槽报警及故障判断
            switch(currentState.flowState)
            {
                case State.TempUp:
                    {
                        // 故障判断 - 温度不升高 / 温度持续下降
                        // wghou
                        // 在升温过程中，需要判断该故障吗？
                        // 有点担心，当首次进入该状态时，特别容易检测到故障
                        if (currentState.stateTime < count)
                            break;
                        if (FaultCheck(count,FaultCode.TempContinueDown))
                            FlowControlFaultOccurEvent(FaultCode.TempContinueDown);
                        break;
                    }
                case State.TempDown:
                    {
                        // 故障判断 - 温度不下降 / 温度持续上升
                        // wghou
                        // 有点担心，当首次进入该状态时，特别容易检测到故障
                        if (currentState.stateTime < count)
                            break;
                        if (FaultCheck(count, FaultCode.TempNotDown))
                            FlowControlFaultOccurEvent(FaultCode.TempNotDown);
                        break;
                    }
                case State.TempControl:
                    {
                        // 故障判断 - 温度持续下降 / 温度持续上升 / 温度波动过大
                        // wghou
                        // 在控温的过程中，需要判断温度波动过大吗？
                        // 有点担心，当首次进入该状态时，特别容易检测到故障
                        if (currentState.stateTime < count)
                            break;
                        if (FaultCheck(count, FaultCode.TempContinueDown))
                            FlowControlFaultOccurEvent(FaultCode.TempContinueDown);
                        if (FaultCheck(count, FaultCode.TempContinueUp))
                            FlowControlFaultOccurEvent(FaultCode.TempContinueUp);
                        if (FaultCheck(count, FaultCode.TempFlucLarge))
                            FlowControlFaultOccurEvent(FaultCode.TempFlucLarge);
                        break;
                    }
                case State.TempStable:
                    {
                        // 故障判断？？
                        // 温度波动过大
                        // wghou
                        // 有点担心，当首次进入该状态时，特别容易检测到故障
                        if (currentState.stateTime < count)
                            break;
                        if (FaultCheck(count, FaultCode.TempFlucLarge))
                            FlowControlFaultOccurEvent(FaultCode.TempFlucLarge);
                        break;
                    }
                default:
                    break;
            }

            // 调试信息
            Debug.WriteLine("当前工作状态： " + currentState.flowState.ToString());
        }


        /// <summary>
        /// 自动 - 升温
        /// </summary>
        private void TempUpStep()
        {
            Trace.Assert(tpDeviceM.temperatures.Count != 0, "tpDeviceM.temperatres.Count 的值为 0 ", "程序出现错误");

            // 首次进入该状态，应改变相应的继电器状态
            // 判断自动流程是否首次进入该状态
            if (currentState.stateChanged == true)
            {
#if false
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 - 快加热
                bool[] st = { true, true, true, false, false, false, false, false, false };
                st.CopyTo(ryDevice.ryStatusToSet, 0);
#else
                // 首先判断是执行升温，还是降温 - 通过判断实际温度，再次确定一下
                // 当前温度小于设定温度，则升温；当前温度大于设定温度，则降温
                if (tpDeviceM.temperatures.Last() < currentState.stateTemp)
                {
                    // 升温
                    currentState.flowState = State.TempUp;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.Elect] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainHeat] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubHeat] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCool] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCircle] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainCoolF] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterIn] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterOut] = false;
                }
                else
                {
                    // 降温
                    currentState.flowState = State.TempDown;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.Elect] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainHeat] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubHeat] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCool] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCircle] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainCoolF] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterIn] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterOut] = false;
                }
#endif

                // 将继电器状态写入下位机
                // 如果出现错误，则暂时只通过其事件触发函数，在主界面中显示错误提示，未作其他处理
                ryDevice.UpdateStatusToDevice();

                // 设置主槽 / 辅槽控温设备的参数
                if(currentState.advanceState == true)
                {
                    // 向主槽 / 辅槽控温设备写入全部参数
                    currentState.paramM.CopyTo(tpDeviceM.tpParamToSet, 0);
                    currentState.paramS.CopyTo(tpDeviceS.tpParamToSet, 0);
                    tpDeviceM.UpdateParamToDevice();
                    tpDeviceS.UpdateParamToDevice();
                }
                else
                {
                    // 只向主槽 / 辅槽控温设备写入温度设定值
                    tpDeviceM.tpParamToSet[(int)Device.TempProtocol.Cmd_t.TempSet] = currentState.stateTemp;
                    tpDeviceM.UpdateParamToDevice();
                    tpDeviceS.tpParamToSet[(int)Device.TempProtocol.Cmd_t.TempSet] = currentState.stateTemp;
                    tpDeviceS.UpdateParamToDevice();
                }
                
                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;

                // 首次进入某一状态
                // 触发事件 - 例如，可以通知主界面执行一些操作
                FlowControlStateChangedEvent(currentState.flowState);

                Debug.WriteLine("当前进入工作状态： " + currentState.flowState.ToString());
                Utils.Logger.Sys("自动控温流程，进入 " + currentState.flowState.ToString() + " 状态...");

                // 首次进入该状态，不进行其他判断（判断是否满足一定条件，需要转换状态），直接返回
                return;
            }

            // 继续执行相应的操作

            // 温度达到设定值，即停止升温，进入控温状态
            if(tpDeviceM.temperatures.Last() > currentState.stateTemp)
            {
                // 如果主槽中温度高于设定值，则进入下一个状态 - 控温

                // 首先，确保此时控制列表队列不能为空
                Trace.Assert(controlFlowList.Count != 0,"controlFlowList 为空","程序出现错误，请终止程序运行！");

                // 读取列表中的第一个状态相，并将其从列表中移除
                currentState = controlFlowList.First();
                controlFlowList.RemoveAt(0);

                // 确保该状态为控温
                Trace.Assert(currentState.flowState == State.TempControl,"TempUp 之后状态不为 TempControl", "程序出现错误");

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
            Trace.Assert(tpDeviceM.temperatures.Count != 0, "tpDeviceM.temperatres.Count 的值为 0 ", "程序出现错误");

            // 判断自动流程是否首次进入该状态
            if (currentState.stateChanged == true)
            {
#if false
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 4 5 6 7 - 到达温度点关闭 6 和 7
                bool[] st = { true, true, true, true, true, true, true, false, false };
                st.CopyTo(ryDevice.ryStatusToSet, 0);
                // 将继电器状态写入下位机
#else
                // 首先判断是执行升温，还是降温 - 通过判断实际温度，再次确定一下
                // 当前温度小于设定温度，则升温；当前温度大于设定温度，则降温
                if (tpDeviceM.temperatures.Last() < currentState.stateTemp)
                {
                    // 升温
                    currentState.flowState = State.TempUp;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.Elect] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainHeat] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubHeat] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCool] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCircle] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainCoolF] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterIn] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterOut] = false;
                }
                else
                {
                    // 降温
                    currentState.flowState = State.TempDown;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.Elect] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainHeat] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubHeat] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCool] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCircle] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainCoolF] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = true;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterIn] = false;
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterOut] = false;
                }
#endif
                ryDevice.UpdateStatusToDevice();

                // 设置主槽 / 辅槽控温设备的参数
                if (currentState.advanceState == true)
                {
                    // 向主槽 / 辅槽控温设备写入全部参数
                    currentState.paramM.CopyTo(tpDeviceM.tpParamToSet, 0);
                    currentState.paramS.CopyTo(tpDeviceS.tpParamToSet, 0);
                    tpDeviceM.UpdateParamToDevice();
                    tpDeviceS.UpdateParamToDevice();
                }
                else
                {
                    // 只向主槽 / 辅槽控温设备写入温度设定值
                    tpDeviceM.tpParamToSet[(int)Device.TempProtocol.Cmd_t.TempSet] = currentState.stateTemp;
                    tpDeviceM.UpdateParamToDevice();
                    tpDeviceS.tpParamToSet[(int)Device.TempProtocol.Cmd_t.TempSet] = currentState.stateTemp;
                    tpDeviceS.UpdateParamToDevice();
                }

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;

                // 首次进入某一状态
                // 触发事件 - 例如，可以通知主界面执行一些操作
                FlowControlStateChangedEvent(currentState.flowState);

                Debug.WriteLine("当前进入工作状态： " + currentState.flowState.ToString());
                Utils.Logger.Sys("自动控温流程，进入 " + currentState.flowState.ToString() + " 状态...");

                // 首次进入该状态，不进行其他判断（判断是否满足一定条件，需要转换状态），直接返回
                return;
            }

            // 继续执行相应的操作

            // 温度下降到温度设定点以下，则立即进入下一个状态
            if (tpDeviceM.temperatures.Last() < currentState.stateTemp)
            {
                // 进入下一个状态，下一个状态应该是 控温

                // 首先，确保此时控制列表队列不能为空
                Trace.Assert(controlFlowList.Count != 0, "controlFlowList 为空", "程序出现错误");

                // 读取列表中的第一个状态项目，并将其从列表中移除
                currentState = controlFlowList.First();
                controlFlowList.RemoveAt(0);

                // 确保该状态为控温
                Trace.Assert(currentState.flowState == State.TempControl, "TempUp 之后状态不为 TempControl", "程序出现错误");

                // 该状态置为首次改变
                currentState.stateChanged = true;
            }


            // 是否还有其他相关操作
            // wghou
            // code
        }


        /// <summary>
        /// 自动 - 控温
        /// </summary>
        private void TempControlStep()
        {
            // 判断自动流程是否首次进入该状态
            if (currentState.stateChanged == true)
            {
#if false
                // 首次进入该状态，应改变相应的继电器状态
                //  1 2 3 4 5 
                bool[] st = { true, true, true, true, true, false, false, false, false };

                // 如果温度设定点在 5 度以下，则开启 辅槽快冷 7 
                if (currentState.stateTemp < 5.0f)
                    st[6] = true;

                st.CopyTo(ryDevice.ryStatusToSet, 0);
#else
                // 首次进入该状态，应改变相应的继电器状态
                //  1 2 3 4 5 
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.Elect] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainHeat] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubHeat] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCool] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCircle] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainCoolF] = false;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = false;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterIn] = false;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterOut] = false;
                // 如果温度设定点在 5 度以下，则开启 辅槽快冷 7 
                if (currentState.stateTemp < 5.0f)
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = true;
#endif

                // 将继电器状态写入下位机
                ryDevice.UpdateStatusToDevice();

                // wghou
                // 这里是否要再次对主槽 / 辅槽的设定温度进行写入确认？

                // 首次进入某一状态
                // 触发事件 - 例如，可以通知主界面执行一些操作
                FlowControlStateChangedEvent(currentState.flowState);

                Debug.WriteLine("当前进入工作状态： " + currentState.flowState.ToString());
                Utils.Logger.Sys("自动控温流程，进入 " + currentState.flowState.ToString() + " 状态...");

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;

                // 首次进入该状态，不进行其他判断（判断是否满足一定条件，需要转换状态），直接返回
                return;
            }

            // 继续执行相应的操作

            // 控温状态下，温度波动度满足判断条件，则立即进入下一状态 - 3 分钟波动度小于 0.0005
            bool steady = tpDeviceM.chekFluc(3 * 60, 0.0005f);
            if (steady)
            {
                // 进入下一个状态，下一个状态应该是 稳定

                // 首先，确保此时控制列表队列不能为空
                Trace.Assert(controlFlowList.Count != 0, "controlFlowList 为空", "程序出现错误");

                // 读取列表中的第一个状态相，并将其从列表中移除
                currentState = controlFlowList.First();
                controlFlowList.RemoveAt(0);

                // 确保该状态为控温
                Trace.Assert(currentState.flowState == State.TempStable, "TempUp 之后状态不为 TempStable", "程序出现错误");

                // 该状态置为首次改变
                currentState.stateChanged = true;

                Utils.Logger.Sys("温度波动度满足 xxx 条件，继续等待 2 分钟...");
            }


            // 是否还有其他相关操作
            // wghou
            // code
        }


        /// <summary>
        /// 自动 - 稳定
        /// </summary>
        private void TempStableStep()
        {
            // 判断自动流程是否首次进入该状态
            if (currentState.stateChanged == true)
            {
#if false
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 4 5 - 电桥 - 温度波动 <= 0.0005 C / 3 min
                bool[] st = { true, true, true, true, true, false, false, false, false };

                // 如果温度设定点在 5 度以下，则开启 辅槽快冷 7 
                if (currentState.stateTemp < 5.0f)
                    st[6] = true;

                st.CopyTo(ryDevice.ryStatusToSet, 0);
#else
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 4 5 - 电桥 - 温度波动 <= 0.0005 C / 3 min
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.Elect] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainHeat] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubHeat] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCool] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCircle] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainCoolF] = false;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = false;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterIn] = false;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterOut] = false;
                // 如果温度设定点在 5 度以下，则开启 辅槽快冷 7 
                if (currentState.stateTemp < 5.0f)
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = true;
#endif
                // 将继电器状态写入下位机
                ryDevice.UpdateStatusToDevice();

                // 首次进入某一状态
                // 触发事件 - 例如，可以通知主界面执行一些操作
                FlowControlStateChangedEvent(currentState.flowState);

                Debug.WriteLine("当前进入工作状态： " + currentState.flowState.ToString());
                Utils.Logger.Sys("自动控温流程，进入 " + currentState.flowState.ToString() + " 状态...");

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;

                // 首次进入该状态，不进行其他判断（判断是否满足一定条件，需要转换状态），直接返回
                return;
            }

            // 继续执行相应的操作

            // 进入稳定状态后，等待 2 分钟后，判断电桥 3 分钟稳定度小于 0.0005，满足则测量
            if(currentState.stateTime > 2 * 60 * 1000 / tpDeviceM.readTempInterval)
            {
                // 判断温度波动度
                if(tpDeviceM.chekFluc(2*60,0.0005f))
                {
                    // 满足波动度判断条件

                    // 温度稳定度达到了要求，进入下一个状态 - 测量
                    // 首先，确保此时控制列表队列不能为空
                    Trace.Assert(controlFlowList.Count != 0, "controlFlowList 为空", "程序出现错误");

                    // 读取列表中的第一个状态相，并将其从列表中移除
                    currentState = controlFlowList.First();
                    controlFlowList.RemoveAt(0);

                    // 确保该状态为测量
                    Trace.Assert(currentState.flowState == State.Measure, "TempUp 之后状态不为 Measure", "程序出现错误");

                    // 该状态置为首次改变
                    currentState.stateChanged = true;

                    Utils.Logger.Sys("2 分钟后，温度波动度满足 xxx 条件，可以测量电导率等数据...");
                }
                else
                {
                    // 不满足波动度判断条件，清空当前流程状态计时，再等待 2 分钟
                    currentState.stateTime = 0;

                    Utils.Logger.Sys("2 分钟后，温度波动度不满足 xxx 条件，继续等待 2 分钟...");
                }
            }


            // 是否还有其他相关操作
            // wghou
            // code
        }


        /// <summary>
        /// 自动 - 测量
        /// </summary>
        private void MeasureStep()
        {
            // 判断自动流程是否首次进入该状态
            if (currentState.stateChanged == true)
            {
#if false
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 4 5 - 电导率测量 - 海水取样
                bool[] st = { true, true, true, false, false, false, false, false, false };

                // 如果温度设定点在 5 度以下，则开启 辅槽快冷 7 
                if (currentState.stateTemp < 5.0f)
                    st[6] = true;

                st.CopyTo(ryDevice.ryStatusToSet, 0);
#else
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 4 5 - 电导率测量 - 海水取样
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.Elect] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainHeat] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubHeat] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCool] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCircle] = true;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.MainCoolF] = false;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = false;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterIn] = false;
                ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.WaterOut] = false;
                // 如果温度设定点在 5 度以下，则开启 辅槽快冷 7 
                if (currentState.stateTemp < 5.0f)
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = true;
#endif
                // 将继电器状态写入下位机
                ryDevice.UpdateStatusToDevice();

                // 首次进入某一状态
                // 触发事件 - 例如，可以通知主界面执行一些操作
                FlowControlStateChangedEvent(currentState.flowState);

                Debug.WriteLine("当前进入工作状态： " + currentState.flowState.ToString());
                Utils.Logger.Sys("自动控温流程，进入 " + currentState.flowState.ToString() + " 状态...");

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;
            }

            // 继续执行相应的操作

            // 这里是不是读取相应的参数并记录就完成任务了
            // wghou

            // 触发电导率测量事件
            FlowControlMeasureEvent(State.Measure);
            Utils.Logger.Sys("测量电导率等数据...");

            // 进入下一个状态

            // 首先，判断此时控制列表队列是否为空
            if( controlFlowList.Count != 0)
            {
                // 控制状态序列不为空，说明实验还没有结束

                // 读取列表中的第一个状态相，并将其从列表中移除
                currentState = controlFlowList.First();
                controlFlowList.RemoveAt(0);

                // 确保该状态为升温 / 降温
                Trace.Assert((currentState.flowState == State.TempUp) || (currentState.flowState == State.TempDown));

                // 该状态置为首次改变
                currentState.stateChanged = true;

                Utils.Logger.Sys("开始下一个温度点的控温 - 稳定 - 测量流程...");
            }
            else
            {
                // 控制状态序列为空，说明实验已经结束了
                currentState = new StateFlow() { flowState = State.Finish };

                Utils.Logger.Sys("所有温度点均已测量完成...");
            }


            // 是否还有其他相关操作
            // wghou
            // code
        }

        private void FinishStep()
        {
            FlowControlFinishEvent(State.Finish);
            currentState = new StateFlow() { flowState = State.Idle };
        }

        /// <summary>
        /// 自动 - 实验完成
        /// </summary>
        private void IdleStep()
        {
            // 完成测量

            // 执行相应的操作
            // wghou
            // code

        }

        #endregion Private Methods



        #endregion 自动控制部分
        // end
    }

}
