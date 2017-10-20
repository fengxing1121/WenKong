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
                FormSetting fm = new FormSetting(deviceAll.tpDeviceM);
                fm.Name = "FormSettingM";
                fm.Text = "主槽控温参数设置";
                fm.Show();
            }
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
                FormSetting fm = new FormSetting(deviceAll.tpDeviceS);
                fm.Name = "FormSettingS";
                fm.Text = "辅槽控温参数设置";
                fm.Show();
            }
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

                    fm.BringToFront();
                    formExist = true;
                }
            }

            if (!formExist)
            {
                FormChart fm = new FormChart(deviceAll.tpDeviceM);
                fm.Name = "FormChartM";
                fm.Text = "主槽温度曲线";
                fm.Show();
            }
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

                    fm.BringToFront();
                    formExist = true;
                }
            }

            if (!formExist)
            {
                FormChart fm = new FormChart(deviceAll.tpDeviceS);
                fm.Name = "FormChartS";
                fm.Text = "辅槽温度曲线";
                fm.Show();
            }
        }


        // 主槽控温设备 - 开始读取温度值
        private void checkBox_startM_Click(object sender, EventArgs e)
        {
            deviceAll.startTemperatureUpdateM(checkBox_startM.Checked);
        }

        // 辅槽控温设备 - 开始读取温度值
        private void checkBox_startS_Click(object sender, EventArgs e)
        {
            deviceAll.startTemperatureUpdateS(checkBox_startS.Checked);
        }


        // 控温设备更新温度值 - 事件处理函数 - 将温度值从 TempDevice 更新到界面
        private void tpDevice_TpTemperatureUpdateTimerEvent(bool err)
        {
            // false 表示从下位机读取温度值时没有错误发生
            if (err == false)
            {
                this.Invoke(new EventHandler(delegate
                {
                    // 更新主槽控温温度 / 功率值
                    if (this.deviceAll.tpMainStart == true)
                    {
                        if (this.deviceAll.tpDeviceM.temperatures.Count > 0)
                            this.label_tempM.Text = this.deviceAll.tpDeviceM.temperatures.Last().ToString() + "℃";
                        else
                        {
                            Debug.WriteLine("未读到温度数据");
                            this.label_tempM.Text = "0.000℃";
                        }
                    }

                    // 更新辅槽控温温度 / 功率值该
                    if(this.deviceAll.tpSubStart == true)
                    {
                        if (this.deviceAll.tpDeviceS.temperatures.Count > 0)
                            this.label_tempS.Text = this.deviceAll.tpDeviceS.temperatures.Last().ToString() + "℃";
                        else
                        {
                            Debug.WriteLine("未读到温度数据");
                            this.label_tempS.Text = "0.000℃";
                        }
                    }
                    
                }));
            }
            // true 表示从下位机读取温度值时发生了错误
            else
            {
                // 更新数据错误
                // wghou
                // code

            }
        }
    }
}
