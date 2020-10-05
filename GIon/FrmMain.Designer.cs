
namespace GIon
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuIonosphere = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreateTask = new System.Windows.Forms.ToolStripMenuItem();
            this.dgTasks = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.coor3BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coor3BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuIonosphere});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(697, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuIonosphere
            // 
            this.menuIonosphere.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCreateTask});
            this.menuIonosphere.Name = "menuIonosphere";
            this.menuIonosphere.Size = new System.Drawing.Size(95, 21);
            this.menuIonosphere.Text = "电离层解算(&T)";
            // 
            // menuCreateTask
            // 
            this.menuCreateTask.Name = "menuCreateTask";
            this.menuCreateTask.Size = new System.Drawing.Size(180, 22);
            this.menuCreateTask.Text = "新建任务(&N)";
            this.menuCreateTask.Click += new System.EventHandler(this.menuCreateTask_Click);
            // 
            // dgTasks
            // 
            this.dgTasks.AllowUserToAddRows = false;
            this.dgTasks.AllowUserToDeleteRows = false;
            this.dgTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTasks.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTasks.Location = new System.Drawing.Point(0, 32);
            this.dgTasks.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dgTasks.Name = "dgTasks";
            this.dgTasks.ReadOnly = true;
            this.dgTasks.RowTemplate.Height = 23;
            this.dgTasks.Size = new System.Drawing.Size(697, 303);
            this.dgTasks.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 340);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(697, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // coor3BindingSource
            // 
            this.coor3BindingSource.DataSource = typeof(GeoFun.GNSS.Coor3);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 362);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgTasks);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmMain";
            this.Text = "GNSSFun";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coor3BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuIonosphere;
        private System.Windows.Forms.ToolStripMenuItem menuCreateTask;
        private System.Windows.Forms.DataGridView dgTasks;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.BindingSource coor3BindingSource;
    }
}