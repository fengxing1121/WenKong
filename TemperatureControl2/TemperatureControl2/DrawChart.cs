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
        private const float spaceLeft = 105;
        private const float spaceRight = 35;
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
        private const int timeColInt = 3;           // Time interval to tag time on x-Axis
        private const int tempChartFixLen = 661;    // Count of point used in chart, 
                                                    // 661 is suitable for 800*? chart
                                                    // Use for saving temperature data only for chart drawing
        private List<float> tempListForChart;
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
            if (tpDevice.temperatures.Count < tempChartFixLen)
            {
                tempListForChart = tpDevice.temperatures.GetRange(0, tpDevice.temperatures.Count);
            }
            else
            {
                tempListForChart = tpDevice.temperatures.GetRange
                    (tpDevice.temperatures.Count - tempChartFixLen, tempChartFixLen);
            }

            // Calculate the Max and Min
            if (tempListForChart.Count > 0)
            {
                max = tempListForChart.Max();
                min = tempListForChart.Min();

                // 为了保证每格的最小分辨率为0.001,要处理一下
                max = (float)Math.Round(max, 3);
                min = (float)Math.Round(min, 3);
                if (max - min <= 0.001 * rowNum)
                {
                    float margin = max - min;
                    max = (float)Math.Round(max + (0.001 * rowNum - margin) / 2, rowNum / 2);
                    min = max - 0.001f * rowNum;
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

            int minuteInterval = timeColInt * tpDevice.readTempInterval / 1000;

            DateTime dt = DateTime.Now;
            int hour = dt.Hour;
            int minute = (int)Math.Round(dt.Minute + (float)dt.Second / 60.0     // 60.0 means 1min = 60s
                - (tempListForChart.Count * tpDevice.readTempInterval / 1000 / 60.0) - minuteInterval);

            // Avoid a minus minute
            if (minute < 0)
            {
                hour--;
                if (hour < 0)
                    hour = hour + 24;       // 24 means 1day = 24hours
                minute = minute + 60;         // 60 means 1hour = 60 minutes
            }

            // Calculate all time tags
            for (int i = 0; i < (colNum + 1) / timeColInt; i++)
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
            MoveTempLocal();

            float mid = (max + min) / 2;
            float margin = max - min;
            float midVer = (startVer + endVer) / 2;
            float spaceVer = height - spaceTop - spaceBottom;
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
            // Vertical text
            for (int i = 0; i < rowNum + 1; i++)
            {
                mGhp.DrawString((max - i * margin / rowNum).ToString("0.000"),
                    mFont, mBrush, startText, startVer + rowInterval * i - 8);
            }
            #endregion

            #region use data to draw chart
            for (int i = 0; i < tempListForChart.Count - 1; i++)
            {
                mGhp.DrawLine(mLinePen, startHor + i, startVer + (tempListForChart[i] - min) / margin * spaceVer,
                    startHor + (i + 1), startVer + (tempListForChart[i + 1] - min) / margin * spaceVer);
            }
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
