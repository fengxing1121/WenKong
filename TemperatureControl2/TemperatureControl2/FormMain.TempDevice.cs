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


        // 主槽控温设备开始读取温度值
        private void checkBox_startM_Click(object sender, EventArgs e)
        {
            deviceAll.startTemperatureUpdateM(checkBox_startM.Checked);
        }

        private void tpDeviceM_TpTemperatureUpdateTimerEvent(bool err)
        {
            if (err == false)
            {
                this.Invoke(new EventHandler(delegate
                {
                    if (this.deviceAll.tpDeviceM.temperatures.Count > 0)
                        this.textBox_tempM.Text = this.deviceAll.tpDeviceM.temperatures.Last().ToString() + "℃";
                    else
                    {
                        Debug.WriteLine("未读到温度数据");
                        this.textBox_tempM.Text = "0.000℃";
                    }
                }));
            }
        }

        // 辅槽控温设备开始读取温度值
        private void checkBox_startS_Click(object sender, EventArgs e)
        {
            deviceAll.startTemperatureUpdateS(checkBox_startS.Checked);
        }

        private void tpDeviceS_TpTemperatureUpdateTimerEvent(bool err)
        {
            if (err == false)
            {
                this.Invoke(new EventHandler(delegate
                {
                    if (this.deviceAll.tpDeviceS.temperatures.Count > 0)
                        this.textBox_tempS.Text = this.deviceAll.tpDeviceS.temperatures.Last().ToString() + "℃";
                    else
                    {
                        Debug.WriteLine("未读到温度数据");
                        this.textBox_tempS.Text = "0.000℃";
                    }
                }));
            }
        }
    }
}
