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
        private CheckBox[] checkBox_ryDevices = new CheckBox[9];
        Device.Devices deviceAll = new Device.Devices();
        
        public FormMain()
        {
            // 初始化
            InitializeComponent();

            // 继电器设备编号
            checkBox_ryDevices[0] = checkBox_elect;
            checkBox_ryDevices[1] = checkBox_mainHeat;
            checkBox_ryDevices[2] = checkBox_subHeat;
            checkBox_ryDevices[3] = checkBox_subCool;
            checkBox_ryDevices[4] = checkBox_subCircle;
            checkBox_ryDevices[5] = checkBox_mainCoolF;
            checkBox_ryDevices[6] = checkBox_subCoolF;
            checkBox_ryDevices[7] = checkBox_waterIn;
            checkBox_ryDevices[8] = checkBox_waterOut;

            // 温度数据读取 - 定时器事件
            deviceAll.TpTemperatureUpdateTimerEvent += tpDevice_TpTemperatureUpdateTimerEvent;
            deviceAll.ryDevice.StatusUpdateToDeviceEvent += RyDev_StatusUpdateEvent;
        }


        /// <summary>
        /// 主窗体载入时，进行设备的初始化及自检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // 给出配置文件的路径
            if(!deviceAll.Configure(@"./config.ini"))
            {
                MessageBox.Show("设备配置错误，请检查硬件连接，并重新运行程序！");
                this.Close();
                return;
            }

            if(!deviceAll.DeviceSelfCheck())
            {
                MessageBox.Show("设备自检错误，请检查硬件连接，并重新运行程序！");
                this.Close();
                return;
            }

            for(int i = 1;i<checkBox_ryDevices.Length;i++)
            {
                this.checkBox_ryDevices[i].Enabled = false;
            }
        }

    }
}
