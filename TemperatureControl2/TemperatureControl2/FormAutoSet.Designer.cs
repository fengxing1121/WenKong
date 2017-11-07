namespace TemperatureControl2
{
    partial class FormAutoSet
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
            this.button_start = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.textBox_tpSet = new System.Windows.Forms.TextBox();
            this.button_add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonNegtive = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_tpAdjust = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_advance = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_fuzzy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_ratio = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_integ = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_power = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpSet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpAdjust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fuzzy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.integration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.power = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(645, 298);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(128, 60);
            this.button_start.TabIndex = 3;
            this.button_start.Text = "开始";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(781, 298);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(128, 60);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "返回";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // textBox_tpSet
            // 
            this.textBox_tpSet.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_tpSet.Location = new System.Drawing.Point(76, 319);
            this.textBox_tpSet.Name = "textBox_tpSet";
            this.textBox_tpSet.Size = new System.Drawing.Size(72, 21);
            this.textBox_tpSet.TabIndex = 6;
            this.textBox_tpSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_tpSet.Enter += new System.EventHandler(this.textBox_tpSet_Enter);
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(781, 230);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(126, 60);
            this.button_add.TabIndex = 7;
            this.button_add.Text = "添加";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "温度设定值";
            // 
            // buttonNegtive
            // 
            this.buttonNegtive.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonNegtive.Location = new System.Drawing.Point(713, 230);
            this.buttonNegtive.Name = "buttonNegtive";
            this.buttonNegtive.Size = new System.Drawing.Size(60, 60);
            this.buttonNegtive.TabIndex = 51;
            this.buttonNegtive.TabStop = false;
            this.buttonNegtive.Text = "-/+";
            this.buttonNegtive.UseVisualStyleBackColor = true;
            this.buttonNegtive.Click += new System.EventHandler(this.buttonNegtive_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonClear.Location = new System.Drawing.Point(849, 94);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(60, 60);
            this.buttonClear.TabIndex = 50;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "Del";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonBack.Location = new System.Drawing.Point(849, 26);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(60, 60);
            this.buttonBack.TabIndex = 49;
            this.buttonBack.TabStop = false;
            this.buttonBack.Text = "←";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonPoint
            // 
            this.buttonPoint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonPoint.Location = new System.Drawing.Point(849, 162);
            this.buttonPoint.Name = "buttonPoint";
            this.buttonPoint.Size = new System.Drawing.Size(60, 60);
            this.buttonPoint.TabIndex = 48;
            this.buttonPoint.TabStop = false;
            this.buttonPoint.Text = ".";
            this.buttonPoint.UseVisualStyleBackColor = true;
            this.buttonPoint.Click += new System.EventHandler(this.buttonPoint_Click);
            // 
            // button0
            // 
            this.button0.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button0.Location = new System.Drawing.Point(645, 230);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(60, 60);
            this.button0.TabIndex = 47;
            this.button0.TabStop = false;
            this.button0.Text = "0";
            this.button0.UseVisualStyleBackColor = true;
            this.button0.Click += new System.EventHandler(this.button0_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button8.Location = new System.Drawing.Point(713, 26);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(60, 60);
            this.button8.TabIndex = 46;
            this.button8.TabStop = false;
            this.button8.Text = "8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button9.Location = new System.Drawing.Point(781, 26);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(60, 60);
            this.button9.TabIndex = 45;
            this.button9.TabStop = false;
            this.button9.Text = "9";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(645, 94);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(60, 60);
            this.button4.TabIndex = 44;
            this.button4.TabStop = false;
            this.button4.Text = "4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(713, 94);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 60);
            this.button5.TabIndex = 43;
            this.button5.TabStop = false;
            this.button5.Text = "5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.Location = new System.Drawing.Point(781, 94);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(60, 60);
            this.button6.TabIndex = 42;
            this.button6.TabStop = false;
            this.button6.Text = "6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.Location = new System.Drawing.Point(645, 26);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(60, 60);
            this.button7.TabIndex = 41;
            this.button7.TabStop = false;
            this.button7.Text = "7";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(645, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 60);
            this.button1.TabIndex = 40;
            this.button1.TabStop = false;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(781, 162);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 60);
            this.button3.TabIndex = 39;
            this.button3.TabStop = false;
            this.button3.Text = "3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(713, 162);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 60);
            this.button2.TabIndex = 38;
            this.button2.TabStop = false;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 298);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 53;
            this.label2.Text = "温度修正值";
            // 
            // textBox_tpAdjust
            // 
            this.textBox_tpAdjust.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_tpAdjust.Location = new System.Drawing.Point(154, 319);
            this.textBox_tpAdjust.Name = "textBox_tpAdjust";
            this.textBox_tpAdjust.Size = new System.Drawing.Size(72, 21);
            this.textBox_tpAdjust.TabIndex = 52;
            this.textBox_tpAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_tpAdjust.Enter += new System.EventHandler(this.textBox_tpAdjust_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 55;
            this.label3.Text = "超前调整值";
            // 
            // textBox_advance
            // 
            this.textBox_advance.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_advance.Location = new System.Drawing.Point(232, 319);
            this.textBox_advance.Name = "textBox_advance";
            this.textBox_advance.Size = new System.Drawing.Size(72, 21);
            this.textBox_advance.TabIndex = 54;
            this.textBox_advance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_advance.Enter += new System.EventHandler(this.textBox_advance_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(317, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 57;
            this.label4.Text = "模糊系数";
            // 
            // textBox_fuzzy
            // 
            this.textBox_fuzzy.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_fuzzy.Location = new System.Drawing.Point(310, 319);
            this.textBox_fuzzy.Name = "textBox_fuzzy";
            this.textBox_fuzzy.Size = new System.Drawing.Size(72, 21);
            this.textBox_fuzzy.TabIndex = 56;
            this.textBox_fuzzy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_fuzzy.Enter += new System.EventHandler(this.textBox_fuzzy_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(395, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 59;
            this.label5.Text = "比例系数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_ratio
            // 
            this.textBox_ratio.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_ratio.Location = new System.Drawing.Point(388, 319);
            this.textBox_ratio.Name = "textBox_ratio";
            this.textBox_ratio.Size = new System.Drawing.Size(72, 21);
            this.textBox_ratio.TabIndex = 58;
            this.textBox_ratio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_ratio.Enter += new System.EventHandler(this.textBox_ratio_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(473, 298);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 61;
            this.label6.Text = "积分系数";
            // 
            // textBox_integ
            // 
            this.textBox_integ.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_integ.Location = new System.Drawing.Point(466, 319);
            this.textBox_integ.Name = "textBox_integ";
            this.textBox_integ.Size = new System.Drawing.Size(72, 21);
            this.textBox_integ.TabIndex = 60;
            this.textBox_integ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_integ.Enter += new System.EventHandler(this.textBox_integ_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(551, 298);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 63;
            this.label7.Text = "功率系数";
            // 
            // textBox_power
            // 
            this.textBox_power.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_power.Location = new System.Drawing.Point(544, 319);
            this.textBox_power.Name = "textBox_power";
            this.textBox_power.Size = new System.Drawing.Size(72, 21);
            this.textBox_power.TabIndex = 62;
            this.textBox_power.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_power.Enter += new System.EventHandler(this.textBox_power_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.tpSet,
            this.tpAdjust,
            this.advance,
            this.fuzzy,
            this.ratio,
            this.integration,
            this.power});
            this.dataGridView1.Location = new System.Drawing.Point(29, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(587, 249);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // index
            // 
            this.index.HeaderText = "序号";
            this.index.Name = "index";
            this.index.ReadOnly = true;
            this.index.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.index.Width = 40;
            // 
            // tpSet
            // 
            this.tpSet.HeaderText = "温度设定值";
            this.tpSet.Name = "tpSet";
            this.tpSet.ReadOnly = true;
            this.tpSet.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tpSet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tpSet.Width = 72;
            // 
            // tpAdjust
            // 
            this.tpAdjust.HeaderText = "温度修正值";
            this.tpAdjust.Name = "tpAdjust";
            this.tpAdjust.ReadOnly = true;
            this.tpAdjust.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tpAdjust.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tpAdjust.Width = 72;
            // 
            // advance
            // 
            this.advance.HeaderText = "超前调整值";
            this.advance.Name = "advance";
            this.advance.ReadOnly = true;
            this.advance.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.advance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.advance.Width = 72;
            // 
            // fuzzy
            // 
            this.fuzzy.HeaderText = "模糊系数";
            this.fuzzy.Name = "fuzzy";
            this.fuzzy.ReadOnly = true;
            this.fuzzy.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.fuzzy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.fuzzy.Width = 72;
            // 
            // ratio
            // 
            this.ratio.HeaderText = "比例系数";
            this.ratio.Name = "ratio";
            this.ratio.ReadOnly = true;
            this.ratio.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ratio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ratio.Width = 72;
            // 
            // integration
            // 
            this.integration.HeaderText = "积分系数";
            this.integration.Name = "integration";
            this.integration.ReadOnly = true;
            this.integration.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.integration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.integration.Width = 72;
            // 
            // power
            // 
            this.power.HeaderText = "功率系数";
            this.power.Name = "power";
            this.power.ReadOnly = true;
            this.power.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.power.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.power.Width = 72;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 298);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 36);
            this.label8.TabIndex = 64;
            this.label8.Text = "主槽\r\n\r\n参数";
            // 
            // FormAutoSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 382);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_power);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_integ);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_ratio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_fuzzy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_advance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_tpAdjust);
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
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.textBox_tpSet);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAutoSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormAutoSet";
            this.Load += new System.EventHandler(this.FormAutoSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox textBox_tpSet;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonNegtive;
        private System.Windows.Forms.Button buttonClear;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_tpAdjust;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_advance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_fuzzy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_ratio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_integ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_power;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpAdjust;
        private System.Windows.Forms.DataGridViewTextBoxColumn advance;
        private System.Windows.Forms.DataGridViewTextBoxColumn fuzzy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratio;
        private System.Windows.Forms.DataGridViewTextBoxColumn integration;
        private System.Windows.Forms.DataGridViewTextBoxColumn power;
    }
}