namespace GeoFun
{
    partial class FrmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.四参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.七参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高斯投影ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.大地主题解算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.大地主题正算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.大地主题反算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.椭球ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.椭球膨胀ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.四参数ToolStripMenuItem,
            this.七参数ToolStripMenuItem,
            this.高斯投影ToolStripMenuItem,
            this.大地主题解算ToolStripMenuItem,
            this.椭球ToolStripMenuItem,
            this.高程ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(933, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 503);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(933, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // 四参数ToolStripMenuItem
            // 
            this.四参数ToolStripMenuItem.Name = "四参数ToolStripMenuItem";
            this.四参数ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.四参数ToolStripMenuItem.Text = "四参数";
            // 
            // 七参数ToolStripMenuItem
            // 
            this.七参数ToolStripMenuItem.Name = "七参数ToolStripMenuItem";
            this.七参数ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.七参数ToolStripMenuItem.Text = "七参数";
            // 
            // 高斯投影ToolStripMenuItem
            // 
            this.高斯投影ToolStripMenuItem.Name = "高斯投影ToolStripMenuItem";
            this.高斯投影ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.高斯投影ToolStripMenuItem.Text = "高斯投影";
            // 
            // 大地主题解算ToolStripMenuItem
            // 
            this.大地主题解算ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.大地主题正算ToolStripMenuItem,
            this.大地主题反算ToolStripMenuItem});
            this.大地主题解算ToolStripMenuItem.Name = "大地主题解算ToolStripMenuItem";
            this.大地主题解算ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.大地主题解算ToolStripMenuItem.Text = "大地主题解算";
            // 
            // 大地主题正算ToolStripMenuItem
            // 
            this.大地主题正算ToolStripMenuItem.Name = "大地主题正算ToolStripMenuItem";
            this.大地主题正算ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.大地主题正算ToolStripMenuItem.Text = "大地主题正算";
            // 
            // 大地主题反算ToolStripMenuItem
            // 
            this.大地主题反算ToolStripMenuItem.Name = "大地主题反算ToolStripMenuItem";
            this.大地主题反算ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.大地主题反算ToolStripMenuItem.Text = "大地主题反算";
            // 
            // 椭球ToolStripMenuItem
            // 
            this.椭球ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.椭球膨胀ToolStripMenuItem});
            this.椭球ToolStripMenuItem.Name = "椭球ToolStripMenuItem";
            this.椭球ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.椭球ToolStripMenuItem.Text = "椭球";
            // 
            // 椭球膨胀ToolStripMenuItem
            // 
            this.椭球膨胀ToolStripMenuItem.Name = "椭球膨胀ToolStripMenuItem";
            this.椭球膨胀ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.椭球膨胀ToolStripMenuItem.Text = "椭球膨胀";
            // 
            // 高程ToolStripMenuItem
            // 
            this.高程ToolStripMenuItem.Name = "高程ToolStripMenuItem";
            this.高程ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.高程ToolStripMenuItem.Text = "高程";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 525);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmMain";
            this.Text = "坐标工具箱";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 四参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 七参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高斯投影ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 大地主题解算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 大地主题正算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 大地主题反算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 椭球ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 椭球膨胀ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高程ToolStripMenuItem;
    }
}