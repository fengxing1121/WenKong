using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;

namespace Device
{
    public enum TempDeviceError
    {
        NOERROR = 0,
        CODEERROE,
        COMERROR,
        OUTOFRANGE,
        CMDNOTEXIT,
        CMDNOTCOMPLETE,
        BCCERROE
    }


    /// <summary>
    /// 温控仪器
    /// </summary>
    public class TempCmd
    {
        protected Dictionary<string, string> setCmd = new Dictionary<string, string>()
        {
            {"A","@35WA" },
            {"B","@35WB" },
            {"C","@35WC" },
            {"D","@35WD" },
            {"E","@35WE" },
            {"F","@35WF" },
            {"G","@35WG" }
        };
        protected Dictionary<string, string> chkCmd = new Dictionary<string, string>()
        {
            {"A","@35RA" },
            {"B","@35RB" },
            {"C","@35RC" },
            {"D","@35RD" },
            {"E","@35RE" },
            {"F","@35RF" },
            {"G","@35RG" },
            {"H","@35RH" },
            {"I","@35RI" }
        };
        protected Dictionary<string, string> errCmd = new Dictionary<string, string>()
        {
            {"A","@35EA" },
            {"B","@35EB" },
            {"C","@35EC" },
            {"D","@35ED" }
        };

        public void resetAdd(uint addH,uint addL)
        { }
    }


    public class TempDevice : TempCmd
    {
        private readonly object _Locker = new object();
        private string _portName = string.Empty;
        private SerialPort _sPort = null;

        /// <summary>
        /// 新建串口
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        public TempDeviceError InitCom(string portName)
        {
            try
            {
                _portName = portName;
                _sPort = new SerialPort(portName,2400,Parity.None,8,StopBits.One);
                // 设置读取超时 500ms
                _sPort.ReadTimeout = 500;
                _sPort.Open();
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine("新建串口 " + portName + " 失败！");
                return TempDeviceError.COMERROR;
            }
            return TempDeviceError.NOERROR;
        }


        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public TempDeviceError CloseCom()
        {
            if (_sPort == null)
                return TempDeviceError.NOERROR;

            try
            {
                _sPort.Close();
                _sPort = null;
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine("关闭串口 " + _portName + " 失败！");
                return TempDeviceError.COMERROR;
            }
            return TempDeviceError.NOERROR;
        }

        /// <summary>
        /// 设置温度值，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public TempDeviceError setTemperature(double temp)
        {
            lock(_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendSetCmd("A", temp);

                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readSetData("A");
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }
            
            return TempDeviceError.NOERROR;
        }

