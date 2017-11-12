using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace Device
{
    public class RelayProtocol
    {
        #region Members

        #region Serial Port
        /// <summary>串口</summary>
        private const int baudrate = 9600;
        private const int dataBits = 8;
        private const StopBits stopBits = StopBits.One;
        private const Parity parity = Parity.None;
        private const int readBufferSize = 64;
        private const int writeBufferSize = 64;
        private const int readTimeout = 200;
        /// <summary>串口</summary>
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
        /// <summary>串口读-写时间间隔</summary>
        private const int intervalOfWR = 20;
        #endregion

        #region Error Code
        /// <summary>
        /// Relay 设备错误代码
        /// </summary>
        public enum Err_r : int
        {
            /// <summary>无错误</summary>
            NoError = 0,
            /// <summary>设备未初始化</summary>
            CRCError,
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
        public enum Cmd_r : int
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
            WaterOut
        };
        #endregion

        // 继电器设备指令组成
        private byte cmdDeviceAddr = 0xFE ;
        private byte[] cmdOrder = { 0x01, 0x05, 0x15 };
        private UInt16[] cmdRelayAddr = { 0x0000, 0x0001, 0x0002, 0x0003, 0x0004, 0x0005, 0x0006, 0x0007, 0x0008, 0x0009, 0x000A, 0x000B, 0x000B, 0x000C, 0x000D, 0x000E, 0x000F };
        private UInt16[] cmdRelayOnOff = { 0xff00, 0x0000 };



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
                // 先主动关闭串口
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
                if(!sPort.IsOpen)
                    sPort.Open();
                Thread.Sleep(intervalOfWR);
                if(sPort.IsOpen)
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
        /// 设置继电器通断状态，返回错误信息
        /// </summary>
        /// <param name="cmd">指令代码</param>
        /// <param name="value">继电器状态</param>
        /// <returns></returns>
        internal Err_r WriteRelayStatus(Cmd_r cmd, bool status)
        {
            byte[] data = new byte[8];
            byte[] dataBack = new byte[8];

            data[0] = cmdDeviceAddr;    // 设备地址
            data[1] = cmdOrder[1];      // 写入
            data[2] = (byte)(cmdRelayAddr[(int)cmd]>>8 & 0x00ff);   // 寄存器地址 / 继电器地址
            data[3] = (byte)(cmdRelayAddr[(int)cmd] & 0x00ff);
            if (status == true)
                data[4] = 0xff;
            else
                data[4] = 0x00;
            data[5] = 0x00;

            // 生成 CRC16 校验码
            genCRC16(ref data);

            try
            {
                // 打开串口
                if(!sPort.IsOpen)
                    sPort.Open();

                // 写入数据
                sPort.Write(data, 0, data.Length);
                Thread.Sleep(intervalOfWR);

                // 读取返回数据
                // 这里不知道为什么，用 Read 读不到正确的返回数据，只能用 for 循环单个字节读取了
                // 有待改进
                // wghou
                //sPort.Read(dataBack, 0, dataBack.Length);
                for(int i = 0;i<dataBack.Length;i++)
                {
                    dataBack[i] =  (Byte)sPort.ReadByte();
                }

                // 关闭端口
                sPort.Close();

                // 判断返回值是否正确（返回值应与写入值完全一样）
                for (int i = 0;i<data.Length;i++ )
                    if(data[i] != dataBack[i])
                    {
                        //Debug.WriteLine("继电器设备 " + cmd.ToString() + " 写入状态时发生校验错误!");
                        return Err_r.CRCError;
                    }
            }
            catch(Exception ex)
            {
                // 当读取返回值时，如果返回错误代码的长度不够 8 位，则会抛出超时异常，也视为串口错误
                //Debug.WriteLine("继电器设备 " + cmd.ToString() + " 写入继电器状态错误！");
                Debug.WriteLine("继电器设备 " + cmd.ToString() + " 写入状态时异常：" + ex.Message);
                // 关闭串口
                try { sPort.Close(); } catch { }
                return Err_r.ComError;
            }

            return Err_r.NoError;
        }


        /// <summary>
        /// 读取全部继电器的状态
        /// </summary>
        /// <returns>返回错误状态</returns>
        internal Err_r ReadAllRelayStatus(out UInt16 status)
        {
            sPort.Close();

            byte[] data = new byte[8];
            byte[] dataBack = new byte[7];
            data[0] = cmdDeviceAddr;    // 设备地址
            data[1] = cmdOrder[0];      // 写入
            data[2] = 0x00;   // 寄存器地址 / 继电器地址
            data[3] = 0x00;
            data[4] = 0x00;
            data[5] = 0x10;
            // 生成 CRC16 校验码
            genCRC16(ref data);

            try
            {
                if(!sPort.IsOpen)
                    sPort.Open();

                sPort.Write(data, 0, data.Length);
                Thread.Sleep(intervalOfWR);

                // 读取返回数据
                sPort.Read(dataBack, 0, dataBack.Length);

                // 返回的结果值
                status = (UInt16)(((UInt16)dataBack[3] << 8) | (UInt16)dataBack[4]);
                // 返回的校验码
                byte[] crc = { dataBack[5], dataBack[6] };
                genCRC16(ref dataBack);

                // 关闭端口
                sPort.Close();

                if (crc[0] == dataBack[5] && crc[1] == dataBack[6])
                    return Err_r.NoError;
                else
                    return Err_r.CRCError;
            }
            catch (Exception ex)
            {
                status = 0x0000;
                //Debug.WriteLine("继电器设备读取继电器状态错误！");
                Debug.WriteLine("继电器设备读取状态时异常：" + ex.Message);
                // 关闭串口
                try { sPort.Close(); } catch { }
                return Err_r.ComError;
            }

        }

        #endregion


        #region Private Methods
        private bool genCRC16(ref byte[] dt)
        {
            if (dt.Length < 2)
                return false;
            UInt16 xda = 0xffff;
            UInt16 xdapoly = 0xA001;
            UInt16 xdabit;

            for(int i = 0;i<dt.Length - 2;i++)
            {
                xda ^= dt[i];
                for(int j = 0;j<8;j++)
                {
                    xdabit = (UInt16)(xda & 0x01);
                    xda >>= 1;
                    if (xdabit == 1)
                        xda ^= xdapoly;
                }
            }

            // 写入CRC16校验码
            dt[dt.Length - 2] = (byte)(xda & 0xff);
            dt[dt.Length - 1] = (byte)(xda >> 8);
            return true;
        }
        #endregion
    }
}
