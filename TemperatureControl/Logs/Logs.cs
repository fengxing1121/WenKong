using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Logs
{
    public static class Logger
    {
        private static string _pathOp = "OperationLog";
        private static string _pathData = "Data";
        private static string _pathSys = "SystemLog";
        private static string _fileOp;
        private static string _fileData;
        private static string _fileSys;
        private static readonly object _Locker = new object();
        private static StreamWriter _writer;

        static Logger()
        {
            // 建立日志文件夹
            if (!Directory.Exists(_pathOp))
                Directory.CreateDirectory(_pathOp);

            if (!Directory.Exists(_pathData))
                Directory.CreateDirectory(_pathData);

            if (!Directory.Exists(_pathSys))
                Directory.CreateDirectory(_pathSys);


            // 新建操作日志文件
            string tim = DateTime.Now.ToString("yyyy-MM-dd-HH");
            _fileOp = _pathOp + "/" + "操作日志" + tim + ".txt";
            _fileData = _pathData + "/" + "数据" + tim + ".txt";
            _fileSys = _pathSys + "/" + "系统日志" + tim + ".txt";

            if (!File.Exists(_fileOp))
            {
                File.Create(_fileOp).Close();
                Debug.WriteLine("日志文件 " + _fileOp + " 不存在，新建该文件！");
            }

            _writer = new StreamWriter(_fileOp, true, Encoding.UTF8);
            _writer.WriteLine("/*****************************/");
            _writer.WriteLine("/********  操作日志  *********/");
            _writer.WriteLine("/******** " + tim + " *********/");
            _writer.WriteLine("/*****************************/");
            _writer.Flush();
            _writer.Close();


            // 新建数据文件
            if (!File.Exists(_fileData))
            {
                File.Create(_fileData).Close();
                Debug.WriteLine("日志文件 " + _fileData + " 不存在，新建该文件！");
            }

            _writer = new StreamWriter(_fileData, true, Encoding.UTF8);
            _writer.WriteLine("/*****************************/");
            _writer.WriteLine("/********  数据文件  *********/");
            _writer.WriteLine("/******** " + tim + " *********/");
            _writer.WriteLine("/*****************************/");
            _writer.Flush();
            _writer.Close();


            // 新建系统日志文件
            if (!File.Exists(_fileSys))
            {
                File.Create(_fileSys).Close();
                Debug.WriteLine("日志文件 " + _fileSys + " 不存在，新建该文件！");
            }

            _writer = new StreamWriter(_fileSys, true, Encoding.UTF8);
            _writer.WriteLine("/*****************************/");
            _writer.WriteLine("/********  系统日志  *********/");
            _writer.WriteLine("/******** " + tim + " *********/");
            _writer.WriteLine("/*****************************/");
            _writer.Flush();
            _writer.Close();

        }

        public static bool Op(string msg)
        {
            lock(_Locker)
            {
                try
                {
                    if (!File.Exists(_fileOp))
                    {
                        File.Create(_fileOp);
                        Debug.WriteLine("日志文件 " + _fileOp + " 不存在，新建该文件！");
                    }

                    _writer = new StreamWriter(_fileOp, true, Encoding.UTF8);
                    _writer.WriteLine(DateTime.Now.ToString() + "    " + msg);
                    _writer.Flush();
                    _writer.Close();
                }
                catch(System.Exception ex)
                {
                    Debug.WriteLine("写入日志文件 " + _fileOp + " 失败！");
                    _writer.Close();
                    return false;
                }
                
            }
            
            return true;
        }
    }
}
