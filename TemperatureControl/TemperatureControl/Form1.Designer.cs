namespace TemperatureControl
{
    partial class Form_main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox_elect = new System.Windows.Forms.CheckBox();
            this.checkBox_auto = new System.Windows.Forms.CheckBox();
            this.checkBox_dataChk = new System.Windows.Forms.CheckBox();
            this.checkBox_man = new System.Windows.Forms.CheckBox();
            this.checkBox_mainHeat = new System.Windows.Forms.CheckBox();
            this.checkBox_mainCoolF = new System.Windows.Forms.CheckBox();
            this.checkBox_subHeat = new System.Windows.Forms.CheckBox();
            this.checkBox_subCoolF = new System.Windows.Forms.CheckBox();
            this.checkBox_subCool = new System.Windows.Forms.CheckBox();
            this.checkBox_waterIn = new System.Windows.Forms.CheckBox();
            this.checkBox_subCircle = new System.Windows.Forms.CheckBox();
            this.checkBox_waterOut = new System.Windows.Forms.CheckBox();
            this.checkBox_startM = new System.Windows.Forms.CheckBox();
            this.checkBox_curveM = new System.Windows.Forms.CheckBox();
            this.checkBox_paramM = new System.Windows.Forms.CheckBox();
            this.checkBox_logM = new System.Windows.Forms.CheckBox();
            this.checkBox_startS = new System.Windows.Forms.CheckBox();
            this.checkBox_curveS = new System.Windows.Forms.CheckBox();
            this.checkBox_paramS = new System.Windows.Forms.CheckBox();
            this.checkBox_logS = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox_elect
            // 
            this.checkBox_elect.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_elect.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_elect.Location = new System.Drawing.Point(599, 272);
            this.checkBox_elect.Name = "checkBox_elect";
            this.checkBox_elect.Size = new System.Drawing.Size(120, 35);
            this.checkBox_elect.TabIndex = 20;
            this.checkBox_elect.Text = "总电源";
            this.checkBox_elect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_elect.UseVisualStyleBackColor = true;
            this.checkBox_elect.CheckStateChanged += new System.EventHandler(this.checkBox_elect_CheckStateChanged);
            // 
            // checkBox_auto
            // 
            this.checkBox_auto.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_auto.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_auto.Location = new System.Drawing.Point(795, 272);
            this.checkBox_auto.Name = "checkBox_auto";
            this.checkBox_auto.Size = new System.Drawing.Size(120, 35);
            this.checkBox_auto.TabIndex = 21;
            this.checkBox_auto.Text = "自动";
            this.checkBox_auto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_auto.UseVisualStyleBackColor = true;
            this.checkBox_auto.CheckedChanged += new System.EventHandler(this.checkBox_auto_CheckedChanged);
            // 
            // checkBox_dataChk
            // 
            this.checkBox_dataChk.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_dataChk.AutoCheck = false;
            this.checkBox_dataChk.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_dataChk.Location = new System.Drawing.Point(599, 343);
            this.checkBox_dataChk.Name = "checkBox_dataChk";
            this.checkBox_dataChk.Size = new System.Drawing.Size(120, 35);
            this.checkBox_dataChk.TabIndex = 22;
            this.checkBox_dataChk.Text = "数据查询";
            this.checkBox_dataChk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_dataChk.UseVisualStyleBackColor = true;
            this.checkBox_dataChk.Click += new System.EventHandler(this.checkBox_dataChk_Click);
            // 
            // checkBox_man
            // 
            this.checkBox_man.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_man.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_man.Location = new System.Drawing.Point(795, 343);
            this.checkBox_man.Name = "checkBox_man";
            this.checkBox_man.Size = new System.Drawing.Size(120, 35);
            this.checkBox_man.TabIndex = 23;
            this.checkBox_man.Text = "手动";
            this.checkBox_man.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_man.UseVisualStyleBackColor = true;
            this.checkBox_man.CheckedChanged += new System.EventHandler(this.checkBox_man_CheckedChanged);
            // 
            // checkBox_mainHeat
            // 
            this.checkBox_mainHeat.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_mainHeat.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_mainHeat.Location = new System.Drawing.Point(599, 414);
            this.checkBox_mainHeat.Name = "checkBox_mainHeat";
            this.checkBox_mainHeat.Size = new System.Drawing.Size(120, 35);
            this.checkBox_mainHeat.TabIndex = 24;
            this.checkBox_mainHeat.Text = "主槽控温";
            this.checkBox_mainHeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_mainHeat.UseVisualStyleBackColor = true;
            this.checkBox_mainHeat.CheckedChanged += new System.EventHandler(this.checkBox_mainHeat_CheckedChanged);
            // 
            // checkBox_mainCoolF
            // 
            this.checkBox_mainCoolF.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_mainCoolF.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_mainCoolF.Location = new System.Drawing.Point(795, 414);
            this.checkBox_mainCoolF.Name = "checkBox_mainCoolF";
            this.checkBox_mainCoolF.Size = new System.Drawing.Size(120, 35);
            this.checkBox_mainCoolF.TabIndex = 25;
            this.checkBox_mainCoolF.Text = "主槽快冷";
            this.checkBox_mainCoolF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_mainCoolF.UseVisualStyleBackColor = true;
            this.checkBox_mainCoolF.CheckedChanged += new System.EventHandler(this.checkBox_mainCoolF_CheckedChanged);
            // 
            // checkBox_subHeat
            // 
            this.checkBox_subHeat.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_subHeat.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subHeat.Location = new System.Drawing.Point(599, 485);
            this.checkBox_subHeat.Name = "checkBox_subHeat";
            this.checkBox_subHeat.Size = new System.Drawing.Size(120, 35);
            this.checkBox_subHeat.TabIndex = 26;
            this.checkBox_subHeat.Text = "辅槽控温";
            this.checkBox_subHeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subHeat.UseVisualStyleBackColor = true;
            this.checkBox_subHeat.CheckedChanged += new System.EventHandler(this.checkBox_subHeat_CheckedChanged);
            // 
            // checkBox_subCoolF
            // 
            this.checkBox_subCoolF.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_subCoolF.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCoolF.Location = new System.Drawing.Point(795, 485);
            this.checkBox_subCoolF.Name = "checkBox_subCoolF";
            this.checkBox_subCoolF.Size = new System.Drawing.Size(120, 35);
            this.checkBox_subCoolF.TabIndex = 27;
            this.checkBox_subCoolF.Text = "辅槽快冷";
            this.checkBox_subCoolF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCoolF.UseVisualStyleBackColor = true;
            this.checkBox_subCoolF.CheckedChanged += new System.EventHandler(this.checkBox_subCoolF_CheckedChanged);
            // 
            // checkBox_subCool
            // 
            this.checkBox_subCool.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_subCool.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCool.Location = new System.Drawing.Point(599, 556);
            this.checkBox_subCool.Name = "checkBox_subCool";
            this.checkBox_subCool.Size = new System.Drawing.Size(120, 35);
            this.checkBox_subCool.TabIndex = 28;
            this.checkBox_subCool.Text = "辅槽制冷";
            this.checkBox_subCool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCool.UseVisualStyleBackColor = true;
            this.checkBox_subCool.CheckedChanged += new System.EventHandler(this.checkBox_subCool_CheckedChanged);
            // 
            // checkBox_waterIn
            // 
            this.checkBox_waterIn.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_waterIn.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_waterIn.Location = new System.Drawing.Point(795, 556);
            this.checkBox_waterIn.Name = "checkBox_waterIn";
            this.checkBox_waterIn.Size = new System.Drawing.Size(120, 35);
            this.checkBox_waterIn.TabIndex = 29;
            this.checkBox_waterIn.Text = "海水进";
            this.checkBox_waterIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_waterIn.UseVisualStyleBackColor = true;
            this.checkBox_waterIn.CheckedChanged += new System.EventHandler(this.checkBox_waterIn_CheckedChanged);
            // 
            // checkBox_subCircle
            // 
            this.checkBox_subCircle.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_subCircle.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCircle.Location = new System.Drawing.Point(599, 627);
            this.checkBox_subCircle.Name = "checkBox_subCircle";
            this.checkBox_subCircle.Size = new System.Drawing.Size(120, 35);
            this.checkBox_subCircle.TabIndex = 30;
            this.checkBox_subCircle.Text = "辅槽循环";
            this.checkBox_subCircle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCircle.UseVisualStyleBackColor = true;
            this.checkBox_subCircle.CheckedChanged += new System.EventHandler(this.checkBox_subCircle_CheckedChanged);
            // 
            // checkBox_waterOut
            // 
            this.checkBox_waterOut.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_waterOut.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_waterOut.Location = new System.Drawing.Point(795, 627);
            this.checkBox_waterOut.Name = "checkBox_waterOut";
            this.checkBox_waterOut.Size = new System.Drawing.Size(120, 35);
            this.checkBox_waterOut.TabIndex = 31;
            this.checkBox_waterOut.Text = "海水出";
            this.checkBox_waterOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_waterOut.UseVisualStyleBackColor = true;
            this.checkBox_waterOut.CheckedChanged += new System.EventHandler(this.checkBox_waterOut_CheckedChanged);
            // 
            // checkBox_startM
            // 
            this.checkBox_startM.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_startM.Location = new System.Drawing.Point(85, 322);
            this.checkBox_startM.Name = "checkBox_startM";
            this.checkBox_startM.Size = new System.Drawing.Size(75, 35);
            this.checkBox_startM.TabIndex = 32;
            this.checkBox_startM.Text = "启动";
            this.checkBox_startM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_startM.UseVisualStyleBackColor = true;
            // 
            // checkBox_curveM
            // 
            this.checkBox_curveM.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_curveM.Location = new System.Drawing.Point(197, 322);
            this.checkBox_curveM.Name = "checkBox_curveM";
            this.checkBox_curveM.Size = new System.Drawing.Size(75, 35);
            this.checkBox_curveM.TabIndex = 33;
            this.checkBox_curveM.Text = "曲线";
            this.checkBox_curveM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_curveM.UseVisualStyleBackColor = true;
            // 
            // checkBox_paramM
            // 
            this.checkBox_paramM.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_paramM.Location = new System.Drawing.Point(309, 322);
            this.checkBox_paramM.Name = "checkBox_paramM";
            this.checkBox_paramM.Size = new System.Drawing.Size(75, 35);
            this.checkBox_paramM.TabIndex = 34;
            this.checkBox_paramM.Text = "参数设置";
            this.checkBox_paramM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_paramM.UseVisualStyleBackColor = true;
            // 
            // checkBox_logM
            // 
            this.checkBox_logM.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_logM.Location = new System.Drawing.Point(421, 322);
            this.checkBox_logM.Name = "checkBox_logM";
            this.checkBox_logM.Size = new System.Drawing.Size(75, 35);
            this.checkBox_logM.TabIndex = 35;
            this.checkBox_logM.Text = "操作日志";
            this.checkBox_logM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_logM.UseVisualStyleBackColor = true;
            // 
            // checkBox_startS
            // 
            this.checkBox_startS.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_startS.Location = new System.Drawing.Point(85, 615);
            this.checkBox_startS.Name = "checkBox_startS";
            this.checkBox_startS.Size = new System.Drawing.Size(75, 35);
            this.checkBox_startS.TabIndex = 36;
            this.checkBox_startS.Text = "启动";
            this.checkBox_startS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_startS.UseVisualStyleBackColor = true;
            // 
            // checkBox_curveS
            // 
            this.checkBox_curveS.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_curveS.Location = new System.Drawing.Point(197, 614);
            this.checkBox_curveS.Name = "checkBox_curveS";
            this.checkBox_curveS.Size = new System.Drawing.Size(75, 35);
            this.checkBox_curveS.TabIndex = 37;
            this.checkBox_curveS.Text = "曲线";
            this.checkBox_curveS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_curveS.UseVisualStyleBackColor = true;
            // 
            // checkBox_paramS
            // 
            this.checkBox_paramS.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_paramS.Location = new System.Drawing.Point(309, 613);
            this.checkBox_paramS.Name = "checkBox_paramS";
            this.checkBox_paramS.Size = new System.Drawing.Size(75, 35);
            this.checkBox_paramS.TabIndex = 38;
            this.checkBox_paramS.Text = "参数设置";
            this.checkBox_paramS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_paramS.UseVisualStyleBackColor = true;
            // 
            // checkBox_logS
            // 
            this.checkBox_logS.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_logS.Location = new System.Drawing.Point(421, 612);
            this.checkBox_logS.Name = "checkBox_logS";
            this.checkBox_logS.Size = new System.Drawing.Size(75, 35);
            this.checkBox_logS.TabIndex = 39;
            this.checkBox_logS.Text = "操作日志";
            this.checkBox_logS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_logS.UseVisualStyleBackColor = true;
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 767);
            this.Controls.Add(this.checkBox_logS);
            this.Controls.Add(this.checkBox_paramS);
            this.Controls.Add(this.checkBox_curveS);
            this.Controls.Add(this.checkBox_startS);
            this.Controls.Add(this.checkBox_logM);
            this.Controls.Add(this.checkBox_paramM);
            this.Controls.Add(this.checkBox_curveM);
            this.Controls.Add(this.checkBox_startM);
            this.Controls.Add(this.checkBox_waterOut);
            this.Controls.Add(this.checkBox_subCircle);
            this.Controls.Add(this.checkBox_waterIn);
            this.Controls.Add(this.checkBox_subCool);
            this.Controls.Add(this.checkBox_subCoolF);
            this.Controls.Add(this.checkBox_subHeat);
            this.Controls.Add(this.checkBox_mainCoolF);
            this.Controls.Add(this.checkBox_mainHeat);
            this.Controls.Add(this.checkBox_man);
            this.Controls.Add(this.checkBox_dataChk);
            this.Controls.Add(this.checkBox_auto);
            this.Controls.Add(this.checkBox_elect);
            this.Name = "Form_main";
            this.Text = "TemperatureControl";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        // wghou
        // 添加设备组件
        private Device.TempDevice tpDeviceM;
        private Device.TempDevice tpDeviceS;
        private Device.RelayDevice ryDevice;
        private System.Windows.Forms.CheckBox checkBox_elect;
        private System.Windows.Forms.CheckBox checkBox_auto;
        private System.Windows.Forms.CheckBox checkBox_dataChk;
        private System.Windows.Forms.CheckBox checkBox_man;
        private System.Windows.Forms.CheckBox checkBox_mainHeat;
        private System.Windows.Forms.CheckBox checkBox_mainCoolF;
        private System.Windows.Forms.CheckBox checkBox_subHeat;
        private System.Windows.Forms.CheckBox checkBox_subCoolF;
        private System.Windows.Forms.CheckBox checkBox_subCool;
        private System.Windows.Forms.CheckBox checkBox_waterIn;
        private System.Windows.Forms.CheckBox checkBox_subCircle;
        private System.Windows.Forms.CheckBox checkBox_waterOut;
        private System.Windows.Forms.CheckBox checkBox_startM;
        private System.Windows.Forms.CheckBox checkBox_curveM;
        private System.Windows.Forms.CheckBox checkBox_paramM;
        private System.Windows.Forms.CheckBox checkBox_logM;
        private System.Windows.Forms.CheckBox checkBox_startS;
        private System.Windows.Forms.CheckBox checkBox_curveS;
        private System.Windows.Forms.CheckBox checkBox_paramS;
        private System.Windows.Forms.CheckBox checkBox_logS;
    }
}

