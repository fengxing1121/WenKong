using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Device
{
    internal class RelayProtocol
    {
        // 设备控制指令
        public enum Cmd_r
        {
            Elect = 0,
            MainHeat,
            MainCoolF,
            SubHeat,
            SubCoolF,
            SubCool,
            WaterIn,
            SubCircle,
            WaterOut
        };

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

        public Err_t SetRelay(Cmd_r cmdName, bool status)
        {
            if(status)
            {
                // 打开开关

            }
            else
            {
                // 关闭开关

            }

            return Err_t.NoError;
        }

        #endregion
    }
}
