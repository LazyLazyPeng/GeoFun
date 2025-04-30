namespace IGSDownloader
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nudYear = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nuDOY = new System.Windows.Forms.NumericUpDown();
            this.lblLeapYear = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuDOY)).BeginInit();
            this.SuspendLayout();
            // 
            // nudYear
            // 
            this.nudYear.Location = new System.Drawing.Point(38, 7);
            this.nudYear.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nudYear.Minimum = new decimal(new int[] {
            1950,
            0,
            0,
            0});
            this.nudYear.Name = "nudYear";
            this.nudYear.Size = new System.Drawing.Size(59, 23);
            this.nudYear.TabIndex = 1;
            this.nudYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudYear.Value = new decimal(new int[] {
            1950,
            0,
            0,
            0});
            this.nudYear.ValueChanged += new System.EventHandler(this.nudYear_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "年";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "年积日";
            // 
            // nuDOY
            // 
            this.nuDOY.Location = new System.Drawing.Point(175, 7);
            this.nuDOY.Maximum = new decimal(new int[] {
            366,
            0,
            0,
            0});
            this.nuDOY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nuDOY.Name = "nuDOY";
            this.nuDOY.Size = new System.Drawing.Size(59, 23);
            this.nuDOY.TabIndex = 3;
            this.nuDOY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nuDOY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblLeapYear
            // 
            this.lblLeapYear.AutoSize = true;
            this.lblLeapYear.Location = new System.Drawing.Point(255, 9);
            this.lblLeapYear.Name = "lblLeapYear";
            this.lblLeapYear.Size = new System.Drawing.Size(32, 17);
            this.lblLeapYear.TabIndex = 5;
            this.lblLeapYear.Text = "平年";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 450);
            this.Controls.Add(this.lblLeapYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nuDOY);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudYear);
            this.Name = "FrmMain";
            this.Text = "IGS数据下载";
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nuDOY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown nudYear;
        private Label label1;
        private Label label2;
        private NumericUpDown nuDOY;
        private Label lblLeapYear;
    }
}