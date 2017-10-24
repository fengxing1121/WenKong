using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ComTest
{
    class Program
    {
        public static SerialPort sPortM = new SerialPort()
        {
            PortName = "COM3",
            BaudRate = 2400,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = Parity.None,
            ReadBufferSize = 64,
            WriteBufferSize = 64
        };

        public static SerialPort sPortS = new SerialPort()
        {
            PortName = "COM5",
            BaudRate = 2400,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = Parity.None,
            ReadBufferSize = 64,
            WriteBufferSize = 64
        };
        public static SerialPort sPortR = new SerialPort()
        {
            PortName = "COM7",
            BaudRate = 9600,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = Parity.None,
            ReadBufferSize = 64,
            WriteBufferSize = 64
        };

        static void Main(string[] args)
        {
            

            sPortM.Open();
            sPortS.Open();
            sPortR.Open();
            sPortM.DataReceived += SPortM_DataReceived;
            sPortS.DataReceived += SPortS_DataReceived;
            sPortR.DataReceived += SPortR_DataReceived;

            while(true)
            {
                ConsoleKeyInfo kinfo = Console.ReadKey(true);
                if (kinfo.Key == ConsoleKey.Q)
                    break;
            }
            sPortM.Close();
            sPortS.Close();
            sPortR.Close();
            return;
        }

        // 继电器设备数据交换
        private static void SPortR_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Console.WriteLine("继电器设备...");
            byte[] dataRev = new byte[8];
            int len = sPortR.BytesToRead;
            if (len != 8)
            {
                Console.WriteLine("接收到了错误数据，长度不为 8 ");
                return;
            }
            sPortR.Read(dataRev, 0, len);
            Console.WriteLine("继电器设备读到了数据：" + BitConverter.ToString(dataRev));
            sPortR.Write(dataRev, 0, len);
        }

        // 辅槽控温设备数据交换
        private static void SPortS_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Console.WriteLine("辅槽控温...");
            string dataRev = string.Empty;
            dataRev = sPortS.ReadTo(":");
            sPortS.DiscardInBuffer();
            //Console.WriteLine("辅槽控温设备读到了数据：" + dataRev.ToString());

            if(dataRev.Contains("W"))
            {
                // 上位机写入数据
                Console.WriteLine(dataRev);
                dataRev = dataRev.Substring(0, 5) + ":";
                sPortS.WriteLine(dataRev);
                Console.WriteLine(dataRev);
            }
            else if(dataRev.Contains("R"))
            {
                // 上位机读取数据
                long tick = DateTime.Now.Ticks;
                Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                float temp = 12.0f * (float)ran.NextDouble();
                dataRev += temp.ToString("0.000");
                dataRev += ":";
                sPortS.WriteLine(dataRev);
            }
            else
            {
                // 未知指令
                // 指令不存在
                sPortS.WriteLine("@35EB:");
            }
            
            //Console.WriteLine(dataRev);
        }

        // 主槽控温设备数据交换
        private static void SPortM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Console.WriteLine("辅槽控温...");
            string dataRev = string.Empty;
            dataRev = sPortM.ReadTo(":");
            sPortM.DiscardInBuffer();
            //Console.WriteLine("辅槽控温设备读到了数据：" + dataRev.ToString());

            if (dataRev.Contains("W"))
            {
                // 上位机写入数据
                //Console.WriteLine(dataRev);
                dataRev = dataRev.Substring(0, 5) + ":";
                sPortM.WriteLine(dataRev);
            }
            else if (dataRev.Contains("R"))
            {
                // 上位机读取数据
                long tick = DateTime.Now.Ticks;
                Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                float temp = 12.0f * (float)ran.NextDouble();
                dataRev += temp.ToString("0.000");
                dataRev += ":";
                sPortM.WriteLine(dataRev);
            }
            else
            {
                // 未知指令
                // 指令不存在
                sPortM.WriteLine("@35EB:");
            }

            //Console.WriteLine(dataRev);
        }
    }
}
