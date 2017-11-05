using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Device
{
    public class TempDevice
    {
        // 温控设备
        #region Members
        // 设备
        public string tpDeviceName = string.Empty;
        public string tpDevicePortName = string.Empty;
        private TempProtocol tpDevice = new TempProtocol();
        /// <summary>
        /// 设备参数值 - 9个
        /// </summary>
        public float[] tpParam = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.1f };
        /// <summary>
        /// 设备参数设定值 - 9个
        /// </summary>
        public float[] tpParamToSet = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.1f };
        public readonly string[] tpParamFormat = { "0.000", "0.000", "0.000", "0", "0", "0", "0", "0.000", "0.000" };
        public readonly string[] tpParamNames =
            { "设定值    ", "调整值    ", "超前调整值", "模糊系数  ", "比例系数  ", "积分系数  ", "功率系数  ", "波动度阈值", "温度阈值  " };

        /// <summary>
        /// 暂时未使用
        /// </summary>
        public float tpPowerShow = 0.0f;
        public List<float> temperatures = new List<float>();
        private int tempMaxLen = 1000;
        public int readTempInterval = 1000;

        /// <summary>
        /// 温控设备设备线程锁，同一时间只允许单一线程访问设备资源（串口 / 数据）
        /// </summary>
        private object tpLocker = new object();
        #endregion

        #region Event
        public delegate void ParamUpdatedEventHandler(TempProtocol.Err_t err);
        /// <summary>
        /// 温控设备向下位机写入参数完成事件
        /// </summary>
        public event ParamUpdatedEventHandler ParamUpdatedToDeviceEvent;
        /// <summary>
        /// 温控设备从下位机读取参数完成事件
        /// </summary>
        public event ParamUpdatedEventHandler ParamUpdatedFromDeviceEvent;
        #endregion



        #region Public Methods

        /// <summary>
        /// 温控设备初始化，并设置串口名称
        /// </summary>
        /// <param name="init">初始化状态，false 则表示初始化失败</param>
        /// <returns>返回设置状态</returns>
        public bool SetDevicePortName(string portName)
        {
            // 线程锁
            lock (tpLocker)
            {
                // 设置设备串口
                bool err = tpDevice.SetPort(portName);
                // 如果设置成功，则保存串口名称
                if(err)
                    tpDevicePortName = portName;

                return err;
            }

        }


        /// <summary>
        /// 温控设备自检
        /// </summary>
        /// <returns></returns>
        public TempProtocol.Err_t SelfCheck()
        {
            float val = 0.0f;
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;
            lock (tpLocker)
            {
                // 读取温度显示值
                err = tpDevice.ReadData(TempProtocol.Cmd_t.TempShow, out val);
                if (err != TempProtocol.Err_t.NoError)
                    return err;
                AddTemperature(val);
                Thread.Sleep(100);

                // 读取功率显示值
                err = tpDevice.ReadData(TempProtocol.Cmd_t.PowerShow, out val);
                if (err != TempProtocol.Err_t.NoError)
                    return err;
                tpPowerShow = val;
                Thread.Sleep(100);

                // 读取温控设备其他参数
                for (int i = 0;i<7;i++)
                {
                    err = tpDevice.ReadData((TempProtocol.Cmd_t)i, out val);
                    if (err != TempProtocol.Err_t.NoError)
                        break;

                    tpParam[i] = val;
                    // wghou
                    // 时间间隔可以再调整
                    Thread.Sleep(100);
                }
            }
            // 从温控设备读取全部参数值，返回错误标志
            return err;
        }


        /// <summary>
        /// 将 tpParamToSet[] 中的参数值写入下位机；
        /// 如发生错误，则立即停止并触发参数写入事件（附带错误信息）；
        /// 如成功，则触发参数写入事件（附带 NoError 信息）
        /// </summary>
        public void UpdateParamToDevice()
        {
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;

            // 更新 xx 阈值
            tpParam[7] = tpParamToSet[7];
            tpParam[8] = tpParamToSet[8];

            // 更新硬件设备参数
            lock(tpLocker)
            {
                for ( int i = 0; i < 7; i++)
                {
                    // 如果参数未改变，则跳过
                    if (Math.Abs(tpParam[i] - tpParamToSet[i]) < 10e-5)
                        continue;

                    // 向设备写入参数
                    err = tpDevice.SendData((TempProtocol.Cmd_t)i, tpParamToSet[i]);

                    // 调试信息
                    Debug.WriteLineIf(err == TempProtocol.Err_t.NoError, "温控设备参数设置成功!  " + tpParamNames[i] + ": " + tpParam[i].ToString());
                    Debug.WriteLineIf(err != TempProtocol.Err_t.NoError, "温控设备参数设置失败!  " + tpParamNames[i] + ": " + err.ToString());

                    // 如发生错误，则结束 for 循环
                    if (err != TempProtocol.Err_t.NoError)
                        break;

                    // 将更新后的参数值写入 tpParam[] 中
                    tpParam[i] = tpParamToSet[i];
                }
            }
            
            // 说明：
            // 参数更新函数主要面向 FormSetting 窗口，分别设置主槽 / 辅槽的控温参数
            // 所以事件 / 错误直接在 TempDevice 中处理，并不会返回到 Devices 中

            // 结果处理 - 事件
            ParamUpdatedToDeviceEvent(err);
        }


        /// <summary>
        /// 从下位机读取参数值到 tpParam[] 中；
        /// 如发生错误，则立即停止并触发参数读取事件（附带错误信息）
        /// 如读取成功，则触发参数读取事件（附带 NoError 信息）
        /// </summary>
        public void UpdateParamFromDevice()
        {
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;

            lock(tpLocker)
            {
                for (int i = 0; i < 7; i++)
                {
                    // 从下位机读取参数值
                    float val = 0.0f;
                    err = tpDevice.ReadData((TempProtocol.Cmd_t)i, out val);

                    // 调试信息
                    Debug.WriteLineIf(err == TempProtocol.Err_t.NoError, "温控设备参数读取成功!  " + tpParamNames[i] + ": " + tpParam[i].ToString());
                    Debug.WriteLineIf(err != TempProtocol.Err_t.NoError, "温控设备参数读取失败!  " + tpParamNames[i] + ": " + err.ToString());

                    // 如发生错误，则立即结束 for 循环
                    if (err != TempProtocol.Err_t.NoError)
                        break;

                    // 如果没有发生错误，则在上位机更新数据
                    tpParam[i] = val;
                }
            }

            // 说明：
            // 参数更新函数主要面向 FormSetting 窗口，分别设置主槽 / 辅槽的控温参数
            // 所以事件 / 错误直接在 TempDevice 中处理，并不会返回到 Devices 中

            // 结果处理 - 事件
            ParamUpdatedFromDeviceEvent(err);
        }


        /// <summary>
        /// 从温控设备硬件读取温度显示值，发生错误则返回上一个状态时的温度值；
        /// 错误信息处理 - 返回错误标志
        /// </summary>
        /// <param name="val">温度显示值</param>
        /// <returns>返回错误标志</returns>
        public TempProtocol.Err_t GetTemperatureShow( out float val)
        {
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;
            lock (tpLocker)
            {
                // 从下位机读取温度显示值
                err = tpDevice.ReadData(TempProtocol.Cmd_t.TempShow, out val);

                if(err == TempProtocol.Err_t.NoError)
                {
                    // 未发生错误，则加入到 temperatures 列表中
                    AddTemperature(val);
                }
                else
                {
                    // 如发生错误，则不向列表中添加新的数据，返回上一时刻的温度显示
                    if (temperatures.Count > 0)
                        val = temperatures.Last();
                    else
                        val = 0.0f;
                }
            }

            // 返回错误标志
            return err;
        }


        /// <summary>
        /// 从温控设备硬件读取功率显示值，如发生错误，则返回上一状态时的功率值；
        /// 错误信息处理 - 返回错误标志
        /// </summary>
        /// <param name="val">功率显示值</param>
        /// <returns>返回错误标志</returns>
        public TempProtocol.Err_t GetPowerShow( out float val)
        {
            TempProtocol.Err_t err = TempProtocol.Err_t.NoError;
            lock (tpLocker)
            {
                // 从下位机读取功率显示值
                err = tpDevice.ReadData(TempProtocol.Cmd_t.PowerShow, out val);

                if (err == TempProtocol.Err_t.NoError)
                {
                    // 未发生错误
                    tpPowerShow = val;
                }
                else
                {
                    // 如发生错误，则返回上一个时刻的功率显示值
                    val = tpPowerShow;
                }
            }

            // 返回错误标志
            return err;
        }


        /// <summary>
        /// 计算并获取温度波动值
        /// </summary>
        /// <param name="count">温度监测次数</param>
        /// <param name="fluctuation">温度波动值</param>
        /// <returns>返回成功与否</returns>
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


        /// <summary>
        /// 判断主槽控温设备的温度波动度是否满足条件
        /// </summary>
        /// <param name="secends">时间长度 / 秒</param>
        /// <param name="crt">波动度阈值</param>
        /// <returns></returns>
        public bool chekFluc(int secends,float crt)
        {
            float fluc = 0.0f;
            if (!GetFluc(secends * 1000 / readTempInterval, out fluc))
                return false;
            else
                return fluc < crt;
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
