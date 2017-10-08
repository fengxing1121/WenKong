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
    /// <summary>
    /// 继电器设备按键及相关
    /// </summary>
    public partial class FormMain
    {
        /// <summary>
        /// 设置继电器设备状态 - 委托
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="status"></param>
        private delegate void RySetHandler();


        // 1 - 总电源
        private void checkBox_elect_Click(object sender, EventArgs e)
        {
            // 异步调用 RelayDevice 函数，设置继电器状态
            // 结果会触发 继电器设置事件 处理函数
            deviceAll.ryDevice.ryStatusToSet[0] = this.checkBox_elect.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);
        }


        // 2 - 主槽控温
        private void checkBox_mainHeat_Click(object sender, EventArgs e)
        {
            // 异步调用 RelayDevice 函数，设置继电器状态
            // 结果会触发 继电器设置事件 处理函数
            deviceAll.ryDevice.ryStatusToSet[1] = this.checkBox_mainHeat.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);
        }


        // 3 - 辅槽控温
        private void checkBox_subHeat_Click(object sender, EventArgs e)
        {
            // 异步调用 RelayDevice 函数，设置继电器状态
            // 结果会触发 继电器设置事件 处理函数
            deviceAll.ryDevice.ryStatusToSet[2] = this.checkBox_subHeat.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);
        }


        // 4 - 辅槽制冷
        private void checkBox_subCool_Click(object sender, EventArgs e)
        {
            // 异步调用 RelayDevice 函数，设置继电器状态
            // 结果会触发 继电器设置事件 处理函数
            deviceAll.ryDevice.ryStatusToSet[3] = this.checkBox_subCool.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);
        }


        // 5 - 辅槽循环
        private void checkBox_subCircle_Click(object sender, EventArgs e)
        {
            // 异步调用 RelayDevice 函数，设置继电器状态
            // 结果会触发 继电器设置事件 处理函数
            deviceAll.ryDevice.ryStatusToSet[4] = this.checkBox_subCircle.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);
        }


        // 6 - 主槽快冷
        private void checkBox_mainCoolF_Click(object sender, EventArgs e)
        {
            // 异步调用 RelayDevice 函数，设置继电器状态
            // 结果会触发 继电器设置事件 处理函数
            deviceAll.ryDevice.ryStatusToSet[5] = this.checkBox_mainCoolF.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);
        }


        // 7 - 辅槽快冷
        private void checkBox_subCoolF_Click(object sender, EventArgs e)
        {
            // 异步调用 RelayDevice 函数，设置继电器状态
            // 结果会触发 继电器设置事件 处理函数
            deviceAll.ryDevice.ryStatusToSet[6] = this.checkBox_subCoolF.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);
        }


        // 8 - 海水进
        private void checkBox_waterIn_Click(object sender, EventArgs e)
        {
            // 异步调用 RelayDevice 函数，设置继电器状态
            // 结果会触发 继电器设置事件 处理函数
            deviceAll.ryDevice.ryStatusToSet[7] = this.checkBox_waterIn.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);
        }


        // 9 - 海水出
        private void checkBox_waterOut_Click(object sender, EventArgs e)
        {
            // 异步调用 RelayDevice 函数，设置继电器状态
            // 结果会触发 继电器设置事件 处理函数
            deviceAll.ryDevice.ryStatusToSet[8] = this.checkBox_waterOut.Checked;
            RySetHandler setRyStatus = new RySetHandler(this.deviceAll.ryDevice.UpdateStatusToDevice);
            setRyStatus.BeginInvoke(null, null);
        }

    }
}
