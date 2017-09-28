using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Device;
using Logs;

namespace TemperatureControl
{
    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();

            // wghou
            this.tpDeviceM = new Device.TempDevice();
            this.tpDeviceS = new Device.TempDevice();
            this.ryDevice = new Device.RelayDevice();
            this.tpDeviceM.InitCom("COM1");
            this.tpDeviceS.InitCom("COM2");
            this.ryDevice.InitCom("COM3");

        }

        // 起始
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        // 结束
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


        // 自动
        private void checkBox_auto_CheckedChanged(object sender, EventArgs e)
        {

        }


        // 手动
        private void checkBox_man_CheckedChanged(object sender, EventArgs e)
        {

        }


        // 数据查询
        private void checkBox_dataChk_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("E:\\");
            Logger.Op("数据查询");
        }


        // 1 - 总电源
        private void checkBox_elect_CheckStateChanged(object sender, EventArgs e)
        {
            if(checkBox_elect.Checked == true)
            {
                ryDevice.openElect();
            }  

            if (checkBox_elect.Checked == false)
            {
                ryDevice.closeElect();
            }
              
        }


        // 2 - 主槽控温
        private void checkBox_mainHeat_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_mainHeat.Checked == true)
            {
                ryDevice.openMainHeat();
            }

            if(checkBox_mainHeat.Checked == false)
            {
                ryDevice.closeMainHeat();
            }
        }


        // 3 - 辅槽控温
        private void checkBox_subHeat_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_subHeat.Checked == true)
            {
                ryDevice.openSubHeat();
            }

            if (checkBox_subHeat.Checked == false)
            {
                ryDevice.closeSubHeat();
            }
        }


        // 4 - 辅槽制冷
        private void checkBox_subCool_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_subCool.Checked == true)
            {
                ryDevice.openSubCool();
            }

            if (checkBox_subCool.Checked == false)
            {
                ryDevice.closeSubCool();
            }
        }


        // 5 - 辅槽循环
        private void checkBox_subCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_subCircle.Checked == true)
            {
                ryDevice.openSubCircle();
            }

            if (checkBox_subCircle.Checked == false)
            {
                ryDevice.closeSubCircle();
            }
        }


        // 6 - 主槽快冷
        private void checkBox_mainCoolF_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_mainCoolF.Checked == true)
            {
                ryDevice.openMainCoolF();
            }

            if (checkBox_mainCoolF.Checked == false)
            {
                ryDevice.closeMainCoolF();
            }
        }


        // 7 - 辅槽快冷
        private void checkBox_subCoolF_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_subCoolF.Checked == true)
            {
                ryDevice.openSubCoolF();
            }

            if (checkBox_subCoolF.Checked == false)
            {
                ryDevice.closeSubCoolF();
            }
        }


        // 8 - 海水进
        private void checkBox_waterIn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_waterIn.Checked == true)
            {
                ryDevice.openWaterIn();
            }

            if (checkBox_waterIn.Checked == false)
            {
                ryDevice.closeWaterIn();
            }
        }


        // 9 - 海水出
        private void checkBox_waterOut_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_waterOut.Checked == true)
            {
                ryDevice.openWaterOut();
            }

            if (checkBox_waterOut.Checked == false)
            {
                ryDevice.closeWaterOut();
            }
        }

    }
}
