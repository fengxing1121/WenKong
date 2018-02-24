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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button_cancel = new System.Windows.Forms.Button();
            this.textBox_tpSetM = new System.Windows.Forms.TextBox();
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
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_advanceM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_fuzzyM = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_ratioM = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_integM = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_powerM = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_powerS = new System.Windows.Forms.TextBox();
            this.textBox_integS = new System.Windows.Forms.TextBox();
            this.textBox_ratioS = new System.Windows.Forms.TextBox();
            this.textBox_fuzzyS = new System.Windows.Forms.TextBox();
            this.textBox_advanceS = new System.Windows.Forms.TextBox();
            this.textBox_tpSetS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button_chkParam = new System.Windows.Forms.Button();
            this.checkBox_start = new System.Windows.Forms.CheckBox();
            this.checkBox_shutDown = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpSet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.advance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fuzzy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.integration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.power = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.edit = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(791, 345);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(128, 60);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "返回";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // textBox_tpSetM
            // 
            this.textBox_tpSetM.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_tpSetM.Location = new System.Drawing.Point(97, 444);
            this.textBox_tpSetM.Name = "textBox_tpSetM";
            this.textBox_tpSetM.Size = new System.Drawing.Size(72, 21);
            this.textBox_tpSetM.TabIndex = 6;
            this.textBox_tpSetM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_tpSetM.Enter += new System.EventHandler(this.textBox_tpSet_Enter);
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(791, 277);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(128, 60);
            this.button_add.TabIndex = 7;
            this.button_add.Text = "添加";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 423);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "温度设定值";
            // 
            // buttonNegtive
            // 
            this.buttonNegtive.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonNegtive.Location = new System.Drawing.Point(723, 277);
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
            this.buttonClear.Location = new System.Drawing.Point(859, 141);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(60, 60);
            this.buttonClear.TabIndex = 50;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "删除";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonBack.Location = new System.Drawing.Point(859, 73);
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
            this.buttonPoint.Location = new System.Drawing.Point(859, 209);
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
            this.button0.Location = new System.Drawing.Point(655, 277);
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
            this.button8.Location = new System.Drawing.Point(723, 73);
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
            this.button9.Location = new System.Drawing.Point(791, 73);
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
            this.button4.Location = new System.Drawing.Point(655, 141);
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
            this.button5.Location = new System.Drawing.Point(723, 141);
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
            this.button6.Location = new System.Drawing.Point(791, 141);
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
            this.button7.Location = new System.Drawing.Point(655, 73);
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
            this.button1.Location = new System.Drawing.Point(655, 209);
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
            this.button3.Location = new System.Drawing.Point(791, 209);
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
            this.button2.Location = new System.Drawing.Point(723, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 60);
            this.button2.TabIndex = 38;
            this.button2.TabStop = false;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 423);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 55;
            this.label3.Text = "超前调整值";
            // 
            // textBox_advanceM
            // 
            this.textBox_advanceM.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_advanceM.Location = new System.Drawing.Point(175, 444);
            this.textBox_advanceM.Name = "textBox_advanceM";
            this.textBox_advanceM.Size = new System.Drawing.Size(72, 21);
            this.textBox_advanceM.TabIndex = 54;
            this.textBox_advanceM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_advanceM.Enter += new System.EventHandler(this.textBox_advance_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(260, 423);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 57;
            this.label4.Text = "模糊系数";
            // 
            // textBox_fuzzyM
            // 
            this.textBox_fuzzyM.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_fuzzyM.Location = new System.Drawing.Point(253, 444);
            this.textBox_fuzzyM.Name = "textBox_fuzzyM";
            this.textBox_fuzzyM.Size = new System.Drawing.Size(72, 21);
            this.textBox_fuzzyM.TabIndex = 56;
            this.textBox_fuzzyM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_fuzzyM.Enter += new System.EventHandler(this.textBox_fuzzy_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(338, 423);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 59;
            this.label5.Text = "比例系数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_ratioM
            // 
            this.textBox_ratioM.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_ratioM.Location = new System.Drawing.Point(331, 444);
            this.textBox_ratioM.Name = "textBox_ratioM";
            this.textBox_ratioM.Size = new System.Drawing.Size(72, 21);
            this.textBox_ratioM.TabIndex = 58;
            this.textBox_ratioM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_ratioM.Enter += new System.EventHandler(this.textBox_ratio_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(416, 423);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 61;
            this.label6.Text = "积分系数";
            // 
            // textBox_integM
            // 
            this.textBox_integM.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_integM.Location = new System.Drawing.Point(409, 444);
            this.textBox_integM.Name = "textBox_integM";
            this.textBox_integM.Size = new System.Drawing.Size(72, 21);
            this.textBox_integM.TabIndex = 60;
            this.textBox_integM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_integM.Enter += new System.EventHandler(this.textBox_integ_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(494, 423);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 63;
            this.label7.Text = "功率系数";
            // 
            // textBox_powerM
            // 
            this.textBox_powerM.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_powerM.Location = new System.Drawing.Point(487, 444);
            this.textBox_powerM.Name = "textBox_powerM";
            this.textBox_powerM.Size = new System.Drawing.Size(72, 21);
            this.textBox_powerM.TabIndex = 62;
            this.textBox_powerM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_powerM.Enter += new System.EventHandler(this.textBox_power_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.tpName,
            this.tpSet,
            this.advance,
            this.fuzzy,
            this.ratio,
            this.integration,
            this.power,
            this.edit});
            this.dataGridView1.Location = new System.Drawing.Point(13, 14);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(619, 391);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 444);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 64;
            this.label8.Text = "主槽";
            // 
            // textBox_powerS
            // 
            this.textBox_powerS.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_powerS.Location = new System.Drawing.Point(487, 474);
            this.textBox_powerS.Name = "textBox_powerS";
            this.textBox_powerS.Size = new System.Drawing.Size(72, 21);
            this.textBox_powerS.TabIndex = 71;
            this.textBox_powerS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_powerS.Enter += new System.EventHandler(this.textBox_powerS_Enter);
            // 
            // textBox_integS
            // 
            this.textBox_integS.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_integS.Location = new System.Drawing.Point(409, 474);
            this.textBox_integS.Name = "textBox_integS";
            this.textBox_integS.Size = new System.Drawing.Size(72, 21);
            this.textBox_integS.TabIndex = 70;
            this.textBox_integS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_integS.Enter += new System.EventHandler(this.textBox_integS_Enter);
            // 
            // textBox_ratioS
            // 
            this.textBox_ratioS.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_ratioS.Location = new System.Drawing.Point(331, 474);
            this.textBox_ratioS.Name = "textBox_ratioS";
            this.textBox_ratioS.Size = new System.Drawing.Size(72, 21);
            this.textBox_ratioS.TabIndex = 69;
            this.textBox_ratioS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_ratioS.Enter += new System.EventHandler(this.textBox_ratioS_Enter);
            // 
            // textBox_fuzzyS
            // 
            this.textBox_fuzzyS.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_fuzzyS.Location = new System.Drawing.Point(253, 474);
            this.textBox_fuzzyS.Name = "textBox_fuzzyS";
            this.textBox_fuzzyS.Size = new System.Drawing.Size(72, 21);
            this.textBox_fuzzyS.TabIndex = 68;
            this.textBox_fuzzyS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_fuzzyS.Enter += new System.EventHandler(this.textBox_fuzzyS_Enter);
            // 
            // textBox_advanceS
            // 
            this.textBox_advanceS.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_advanceS.Location = new System.Drawing.Point(175, 474);
            this.textBox_advanceS.Name = "textBox_advanceS";
            this.textBox_advanceS.Size = new System.Drawing.Size(72, 21);
            this.textBox_advanceS.TabIndex = 67;
            this.textBox_advanceS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_advanceS.Enter += new System.EventHandler(this.textBox_advanceS_Enter);
            // 
            // textBox_tpSetS
            // 
            this.textBox_tpSetS.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_tpSetS.Location = new System.Drawing.Point(97, 474);
            this.textBox_tpSetS.Name = "textBox_tpSetS";
            this.textBox_tpSetS.Size = new System.Drawing.Size(72, 21);
            this.textBox_tpSetS.TabIndex = 65;
            this.textBox_tpSetS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_tpSetS.Enter += new System.EventHandler(this.textBox_tpSetS_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 474);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 72;
            this.label9.Text = "辅槽";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(705, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 39);
            this.label10.TabIndex = 73;
            this.label10.Text = "温度点设定";
            // 
            // button_chkParam
            // 
            this.button_chkParam.Location = new System.Drawing.Point(655, 420);
            this.button_chkParam.Name = "button_chkParam";
            this.button_chkParam.Size = new System.Drawing.Size(128, 60);
            this.button_chkParam.TabIndex = 74;
            this.button_chkParam.Text = "查询参数";
            this.button_chkParam.UseVisualStyleBackColor = true;
            this.button_chkParam.Click += new System.EventHandler(this.button_chk_Click);
            // 
            // checkBox_start
            // 
            this.checkBox_start.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_start.Location = new System.Drawing.Point(655, 345);
            this.checkBox_start.Name = "checkBox_start";
            this.checkBox_start.Size = new System.Drawing.Size(128, 60);
            this.checkBox_start.TabIndex = 75;
            this.checkBox_start.Text = "开始";
            this.checkBox_start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_start.UseVisualStyleBackColor = true;
            this.checkBox_start.Click += new System.EventHandler(this.checkBox_start_Click);
            // 
            // checkBox_shutDown
            // 
            this.checkBox_shutDown.AutoSize = true;
            this.checkBox_shutDown.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_shutDown.ForeColor = System.Drawing.Color.Red;
            this.checkBox_shutDown.Location = new System.Drawing.Point(807, 431);
            this.checkBox_shutDown.Name = "checkBox_shutDown";
            this.checkBox_shutDown.Size = new System.Drawing.Size(112, 36);
            this.checkBox_shutDown.TabIndex = 76;
            this.checkBox_shutDown.Text = "实验完成后\r\n关闭计算机";
            this.checkBox_shutDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_shutDown.UseVisualStyleBackColor = true;
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
            // tpName
            // 
            this.tpName.HeaderText = "主槽/辅槽";
            this.tpName.Name = "tpName";
            this.tpName.ReadOnly = true;
            this.tpName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.tpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.tpName.Width = 72;
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
            // edit
            // 
            this.edit.HeaderText = "编辑";
            this.edit.Name = "edit";
            this.edit.ReadOnly = true;
            this.edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.edit.Width = 70;
            // 
            // FormAutoSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 512);
            this.Controls.Add(this.checkBox_shutDown);
            this.Controls.Add(this.checkBox_start);
            this.Controls.Add(this.button_chkParam);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_powerS);
            this.Controls.Add(this.textBox_integS);
            this.Controls.Add(this.textBox_ratioS);
            this.Controls.Add(this.textBox_fuzzyS);
            this.Controls.Add(this.textBox_advanceS);
            this.Controls.Add(this.textBox_tpSetS);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_powerM);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_integM);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_ratioM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_fuzzyM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_advanceM);
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
            this.Controls.Add(this.textBox_tpSetM);
            this.Controls.Add(this.button_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAutoSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAutoSet";
            this.Load += new System.EventHandler(this.FormAutoSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox textBox_tpSetM;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_advanceM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_fuzzyM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_ratioM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_integM;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_powerM;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_powerS;
        private System.Windows.Forms.TextBox textBox_integS;
        private System.Windows.Forms.TextBox textBox_ratioS;
        private System.Windows.Forms.TextBox textBox_fuzzyS;
        private System.Windows.Forms.TextBox textBox_advanceS;
        private System.Windows.Forms.TextBox textBox_tpSetS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button_chkParam;
        private System.Windows.Forms.CheckBox checkBox_start;
        private System.Windows.Forms.CheckBox checkBox_shutDown;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tpSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn advance;
        private System.Windows.Forms.DataGridViewTextBoxColumn fuzzy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratio;
        private System.Windows.Forms.DataGridViewTextBoxColumn integration;
        private System.Windows.Forms.DataGridViewTextBoxColumn power;
        private System.Windows.Forms.DataGridViewLinkColumn edit;
    }
}