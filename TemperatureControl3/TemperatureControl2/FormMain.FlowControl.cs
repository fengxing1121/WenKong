using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TemperatureControl2
{
    /// <summary>
    /// 系统流程控制相关
    /// </summary>
    public partial class FormMain
    {
        /// <summary>
        /// 控制流程按键 - 自动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_auto_Click(object sender, EventArgs e)
        {
            bool fmExit = false;
            foreach(Form fm in Application.OpenForms)
            {
                if (fm.Name == "FormAutoSet")
                {
                    fm.WindowState = FormWindowState.Normal;
                    fm.BringToFront();
                    fmExit = true;
                }
            }

            if(!fmExit)
            {
                FormAutoSet fm = new FormAutoSet(deviceAll);
                fm.Name = "FormAutoSet";
                fm.Show();
            }

            return;

            // wghou 20180105
            // 该段代码应删除
            if(this.checkBox_auto.Checked == true)
            {
                Form fm = new FormAutoSet(deviceAll);
                //fm.Location = new System.Drawing.Point(400,500);

                Utils.Logger.Op("点击自动控温按键，开始设置自动控温流程...");
                Utils.Logger.Sys("点击自动控温按键，开始设置自动控温流程...");
                //Utils.Logger.TempData("开始自动控温流程");

                DialogResult dr = fm.ShowDialog();
                if(dr == DialogResult.OK)
                {
                    // 设置好了实验流程，开始进行实验
                    // 先检查流程格式是否正确
                    if(deviceAll.temperaturePointList.Count !=0)
                    {
                        // controlFlowList 不为空，且第一项为 TempUp 或者 TempDown
                        this.checkBox_man.Enabled = false;
                        lock(this.deviceAll.stepLocker)
                        {
                            // 设置初始运行状态
                            this.deviceAll.currentState = new Device.Devices.StateStruct() { flowState = Device.Devices.State.Start, stateChanged = true, stateCounts = 0, tempPoint = deviceAll.temperaturePointList.First() };
                            // 开始运行自动测温
                            deviceAll.autoStart = true;
                        }

                        // 当前状态提示
                        this.label_controlState.Text = "自动控温流程启动";

                    }
                    else
                    {
                        // 实验流程格式不正确
                        this.checkBox_auto.Checked = false;
                        lock(this.deviceAll.stepLocker)
                        {
                            // 停止系统运行
                            deviceAll.autoStart = false;
                            // 设置运行状态为空闲
                            this.deviceAll.currentState = new Device.Devices.StateStruct() { flowState = Device.Devices.State.Idle, stateChanged = true, stateCounts = 0, tempPoint = new Device.Devices.TemperaturePoint() };
                        }
                        MessageBox.Show("实验流程格式不正确，请重新设置!");
                    }
                }
                else
                {
                    // 取消了实验流程设置，不开始实验操作
                    this.checkBox_auto.Checked = false;
                    lock(this.deviceAll.stepLocker)
                    {
                        // 停止运行自动测温
                        deviceAll.autoStart = false;
                        // 设置运行状态为空闲
                        this.deviceAll.currentState = new Device.Devices.StateStruct() { flowState = Device.Devices.State.Idle, stateChanged = true, stateCounts = 0, tempPoint = new Device.Devices.TemperaturePoint() };
                    }

                    Utils.Logger.Op("取消了自动控温流程设置...");
                    Utils.Logger.Sys("取消了自动控温流程设置...");
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("确定要停止运行？", "推出系统",MessageBoxButtons.OKCancel);
                if(dr == DialogResult.OK)
                {
                    Debug.WriteLine("已暂停了当前的自动控温流程!");
                    this.checkBox_man.Enabled = true;

                    // 保证流程单步已经执行完成或还没有开始
                    lock(deviceAll.stepLocker)
                    {
                        deviceAll.autoStart = false;
                        // 如果当前流程不为空闲，则将其装回到 Devices.controlFlowList 中
                        if (this.deviceAll.currentState.flowState != Device.Devices.State.Idle)
                        {
                            // 停止运行自动测温
                            this.deviceAll.autoStart = false;
                            // 设置运行状态为空闲
                            this.deviceAll.currentState = new Device.Devices.StateStruct() { flowState = Device.Devices.State.Idle, stateChanged = true, stateCounts = 0, tempPoint = new Device.Devices.TemperaturePoint() };
                        }
                    }

                    this.label_controlState.Text = "自动控温流程停止";
                    Utils.Logger.Op("已暂停了当前的自动控温流程.");
                    Utils.Logger.Sys("已暂停了当前的自动控温流程.");
                    //Utils.Logger.TempData("已暂停了当前的自动控温流程");

                }
                else
                {
                    Debug.WriteLine("取消");
                    checkBox_auto.Checked = true;
                    deviceAll.autoStart = true;
                }
            }
            
        }

        private void checkBox_man_Click(object sender, EventArgs e)
        {
            // 开启手动模式
            if (this.checkBox_man.Checked == true)
            {
                //this.checkBox_auto.Enabled = false;
#if false
                for (int i = 1; i < this.checkBox_ryDevices.Length; i++)
                    checkBox_ryDevices[i].Enabled = true;
#endif
                // 所有继电器按键全部启用
                foreach (Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
                    checkBox_ryDevice[cmd].Enabled = true;

                //this.label_controlState.Text = "手动模式开启";
                Utils.Logger.Op("开启手动模式.");
                Utils.Logger.Sys("开启手动模式.");
            }

            // 关闭手动模式
            else
            {
                //this.checkBox_auto.Enabled = true;
#if false
                for (int i = 1; i < this.checkBox_ryDevices.Length; i++)
                    checkBox_ryDevices[i].Enabled = false;
#endif

                // 所有继电器按键全部禁用 - 总电源按键启用
                foreach (Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
                    checkBox_ryDevice[cmd].Enabled = false;
                checkBox_ryDevice[Device.RelayProtocol.Cmd_r.Elect].Enabled = true;

                //this.label_controlState.Text = "手动模式关闭";
                Utils.Logger.Op("关闭手动模式.");
                Utils.Logger.Sys("关闭手动模式.");
            }

            // 设置主电源的禁用状态
            this.checkBox_elect.Enabled = this.deviceAll.ryElecEnable;
        }



        /// <summary>
        /// 自动控制状态下，进入流程中新的一个状态时的事件处理函数
        /// </summary>
        /// <param name="st"></param>
        private void DeviceAll_FlowControlStateChangedEvent(Device.Devices.State st)
        {
            if(st == Device.Devices.State.Finish)
            {
                // 所有温度点均已经测量完成


                //Utils.Logger.Sys("自动控温流程运行结束，测量数据已保存到相应文件中!");

                // wghou
                // 问题来了，要是在这个时候，通信出现了问题，怎么办？？
                // 先关闭除总电源以外的所有继电器电源
                Utils.Logger.Sys("测量完成，关闭除总电源外所有继电器！");
                foreach (Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
                    deviceAll.ryDevice.ryStatusToSet[(int)cmd] = false;
                deviceAll.ryDevice.ryStatusToSet[(int)Device.RelayProtocol.Cmd_r.Elect] = true;
                deviceAll.ryDevice.UpdateStatusToDevice();

                // 所有温度点全部测量完成
                // 是否关闭计算机
                if(deviceAll.shutDownComputer)
                {
                    // 用户已设定，实验完成后关闭计算机，故，弹出对话框，十分中没有操作，就关闭计算机
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        FormFinish fm = new FormFinish();
                        DialogResult rtl = fm.ShowDialog();
                        if (rtl == DialogResult.OK)
                        {
                            Utils.Logger.Sys("十分钟用户未操作 / 选择关机，关闭总电源并关闭计算机！");
                            // 关闭所有继电器电源
                            foreach (Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
                                deviceAll.ryDevice.ryStatusToSet[(int)cmd] = false;
                            deviceAll.ryDevice.UpdateStatusToDevice();
                            // 60秒后关闭计算机
                            System.Diagnostics.Process.Start("shutdown.exe", "-s -t 60");
                            this.Close();
                        }
                        else
                        {
                            // 用户选择取消关机
                            Utils.Logger.Sys("用户取消了关机操作，软件继续运行。");
                        }
                    }));
                }
                else
                {
                    // 用户已设定，实验完成后不关闭计算机，故，只把所有电源关闭了
                }
                
            }
            else if( st == Device.Devices.State.Start || st == Device.Devices.State.Undefine)
            {
                // 无操作
            }
            else
            {
                // 控温流程显示
                this.BeginInvoke(new EventHandler(delegate
                {
                    // 当前状态提示
                    this.label_controlState.Text = "自动控温流程： " + deviceAll.StateName[(int)st];
                }));

                //Utils.Logger.Sys("自动控温流程进入 " + deviceAll.StateName[(int)deviceAll.currentState.flowState] + " 状态.");
            }
            
        }


        /// <summary>
        /// 主槽控温报警及故障判断 - 故障发生时处理函数
        /// </summary>
        /// <param name="fCode"></param>
        private void DeviceAll_FlowControlFaultOccurEvent(Device.Devices.FaultCode fCode)
        {
            
            if(fCode == Device.Devices.FaultCode.CodeError)
            {
                // 程序错误，应立即退出整个程序
                //Utils.Logger.Sys("程序代码出现了错误，应立即退出程序!");
            }
            else if(fCode == Device.Devices.FaultCode.SensorError)
            {
                // 读取电桥温度错误
                //Utils.Logger.Sys("电桥温度读取出现错误！");
            }
            else
            {
                // 设备故障报警
                //Utils.Logger.Sys("设备故障报警！");
            }

            // 警告信息及处理时间
            int errTm = 600;
            this.BeginInvoke(new EventHandler(delegate
            {
                FormAlarm fmA = null;
                bool formExit = false;
                foreach(Form fm in Application.OpenForms)
                {
                    if(fm.Name == "FormAlarm")
                    {
                        fmA = (FormAlarm)fm;
                        formExit = true;
                    }
                }

                if(!formExit)
                {
                    fmA = new FormAlarm(errTm);
                    fmA.Name = "FormAlarm";
                    //fmA.Location = new System.Drawing.Point(600, 300);
                    fmA.shutdownSystem += FmA_shutdownSystem;
                }

                // 只是确保一下，应该没什么作用
                if (fmA == null)
                    return;

                if(fmA.errCount.ContainsKey(fCode))
                {
                    fmA.errCount[fCode] = fmA.errCount[fCode] + 1;
                }
                else
                {
                    Utils.Logger.Sys("自动控温流程出现故障，故障代码： " + fCode.ToString());
                    fmA.errCount[fCode] = 1;
                }

                // wghou
                // 出现错误时，如果是首次出现，则新建 Alarm 窗口并弹出，但如果窗口已经存在，则只闪烁任务栏提示
                if(formExit)
                {
                    IntPtr hwnd = FindWindow(null, "FormAlarm");
                    if (hwnd != null)
                        FlashWindowEx(hwnd, flashType.FLASHW_TIMER);
                }
                else
                {
                    fmA.Show();
                }

            }));
            
        }

        private void FmA_shutdownSystem(object sender, EventArgs e)
        {
            this.BeginInvoke(new EventHandler(delegate
            {
                Utils.Logger.Sys("出现错误，用户未做处理，关闭系统软件！");
                // 60秒后关闭计算机
                System.Diagnostics.Process.Start("shutdown.exe", "-s -t 60");
                this.Close();
            }));
        }

        #region 实现窗口闪烁

        [DllImport("user32.dll")]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        /// <summary>
        /// 闪烁类型
        /// </summary>
        public enum flashType : uint
        {
            FLASHW_STOP = 0,    //停止闪烁
            FALSHW_CAPTION = 1, //只闪烁标题
            FLASHW_TRAY = 2,    //只闪烁任务栏
            FLASHW_ALL = 3,     //标题和任务栏同时闪烁
            FLASHW_PARAM1 = 4,
            FLASHW_PARAM2 = 12,
            FLASHW_TIMER = FLASHW_TRAY | FLASHW_PARAM1,     //无条件闪烁任务栏直到发送停止标志或者窗口被激活，如果未激活，停止时高亮
            FLASHW_TIMERNOFG = FLASHW_TRAY | FLASHW_PARAM2  //未激活时闪烁任务栏直到发送停止标志或者窗体被激活，停止后高亮

            // https://msdn.microsoft.com/en-us/library/ms679348
            //FLASHW_ALL = 0x00000003,        // Flash both the window caption and taskbar button. This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
            //FLASHW_CAPTION = 0x00000001,    // Flash the window caption.
            //FLASHW_STOP = 0,                // Stop flashing. The system restores the window to its original state.
            //FLASHW_TIMER = 0x00000004,      // Flash continuously, until the FLASHW_STOP flag is set.
            //FLASHW_TIMERNOFG = 0x0000000C,  // Flash continuously until the window comes to the foreground.
            //FLASHW_TRAY =0x00000002,        // Flash the taskbar button.
        }

        /// <summary>
        /// 包含系统应在指定时间内闪烁窗口次数和闪烁状态的信息
        /// </summary>
        public struct FLASHWINFO
        {
            /// <summary>
            /// 结构大小
            /// </summary>
            public uint cbSize;
            /// <summary>
            /// 要闪烁或停止的窗口句柄
            /// </summary>
            public IntPtr hwnd;
            /// <summary>
            /// 闪烁的类型
            /// </summary>
            public uint dwFlags;
            /// <summary>
            /// 闪烁窗口的次数
            /// </summary>
            public uint uCount;
            /// <summary>
            /// 窗口闪烁的频度，毫秒为单位；若该值为0，则为默认图标的闪烁频度
            /// </summary>
            public uint dwTimeout;
        }

        /// <summary>
        /// 闪烁窗口
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="type">闪烁类型</param>
        /// <returns></returns>
        public static bool FlashWindowEx(IntPtr hWnd, flashType type)
        {
            FLASHWINFO fInfo = new FLASHWINFO();
            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            fInfo.hwnd = hWnd;//要闪烁的窗口的句柄，该窗口可以是打开的或最小化的
            fInfo.dwFlags = (uint)type;//闪烁的类型
            //fInfo.uCount = UInt32.MaxValue;//闪烁窗口的次数
            fInfo.uCount = 5;//闪烁窗口的次数
            fInfo.dwTimeout = 0; //窗口闪烁的频度，毫秒为单位；若该值为0，则为默认图标的闪烁频度
            return FlashWindowEx(ref fInfo);
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        #endregion

    }
}
