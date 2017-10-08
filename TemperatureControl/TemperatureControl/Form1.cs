using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Device;
using Logs;

namespace TemperatureControl
{
    public partial class Form_main : Form
    {
        ///////////////////////////////////////
        #region 用户自定义成员变量部分 by wghou
        private Device.TempDevice tpDeviceM;
        private Device.TempDevice tpDeviceS;
        private Device.RelayDevice ryDevice;
        private FormSetting formStM;
        private FormSetting formStS;
        #endregion


        public Form_main()
        {
            InitializeComponent();

            // wghou
            this.tpDeviceM = new Device.TempDevice();
            this.tpDeviceS = new Device.TempDevice();
            this.ryDevice = new Device.RelayDevice();
            //this.tpDeviceM.InitCom("COM1");
            //this.tpDeviceS.InitCom("COM2");
            //this.ryDevice.InitCom("COM3");


            this.ryDevice.DeviceUpdate += HandleDeviceUpdateEvent;
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
            if (checkBox_auto.Checked == true)
            {
                Logger.Op("自动      开始");
            }

            if (checkBox_auto.Checked == false)
            {
                Logger.Op("自动      停止");
            }
        }


        // 手动
        private void checkBox_man_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_man.Checked == true)
            {
                Logger.Op("手动      开始");
            }

            if (checkBox_man.Checked == false)
            {
                Logger.Op("手动      停止");
            }
            
        }


        // 数据查询
        private void checkBox_dataChk_Click(object sender, EventArgs e)
        {
            Logger.Op("数据查询");
            System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory()+"/Logs");
        }


        // Device Update Event Handle
        void HandleDeviceUpdateEvent(object sender, RelayDevice.RelayDeviceEventArgs e)
        {
            if(e.Sucess == true)
            {
                this.BeginInvoke(new System.Threading.ThreadStart(delegate ()
                {
                    this.checkBox_elect.CheckState = this.checkBox_elect.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
                }));
                MessageBox.Show(ryDevice.paraChNames[e.Index] + "操作成功！");
            }
            else
            {
                MessageBox.Show(ryDevice.paraChNames[e.Index] + "操作失败！");
            }
        }


        // 1 - 总电源
        private void checkBox_elect_Click(object sender, EventArgs e)
        {
            if(checkBox_elect.CheckState == CheckState.Unchecked)
            {
                Logger.Op("总电源  开");
                this.BeginInvoke(new EventHandler(delegate
                {
                    ryDevice.setParam(RelayDevice.Paras_r.Elect, true);
                }));
            }
            else
            {
                Logger.Op("总电源  关");
                this.BeginInvoke(new EventHandler(delegate
                {
                    ryDevice.setParam(RelayDevice.Paras_r.Elect, false);
                }));
            }

        }

        // 2 - 主槽控温
        private void checkBox_mainHeat_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_mainHeat.Checked == true)
            {
                Logger.Op("主槽控温  开");
                
            }

            if(checkBox_mainHeat.Checked == false)
            {
                Logger.Op("主槽控温  关");
                
            }
        }


        // 3 - 辅槽控温
        private void checkBox_subHeat_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_subHeat.Checked == true)
            {
                Logger.Op("辅槽控温  开");
                
            }

            if (checkBox_subHeat.Checked == false)
            {
                Logger.Op("辅槽控温  关");
                
            }
        }


        // 4 - 辅槽制冷
        private void checkBox_subCool_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_subCool.Checked == true)
            {
                Logger.Op("辅槽制冷  开");
                
            }

            if (checkBox_subCool.Checked == false)
            {
                Logger.Op("辅槽制冷  关");
               
            }
        }


        // 5 - 辅槽循环
        private void checkBox_subCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_subCircle.Checked == true)
            {
                Logger.Op("辅槽循环  开");
                
            }

            if (checkBox_subCircle.Checked == false)
            {
                Logger.Op("辅槽循环  关");
                
            }
        }


        // 6 - 主槽快冷
        private void checkBox_mainCoolF_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_mainCoolF.Checked == true)
            {
                Logger.Op("主槽快冷  开");
                
            }

            if (checkBox_mainCoolF.Checked == false)
            {
                Logger.Op("主槽快冷  关");
                
            }
        }


        // 7 - 辅槽快冷
        private void checkBox_subCoolF_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_subCoolF.Checked == true)
            {
                Logger.Op("辅槽快冷  开");
               
            }

            if (checkBox_subCoolF.Checked == false)
            {
                Logger.Op("辅槽快冷  关");
                
            }
        }


        // 8 - 海水进
        private void checkBox_waterIn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_waterIn.Checked == true)
            {
                Logger.Op("海水进    开");
                
            }

            if (checkBox_waterIn.Checked == false)
            {
                Logger.Op("海水进    关");
                
            }
        }


        // 9 - 海水出
        private void checkBox_waterOut_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_waterOut.Checked == true)
            {
                Logger.Op("海水出    开");
                
            }

            if (checkBox_waterOut.Checked == false)
            {
                Logger.Op("海水出    关");
                
            }
        }

        private void checkBox_paramM_Click(object sender, EventArgs e)
        {
            if(formStM == null || formStM.IsDisposed)
            {
                formStM = new FormSetting(ref tpDeviceM);
                formStM.Text = "主槽控温";
            }
            formStM.Show();
            formStM.Activate();
            //formStM.ShowDialog();
        }


        private void checkBox_paramS_Click(object sender, EventArgs e)
        {

        }

        
    }
}