        /// <summary>
        /// 从仪表读取温度，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public TempDeviceError getTemperature( ref double temp)
        {
            lock(_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendGetCmd("A");
                if(err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readGetData("A", ref temp);
                if(err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }

            return TempDeviceError.NOERROR;
        }


        /// <summary>
        /// 设置温度修正值，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="tempR"></param>
        /// <returns></returns>
        public TempDeviceError setTemperatrueRev(double tempR)
        {
            lock(_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendSetCmd("B", tempR);
                if(err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readSetData("B");
                if(err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 从仪表读取温度修正值，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="tempR"></param>
        /// <returns></returns>
        public TempDeviceError getTemperatureRev(ref double tempR)
        {
            lock(_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendGetCmd("B");
                if(err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readGetData("B", ref tempR);
                if(err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }

            return TempDeviceError.NOERROR;

        }

        /// <summary>
        /// 设置超前调整值，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="adjust"></param>
        /// <returns></returns>
        public TempDeviceError setAdvanceAdjust(double adjust)
        {
            lock (_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendSetCmd("C", adjust);
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readSetData("C");
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 读取超前调整值，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="adjust"></param>
        /// <returns></returns>
        public TempDeviceError getAdvanceAdjust(ref double adjust)
        {
            lock (_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendGetCmd("C");
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readGetData("C", ref adjust);
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 设置模糊系数，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="fuzzy"></param>
        /// <returns></returns>
        public TempDeviceError setFuzzyCoef(double fuzzy)
        {
            lock (_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendSetCmd("D", fuzzy);
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readSetData("D");
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 读取模糊系数，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="fuzzy"></param>
        /// <returns></returns>
        public TempDeviceError getFuzzeCoef(ref double fuzzy)
        {
            lock (_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendGetCmd("D");
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readGetData("D", ref fuzzy);
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 设置比例系数，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public TempDeviceError setProportionCoef(double prop)
        {
            lock (_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendSetCmd("E", prop);
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readSetData("E");
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 读取比例系数，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public TempDeviceError getProportionCoef(ref double prop)
        {
            lock (_Locker)
            {
                // 创建并发送指令
                TempDeviceError err = sendGetCmd("E");
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }

                // 接收下位机返回信息
                err = readGetData("E", ref prop);
                if (err != TempDeviceError.NOERROR)
                {
                    return err;
                }
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 设置积分系数，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="integral"></param>
        /// <returns></returns>
        public TempDeviceError setIntegralCoef(double integral)
        {
            // 创建并发送指令
            TempDeviceError err = sendSetCmd("F", integral);
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            // 接收下位机返回信息
            err = readSetData("F");
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 读取积分系数，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="integral"></param>
        /// <returns></returns>
        public TempDeviceError getIntegralCoef(ref double integral)
        {
            // 创建并发送指令
            TempDeviceError err = sendGetCmd("F");
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            // 接收下位机返回信息
            err = readGetData("F", ref integral);
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 设置功率系数，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public TempDeviceError setPowerCoef(double power)
        {
            // 创建并发送指令
            TempDeviceError err = sendSetCmd("G", power);
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            // 接收下位机返回信息
            err = readSetData("G");
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 读取功率系数，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public TempDeviceError getPowerCoef(ref double power)
        {
            // 创建并发送指令
            TempDeviceError err = sendGetCmd("G");
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            // 接收下位机返回信息
            err = readGetData("G", ref power);
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 读取温度显示值，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="tempD"></param>
        /// <returns></returns>
        public TempDeviceError getTemperatureDisp(ref double tempD)
        {
            // 创建并发送指令
            TempDeviceError err = sendGetCmd("H");
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            // 接收下位机返回信息
            err = readGetData("H", ref tempD);
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            return TempDeviceError.NOERROR;

        }


        /// <summary>
        /// 读取加热功率，返回错误类型 TempDeviceError
        /// </summary>
        /// <param name="powerH"></param>
        /// <returns></returns>
        public TempDeviceError getHeatPower(ref double powerH)
        {
            // 创建并发送指令
            TempDeviceError err = sendGetCmd("I");
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            // 接收下位机返回信息
            err = readGetData("I", ref powerH);
            if (err != TempDeviceError.NOERROR)
            {
                return err;
            }

            return TempDeviceError.NOERROR;
        }

        /// <summary>
        /// 发送一个读取参数值指令
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        private TempDeviceError sendGetCmd(string _key)
        {
            // 参数格式错误
            Debug.Assert(_key.Length == 1);

            string cmd = string.Empty;
            uint rcc = 0;

            // 创建指令
            cmd = cmd.Insert(0, rcc.ToString());
            cmd = cmd.Insert(0, ":");
            // 这里必须要确保 A 指令存在
            if (chkCmd.ContainsKey(_key))
                cmd = cmd.Insert(0, chkCmd[_key]);
            else
            {
                Debug.WriteLine("创建 R"+ _key + " 指令错误！");
                return TempDeviceError.CODEERROE;
            }

            // 生成 rcc 校验

            // 发送指令
            try
            {
                _sPort.WriteLine(cmd);
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine("发送 R" + _key + " 指令错误！");
                return TempDeviceError.COMERROR;
            }

            return TempDeviceError.NOERROR;
        }


        /// <summary>
        /// 发送一个设置参数指令
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        private TempDeviceError sendSetCmd(string _key,double _value)
        {
            // 参数格式错误
            Debug.Assert(_key.Length == 1);

            string cmd = string.Empty;
            uint rcc = 0;

            // 创建指令
            cmd = cmd.Insert(0, rcc.ToString());
            cmd = cmd.Insert(0, ":");
            cmd = cmd.Insert(0, _value.ToString());
            // 这里必须要确保 A 指令存在
            if (setCmd.ContainsKey(_key))
                cmd = cmd.Insert(0, setCmd[_key]);
            else
            {
                Debug.WriteLine("创建 W" + _key + " 指令错误！");
                return TempDeviceError.CODEERROE;
            }

            // wghou
            // 生成 rcc 校验

            // 检查端口是否创建
            if (_sPort == null)
            {
                Debug.WriteLine("串口未创建！");
                return TempDeviceError.COMERROR;
            }

            // 发送指令
            try
            {
                _sPort.WriteLine(cmd);
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine("发送指令 W" + _key + "错误！");
                return TempDeviceError.COMERROR;
            }

            return TempDeviceError.NOERROR;
        }


        /// <summary>
        /// 向设备发送设置参数指令后，读取返回值
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        private TempDeviceError readSetData(string _key)
        {
            // 参数格式错误
            Debug.Assert(_key.Length == 1);

            string data = string.Empty;

            // 检查端口是否创建
            if (_sPort == null)
            {
                Debug.WriteLine("串口未创建！");
                return TempDeviceError.COMERROR;
            }

            // 从串口读取数据
            try
            {
                data = _sPort.ReadLine();
                //Console.WriteLine(data);
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine("发送指令 W{" + _key + " 后，读取数据失败！");
                return TempDeviceError.COMERROR;
            }

            // 奇偶校验
            // rcc

            // 检查返回数据
            if (data.Contains(chkCmd[_key]))
            {
                // 数据正确
                return TempDeviceError.NOERROR;
            }
            // 检查仪表是否回送错误信息，检测到错误信息则返回
            else if (data.Contains(errCmd["A"]))
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 数据超范围！");
                return TempDeviceError.OUTOFRANGE;
            }
            else if (data.Contains(errCmd["B"]))
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 该指令不存在！");
                return TempDeviceError.CMDNOTEXIT;
            }
            else if (data.Contains(errCmd["C"]))
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 指令不完整！");
                return TempDeviceError.CMDNOTCOMPLETE;
            }
            else if (data.Contains(errCmd["D"]))
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 校验和错误！");
                return TempDeviceError.BCCERROE;
            }
            // 程序出现错误
            else
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 程序出现未知错误！");
                return TempDeviceError.CODEERROE;
            }

        }


        /// <summary>
        /// 向设备发送读取参数指令后，读取返回值
        /// </summary>
        /// <returns></returns>
        private TempDeviceError readGetData(string _key, ref double _value)
        {
            // 参数格式错误
            Debug.Assert(_key.Length == 1);

            string data = string.Empty;

            if (_sPort == null)
            {
                Debug.WriteLine("串口未创建！");
                return TempDeviceError.COMERROR;
            }

            // 从串口读取数据
            try
            {
                data = _sPort.ReadLine();
                //Console.WriteLine(data);
            }
            catch (System.IO.IOException ex)
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，读取数据失败！");
                return TempDeviceError.COMERROR;
            }

            // 奇偶校验
            // rcc

            // 检查返回数据
            if (data.Contains(setCmd[_key]))
            {
                // 数据正确
                // wghou
                // 数据的截取计算还需要修改
                _value = double.Parse(data.Substring(5, data.Length - 7));
                return TempDeviceError.NOERROR;
            }
            // 检查仪表是否回送错误信息，检测到错误信息则返回
            else if (data.Contains(errCmd["A"]))
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 数据超范围！");
                return TempDeviceError.OUTOFRANGE;
            }
            else if (data.Contains(errCmd["B"]))
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 该指令不存在！");
                return TempDeviceError.CMDNOTEXIT;
            }
            else if (data.Contains(errCmd["C"]))
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 指令不完整！");
                return TempDeviceError.CMDNOTCOMPLETE;
            }
            else if (data.Contains(errCmd["D"]))
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 校验和错误！");
                return TempDeviceError.BCCERROE;
            }
            // 程序出现错误
            else
            {
                Debug.WriteLine("发送指令 W" + _key + " 后，监测到 程序出现位置错误！");
                return TempDeviceError.CODEERROE;
            }

        }

    }

}
