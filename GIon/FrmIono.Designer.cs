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
            this.rbMultiStationMultiDay = new System.Windows.Forms.RadioButton();
            this.rbSingleStationMultiDay = new System.Windows.Forms.RadioButton();
            this.rbSingleStationSingleDay = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.rbROTI = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbxCutOffAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbxMinArcLen)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.btnOpen.Location = new System.Drawing.Point(597, 12);
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
            this.tbxFolderIn.Size = new System.Drawing.Size(478, 23);
            this.tbxFolderIn.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(626, 381);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 37);
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
            this.tbxMsg.Size = new System.Drawing.Size(572, 406);
            this.tbxMsg.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbMultiStationMultiDay);
            this.groupBox1.Controls.Add(this.rbSingleStationMultiDay);
            this.groupBox1.Controls.Add(this.rbSingleStationSingleDay);
            this.groupBox1.Location = new System.Drawing.Point(597, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 84);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // rbMultiStationMultiDay
            // 
            this.rbMultiStationMultiDay.AutoSize = true;
            this.rbMultiStationMultiDay.Location = new System.Drawing.Point(4, 59);
            this.rbMultiStationMultiDay.Name = "rbMultiStationMultiDay";
            this.rbMultiStationMultiDay.Size = new System.Drawing.Size(95, 18);
            this.rbMultiStationMultiDay.TabIndex = 2;
            this.rbMultiStationMultiDay.Text = "多站多天解";
            this.rbMultiStationMultiDay.UseVisualStyleBackColor = true;
            // 
            // rbSingleStationMultiDay
            // 
            this.rbSingleStationMultiDay.AutoSize = true;
            this.rbSingleStationMultiDay.Checked = true;
            this.rbSingleStationMultiDay.Location = new System.Drawing.Point(4, 36);
            this.rbSingleStationMultiDay.Name = "rbSingleStationMultiDay";
            this.rbSingleStationMultiDay.Size = new System.Drawing.Size(95, 18);
            this.rbSingleStationMultiDay.TabIndex = 1;
            this.rbSingleStationMultiDay.TabStop = true;
            this.rbSingleStationMultiDay.Text = "单站多天解";
            this.rbSingleStationMultiDay.UseVisualStyleBackColor = true;
            // 
            // rbSingleStationSingleDay
            // 
            this.rbSingleStationSingleDay.AutoSize = true;
            this.rbSingleStationSingleDay.Location = new System.Drawing.Point(4, 12);
            this.rbSingleStationSingleDay.Name = "rbSingleStationSingleDay";
            this.rbSingleStationSingleDay.Size = new System.Drawing.Size(95, 18);
            this.rbSingleStationSingleDay.TabIndex = 0;
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
            this.groupBox2.Location = new System.Drawing.Point(597, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(133, 152);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 455);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(740, 22);
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
            this.groupBox3.Location = new System.Drawing.Point(597, 282);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(133, 68);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
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
            // FrmIono
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 477);
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
    }
}