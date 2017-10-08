using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace Device
{
    public class RelayProtocol
    {
        #region Members

        #region Serial Port
        /// <summary>串口</summary>
        private SerialPort sPort = null;
        /// <summary>串口读-写时间间隔</summary>
        private const int intervalOfWR = 20;
        #endregion

        #region Error Code
        /// <summary>
        /// Relay 设备错误代码
        /// </summary>
        public enum Err_r
        {
            /// <summary>无错误</summary>
            NoError = 0,
            /// <summary>设备未初始化</summary>
            UnInited,
            /// <summary>串口错误</summary>
            ComError,
            /// <summary>程序错误</summary>
            CodeError
        };
        #endregion

        #region Commands
        /// <summary>
        /// Relay 设备指令代码
        /// </summary>
        public enum Cmd_r
        {
            /// <summary>总电源</summary>
            Elect = 0,
            /// <summary>主槽控温</summary>
            MainHeat,
            /// <summary>辅槽控温</summary>
            SubHeat,
            /// <summary>辅槽制冷</summary>
            SubCool,
            /// <summary>辅槽循环</summary>
            SubCircle,
            /// <summary>主槽快冷</summary>
            MainCoolF,
            /// <summary>辅槽快冷</summary>
            SubCoolF,
            /// <summary>海水进</summary>
            WaterIn,
            /// <summary>海水出</summary>
            WaterOut,
            /// <summary>设备初始化</summary>
            InitDevice
        };
        #endregion

        #endregion



        #region Public Methods
        /// <summary>
        /// 新建串口，设置串口名称
        /// </summary>
        /// <param name="portName">串口名称</param>
        /// <returns></returns>
        internal bool SetPort(string portName)
        {
            try
            {
                sPort = new SerialPort(portName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("新建串口失败!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置继电器通断状态
        /// </summary>
        /// <param name="cmd">指令代码</param>
        /// <param name="value">继电器状态</param>
        /// <returns></returns>
        internal Err_r SendData(Cmd_r cmd, bool status)
        {
            return Err_r.NoError;
        }
        #endregion


        #region Private Methods

        #endregion
    }
}
