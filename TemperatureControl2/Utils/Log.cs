using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Utils
{
    public static class Logger
    {
        private static string _pathLog;
        private static string _pathOp;
        private static string _pathData;
        private static string _pathSys;
        private static string _fileOp;
        private static string _fileData;
        private static string _fileTempData;
        private static string _fileSys;
        private static readonly object _Locker = new object();
        private static StreamWriter _writer;

        static Logger()
        {
            // 设置路径
            _pathLog = "Logs";
            _pathOp = _pathLog + "/OperationLog";
            _pathData = _pathLog + "/Data";
            _pathSys = _pathLog + "/SystemLog";

            // 建立日志文件夹
            if (!Directory.Exists(_pathLog))
                Directory.CreateDirectory(_pathLog);

            if (!Directory.Exists(_pathOp))
                Directory.CreateDirectory(_pathOp);

            if (!Directory.Exists(_pathData))
                Directory.CreateDirectory(_pathData);

            if (!Directory.Exists(_pathSys))
                Directory.CreateDirectory(_pathSys);


            // 新建操作日志文件
            string tim = DateTime.Now.ToString("yyyy年M月dd日HH时");
            _fileOp = _pathOp + "/" + "操作日志 " + tim + ".txt";
            _fileData = _pathData + "/" + "数据 " + tim + ".txt";
            _fileSys = _pathSys + "/" + "系统日志 " + tim + ".txt";
            _fileTempData = _pathData + "/" + "实时温度数据 " + tim + ".txt";

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
            Debug.WriteLine(tim + "  开始写入操作日志文件");


            // 新建数据文件
            if (!File.Exists(_fileData))
            {
                File.Create(_fileData).Close();
                Debug.WriteLine("数据文件 " + _fileData + " 不存在，新建该文件！");
            }

            _writer = new StreamWriter(_fileData, true, Encoding.UTF8);
            _writer.WriteLine("/*****************************/");
            _writer.WriteLine("/********  数据文件  *********/");
            _writer.WriteLine("/******** " + tim + " *********/");
            _writer.WriteLine("/*****************************/");
            _writer.Flush();
            _writer.Close();
            Debug.WriteLine(tim + "  开始写入数据文件");

            // 新建温度数据文件
            if (!File.Exists(_fileTempData))
            {
                File.Create(_fileTempData).Close();
                Debug.WriteLine("温度数据 " + _fileTempData + " 不存在，新建该文件！");
            }

            _writer = new StreamWriter(_fileTempData, true, Encoding.UTF8);
            _writer.WriteLine("/*****************************/");
            _writer.WriteLine("/********  温度数据文件  *********/");
            _writer.WriteLine("/******** " + tim + " *********/");
            _writer.WriteLine("/*****************************/");
            _writer.Flush();
            _writer.Close();
            Debug.WriteLine(tim + "  开始写入数据文件");


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
            Debug.WriteLine(tim + "  开始写入系统日志文件");

        }


        /// <summary>
        /// 输出操作日志，信息包括：当前时间 + msg
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Op(string msg)
        {
            lock (_Locker)
            {
                try
                {
                    if (!File.Exists(_fileOp))
                    {
                        File.Create(_fileOp).Close();
                        Debug.WriteLine("日志文件 " + _fileOp + " 不存在，新建该文件！");
                    }

                    _writer = new StreamWriter(_fileOp, true, Encoding.UTF8);
                    _writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + msg);
                    _writer.Flush();
                    _writer.Close();
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + msg);
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("写入日志文件 " + _fileOp + " 失败！");
                    _writer.Close();
                    return false;
                }

            }

            return true;
        }


        /// <summary>
        /// 输出系统日志，信息包括：当前时间 + msg
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Sys(string msg)
        {
            lock (_Locker)
            {
                try
                {
                    if (!File.Exists(_fileSys))
                    {
                        File.Create(_fileSys).Close();
                        Debug.WriteLine("日志文件 " + _fileSys + " 不存在，新建该文件！");
                    }

                    _writer = new StreamWriter(_fileSys, true, Encoding.UTF8);
                    _writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + msg);
                    _writer.Flush();
                    _writer.Close();
                    Debug.WriteLine(DateTime.Now.ToString("HHh:mm:ss") + "    " + msg);
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("写入日志文件 " + _fileSys + " 失败！");
                    _writer.Close();
                    return false;
                }

            }
            return true;
        }

        public static bool Data(string data)
        {
            lock (_Locker)
            {
                try
                {
                    if (!File.Exists(_fileData))
                    {
                        File.Create(_fileData).Close();
                        Debug.WriteLine("日志文件 " + _fileData + " 不存在，新建该文件！");
                    }

                    _writer = new StreamWriter(_fileData, true, Encoding.UTF8);
                    _writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + data);
                    _writer.Flush();
                    _writer.Close();
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + data);
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("写入日志文件 " + _fileData + " 失败！");
                    _writer.Close();
                    return false;
                }

            }
            return true;
        }

        public static bool TempData(string data)
        {
            lock (_Locker)
            {
                try
                {
                    if (!File.Exists(_fileTempData))
                    {
                        File.Create(_fileTempData).Close();
                        Debug.WriteLine("日志文件 " + _fileTempData + " 不存在，新建该文件！");
                    }

                    _writer = new StreamWriter(_fileTempData, true, Encoding.UTF8);
                    _writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + data);
                    _writer.Flush();
                    _writer.Close();
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + data);
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("写入日志文件 " + _fileTempData + " 失败！");
                    _writer.Close();
                    return false;
                }

            }
            return true;
        }
    }
}
