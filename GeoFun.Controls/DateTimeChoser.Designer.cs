
namespace GeoFun.Controls
{
    partial class DateTimeChoser
    {

        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.panel_consume = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_second = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_minite = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_hour = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_date = new System.Windows.Forms.Label();
            this.label_time = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_consume.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.AllowDrop = true;
            this.monthCalendar1.Location = new System.Drawing.Point(-3, 15);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // panel_consume
            // 
            this.panel_consume.BackColor = System.Drawing.Color.White;
            this.panel_consume.Controls.Add(this.label6);
            this.panel_consume.Controls.Add(this.comboBox_second);
            this.panel_consume.Controls.Add(this.label5);
            this.panel_consume.Controls.Add(this.comboBox_minite);
            this.panel_consume.Controls.Add(this.label4);
            this.panel_consume.Controls.Add(this.comboBox_hour);
            this.panel_consume.Location = new System.Drawing.Point(-2, 173);
            this.panel_consume.Name = "panel_consume";
            this.panel_consume.Size = new System.Drawing.Size(222, 30);
            this.panel_consume.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "秒";
            // 
            // comboBox_second
            // 
            this.comboBox_second.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_second.FormattingEnabled = true;
            this.comboBox_second.Location = new System.Drawing.Point(149, 6);
            this.comboBox_second.Name = "comboBox_second";
            this.comboBox_second.Size = new System.Drawing.Size(40, 20);
            this.comboBox_second.TabIndex = 14;
            this.comboBox_second.Text = "0";
            this.comboBox_second.SelectedIndexChanged += new System.EventHandler(this.TimeChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(126, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "分";
            // 
            // comboBox_minite
            // 
            this.comboBox_minite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_minite.FormattingEnabled = true;
            this.comboBox_minite.Location = new System.Drawing.Point(80, 6);
            this.comboBox_minite.Name = "comboBox_minite";
            this.comboBox_minite.Size = new System.Drawing.Size(40, 20);
            this.comboBox_minite.TabIndex = 12;
            this.comboBox_minite.Text = "0";
            this.comboBox_minite.SelectedIndexChanged += new System.EventHandler(this.TimeChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "时";
            // 
            // comboBox_hour
            // 
            this.comboBox_hour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_hour.FormattingEnabled = true;
            this.comboBox_hour.Location = new System.Drawing.Point(9, 6);
            this.comboBox_hour.Name = "comboBox_hour";
            this.comboBox_hour.Size = new System.Drawing.Size(42, 20);
            this.comboBox_hour.TabIndex = 10;
            this.comboBox_hour.Text = "0";
            this.comboBox_hour.SelectedIndexChanged += new System.EventHandler(this.TimeChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label_date);
            this.panel1.Controls.Add(this.label_time);
            this.panel1.Location = new System.Drawing.Point(-3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 23);
            this.panel1.TabIndex = 25;
            // 
            // label_date
            // 
            this.label_date.AutoSize = true;
            this.label_date.BackColor = System.Drawing.Color.White;
            this.label_date.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_date.Location = new System.Drawing.Point(18, 3);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(98, 16);
            this.label_date.TabIndex = 26;
            this.label_date.Text = "2016-06-12";
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.BackColor = System.Drawing.Color.White;
            this.label_time.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_time.Location = new System.Drawing.Point(118, 3);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(80, 16);
            this.label_time.TabIndex = 25;
            this.label_time.Text = "12:23:35";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel_consume);
            this.panel2.Controls.Add(this.monthCalendar1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 202);
            this.panel2.TabIndex = 26;
            // 
            // DateTimeChoser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "DateTimeChoser";
            this.Size = new System.Drawing.Size(215, 202);
            this.panel_consume.ResumeLayout(false);
            this.panel_consume.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel panel_consume;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_second;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_minite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_hour;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Panel panel2;

    }
}
