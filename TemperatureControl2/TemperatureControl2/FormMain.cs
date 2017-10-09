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
        }

        
    }
}
