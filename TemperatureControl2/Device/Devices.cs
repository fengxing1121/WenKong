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
        public TempDevice tpDeviceM = new TempDevice() { tpDeviceName = "主槽控温设备"};
        /// <summary>
        /// 辅槽控温设备
        /// </summary>
        public TempDevice tpDeviceS = new TempDevice() { tpDeviceName = "辅槽控温设备" };
        /// <summary>
        /// 传感器设备
        /// </summary>
        public SensorDevice srDevice = new SensorDevice();
        /// <summary>
        /// 定时器，用于每隔一段时间从设备读取温度显示值，并在主界面中更新
        /// </summary>
        public Timer tpTemperatureUpdateTimer = new Timer();
        public UInt64 timeStart = 0;
        // 自定义定时器事件，当执行完定时器触发函数后，触发该事件
        public delegate void TpTemperatureUpdateTimerEventHandler();
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

            try
            {
                // 如果配置文件不存在，则新建
                if (!File.Exists(configFilePath))
                {
                    // 设备端口号
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "PortName", "tempMain", "COM1");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "PortName", "tempSub", "COM2");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "PortName", "relay", "COM3");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "PortName", "sensor", "COM4");

                    // 相关参数
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "ReadIntervalSec", "2");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "SteadyTimeSec", "300");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "bridgeSteadyTimeSec", "120");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "FlucValue", "0.0001");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "controlTempThr", "0.4");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempNotUpOrDownFaultTimeSec", "600");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempNotUpOrDwonFaultThr", "0.4");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "flucFaultTimeSec", "120");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "flucFaultThr", "0.4");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempBiasFaultThr", "2");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempMaxValue", "40");
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempMinValue", "-2");

                    // 一些其他的调试参数
                    // 升序还是降序
                    Utils.IniReadWrite.INIWriteValue(configFilePath, "Ohters", "sort", "descend");
                }

                //////////////////////////////////////////
                // 配置参数
                // 主槽控温设备
                confOK &= tpDeviceM.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "PortName", "tempMain", "COM1"));
                // 温度读取时间间隔
                tpDeviceM.readTempIntervalSec = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "ReadIntervalSec", "2"));
                Debug.WriteLineIf(!confOK, "配置主槽控温设备失败! 端口号: " + tpDeviceM.tpDevicePortName);
                Debug.WriteLineIf(confOK, "配置主槽控温设备成功! 端口号: " + tpDeviceM.tpDevicePortName);
                if (!confOK)
                    Utils.Logger.Sys("配置主槽控温设备失败! 端口号: " + tpDeviceM.tpDevicePortName);


                // 辅槽控温设备
                confOK &= tpDeviceS.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "PortName", "tempSub", "COM2"));
                // 温度读取时间间隔
                tpDeviceS.readTempIntervalSec = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "ReadIntervalSec", "2"));
                Debug.WriteLineIf(!confOK, "配置辅槽控温设备失败! 端口号: " + tpDeviceS.tpDevicePortName);
                Debug.WriteLineIf(confOK, "配置辅槽控温设备成功! 端口号: " + tpDeviceS.tpDevicePortName);
                if (!confOK)
                    Utils.Logger.Sys("配置辅槽控温设备失败! 端口号: " + tpDeviceS.tpDevicePortName);


                // 继电器设备
                confOK &= ryDevice.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "PortName", "relay", "COM3"));
                Debug.WriteLineIf(!confOK, "配置继电器设备失败! 端口号: " + ryDevice.ryDevicePortName);
                Debug.WriteLineIf(confOK, "配置继电器设备成功! 端口号: " + ryDevice.ryDevicePortName);
                if (!confOK)
                    Utils.Logger.Sys("配置继电器设备失败! 端口号: " + ryDevice.ryDevicePortName);

                // 传感器设备
                confOK &= srDevice.SetDevicePortName(Utils.IniReadWrite.INIGetStringValue(configFilePath, "PortName", "sensor", "COM4"));
                Debug.WriteLineIf(!confOK, "配置传感器设备失败! 端口号: " + srDevice.srDevicePortName);
                Debug.WriteLineIf(confOK, "配置传感器设备成功! 端口号: " + srDevice.srDevicePortName);
                if (!confOK)
                    Utils.Logger.Sys("配置传感器设备失败! 端口号: " + srDevice.srDevicePortName);


                ////////////////////////////////////////
                // 参数设置
                // 设置定时器更新时间
                tpTemperatureUpdateTimer.Interval = tpDeviceM.readTempIntervalSec *1000;
                // 从配置文件读取 稳定时间 / 波动度需满足的条件
                steadyTimeSec = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "SteadyTimeSecond", "300"));
                bridgeSteadyTimeSec = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "bridgeSteadyTimeSec", "120"));
                flucValue = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "FlucValue", "0.0001"));
                controlTempThr = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "controlTempThr", "0.4"));
                tempNotUpOrDownFaultTimeSec = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "tempNotUpOrDownFaultTimeSec", "600"));
                tempNotUpOrDwonFaultThr = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "tempNotUpOrDwonFaultThr", "0.4"));
                flucFaultTimeSec = int.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "flucFaultTimeSec", "120"));
                flucFaultThr = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "flucFaultThr", "0.4"));
                tempBiasFaultThr = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "tempBiasFaultThr", "2"));
                tempMaxValue = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "tempMaxValue", "40"));
                tempMinValue = float.Parse(Utils.IniReadWrite.INIGetStringValue(configFilePath, "Paramters", "tempMinValue", "-2"));
                // 默认降序
                sort = Utils.IniReadWrite.INIGetStringValue(configFilePath, "Others", "sort", "descend");
            }
            catch(Exception ex)
            {
                Utils.Logger.Sys("从配置文件读取参数过程中发生异常：" + ex.Message.ToString());
                confOK = false;
            }
            
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
            // 程序运行总时间计数
            timeStart += (UInt64)tpDeviceM.readTempIntervalSec;
            // 确保不越界
            if (timeStart > (UInt64.MaxValue - 5 * (UInt64)tpDeviceM.readTempIntervalSec))
                timeStart = (UInt64.MaxValue - 5 * (UInt64)tpDeviceM.readTempIntervalSec);

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
            // 记录主槽温度
            Utils.Logger.TempData("主槽温度: " + tpDeviceM.temperatures.Last().ToString("0.0000"));

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
            TpTemperatureUpdateTimerEvent();


            // 如果温度读取发生了错误
            if (err != TempProtocol.Err_t.NoError)
            {
                // 如果读取参数发生了错误，则触发 FlowControlFaultOccurEvent 事件
                FlowControlFaultOccurEvent(FaultCode.TempError);
                return;
            }


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
            public float stateTemp { get { return paramM[0]; } }
            /// <summary>
            /// 主槽控温表参数 - 7个
            /// </summary>
            public float[] paramM = new float[7];
            /// <summary>
            /// 辅槽控温表参数 - 7个
            /// </summary>
            public float[] paramS = new float[7];
        }


        /// <summary>
        /// 自动控制各流程的名称
        /// </summary>
        public string[] StateName = { "升温", "降温", "控温", "稳定", "测量", "完成", "空闲", "开始", "未定义" };
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
            public Int32 stateCounts;
            public TemperaturePoint tempPoint;
        }
        /// <summary>
        /// 当前工作状态 - 实例
        /// </summary>
        public StateStruct currentState = new StateStruct() { flowState = State.Idle, stateChanged = true, stateCounts = 0, tempPoint = new TemperaturePoint() };
        /// <summary>
        /// 是否开始自动运行
        /// </summary>
        public bool autoStart = false;
        /// <summary>
        /// 锁 - 保证自动控制流程步骤执行的时候，相应资源（例如 currentState autoStart controlFlowList etc. ）不会被访问
        /// </summary>
        public Object stepLocker = new Object();
        /// <summary>
        /// 自动流程中，进入某一状态时触发的事件
        /// </summary>
        /// <param name="st"></param>
        public delegate void FlowControlStateChangedEventHandler(State st);
        /// <summary>
        /// 自动操作流程中，进入某一状态时触发的事件
        /// </summary>
        public event FlowControlStateChangedEventHandler FlowControlStateChangedEvent;


        #region 阈值参数
        /// <summary>
        /// 稳定时间 second
        /// </summary>
        public int steadyTimeSec = 300;
        /// <summary>
        /// 电桥温度稳定时间
        /// </summary>
        public int bridgeSteadyTimeSec = 120;
        /// <summary>
        /// 波动度判断
        /// </summary>
        public float flucValue = 0.001f;
        /// <summary>
        /// 进入控温状态时的温度阈值
        /// </summary>
        public float controlTempThr = 0.4f;
        /// <summary>
        /// 温度不升 / 不降故障判断时间
        /// </summary>
        public int tempNotUpOrDownFaultTimeSec = 600;
        /// <summary>
        /// 温度不升 / 不降故障温度阈值
        /// </summary>
        public float tempNotUpOrDwonFaultThr = 0.4f;
        /// <summary>
        /// 波动度过大故障判断时间
        /// </summary>
        public int flucFaultTimeSec = 120;
        /// <summary>
        /// 波动度过大故障阈值
        /// </summary>
        public float flucFaultThr = 0.4f;
        /// <summary>
        /// 温度偏离设定点故障阈值
        /// </summary>
        public float tempBiasFaultThr = 2.0f;
        /// <summary>
        /// 控温槽温度上限
        /// </summary>
        public float tempMaxValue = 40.0f;
        /// <summary>
        /// 控温槽温度下限
        /// </summary>
        public float tempMinValue = -2.0f;

        public string sort = "descend";
        #endregion 阈值参数




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
            TempNotUp,
            /// <summary>
            /// 温度波动过大
            /// </summary>
            TempFlucLarge,
            /// <summary>
            /// 温度持续上升
            /// </summary>
            TempBasis,
            /// <summary>
            /// 控温槽中的温度超出界限
            /// </summary>
            TempOutRange,
            /// <summary>
            /// 读电桥温度错误
            /// </summary>
            SensorError,
            /// <summary>
            /// 继电器设备错误
            /// </summary>
            RelayError,
            /// <summary>
            /// 温控设备错误
            /// </summary>
            TempError,
            /// <summary>
            /// 温控设备参数写入错误
            /// </summary>
            TempParamSetError,
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
            // 执行控制流程，当前温度列表绝对不能为空！
            // 如果为空，说明出现了严重的程序 Bug
            if (tpDeviceM.temperatures.Count == 0)
            {
                // 如果出现这个错误，是否要考虑结束整个程序
                FlowControlFaultOccurEvent(FaultCode.CodeError);
                return;
            }

            if(tpDeviceM.temperatures.Last() > tempMaxValue || tpDeviceM.temperatures.Last() < tempMinValue)
            {
                // 控温槽温度越过了界限
                FlowControlFaultOccurEvent(FaultCode.TempOutRange);
            }

            // 如果没有开启自动状态，则不执行状态调度
            if (!auto)
                return;

            // 当前状态工作时间计时
            // 正常状况下，该计数器应该是不会溢出的
            // wghou
            // 考虑溢出状况
            currentState.stateCounts++;
            if (currentState.stateCounts == Int32.MaxValue)
                currentState.stateCounts = Int32.MaxValue;

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
                    FlowControlFaultOccurEvent(FaultCode.CodeError);
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
            // 主槽报警及故障判断
            switch (currentState.flowState)
            {
                case State.TempUp:
                    {
                        // 故障判断 - 温度不升高
                        faultCheckTempNotUp();
                        break;
                    }
                case State.TempDown:
                    {
                        // 故障判断 - 温度不下降
                        faultCheckTempNotDown();
                        break;
                    }
                case State.TempControl:
                    {
                        // 当前温度与设定温度偏差过大
                        faultCheckBasis();

                        // 进入某一状态后，等待 flucFaultTimeSec 再判断波动度 flucFaultThr
                        faultCheckTempFlucLarge();
                        break;

                    }
                case State.TempStable:
                    {
                        // 当前温度与设定温度偏差过大
                        faultCheckBasis();

                        // 进入某一状态后，等待 flucFaultTimeSec 再判断波动度 flucFaultThr
                        faultCheckTempFlucLarge();
                        break;
                    }
                default:
                    break;
            }

            return;
        }


        /// <summary>
        /// 故障判断 - 温度偏离 - 检测到故障则返回 true
        /// </summary>
        /// <returns></returns>
        private void faultCheckBasis()
        {
            // 判断温度偏离设定点
            if(tpDeviceM.temperatures.Count !=0)
            {
                if (Math.Abs(tpDeviceM.temperatures.Last() - currentState.tempPoint.stateTemp) > tempBiasFaultThr)
                {
                    FlowControlFaultOccurEvent(FaultCode.TempBasis);
                }
            }

            return;
        }


        /// <summary>
        /// 故障判断 - 温度不下降 - 检测到故障则返回 true
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private void faultCheckTempNotDown()
        {
            // 故障判断 - 温度不下降 / 温度持续上升
            // 进入某一状态后，等待 tempNotUpOrDownFaultTimeSec 再判断温度是否上升 tempNotUpOrDwonFaultThr
            int count = tempNotUpOrDownFaultTimeSec / tpDeviceM.readTempIntervalSec;
            if (currentState.stateCounts < count)
                return;

            // 判断温度不下降
            // 如果 tpDeviceM.temperatures 中存储的温度值过少，即系统运行时间太短，则不检测 
            //int count = 1 * 10 * 1000 / tpDeviceM.readTempInterval;
            if (tpDeviceM.temperatures.Count > count)
            {
                // 如果count 个之前的温度值减去当前温度 小于 0.4 ，说明温度没有下降
                if (tpDeviceM.temperatures[tpDeviceM.temperatures.Count - count] - tpDeviceM.temperatures.Last() < tempNotUpOrDwonFaultThr)
                    FlowControlFaultOccurEvent(FaultCode.TempNotDown);
            }

            return;
        }


        /// <summary>
        /// 故障判断 - 温度波动度过大 - 检测到故障则返回 true
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private void faultCheckTempFlucLarge()
        {
            // 进入某一状态后，等待 flucFaultTimeSec 再判断波动度 flucFaultThr
            int count = flucFaultTimeSec / tpDeviceM.readTempIntervalSec;
            if (currentState.stateCounts < count)
                return;

            // 判断温度波动大
            // 如果 tpDeviceM.temperatures 中存储的温度值过少，即系统运行时间太短，则不检测 
            //int count = 1 * 10 * 1000 / tpDeviceM.readTempInterval;
            float fluc = 0.0f;
            // 如果获取波动度大于 0.4，说明波动度过大
            if (tpDeviceM.GetFluc(count, out fluc) && fluc > flucFaultThr)
            {
                FlowControlFaultOccurEvent(FaultCode.TempFlucLarge);
            }

            return;
        }


        /// <summary>
        /// 故障判断 - 温度没有上升 - 检测到故障则返回 true
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private void faultCheckTempNotUp()
        {
            // 故障判断 - 温度不升高 / 温度持续下降
            // 进入某一状态后，等待 tempNotUpOrDownFaultTimeSec 再判断温度是否上升 tempNotUpOrDwonFaultThr
            int count = tempNotUpOrDownFaultTimeSec / tpDeviceM.readTempIntervalSec;
            if (currentState.stateCounts < count)
                return;

            // 温度没有上升
            // 如果 tpDeviceM.temperatures 中存储的温度值过少，即系统运行时间太短，则不检测 
            //int count = 1 * 10 * 1000 / tpDeviceM.readTempInterval;
            if (tpDeviceM.temperatures.Count > count)
            {
                // 如果count 个之前的温度值 减去 当前温度，小于 0.4 ，说明温度没有上升
                if (tpDeviceM.temperatures.Last() - tpDeviceM.temperatures[tpDeviceM.temperatures.Count - count] < tempNotUpOrDwonFaultThr)
                    FlowControlFaultOccurEvent(FaultCode.TempNotUp);
            }

            return;
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
            currentState = new StateStruct() { flowState = State.Undefine, stateChanged = true, stateCounts = 0, tempPoint = temperaturePointList.First() };

            // 如果当前温度点刚好处于温度点附近，则直接进入控温状态
            // wghou
            // 这里应该设置一个多大的阈值？？？
            if (Math.Abs(tpDeviceM.temperatures.Last() - currentState.tempPoint.stateTemp) < controlTempThr)
            {
                // 状态 - 控温
                currentState.flowState = State.TempControl;
            }
            // 当前温度点小于温度设定点，则升温
            else if(tpDeviceM.temperatures.Last() < currentState.tempPoint.stateTemp)
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
                // 如果出现错误，则通过 FlowControlFaultOccurEvent 事件通知主界面提示错误
                RelayProtocol.Err_r ryErr = ryDevice.UpdateStatusToDeviceReturnErr();
                if (ryErr != RelayProtocol.Err_r.NoError)
                    FlowControlFaultOccurEvent(FaultCode.RelayError);
                //ryDevice.UpdateStatusToDevice();

                // 设置主槽 / 辅槽控温设备的参数
                // 向主槽 / 辅槽控温设备写入全部参数
                currentState.tempPoint.paramM.CopyTo(tpDeviceM.tpParamToSet, 0);
                currentState.tempPoint.paramS.CopyTo(tpDeviceS.tpParamToSet, 0);

                // 将参数更新到下位机
                // 如果出现错误，则通过 FlowControlFaultOccurEvent 事件通知主界面提示错误
                TempProtocol.Err_t tpErr = TempProtocol.Err_t.NoError;
                tpErr = tpDeviceM.UpdateParamToDeviceReturnErr();
                if (tpErr != TempProtocol.Err_t.NoError)
                    FlowControlFaultOccurEvent(FaultCode.TempError);


                tpErr = tpDeviceS.UpdateParamToDeviceReturnErr();
                if (tpErr != TempProtocol.Err_t.NoError)
                    FlowControlFaultOccurEvent(FaultCode.TempError);
                //tpDeviceM.UpdateParamToDevice();
                //tpDeviceS.UpdateParamToDevice();


                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;

                // 首次进入某一状态
                // 触发事件 - 例如，可以通知主界面执行一些操作
                FlowControlStateChangedEvent(currentState.flowState);

                Debug.WriteLine("当前进入工作状态： " + currentState.flowState.ToString());
                Utils.Logger.Sys("自动控温流程，进入 升温 状态...");
                Utils.Logger.TempData("进入升温状态，温度设定点： " + currentState.tempPoint.stateTemp.ToString("0.0000"));

                // 首次进入该状态，不进行其他判断（判断是否满足一定条件，需要转换状态），直接返回
                return;
            }

            // 继续执行相应的操作

            // 温度达到设定值，即停止升温，进入控温状态
            if(tpDeviceM.temperatures.Last() > currentState.tempPoint.stateTemp)
            {
                // 如果主槽中温度高于设定值，则进入下一个状态 - 控温
                // 状态 - 控温 / 首次进入 - true / 状态时间 - 0 / 状态温度点 - currentState.stateTemp
                currentState = new StateStruct() { flowState = State.TempControl, stateChanged = true, stateCounts = 0, tempPoint = currentState.tempPoint };
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
                // 将继电器状态写入下位机
                // 如果出现错误，则通过 FlowControlFaultOccurEvent 事件通知主界面提示错误
                RelayProtocol.Err_r ryErr = ryDevice.UpdateStatusToDeviceReturnErr();
                if (ryErr != RelayProtocol.Err_r.NoError)
                    FlowControlFaultOccurEvent(FaultCode.RelayError);
                //ryDevice.UpdateStatusToDevice();

                // 设置主槽 / 辅槽控温设备的参数
                // 向主槽 / 辅槽控温设备写入全部参数
                currentState.tempPoint.paramM.CopyTo(tpDeviceM.tpParamToSet, 0);
                currentState.tempPoint.paramS.CopyTo(tpDeviceS.tpParamToSet, 0);


                // 将参数更新到下位机
                // 如果出现错误，则通过 FlowControlFaultOccurEvent 事件通知主界面提示错误
                TempProtocol.Err_t tpErr = TempProtocol.Err_t.NoError;
                tpErr = tpDeviceM.UpdateParamToDeviceReturnErr();
                if (tpErr != TempProtocol.Err_t.NoError)
                    FlowControlFaultOccurEvent(FaultCode.TempError);

                tpErr = tpDeviceS.UpdateParamToDeviceReturnErr();
                if (tpErr != TempProtocol.Err_t.NoError)
                    FlowControlFaultOccurEvent(FaultCode.TempError);
                //tpDeviceM.UpdateParamToDevice();
                //tpDeviceS.UpdateParamToDevice();


                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;

                // 首次进入某一状态
                // 触发事件 - 例如，可以通知主界面执行一些操作
                FlowControlStateChangedEvent(currentState.flowState);

                Debug.WriteLine("当前进入工作状态： " + currentState.flowState.ToString());
                Utils.Logger.Sys("自动控温流程，进入 降温 状态...");
                Utils.Logger.TempData("进入降温状态，温度设定点： " + currentState.tempPoint.stateTemp.ToString("0.0000"));

                // 首次进入该状态，不进行其他判断（判断是否满足一定条件，需要转换状态），直接返回
                return;
            }

            // 继续执行相应的操作

            // 温度下降到温度设定点以下，则立即进入下一个状态
            if (tpDeviceM.temperatures.Last() < currentState.tempPoint.stateTemp)
            {
                // 进入下一个状态，下一个状态应该是 控温
                // 状态 - 控温 / 首次进入 - true / 状态时间 - 0 / 状态温度点 - currentState.stateTemp
                currentState = new StateStruct() { flowState = State.TempControl, stateChanged = true, stateCounts = 0, tempPoint = currentState.tempPoint };
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
                if (currentState.tempPoint.stateTemp < 5.0f)
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = true;
#endif
                // 将继电器状态写入下位机
                // 如果出现错误，则通过 FlowControlFaultOccurEvent 事件通知主界面提示错误
                RelayProtocol.Err_r ryErr = ryDevice.UpdateStatusToDeviceReturnErr();
                if (ryErr != RelayProtocol.Err_r.NoError)
                    FlowControlFaultOccurEvent(FaultCode.RelayError);
                //ryDevice.UpdateStatusToDevice();

                // wghou
                // 这里是否要再次对主槽 / 辅槽的设定温度进行写入确认？

                // 首次进入某一状态
                // 触发事件 - 例如，可以通知主界面执行一些操作
                FlowControlStateChangedEvent(currentState.flowState);

                Debug.WriteLine("当前进入工作状态： " + currentState.flowState.ToString());
                Utils.Logger.Sys("自动控温流程，进入 控温 状态...");
                Utils.Logger.TempData("进入控温状态，温度设定点： " + currentState.tempPoint.stateTemp.ToString("0.0000"));

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;

                // 首次进入该状态，不进行其他判断（判断是否满足一定条件，需要转换状态），直接返回
                return;
            }

            // 继续执行相应的操作

            // 控温状态下，温度波动度满足判断条件，则立即进入下一状态 - 3 分钟波动度小于 0.0005

            // 5 分钟 0.001
            bool steady = tpDeviceM.checkFlucSeconds(steadyTimeSec, flucValue);
            if (steady)
            {
                // 进入下一个状态，下一个状态应该是 稳定
                // 状态 - 稳定 / 首次进入 - true / 状态时间 - 0 / 状态温度点 - currentState.stateTemp
                currentState = new StateStruct() { flowState = State.TempStable, stateChanged = true, stateCounts = 0, tempPoint = currentState.tempPoint };
                Utils.Logger.Sys((steadyTimeSec/60).ToString("0") + " 分钟温度波动度满足波动度小于 " + flucValue.ToString("0.0000") + "℃");
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
                if (currentState.tempPoint.stateTemp < 5.0f)
                    ryDevice.ryStatusToSet[(int)RelayProtocol.Cmd_r.SubCoolF] = true;
#endif
                // 将继电器状态写入下位机
                // 如果出现错误，则通过 FlowControlFaultOccurEvent 事件通知主界面提示错误
                RelayProtocol.Err_r ryErr = ryDevice.UpdateStatusToDeviceReturnErr();
                if (ryErr != RelayProtocol.Err_r.NoError)
                    FlowControlFaultOccurEvent(FaultCode.RelayError);
                //ryDevice.UpdateStatusToDevice();

                // 首次进入某一状态
                // 触发事件 - 例如，可以通知主界面执行一些操作
                FlowControlStateChangedEvent(currentState.flowState);

                Debug.WriteLine("当前进入工作状态： " + currentState.flowState.ToString());
                Utils.Logger.Sys("自动控温流程，进入 稳定 状态...");
                Utils.Logger.TempData("进入稳定状态，温度设定点： " + currentState.tempPoint.stateTemp.ToString("0.0000"));

                // 将首次进入该状态标志位置为 false
                currentState.stateChanged = false;

                // 首次进入该状态，不进行其他判断（判断是否满足一定条件，需要转换状态），直接返回
                return;
            }

            // 继续执行相应的操作


            // wghou
            // 暂时用辅槽的温度代替电桥温度
