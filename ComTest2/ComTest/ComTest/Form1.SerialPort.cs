
#define OUT1



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
using System.Drawing;

namespace ComTest
{
    public partial class Form1
    {
        ////////////////////////////
        // 参数 - 用于生成相关类型的数据曲线
        ////////////////////////////

        ////////////////////////////
        // 继电器状态
        ///
        /// <summary>
        /// 继电器设备错误状态
        /// </summary>
        enum RyStatus : int
        {
            /// <summary>
            /// 处于正常工作状态
            /// </summary>
            OK = 0,
            /// <summary>
            /// 连接断开 - 即不返回任何数据
            /// </summary>
            DisConnected = 1,
            /// <summary>
            /// 数据错误
            /// </summary>
            DataErr = 2,
        }
        /// <summary>
        /// 锁 - 用于锁定继电器状态参数
        /// </summary>
        object ryLocker = new object();
        /// <summary>
        /// 继电器设备 - 当前工作状态 - 默认正常工作
        /// </summary>
        RyStatus ryErrStatus = RyStatus.OK;
        /// <summary>
        /// 继电器设备 - 在产生一次错误状态后，是否保持错误状态 - 默认不保持
        /// </summary>
        bool ryErrLast = false;
        /// <summary>
        /// 继电器设备代码错误
        /// </summary>
        bool ryCodeErr = false;


        ///////////////////////////////
        // 控温槽状态
        //
        /// <summary>
        /// 温控设备错误状态
        /// </summary>
        enum TempStatus : int
        {
            /// <summary>
            /// 工作状态正常
            /// </summary>
            OK = 0,
            /// <summary>
            /// 连接断开 - 即不返回数据
            /// </summary>
            DisConnected = 1,
            /// <summary>
            /// 数据校验错误
            /// </summary>
            DataErr = 2,
            /// <summary>
            /// 温度不下降
            /// </summary>
            //TempNotDown = 3,
            /// <summary>
            /// 温度不上升
            /// </summary>
            //TempNotUp = 4,
            /// <summary>
            /// 温度波动度大
            /// </summary>
            //TempFlucLarge = 5,
            /// <summary>
            /// 温度偏离设定值
            /// </summary>
            //TempBasis = 6,
            /// <summary>
            /// 温度超出设定界限
            /// </summary>
            //TempOutRange = 7,
        }

        /// <summary>
        /// 控温槽参数
        /// </summary>
        class TempParam
        {
            /// <summary>
            /// 当前温度
            /// </summary>
            public float CurTemp = 18.0f;
            /// <summary>
            /// 温度设定点
            /// </summary>
            public float SetTemp = 18.0f;
            /// <summary>
            /// 当前是否已经稳定
            /// </summary>
            public bool Steady = false;
            /// <summary>
            /// 温度变化速率 - 下降
            /// </summary>
            public float ChangeRate = 0.025f;
            /// <summary>
            /// 按正弦信号 - 0~10
            /// </summary>
            public int PhaseCount = 0;
            /// <summary>
            /// 温度波动度
            /// </summary>
            public float Fluc = 0.05f;
            /// <summary>
            /// 附加温度值
            /// </summary>
            public float CurTempPlus = 0.0f;
            /// <summary>
            /// 附加温度变化率
            /// </summary>
            public float ChangeRatePlus = 0.0f;
            /// <summary>
            /// 附加温度波动度
            /// </summary>
            public float FlucPlus = 0.0f;
        }

        ////////////////////////////////
        // 主控温槽状态
        ///
        /// <summary>
        /// 锁 - 用于锁定主控温槽状态参数
        /// </summary>
        object tmLocker = new object();
        /// <summary>
        /// 主控温槽 - 错误状态
        /// </summary>
        TempStatus tmErrStatus = TempStatus.OK;
        /// <summary>
        /// 主控温槽 - 在产生一次错误状态后，是否保持错误状态 - 默认不保持
        /// </summary>
        bool tmErrLast = false;
        /// <summary>
        /// 主控温槽设备代码错误
        /// </summary>
        bool tmCodeErr = false;
        /// <summary>
        /// 主控温槽 - 参数
        /// </summary>
        TempParam tmParam = new TempParam();


