using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TemperatureControl2
{
    static class Program
    {
#if DEBUG
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();//Call Sysytem API，Disposed Console
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();//Call Sysytem API，Show Console
#endif
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
            AllocConsole();//Show Console
#endif
            Application.Run(new FormMain());
#if DEBUG
            FreeConsole();//Disposed Console
#endif
        }
    }
}
