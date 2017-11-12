namespace TemperatureControl2
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtTempSet = new System.Windows.Forms.TextBox();
            this.TxtTempCorrect = new System.Windows.Forms.TextBox();
            this.TxtLeadAdjust = new System.Windows.Forms.TextBox();
            this.TxtFuzzy = new System.Windows.Forms.TextBox();
            this.TxtRatio = new System.Windows.Forms.TextBox();
            this.TxtIntegral = new System.Windows.Forms.TextBox();
            this.TxtPower = new System.Windows.Forms.TextBox();
            this.TxtFlucThr = new System.Windows.Forms.TextBox();
            this.BntRead = new System.Windows.Forms.Button();
            this.BntUpdate = new System.Windows.Forms.Button();
            this.TxtTempThr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonPoint = new System.Windows.Forms.Button();
            this.button0 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonNegtive = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(30, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "温度设定值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(30, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "温度修正值：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(30, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "超前调整值：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(42, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "模糊系数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(42, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "比例系数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(42, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "积分系数：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(42, 266);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "功率系数：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(30, 305);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "波动度阈值：";
            // 
            // TxtTempSet
            // 
            this.TxtTempSet.BackColor = System.Drawing.SystemColors.Control;
            this.TxtTempSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtTempSet.Location = new System.Drawing.Point(130, 29);
            this.TxtTempSet.Name = "TxtTempSet";
            this.TxtTempSet.Size = new System.Drawing.Size(100, 23);
            this.TxtTempSet.TabIndex = 2;
            this.TxtTempSet.Enter += new System.EventHandler(this.TxtTempSet_Enter);
            // 
            // TxtTempCorrect
            // 
            this.TxtTempCorrect.BackColor = System.Drawing.SystemColors.Control;
            this.TxtTempCorrect.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtTempCorrect.Location = new System.Drawing.Point(130, 68);
            this.TxtTempCorrect.Name = "TxtTempCorrect";
            this.TxtTempCorrect.Size = new System.Drawing.Size(100, 23);
            this.TxtTempCorrect.TabIndex = 2;
            this.TxtTempCorrect.Enter += new System.EventHandler(this.TxtTempCorrect_Enter);
            // 
            // TxtLeadAdjust
            // 
            this.TxtLeadAdjust.BackColor = System.Drawing.SystemColors.Control;
            this.TxtLeadAdjust.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtLeadAdjust.Location = new System.Drawing.Point(130, 107);
            this.TxtLeadAdjust.Name = "TxtLeadAdjust";
            this.TxtLeadAdjust.Size = new System.Drawing.Size(100, 23);
            this.TxtLeadAdjust.TabIndex = 2;
            this.TxtLeadAdjust.Enter += new System.EventHandler(this.TxtLeadAdjust_Enter);
            // 
            // TxtFuzzy
            // 
            this.TxtFuzzy.BackColor = System.Drawing.SystemColors.Control;
            this.TxtFuzzy.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtFuzzy.Location = new System.Drawing.Point(130, 146);
            this.TxtFuzzy.Name = "TxtFuzzy";
            this.TxtFuzzy.Size = new System.Drawing.Size(100, 23);
            this.TxtFuzzy.TabIndex = 2;
            this.TxtFuzzy.Enter += new System.EventHandler(this.TxtFuzzy_Enter);
            // 
            // TxtRatio
            // 
            this.TxtRatio.BackColor = System.Drawing.SystemColors.Control;
            this.TxtRatio.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtRatio.Location = new System.Drawing.Point(130, 185);
            this.TxtRatio.Name = "TxtRatio";
            this.TxtRatio.Size = new System.Drawing.Size(100, 23);
            this.TxtRatio.TabIndex = 2;
            this.TxtRatio.Enter += new System.EventHandler(this.TxtRatio_Enter);
            // 
            // TxtIntegral
            // 
            this.TxtIntegral.BackColor = System.Drawing.SystemColors.Control;
            this.TxtIntegral.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtIntegral.Location = new System.Drawing.Point(130, 224);
            this.TxtIntegral.Name = "TxtIntegral";
            this.TxtIntegral.Size = new System.Drawing.Size(100, 23);
            this.TxtIntegral.TabIndex = 2;
            this.TxtIntegral.Enter += new System.EventHandler(this.TxtIntegral_Enter);
            // 
            // TxtPower
            // 
            this.TxtPower.BackColor = System.Drawing.SystemColors.Control;
            this.TxtPower.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtPower.Location = new System.Drawing.Point(130, 263);
            this.TxtPower.Name = "TxtPower";
            this.TxtPower.Size = new System.Drawing.Size(100, 23);
            this.TxtPower.TabIndex = 2;
            this.TxtPower.Enter += new System.EventHandler(this.TxtPower_Enter);
            // 
            // TxtFlucThr
            // 
            this.TxtFlucThr.BackColor = System.Drawing.SystemColors.Control;
            this.TxtFlucThr.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtFlucThr.Location = new System.Drawing.Point(130, 302);
            this.TxtFlucThr.Name = "TxtFlucThr";
            this.TxtFlucThr.Size = new System.Drawing.Size(100, 23);
            this.TxtFlucThr.TabIndex = 2;
            this.TxtFlucThr.Enter += new System.EventHandler(this.TxtFlucThr_Enter);
            // 
            // BntRead
            // 
            this.BntRead.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BntRead.Location = new System.Drawing.Point(265, 301);
            this.BntRead.Name = "BntRead";
            this.BntRead.Size = new System.Drawing.Size(128, 60);
            this.BntRead.TabIndex = 3;
            this.BntRead.TabStop = false;
            this.BntRead.Text = "查询参数";
            this.BntRead.UseVisualStyleBackColor = true;
            this.BntRead.Click += new System.EventHandler(this.BntRead_Click);
            // 
            // BntUpdate
            // 
            this.BntUpdate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BntUpdate.Location = new System.Drawing.Point(401, 233);
            this.BntUpdate.Name = "BntUpdate";
            this.BntUpdate.Size = new System.Drawing.Size(128, 60);
            this.BntUpdate.TabIndex = 3;
            this.BntUpdate.TabStop = false;
            this.BntUpdate.Text = "更新参数";
            this.BntUpdate.UseVisualStyleBackColor = true;
            this.BntUpdate.Click += new System.EventHandler(this.BntUpdate_Click);
            // 
            // TxtTempThr
            // 
            this.TxtTempThr.BackColor = System.Drawing.SystemColors.Control;
            this.TxtTempThr.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtTempThr.Location = new System.Drawing.Point(130, 341);
            this.TxtTempThr.Name = "TxtTempThr";
            this.TxtTempThr.Size = new System.Drawing.Size(100, 23);
            this.TxtTempThr.TabIndex = 2;
            this.TxtTempThr.Enter += new System.EventHandler(this.TxtTempThr_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(42, 344);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 17);
            this.label9.TabIndex = 1;
            this.label9.Text = "温度阈值：";
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonBack.Location = new System.Drawing.Point(469, 29);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(60, 60);
            this.buttonBack.TabIndex = 35;
            this.buttonBack.TabStop = false;
            this.buttonBack.Text = "←";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonPoint
            // 
            this.buttonPoint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonPoint.Location = new System.Drawing.Point(469, 165);
            this.buttonPoint.Name = "buttonPoint";
            this.buttonPoint.Size = new System.Drawing.Size(60, 60);
            this.buttonPoint.TabIndex = 34;
            this.buttonPoint.TabStop = false;
            this.buttonPoint.Text = ".";
            this.buttonPoint.UseVisualStyleBackColor = true;
            this.buttonPoint.Click += new System.EventHandler(this.buttonPoint_Click);
            // 
            // button0
            // 
            this.button0.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button0.Location = new System.Drawing.Point(265, 233);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(60, 60);
            this.button0.TabIndex = 33;
            this.button0.TabStop = false;
            this.button0.Text = "0";
            this.button0.UseVisualStyleBackColor = true;
            this.button0.Click += new System.EventHandler(this.button0_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button8.Location = new System.Drawing.Point(333, 29);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(60, 60);
            this.button8.TabIndex = 32;
            this.button8.TabStop = false;
            this.button8.Text = "8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button9.Location = new System.Drawing.Point(401, 29);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(60, 60);
            this.button9.TabIndex = 31;
            this.button9.TabStop = false;
            this.button9.Text = "9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(265, 97);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 60);
            this.button4.TabIndex = 30;
            this.button4.TabStop = false;
            this.button4.Text = "4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(333, 97);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 60);
            this.button5.TabIndex = 29;
            this.button5.TabStop = false;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.Location = new System.Drawing.Point(401, 97);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(60, 60);
            this.button6.TabIndex = 28;
            this.button6.TabStop = false;
            this.button6.Text = "6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.Location = new System.Drawing.Point(265, 29);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(60, 60);
            this.button7.TabIndex = 27;
            this.button7.TabStop = false;
            this.button7.Text = "7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(265, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 60);
            this.button1.TabIndex = 26;
            this.button1.TabStop = false;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(401, 165);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 60);
            this.button3.TabIndex = 25;
            this.button3.TabStop = false;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(333, 165);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 60);
            this.button2.TabIndex = 24;
            this.button2.TabStop = false;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonClear.Location = new System.Drawing.Point(469, 97);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(60, 60);
            this.buttonClear.TabIndex = 36;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "Del";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonNegtive
            // 
            this.buttonNegtive.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonNegtive.Location = new System.Drawing.Point(333, 233);
            this.buttonNegtive.Name = "buttonNegtive";
            this.buttonNegtive.Size = new System.Drawing.Size(60, 60);
            this.buttonNegtive.TabIndex = 37;
            this.buttonNegtive.TabStop = false;
            this.buttonNegtive.Text = "-/+";
            this.buttonNegtive.UseVisualStyleBackColor = true;
            this.buttonNegtive.Click += new System.EventHandler(this.buttonNegtive_Click);
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button10.Location = new System.Drawing.Point(401, 302);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(128, 60);
            this.button10.TabIndex = 38;
            this.button10.TabStop = false;
            this.button10.Text = "返回主界面";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 402);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.buttonNegtive);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonPoint);
            this.Controls.Add(this.button0);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BntUpdate);
            this.Controls.Add(this.BntRead);
            this.Controls.Add(this.TxtTempThr);
            this.Controls.Add(this.TxtFlucThr);
            this.Controls.Add(this.TxtPower);
            this.Controls.Add(this.TxtIntegral);
            this.Controls.Add(this.TxtRatio);
            this.Controls.Add(this.TxtFuzzy);
            this.Controls.Add(this.TxtLeadAdjust);
            this.Controls.Add(this.TxtTempCorrect);
            this.Controls.Add(this.TxtTempSet);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSetting_FormClosed);
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtTempSet;
        private System.Windows.Forms.TextBox TxtTempCorrect;
        private System.Windows.Forms.TextBox TxtLeadAdjust;
        private System.Windows.Forms.TextBox TxtFuzzy;
        private System.Windows.Forms.TextBox TxtRatio;
        private System.Windows.Forms.TextBox TxtIntegral;
        private System.Windows.Forms.TextBox TxtPower;
        private System.Windows.Forms.TextBox TxtFlucThr;
        private System.Windows.Forms.Button BntRead;
        private System.Windows.Forms.Button BntUpdate;
        private System.Windows.Forms.TextBox TxtTempThr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonPoint;
        private System.Windows.Forms.Button button0;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonNegtive;
        private System.Windows.Forms.Button button10;
    }
}