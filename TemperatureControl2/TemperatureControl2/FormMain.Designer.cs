namespace TemperatureControl2
{
    partial class FormMain
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
            this.checkBox_curveM = new System.Windows.Forms.CheckBox();
            this.checkBox_paramM = new System.Windows.Forms.CheckBox();
            this.checkBox_logM = new System.Windows.Forms.CheckBox();
            this.checkBox_curveS = new System.Windows.Forms.CheckBox();
            this.checkBox_paramS = new System.Windows.Forms.CheckBox();
            this.checkBox_logS = new System.Windows.Forms.CheckBox();
            this.label_tempM = new System.Windows.Forms.Label();
            this.label_tempS = new System.Windows.Forms.Label();
            this.label_powerM = new System.Windows.Forms.Label();
            this.label_powerS = new System.Windows.Forms.Label();
            this.label_tempSetM = new System.Windows.Forms.Label();
            this.label_tempSetS = new System.Windows.Forms.Label();
            this.label_controlState = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_fluc = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button_debug = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_elect
            // 
            this.checkBox_elect.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_elect.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_elect.Location = new System.Drawing.Point(518, 249);
            this.checkBox_elect.Name = "checkBox_elect";
            this.checkBox_elect.Size = new System.Drawing.Size(140, 45);
            this.checkBox_elect.TabIndex = 20;
            this.checkBox_elect.Text = "总电源";
            this.checkBox_elect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_elect.UseVisualStyleBackColor = true;
            this.checkBox_elect.Click += new System.EventHandler(this.checkBox_elect_Click);
            // 
            // checkBox_auto
            // 
            this.checkBox_auto.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_auto.AutoCheck = false;
            this.checkBox_auto.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_auto.Location = new System.Drawing.Point(741, 249);
            this.checkBox_auto.Name = "checkBox_auto";
            this.checkBox_auto.Size = new System.Drawing.Size(140, 45);
            this.checkBox_auto.TabIndex = 21;
            this.checkBox_auto.Text = "自动";
            this.checkBox_auto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_auto.UseVisualStyleBackColor = true;
            this.checkBox_auto.Click += new System.EventHandler(this.checkBox_auto_Click);
            // 
            // checkBox_dataChk
            // 
            this.checkBox_dataChk.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_dataChk.AutoCheck = false;
            this.checkBox_dataChk.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_dataChk.Location = new System.Drawing.Point(518, 320);
            this.checkBox_dataChk.Name = "checkBox_dataChk";
            this.checkBox_dataChk.Size = new System.Drawing.Size(140, 45);
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
            this.checkBox_man.Location = new System.Drawing.Point(741, 320);
            this.checkBox_man.Name = "checkBox_man";
            this.checkBox_man.Size = new System.Drawing.Size(140, 45);
            this.checkBox_man.TabIndex = 23;
            this.checkBox_man.Text = "手动";
            this.checkBox_man.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_man.UseVisualStyleBackColor = true;
            this.checkBox_man.Click += new System.EventHandler(this.checkBox_man_Click);
            // 
            // checkBox_mainHeat
            // 
            this.checkBox_mainHeat.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_mainHeat.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_mainHeat.Location = new System.Drawing.Point(518, 391);
            this.checkBox_mainHeat.Name = "checkBox_mainHeat";
            this.checkBox_mainHeat.Size = new System.Drawing.Size(140, 45);
            this.checkBox_mainHeat.TabIndex = 24;
            this.checkBox_mainHeat.Text = "主槽控温";
            this.checkBox_mainHeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_mainHeat.UseVisualStyleBackColor = true;
            this.checkBox_mainHeat.Click += new System.EventHandler(this.checkBox_mainHeat_Click);
            // 
            // checkBox_mainCoolF
            // 
            this.checkBox_mainCoolF.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_mainCoolF.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_mainCoolF.Location = new System.Drawing.Point(741, 391);
            this.checkBox_mainCoolF.Name = "checkBox_mainCoolF";
            this.checkBox_mainCoolF.Size = new System.Drawing.Size(140, 45);
            this.checkBox_mainCoolF.TabIndex = 25;
            this.checkBox_mainCoolF.Text = "主槽快冷";
            this.checkBox_mainCoolF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_mainCoolF.UseVisualStyleBackColor = true;
            this.checkBox_mainCoolF.Click += new System.EventHandler(this.checkBox_mainCoolF_Click);
            // 
            // checkBox_subHeat
            // 
            this.checkBox_subHeat.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_subHeat.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subHeat.Location = new System.Drawing.Point(518, 462);
            this.checkBox_subHeat.Name = "checkBox_subHeat";
            this.checkBox_subHeat.Size = new System.Drawing.Size(140, 45);
            this.checkBox_subHeat.TabIndex = 26;
            this.checkBox_subHeat.Text = "辅槽控温";
            this.checkBox_subHeat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subHeat.UseVisualStyleBackColor = true;
            this.checkBox_subHeat.Click += new System.EventHandler(this.checkBox_subHeat_Click);
            // 
            // checkBox_subCoolF
            // 
            this.checkBox_subCoolF.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_subCoolF.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCoolF.Location = new System.Drawing.Point(741, 462);
            this.checkBox_subCoolF.Name = "checkBox_subCoolF";
            this.checkBox_subCoolF.Size = new System.Drawing.Size(140, 45);
            this.checkBox_subCoolF.TabIndex = 27;
            this.checkBox_subCoolF.Text = "辅槽快冷";
            this.checkBox_subCoolF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCoolF.UseVisualStyleBackColor = true;
            this.checkBox_subCoolF.Click += new System.EventHandler(this.checkBox_subCoolF_Click);
            // 
            // checkBox_subCool
            // 
            this.checkBox_subCool.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_subCool.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCool.Location = new System.Drawing.Point(518, 533);
            this.checkBox_subCool.Name = "checkBox_subCool";
            this.checkBox_subCool.Size = new System.Drawing.Size(140, 45);
            this.checkBox_subCool.TabIndex = 28;
            this.checkBox_subCool.Text = "辅槽制冷";
            this.checkBox_subCool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCool.UseVisualStyleBackColor = true;
            this.checkBox_subCool.Click += new System.EventHandler(this.checkBox_subCool_Click);
            // 
            // checkBox_waterIn
            // 
            this.checkBox_waterIn.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_waterIn.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_waterIn.Location = new System.Drawing.Point(741, 533);
            this.checkBox_waterIn.Name = "checkBox_waterIn";
            this.checkBox_waterIn.Size = new System.Drawing.Size(140, 45);
            this.checkBox_waterIn.TabIndex = 29;
            this.checkBox_waterIn.Text = "海水进";
            this.checkBox_waterIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_waterIn.UseVisualStyleBackColor = true;
            this.checkBox_waterIn.Click += new System.EventHandler(this.checkBox_waterIn_Click);
            // 
            // checkBox_subCircle
            // 
            this.checkBox_subCircle.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_subCircle.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCircle.Location = new System.Drawing.Point(518, 604);
            this.checkBox_subCircle.Name = "checkBox_subCircle";
            this.checkBox_subCircle.Size = new System.Drawing.Size(140, 45);
            this.checkBox_subCircle.TabIndex = 30;
            this.checkBox_subCircle.Text = "辅槽循环";
            this.checkBox_subCircle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_subCircle.UseVisualStyleBackColor = true;
            this.checkBox_subCircle.Click += new System.EventHandler(this.checkBox_subCircle_Click);
            // 
            // checkBox_waterOut
            // 
            this.checkBox_waterOut.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_waterOut.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_waterOut.Location = new System.Drawing.Point(741, 604);
            this.checkBox_waterOut.Name = "checkBox_waterOut";
            this.checkBox_waterOut.Size = new System.Drawing.Size(140, 45);
            this.checkBox_waterOut.TabIndex = 31;
            this.checkBox_waterOut.Text = "海水出";
            this.checkBox_waterOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_waterOut.UseVisualStyleBackColor = true;
            this.checkBox_waterOut.Click += new System.EventHandler(this.checkBox_waterOut_Click);
            // 
            // checkBox_curveM
            // 
            this.checkBox_curveM.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_curveM.AutoCheck = false;
            this.checkBox_curveM.Location = new System.Drawing.Point(100, 278);
            this.checkBox_curveM.Name = "checkBox_curveM";
            this.checkBox_curveM.Size = new System.Drawing.Size(80, 40);
            this.checkBox_curveM.TabIndex = 33;
            this.checkBox_curveM.Text = "曲线";
            this.checkBox_curveM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_curveM.UseVisualStyleBackColor = true;
            this.checkBox_curveM.Click += new System.EventHandler(this.checkBox_curveM_Click);
            // 
            // checkBox_paramM
            // 
            this.checkBox_paramM.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_paramM.AutoCheck = false;
            this.checkBox_paramM.Location = new System.Drawing.Point(215, 278);
            this.checkBox_paramM.Name = "checkBox_paramM";
            this.checkBox_paramM.Size = new System.Drawing.Size(80, 40);
            this.checkBox_paramM.TabIndex = 34;
            this.checkBox_paramM.Text = "参数设置";
            this.checkBox_paramM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_paramM.UseVisualStyleBackColor = true;
            this.checkBox_paramM.Click += new System.EventHandler(this.checkBox_paramM_Click);
            // 
            // checkBox_logM
            // 
            this.checkBox_logM.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_logM.AutoCheck = false;
            this.checkBox_logM.Location = new System.Drawing.Point(330, 278);
            this.checkBox_logM.Name = "checkBox_logM";
            this.checkBox_logM.Size = new System.Drawing.Size(80, 40);
            this.checkBox_logM.TabIndex = 35;
            this.checkBox_logM.Text = "操作日志";
            this.checkBox_logM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_logM.UseVisualStyleBackColor = true;
            this.checkBox_logM.CheckedChanged += new System.EventHandler(this.checkBox_logM_CheckedChanged);
            this.checkBox_logM.Click += new System.EventHandler(this.checkBox_logM_Click);
            // 
            // checkBox_curveS
            // 
            this.checkBox_curveS.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_curveS.AutoCheck = false;
            this.checkBox_curveS.Location = new System.Drawing.Point(100, 606);
            this.checkBox_curveS.Name = "checkBox_curveS";
            this.checkBox_curveS.Size = new System.Drawing.Size(80, 40);
            this.checkBox_curveS.TabIndex = 37;
            this.checkBox_curveS.Text = "曲线";
            this.checkBox_curveS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_curveS.UseVisualStyleBackColor = true;
            this.checkBox_curveS.Click += new System.EventHandler(this.checkBox_curveS_Click);
            // 
            // checkBox_paramS
            // 
            this.checkBox_paramS.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_paramS.AutoCheck = false;
            this.checkBox_paramS.Location = new System.Drawing.Point(215, 609);
            this.checkBox_paramS.Name = "checkBox_paramS";
            this.checkBox_paramS.Size = new System.Drawing.Size(80, 40);
            this.checkBox_paramS.TabIndex = 38;
            this.checkBox_paramS.Text = "参数设置";
            this.checkBox_paramS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_paramS.UseVisualStyleBackColor = true;
            this.checkBox_paramS.Click += new System.EventHandler(this.checkBox_paramS_Click);
            // 
            // checkBox_logS
            // 
            this.checkBox_logS.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_logS.AutoCheck = false;
            this.checkBox_logS.Location = new System.Drawing.Point(330, 609);
            this.checkBox_logS.Name = "checkBox_logS";
            this.checkBox_logS.Size = new System.Drawing.Size(80, 40);
            this.checkBox_logS.TabIndex = 39;
            this.checkBox_logS.Text = "操作日志";
            this.checkBox_logS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_logS.UseVisualStyleBackColor = true;
            this.checkBox_logS.CheckedChanged += new System.EventHandler(this.checkBox_logS_CheckedChanged);
            this.checkBox_logS.Click += new System.EventHandler(this.checkBox_logS_Click);
            // 
            // label_tempM
            // 
            this.label_tempM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_tempM.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_tempM.Location = new System.Drawing.Point(100, 65);
            this.label_tempM.Name = "label_tempM";
            this.label_tempM.Size = new System.Drawing.Size(310, 70);
            this.label_tempM.TabIndex = 45;
            this.label_tempM.Text = "0.0000℃";
            this.label_tempM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_tempS
            // 
            this.label_tempS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_tempS.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_tempS.Location = new System.Drawing.Point(97, 400);
            this.label_tempS.Name = "label_tempS";
            this.label_tempS.Size = new System.Drawing.Size(310, 70);
            this.label_tempS.TabIndex = 46;
            this.label_tempS.Text = "0.0000℃";
            this.label_tempS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_powerM
            // 
            this.label_powerM.BackColor = System.Drawing.SystemColors.Control;
            this.label_powerM.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_powerM.Location = new System.Drawing.Point(351, 183);
            this.label_powerM.Name = "label_powerM";
            this.label_powerM.Size = new System.Drawing.Size(55, 28);
            this.label_powerM.TabIndex = 47;
            this.label_powerM.Text = "00%";
            this.label_powerM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_powerS
            // 
            this.label_powerS.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_powerS.Location = new System.Drawing.Point(355, 522);
            this.label_powerS.Name = "label_powerS";
            this.label_powerS.Size = new System.Drawing.Size(52, 28);
            this.label_powerS.TabIndex = 48;
            this.label_powerS.Text = "00%";
            this.label_powerS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_tempSetM
            // 
            this.label_tempSetM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_tempSetM.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_tempSetM.Location = new System.Drawing.Point(100, 182);
            this.label_tempSetM.Name = "label_tempSetM";
            this.label_tempSetM.Size = new System.Drawing.Size(130, 30);
            this.label_tempSetM.TabIndex = 49;
            this.label_tempSetM.Text = "0.0000℃";
            this.label_tempSetM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_tempSetS
            // 
            this.label_tempSetS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_tempSetS.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_tempSetS.Location = new System.Drawing.Point(100, 521);
            this.label_tempSetS.Name = "label_tempSetS";
            this.label_tempSetS.Size = new System.Drawing.Size(130, 30);
            this.label_tempSetS.TabIndex = 50;
            this.label_tempSetS.Text = "0.0000℃";
            this.label_tempSetS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_controlState
            // 
            this.label_controlState.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_controlState.Location = new System.Drawing.Point(519, 75);
            this.label_controlState.Name = "label_controlState";
            this.label_controlState.Size = new System.Drawing.Size(358, 31);
            this.label_controlState.TabIndex = 52;
            this.label_controlState.Text = "系统启动";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(266, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 30);
            this.label1.TabIndex = 53;
            this.label1.Text = "加热功率";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(266, 521);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 30);
            this.label2.TabIndex = 54;
            this.label2.Text = "加热功率";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_fluc
            // 
            this.label_fluc.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_fluc.Location = new System.Drawing.Point(519, 106);
            this.label_fluc.Name = "label_fluc";
            this.label_fluc.Size = new System.Drawing.Size(358, 31);
            this.label_fluc.TabIndex = 56;
            this.label_fluc.Text = "主控温槽波动度：****";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(518, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(363, 146);
            this.label3.TabIndex = 57;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(102, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 12);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 58;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(99, 402);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 12);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 59;
            this.pictureBox2.TabStop = false;
            // 
            // button_debug
            // 
            this.button_debug.Location = new System.Drawing.Point(12, 12);
            this.button_debug.Name = "button_debug";
            this.button_debug.Size = new System.Drawing.Size(75, 23);
            this.button_debug.TabIndex = 60;
            this.button_debug.Text = "调试";
            this.button_debug.UseVisualStyleBackColor = true;
            this.button_debug.Click += new System.EventHandler(this.button_debug_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(973, 684);
            this.Controls.Add(this.button_debug);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_fluc);
            this.Controls.Add(this.label_controlState);
            this.Controls.Add(this.label_tempSetS);
            this.Controls.Add(this.label_tempSetM);
            this.Controls.Add(this.label_powerS);
            this.Controls.Add(this.label_powerM);
            this.Controls.Add(this.label_tempS);
            this.Controls.Add(this.label_tempM);
            this.Controls.Add(this.checkBox_logS);
            this.Controls.Add(this.checkBox_paramS);
            this.Controls.Add(this.checkBox_curveS);
            this.Controls.Add(this.checkBox_logM);
            this.Controls.Add(this.checkBox_paramM);
            this.Controls.Add(this.checkBox_curveM);
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
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TemperatureControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.CheckBox checkBox_curveM;
        private System.Windows.Forms.CheckBox checkBox_paramM;
        private System.Windows.Forms.CheckBox checkBox_logM;
        private System.Windows.Forms.CheckBox checkBox_curveS;
        private System.Windows.Forms.CheckBox checkBox_paramS;
        private System.Windows.Forms.CheckBox checkBox_logS;
        private System.Windows.Forms.Label label_tempM;
        private System.Windows.Forms.Label label_tempS;
        private System.Windows.Forms.Label label_powerM;
        private System.Windows.Forms.Label label_powerS;
        private System.Windows.Forms.Label label_tempSetM;
        private System.Windows.Forms.Label label_tempSetS;
        private System.Windows.Forms.Label label_controlState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_fluc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button_debug;
    }
}

