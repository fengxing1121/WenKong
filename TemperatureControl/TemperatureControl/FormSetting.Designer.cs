namespace TemperatureControl
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
            System.Windows.Forms.Label label1;
            this.button_apply = new System.Windows.Forms.Button();
            this.button_read = new System.Windows.Forms.Button();
            this.button_return = new System.Windows.Forms.Button();
            this.textBox_tempSet = new System.Windows.Forms.TextBox();
            this.textBox_tempRev = new System.Windows.Forms.TextBox();
            this.textBox_adjust = new System.Windows.Forms.TextBox();
            this.textBox_fuzzy = new System.Windows.Forms.TextBox();
            this.textBox_prop = new System.Windows.Forms.TextBox();
            this.textBox_integ = new System.Windows.Forms.TextBox();
            this.textBox_power = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(136, 40);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(65, 12);
            label1.TabIndex = 10;
            label1.Text = "温度设定值";
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(294, 327);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(75, 23);
            this.button_apply.TabIndex = 0;
            this.button_apply.Text = "应用";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // button_read
            // 
            this.button_read.Location = new System.Drawing.Point(192, 327);
            this.button_read.Name = "button_read";
            this.button_read.Size = new System.Drawing.Size(75, 23);
            this.button_read.TabIndex = 1;
            this.button_read.Text = "读取";
            this.button_read.UseVisualStyleBackColor = true;
            this.button_read.Click += new System.EventHandler(this.button_read_Click);
            // 
            // button_return
            // 
            this.button_return.Location = new System.Drawing.Point(398, 327);
            this.button_return.Name = "button_return";
            this.button_return.Size = new System.Drawing.Size(75, 23);
            this.button_return.TabIndex = 2;
            this.button_return.Text = "返回";
            this.button_return.UseVisualStyleBackColor = true;
            this.button_return.Click += new System.EventHandler(this.button_return_Click);
            // 
            // textBox_tempSet
            // 
            this.textBox_tempSet.Location = new System.Drawing.Point(247, 37);
            this.textBox_tempSet.Name = "textBox_tempSet";
            this.textBox_tempSet.Size = new System.Drawing.Size(100, 21);
            this.textBox_tempSet.TabIndex = 3;
            // 
            // textBox_tempRev
            // 
            this.textBox_tempRev.Location = new System.Drawing.Point(247, 73);
            this.textBox_tempRev.Name = "textBox_tempRev";
            this.textBox_tempRev.Size = new System.Drawing.Size(100, 21);
            this.textBox_tempRev.TabIndex = 4;
            // 
            // textBox_adjust
            // 
            this.textBox_adjust.Location = new System.Drawing.Point(247, 109);
            this.textBox_adjust.Name = "textBox_adjust";
            this.textBox_adjust.Size = new System.Drawing.Size(100, 21);
            this.textBox_adjust.TabIndex = 5;
            // 
            // textBox_fuzzy
            // 
            this.textBox_fuzzy.Location = new System.Drawing.Point(247, 145);
            this.textBox_fuzzy.Name = "textBox_fuzzy";
            this.textBox_fuzzy.Size = new System.Drawing.Size(100, 21);
            this.textBox_fuzzy.TabIndex = 6;
            // 
            // textBox_prop
            // 
            this.textBox_prop.Location = new System.Drawing.Point(247, 181);
            this.textBox_prop.Name = "textBox_prop";
            this.textBox_prop.Size = new System.Drawing.Size(100, 21);
            this.textBox_prop.TabIndex = 7;
            // 
            // textBox_integ
            // 
            this.textBox_integ.Location = new System.Drawing.Point(247, 217);
            this.textBox_integ.Name = "textBox_integ";
            this.textBox_integ.Size = new System.Drawing.Size(100, 21);
            this.textBox_integ.TabIndex = 8;
            // 
            // textBox_power
            // 
            this.textBox_power.Location = new System.Drawing.Point(247, 253);
            this.textBox_power.Name = "textBox_power";
            this.textBox_power.Size = new System.Drawing.Size(100, 21);
            this.textBox_power.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "温度修正值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "超前调整值";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(148, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "模糊系数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(148, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "比例系数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(148, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "积分系数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(148, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "功率系数";
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 401);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.textBox_power);
            this.Controls.Add(this.textBox_integ);
            this.Controls.Add(this.textBox_prop);
            this.Controls.Add(this.textBox_fuzzy);
            this.Controls.Add(this.textBox_adjust);
            this.Controls.Add(this.textBox_tempRev);
            this.Controls.Add(this.textBox_tempSet);
            this.Controls.Add(this.button_return);
            this.Controls.Add(this.button_read);
            this.Controls.Add(this.button_apply);
            this.Name = "FormSetting";
            this.Text = "FormSetting";
            this.Shown += new System.EventHandler(this.FormSetting_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.Button button_read;
        private System.Windows.Forms.Button button_return;
        private System.Windows.Forms.TextBox textBox_tempSet;
        private System.Windows.Forms.TextBox textBox_tempRev;
        private System.Windows.Forms.TextBox textBox_adjust;
        private System.Windows.Forms.TextBox textBox_fuzzy;
        private System.Windows.Forms.TextBox textBox_prop;
        private System.Windows.Forms.TextBox textBox_integ;
        private System.Windows.Forms.TextBox textBox_power;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}