#if false
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
                    currentState = new StateStruct() { flowState = State.Measure, stateChanged = true, stateTime = 0, tempPoint = currentState.tempPoint };

                    Utils.Logger.Sys("2 分钟后，温度波动度满足 xxx 条件，可以测量电导率等数据...");
                }
                else
                {
                    // 不满足波动度条件，则重新返回控温流程
                    currentState = new StateStruct() { flowState = State.TempControl, stateChanged = true, stateTime = 0, tempPoint = currentState.tempPoint };
                    Utils.Logger.Sys("2 分钟后，测温电桥波动度不满足 xxx 条件，重新返回控温状态...");
                    return;
                }
            }
#else

            // 2 分钟 0.001
            if (currentState.stateCounts > bridgeSteadyTimeSec / tpDeviceM.readTempIntervalSec)
            {
                bool steady = tpDeviceS.checkFlucSeconds(currentState.stateCounts, flucValue);
                if (steady)
                {
                    // 满足波动度判断条件

                    // 温度稳定度达到了要求，进入下一个状态 - 测量
                    // 状态 - 测量 / 首次进入 - true / 状态时间 - 0 / 状态温度点 - currentState.stateTemp
                    currentState = new StateStruct() { flowState = State.Measure, stateChanged = true, stateCounts = 0, tempPoint = currentState.tempPoint };

                    Utils.Logger.Sys((bridgeSteadyTimeSec/60).ToString("0") + " 分钟电桥温度波动度小于 " + flucValue.ToString("0.0000") + "℃，可以测量电导率等数据");
                }
                //else
                //{
                //    // 不满足波动度条件，则重新返回控温流程
                //    currentState = new StateStruct() { flowState = State.TempControl, stateChanged = true, stateTime = 0, tempPoint = currentState.tempPoint };
                //    Utils.Logger.Sys("2 分钟后，测温电桥波动度不满足 xxx 条件，重新返回控温状态...");
                //    return;
                //}
            }
            
