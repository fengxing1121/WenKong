using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TemperatureControl2
{
    /// <summary>
    /// 温控设备及相关代码
    /// </summary>
    public partial class FormMain
    {
        #region 控温表
        // 主槽控温表 - 参数设置
        private void checkBox_paramM_Click(object sender, EventArgs e)
        {
            bool formExist = false;
            foreach(Form fm in Application.OpenForms)
            {
                if(fm.Name == "FormSettingM")
                {
                    // Avoid form being minimized
                    fm.WindowState = FormWindowState.Normal;

                    fm.BringToFront();
                    formExist = true;
                }
            }

            if(!formExist)
            {
                FormSetting fm = new FormSetting(deviceAll.tpDeviceM, deviceAll);
                fm.Name = "FormSettingM";
                fm.Text = "主槽控温参数设置";
                //fm.Location = new System.Drawing.Point(600,300);
                fm.Show();
            }

            Utils.Logger.Sys("打开主槽控温设备参数设置界面!");
            Utils.Logger.Op("打开主槽控温设备参数设置界面!");
        }


        // 辅槽控温表 - 参数设置
        private void checkBox_paramS_Click(object sender, EventArgs e)
        {
            bool formExist = false;
            foreach (Form fm in Application.OpenForms)
            {
                if (fm.Name == "FormSettingS")
                {
                    // Avoid form being minimized
                    fm.WindowState = FormWindowState.Normal;

                    fm.BringToFront();
                    formExist = true;
                }
            }

            if (!formExist)
            {
                FormSetting fm = new FormSetting(deviceAll.tpDeviceS,deviceAll);
                fm.Name = "FormSettingS";
                fm.Text = "辅槽控温参数设置";
                //fm.Location = new System.Drawing.Point(600, 500);
                fm.Show();
            }

            Utils.Logger.Sys("打开辅槽控温设备参数设置界面!");
            Utils.Logger.Op("打开辅槽控温设备参数设置界面!");
        }
        #endregion


        // 主槽控温表 - 温度曲线
        private void checkBox_curveM_Click(object sender, EventArgs e)
        {
            bool formExist = false;
            foreach (Form fm in Application.OpenForms)
            {
                if (fm.Name == "FormChartM")
                {
                    // Avoid form being minimized
                    fm.WindowState = FormWindowState.Normal;
                    fm.Location = new System.Drawing.Point(10, 12);
                    fm.BringToFront();
                    formExist = true;
                }
            }

            if (!formExist)
            {
                FormChart fm = new FormChart(deviceAll,deviceAll.tpDeviceM);
                fm.Location = new System.Drawing.Point(10, 12);
                fm.Name = "FormChartM";
                fm.Text = "主槽温度曲线";
                fm.Show();
            }

            Utils.Logger.Sys("打开主槽控温设备温度曲线界面!");
            Utils.Logger.Op("打开主槽控温设备温度曲线界面!");
        }


        // 辅主槽控温表 - 温度曲线
        private void checkBox_curveS_Click(object sender, EventArgs e)
        {
            bool formExist = false;
            foreach (Form fm in Application.OpenForms)
            {
                if (fm.Name == "FormChartS")
                {
                    // Avoid form being minimized
                    fm.WindowState = FormWindowState.Normal;
                    fm.Location = new System.Drawing.Point(10, 380);
                    fm.BringToFront();
                    formExist = true;
                }
            }

            if (!formExist)
            {
                FormChart fm = new FormChart(deviceAll, deviceAll.tpDeviceS);
                fm.Location = new System.Drawing.Point(10, 380);
                fm.Name = "FormChartS";
                fm.Text = "辅槽温度曲线";
                fm.Show();
            }

            Utils.Logger.Sys("打开辅槽控温设备温度曲线界面!");
            Utils.Logger.Op("打开辅槽控温设备温度曲线界面!");
        }


        /// <summary>
        /// 控温设备更新温度值 - 事件处理函数 - 将温度值从 TempDevice 更新到界面
        /// 不对错误进行处理
        /// </summary>
        /// <param name="err"></param>
        private void tpDevice_TpTemperatureUpdateTimerEvent()
        {
            // 不论有没有错误发生，都会从 TempDevice 中更新数据到主界面
            // 也就是说，不在这儿处理错误
            this.BeginInvoke(new EventHandler(delegate
            {
                // 更新主槽控温表温度 / 功率值
                if (this.deviceAll.tpDeviceM.temperatures.Count > 0)
                {
                    string tpM = this.deviceAll.tpDeviceM.temperatures.Last().ToString("0.0000");
                    this.label_tempM.Text = tpM + "℃";
                    //Utils.Logger.Sys("主槽温度： " + tpM);
                } 
                else
                {
                    Debug.WriteLine("未读到温度数据");
                    this.label_tempM.Text = "0.0000℃";
                }
                // 功率系数
                this.label_powerM.Text = this.deviceAll.tpDeviceM.tpPowerShow.ToString("0") + "%";

                // 更新辅槽控温温度 / 功率值
                if (this.deviceAll.tpDeviceS.temperatures.Count > 0)
                    this.label_tempS.Text = this.deviceAll.tpDeviceS.temperatures.Last().ToString("0.000") + "℃";
                else
                {
                    Debug.WriteLine("未读到温度数据");
                    this.label_tempS.Text = "0.000℃";
                }
                // 功率系数
                this.label_powerS.Text = this.deviceAll.tpDeviceS.tpPowerShow.ToString("0") + "%";

                // 当前状态提示
                // wghou

                float fluc = 0.0f;
                deviceAll.tpDeviceM.GetFlucDurCountOrLess(deviceAll.steadyTimeSec / deviceAll.tpDeviceM.readTempIntervalSec, out fluc);
                this.label_fluc.Text = "主控温槽波动度：" + fluc.ToString("0.0000") + "℃ / " + (deviceAll.steadyTimeSec / 60).ToString("0") + " 分钟";

                /*
                if (deviceAll.tpDeviceM.GetFluc( deviceAll.steadyTimeSec / deviceAll.tpDeviceM.readTempIntervalSec, out fluc))
                    this.label_fluc.Text = "主控温槽波动度：" + fluc.ToString("0.0000") + "℃ / " + (deviceAll.steadyTimeSec / 60).ToString("0") + " 分钟";
                else
                    this.label_fluc.Text = "主控温槽波动度：** / " + (deviceAll.steadyTimeSec/60).ToString("0") + " 分钟"; 
                */

                // 设置主电源的禁用状态
                this.checkBox_elect.Enabled = this.deviceAll.ryElecEnable;

            }));

            //Utils.Logger.Sys("从温控设备成功读到了温度 / 功率等数据.");           
        }


        /// <summary>
        /// 主槽控温设备参数更新 / 写入 - 事件处理函数 - 需要更新主界面中的温度设定值
        /// </summary>
        /// <param name="err"></param>
        private void TpDeviceM_ParamUpdatedToDeviceEvent(Device.TempProtocol.Err_t err)
        {
            // 不论有没有错误发生，都更新主界面中的温度设定值
            // 因为 tpParam[] 中始终存放有参数的正确值
            // 如果温度设定值写入 / 读取正确，而后面的其他参数发生了错误，同样会返回错误标志位
            this.BeginInvoke(new EventHandler(delegate
            {
                // 更新主界面的温度设定值
                this.label_tempSetM.Text = deviceAll.tpDeviceM.tpParam[(int)Device.TempProtocol.Cmd_t.TempSet].ToString("0.0000") + "℃";
                this.label_tempSetS.Text = deviceAll.tpDeviceS.tpParam[(int)Device.TempProtocol.Cmd_t.TempSet].ToString("0.000") + "℃";
            }));
        }
    }
}
