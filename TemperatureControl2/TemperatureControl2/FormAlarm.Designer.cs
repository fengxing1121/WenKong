namespace TemperatureControl2
{
    partial class FormAlarm
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
            this.textBox_errMessage = new System.Windows.Forms.TextBox();
            this.label_errTime = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_errMessage
            // 
            this.textBox_errMessage.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_errMessage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_errMessage.Location = new System.Drawing.Point(51, 35);
            this.textBox_errMessage.Multiline = true;
            this.textBox_errMessage.Name = "textBox_errMessage";
            this.textBox_errMessage.Size = new System.Drawing.Size(451, 133);
            this.textBox_errMessage.TabIndex = 0;
            // 
            // label_errTime
            // 
            this.label_errTime.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_errTime.Location = new System.Drawing.Point(51, 212);
            this.label_errTime.Name = "label_errTime";
            this.label_errTime.Size = new System.Drawing.Size(451, 23);
            this.label_errTime.TabIndex = 1;
            this.label_errTime.Text = "0";
            this.label_errTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(403, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 35);
            this.button1.TabIndex = 2;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // FormAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 303);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_errTime);
            this.Controls.Add(this.textBox_errMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAlarm";
            this.Text = "FormAlarm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_errMessage;
        private System.Windows.Forms.Label label_errTime;
        private System.Windows.Forms.Button button1;
    }
}