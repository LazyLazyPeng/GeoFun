namespace GIon
{
    partial class FrmIono
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbxFolderIn = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbxMsg = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbMultiStationSingleDay = new System.Windows.Forms.RadioButton();
            this.rbMultiStationMultiDay = new System.Windows.Forms.RadioButton();
            this.rbSingleStationMultiDay = new System.Windows.Forms.RadioButton();
            this.rbSingleStationSingleDay = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbROTI = new System.Windows.Forms.RadioButton();
            this.rbOriginalMeas = new System.Windows.Forms.RadioButton();
            this.rbSmooth = new System.Windows.Forms.RadioButton();
            this.rbDifference = new System.Windows.Forms.RadioButton();
            this.rbPolynomial = new System.Windows.Forms.RadioButton();
            this.tbxCutOffAngle = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxMinArcLen = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkListBoxOutput = new System.Windows.Forms.CheckedListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.btnSelAll = new System.Windows.Forms.Button();
            this.btnSelOthers = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbxCutOffAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbxMinArcLen)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "待解算文件夹";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(812, 14);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(78, 25);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "打开...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbxFolderIn
            // 
            this.tbxFolderIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFolderIn.Location = new System.Drawing.Point(109, 14);
            this.tbxFolderIn.Name = "tbxFolderIn";
            this.tbxFolderIn.Size = new System.Drawing.Size(683, 23);
            this.tbxFolderIn.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(923, 488);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(117, 44);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "开始解算";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbxMsg
            // 
            this.tbxMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxMsg.Location = new System.Drawing.Point(15, 49);
            this.tbxMsg.Multiline = true;
            this.tbxMsg.Name = "tbxMsg";
            this.tbxMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxMsg.Size = new System.Drawing.Size(777, 605);
            this.tbxMsg.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbMultiStationSingleDay);
            this.groupBox1.Controls.Add(this.rbMultiStationMultiDay);
            this.groupBox1.Controls.Add(this.rbSingleStationMultiDay);
            this.groupBox1.Controls.Add(this.rbSingleStationSingleDay);
            this.groupBox1.Location = new System.Drawing.Point(812, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 121);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // rbMultiStationSingleDay
            // 
            this.rbMultiStationSingleDay.AutoSize = true;
            this.rbMultiStationSingleDay.Location = new System.Drawing.Point(6, 68);
            this.rbMultiStationSingleDay.Name = "rbMultiStationSingleDay";
            this.rbMultiStationSingleDay.Size = new System.Drawing.Size(95, 18);
            this.rbMultiStationSingleDay.TabIndex = 3;
            this.rbMultiStationSingleDay.Text = "多站单天解";
            this.rbMultiStationSingleDay.UseVisualStyleBackColor = true;
            // 
            // rbMultiStationMultiDay
            // 
            this.rbMultiStationMultiDay.AutoSize = true;
            this.rbMultiStationMultiDay.Location = new System.Drawing.Point(6, 96);
            this.rbMultiStationMultiDay.Name = "rbMultiStationMultiDay";
            this.rbMultiStationMultiDay.Size = new System.Drawing.Size(95, 18);
            this.rbMultiStationMultiDay.TabIndex = 2;
            this.rbMultiStationMultiDay.Text = "多站多天解";
            this.rbMultiStationMultiDay.UseVisualStyleBackColor = true;
            // 
            // rbSingleStationMultiDay
            // 
            this.rbSingleStationMultiDay.AutoSize = true;
            this.rbSingleStationMultiDay.Location = new System.Drawing.Point(6, 40);
            this.rbSingleStationMultiDay.Name = "rbSingleStationMultiDay";
            this.rbSingleStationMultiDay.Size = new System.Drawing.Size(95, 18);
            this.rbSingleStationMultiDay.TabIndex = 1;
            this.rbSingleStationMultiDay.Text = "单站多天解";
            this.rbSingleStationMultiDay.UseVisualStyleBackColor = true;
            // 
            // rbSingleStationSingleDay
            // 
            this.rbSingleStationSingleDay.AutoSize = true;
            this.rbSingleStationSingleDay.Checked = true;
            this.rbSingleStationSingleDay.Location = new System.Drawing.Point(6, 12);
            this.rbSingleStationSingleDay.Name = "rbSingleStationSingleDay";
            this.rbSingleStationSingleDay.Size = new System.Drawing.Size(95, 18);
            this.rbSingleStationSingleDay.TabIndex = 0;
            this.rbSingleStationSingleDay.TabStop = true;
            this.rbSingleStationSingleDay.Text = "单站单天解";
            this.rbSingleStationSingleDay.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbROTI);
            this.groupBox2.Controls.Add(this.rbOriginalMeas);
            this.groupBox2.Controls.Add(this.rbSmooth);
            this.groupBox2.Controls.Add(this.rbDifference);
            this.groupBox2.Controls.Add(this.rbPolynomial);
            this.groupBox2.Location = new System.Drawing.Point(812, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 152);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // rbROTI
            // 
            this.rbROTI.AutoSize = true;
            this.rbROTI.Location = new System.Drawing.Point(6, 120);
            this.rbROTI.Name = "rbROTI";
            this.rbROTI.Size = new System.Drawing.Size(53, 18);
            this.rbROTI.TabIndex = 4;
            this.rbROTI.Text = "ROTI";
            this.rbROTI.UseVisualStyleBackColor = true;
            // 
            // rbOriginalMeas
            // 
            this.rbOriginalMeas.AutoSize = true;
            this.rbOriginalMeas.Checked = true;
            this.rbOriginalMeas.Location = new System.Drawing.Point(6, 18);
            this.rbOriginalMeas.Name = "rbOriginalMeas";
            this.rbOriginalMeas.Size = new System.Drawing.Size(95, 18);
            this.rbOriginalMeas.TabIndex = 3;
            this.rbOriginalMeas.TabStop = true;
            this.rbOriginalMeas.Text = "原始观测值";
            this.rbOriginalMeas.UseVisualStyleBackColor = true;
            // 
            // rbSmooth
            // 
            this.rbSmooth.AutoSize = true;
            this.rbSmooth.Location = new System.Drawing.Point(6, 96);
            this.rbSmooth.Name = "rbSmooth";
            this.rbSmooth.Size = new System.Drawing.Size(102, 18);
            this.rbSmooth.TabIndex = 2;
            this.rbSmooth.Text = "5阶滑动平均";
            this.rbSmooth.UseVisualStyleBackColor = true;
            // 
            // rbDifference
            // 
            this.rbDifference.AutoSize = true;
            this.rbDifference.Location = new System.Drawing.Point(6, 70);
            this.rbDifference.Name = "rbDifference";
            this.rbDifference.Size = new System.Drawing.Size(109, 18);
            this.rbDifference.TabIndex = 1;
            this.rbDifference.Text = "相位二阶差分";
            this.rbDifference.UseVisualStyleBackColor = true;
            // 
            // rbPolynomial
            // 
            this.rbPolynomial.AutoSize = true;
            this.rbPolynomial.Location = new System.Drawing.Point(6, 44);
            this.rbPolynomial.Name = "rbPolynomial";
            this.rbPolynomial.Size = new System.Drawing.Size(95, 18);
            this.rbPolynomial.TabIndex = 0;
            this.rbPolynomial.Text = "多项式拟合";
            this.rbPolynomial.UseVisualStyleBackColor = true;
            // 
            // tbxCutOffAngle
            // 
            this.tbxCutOffAngle.Location = new System.Drawing.Point(85, 12);
            this.tbxCutOffAngle.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.tbxCutOffAngle.Name = "tbxCutOffAngle";
            this.tbxCutOffAngle.Size = new System.Drawing.Size(39, 23);
            this.tbxCutOffAngle.TabIndex = 7;
            this.tbxCutOffAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxCutOffAngle.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "截止高度角";
            // 
            // tbxMinArcLen
            // 
            this.tbxMinArcLen.Location = new System.Drawing.Point(76, 38);
            this.tbxMinArcLen.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.tbxMinArcLen.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.tbxMinArcLen.Name = "tbxMinArcLen";
            this.tbxMinArcLen.Size = new System.Drawing.Size(48, 23);
            this.tbxMinArcLen.TabIndex = 9;
            this.tbxMinArcLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxMinArcLen.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "最短弧段长";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 654);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1193, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(27, 17);
            this.statusLabel.Text = "1/1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tbxMinArcLen);
            this.groupBox3.Controls.Add(this.tbxCutOffAngle);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(812, 326);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(159, 68);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnSelOthers);
            this.groupBox4.Controls.Add(this.btnSelAll);
            this.groupBox4.Controls.Add(this.chkListBoxOutput);
            this.groupBox4.Location = new System.Drawing.Point(990, 184);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(183, 210);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "输出项";
            // 
            // chkListBoxOutput
            // 
            this.chkListBoxOutput.FormattingEnabled = true;
            this.chkListBoxOutput.Items.AddRange(new object[] {
            "L",
            "B",
            "H",
            "GPST",
            "STEC",
            "VTEC",
            "RTEC",
            "DCB"});
            this.chkListBoxOutput.Location = new System.Drawing.Point(15, 22);
            this.chkListBoxOutput.Name = "chkListBoxOutput";
            this.chkListBoxOutput.Size = new System.Drawing.Size(162, 148);
            this.chkListBoxOutput.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.radioButton2);
            this.groupBox5.Controls.Add(this.radioButton1);
            this.groupBox5.Location = new System.Drawing.Point(990, 49);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(183, 119);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "数学模型";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(15, 38);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 18);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.Text = "多项式模型";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(15, 84);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(109, 18);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.Text = "球谐函数模型";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // btnSelAll
            // 
            this.btnSelAll.Location = new System.Drawing.Point(47, 178);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(43, 23);
            this.btnSelAll.TabIndex = 1;
            this.btnSelAll.Text = "全选";
            this.btnSelAll.UseVisualStyleBackColor = true;
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnSelOthers
            // 
            this.btnSelOthers.Location = new System.Drawing.Point(111, 178);
            this.btnSelOthers.Name = "btnSelOthers";
            this.btnSelOthers.Size = new System.Drawing.Size(43, 23);
            this.btnSelOthers.TabIndex = 2;
            this.btnSelOthers.Text = "反选";
            this.btnSelOthers.UseVisualStyleBackColor = true;
            this.btnSelOthers.Click += new System.EventHandler(this.btnSelOthers_Click);
            // 
            // FrmIono
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 676);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbxMsg);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbxFolderIn);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmIono";
            this.Text = "电离层计算程序";
            this.Load += new System.EventHandler(this.FrmIono_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbxCutOffAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbxMinArcLen)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox tbxFolderIn;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbxMsg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbMultiStationMultiDay;
        private System.Windows.Forms.RadioButton rbSingleStationMultiDay;
        private System.Windows.Forms.RadioButton rbSingleStationSingleDay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbDifference;
        private System.Windows.Forms.RadioButton rbPolynomial;
        private System.Windows.Forms.NumericUpDown tbxCutOffAngle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown tbxMinArcLen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbSmooth;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbOriginalMeas;
        private System.Windows.Forms.RadioButton rbROTI;
        private System.Windows.Forms.RadioButton rbMultiStationSingleDay;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckedListBox chkListBoxOutput;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btnSelOthers;
        private System.Windows.Forms.Button btnSelAll;
    }
}