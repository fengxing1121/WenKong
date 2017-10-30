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
        // 数据查询
        private void checkBox_dataChk_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.IO.Directory.GetCurrentDirectory());
            Utils.Logger.Sys("查询数据!");
            Utils.Logger.Op("查询数据!");
        }
    }
}
