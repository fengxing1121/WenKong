using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

namespace Device
{
    public class SensorDevice
    {
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


        #region Public Members
        public string srDeviceName = string.Empty;
        public string srDevicePortName = string.Empty;
        #endregion


        #region Private Members
        /// <summary>
        /// 传感器测得的温度值
        /// </summary>
        private List<float> temperatures = new List<float>();
        /// <summary>
        /// 锁
        /// </summary>
        private Object srLocker = new object();
        #endregion


        #region Public Methods
        /// <summary>
        /// 初始化传感器设备 - 设定串口
        /// </summary>
        /// <param name="portName">端口名称</param>
        /// <returns></returns>
        public bool SetDevicePortName(string portName)
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
                if (!sPort.IsOpen)
                    sPort.Open();
                Thread.Sleep(intervalOfWR);
                if (sPort.IsOpen)
                    sPort.Close();
                srDevicePortName = portName;
                return true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("继电器设备新建串口时发生异常：" + ex.Message);
                return false;
            }

            return true;
        }


        /// <summary>
        /// 传感器设备自检
        /// </summary>
        /// <returns></returns>
        public bool SelfCheck()
        {

            return true;
        }


        /// <summary>
        /// 获取传感器的值，存储在 SensorDevide.temperature 列表中，返回错误状态
        /// </summary>
        /// <param name="val">电桥温度值</param>
        /// <returns></returns>
        public bool GetSensorValue(out float val)
        {
            bool st = true;
            if(st)
            {
                // 读取电桥温度值
                val = 10.0f;
                temperatures.Add(val);
            }
            else
            {
                // 电桥温度值为 0 
                val = 0.0f;
            }
            
            // 返回错误状态
            return st;
        }


        /// <summary>
        /// 读取电桥温度值，存储在 SensorDevide.temperature 列表中，返回错误状态
        /// </summary>
        /// <returns></returns>
        public bool UpdateSensorValue()
        {
            float val = 0.0f;
            // 读取电桥温度值，返回错误状态
            return GetSensorValue(out val);
        }


        /// <summary>
        /// 计算并获取温度波动值
        /// </summary>
        /// <param name="count">温度监测次数</param>
        /// <param name="fluctuation">温度波动值</param>
        /// <returns>返回成功与否</returns>
        public bool GetFluc(int count, out float fluctuation)
        {
            lock (srLocker)
            {
                if (temperatures.Count == 0 || temperatures.Count < count)
                {
                    // If there is not temperature data in list, output extreme fluctuation
                    fluctuation = -1;
                    return false;
                }
                else
                {
                    fluctuation = temperatures.GetRange(temperatures.Count - count, count).Max() -
                        temperatures.GetRange(temperatures.Count - count, count).Min();
                    return true;
                }
            }
        }


        /// <summary>
        /// 判断传感器设备的温度波动度是否满足条件
        /// </summary>
        /// <param name="secends">时间长度 / 秒</param>
        /// <param name="crt">波动度阈值</param>
        /// <returns></returns>
        public bool chekFluc(int count, float crt)
        {
            float fluc = 0.0f;
            if (!GetFluc(count, out fluc))
                return false;
            else
                return fluc < crt;
        }
        #endregion
    }
}
