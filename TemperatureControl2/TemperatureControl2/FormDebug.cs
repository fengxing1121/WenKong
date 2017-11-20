using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TemperatureControl2
{
    public partial class FormDebug : Form
    {
        private Device.Devices devicesAll;

        private TextBox[] devParam = new TextBox[11];
        private TextBox tx = null;

        public FormDebug(Device.Devices devAll)
        {
            InitializeComponent();
            devicesAll = devAll;
            devParam[0] = textBox1;
            devParam[1] = textBox2;
            devParam[2] = textBox3;
            devParam[3] = textBox4;
            devParam[4] = textBox5;
            devParam[5] = textBox6;
            devParam[6] = textBox7;
            devParam[7] = textBox8;
            devParam[8] = textBox9;
            devParam[9] = textBox10;
            devParam[10] = textBox11;
            
            //comboBox1.Items
        }
        private void FormDebug_Load(object sender, EventArgs e)
        {
            textBox1.Text = devicesAll.steadyTimeSec.ToString("0");
            textBox2.Text = devicesAll.bridgeSteadyTimeSec.ToString("0");
            textBox3.Text = devicesAll.flucValue.ToString("0.0000");
            textBox4.Text = devicesAll.controlTempThr.ToString("0.0000");
            textBox5.Text = devicesAll.tempNotUpOrDownFaultTimeSec.ToString("0");
            textBox6.Text = devicesAll.tempNotUpOrDwonFaultThr.ToString("0.0000");
            textBox7.Text = devicesAll.flucFaultTimeSec.ToString("0");
            textBox8.Text = devicesAll.flucFaultThr.ToString("0.0000");
            textBox9.Text = devicesAll.tempBiasFaultThr.ToString("0.0000");
            textBox10.Text = devicesAll.tempMaxValue.ToString("0.0000");
            textBox11.Text = devicesAll.tempMinValue.ToString("0.0000");

            if(devicesAll.sort == "ascend")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }


        private void BntUpdate_Click(object sender, EventArgs e)
        {
            float[] paramCache = new float[11];
            // 设置温控设备参数
            for (int i = 0; i < 11; i++)
            {
                float newVal = 0.0f;

                if (float.TryParse(this.devParam[i].Text, out newVal) != true)
                {
                    // 参数数据格式错误哦
                    MessageBox.Show("参数 " + devParam[i].Text + " 格式错误!");
                    return;
                }

                // 将参数写入参数设置缓存
                paramCache[i] = newVal;
            }

            devicesAll.steadyTimeSec = (int)paramCache[0];
            devicesAll.bridgeSteadyTimeSec = (int)paramCache[1];
            devicesAll.flucValue = paramCache[2];
            devicesAll.controlTempThr = paramCache[3];
            devicesAll.tempNotUpOrDownFaultTimeSec = (int)paramCache[4];
            devicesAll.tempNotUpOrDwonFaultThr = paramCache[5];
            devicesAll.flucFaultTimeSec = (int)paramCache[6];
            devicesAll.flucFaultThr = paramCache[7];
            devicesAll.tempBiasFaultThr = paramCache[8];
            devicesAll.tempMaxValue = paramCache[9];
            devicesAll.tempMinValue = paramCache[10];

            if(comboBox1.SelectedIndex == 0)
            {
                devicesAll.sort = "ascend";
            }
            else
            {
                devicesAll.sort = "descend";
            }

            // 写入到文本中
            // 相关参数
            string configFilePath = @"./config.ini";
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "SteadyTimeSec", devicesAll.steadyTimeSec.ToString("0"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "bridgeSteadyTimeSec", devicesAll.bridgeSteadyTimeSec.ToString("0"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "FlucValue", devicesAll.flucValue.ToString("0.0000"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "controlTempThr", devicesAll.controlTempThr.ToString("0.0000"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempNotUpOrDownFaultTimeSec", devicesAll.tempNotUpOrDownFaultTimeSec.ToString("0"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempNotUpOrDwonFaultThr", devicesAll.tempNotUpOrDwonFaultThr.ToString("0.0000"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "flucFaultTimeSec", devicesAll.flucFaultTimeSec.ToString("0"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "flucFaultThr", devicesAll.flucFaultThr.ToString("0.0000"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempBiasFaultThr", devicesAll.tempBiasFaultThr.ToString("0.0000"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempMaxValue", devicesAll.tempMaxValue.ToString("0.0000"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Paramters", "tempMinValue", devicesAll.tempMinValue.ToString("0.0000"));
            Utils.IniReadWrite.INIWriteValue(configFilePath, "Ohters", "sort", devicesAll.sort);
        }

        private void BntRead_Click(object sender, EventArgs e)
        {
            textBox1.Text = devicesAll.steadyTimeSec.ToString("0");
            textBox2.Text = devicesAll.bridgeSteadyTimeSec.ToString("0");
            textBox3.Text = devicesAll.flucValue.ToString("0.0000");
            textBox4.Text = devicesAll.controlTempThr.ToString("0.0000");
            textBox5.Text = devicesAll.tempNotUpOrDownFaultTimeSec.ToString("0");
            textBox6.Text = devicesAll.tempNotUpOrDwonFaultThr.ToString("0.0000");
            textBox7.Text = devicesAll.flucFaultTimeSec.ToString("0");
            textBox8.Text = devicesAll.flucFaultThr.ToString("0.0000");
            textBox9.Text = devicesAll.tempBiasFaultThr.ToString("0.0000");
            textBox10.Text = devicesAll.tempMaxValue.ToString("0.0000");
            textBox11.Text = devicesAll.tempMinValue.ToString("0.0000");
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "9";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-9";
                }
                else
                {
                    tx.Text += "9";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "8";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-8";
                }
                else
                {
                    tx.Text += "8";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "7";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-7";
                }
                else
                {
                    tx.Text += "7";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "6";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-6";
                }
                else
                {
                    tx.Text += "6";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "5";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-5";
                }
                else
                {
                    tx.Text += "5";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "4";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-4";
                }
                else
                {
                    tx.Text += "4";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "3";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-3";
                }
                else
                {
                    tx.Text += "3";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "2";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-2";
                }
                else
                {
                    tx.Text += "2";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 1 && tx.Text == "0")
                {
                    tx.Text = "1";
                }
                else if (tx.Text.Length == 2 && tx.Text == "-0")
                {
                    tx.Text = "-1";
                }
                else
                {
                    tx.Text += "1";
                }

            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length == 2 && tx.Text == "-0")
                {

                }
                else if (tx.Text.Length != 1 || tx.Text == "-" || int.Parse(tx.Text) != 0)
                {
                    tx.Text += "0";
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void buttonNegtive_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text == "")
                {
                    tx.Text = "-";
                }
                else if (tx.Text[0] == '-')
                {
                    tx.Text = tx.Text.Remove(0, 1);
                }
                else
                {
                    tx.Text = tx.Text.Insert(0, "-");
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void buttonPoint_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (!tx.Text.Contains("."))
                {
                    if (tx.Text.Length == 0)
                    {
                        tx.Text = "0.";
                    }
                    else if (tx.Text.Length == 1 && tx.Text == "-")
                    {
                        tx.Text = "-0.";
                    }
                    else
                    {
                        tx.Text += ".";
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选定设定项!");
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                if (tx.Text.Length > 0)
                    tx.Text = tx.Text.Substring(0, tx.Text.Length - 1);
            }
            //else
            //{
            //    MessageBox.Show("请先选定设定项!");
            //}
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.Text = "";
            }
            //else
            //{
            //    MessageBox.Show("请先选定设定项!");
            //}
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox1;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox2;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox3;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox4;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox5;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox6;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox7;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox8;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox9;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox10;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        private void textBox11_Enter(object sender, EventArgs e)
        {
            if (tx != null)
            {
                tx.BackColor = System.Drawing.SystemColors.Control;
            }

            tx = this.textBox11;
            tx.BackColor = System.Drawing.SystemColors.Window;
        }

        
    }
}
