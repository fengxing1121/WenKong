using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

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
            if(this.checkBox_auto.Checked == true)
            {
                Form fm = new FormAutoSet(deviceAll);
                fm.Location = new System.Drawing.Point(400,500);

                Utils.Logger.Op("点击自动控温按键，开始设置自动控温流程...");
                Utils.Logger.Sys("点击自动控温按键，开始设置自动控温流程...");

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
                            this.deviceAll.currentState = new Device.Devices.StateStruct() { flowState = Device.Devices.State.Start, stateChanged = true, stateTime = 0, stateTemp = deviceAll.temperaturePointList.First() };
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
                            this.deviceAll.currentState = new Device.Devices.StateStruct() { flowState = Device.Devices.State.Idle, stateChanged = true, stateTime = 0, stateTemp = new Device.Devices.TemperaturePoint() };
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
                        this.deviceAll.currentState = new Device.Devices.StateStruct() { flowState = Device.Devices.State.Idle, stateChanged = true, stateTime = 0, stateTemp = new Device.Devices.TemperaturePoint() };
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
                            this.deviceAll.currentState = new Device.Devices.StateStruct() { flowState = Device.Devices.State.Idle, stateChanged = true, stateTime = 0, stateTemp = new Device.Devices.TemperaturePoint() };
                        }
                    }

                    this.label_controlState.Text = "自动控温流程停止";
                    Utils.Logger.Op("已暂停了当前的自动控温流程.");
                    Utils.Logger.Sys("已暂停了当前的自动控温流程.");

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
                this.checkBox_auto.Enabled = false;
#if false
                for (int i = 1; i < this.checkBox_ryDevices.Length; i++)
                    checkBox_ryDevices[i].Enabled = true;
#endif
                // 所有继电器按键全部启用
                foreach (Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
                    checkBox_ryDevice[cmd].Enabled = true;

                this.label_controlState.Text = "手动模式开启";
                Utils.Logger.Op("开启手动模式.");
                Utils.Logger.Sys("开启手动模式.");
            }

            // 关闭手动模式
            else
            {
                this.checkBox_auto.Enabled = true;
#if false
                for (int i = 1; i < this.checkBox_ryDevices.Length; i++)
                    checkBox_ryDevices[i].Enabled = false;
#endif

                // 所有继电器按键全部禁用 - 总电源按键启用
                foreach (Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
                    checkBox_ryDevice[cmd].Enabled = false;
                checkBox_ryDevice[Device.RelayProtocol.Cmd_r.Elect].Enabled = true;

                this.label_controlState.Text = "手动模式关闭";
                Utils.Logger.Op("关闭手动模式.");
                Utils.Logger.Sys("关闭手动模式.");
            }
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


                Utils.Logger.Sys("自动控温流程运行结束，测量数据已保存到相应文件中!");

                // wghou
                // 问题来了，要是在这个时候，通信出现了问题，怎么办？？
                // 先关闭除总电源以外的所有继电器电源
                foreach (Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
                    deviceAll.ryDevice.ryStatusToSet[(int)cmd] = false;
                deviceAll.ryDevice.ryStatusToSet[(int)Device.RelayProtocol.Cmd_r.Elect] = true;
                deviceAll.ryDevice.UpdateStatusToDevice();

                // 关闭所有继电器电源
                foreach (Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
                    deviceAll.ryDevice.ryStatusToSet[(int)cmd] = false;
                deviceAll.ryDevice.UpdateStatusToDevice();

                this.BeginInvoke(new EventHandler(delegate
                {
                    MessageBox.Show("自动控温流程执行完毕！");
                }));
            }
            else if(st == Device.Devices.State.Idle || st == Device.Devices.State.Start || st == Device.Devices.State.Undefine)
            {
                // 无操作
            }
            else
            {
                // 控温流程显示
                this.BeginInvoke(new EventHandler(delegate
                {
                    // 当前状态提示
                    this.label_controlState.Text = "自动控温流程： " + deviceAll.StateName[(int)deviceAll.currentState.flowState];
                }));

                Utils.Logger.Sys("自动控温流程进入 " + deviceAll.StateName[(int)deviceAll.currentState.flowState] + " 状态.");
            }
            
        }


        /// <summary>
        /// 主槽控温报警及故障判断 - 故障发生时处理函数
        /// </summary>
        /// <param name="fCode"></param>
        private void DeviceAll_FlowControlFaultOccurEvent(Device.Devices.FaultCode fCode)
        {
            // 警告信息及处理时间
            string msg = string.Empty;
            int errTm = 600;


            if(fCode == Device.Devices.FaultCode.CodeError)
            {
                // 程序错误，应立即退出整个程序
            }
            else if(fCode == Device.Devices.FaultCode.SensorError)
            {
                // 读取电桥温度错误
            }
            else
            {
                // 设备故障报警
            }

            this.BeginInvoke(new EventHandler(delegate
            {
                bool formExit = false;
                foreach(Form fm in Application.OpenForms)
                {
                    if(fm.Name == "FormAlarm")
                    {
                        fm.WindowState = FormWindowState.Normal;

                        fm.BringToFront();
                        formExit = true;
                    }
                }

                if(!formExit)
                {
                    FormAlarm fm = new FormAlarm(msg, errTm);
                    fm.Name = "FormAlarm";
                    fm.Text = "";
                    fm.Location = new System.Drawing.Point(600, 300);
                    fm.Show();
                }

            }));
            
        }
    }
}