#endif

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
                Utils.Logger.Sys("电桥温度： " + tpDeviceS.temperatures.Last().ToString("0.0000"));
                Utils.Logger.Data("电桥温度： " + tpDeviceS.temperatures.Last().ToString("0.0000"));
                Utils.Logger.TempData("进入测量状态，温度设定点： " + currentState.tempPoint.stateTemp.ToString("0.0000"));
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
                currentState = new StateStruct() { flowState = State.TempDown, stateChanged = true, stateCounts = 0, tempPoint = temperaturePointList.First() };
                if(tpDeviceM.temperatures.Count !=0)
                {
                    // 默认是降温的过程
                    // 但是，如果设定温度点高于当前温度，还是应该修改为升温
                    if(tpDeviceM.temperatures.Last() < currentState.tempPoint.stateTemp)
                    {
                        currentState.flowState = State.TempUp;
                    }
                }
                else
                {
                    FlowControlFaultOccurEvent(FaultCode.CodeError);
                }

                Utils.Logger.Sys("开始下一个温度点的控温 - 稳定 - 测量流程...");
            }
            else
            {
                // 控制状态序列为空，说明实验已经结束了
                currentState = new StateStruct() { flowState = State.Finish };
                Utils.Logger.Sys("所有温度点均已测量完成...");
                Utils.Logger.TempData("所有温度点均已测量完成...");
            }


            // 是否还有其他相关操作
            // wghou
            // code
        }

        private void FinishStep()
        {
            // 触发温度点测量完成事件
            FlowControlStateChangedEvent(State.Finish);
            currentState = new StateStruct() { flowState = State.Idle, stateChanged = true, stateCounts = 0, tempPoint = new TemperaturePoint() };
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
