using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemperatureControl2
{
    /// <summary>
    /// 系统设置 / 操作相关的代码部分
    /// </summary>
    public partial class FormMain
    {
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
            System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory());
        }
    }
}
