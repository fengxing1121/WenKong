using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TemperatureControl2
{
    /// <summary>
    /// 系统设置 / 操作相关的代码部分
    /// </summary>
    public partial class FormMain
    {
        // 数据查询
        private void checkBox_dataChk_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory() + "/Logs/Data");
            }
            catch (Exception ex) { }
            
            //Utils.Logger.Sys("查询数据!");
            Utils.Logger.Op("查询数据!");
        }

        private void checkBox_logM_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory() + "/Logs/OperationLog");
            }
            catch (Exception ex) { }
        }

        private void checkBox_logS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory() + "/Logs/OperationLog");
            }
            catch (Exception ex) { }
        }

        private void checkBox_logM_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory() + "/Logs/OperationLog");
            }
            catch (Exception ex) { }
        }

        private void checkBox_logS_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory() + "/Logs/OperationLog");
            }
            catch (Exception ex) { }
        }

        // 调试按钮
        private void button_debug_Click(object sender, EventArgs e)
        {
            bool formExist = false;
            foreach (Form fm in Application.OpenForms)
            {
                if (fm.Name == "FormDebug")
                {
                    // Avoid form being minimized
                    fm.WindowState = FormWindowState.Normal;

                    fm.BringToFront();
                    formExist = true;
                }
            }

            if (!formExist)
            {
                FormDebug fm = new FormDebug(deviceAll);
                fm.Name = "FormDebug";
                fm.Text = "调试参数设置";
                //fm.Location = new System.Drawing.Point(600,300);
                fm.Show();
            }
        }
    }
}
