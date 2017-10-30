using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace Device
{
    public class TempProtocol
    {
        #region Members

        #region Serial Port
        private const int baudrate = 2400;
        private const int dataBits = 8;
        private const StopBits stopBits = StopBits.One;
        private const Parity parity = Parity.None;
        private const int readBufferSize = 64;
        private const int writeBufferSize = 64;
        private const int readTimeout = 200;
        /// <summary>串口</summary>
        internal SerialPort sPort = new SerialPort()
        {
            // Init all parameters except portname, as other parameter should not be easily changed.
            BaudRate = baudrate,
            DataBits = dataBits,
            StopBits = stopBits,
            Parity = parity,
            ReadBufferSize = readBufferSize,
            WriteBufferSize = writeBufferSize,
            ReadTimeout = readTimeout
        };
        
        /// <summary>串口读-写时间间隔</summary>
        private const int intervalOfWR = 20;
        #endregion

        #region Error Code
        /// <summary>
        /// 温控设备错误代码
        /// </summary>
        public enum Err_t : int
        {
            /// <summary>无错误</summary>
            NoError = 0,
            /// <summary>数据超范围</summary>
            NotInRange,
            /// <summary>指令不存在</summary>
            UnknownCmd,
            /// <summary>指令不完整</summary>
            IncompleteCmd,
            /// <summary>校验和错误</summary>
            BCCError,
            /// <summary>设备未初始化</summary>
            //UnInited,
            /// <summary>串口错误</summary>
            ComError,
            /// <summary>程序错误</summary>
            CodeError
        };
        private readonly string[] errorWords = { "A", "B", "C", "D" };
        private const char errorFlag = 'E';
        #endregion

        #region Commands
        /// <summary>
        /// 温控设备指令代码
        /// </summary>
        public enum Cmd_t : int
        {
            /// <summary>温度设定值</summary>
            TempSet = 0,
            /// <summary>温度调整值</summary>
            TempCorrect,
            /// <summary>超前调整值</summary>
            LeadAdjust,
            /// <summary>模糊系数</summary>
            Fuzzy,
            /// <summary>比例系数</summary>
            Ratio,
            /// <summary>积分系数</summary>
            Integral,
            /// <summary>功率系数</summary>
            Power,
            /// <summary>当前温度值</summary>
            TempShow,
            /// <summary>当前功率值</summary>
            PowerShow
        };

        // 温控设备指令组成
        private const string cmdHead_W = "@35W";
        private const string cmdHead_R = "@35R";
        private readonly string[] cmdWords = { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        //private readonly string[] cmdFormats = { "0.000", "0.000", "0.000", "0", "0", "0", "0", "0.000", "0" };
        private readonly string[] cmdFormats = { "0.000", "0.000", "0.000", "0", "0", "0", "0", "0.0000", "0" };
        private const string cmdFinish = ":";
        //private const string cmdEnd = "\r"; // Todo: The endflag may be \r\n, check it.
        private const string cmdEnd = "\r\n"; // Todo: The endflag may be \r\n, check it.
        private readonly string[] cmdRW = { "w", "w", "w", "w", "w", "w", "w", "r", "r" };
        #endregion

        #endregion



        #region Public Methods
        /// <summary>
        /// 新建串口，并设置串口名称
        /// </summary>
        /// <param name="portName">串口名称</param>
        /// <returns></returns>
        internal bool SetPort(string portName)
        {
            try
            {
                try { sPort.Close(); } catch { }

                string[] portNames = SerialPort.GetPortNames();
                if (portNames.Contains(portName.ToUpper()))
                {
                    sPort.PortName = portName;
                }
                else
                {
                    return false;
                }

                // 串口打开 / 关闭测试
                // 不知道为什么，Thread.Sleep() 会引发 System.EntryPointNotFoundException 异常
                // 但是，并不会被 catch 到
                // 串口打开 / 关闭测试
                if (!sPort.IsOpen)
                    sPort.Open();
                Thread.Sleep(intervalOfWR);
                if (sPort.IsOpen)
                    sPort.Close();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("继电器设备新建串口时发生异常：" + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 向温控设备写入数据，返回错误代码
        /// </summary>
        /// <param name="cmd">参数指令符</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        internal Err_t SendData(Cmd_t cmd, float val)
        {
            // 不支持这四条指令
            Debug.Assert(cmd != Cmd_t.TempShow);
            Debug.Assert(cmd != Cmd_t.PowerShow);

            // 设置 温度设定值 / 温度调整值 / 超前调整值 / 模糊系数 / 比例系数 / 积分系数 / 功率系数
            if (cmdRW[(int)cmd] != "w")
            {
                return Err_t.CodeError;
            }

            // 创建指令
            string command = ConstructCommand(cmd, val, true);
            // 用于存放返回的数据
            string data = string.Empty;

            // 从串口发送指令
            try
            {
                if(!sPort.IsOpen)
                    this.sPort.Open();

                // 写入数据
                this.sPort.Write(command);
                //this.sPort.Write(command + "\r\n");
                // 读取返回数据
                Thread.Sleep(intervalOfWR);
                data = this.sPort.ReadTo(cmdFinish);
                //Improve: Add BCC checker
                sPort.DiscardInBuffer();
                sPort.Close();
            }
            catch(Exception ex)
            {
                // 串口发生错误！
                Debug.WriteLine("温控设备写入参数 " + cmd.ToString() + " 异常: " + ex.Message);
                // 关闭串口
                try { sPort.Close(); } catch { }
                return Err_t.ComError;
            }

            // 返回错误状态
            return IsError(data);
        }


        /// <summary>
        /// 从温控设备读取数据，返回错误代码
        /// </summary>
        /// <param name="cmd">参数指令符</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        internal Err_t ReadData(Cmd_t cmd, out float val)
        {
            // 读取 温度设定值 / 温度调整值 / 超前调整值 / 模糊系数 / 比例系数 / 积分系数 / 功率系数 / 温度显示值 / 功率显示值
            // 创建指令
            string command = ConstructCommand(cmd, 0.0f, false);
            // 用于存放返回的数据
            string data = string.Empty;

            try
            {
                if(!sPort.IsOpen)
                    this.sPort.Open();

                // 写入数据
                this.sPort.Write(command);
                //this.sPort.Write(command + "\r\n");
                // 读取返回数据
                Thread.Sleep(intervalOfWR);
                data = this.sPort.ReadTo(cmdFinish);
                //Improve: Add BCC checker
                sPort.DiscardInBuffer();
                sPort.Close();
            }
            catch(Exception ex)
            {
                val = 0.0f;
                Debug.WriteLine("温控设备读取参数 " + cmd.ToString() + " 异常: " + ex.Message);
                // 关闭串口
                try { sPort.Close(); } catch { }
                return Err_t.ComError;
            }

            // 检查错误并提取参数值
            Err_t err = IsError(data);
            if (err != Err_t.NoError)
            {
                // 发生错误
                val = 0.0f;
            }
            else
            {
                // 未发生错误
                // 如果格式转化错误，则返回 ComErr
                if (!float.TryParse(data.Substring(5), out val))
                    err = Err_t.ComError;
            }

            return err;
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Determine i there is any error in communication
        /// </summary>
        /// <returns>Is Error?</returns>
        private Err_t IsError(string cmd)
        {
            Err_t err = Err_t.NoError;

            if (cmd[3] == errorFlag)
            {
                err = (Err_t)(Array.IndexOf(errorWords, cmd[4].ToString()) + 1);
            }

            return err;
        }


        /// <summary>
        /// Construct command using given name and value
        /// </summary>
        /// <param name="commandName">Name of command</param>
        /// <param name="value">Value of command</param>
        /// <param name="W_R">True for write, false for read</param>
        /// <returns></returns>
        private string ConstructCommand(Cmd_t cmdName, float val, bool W_R)
        {
            string command = "";

            if (W_R)
            {
                command += cmdHead_W;
                command += cmdWords[(int)cmdName];
                command += val.ToString(cmdFormats[(int)cmdName]);
                command += cmdFinish;
                command += BCCCal(command, false);
                command += cmdEnd;
            }
            else
            {
                command += cmdHead_R;
                command += cmdWords[(int)cmdName];
                command += cmdFinish;
                command += BCCCal(command, false);
                command += cmdEnd;
            }

            return command;
        }

        /// <summary>
        /// Calculate BCC (similiar with CRC) by given command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="cal">If true, calculate and return BCC. Otherwise, return ""</param>
        /// <returns></returns>
        private string BCCCal(string command, bool ifCal)
        {
            string BCC = "";

            if (ifCal)
            {
                // Do not implement as it isn't used in current project
                // ...
            }
            else
            {
                BCC = "";
            }

            return BCC;
        }

        /// <summary>
        /// Check if BCC is correct
        /// </summary>
        /// <param name="command">Command returned by MCU</param>
        /// <param name="ifCheck">If true, check and return result. Otherwise, return true.</param>
        /// <returns></returns>
        private bool CheckBCC(string command, bool ifCheck)
        {
            return true;
        }
        #endregion
    }
}
