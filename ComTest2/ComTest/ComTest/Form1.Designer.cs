namespace ComTest
{
    partial class Form1
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
            this.groupBox_Sr = new System.Windows.Forms.GroupBox();
            this.pictureBox_Sr = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_RyTempValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_SrStatus = new System.Windows.Forms.ComboBox();
            this.checkBox_SrErrLast = new System.Windows.Forms.CheckBox();
            this.groupBox_Ry = new System.Windows.Forms.GroupBox();
            this.pictureBox_Ry = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_RyStatus = new System.Windows.Forms.ComboBox();
            this.checkBox_RyErrLast = new System.Windows.Forms.CheckBox();
            this.groupBox_Sr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Sr)).BeginInit();
            this.groupBox_Ry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Ry)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_Sr
            // 
            this.groupBox_Sr.Controls.Add(this.pictureBox_Sr);
            this.groupBox_Sr.Controls.Add(this.label2);
            this.groupBox_Sr.Controls.Add(this.textBox_RyTempValue);
            this.groupBox_Sr.Controls.Add(this.label1);
            this.groupBox_Sr.Controls.Add(this.comboBox_SrStatus);
            this.groupBox_Sr.Controls.Add(this.checkBox_SrErrLast);
            this.groupBox_Sr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_Sr.Location = new System.Drawing.Point(14, 14);
            this.groupBox_Sr.Name = "groupBox_Sr";
            this.groupBox_Sr.Size = new System.Drawing.Size(364, 197);
            this.groupBox_Sr.TabIndex = 0;
            this.groupBox_Sr.TabStop = false;
            this.groupBox_Sr.Text = "传感器设备";
            // 
            // pictureBox_Sr
            // 
            this.pictureBox_Sr.Location = new System.Drawing.Point(318, 22);
            this.pictureBox_Sr.Name = "pictureBox_Sr";
            this.pictureBox_Sr.Size = new System.Drawing.Size(40, 15);
            this.pictureBox_Sr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_Sr.TabIndex = 5;
            this.pictureBox_Sr.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(7, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "传感器温度值：";
            // 
            // textBox_RyTempValue
            // 
            this.textBox_RyTempValue.Location = new System.Drawing.Point(7, 145);
            this.textBox_RyTempValue.Name = "textBox_RyTempValue";
            this.textBox_RyTempValue.Size = new System.Drawing.Size(205, 23);
            this.textBox_RyTempValue.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "设备错误状态：";
            // 
            // comboBox_SrStatus
            // 
            this.comboBox_SrStatus.FormattingEnabled = true;
            this.comboBox_SrStatus.Items.AddRange(new object[] {
            "正常工作 - OK",
            "连接断开 - DisConnected",
            "错误 - DataErr"});
            this.comboBox_SrStatus.Location = new System.Drawing.Point(7, 65);
            this.comboBox_SrStatus.Name = "comboBox_SrStatus";
            this.comboBox_SrStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox_SrStatus.Size = new System.Drawing.Size(205, 22);
            this.comboBox_SrStatus.TabIndex = 1;
            this.comboBox_SrStatus.SelectedIndexChanged += new System.EventHandler(this.comboBox_SrStatus_SelectedIndexChanged);
            // 
            // checkBox_SrErrLast
            // 
            this.checkBox_SrErrLast.AutoSize = true;
            this.checkBox_SrErrLast.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_SrErrLast.Location = new System.Drawing.Point(238, 67);
            this.checkBox_SrErrLast.Name = "checkBox_SrErrLast";
            this.checkBox_SrErrLast.Size = new System.Drawing.Size(110, 18);
            this.checkBox_SrErrLast.TabIndex = 0;
            this.checkBox_SrErrLast.Text = "保持错误状态";
            this.checkBox_SrErrLast.UseVisualStyleBackColor = true;
            this.checkBox_SrErrLast.CheckedChanged += new System.EventHandler(this.checkBox_SrErrLast_CheckedChanged);
            // 
            // groupBox_Ry
            // 
            this.groupBox_Ry.Controls.Add(this.pictureBox_Ry);
            this.groupBox_Ry.Controls.Add(this.label4);
            this.groupBox_Ry.Controls.Add(this.comboBox_RyStatus);
            this.groupBox_Ry.Controls.Add(this.checkBox_RyErrLast);
            this.groupBox_Ry.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_Ry.Location = new System.Drawing.Point(433, 14);
            this.groupBox_Ry.Name = "groupBox_Ry";
            this.groupBox_Ry.Size = new System.Drawing.Size(364, 197);
            this.groupBox_Ry.TabIndex = 1;
            this.groupBox_Ry.TabStop = false;
            this.groupBox_Ry.Text = "继电器设备";
            // 
            // pictureBox_Ry
            // 
            this.pictureBox_Ry.Location = new System.Drawing.Point(318, 22);
            this.pictureBox_Ry.Name = "pictureBox_Ry";
            this.pictureBox_Ry.Size = new System.Drawing.Size(40, 15);
            this.pictureBox_Ry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_Ry.TabIndex = 5;
            this.pictureBox_Ry.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(7, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "设备错误状态：";
            // 
            // comboBox_RyStatus
            // 
            this.comboBox_RyStatus.FormattingEnabled = true;
            this.comboBox_RyStatus.Items.AddRange(new object[] {
            "正常工作 - OK",
            "连接断开 - DisConnected",
            "数据错误 - DataErr"});
            this.comboBox_RyStatus.Location = new System.Drawing.Point(7, 65);
            this.comboBox_RyStatus.Name = "comboBox_RyStatus";
            this.comboBox_RyStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox_RyStatus.Size = new System.Drawing.Size(205, 22);
            this.comboBox_RyStatus.TabIndex = 1;
            this.comboBox_RyStatus.SelectedIndexChanged += new System.EventHandler(this.comboBox_RyStatus_SelectedIndexChanged);
            // 
            // checkBox_RyErrLast
            // 
            this.checkBox_RyErrLast.AutoSize = true;
            this.checkBox_RyErrLast.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_RyErrLast.Location = new System.Drawing.Point(238, 67);
            this.checkBox_RyErrLast.Name = "checkBox_RyErrLast";
            this.checkBox_RyErrLast.Size = new System.Drawing.Size(110, 18);
            this.checkBox_RyErrLast.TabIndex = 0;
            this.checkBox_RyErrLast.Text = "保持错误状态";
            this.checkBox_RyErrLast.UseVisualStyleBackColor = true;
            this.checkBox_RyErrLast.CheckedChanged += new System.EventHandler(this.checkBox_RyErrLast_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 465);
            this.Controls.Add(this.groupBox_Ry);
            this.Controls.Add(this.groupBox_Sr);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.groupBox_Sr.ResumeLayout(false);
            this.groupBox_Sr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Sr)).EndInit();
            this.groupBox_Ry.ResumeLayout(false);
            this.groupBox_Ry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Ry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_Sr;
        private System.Windows.Forms.ComboBox comboBox_SrStatus;
        private System.Windows.Forms.CheckBox checkBox_SrErrLast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_RyTempValue;
        private System.Windows.Forms.PictureBox pictureBox_Sr;
        private System.Windows.Forms.GroupBox groupBox_Ry;
        private System.Windows.Forms.PictureBox pictureBox_Ry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_RyStatus;
        private System.Windows.Forms.CheckBox checkBox_RyErrLast;
    }
}

