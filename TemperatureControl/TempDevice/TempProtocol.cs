using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace Device
{
    internal static class DevicePortName
    {
        public static List<string> upperName = new List<string>();
    }

    // 错误指令码
    public enum Err_t
    {
        NoError = 0,
        NotInRange,
        UnknownCmd,
        IncompleteCmd,
        BCCError,
        ComError,
        CodeError
    };

    internal class TempProtocol
    {
        // 从温控设备读取 / 写入参数所需的指令码
        #region 命令
        public enum Cmd_t
        {
            TempSet = 0,
            TempCorrect,
            LeadAdjust,
            Fuzzy,
            Ratio,
            Integral,
            Power,
            TempShow,
            PowerShow
        };

        private const string cmdHead_W = "@35W";
        private const string cmdHead_R = "@35R";
        private readonly string[] cmdWords = { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        private readonly string[] cmdFormats = { "0.000", "0.000", "0.000", "0", "0", "0", "0", "0.000", "0" };
        private const string cmdFinish = ":";
        private const string cmdEnd = "\r"; // Todo: The endflag may be \r\n, check it.
        private readonly string[] cmdRW = { "w", "w", "w", "w", "w", "w", "w", "r", "r" };
        #endregion

        #region 错误标识符
        private readonly string[] errorWords = { "A", "B", "C", "D" };
        private const char errorFlag = 'E';
        #endregion

        // 串口参数
        #region 串口设置
        private const int baudrate = 2400;
        private const int dataBits = 8;
        private const StopBits stopBits = StopBits.One;
        private const Parity parity = Parity.None;
        private const int readBufferSize = 64;
        private const int writeBufferSize = 64;
        private const int readTimeout = 5000;
        private SerialPort sPort = new SerialPort()
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

        // 命令 写-读 间隔
        private const int intervalOfWR = 20;
        #endregion


        // Public Methods
        #region Public Methods
        /// <summary>
        /// 初始化串口：新建串口，选择串口
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        public Err_t SetPort(string portName)
        {
            if (DevicePortName.upperName.Contains(portName.ToUpper()))
                return Err_t.ComError;

            string[] portNames = SerialPort.GetPortNames();

            if (portNames.Contains(portName.ToUpper()))
            {
                this.sPort.PortName = portName;
                DevicePortName.upperName.Add(portName.ToUpper());
                return Err_t.NoError;
            }

            // wghou
            // 未完成

            return Err_t.CodeError;
        }


        /// <summary>
        /// 设置设备参数
        /// </summary>
        /// <param name="cmdName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public Err_t SendData(Cmd_t cmdName, float sendValue)
        {
            // 关闭串口
            sPort.Close();

            // 检查参数是否支持写入
            if (cmdRW[(int)cmdName] != "w")
            {
                Debug.WriteLine(String.Format("参数 {0} 不支持写入!",Enum.GetName(cmdName.GetType(),cmdName)));
                return Err_t.CodeError;
            }

            // 通过串口写入指令
            string cmdSD = ConstructCmd(cmdName, sendValue, true);
            try
            {
                this.sPort.Open();
                this.sPort.Write(cmdSD);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("写入串口失败!");
                return Err_t.ComError;
            }

            // 从串口读取返回数据
            Thread.Sleep(intervalOfWR);
            string cmdBK = string.Empty;
            try
            {
                cmdBK = this.sPort.ReadTo(cmdFinish);
                this.sPort.DiscardInBuffer();
                this.sPort.Close();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("读取串口失败!");
                return Err_t.ComError;
            }
            
            // 判断返回数据
            Err_t err = IsError(cmdBK);
            return err;
        }


        /// <summary>
        /// 读取设备参数值
        /// </summary>
        /// <param name="cmdName">参数名称</param>
        /// <param name="readValue">参数值</param>
        /// <returns></returns>
        public Err_t ReadData(Cmd_t cmdName, out float readValue)
        {
            // 关闭串口
            sPort.Close();

            // 通过串口写入指令
            string cmdSD = ConstructCmd(cmdName, 0.0f, false);
            try
            {
                this.sPort.Open();
                this.sPort.Write(cmdSD);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("写入串口失败!");
                readValue = 0.0f;
                return Err_t.ComError;
            }

            // 从串口读取返回数据
            Thread.Sleep(intervalOfWR);
            string cmdBK = string.Empty;
            try
            {
                cmdBK = this.sPort.ReadTo(cmdFinish);
                this.sPort.DiscardInBuffer();
                this.sPort.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("读取串口失败!");
                readValue = 0.0f;
                return Err_t.ComError;
            }

            // 判断返回数据
            Err_t err = IsError(cmdBK);
            if (err != Err_t.NoError)
            {
                readValue = 0.0f;
            }
            else
            {
                readValue = float.Parse(cmdBK.Substring(5));
            }
            return err;
        }
        #endregion


        // private Methods
        #region Private Methods
        /// <summary>
        /// 创建指令
        /// </summary>
        /// <param name="commandName">指令名称</param>
        /// <param name="value">指令所对应的设备参数值</param>
        /// <param name="W_R">true for write, false for read</param>
        /// <returns></returns>
        private string ConstructCmd(Cmd_t commandName, float value, bool W_R)
        {
            string command = "";

            if (W_R)
            {
                command += cmdHead_W;
                command += cmdWords[(int)commandName];
                command += value.ToString(cmdFormats[(int)commandName]);
                command += cmdFinish;
                command += BCCCal(command, false);
                command += cmdEnd;
            }
            else
            {
                command += cmdHead_R;
                command += cmdWords[(int)commandName];
                command += cmdFinish;
                command += BCCCal(command, false);
                command += cmdEnd;
            }

            return command;
        }

        /// <summary>
        /// 计算指令的 BCC 校验码
        /// </summary>
        /// <param name="command">指令</param>
        /// <param name="ifCal">如果 ifCal == true, 则返回 BCC 码; 如果 ifCal == false, 则返回 ""</param>
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
        /// 判断设备是否返回了错误指令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private Err_t IsError(string command)
        {
            Err_t error = Err_t.NoError;

            if (command[3] == errorFlag)
            {
                error = (Err_t)(Array.IndexOf(errorWords, command[4].ToString()) + 1);
            }

            return error;
        }
        #endregion
    }
}
