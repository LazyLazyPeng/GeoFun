
namespace GIon
{
    partial class FrmSPMModel
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
            this.tbxLatMax = new System.Windows.Forms.TextBox();
            this.tbxLatMin = new System.Windows.Forms.TextBox();
            this.tbxLonMin = new System.Windows.Forms.TextBox();
            this.tbxLonMax = new System.Windows.Forms.TextBox();
            this.tbxOpen = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbxResolution = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSave = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxLatMax
            // 
            this.tbxLatMax.Location = new System.Drawing.Point(102, 23);
            this.tbxLatMax.Name = "tbxLatMax";
            this.tbxLatMax.Size = new System.Drawing.Size(100, 21);
            this.tbxLatMax.TabIndex = 0;
            this.tbxLatMax.Text = "55";
            this.tbxLatMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxLatMin
            // 
            this.tbxLatMin.Location = new System.Drawing.Point(102, 112);
            this.tbxLatMin.Name = "tbxLatMin";
            this.tbxLatMin.Size = new System.Drawing.Size(100, 21);
            this.tbxLatMin.TabIndex = 1;
            this.tbxLatMin.Text = "15";
            this.tbxLatMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxLonMin
            // 
            this.tbxLonMin.Location = new System.Drawing.Point(18, 69);
            this.tbxLonMin.Name = "tbxLonMin";
            this.tbxLonMin.Size = new System.Drawing.Size(100, 21);
            this.tbxLonMin.TabIndex = 2;
            this.tbxLonMin.Text = "70";
            this.tbxLonMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxLonMax
            // 
            this.tbxLonMax.Location = new System.Drawing.Point(187, 69);
            this.tbxLonMax.Name = "tbxLonMax";
            this.tbxLonMax.Size = new System.Drawing.Size(100, 21);
            this.tbxLonMax.TabIndex = 3;
            this.tbxLonMax.Text = "140";
            this.tbxLonMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxOpen
            // 
            this.tbxOpen.Location = new System.Drawing.Point(66, 25);
            this.tbxOpen.Name = "tbxOpen";
            this.tbxOpen.Size = new System.Drawing.Size(304, 21);
            this.tbxOpen.TabIndex = 4;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(385, 23);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(70, 23);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbxResolution
            // 
            this.tbxResolution.Location = new System.Drawing.Point(390, 145);
            this.tbxResolution.Name = "tbxResolution";
            this.tbxResolution.Size = new System.Drawing.Size(65, 21);
            this.tbxResolution.TabIndex = 6;
            this.tbxResolution.Text = "0.2";
            this.tbxResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(388, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "格网分辨率(°)";
            // 
            // tbxSave
            // 
            this.tbxSave.Location = new System.Drawing.Point(68, 64);
            this.tbxSave.Name = "tbxSave";
            this.tbxSave.Size = new System.Drawing.Size(304, 21);
            this.tbxSave.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(385, 62);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "打开";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(390, 201);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "开始计算";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "输入文件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "输出文件";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxLatMax);
            this.groupBox1.Controls.Add(this.tbxLatMin);
            this.groupBox1.Controls.Add(this.tbxLonMin);
            this.groupBox1.Controls.Add(this.tbxLonMax);
            this.groupBox1.Location = new System.Drawing.Point(66, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 143);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // FrmSPMModel
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 252);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbxSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxResolution);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.tbxOpen);
            this.Name = "FrmSPMModel";
            this.Text = "球谐函数模型计算";
            this.Load += new System.EventHandler(this.FrmSPMModel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxLatMax;
        private System.Windows.Forms.TextBox tbxLatMin;
        private System.Windows.Forms.TextBox tbxLonMin;
        private System.Windows.Forms.TextBox tbxLonMax;
        private System.Windows.Forms.TextBox tbxOpen;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox tbxResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxSave;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}