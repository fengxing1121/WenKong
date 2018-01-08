using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TemperatureControl2
{
    public partial class FormSelfCheck : Form
    {
        Device.Devices deviceAll;
        PictureBox[] picBox = new PictureBox[9];

        Bitmap mBmpGray;
        Bitmap mBmpGreen;
        public FormSelfCheck(Device.Devices dev)
        {
            InitializeComponent();
            mBmpGray = new Bitmap(pictureBox1.Width, pictureBox1.Width);
            mBmpGreen = new Bitmap(pictureBox1.Width, pictureBox1.Width);
            deviceAll = dev;
            picBox[0] = pictureBox1;
            picBox[1] = pictureBox2;
            picBox[2] = pictureBox3;
            picBox[3] = pictureBox4;
            picBox[4] = pictureBox5;
            picBox[5] = pictureBox6;
            picBox[6] = pictureBox7;
            picBox[7] = pictureBox8;
            picBox[8] = pictureBox9;
            
            Graphics mGhp = Graphics.FromImage(mBmpGray);
            mGhp.Clear(Color.Gray);
            for(int i = 0;i<9;i++)
                this.picBox[i].Image = mBmpGray;
        }

        private void FormSelfCheck_Shown(object sender, EventArgs e)
        {
            this.backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            timer1.Interval = 200;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
            label2.Text = "控温设备自检中...";
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            if (deviceAll.tpDeviceM.SelfCheck() != Device.TempProtocol.Err_t.NoError)
            {
                // 主槽控温设备自检失败
                MessageBox.Show("主槽控温设备自检失败！");
                goto ExitErr;
            }

            if (deviceAll.tpDeviceS.SelfCheck() != Device.TempProtocol.Err_t.NoError)
            {
                // 辅槽控温设备自检失败
                MessageBox.Show("辅槽控温设备自检失败！");
                goto ExitErr;
            }

            goto ExitOK;

            this.BeginInvoke(new EventHandler(delegate
            {
                label2.Text = "继电器设备自检中...";
            }));

            for(int cmd = 0;cmd<9;cmd++)
            {
                Device.RelayProtocol.Err_r err = deviceAll.ryDevice.SelfCheckOneByOne((Device.RelayProtocol.Cmd_r)cmd, true);
                if (err == Device.RelayProtocol.Err_r.NoError)
                {
                    // 自检成功
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        Graphics mGhp = Graphics.FromImage(mBmpGreen);
                        mGhp.Clear(Color.Green);
                        this.picBox[(int)cmd].Image = mBmpGreen;
                    }));
                }
                else
                {
                    // 继电器设备自检失败
                    MessageBox.Show("继电器设备自检失败！");
                    goto ExitErr;
                }
                Thread.Sleep(1000);
            }

            for (int cmd = 8; cmd >= 0; cmd--)
            {
                Device.RelayProtocol.Err_r err = deviceAll.ryDevice.SelfCheckOneByOne((Device.RelayProtocol.Cmd_r)cmd, false);
                if (err == Device.RelayProtocol.Err_r.NoError)
                {
                    // 自检成功
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        Graphics mGhp = Graphics.FromImage(mBmpGray);
                        mGhp.Clear(Color.Gray);
                        this.picBox[(int)cmd].Image = mBmpGray;
                    }));
                }
                else
                {
                    // 继电器设备自检失败
                    MessageBox.Show("继电器设备自检失败！");
                    goto ExitErr;
                }
                Thread.Sleep(1000);
            }


            this.BeginInvoke(new EventHandler(delegate
            {
                label2.Text = "传感器设备自检中...";
            }));

            if (deviceAll.srDevice.SelfCheck() != true)
            {
                // 传感器设备自检失败
                MessageBox.Show("传感器设备自检失败！");
                goto ExitErr;
            }

            ExitOK:
            this.BeginInvoke(new EventHandler(delegate
            {
                label2.Text = "设备自检成功！";
            }));

            Thread.Sleep(1000);
            this.BeginInvoke(new EventHandler(delegate
            {
                this.DialogResult = DialogResult.OK;
            }));
            return;

            ExitErr:
            this.BeginInvoke(new EventHandler(delegate
            {
                this.Close();
            }));
            return;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            this.backgroundWorker1.RunWorkerAsync();

        }

    }
}
