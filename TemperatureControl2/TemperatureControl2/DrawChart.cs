using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TemperatureControl2
{
    class DrawChart : IDisposable
    {
        #region Members

        // 数据来源
        public Device.TempDevice tpDevice;

        // Value
        private float max = 0;
        private float min = 0;

        // Size parameters
        // Improve: Is there any redundancy for these parameters?
        private int height;
        private int width;
        private int colNum;
        private int rowNum;
        private float colInterval;
        private float rowInterval;
        private float startVer;
        private float endVer;
        private float startHor;
        private float endHor;
        //private const float spaceLeft = 105;
        private const float spaceLeft = 79;
        private const float spaceRight = 25;
        private const float spaceTop = 25;
        private const float spaceBottom = 25;
        private const float startText = 30;

        // Color parameters
        private Color backColor = Color.Black;
        private Color axisColor = Color.DarkGreen;
        private Color textColor = Color.Yellow;
        private Color lineColor = Color.Red;

        // Draw tools
        private Bitmap mBmp;
        private Pen mLinePen;   // For line
        private Pen mAxisPen;   // For axis
        private Brush mBrush;   // For text
        private Graphics mGhp;

        // Time line
        private const int timeColInt = 2;           // Time interval to tag time on x-Axis
        private const int tempChartFixLen = 601;    // Count of point used in chart, 
                                                    // 661 is suitable for 800*? chart
                                                    // Use for saving temperature data only for chart drawing
        private List<float> tempListForChart;// 所有要绘制的温度数据

        private int digits;     // 数据分辨率
        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="height">Height of chart</param>
        /// <param name="width">width of chart</param>
        /// <param name="colNum">Column number of chart</param>
        /// <param name="rowNum">Row number of chart</param>
        public DrawChart(Device.TempDevice dev, int height, int width, int colNum, int rowNum)
        {
            tpDevice = dev;

            this.height = height;
            this.width = width;
            this.colNum = colNum;
            this.rowNum = rowNum;

            CalcSize();
            PutOnColor();

            mBmp = new Bitmap(this.width, this.height);
            mGhp = Graphics.FromImage(mBmp);

            if (tpDevice.tpDeviceName == "主槽控温设备")
                digits = 4;
            else if (tpDevice.tpDeviceName == "辅槽控温设备")
                digits = 3;
            else
                digits = 4;     // 默认 4 位有效数字

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Calculate size of chart
        /// </summary>
        private void CalcSize()
        {
            float spaceVertical = height - spaceTop - spaceBottom;
            float spaceHorizontal = width - spaceLeft - spaceRight;

            rowInterval = spaceVertical / rowNum;
            colInterval = spaceHorizontal / colNum;

            startVer = spaceTop;
            endVer = height - spaceBottom;
            startHor = spaceLeft;
            endHor = width - spaceRight;
        }

        /// <summary>
        /// Put color on draw tools
        /// </summary>
        private void PutOnColor()
        {
            mBrush = new SolidBrush(textColor);
            mLinePen = new Pen(lineColor);
            mAxisPen = new Pen(axisColor);
        }


        /// <summary>
        /// Move temperature data from global list to local list 
        /// Restrict the length for chart animatino effect
        /// Calculate the max and min of temperature
        /// </summary>
        private void MoveTempLocal()
        {
            lock(tpDevice.tpShowLocker)
            {
                // 将温度数据从 Device.TempDevice.temperatures 中读取到 tempListForChart 中
                if (tpDevice.temperaturesShow.Count < tempChartFixLen)
                {
                    tempListForChart = tpDevice.temperaturesShow.GetRange(0, tpDevice.temperaturesShow.Count);
                }
                else
                {
                    tempListForChart = tpDevice.temperaturesShow.GetRange
                        (tpDevice.temperaturesShow.Count - tempChartFixLen, tempChartFixLen);
                }
            }

            // 计算温度值大小的范围
            // Calculate the Max and Min
            if (tempListForChart.Count > 0)
            {
                max = tempListForChart.Max();
                min = tempListForChart.Min();

                // 为了保证每格的最小分辨率为0.001,要处理一下
                // wghou -> 分辨率改为 0.0001
                if(digits == 3)
                {
                    max = (float)Math.Round(max, 3);
                    min = (float)Math.Round(min, 3);

                    //if (max - min <= 0.001f * rowNum)
                    //{
                    //    float margin = max - min;
                    //    //max = (float)Math.Round(max + (0.001f * rowNum - margin) / 2, rowNum / 2);
                    //    max = (float)Math.Round(min + (max - min) / 2 + (0.001f * rowNum) / 2, 3);
                    //    min = max - 0.001f * rowNum;
                    //}

                    // 总的幅度（范围）是 0.001*rowNum 的整数倍
                    float marg = max - min;
                    //int tms = (int)((marg + 0.001f * (rowNum - 1)) / (0.001f * rowNum));
                    int tms = (int)Math.Ceiling(marg / (0.001f * rowNum));
                    if (tms == 0) tms = 1;
                    max = (float)Math.Round(min + marg/2 + tms*0.001f*rowNum/2, 3);
                    min = (float)(max - tms * 0.001f * rowNum);
                }
                else
                {
                    max = (float)Math.Round(max, 4);
                    min = (float)Math.Round(min, 4);

                    //if (max - min <= 0.0001f * rowNum)
                    //{
                    //    float margin = max - min;
                    //    max = (float)Math.Round(max + (0.0001f * rowNum - margin) / 2, rowNum / 2);
                    //    //max = (float)Math.Round((max - min) / 2 + (0.0001f * rowNum) / 2, 3);
                    //    min = max - 0.0001f * rowNum;
                    //}

                    // 总的幅度（范围）是 0.001*rowNum 的整数倍
                    float marg = max - min;
                    //int tms = (int)((marg + 0.001f * (rowNum - 1)) / (0.001f * rowNum));
                    int tms = (int)Math.Ceiling(marg / (0.0001f * rowNum));
                    if (tms == 0) tms = 1;
                    max = (float)Math.Round(min + marg / 2 + tms * 0.0001f * rowNum / 2, 4);
                    min = (float)(max - tms * 0.0001f * rowNum);
                }
            }
        }

        /// <summary>
        /// Calculate all time tag for chart
        /// </summary>
        /// <returns>Time tag list(hour, minute)</returns>
        private List<int[]> CalcTimeTags()
        {
            List<int[]> timeTags = new List<int[]>();

            int minuteInterval = timeColInt * tpDevice.readTempIntervalSec;

            DateTime dt = DateTime.Now;
            int hour = dt.Hour;
            int minute = (int)Math.Round(dt.Minute + (float)dt.Second / 60.0     // 60.0 means 1min = 60s
                - (tempListForChart.Count * tpDevice.readTempIntervalSec / 60.0) - minuteInterval);

            // Avoid a minus minute
            if (minute < 0)
            {
                hour--;
                if (hour < 0)
                    hour = hour + 24;       // 24 means 1day = 24hours
                minute = minute + 60;         // 60 means 1hour = 60 minutes
            }

            // Calculate all time tags
            //for (int i = 0; i < (colNum + 1) / timeColInt; i++)
            for (int i = 0; i < colNum / timeColInt + 1; i++)
            {
                minute = minute + minuteInterval;
                if (minute >= 60)
                {
                    hour++;
                    if (hour >= 24)
                        hour = hour - 24;
                    minute = minute - 60;
                }
                timeTags.Add(new int[] { hour, minute });
            }

            return timeTags;
        }

        #endregion

        #region Pulic Methods
        /// <summary>
        /// Draw chart on bitmap
        /// </summary>
        /// <param name="max">Max value of Y</param>
        /// <param name="min">Min value of Y</param>
        /// <returns></returns>
        public Bitmap Draw()
        {
            // 将温度值读取到了 DrawChart.tempListForChart 中
            MoveTempLocal();

            float mid = (max + min) / 2;
            float margin = max - min;
            float midVer = (startVer + endVer) / 2;
            float spaceVer = height - spaceTop - spaceBottom;
            float spaceHor = width - spaceLeft - spaceRight;
            Font mFont = new Font("微软雅黑", 10, FontStyle.Regular);

            mGhp.Clear(backColor);

            #region draw xy axis
            // Main Axis
            mAxisPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            mGhp.DrawLine(mAxisPen, startHor, startVer, startHor, endVer);  // Vertiacal
            mGhp.DrawLine(mAxisPen, startHor, endVer, endHor, endVer);      // Horizontal

            // Sub Axis
            mAxisPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            // Vertical
            for (int i = 1; i <= colNum; i++)
                mGhp.DrawLine(mAxisPen, startHor + i * colInterval, startVer, startHor + i * colInterval, endVer);
            // Horizontal
            for (int i = 0; i < rowNum; i++)
                mGhp.DrawLine(mAxisPen, startHor, startVer + i * rowInterval, endHor, startVer + i * rowInterval);

            // 绘制垂直方向温度的刻度值
            // Vertical text
#if false
            for (int i = 0; i < rowNum + 1; i++)
            {
                mGhp.DrawString((max - i * margin / rowNum).ToString("0.000"),
                    mFont, mBrush, startText, startVer + rowInterval * i - 8);
            }
#endif
            // wghou -> 分辨率改为 0.0001
            if(digits==3)
            {
                float max3f = (float)Math.Round(max, digits);
                float step3f = (float)Math.Round(margin / rowNum, digits);
                for (int i = 0; i < rowNum + 1; i++)
                {
                    // wghou -> 分辨率改为 0.0001
                    // 默认 4 位
                    mGhp.DrawString((max3f - i * step3f).ToString("0.000"),
                    mFont, mBrush, startText, startVer + rowInterval * i - 8);
                }
            }
            else
            {
                float max3f = (float)Math.Round(max, digits);
                float step3f = (float)Math.Round(margin / rowNum, digits);
                for (int i = 0; i < rowNum + 1; i++)
                {
                    // wghou -> 分辨率改为 0.0001
                    // 默认 4 位
                    mGhp.DrawString((max3f - i * step3f).ToString("0.0000"),
                    mFont, mBrush, startText, startVer + rowInterval * i - 8);
                }
            }


            
            #endregion

            // 将温度数据绘制到图表中
            #region use data to draw chart
#if false
            for (int i = 0; i < tempListForChart.Count - 1; i++)
            {
                mGhp.DrawLine(mLinePen, startHor + i, startVer + (tempListForChart[i] - min) / margin * spaceVer,
                    startHor + (i + 1), startVer + (tempListForChart[i + 1] - min) / margin * spaceVer);
            }
#endif
            float interval = spaceHor / (tempChartFixLen -1 );
            // wghou 20180109
            // 关于温度分辨率的调整
#if false
            for (int i = 0; i < tempListForChart.Count - 1; i++)
            {
                mGhp.DrawLine(mLinePen, startHor + i * interval, endVer - (tempListForChart[i] - min) / margin * spaceVer,
                    startHor + (i + 1) * interval, endVer - (tempListForChart[i + 1] - min) / margin * spaceVer);
            }
#else
            float temperatureFirst = 0.0f, temperatureNext = 0.0f;
            for (int i = 0; i < tempListForChart.Count - 1; i++)
            {
                temperatureFirst = (float)Math.Round(tempListForChart[i], digits);
                temperatureNext = (float)Math.Round(tempListForChart[i +1], digits);
                mGhp.DrawLine(mLinePen, startHor + i * interval, endVer - (temperatureFirst - min) / margin * spaceVer,
                    startHor + (i + 1) * interval, endVer - (temperatureNext - min) / margin * spaceVer);
            }
#endif
            #endregion

            #region tag time to x axis
            List<int[]> timeTags = CalcTimeTags();
            // Draw all time tags
            for (int i = 0; i < timeTags.Count; i++)
            {
                mGhp.DrawString(String.Format("{0:D2}:{1:D2}", timeTags[i][0], timeTags[i][1]),
                    mFont, mBrush, startHor + i * timeColInt * colInterval - 15, endVer + 10);
            }
#endregion

            return mBmp;
        }

        /// <summary>
        /// Dispose native resourses
        /// </summary>
        public void Dispose()
        {
            if (mBmp != null)
                mBmp.Dispose();
            if (mLinePen != null)
                mLinePen.Dispose();
            if (mAxisPen != null)
                mAxisPen.Dispose();
            if (mBrush != null)
                mBrush.Dispose();
            if (mGhp != null)
                mGhp.Dispose();
        }
#endregion
    }
}