        //////////////////////////////
        // 辅控温槽状态
        ///
        /// <summary>
        /// 锁 - 用于锁定辅控温槽状态参数
        /// </summary>
        object tsLocker = new object();
        /// <summary>
        /// 辅控温槽 - 错误状态
        /// </summary>
        TempStatus tsErrStatus = TempStatus.OK;
        /// <summary>
        /// 辅控温槽 - 在产生一次错误状态后，是否保持错误状态 - 默认不保持
        /// </summary>
        bool tsErrLast = false;
        /// <summary>
        /// 辅控温槽代码错误
        /// </summary>
        bool tsCodeErr = false;
        /// <summary>
        /// 辅控温槽 - 参数
        /// </summary>
        TempParam tsParam = new TempParam();


        ///////////////////////////////
        // 传感器设备
        ///
        /// <summary>
        /// 传感器设备错误状态
        /// </summary>
        enum SrStatus : int
        {
            /// <summary>
            /// 正常工作状态
            /// </summary>
            OK = 0,
            /// <summary>
            /// 连接断开
            /// </summary>
            DisConnected = 1,
            /// <summary>
            /// 数据错误
            /// </summary>
            DataErr = 2,
        }
        /// <summary>
        /// 传感器温度值
        /// </summary>
        float srValue = 12.00f;
        /// <summary>
        /// 锁 - 用于锁定传感器状态参数
        /// </summary>
        object srLocker = new object();
        /// <summary>
        /// 传感器 - 错误状态
        /// </summary>
        SrStatus srErrStatus = SrStatus.OK;
        /// <summary>
        /// 传感器 - 在产生一次错误状态后，是否保持错误状态 - 默认不保持
        /// </summary>
        bool srErrLast = false;
        /// <summary>
        /// 传感器设备代码错误
        /// </summary>
        bool srCodeErr = false;


        ////////////////////////////////////
        // 端口数据传输函数
        ////////////////////////////////////
        ///
        /// <summary>
        /// 传感器 - 信息收发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SPortSr_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string dataRev = string.Empty;

            try
            {
                dataRev = sPortSr.ReadTo(":");
                sPortSr.DiscardInBuffer();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("SPortSr_DataReceived Exception when ReadData : " + ex.Message);

                // 出现代码错误
                srCodeErr = true;
            }
            

            lock(ryLocker)
            {
                // 调试信息
                Debug.WriteLine("传感器设备读取到了数据：" + dataRev + " 设备状态：" + srErrStatus.ToString());

                try
                {
                    switch (srErrStatus)
                    {
                        case SrStatus.OK:
                            // 正常工作状态
                            if (dataRev.Contains("R"))
                            {
                                // 上位机读取数据
                                float temp = srValue;
                                dataRev += temp.ToString("0.000");
                                dataRev += ":";
                                sPortSr.WriteLine(dataRev);
                            }
                            else
                            {
                                // 未知指令
                                // 指令不存在
                                sPortSr.WriteLine("@35EB:");
                            }
                            //Debug.WriteLine("传感器设备返回了数据：" + dataRev.ToString());
                            break;
                        case SrStatus.DisConnected:
                            // 连接断开状态 - 不返回任何数据
                            break;
                        case SrStatus.DataErr:
                            // 错误状态
                            sPortSr.WriteLine("@35EB:");
                            //Debug.WriteLine("传感器错误数据： @035EB.");
                            break;
                        default:
                            // 默认，正常工作状态
                            if (dataRev.Contains("R"))
                            {
                                // 上位机读取数据
                                float temp = 12.0f;
                                dataRev += temp.ToString("0.000");
                                dataRev += ":";
                                sPortSr.WriteLine(dataRev);
                            }
                            else
                            {
                                // 未知指令
                                // 指令不存在
                                sPortSr.WriteLine("@35EB:");
                            }
                            //Debug.WriteLine("传感器设备返回了数据：" + dataRev.ToString());
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("SPortSr_DataReceived exception when writeData : " + ex.Message);

                    // 出现代码错误
                    srCodeErr = true;
                }

                // 如果错误不持续，则清空错误标记为
                if (!srErrLast && srErrStatus != SrStatus.OK)
                    this.BeginInvoke(new EventHandler(delegate { srErrStatus = SrStatus.OK; this.comboBox_SrStatus.SelectedIndex = 0; }));
            }
        }


