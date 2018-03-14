using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.POIFS.FileSystem;

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
        private static string _fileSys;
        private static readonly object _Locker = new object();
        private static StreamWriter _writer = null;

        static Logger()
        {
            // 设置路径
            _pathLog = "Logs";
            _pathOp = _pathLog + "/OperationLog";
            _pathData = _pathLog + "/Data";
            _pathSys = _pathLog + "/SystemLog";

            

            try
            {
                // 建立日志文件夹
                if (!Directory.Exists(_pathLog))
                    Directory.CreateDirectory(_pathLog);

                if (!Directory.Exists(_pathOp))
                    Directory.CreateDirectory(_pathOp);

                if (!Directory.Exists(_pathData))
                    Directory.CreateDirectory(_pathData);

                if (!Directory.Exists(_pathSys))
                    Directory.CreateDirectory(_pathSys);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("日志目录初始化错误");
            }

            string tim = DateTime.Now.ToString("yyyy年M月dd日");
            _fileOp = _pathOp + "/" + "操作日志 " + tim + ".txt";
            _fileData = _pathData + "/" + "数据 " + tim + ".xls";
            _fileSys = _pathSys + "/" + "系统日志 " + tim + ".txt";

            try
            {
                // 新建操作日志文件
                if (!File.Exists(_fileOp))
                {
                    File.Create(_fileOp).Close();
                    Debug.WriteLine("日志文件 " + _fileOp + " 不存在，新建该文件！");

                    _writer = new StreamWriter(_fileOp, true, Encoding.UTF8);
                    _writer.WriteLine("/*****************************/");
                    _writer.WriteLine("/********  操作日志  *********/");
                    _writer.WriteLine("/******** " + tim + " *********/");
                    _writer.WriteLine("/*****************************/");
                    _writer.Flush();
                    _writer.Close();
                    _writer = null;
                    Debug.WriteLine(tim + "  开始写入操作日志文件");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("写入操作日志错误！");
                try { _writer.Close(); _writer = null; } catch { }
            }

            try
            {
                // 新建系统日志文件
                if (!File.Exists(_fileSys))
                {
                    File.Create(_fileSys).Close();
                    Debug.WriteLine("日志文件 " + _fileSys + " 不存在，新建该文件！");
                    _writer = new StreamWriter(_fileSys, true, Encoding.UTF8);
                    _writer.WriteLine("/*****************************/");
                    _writer.WriteLine("/********  系统日志  *********/");
                    _writer.WriteLine("/******** " + tim + " *********/");
                    _writer.WriteLine("/*****************************/");
                    _writer.Flush();
                    _writer.Close();
                    _writer = null;
                    Debug.WriteLine(tim + "  开始写入系统日志文件");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("写入系统日志错误！");
                try { _writer.Close(); _writer = null; } catch { }
            }

            FileStream fs = null;
            HSSFWorkbook wb = null;
            try
            {
                // 新建数据文件
                if (File.Exists(_fileData))
                    return;

                File.Create(_fileData).Close();
                Debug.WriteLine("数据文件 " + _fileData + " 不存在，新建该文件！");

                fs = new FileStream(_fileData, FileMode.Create);
                wb = new HSSFWorkbook();
                wb.CreateSheet("实时温度");
                wb.CreateSheet("电桥温度");
                wb.CreateSheet("电导率数据");

                ISheet sheet0 = wb.GetSheet("实时温度");
                IRow rowSh0 = sheet0.GetRow(0);
                rowSh0 = sheet0.CreateRow(0);
                ICell cell0Sh0 = rowSh0.CreateCell(0, CellType.String);
                cell0Sh0.SetCellValue("时间");
                ICell cell1Sh0 = rowSh0.CreateCell(1, CellType.String);
                cell1Sh0.SetCellValue("主槽温度值");

                ISheet sheet1 = wb.GetSheet("电桥温度");
                IRow rowSh1 = sheet1.GetRow(0);
                rowSh1 = sheet1.CreateRow(0);
                ICell cell0Sh1 = rowSh1.CreateCell(0, CellType.String);
                cell0Sh1.SetCellValue("时间");
                ICell cell1Sh1 = rowSh1.CreateCell(1, CellType.String);
                cell1Sh1.SetCellValue("电桥温度值");

                ISheet sheet2 = wb.GetSheet("电导率数据");
                IRow rowSh2 = sheet2.CreateRow(0);
                ICell cell0Sh2 = rowSh2.CreateCell(0, CellType.String);
                cell0Sh2.SetCellValue("时间");
                ICell cell1Sh2 = rowSh2.CreateCell(1, CellType.String);
                cell1Sh2.SetCellValue("温度值");
                ICell cell2Sh2 = rowSh2.CreateCell(2, CellType.String);
                cell2Sh2.SetCellValue("电导率");

                fs.Flush();
                wb.Write(fs);
                fs.Close();
                fs.Dispose();
                wb = null;
                fs = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("新建数据文件失败！");
                
                try { wb = null; fs.Close(); fs.Dispose(); fs = null; } catch { }
            }

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
                }
                catch(Exception ex)
                {
                    // 新建文件仍然失败
                    //throw new Exception("操作日志文件不存在，且重建失败！");
                    return false;
                }


                try
                {
                    _writer = new StreamWriter(_fileOp, true, Encoding.UTF8);
                    _writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + msg);
                    _writer.Flush();
                    _writer.Close();
                    _writer = null;
                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + msg);
                }
                catch (System.Exception ex)
                {
                    // 写入日志文件失败，这个异常就不处理了
                    // 因为也不是致命的错误，返回 false 就可以了
                    Debug.WriteLine("写入日志文件 " + _fileOp + " 失败！");
                    try { _writer.Close(); _writer = null; } catch { }
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
                }
                catch(Exception ex)
                {
                    // 新建文件仍然失败
                    //throw new Exception("系统日志文件不存在，且重建失败！");
                    return false;
                }

                try
                {
                    _writer = new StreamWriter(_fileSys, true, Encoding.UTF8);
                    _writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "    " + msg);
                    _writer.Flush();
                    _writer.Close();
                    _writer = null;
                    Debug.WriteLine(DateTime.Now.ToString("HHh:mm:ss") + "    " + msg);
                }
                catch (System.Exception ex)
                {
                    // 写入日志文件失败，这个异常就不处理了
                    // 因为也不是致命的错误，返回 false 就可以了
                    Debug.WriteLine("写入日志文件 " + _fileSys + " 失败！");
                    try { _writer.Close(); _writer = null; } catch { }
                    return false;
                }

            }
            return true;
        }


        /// <summary>
        /// 记录电导率
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="conductivity"></param>
        /// <returns></returns>
        public static bool ConductivityData(float temp, float conductivity)
        {
            lock (_Locker)
            {
                FileStream fs = null;
                HSSFWorkbook wb = null;

                try
                {
                    if (!File.Exists(_fileData))
                    {
                        File.Create(_fileData).Close();
                        Debug.WriteLine("数据文件 " + _fileData + " 不存在，新建该文件！");

                        fs = new FileStream(_fileData, FileMode.Create);
                        wb = new HSSFWorkbook();
                        wb.CreateSheet("实时温度");
                        wb.CreateSheet("电桥温度");
                        wb.CreateSheet("电导率数据");

                        ISheet sheet0 = wb.GetSheet("实时温度");
                        IRow rowSh0 = sheet0.GetRow(0);
                        rowSh0 = sheet0.CreateRow(0);
                        ICell cell0Sh0 = rowSh0.CreateCell(0, CellType.String);
                        cell0Sh0.SetCellValue("时间");
                        ICell cell1Sh0 = rowSh0.CreateCell(1, CellType.String);
                        cell1Sh0.SetCellValue("主槽温度值");

                        ISheet sheet1 = wb.GetSheet("电桥温度");
                        IRow rowSh1 = sheet1.GetRow(0);
                        rowSh1 = sheet1.CreateRow(0);
                        ICell cell0Sh1 = rowSh1.CreateCell(0, CellType.String);
                        cell0Sh1.SetCellValue("时间");
                        ICell cell1Sh1 = rowSh1.CreateCell(1, CellType.String);
                        cell1Sh1.SetCellValue("电桥温度值");

                        ISheet sheet2 = wb.GetSheet("电导率数据");
                        IRow rowSh2 = sheet2.CreateRow(0);
                        ICell cell0Sh2 = rowSh2.CreateCell(0, CellType.String);
                        cell0Sh2.SetCellValue("时间");
                        ICell cell1Sh2 = rowSh2.CreateCell(1, CellType.String);
                        cell1Sh2.SetCellValue("温度值");
                        ICell cell2Sh2 = rowSh2.CreateCell(2, CellType.String);
                        cell2Sh2.SetCellValue("电导率");

                        fs.Flush();
                        wb.Write(fs);
                        fs.Close();
                        fs.Dispose();
                        wb = null;
                        fs = null;
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("数据文件不存在，且重建失败！");
                    try { wb = null; fs.Close(); fs.Dispose(); fs = null; } catch { }
                    //throw new Exception("数据文件不存在，且重建失败！");
                    return false;
                }

                FileStream fout = null;
                try
                {
                    // 读取流
                    fs = new FileStream(_fileData, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    POIFSFileSystem ps = new POIFSFileSystem(fs);
                    wb = new HSSFWorkbook(ps);
                    ISheet sheet = wb.GetSheet("电导率数据");

                    // 写入流
                    fout = new FileStream(_fileData, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    // 在工作表结尾添加一行
                    IRow row = sheet.CreateRow(sheet.LastRowNum + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(DateTime.Now.ToString("HH:mm:ss"));
                    ICell cell1 = row.CreateCell(1);
                    cell1.SetCellValue(temp);
                    ICell cell2 = row.CreateCell(2);
                    cell2.SetCellValue(conductivity);

                    // 写入到 xls 文件
                    fout.Flush();
                    wb.Write(fout);
                    wb = null;
                    fout.Close();
                    fout = null;
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("电导率写入失败");
                    try { wb = null; fout.Close();fout = null; } catch { }
                    return false;
                }

            }
            return true;
        }


        /// <summary>
        /// 记录实时温度值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception">系统日志文件不存在，重建失败</exception>
        public static bool TempData(float data)
        {
            lock (_Locker)
            {
                FileStream fs = null;
                HSSFWorkbook wb = null;
                try
                {
                    if (!File.Exists(_fileData))
                    {
                        File.Create(_fileData).Close();
                        Debug.WriteLine("数据文件 " + _fileData + " 不存在，新建该文件！");

                        fs = new FileStream(_fileData, FileMode.Create);
                        wb = new HSSFWorkbook();
                        wb.CreateSheet("实时温度");
                        wb.CreateSheet("电桥温度");
                        wb.CreateSheet("电导率数据");

                        ISheet sheet0 = wb.GetSheet("实时温度");
                        IRow rowSh0 = sheet0.GetRow(0);
                        rowSh0 = sheet0.CreateRow(0);
                        ICell cell0Sh0 = rowSh0.CreateCell(0, CellType.String);
                        cell0Sh0.SetCellValue("时间");
                        ICell cell1Sh0 = rowSh0.CreateCell(1, CellType.String);
                        cell1Sh0.SetCellValue("主槽温度值");

                        ISheet sheet1 = wb.GetSheet("电桥温度");
                        IRow rowSh1 = sheet1.GetRow(0);
                        rowSh1 = sheet1.CreateRow(0);
                        ICell cell0Sh1 = rowSh1.CreateCell(0, CellType.String);
                        cell0Sh1.SetCellValue("时间");
                        ICell cell1Sh1 = rowSh1.CreateCell(1, CellType.String);
                        cell1Sh1.SetCellValue("电桥温度值");

                        ISheet sheet2 = wb.GetSheet("电导率数据");
                        IRow rowSh2 = sheet2.CreateRow(0);
                        ICell cell0Sh2 = rowSh2.CreateCell(0, CellType.String);
                        cell0Sh2.SetCellValue("时间");
                        ICell cell1Sh2 = rowSh2.CreateCell(1, CellType.String);
                        cell1Sh2.SetCellValue("温度值");
                        ICell cell2Sh2 = rowSh2.CreateCell(2, CellType.String);
                        cell2Sh2.SetCellValue("电导率");

                        fs.Flush();
                        wb.Write(fs);
                        fs.Close();
                        fs.Dispose();
                        wb = null;
                        fs = null;
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("数据文件不存在，且重建失败！");
                    try { wb = null; fs.Close(); fs.Dispose(); fs = null; } catch { }
                    //throw new Exception("数据文件不存在，且重建失败！");
                    return false;
                }

                FileStream fout = null;
                try
                {
                    // 读取流
                    fs = new FileStream(_fileData, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    POIFSFileSystem ps = new POIFSFileSystem(fs);
                    wb = new HSSFWorkbook(ps);
                    ISheet sheet = wb.GetSheet("实时温度");

                    // 写入流
                    fout = new FileStream(_fileData, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    // 在工作表结尾添加一行
                    IRow row = sheet.CreateRow(sheet.LastRowNum + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(DateTime.Now.ToString("HH:mm:ss"));
                    ICell cell1 = row.CreateCell(1);
                    cell1.SetCellValue(data);

                    // 写入到 xls 文件
                    fout.Flush();
                    wb.Write(fout);
                    wb = null;
                    fout.Close();
                    fout = null;
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("实时温度写入失败");
                    try { wb = null; fout.Close(); fout = null; } catch { }
                    return false;
                }

            }
            return true;
        }


        /// <summary>
        /// 电桥温度 - 记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool BridgeData(float data)
        {
            lock (_Locker)
            {
                FileStream fs = null;
                HSSFWorkbook wb = null;
                try
                {
                    if (!File.Exists(_fileData))
                    {
                        File.Create(_fileData).Close();
                        Debug.WriteLine("数据文件 " + _fileData + " 不存在，新建该文件！");

                        fs = new FileStream(_fileData, FileMode.Create);
                        wb = new HSSFWorkbook();
                        wb.CreateSheet("实时温度");
                        wb.CreateSheet("电桥温度");
                        wb.CreateSheet("电导率数据");

                        ISheet sheet0 = wb.GetSheet("实时温度");
                        IRow rowSh0 = sheet0.GetRow(0);
                        rowSh0 = sheet0.CreateRow(0);
                        ICell cell0Sh0 = rowSh0.CreateCell(0, CellType.String);
                        cell0Sh0.SetCellValue("时间");
                        ICell cell1Sh0 = rowSh0.CreateCell(1, CellType.String);
                        cell1Sh0.SetCellValue("主槽温度值");

                        ISheet sheet1 = wb.GetSheet("电桥温度");
                        IRow rowSh1 = sheet1.GetRow(0);
                        rowSh1 = sheet1.CreateRow(0);
                        ICell cell0Sh1 = rowSh1.CreateCell(0, CellType.String);
                        cell0Sh1.SetCellValue("时间");
                        ICell cell1Sh1 = rowSh1.CreateCell(1, CellType.String);
                        cell1Sh1.SetCellValue("电桥温度值");

                        ISheet sheet2 = wb.GetSheet("电导率数据");
                        IRow rowSh2 = sheet2.CreateRow(0);
                        ICell cell0Sh2 = rowSh2.CreateCell(0, CellType.String);
                        cell0Sh2.SetCellValue("时间");
                        ICell cell1Sh2 = rowSh2.CreateCell(1, CellType.String);
                        cell1Sh2.SetCellValue("温度值");
                        ICell cell2Sh2 = rowSh2.CreateCell(2, CellType.String);
                        cell2Sh2.SetCellValue("电导率");

                        fs.Flush();
                        wb.Write(fs);
                        fs.Close();
                        fs.Dispose();
                        wb = null;
                        fs = null;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("数据文件不存在，且重建失败！");
                    try { wb = null; fs.Close(); fs.Dispose(); fs = null; } catch { }
                    //throw new Exception("数据文件不存在，且重建失败！");
                    return false;
                }

                FileStream fout = null;
                try
                {
                    // 读取流
                    fs = new FileStream(_fileData, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    POIFSFileSystem ps = new POIFSFileSystem(fs);
                    wb = new HSSFWorkbook(ps);
                    ISheet sheet = wb.GetSheet("电桥温度");

                    // 写入流
                    fout = new FileStream(_fileData, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    // 在工作表结尾添加一行
                    IRow row = sheet.CreateRow(sheet.LastRowNum + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(DateTime.Now.ToString("HH:mm:ss"));
                    ICell cell1 = row.CreateCell(1);
                    cell1.SetCellValue(data);

                    // 写入到 xls 文件
                    fout.Flush();
                    wb.Write(fout);
                    wb = null;
                    fout.Close();
                    fout = null;
                }
                catch (System.Exception ex)
                {
                    Debug.WriteLine("电桥温度写入失败");
                    try { wb = null; fout.Close(); fout = null; } catch { }
                    return false;
                }

            }
            return true;
        }
    }
}
