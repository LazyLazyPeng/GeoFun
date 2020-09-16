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
            this.rbPolynomial = new System.Windows.Forms.RadioButton();
            this.rbDifference = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "待解算文件夹";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(529, 20);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbxFolderIn
            // 
            this.tbxFolderIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFolderIn.Location = new System.Drawing.Point(109, 20);
            this.tbxFolderIn.Name = "tbxFolderIn";
            this.tbxFolderIn.Size = new System.Drawing.Size(408, 23);
            this.tbxFolderIn.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(529, 53);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
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
            this.tbxMsg.Size = new System.Drawing.Size(502, 293);
            this.tbxMsg.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbMultiStationMultiDay);
            this.groupBox1.Controls.Add(this.rbSingleStationMultiDay);
            this.groupBox1.Controls.Add(this.rbSingleStationSingleDay);
            this.groupBox1.Location = new System.Drawing.Point(522, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(125, 114);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // rbMultiStationMultiDay
            // 
            this.rbMultiStationMultiDay.AutoSize = true;
            this.rbMultiStationMultiDay.Location = new System.Drawing.Point(4, 81);
            this.rbMultiStationMultiDay.Name = "rbMultiStationMultiDay";
            this.rbMultiStationMultiDay.Size = new System.Drawing.Size(95, 18);
            this.rbMultiStationMultiDay.TabIndex = 2;
            this.rbMultiStationMultiDay.Text = "多站多天解";
            this.rbMultiStationMultiDay.UseVisualStyleBackColor = true;
            // 
            // rbSingleStationMultiDay
            // 
            this.rbSingleStationMultiDay.AutoSize = true;
            this.rbSingleStationMultiDay.Location = new System.Drawing.Point(4, 45);
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
            this.rbSingleStationSingleDay.Location = new System.Drawing.Point(4, 12);
            this.rbSingleStationSingleDay.Name = "rbSingleStationSingleDay";
            this.rbSingleStationSingleDay.Size = new System.Drawing.Size(95, 18);
            this.rbSingleStationSingleDay.TabIndex = 0;
            this.rbSingleStationSingleDay.TabStop = true;
            this.rbSingleStationSingleDay.Text = "单站单天解";
            this.rbSingleStationSingleDay.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbDifference);
            this.groupBox2.Controls.Add(this.rbPolynomial);
            this.groupBox2.Location = new System.Drawing.Point(522, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(125, 68);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // rbPolynomial
            // 
            this.rbPolynomial.AutoSize = true;
            this.rbPolynomial.Checked = true;
            this.rbPolynomial.Location = new System.Drawing.Point(6, 13);
            this.rbPolynomial.Name = "rbPolynomial";
            this.rbPolynomial.Size = new System.Drawing.Size(95, 18);
            this.rbPolynomial.TabIndex = 0;
            this.rbPolynomial.TabStop = true;
            this.rbPolynomial.Text = "多项式拟合";
            this.rbPolynomial.UseVisualStyleBackColor = true;
            // 
            // rbDifference
            // 
            this.rbDifference.AutoSize = true;
            this.rbDifference.Location = new System.Drawing.Point(6, 41);
            this.rbDifference.Name = "rbDifference";
            this.rbDifference.Size = new System.Drawing.Size(109, 18);
            this.rbDifference.TabIndex = 1;
            this.rbDifference.Text = "相位二阶差分";
            this.rbDifference.UseVisualStyleBackColor = true;
            // 
            // FrmIono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 354);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}