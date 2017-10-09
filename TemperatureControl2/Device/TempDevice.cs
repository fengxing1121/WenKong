using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Device
{
    public class TempDevice
    {
        // 温控设备
        #region Members
        // 设备
        public bool tpDeviceInited = false;
        public string tpDeviceName = string.Empty;
        public string tpDevicePortName = string.Empty;
        TempProtocol tpDevice = new TempProtocol();
        // 设备参数
        public float[] tpParam = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.1f };
        public float[] tpParamToSet = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.1f };
        public readonly string[] tpParamFormat = { "0.000", "0.000", "0.000", "0", "0", "0", "0", "0.000", "0.000" };
        public readonly string[] tpParamNames =
            { "设定值    ", "调整值    ", "超前调整值", "模糊系数  ", "比例系数  ", "积分系数  ", "功率系数  ", "波动度阈值", "温度阈值  " };

        /// <summary>
        /// 暂时未使用
        /// </summary>
        public float tpFluc = 0.0f;
        public float tpPowerShow = 0.0f;
        public List<float> temperatures = new List<float>();
        public int tempMaxLen = 1000;
        public int readTempInterval = 1000;
        public readonly string powerShowFormat = "0";
        public readonly string tempShowFormat = "0.000";

        /// <summary>
        /// 温控设备设备线程锁，同一时间只允许单一线程访问设备资源（串口 / 数据）
        /// </summary>
        private object tpLocker = new object();
        #endregion

        #region Event
        public delegate void ParamUpdatedEventHandler(bool st, TempProtocol.Err_t err);
        public event ParamUpdatedEventHandler ParamUpdatedToDeviceEvent;
        public event ParamUpdatedEventHandler ParamUpdatedFromDeviceEvent;
        #endregion



        #region Public Methods

        /// <summary>
        /// 温控设备初始化，触发 tpInitDeviceEvent 事件
        /// </summary>
        /// <param name="init">初始化状态，false 则表示关闭设备</param>
        public void InitTempDevice(bool init)
        {
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;
            // 线程锁
            lock (tpLocker)
            {
                // 先主动关闭串口
                tpDevice.sPort.Close();

                // 初始化设备
                if (init == true)
                {
                    if (tpDevice.SetPort("COM1"))
                        tpDeviceInited = true;
                    else
                        tpDeviceInited = false;
                }
                // 关闭设备
                else
                {
                    tpDevice.sPort.Close();
                    tpDeviceInited = false;
                }
            }

        }

        public void UpdateParamToDevice()
        {
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;

            // 更新 xx 阈值
            tpParam[7] = tpParamToSet[7];
            tpParam[8] = tpParamToSet[8];

            // 更新硬件设备参数
            int i = 0;
            for(i = 0;i<7;i++)
            {
                lock(tpLocker)
                {
                    if(Math.Abs(tpParam[i] - tpParamToSet[i]) > 10e-5)
                        err = tpDevice.SendData((TempProtocol.Cmd_t)i, tpParam[i]);
                }

                // 调试信息
                Debug.WriteLineIf(err == TempProtocol.Err_t.NoError,"温控设备参数设置成功!  " + tpParamNames[i] + ": " + tpParam[i].ToString());
                Debug.WriteLineIf(err != TempProtocol.Err_t.NoError, "温控设备参数设置失败!  " + tpParamNames[i] + ": " + err.ToString());

                // 如发生错误，则结束
                if (err != TempProtocol.Err_t.NoError)
                    break;
            }

            // 结果处理 - 事件
            if(err == TempProtocol.Err_t.NoError)
            {
                // wghou
                // 参数设置成功
                ParamUpdatedToDeviceEvent(true, TempProtocol.Err_t.NoError);
            }
            else
            {
                // wghou
                // 参数设置失败
                ParamUpdatedToDeviceEvent(false, err);
            }

        }


        public void UpdateParamFromDevice()
        {
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;
            int i = 0;
            float val = 0.0f;
            for (i = 0; i < 7; i++)
            {
                lock (tpLocker)
                {
                    err = tpDevice.ReadData((TempProtocol.Cmd_t)i, out val);
                }

                // 调试信息
                Debug.WriteLineIf(err == TempProtocol.Err_t.NoError, "温控设备参数读取成功!  " + tpParamNames[i] + ": " + tpParam[i].ToString());
                Debug.WriteLineIf(err != TempProtocol.Err_t.NoError, "温控设备参数读取失败!  " + tpParamNames[i] + ": " + err.ToString());

                // 如发生错误，则结束
                if (err != TempProtocol.Err_t.NoError)
                    break;

                // 如果没有发生错误，则在上位机更新数据
                tpParam[i] = val;
            }

            // 结果处理 - 事件
            if (err == TempProtocol.Err_t.NoError)
            {
                // wghou
                // 参数设置成功
                ParamUpdatedFromDeviceEvent(true, TempProtocol.Err_t.NoError);
            }
            else
            {
                // wghou
                // 参数设置失败
                ParamUpdatedFromDeviceEvent(false, err);
            }

        }


        /// <summary>
        /// 从温控设备硬件读取温度显示值，发生错误则返回上一个状态时的温度值
        /// </summary>
        /// <param name="val">温度显示值</param>
        public void GetTemperatureShow( out float val)
        {
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;
            lock (tpLocker)
            {
                err = tpDevice.ReadData(TempProtocol.Cmd_t.TempShow, out val);
                if(err == TempProtocol.Err_t.NoError)
                {
                    // 未发生错误
                    AddTemperature(val);
                }
                else
                {
                    if (temperatures.Count > 0)
                        val = temperatures.Last();
                    else
                        val = 0.0f;
                }
            }

            if(err != TempProtocol.Err_t.NoError)
            {
                // wghou
                // 参数读取发生错误
            }
        }


        /// <summary>
        /// 从温控设备硬件读取功率显示值，如发生错误，则返回上一状态时的功率值
        /// </summary>
        /// <param name="val">功率显示值</param>
        public void GetPowerShow( out float val)
        {
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;
            lock (tpLocker)
            {
                err = tpDevice.ReadData(TempProtocol.Cmd_t.PowerShow, out val);
                if (err == TempProtocol.Err_t.NoError)
                {
                    // 未发生错误
                    tpPowerShow = val;
                }
                else
                {
                    val = tpPowerShow;
                }
            }

            if(err != TempProtocol.Err_t.NoError)
            {
                // wghou
                // 参数读取发生了错误
            }
        }

        /// <summary>
        /// 获取温度波动值
        /// </summary>
        /// <param name="count">温度监测次数</param>
        /// <param name="fluctuation">温度波动值</param>
        /// <returns></returns>
        public bool GetFluc(int count, out float fluctuation)
        {
            lock (tpLocker)
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

        #endregion


        #region Private Methods
        /// <summary>
        /// 获取温度列表中的最大温度值和最小温度值
        /// </summary>
        /// <param name="count">范围</param>
        /// <param name="tempMax">最大温度值</param>
        /// <param name="tempMin">最小温度值</param>
        /// <returns></returns>
        private bool GetMaxMinTemperatures(int count, out float tempMax, out float tempMin)
        {
            if (temperatures.Count == 0 || temperatures.Count < count)
            {
                // There is no data in list
                tempMax = 10000;
                tempMin = -1000;
                return false;
            }
            else
            {
                // 获取温度最大值 / 最小值
                tempMin = temperatures.GetRange(temperatures.Count - count, count).Min();
                tempMax = temperatures.GetRange(temperatures.Count - count, count).Max();
                return true;
            }
        }


        /// <summary>
        /// 向温度值列表中添加温度值
        /// </summary>
        /// <param name="val">温度值</param>
        private void AddTemperature(float val)
        {
            if (temperatures.Count == tempMaxLen)
            {
                temperatures.RemoveAt(0);
            }
            temperatures.Add(val);
        }
        #endregion
    }
}
