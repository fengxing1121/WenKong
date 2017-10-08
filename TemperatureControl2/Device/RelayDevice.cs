using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Device
{
    public class RelayDevice
    {
        // 继电器设备
        #region Memebers
        // 设备
        public bool ryDeviceInited = false;
        public string ryDeviceName = string.Empty;
        public string ryDevicePortName = string.Empty;
        RelayProtocol ryDevice = new RelayProtocol();
        /// <summary>
        /// Relay 设备各继电器状态
        /// </summary>
        public bool[] ryStatus = { false, false, false, false, false, false, false, false, false };
        /// <summary>
        /// Relay 设备各继电器将要设置的状态，调用 SetRelayStatusAll 后将更新继电器状态
        /// </summary>
        public bool[] ryStatusToSet = { false, false, false, false, false, false, false, false, false };
        public readonly string[] ryName =
            {"总电源", "主槽控温", "辅槽控温", "辅槽制冷", "辅槽循环", "主槽快冷", "辅槽快冷", "海水进", "海水出"};
        /// <summary>
        /// 设备线程锁，同一时间只允许单一线程访问设备资源（串口 / 数据）
        /// </summary>
        private object ryLocker = new object();

        #endregion



        #region Methods
        /// <summary>
        /// 继电器设备初始化
        /// </summary>
        /// <param name="init">初始化状态，false 则表示关闭设备</param>
        public void InitRelayDevice(bool init)
        {
            RelayProtocol.Err_r err = RelayProtocol.Err_r.NoError;
            // 线程锁
            lock (ryLocker)
            {
                // 初始化设备
                if (init == true)
                {

                    ryDeviceInited = true;
                }
                // 关闭设备
                else
                {

                    ryDeviceInited = false;
                }
            }

        }

        /// <summary>
        /// 设置继电器状态
        /// </summary>
        public void UpdateStatusToDevice()
        {
            RelayProtocol.Err_r err = RelayProtocol.Err_r.NoError;
            int i = 0;
            lock(ryLocker)
            {
                for (i = 0; i < 9; i++)
                {
                    // 如果要设置的继电器状态与当前状态相同，则跳过
                    if (ryStatus[i] == ryStatusToSet[i])
                        continue;

                    // 设置继电器状态
                    err = ryDevice.SendData((RelayProtocol.Cmd_r)i, ryStatusToSet[i]);
                    // 调试信息
                    Debug.WriteLineIf(err == RelayProtocol.Err_r.NoError, "继电器 " + ryName[i] +" 状态更新成功!  " + ryStatusToSet[i].ToString());
                    Debug.WriteLineIf(err != RelayProtocol.Err_r.NoError, "继电器 " + ryName[i] + " 状态更新成功!  " + err.ToString());

                    if (err == RelayProtocol.Err_r.NoError)
                    {
                        ryStatus[i] = ryStatusToSet[i];
                    }  
                    else
                    {
                        break;
                    }
                }
            }

            // 如果在设置继电器状态过程中发生了错误，则触发设备错误事件
            if(err != RelayProtocol.Err_r.NoError)
            {
                // wghou
                // 设置出现错误
            }
            else
            {
                // wghou
                // 设置完成
            }
        }

        #endregion
    }
}
