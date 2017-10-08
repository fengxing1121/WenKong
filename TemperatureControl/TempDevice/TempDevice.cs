using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Device
{
    public class TempDevice
    {
        #region Parameters
        /// <summary>
        /// List of all parameters
        /// </summary>
        public enum Paras_t
        {
            TempSet = 0,
            TempCorrect,
            LeadAdjust,
            Fuzzy,
            Ratio,
            Integral,
            Power,
            FlucThr,
            TempThr
        }
        #endregion

        #region Protocol
        private TempProtocol tpProtocol = new TempProtocol();
        #endregion

        #region Device members
        // Timer
        private Timer tick = new Timer();
        private DateTime ctrlStartTime;
        private int readTempInterval = 1000;

        // 线程安全
        private object locker = new object();

        // Device parameters
        public float[] paraValues = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.1f };
        public string[] paraFormat = { "0.000", "0.000", "0.000", "0", "0", "0", "0", "0.000", "0.000" };
        public string[] paraChNames =
            { "设定值    ", "调整值    ", "超前调整值", "模糊系数  ", "比例系数  ", "积分系数  ", "功率系数  ", "波动度阈值", "温度阈值  " };
        private const int tempMaxLen = 1000;
        private List<float> temperatures = new List<float>();
        private float powerShow = 0.0f;
        private bool firstReadPara = true;

        // Device Name
        private string deviceName = string.Empty;
        private string portName = string.Empty;

        #endregion

        #region Public Methods

        public bool setParam(Paras_t param, float value)
        {
            Err_t err = Err_t.NoError;
            lock (locker)
            {
                if ((int)param < 7)
                {
                    err = tpProtocol.SendData((TempProtocol.Cmd_t)param, value);
                    if(err == Err_t.NoError)
                        paraValues[(int)param] = value;
                }
                else
                {
                    paraValues[(int)param] = value;
                }
            }

            return (err == Err_t.NoError);
        }

        public bool getParam(Paras_t param, out float value)
        {
            Err_t err = Err_t.NoError;
            lock (locker)
            {
                if ((int)param < 7)
                {
                    err = tpProtocol.ReadData((TempProtocol.Cmd_t)param, out value);
                    if (err == Err_t.NoError)
                        paraValues[(int)param] = value;
                }
                else
                {
                    value = paraValues[(int)param];
                }
            }

            return (err == Err_t.NoError);
        }

        public bool getTemperatureShow(out float temp)
        {
            Err_t err = Err_t.NoError;
            lock (locker)
            {
                err = tpProtocol.ReadData(TempProtocol.Cmd_t.TempShow, out temp);
                if (err == Err_t.NoError)
                    AddTemperature(temp);
            }

            return (err == Err_t.NoError);
        }

        public bool getPowerShow(out float power)
        {
            Err_t err = Err_t.NoError;
            lock (locker)
            {
                err = tpProtocol.ReadData(TempProtocol.Cmd_t.PowerShow, out power);
                if (err == Err_t.NoError)
                    powerShow = power;
            }

            return (err == Err_t.NoError);
        }


        /// <summary>
        /// 温度波动度
        /// </summary>
        /// <param name="count"></param>
        /// <param name="fluctuation"></param>
        /// <returns></returns>
        public bool getFluc(int count, out float fluctuation)
        {
            if(temperatures.Count == 0 || temperatures.Count < count)
            {
                //
                fluctuation = -1;
                return false;
            }
            else
            {
                // 获取温度波动度
                fluctuation = temperatures.GetRange(temperatures.Count - count, count).Max() -
                    temperatures.GetRange(temperatures.Count - count, count).Min();
                return true;
            }
        }
        #endregion

        /// <summary>
        /// 缓存温度值
        /// </summary>
        /// <param name="data"></param>
        private void AddTemperature(float data)
        {
            if (temperatures.Count == tempMaxLen)
            {
                temperatures.RemoveAt(0);
            }
            temperatures.Add(data);
        }
        
    }
}
