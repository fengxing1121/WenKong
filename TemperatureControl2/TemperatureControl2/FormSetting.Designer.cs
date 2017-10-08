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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(54, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "设定值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(54, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "调整值：";
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
            this.TxtTempSet.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtTempSet.Location = new System.Drawing.Point(130, 29);
            this.TxtTempSet.Name = "TxtTempSet";
            this.TxtTempSet.Size = new System.Drawing.Size(100, 23);
            this.TxtTempSet.TabIndex = 2;
            // 
            // TxtTempCorrect
            // 
            this.TxtTempCorrect.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtTempCorrect.Location = new System.Drawing.Point(130, 68);
            this.TxtTempCorrect.Name = "TxtTempCorrect";
            this.TxtTempCorrect.Size = new System.Drawing.Size(100, 23);
            this.TxtTempCorrect.TabIndex = 2;
            // 
            // TxtLeadAdjust
            // 
            this.TxtLeadAdjust.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtLeadAdjust.Location = new System.Drawing.Point(130, 107);
            this.TxtLeadAdjust.Name = "TxtLeadAdjust";
            this.TxtLeadAdjust.Size = new System.Drawing.Size(100, 23);
            this.TxtLeadAdjust.TabIndex = 2;
            // 
            // TxtFuzzy
            // 
            this.TxtFuzzy.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtFuzzy.Location = new System.Drawing.Point(130, 146);
            this.TxtFuzzy.Name = "TxtFuzzy";
            this.TxtFuzzy.Size = new System.Drawing.Size(100, 23);
            this.TxtFuzzy.TabIndex = 2;
            // 
            // TxtRatio
            // 
            this.TxtRatio.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtRatio.Location = new System.Drawing.Point(130, 185);
            this.TxtRatio.Name = "TxtRatio";
            this.TxtRatio.Size = new System.Drawing.Size(100, 23);
            this.TxtRatio.TabIndex = 2;
            // 
            // TxtIntegral
            // 
            this.TxtIntegral.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtIntegral.Location = new System.Drawing.Point(130, 224);
            this.TxtIntegral.Name = "TxtIntegral";
            this.TxtIntegral.Size = new System.Drawing.Size(100, 23);
            this.TxtIntegral.TabIndex = 2;
            // 
            // TxtPower
            // 
            this.TxtPower.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtPower.Location = new System.Drawing.Point(130, 263);
            this.TxtPower.Name = "TxtPower";
            this.TxtPower.Size = new System.Drawing.Size(100, 23);
            this.TxtPower.TabIndex = 2;
            // 
            // TxtFlucThr
            // 
            this.TxtFlucThr.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtFlucThr.Location = new System.Drawing.Point(130, 302);
            this.TxtFlucThr.Name = "TxtFlucThr";
            this.TxtFlucThr.Size = new System.Drawing.Size(100, 23);
            this.TxtFlucThr.TabIndex = 2;
            // 
            // BntRead
            // 
            this.BntRead.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BntRead.Location = new System.Drawing.Point(27, 392);
            this.BntRead.Name = "BntRead";
            this.BntRead.Size = new System.Drawing.Size(88, 43);
            this.BntRead.TabIndex = 3;
            this.BntRead.Text = "查询参数";
            this.BntRead.UseVisualStyleBackColor = true;
            this.BntRead.Click += new System.EventHandler(this.BntRead_Click);
            // 
            // BntUpdate
            // 
            this.BntUpdate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BntUpdate.Location = new System.Drawing.Point(144, 392);
            this.BntUpdate.Name = "BntUpdate";
            this.BntUpdate.Size = new System.Drawing.Size(86, 43);
            this.BntUpdate.TabIndex = 3;
            this.BntUpdate.Text = "更新参数";
            this.BntUpdate.UseVisualStyleBackColor = true;
            this.BntUpdate.Click += new System.EventHandler(this.BntUpdate_Click);
            // 
            // TxtTempThr
            // 
            this.TxtTempThr.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtTempThr.Location = new System.Drawing.Point(130, 341);
            this.TxtTempThr.Name = "TxtTempThr";
            this.TxtTempThr.Size = new System.Drawing.Size(100, 23);
            this.TxtTempThr.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(30, 344);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 17);
            this.label9.TabIndex = 1;
            this.label9.Text = "温度度阈值：";
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 467);
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
            this.MaximizeBox = false;
            this.Name = "FormSetting";
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
    }
}