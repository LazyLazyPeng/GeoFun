
namespace GIon
{
    partial class FrmObsExtrator
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
            this.chkBoxObsTypes = new System.Windows.Forms.CheckedListBox();
            this.chkBoxFiles = new System.Windows.Forms.CheckedListBox();
            this.btnFileAll = new System.Windows.Forms.Button();
            this.btnFileOthers = new System.Windows.Forms.Button();
            this.btnFileNone = new System.Windows.Forms.Button();
            this.btnObsNone = new System.Windows.Forms.Button();
            this.btnObsOthers = new System.Windows.Forms.Button();
            this.btnObsAll = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkBoxObsTypes
            // 
            this.chkBoxObsTypes.FormattingEnabled = true;
            this.chkBoxObsTypes.Location = new System.Drawing.Point(563, 48);
            this.chkBoxObsTypes.Name = "chkBoxObsTypes";
            this.chkBoxObsTypes.Size = new System.Drawing.Size(205, 256);
            this.chkBoxObsTypes.TabIndex = 0;
            // 
            // chkBoxFiles
            // 
            this.chkBoxFiles.FormattingEnabled = true;
            this.chkBoxFiles.Location = new System.Drawing.Point(2, 48);
            this.chkBoxFiles.Name = "chkBoxFiles";
            this.chkBoxFiles.Size = new System.Drawing.Size(541, 382);
            this.chkBoxFiles.TabIndex = 1;
            // 
            // btnFileAll
            // 
            this.btnFileAll.Location = new System.Drawing.Point(400, 441);
            this.btnFileAll.Name = "btnFileAll";
            this.btnFileAll.Size = new System.Drawing.Size(43, 27);
            this.btnFileAll.TabIndex = 3;
            this.btnFileAll.Text = "全选";
            this.btnFileAll.UseVisualStyleBackColor = true;
            // 
            // btnFileOthers
            // 
            this.btnFileOthers.Location = new System.Drawing.Point(450, 441);
            this.btnFileOthers.Name = "btnFileOthers";
            this.btnFileOthers.Size = new System.Drawing.Size(43, 27);
            this.btnFileOthers.TabIndex = 4;
            this.btnFileOthers.Text = "反选";
            this.btnFileOthers.UseVisualStyleBackColor = true;
            // 
            // btnFileNone
            // 
            this.btnFileNone.Location = new System.Drawing.Point(500, 441);
            this.btnFileNone.Name = "btnFileNone";
            this.btnFileNone.Size = new System.Drawing.Size(43, 27);
            this.btnFileNone.TabIndex = 5;
            this.btnFileNone.Text = "清除";
            this.btnFileNone.UseVisualStyleBackColor = true;
            // 
            // btnObsNone
            // 
            this.btnObsNone.Location = new System.Drawing.Point(726, 321);
            this.btnObsNone.Name = "btnObsNone";
            this.btnObsNone.Size = new System.Drawing.Size(43, 27);
            this.btnObsNone.TabIndex = 8;
            this.btnObsNone.Text = "清除";
            this.btnObsNone.UseVisualStyleBackColor = true;
            this.btnObsNone.Click += new System.EventHandler(this.btnObsNone_Click);
            // 
            // btnObsOthers
            // 
            this.btnObsOthers.Location = new System.Drawing.Point(675, 321);
            this.btnObsOthers.Name = "btnObsOthers";
            this.btnObsOthers.Size = new System.Drawing.Size(43, 27);
            this.btnObsOthers.TabIndex = 7;
            this.btnObsOthers.Text = "反选";
            this.btnObsOthers.UseVisualStyleBackColor = true;
            this.btnObsOthers.Click += new System.EventHandler(this.btnObsOthers_Click);
            // 
            // btnObsAll
            // 
            this.btnObsAll.Location = new System.Drawing.Point(625, 321);
            this.btnObsAll.Name = "btnObsAll";
            this.btnObsAll.Size = new System.Drawing.Size(43, 27);
            this.btnObsAll.TabIndex = 6;
            this.btnObsAll.Text = "全选";
            this.btnObsAll.UseVisualStyleBackColor = true;
            this.btnObsAll.Click += new System.EventHandler(this.btnObsAll_Click);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(642, 391);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 27);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(642, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmObsExtrator
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(775, 488);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnObsNone);
            this.Controls.Add(this.btnObsOthers);
            this.Controls.Add(this.btnObsAll);
            this.Controls.Add(this.btnFileNone);
            this.Controls.Add(this.btnFileOthers);
            this.Controls.Add(this.btnFileAll);
            this.Controls.Add(this.chkBoxFiles);
            this.Controls.Add(this.chkBoxObsTypes);
            this.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmObsExtrator";
            this.Text = "观测值提取";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkBoxObsTypes;
        private System.Windows.Forms.CheckedListBox chkBoxFiles;
        private System.Windows.Forms.Button btnFileAll;
        private System.Windows.Forms.Button btnFileOthers;
        private System.Windows.Forms.Button btnFileNone;
        private System.Windows.Forms.Button btnObsNone;
        private System.Windows.Forms.Button btnObsOthers;
        private System.Windows.Forms.Button btnObsAll;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}