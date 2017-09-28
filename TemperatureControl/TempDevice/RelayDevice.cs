using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;

namespace Device
{
    /// <summary>
    /// 
    /// </summary>
    public enum RelayDeviceError
    {
        NOERROR = 0,
        CODEERROR,
        COMERROR,
        ERROR
    }


    /// <summary>
    /// 
    /// </summary>
    public class RelayCmd
    {

    }


    /// <summary>
    /// 继电器
    /// </summary>
    public class RelayDevice : RelayCmd
    {
        private readonly object _Locker = new object();
        private string _portName = string.Empty;
        private SerialPort _sPort = null;

        /// <summary>
        /// 新建串口
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        public RelayDeviceError InitCom(string portName)
        {
            try
            {
                _portName = portName;
                _sPort = new SerialPort(portName, 2400, Parity.None, 8, StopBits.One);
                // 设置读取超时 500ms
                _sPort.ReadTimeout = 500;
                _sPort.Open();
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine("新建串口 "+ portName + " 失败！");
                return RelayDeviceError.COMERROR;
            }
            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError CloseCom()
        {
            if (_sPort == null)
                return RelayDeviceError.NOERROR;

            try
            {
                _sPort.Close();
                _sPort = null;
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine("关闭串口 " + _portName  + " 失败！");
                return RelayDeviceError.COMERROR;
            }
            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError openElect()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError closeElect()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError openMainHeat()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError closeMainHeat()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError openMainCoolF()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError closeMainCoolF()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError openSubHeat()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError closeSubHeat()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError openSubCoolF()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError closeSubCoolF()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError openSubCool()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError closeSubCool()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError openWaterIn()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError closeWaterIn()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError openSubCircle()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError closeSubCircle()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError openWaterOut()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RelayDeviceError closeWaterOut()
        {
            lock (_Locker)
            {

            }

            return RelayDeviceError.NOERROR;
        }
    }
}