        /// <summary>
        /// 继电器 - 信息收发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SPortRy_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] dataRev = new byte[8];
            int len = 8;

            try
            {
                len = sPortRy.BytesToRead;
                if (len != 8)
                {
                    Debug.WriteLine("接收到了错误数据，长度不为 8 ");
                    return;
                }
                sPortRy.Read(dataRev, 0, len);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("SPortRy_DataReceived exception when readData : " + ex.Message);

                // 出现代码错误
                ryCodeErr = true;
            }
            

            // 读取到数据后，根据所模拟的继电器状态，返回相应的继电器模拟数据
            lock(ryLocker)
            {
                // 调试信息输出
                Debug.WriteLine("接收到传感器数据： " + BitConverter.ToString(dataRev) + " 设备状态：" + ryErrStatus.ToString());

                try
                {
                    switch (ryErrStatus)
                    {
                        case RyStatus.OK:
                            // 将原始数据返回，即为正确数据
                            sPortRy.Write(dataRev, 0, len);
                            break;
                        case RyStatus.DisConnected:
                            // 不返回任何数据，即模拟连接断开
                            break;
                        case RyStatus.DataErr:
                            sPortRy.WriteLine("@35EB:");
                            Debug.WriteLine("继电器错误数据： @035EB.");
                            break;
                        default:
                            // 默认状态下，为无错误状态
                            sPortRy.Write(dataRev, 0, len);
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("SPortRy_DataReceived exception when writeData : " + ex.Message);

                    // 出现代码错误
                    ryCodeErr = true;
                }

                // 如果错误不持续，则清空错误标记为
                if (!ryErrLast && ryErrStatus != RyStatus.OK)
                    this.BeginInvoke(new EventHandler(delegate { ryErrStatus = RyStatus.OK; this.comboBox_RyStatus.SelectedIndex = 0; }));
            }
            
        }


        /// <summary>
        /// 辅槽控温 - 信息收发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SPortTs_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string dataRev = string.Empty;

            try
            {
                dataRev = sPortTs.ReadTo(":");
                sPortTs.DiscardInBuffer();
                //Debug.WriteLine("辅槽设备读取到了数据：" + dataRev.ToString());
            }
            catch(Exception ex)
            {
                // 端口接收数据时发生错误
                Debug.WriteLine("SPortTs_DataReceived Exception when readData : " + ex.Message);
                tsCodeErr = true;
            }
            
            lock(tsLocker)
            {
                // 调试信息输出
                //Debug.WriteLine("辅槽设备读取到了数据：" + dataRev + " 设备状态：" + tsErrStatus.ToString());

                try
                {
                    switch(tsErrStatus)
                    {
                        case TempStatus.OK:
                            processDataTs(dataRev);
                            break;

                        case TempStatus.DisConnected:
                            // 断开状态，不返回任何数据
                            break;

                        case TempStatus.DataErr:
                            // 返回错误数据
                            sPortTs.WriteLine("@35EB:");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // 端口接收数据时发生错误
                    Debug.WriteLine("SPortTs_DataReceived Exception when writeData : " + ex.Message);
                    tsCodeErr = true;
                }

                // 如果错误不持续，则清空错误标记为
                if (!tsErrLast && tsErrStatus != TempStatus.OK) this.BeginInvoke(new EventHandler(delegate { tsErrStatus = TempStatus.OK; }));
            }
            return;
        }

