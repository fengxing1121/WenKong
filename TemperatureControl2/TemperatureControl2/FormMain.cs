using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TemperatureControl2
{
    public partial class FormMain : Form
    {
        // wghou
        //private CheckBox[] checkBox_ryDevices = new CheckBox[9];
        private Dictionary<Device.RelayProtocol.Cmd_r, CheckBox> checkBox_ryDevice = new Dictionary<Device.RelayProtocol.Cmd_r, CheckBox>();
        Device.Devices deviceAll = new Device.Devices();
        
        public FormMain()
        {
            // 初始化
            InitializeComponent();

            // 继电器设备编号
            checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.Elect, checkBox_elect);
            checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.MainHeat, checkBox_mainHeat);
            checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.SubHeat, checkBox_subHeat);
            checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.SubCool, checkBox_subCool);
            checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.SubCircle, checkBox_subCircle);
            checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.MainCoolF, checkBox_mainCoolF);
            checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.SubCoolF, checkBox_subCoolF);
            checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.WaterIn, checkBox_waterIn);
            checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.WaterOut, checkBox_waterOut);
#if false
            checkBox_ryDevices[0] = checkBox_elect;
            checkBox_ryDevices[1] = checkBox_mainHeat;
            checkBox_ryDevices[2] = checkBox_subHeat;
            checkBox_ryDevices[3] = checkBox_subCool;
            checkBox_ryDevices[4] = checkBox_subCircle;
            checkBox_ryDevices[5] = checkBox_mainCoolF;
            checkBox_ryDevices[6] = checkBox_subCoolF;
            checkBox_ryDevices[7] = checkBox_waterIn;
            checkBox_ryDevices[8] = checkBox_waterOut;
#endif

            // 温度数据读取 - 定时器事件
            deviceAll.TpTemperatureUpdateTimerEvent += tpDevice_TpTemperatureUpdateTimerEvent;
            // 继电器状态设置 - 状态更新事件
            deviceAll.ryDevice.StatusUpdateToDeviceEvent += RyDev_StatusUpdateEvent;
            // 主槽 / 辅槽控温设备参数更新事件
            deviceAll.tpDeviceM.ParamUpdatedToDeviceEvent += TpDeviceM_ParamUpdatedToDeviceEvent;
            deviceAll.tpDeviceM.ParamUpdatedFromDeviceEvent += TpDeviceM_ParamUpdatedToDeviceEvent;
            deviceAll.tpDeviceS.ParamUpdatedFromDeviceEvent += TpDeviceM_ParamUpdatedToDeviceEvent;
            deviceAll.tpDeviceS.ParamUpdatedToDeviceEvent += TpDeviceM_ParamUpdatedToDeviceEvent;
            deviceAll.FlowControlStateChangedEvent += DeviceAll_FlowControlStateChangedEvent;
        }


        /// <summary>
        /// 主窗体载入时，进行设备的初始化及自检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("温控系统开始运行...");

            // 给出配置文件的路径
            Debug.WriteLine("开始配置设备端口...");
            if(!deviceAll.Configure(@"./config.ini"))
            {
                MessageBox.Show("设备串口配置错误，请检查串口设置及连接，并重新运行程序！");
                this.Close();
                return;
            }

#if DEBUG
#else
            // 设备开始自检
            Debug.WriteLine("开始设备自检...");
            if(!deviceAll.DeviceSelfCheck())
            {
                MessageBox.Show("设备自检错误，请检查硬件连接，并重新运行程序！");
                this.Close();
                return;
            }

            // 更新主界面显示数值
            // 功率系数
            label_powerM.Text = this.deviceAll.tpDeviceM.tpPowerShow.ToString("00");
            label_powerS.Text = this.deviceAll.tpDeviceS.tpPowerShow.ToString("00");
            // 温度显示值
            label_tempM.Text = this.deviceAll.tpDeviceM.temperatures.Last().ToString("0.000");
            label_tempS.Text = this.deviceAll.tpDeviceS.temperatures.Last().ToString("0.000");
            // 温度设定值
            label_tempSetM.Text = this.deviceAll.tpDeviceM.tpParam[0].ToString("0.000");
            label_tempSetS.Text = this.deviceAll.tpDeviceS.tpParam[0].ToString("0.000");
#endif

// 禁用继电器按键
#if false
            for (int i = 1; i < checkBox_ryDevices.Length; i++)
            {
                this.checkBox_ryDevices[i].Enabled = false;
            }
#endif
            // 禁用所有继电器按键
            foreach(Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
            {
                this.checkBox_ryDevice[cmd].Enabled = false;
            }
            this.checkBox_ryDevice[Device.RelayProtocol.Cmd_r.Elect].Enabled = true;

            // 主槽控温表 / 辅槽控温表 开始读取参数
            deviceAll.tpTemperatureUpdateTimer.Start();
            deviceAll.tpTemperatureUpdateTimer.Interval = deviceAll.tpDeviceM.readTempInterval;
        }

    }
}
