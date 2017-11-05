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

        #region Devices
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
        #endregion Devices



        #region Init Methods
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
                Utils.IniReadWrite.INIWriteValue(configFilePath, "sensorDevice", "COM", "COM4");

                // 系统参数
                Utils.IniReadWrite.INIWriteValue(configFilePath, "system", "SteadyTime", "300");
                Utils.IniReadWrite.INIWriteValue(configFilePath, "system", "FlucValue", "0.0001");
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
            confOK &= srDevice.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "sensorDevice", "COM", "COM4"));
            Debug.WriteLineIf(!confOK, "配置传感器设备失败! 端口号: " + srDevice.srDevicePortName);
            Debug.WriteLineIf(confOK, "配置传感器设备成功! 端口号: " + srDevice.srDevicePortName);
            if (!confOK)
                Utils.Logger.Sys("配置传感器设备失败! 端口号: " + srDevice.srDevicePortName);


            // 设置定时器更新时间
            tpTemperatureUpdateTimer.Interval = tpDeviceM.readTempInterval;
            // 从配置文件读取 稳定时间 / 波动度需满足的条件
            steadyTime = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "system", "SteadyTime", "300"));
            flucValue = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "system", "FlucValue", "0.0001"));


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
            if (!srDevice.SelfCheck())
            {
                checkOK = false;
                Debug.WriteLine("传感器设备自检失败!");
                Utils.Logger.Sys("传感器设备自检失败!");
            }
            else
            {
                Debug.WriteLine("传感器设备自检成功!");
            }

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
        }
        #endregion Init Methods



        #region Timer Method
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

            // 如果温度读取发生了错误，则停止执行自动控温流程
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
        #endregion Timer Method



        #region 自动控制部分

        #region 类型定义
        /// <summary>
        /// 系统所要测量的温度点
        /// </summary>
        public class TemperaturePoint
        {
            /// <summary>
            /// 当前工作状态下的应达到的温度值
            /// </summary>
            public float stateTemp = 0.0f;
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
            Idle,
            /// <summary>
            /// 流程开始，会先做一个判断，是升温 / 降温 / 还是直接控温
            /// </summary>
            Start,
            /// <summary>
            /// 未定义状态
            /// </summary>
            Undefine
        }
        #endregion

        #region Public Members
        /// <summary>
        /// 自动控制各流程的名称
        /// </summary>
        public string[] StateName = { "升温", "降温", "控温", "稳定","测量", "完成", "空闲", "开始", "未定义" };
        /// <summary>
        /// 系统自动状态下，完成整个实验所需的温度点序列 - 当温度点完成测量时，才将该温度点从列表中删除
        /// </summary>
        public List<TemperaturePoint> temperaturePointList = new List<TemperaturePoint>();
        /// <summary>
        /// 当前工作状态 - 类
        /// </summary>
        public struct StateStruct
        {
            public State flowState;
            public bool stateChanged;
            public int stateTime;
            public TemperaturePoint stateTemp;
        }
        /// <summary>
        /// 当前工作状态 - 实例
        /// </summary>
        public StateStruct currentState = new StateStruct() { flowState = State.Idle, stateChanged = true, stateTime = 0, stateTemp = new TemperaturePoint() };
        /// <summary>
        /// 是否开始自动运行
        /// </summary>
        public bool autoStart = false;
        /// <summary>
        /// 稳定时间 second
        /// </summary>
        public int steadyTime;
        /// <summary>
        /// 波动度判断
        /// </summary>
        private float flucValue = 0.001f;
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
        #endregion



        #region 故障判断部分
        public enum FaultCode : int
        {
            /// <summary>
            /// 温度不降
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
            TempContinueUp,
            /// <summary>
            /// 读电桥温度错误
            /// </summary>
            SensorError,
            /// <summary>
            /// 其他错误
            /// </summary>
            CodeError,
        }
        //
        public delegate void FlowControlFaultOccurEventHandler(FaultCode fCode);
        /// <summary>
        /// 自动控温流程中，判断发生故障事件 - 通知主界面完成相应的处理
        /// </summary>
        public event FlowControlFaultOccurEventHandler FlowControlFaultOccurEvent;
        #endregion 故障判断部分


        // 流程控制部分
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
            // 正常状况下，该计数器应该是不会溢出的
            // wghou
            // 考虑溢出状况
            currentState.stateTime++;

            // 执行控制流程，当前温度列表绝对不能为空！
            // 如果为空，说明出现了严重的程序 Bug
            if(tpDeviceM.temperatures.Count == 0)
            {
                // wghou
                // 如果出现这个错误，是否要考虑结束整个程序
                FlowControlFaultOccurEvent(FaultCode.CodeError);
                return;
            }

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

                case State.Start:
                    StartStep();
                    break;

                default:
                    // 通常不会出现该情况
                    Debug.WriteLine("控温流程 case: default ");
                    Utils.Logger.Sys("异常：控温流程 case: default");
                    break;
            }


            // 故障判断子步骤 - 判断主要故障，如 温度不降 / 温度持续上升 / 温度持续下降 / 温度波动过大 等等
            // 如果发生错误，则触发 FlowControlFaultOccurEvent 事件，通知主界面做相应的处理
            faultCheckSubStep();


            // 调试信息
            Debug.WriteLine("当前工作状态： " + currentState.flowState.ToString());
        }

        #region Fault Check Methods
        /// <summary>
        /// 故障判断 - 子步骤 - 判断主要故障，如 温度不降 / 温度持续上升 / 温度持续下降 / 温度波动过大 等等
        /// </summary>
        private void faultCheckSubStep()
        {
            // 在多长的时间段内检测故障
            // 还是担心的那些事情，如果刚刚进入某个状态，特别容易报错
            int count = 1 * 60 * 1000 / tpDeviceM.readTempInterval;
            // 主槽报警及故障判断
            switch (currentState.flowState)
            {
                case State.TempUp:
                    {
                        // 故障判断 - 温度不升高 / 温度持续下降
                        // wghou
                        // 在升温过程中，需要判断该故障吗？
                        // 有点担心，当首次进入该状态时，特别容易检测到故障
                        if (currentState.stateTime < count)
                            break;
                        if (faultCheckTempContinueDown(count))
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
                        if (faultCheckTempNotDown(count))
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
                        if (faultCheckTempContinueDown(count))
                            FlowControlFaultOccurEvent(FaultCode.TempContinueDown);
                        if (faultCheckTempContinueUp(count))
                            FlowControlFaultOccurEvent(FaultCode.TempContinueUp);
                        if (faultCheckTempFlucLarge(count))
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
                        if (faultCheckTempFlucLarge(count))
                            FlowControlFaultOccurEvent(FaultCode.TempFlucLarge);
                        break;
                    }
                default:
                    break;
            }
        }


        /// <summary>
        /// 故障判断 - 温度不下降 - 检测到故障则返回 true
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool faultCheckTempNotDown(int count)
        {
            bool fault = false;
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
            return fault;
        }


        /// <summary>
        /// 故障判断 - 温度持续下降 - 检测到故障则返回 true
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool faultCheckTempContinueDown(int count)
        {
            bool fault = false;
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
            return fault;
        }


        /// <summary>
        /// 故障判断 - 温度波动度过大 - 检测到故障则返回 true
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool faultCheckTempFlucLarge(int count)
        {
            bool fault = false;
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
            return fault;
        }


        /// <summary>
        /// 故障判断 - 温度持续上升 - 检测到故障则返回 true
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool faultCheckTempContinueUp(int count)
        {
            bool fault = false;
            // 温度持续上升
            // 如果 tpDeviceM.temperatures 中存储的温度值过少，即系统运行时间太短，则不检测 
            //int count = 1 * 10 * 1000 / tpDeviceM.readTempInterval;
            if (tpDeviceM.temperatures.Count > count)
            {
                // 如果count 个之前的温度值比当前温度大 Device.TempDevice.tpParam[8] (温度阈值)，说明温度持续下降
                if (tpDeviceM.temperatures.Last() - tpDeviceM.temperatures[tpDeviceM.temperatures.Count - count] > tpDeviceM.tpParam[8])
                    fault = true;
                else
                    fault = false;
            }
            else
            {
                fault = false;
            }
            return fault;
        }
        #endregion Fault Check Methods

        


        #region Step Methods
        /// <summary>
        /// 控温流程开始，首先根据当前温度以及温度设定点，判断应执行升温 / 降温 / 还是直接控温
        /// </summary>
        private void StartStep()
        {
            // 确保 temperaturePointList 不为空
            if(temperaturePointList.Count == 0)
            {
                currentState = new StateStruct() { flowState = State.Idle };
                return;
            }

            // 定义当前状态
            currentState = new StateStruct() { flowState = State.Undefine, stateChanged = true, stateTime = 0, stateTemp = temperaturePointList.First() };

            // 如果当前温度点刚好处于温度点附近，则直接进入控温状态
            // wghou
            // 这里应该设置一个多大的阈值？？？
            if (Math.Abs(tpDeviceM.temperatures.Last() - currentState.stateTemp.stateTemp) < 0.05)
            {
                // 状态 - 控温
                currentState.flowState = State.TempControl;
            }
            // 当前温度点小于温度设定点，则升温
            else if(tpDeviceM.temperatures.Last() < currentState.stateTemp.stateTemp)
            {
                // 状态 - 升温 
                currentState.flowState = State.TempUp;
            }
            // 否则，降温
            else
            {
                // 状态 - 降温
                currentState.flowState = State.TempDown;
            }
        }

        /// <summary>
        /// 自动 - 升温
        /// </summary>
        private void TempUpStep()
        {
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
#endif
                // 将继电器状态写入下位机
                // 如果出现错误，则暂时只通过其事件触发函数，在主界面中显示错误提示，未作其他处理
                ryDevice.UpdateStatusToDevice();

                // 设置主槽 / 辅槽控温设备的参数
                if(currentState.stateTemp.advanceState == true)
                {
                    // 向主槽 / 辅槽控温设备写入全部参数
                    currentState.stateTemp.paramM.CopyTo(tpDeviceM.tpParamToSet, 0);
                    currentState.stateTemp.paramS.CopyTo(tpDeviceS.tpParamToSet, 0);
                    tpDeviceM.UpdateParamToDevice();
                    tpDeviceS.UpdateParamToDevice();
                }
                else
                {
                    // 只向主槽 / 辅槽控温设备写入温度设定值
                    tpDeviceM.tpParamToSet[(int)Device.TempProtocol.Cmd_t.TempSet] = currentState.stateTemp.stateTemp;
                    tpDeviceM.UpdateParamToDevice();
                    tpDeviceS.tpParamToSet[(int)Device.TempProtocol.Cmd_t.TempSet] = currentState.stateTemp.stateTemp;
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
            if(tpDeviceM.temperatures.Last() > currentState.stateTemp.stateTemp)
            {
                // 如果主槽中温度高于设定值，则进入下一个状态 - 控温
                // 状态 - 控温 / 首次进入 - true / 状态时间 - 0 / 状态温度点 - currentState.stateTemp
                currentState = new StateStruct() { flowState = State.TempControl, stateChanged = true, stateTime = 0, stateTemp = currentState.stateTemp };
                return;
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
#if false
                // 首次进入该状态，应改变相应的继电器状态
                // 1 2 3 4 5 6 7 - 到达温度点关闭 6 和 7
                bool[] st = { true, true, true, true, true, true, true, false, false };
                st.CopyTo(ryDevice.ryStatusToSet, 0);
                // 将继电器状态写入下位机
#else
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
#endif
                ryDevice.UpdateStatusToDevice();

                // 设置主槽 / 辅槽控温设备的参数
                if (currentState.stateTemp.advanceState == true)
                {
                    // 向主槽 / 辅槽控温设备写入全部参数
                    currentState.stateTemp.paramM.CopyTo(tpDeviceM.tpParamToSet, 0);
                    currentState.stateTemp.paramS.CopyTo(tpDeviceS.tpParamToSet, 0);
                    tpDeviceM.UpdateParamToDevice();
                    tpDeviceS.UpdateParamToDevice();
                }
                else
                {
                    // 只向主槽 / 辅槽控温设备写入温度设定值
                    tpDeviceM.tpParamToSet[(int)Device.TempProtocol.Cmd_t.TempSet] = currentState.stateTemp.stateTemp;
                    tpDeviceM.UpdateParamToDevice();
                    tpDeviceS.tpParamToSet[(int)Device.TempProtocol.Cmd_t.TempSet] = currentState.stateTemp.stateTemp;
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
            if (tpDeviceM.temperatures.Last() < currentState.stateTemp.stateTemp)
            {
                // 进入下一个状态，下一个状态应该是 控温
                // 状态 - 控温 / 首次进入 - true / 状态时间 - 0 / 状态温度点 - currentState.stateTemp
                currentState = new StateStruct() { flowState = State.TempControl, stateChanged = true, stateTime = 0, stateTemp = currentState.stateTemp };
                return;
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
                if (currentState.stateTemp.stateTemp < 5.0f)
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
            bool steady = tpDeviceM.chekFluc(steadyTime, flucValue);
            if (steady)
            {
                // 进入下一个状态，下一个状态应该是 稳定
                // 状态 - 稳定 / 首次进入 - true / 状态时间 - 0 / 状态温度点 - currentState.stateTemp
                currentState = new StateStruct() { flowState = State.TempStable, stateChanged = true, stateTime = 0, stateTemp = currentState.stateTemp };
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
                if (currentState.stateTemp.stateTemp < 5.0f)
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


            // 读取电桥温度
            // wghou
            // 如果出现错误怎么办？？？
            // 是不是需要直接停止系统流程？？？
            if(!srDevice.UpdateSensorValue())
            {
                // 读取电桥温度出现了错误
                // 触发事件
                FlowControlFaultOccurEvent(FaultCode.SensorError);
            }

            // wghou
            // 需要改动的地方 - 这里是判断电桥的温度波动度，如果不满足，则重新返回控温状态
            //
            // 进入稳定状态后，等待 2 分钟后，判断电桥 3 分钟稳定度小于 0.0005，满足则测量
            if(currentState.stateTime > 2 * 60 * 1000 / tpDeviceM.readTempInterval)
            {
                // 判断温度波动度
                if(srDevice.chekFluc(currentState.stateTime,flucValue))
                {
                    // 满足波动度判断条件

                    // 温度稳定度达到了要求，进入下一个状态 - 测量
                    // 状态 - 测量 / 首次进入 - true / 状态时间 - 0 / 状态温度点 - currentState.stateTemp
                    currentState = new StateStruct() { flowState = State.Measure, stateChanged = true, stateTime = 0, stateTemp = currentState.stateTemp };

                    Utils.Logger.Sys("2 分钟后，温度波动度满足 xxx 条件，可以测量电导率等数据...");
                }
                else
                {
                    // 不满足波动度条件，则重新返回控温流程
                    currentState = new StateStruct() { flowState = State.TempControl, stateChanged = true, stateTime = 0, stateTemp = currentState.stateTemp };
                    Utils.Logger.Sys("2 分钟后，测温电桥波动度不满足 xxx 条件，重新返回控温状态...");
                    return;
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
            // 继续执行相应的操作

            // 这里是不是读取相应的参数并记录就完成任务了
            // wghou
            // 如果出现了错误，应该怎样处理？？？
            // wghou
            if(!srDevice.UpdateSensorValue())
            {
                // 读取电桥温度出现了错误
                FlowControlFaultOccurEvent(FaultCode.SensorError);
            }
            else
            {
                // 记录电桥温度
            }


            // 触发电导率测量事件
            FlowControlStateChangedEvent(State.Measure);
            Utils.Logger.Sys("测量电导率等数据...");

            // 进入下一个状态
            // 首先将已经测量完成的温度点删除掉
            try { temperaturePointList.RemoveAt(0); } catch { }
            

            // 首先，判断此时控制列表队列是否为空
            if( temperaturePointList.Count != 0)
            {
                // 控制状态序列不为空，说明实验还没有结束

                // 读取列表中的第一个温度点
                currentState = new StateStruct() { flowState = State.TempDown, stateChanged = true, stateTime = 0, stateTemp = temperaturePointList.First() };
                Utils.Logger.Sys("开始下一个温度点的控温 - 稳定 - 测量流程...");
            }
            else
            {
                // 控制状态序列为空，说明实验已经结束了
                currentState = new StateStruct() { flowState = State.Finish };
                Utils.Logger.Sys("所有温度点均已测量完成...");
            }


            // 是否还有其他相关操作
            // wghou
            // code
        }

        private void FinishStep()
        {
            // 触发温度点测量完成事件
            FlowControlStateChangedEvent(State.Finish);
            currentState = new StateStruct() { flowState = State.Idle, stateChanged = true, stateTime = 0, stateTemp = new TemperaturePoint() };
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
        #endregion Step Methods


        #endregion Private Methods



        #endregion 自动控制部分
        // end
    }

}
