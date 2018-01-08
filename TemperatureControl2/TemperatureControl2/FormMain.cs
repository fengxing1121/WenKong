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
        // 闪烁等
        Bitmap mBmp;
        private bool flp = false;
        private Timer timPic = new Timer();
        
        public FormMain()
        {
            // 初始化
            InitializeComponent();

            // 继电器设备编号
            try
            {
                checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.Elect, checkBox_elect);
                checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.MainHeat, checkBox_mainHeat);
                checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.SubHeat, checkBox_subHeat);
                checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.SubCool, checkBox_subCool);
                checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.SubCircle, checkBox_subCircle);
                checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.MainCoolF, checkBox_mainCoolF);
                checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.SubCoolF, checkBox_subCoolF);
                checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.WaterIn, checkBox_waterIn);
                checkBox_ryDevice.Add(Device.RelayProtocol.Cmd_r.WaterOut, checkBox_waterOut);
            }
            catch { }
        }


        /// <summary>
        /// 主窗体载入时，进行设备的初始化及自检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {

            Utils.Logger.Op("系统启动，开始自检...");
            Utils.Logger.Sys("系统启动，开始自检...");

            Debug.WriteLine("温控系统开始运行...");

            // 给出配置文件的路径
            Debug.WriteLine("开始配置设备端口...");
            if(!deviceAll.Configure(@"./config.ini"))
            {
                Utils.Logger.Sys("设备串口配置错误，请检查串口设置及连接，系统退出!");
                MessageBox.Show("设备串口配置错误，请检查串口设置及连接，并重新运行程序！");
                this.Close();
                return;
            }
            Utils.Logger.Sys("设备端口配置成功!");

            // 设备开始自检
            //Utils.Logger.Sys("设备开始自检...");
            //Debug.WriteLine("开始设备自检...");
            //if (!deviceAll.DeviceSelfCheck())
            //{
            //    Utils.Logger.Sys("设备自检错误，请检查硬件连接，系统退出!");
            //    MessageBox.Show("设备自检错误，请检查硬件连接，并重新运行程序！");
            //    this.Close();
            //    return;
            //}

            FormSelfCheck fm = new FormSelfCheck(deviceAll);
            DialogResult rt = fm.ShowDialog();
            if (rt != DialogResult.OK)
            {
                //MessageBox.Show("设备自检错误！");
                Debug.WriteLine("自检错误！");
                this.Close();
                return;
            }
                

            Utils.Logger.Sys("设备自检成功，系统开始运行...");
            Utils.Logger.Op("设备自检成功，系统开始运行...");
            //Utils.Logger.TempData("系统开始运行...");


            // 初始化主界面中的显示相
            InitMainFormShow();

            // 注册事件处理函数
            RegisterEventHandler();

            // 主槽控温表 / 辅槽控温表 开始读取参数
            // wghou 20180105
            // 暂时先不开始读取温度，等待自检
            deviceAll.tpTemperatureUpdateTimer.Start();

            // 闪烁灯
            mBmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            timPic.Interval = 500;
            timPic.Tick += TimPic_Tick;
            timPic.Start();

        }


        // 指示灯闪烁
        private void TimPic_Tick(object sender, EventArgs e)
        {
            Graphics mGhp = Graphics.FromImage(mBmp);
            mGhp.Clear(SystemColors.Control);
            if(flp)
            {
                mGhp.Clear(SystemColors.Control);
                flp = false;
            }
            else
            {
                mGhp.Clear(Color.Green);
                flp = true;
            }

            pictureBox1.Image = mBmp;
            pictureBox2.Image = mBmp;
        }



        /// <summary>
        /// 初始化主界面中的显示项
        /// </summary>
        void InitMainFormShow()
        {
            // 更新主界面显示数值
            // 功率系数
            label_powerM.Text = this.deviceAll.tpDeviceM.tpPowerShow.ToString("0") + "%";
            label_powerS.Text = this.deviceAll.tpDeviceS.tpPowerShow.ToString("0") + "%";
            // 温度显示值
            if (this.deviceAll.tpDeviceM.temperatures.Count != 0)
                label_tempM.Text = this.deviceAll.tpDeviceM.temperatures.Last().ToString("0.0000") + "℃";
            if (this.deviceAll.tpDeviceS.temperatures.Count != 0)
                label_tempS.Text = this.deviceAll.tpDeviceS.temperatures.Last().ToString("0.0000") + "℃";
            // 温度设定值
            label_tempSetM.Text = this.deviceAll.tpDeviceM.tpParam[0].ToString("0.0000") + "℃";
            label_tempSetS.Text = this.deviceAll.tpDeviceS.tpParam[0].ToString("0.0000") + "℃";


            // 禁用所有继电器按键 
            // 并更新继电器状态
            foreach (Device.RelayProtocol.Cmd_r cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
            {
                this.checkBox_ryDevice[cmd].Enabled = false;
                this.checkBox_ryDevice[cmd].Checked = deviceAll.ryDevice.ryStatus[(int)cmd];
            }
            // 启用总电源开关
            this.checkBox_elect.Enabled = true;
            // 打开总电源开关
            this.checkBox_elect.Checked = true;
            deviceAll.ryDevice.ryStatusToSet[(int)Device.RelayProtocol.Cmd_r.Elect] = this.checkBox_elect.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);

        }


        /// <summary>
        /// 注册事件处理函数
        /// </summary>
        void RegisterEventHandler()
        {
            // 温度数据读取 - 定时器事件 - 将温度值从 Devices.tpDeviceM.temperatures.Last 更新到主界面
            deviceAll.TpTemperatureUpdateTimerEvent += tpDevice_TpTemperatureUpdateTimerEvent;


            // 继电器状态设置 - 状态更新事件 - 发生错误则弹出提示对话框
            deviceAll.ryDevice.StatusUpdateToDeviceEvent += RyDev_StatusUpdateEvent;


            // 主槽 / 辅槽控温设备参数更新事件
            // 当主槽 / 辅槽控温设备的参数发生变化时（更新或者写入）时，把相应参数更新到主界面
            deviceAll.tpDeviceM.ParamUpdatedToDeviceEvent += TpDeviceM_ParamUpdatedToDeviceEvent;
            deviceAll.tpDeviceM.ParamUpdatedFromDeviceEvent += TpDeviceM_ParamUpdatedToDeviceEvent;
            deviceAll.tpDeviceS.ParamUpdatedFromDeviceEvent += TpDeviceM_ParamUpdatedToDeviceEvent;
            deviceAll.tpDeviceS.ParamUpdatedToDeviceEvent += TpDeviceM_ParamUpdatedToDeviceEvent;


            // 自动控制流程时，当进入新的一个状态时，通知主界面，进行相应的显示
            deviceAll.FlowControlStateChangedEvent += DeviceAll_FlowControlStateChangedEvent;


            // 主槽报警及故障判断 - 弹出错误提示界面 - 未及时处理则关机
            deviceAll.FlowControlFaultOccurEvent += DeviceAll_FlowControlFaultOccurEvent;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 关闭继电器
            foreach (var cmd in Enum.GetValues(typeof(Device.RelayProtocol.Cmd_r)))
            {
                this.deviceAll.ryDevice.ryStatusToSet[(int)cmd] = false;
            }
            Utils.Logger.Sys("关闭所有继电器！");
            try
            {
                deviceAll.ryDevice.StatusUpdateToDeviceEvent -= RyDev_StatusUpdateEvent;
                this.deviceAll.ryDevice.UpdateStatusToDevice();
            }
            catch(Exception ex)
            {

            }
            // 关闭系统
            Utils.Logger.Sys("关闭系统软件！");
        }
    }
}
