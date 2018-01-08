using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Device
{
    public class RelayDevice
    {
        // 继电器设备
        #region Memebers
        // 设备
        public string ryDeviceName = string.Empty;
        public string ryDevicePortName = string.Empty;
        RelayProtocol ryDeviceProtocol = new RelayProtocol();
        /// <summary>
        /// Relay 设备各继电器状态
        /// </summary>
        public bool[] ryStatus = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        /// <summary>
        /// Relay 设备各继电器将要设置的状态，调用 SetRelayStatusAll 后将更新继电器状态
        /// </summary>
        public bool[] ryStatusToSet = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        /// <summary>
        /// 设备线程锁，同一时间只允许单一线程访问设备资源（串口 / 数据）
        /// </summary>
        private object ryLocker = new object();
        #endregion

        #region Event
        public delegate void StatusUpdateEventHandler(RelayProtocol.Err_r err);
        /// <summary>
        /// 向下位机写入继电器状态完成事件
        /// </summary>
        public event StatusUpdateEventHandler StatusUpdateToDeviceEvent;
        #endregion


        #region Methods
        /// <summary>
        /// 继电器设备初始化
        /// </summary>
        /// <param name="init">初始化状态，false 则表示关闭设备</param>
        public bool SetDevicePortName(string portName)
        {
            // 线程锁
            lock (ryLocker)
            {
                // 设置设备串口
                bool err = ryDeviceProtocol.SetPort(portName);
                // 如果设置成功，则保存端口名称
                if(err)
                    ryDevicePortName = portName;
                return err;
            }

        }


        /// <summary>
        /// 继电器设备逐个自检
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public RelayProtocol.Err_r SelfCheckOneByOne(RelayProtocol.Cmd_r cmd, bool status)
        {
            RelayProtocol.Err_r err = RelayProtocol.Err_r.NoError;
            err = ryDeviceProtocol.WriteRelayStatus(cmd, status);
            return err;
        }


        /// <summary>
        /// 继电器设备自检
        /// </summary>
        /// <returns></returns>
        public RelayProtocol.Err_r SelfCheck()
        {
            RelayProtocol.Err_r err = RelayProtocol.Err_r.NoError;
            /////////////////////////////////////////////////////////////////////////
            // 打开总电源开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.Elect, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 打开主槽控温开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.MainHeat, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 打开辅槽控温开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.SubHeat, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 打开辅槽制冷源开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.SubCool, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 打开辅槽循环开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.SubCircle, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 打开主槽快冷开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.MainCoolF, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 打开辅槽快冷开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.SubCoolF, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 打开海水进开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.WaterIn, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 打开海水出开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.WaterOut, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);

            /////////////////////////////////////////////////////////////////////////
            // 关闭海水出开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.WaterOut, false);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 关闭海水进开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.WaterIn, false);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 关闭辅槽快冷开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.SubCoolF, false);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 关闭主槽快冷开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.MainCoolF, false);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 关闭辅槽循环开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.SubCircle, false);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 关闭辅槽制冷源开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.SubCool, false);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 关闭辅槽控温开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.SubHeat, false);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 关闭主槽控温开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.MainHeat, false);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);
            // 关闭总电源开关
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.Elect, false);
            if (err != RelayProtocol.Err_r.NoError)
                return err;
            Thread.Sleep(1000);

            //////////////////////////
            // 最后，打开总电源
            err = ryDeviceProtocol.WriteRelayStatus(RelayProtocol.Cmd_r.Elect, true);
            if (err != RelayProtocol.Err_r.NoError)
                return err;

            return RelayProtocol.Err_r.NoError;
        }


        /// <summary>
        /// 将 ryStatusToSet[] 中的继电器状态更新到设备硬件中；
        /// 如果发生了错误，则触发设备错误事件（未实现）
        /// </summary>
        public void UpdateStatusToDevice()
        {
            RelayProtocol.Err_r err = RelayProtocol.Err_r.NoError;
            lock(ryLocker)
            {
                // 遍历枚举类型 RelayProtocol.Cmd_r 中所有的值
                foreach(RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(RelayProtocol.Cmd_r)))
                {
                    // 如果要设置的继电器状态与当前状态相同，则跳过
                    if (ryStatus[(int)cmd] == ryStatusToSet[(int)cmd])
                        continue;

                    err = ryDeviceProtocol.WriteRelayStatus(cmd, ryStatusToSet[(int)cmd]);
                    // 暂停一段时间
                    Thread.Sleep(20);
                    // 调试信息
                    Debug.WriteLineIf(err == RelayProtocol.Err_r.NoError, "继电器 " + cmd.ToString() + " 状态更新成功!  " + ryStatusToSet[(int)cmd].ToString());
                    Debug.WriteLineIf(err != RelayProtocol.Err_r.NoError, "继电器 " + cmd.ToString() + " 状态更新失败!  " + err.ToString());

                    if (err == RelayProtocol.Err_r.NoError)
                    {
                        // 设置继电器状态成功，更新 ryStatus[] 中的值
                        ryStatus[(int)cmd] = ryStatusToSet[(int)cmd];
                    }
                    else
                    {
                        // 设置继电器状态失败，结束 for 循环
                        break;
                    }
                }

                // 改进
#if false
                for (int i = 0; i < 9; i++)
                {
                    // 如果要设置的继电器状态与当前状态相同，则跳过
                    if (ryStatus[i] == ryStatusToSet[i])
                        continue;

                    // 设置继电器状态
                    
                    err = ryDeviceProtocol.WriteRelayStatus((RelayProtocol.Cmd_r)i, ryStatusToSet[i]);
                    // 调试信息
                    Debug.WriteLineIf(err == RelayProtocol.Err_r.NoError, "继电器 " + ryName[i] + " 状态更新成功!  " + ryStatusToSet[i].ToString());
                    Debug.WriteLineIf(err != RelayProtocol.Err_r.NoError, "继电器 " + ryName[i] + " 状态更新失败!  " + err.ToString());

                    if (err == RelayProtocol.Err_r.NoError)
                    {
                        // 设置继电器状态成功，更新 ryStatus[] 中的值
                        ryStatus[i] = ryStatusToSet[i];
                    }  
                    else
                    {
                        // 设置继电器状态失败，结束 for 循环
                        break;
                    }
                }
#endif

            }


            // 触发设备错误事件，并返回设置错误信息
            StatusUpdateToDeviceEvent(err);
        }


        internal RelayProtocol.Err_r UpdateStatusToDeviceReturnErr()
        {
            RelayProtocol.Err_r err = RelayProtocol.Err_r.NoError;
            lock (ryLocker)
            {
                // 遍历枚举类型 RelayProtocol.Cmd_r 中所有的值
                foreach (RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(RelayProtocol.Cmd_r)))
                {
                    // 如果要设置的继电器状态与当前状态相同，则跳过
                    if (ryStatus[(int)cmd] == ryStatusToSet[(int)cmd])
                        continue;

                    err = ryDeviceProtocol.WriteRelayStatus(cmd, ryStatusToSet[(int)cmd]);
                    // 暂停一段时间
                    Thread.Sleep(20);
                    // 调试信息
                    Debug.WriteLineIf(err == RelayProtocol.Err_r.NoError, "继电器 " + cmd.ToString() + " 状态更新成功!  " + ryStatusToSet[(int)cmd].ToString());
                    Debug.WriteLineIf(err != RelayProtocol.Err_r.NoError, "继电器 " + cmd.ToString() + " 状态更新失败!  " + err.ToString());

                    if (err == RelayProtocol.Err_r.NoError)
                    {
                        // 设置继电器状态成功，更新 ryStatus[] 中的值
                        ryStatus[(int)cmd] = ryStatusToSet[(int)cmd];
                    }
                    else
                    {
                        // 设置继电器状态失败，结束 for 循环
                        break;
                    }
                }
            }


            // 触发设备错误事件，并返回设置错误信息
            StatusUpdateToDeviceEvent(err);

            return err;
        }

        #endregion
    }
}
