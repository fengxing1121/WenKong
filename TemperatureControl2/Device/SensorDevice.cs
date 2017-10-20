using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Device
{
    public class SensorDevice
    {

        /// <summary>
        /// 初始化传感器设备
        /// </summary>
        /// <returns></returns>
        public bool InitDevice()
        {

            return true;
        }


        /// <summary>
        /// 获取传感器的值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool GetSensorValue(out float val)
        {
            val = 0.0f;
            return true;
        }
    }
}