        void processDataTs(string dataRev)
        {
            // 先判断温度是否已经稳定
            if (Math.Abs(tsParam.CurTemp - tsParam.SetTemp) * 2 < Math.Abs(tsParam.ChangeRate + tsParam.ChangeRatePlus)) tsParam.Steady = true;
            else tsParam.Steady = false;

            if (dataRev.Contains("W"))
            {
                // 上位机写入数据 - 一般是参数

                // 从上位机数据中提取正确数据 - 如 温度设定点等
                // wghou
                if(dataRev[4] == 'A')
                {
                    // 温度设定值
                    float val = 18.0f;
                    if(!float.TryParse(dataRev.Substring(5),out val))
                    {
                        // 发生了错误
                        sPortTs.WriteLine("@35EB:");
                        return;
                    }

                    Debug.WriteLine("辅槽更新了温度设定值：" + val.ToString());
                    // 更新温度设定值
                    tsParam.SetTemp = val;
                    this.BeginInvoke(new EventHandler(delegate { this.label_SetTempS.Text = val.ToString("0.000"); }));
                }

                // 向上位机返回正确数据
                dataRev = dataRev.Substring(0, 5) + ":";
                sPortTs.WriteLine(dataRev);
            }
            else if (dataRev.Contains("R"))
            {
                // 上位机读取数据
                if(dataRev[4] == 'H')
                {
                    // 读取温度显示值
                    // 计算出当前的温度显示值

                    // 如果未稳定，则使温度发生一个变化
                    if(!tsParam.Steady) tsParam.CurTemp += (tsParam.ChangeRate + tsParam.ChangeRatePlus) * Math.Sign(tsParam.SetTemp - tsParam.CurTemp);

                    // 如果改变后的温度值接近温度设定点，则设当前温度点为温度设定值
                    if(Math.Abs(tsParam.CurTemp - tsParam.SetTemp) <= Math.Abs((tsParam.ChangeRate + tsParam.ChangeRatePlus)/2))
                    {
                        tsParam.CurTemp = tsParam.SetTemp;
                        tsParam.Steady = true;
                    }

                    // 计算波动度
                    tsParam.PhaseCount++;
                    if (tsParam.PhaseCount >= 20) tsParam.PhaseCount = 0;

                    float fluc = (tsParam.Fluc + tsParam.FlucPlus) * (float)Math.Sin(3.1415 * tsParam.PhaseCount / 10) / 2;

                    // 如果稳定，则波动度为零
                    //if (tsParam.Steady) fluc = 0;

                    // 最终的显示值 = 当前温度值 + 附加温度值 + 计算波动度 + 附加波动度
                    float val = tsParam.CurTemp + tsParam.CurTempPlus + fluc;

                    // 界面显示的当前温度值为 curTemp + 波动度
                    this.BeginInvoke(new EventHandler(delegate { this.label_CurTempS.Text = val.ToString("0.000"); }));

                    dataRev += val.ToString("0.000");
                    dataRev += ":";
                    sPortTs.WriteLine(dataRev);
                }
                else
                {
                    dataRev += "12.000";
                    dataRev += ":";
                    sPortTs.WriteLine(dataRev);
                }
                
            }
            else
            {
                // 未知指令
                // 指令不存在
                sPortTs.WriteLine("@35EB:");
            }
        }


        /// <summary>
        /// 主槽控温 - 信息收发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SPortTm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string dataRev = string.Empty;

            try
            {
                dataRev = sPortTm.ReadTo(":");
                sPortTm.DiscardInBuffer();
                //Debug.WriteLine("主槽设备读取到了数据：" + dataRev.ToString());
            }
            catch (Exception ex)
            {
                // 端口接收数据时发生错误
                Debug.WriteLine("SPortTm_DataReceived Exception when readData : " + ex.Message);
                tmCodeErr = true;
            }

