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
                FormChart fm = new FormChart(deviceAll,deviceAll.tpDeviceM);
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
                FormChart fm = new FormChart(deviceAll, deviceAll.tpDeviceS);
                fm.Name = "FormChartS";
                fm.Text = "辅槽温度曲线";
                fm.Show();
            }
        }


        /// <summary>
        /// 控温设备更新温度值 - 事件处理函数 - 将温度值从 TempDevice 更新到界面
        /// </summary>
        /// <param name="err"></param>
        private void tpDevice_TpTemperatureUpdateTimerEvent(Device.TempProtocol.Err_t err)
        {
            // false 表示从下位机读取温度值时没有错误发生
            if (err == Device.TempProtocol.Err_t.NoError)
            {
                this.BeginInvoke(new EventHandler(delegate
                {
                    // 更新主槽控温表温度 / 功率值
                    if (this.deviceAll.tpDeviceM.temperatures.Count > 0)
                        this.label_tempM.Text = this.deviceAll.tpDeviceM.temperatures.Last().ToString() + "℃";
                    else
                    {
                        Debug.WriteLine("未读到温度数据");
                        this.label_tempM.Text = "0.000℃";
                    }
                    // 功率系数
                    this.label_powerM.Text = this.deviceAll.tpDeviceM.tpPowerShow.ToString("0.000") + "%";

                    // 更新辅槽控温温度 / 功率值
                    if (this.deviceAll.tpDeviceS.temperatures.Count > 0)
                        this.label_tempS.Text = this.deviceAll.tpDeviceS.temperatures.Last().ToString("0.") + "℃";
                    else
                    {
                        Debug.WriteLine("未读到温度数据");
                        this.label_tempS.Text = "0.000℃";
                    }
                    // 功率系数
                    this.label_powerS.Text = this.deviceAll.tpDeviceS.tpPowerShow.ToString() + "%";

                }));
            }
            // true 表示从下位机读取温度值时发生了错误
            else
            {
                // 更新数据错误
                // wghou
                // code
                this.BeginInvoke(new EventHandler(delegate
                {
                    MessageBox.Show("读取温控设备温度显示值/功率系数时发生错误！/r错误代码：" + err.ToString());
                }));
            }
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
                this.label_tempSetM.Text = deviceAll.tpDeviceM.tpParam[(int)Device.TempProtocol.Cmd_t.TempSet].ToString("0.000");
                this.label_tempSetS.Text = deviceAll.tpDeviceS.tpParam[(int)Device.TempProtocol.Cmd_t.TempSet].ToString("0.000");
            }));
        }
    }
}
