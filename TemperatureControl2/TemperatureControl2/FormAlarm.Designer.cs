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
            this.components = new System.ComponentModel.Container();
            this.label_errMessage = new System.Windows.Forms.Label();
            this.label_errTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_errMessage
            // 
            this.label_errMessage.Location = new System.Drawing.Point(71, 56);
            this.label_errMessage.Name = "label_errMessage";
            this.label_errMessage.Size = new System.Drawing.Size(100, 23);
            this.label_errMessage.TabIndex = 0;
            this.label_errMessage.Text = "label_errMessage";
            // 
            // label_errTime
            // 
            this.label_errTime.Location = new System.Drawing.Point(73, 123);
            this.label_errTime.Name = "label_errTime";
            this.label_errTime.Size = new System.Drawing.Size(100, 23);
            this.label_errTime.TabIndex = 1;
            this.label_errTime.Text = "label_errTime";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(404, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 315);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_errTime);
            this.Controls.Add(this.label_errMessage);
            this.Name = "FormAlarm";
            this.Text = "FormAlarm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_errMessage;
        private System.Windows.Forms.Label label_errTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}