            lock (tmLocker)
            {
                // 调试信息输出
                //Debug.WriteLine("主槽设备读取到了数据：" + dataRev + " 设备状态：" + tmErrStatus.ToString());

                try
                {
                    switch (tmErrStatus)
                    {
                        case TempStatus.OK:
                            processDataTm(dataRev);
                            break;

                        case TempStatus.DisConnected:
                            // 断开状态，不返回任何数据
                            break;

                        case TempStatus.DataErr:
                            // 返回错误数据
                            sPortTm.WriteLine("@35EB:");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // 端口接收数据时发生错误
                    Debug.WriteLine("SPortTm_DataReceived Exception when writeData : " + ex.Message);
                    tmCodeErr = true;
                }

                // 如果错误不持续，则清空错误标记为
                if (!tmErrLast) tmErrStatus = TempStatus.OK;
            }
            return;
        }

        void processDataTm(string dataRev)
        {
            // 先判断温度是否已经稳定
            if (Math.Abs(tmParam.CurTemp - tmParam.SetTemp) * 2.0f < Math.Abs(tmParam.ChangeRate + tmParam.ChangeRatePlus)) tmParam.Steady = true;
            else tmParam.Steady = false;

            if (dataRev.Contains("W"))
            {
                // 上位机写入数据 - 一般是参数

                // 从上位机数据中提取正确数据 - 如 温度设定点等
                // wghou
                if (dataRev[4] == 'A')
                {
                    // 温度设定值
                    float val = 18.0f;
                    if (!float.TryParse(dataRev.Substring(5), out val))
                    {
                        // 发生了错误
                        sPortTm.WriteLine("@35EB:");
                        return;
                    }

                    Debug.WriteLine("主槽更新了温度设定值：" + val.ToString());
                    // 更新温度设定值
                    tmParam.SetTemp = val;

                    // 更新界面显示值
                    this.BeginInvoke(new EventHandler(delegate { this.label_SetTempM.Text = val.ToString("0.0000"); }));
                }

                // 向上位机返回正确数据
                dataRev = dataRev.Substring(0, 5) + ":";
                sPortTm.WriteLine(dataRev);
            }
            else if (dataRev.Contains("R"))
            {
                // 上位机读取数据
                if (dataRev[4] == 'H')
                {
                    // 读取温度显示值
                    // 计算出当前的温度显示值

                    // 如果未稳定，则使温度发生一个变化
                    if (!tmParam.Steady) tmParam.CurTemp += (tmParam.ChangeRate + tmParam.ChangeRatePlus) * Math.Sign(tmParam.SetTemp - tmParam.CurTemp);

                    // 如果改变后的温度值接近温度设定点，则设当前温度点为温度设定值
                    if (Math.Abs(tmParam.CurTemp - tmParam.SetTemp) * 2.0f <= Math.Abs(tmParam.ChangeRate + tmParam.ChangeRatePlus))
                    {
                        tmParam.CurTemp = tmParam.SetTemp;
                        tmParam.Steady = true;
                    }

                    // 计算波动度
                    tmParam.PhaseCount++;
                    if (tmParam.PhaseCount >= 30) tmParam.PhaseCount = 0;

                    float fluc = (tmParam.Fluc + tmParam.FlucPlus) * (float)Math.Sin(3.1415 * tmParam.PhaseCount / 15) / 2;

                    // 如果稳定，则波动度为零
                    //if (tmParam.Steady) fluc = 0;

                    // 最终的显示值 = 当前温度值 + 附加温度值 + 计算波动度 + 附加波动度
                    float val = tmParam.CurTemp + tmParam.CurTempPlus + fluc;

                    // 界面显示值 - 当前温度值 = curTemp + 波动度
                    this.BeginInvoke(new EventHandler(delegate { this.label_CurTempM.Text = val.ToString("0.0000"); }));

                    dataRev += val.ToString("0.0000");
                    dataRev += ":";
                    sPortTm.WriteLine(dataRev);
                }
                else
                {
                    dataRev += "12.0000";
                    dataRev += ":";
                    sPortTm.WriteLine(dataRev);
                }

            }
            else
            {
                // 未知指令
                // 指令不存在
                sPortTm.WriteLine("@35EB:");
            }
        }
    }
}
