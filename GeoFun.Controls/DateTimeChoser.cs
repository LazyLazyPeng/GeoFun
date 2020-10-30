using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoFun.Controls
{
    public partial class DateTimeChoser : UserControl
    {

        public static bool showConfirmButton = true;   // 日期时间选择时，是否显示确定按钮  

        /// <summary>  
        /// 为textBoox添加一个日期时间选择控件，辅助日期时间的输入  
        /// </summary>  
        public static void AddTo(TextBox textBox)
        {
            try
            {
                DateTime time = DateTime.Parse(textBox.Text);
            }
            catch (Exception ex)
            {
                textBox.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            textBox.MouseClick += textBoox_MouseClick;
        }

        /// <summary>  
        /// 为textBoox添加一个日期时间选择控件，辅助日期时间的输入,并设置初始时显示的时间  
        /// </summary>  
        public static void AddTo(TextBox textBoox, DateTime dateTime)
        {
            textBoox.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            textBoox.MouseClick += textBoox_MouseClick;
        }

        private static void textBoox_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // 创建一个关联到textBox的日期时间选择控件  
            DateTimeChoser choser = new DateTimeChoser();
            choser.showOn(textBox);

            // 设置显示的时间为文本框中的日期时间  
            try
            {
                DateTime time = DateTime.Parse(textBox.Text);
                choser.setDateTime(time);
            }
            catch (Exception ex) { }
        }

        public DateTimeChoser()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            // 时分秒设置  
            for (int i = 0; i < 24; i++) comboBox_hour.Items.Add((i < 10 ? "0" : "") + i);
            for (int i = 0; i < 60; i = i + 1) comboBox_minite.Items.Add((i < 10 ? "0" : "") + i);
            for (int i = 0; i < 60; i++) comboBox_second.Items.Add((i < 10 ? "0" : "") + i);

            comboBox_hour.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_minite.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_second.DropDownStyle = ComboBoxStyle.DropDownList;

            //设置显示的日期时间  
            setDateTime(DateTime.Now);
        }

        public delegate void DateTimeChanged_Handle(object sender, EventArgs e);
        [Description("当选择日期或时间变动时，调用此事件"), Category("自定义事件")]
        public event DateTimeChanged_Handle DateTimeChanged;

        // 选择日期变动  
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime S = monthCalendar1.SelectionStart;
            string date = S.ToString("yyyy-MM-dd");
            if (!date.Equals(label_date.Text))
            {
                label_date.Text = date;
                if (DateTimeChanged != null) DateTimeChanged(this, new EventArgs());
            }
        }

        //选择的时间变动  
        private void TimeChanged(object sender, EventArgs e)
        {
            string time = comboBox_hour.Text + ":" + comboBox_minite.Text + ":" + comboBox_second.Text;
            if (!time.Equals(label_time.Text))
            {
                label_time.Text = time;
                if (DateTimeChanged != null) DateTimeChanged(this, new EventArgs());
            }
        }

        // 设置显示到指定的日期时间  
        public void setDateTime(DateTime now)
        {
            // 初始时界面显示当前的日期时间  
            label_date.Text = now.ToString("yyyy-MM-dd");
            monthCalendar1.SetDate(now);

            // 设置时间  
            label_time.Text = now.ToString("HH:mm:ss");
            comboBox_hour.SelectedIndex = now.Hour;
            comboBox_minite.SelectedIndex = now.Minute;
            comboBox_second.SelectedIndex = now.Second;
        }

        // 获取当前选择的日期时间  
        public string getDateTime()
        {
            return label_date.Text + " " + label_time.Text;
        }


        #region 自定义控件输入绑定逻辑，将当前日期时间控件绑定到指定的TextBox

        private Form form;
        TextBox textbox;
        private Delegate[] textboxEvents;

        // 在指定的TextBox中，显示当前日期时间选择控件，进行日期时间的输入  
        public void showOn(TextBox textBox, int offX = 0, int offY = 0)
        {
            Point P = getLocation(textBox);
            P = new Point(P.X, P.Y + textBox.Height);

            show(textBox, P.X + offX, P.Y + offY, showConfirmButton);
        }

        // 在TextBox点击时，调用DateTimeChoser进行日期时间的选择,当再次点击时，关闭之前的日期选择状态  
        private void show(TextBox textbox, int L, int T, bool showButton)
        {
            this.textbox = textbox;
            textboxEvents = getEvents(textbox, "MouseClick");  // 获取TextBox原有事件处理逻辑  
            ClearEvent(textbox, "MouseClick");          // 移除TextBox原有MouseClick事件处理逻辑  

            // 新建一个窗体  
            form = new Form();
            form.Width = this.Width;
            form.Height = this.Height;
            if (showButton) form.Height = this.Height + 40;
            form.FormBorderStyle = FormBorderStyle.None;    // 无边框  
            form.ShowInTaskbar = false;                     // 不在任务栏中显示  
            form.BackColor = Color.White;                   //   

            form.Location = new Point(L, T);

            // 将当前日期时间选择控件添加到form中  
            this.Left = 0; this.Top = 0;
            form.Controls.Add(this);

            if (showButton)
            {
                Button button = new Button();
                button.Text = "确定";
                button.ForeColor = Color.Blue;
                button.Left = (this.Width - button.Width) / 2;
                button.Top = this.Height + (40 - button.Height) / 2;
                form.Controls.Add(button);

                button.Click += button_Click;
            }

            form.Show();             // 显示日期时间选择  
            form.Location = new Point(L, T);
            form.TopMost = true;
            form.Activate();         // 当前界面获取到焦点  

            Form Parent = getParentForm(textbox);       // 获取TextBox的父窗体  
            if (Parent != null) Parent.FormClosed += Parent_FormClosed;

            textbox.MouseClick += textbox_MouseClick;
        }

        // 添加  
        private void button_Click(object sender, EventArgs e)
        {
            textbox_MouseClick(textbox, null);
        }

        // 关闭当前form  
        private void Parent_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (form != null)
            {
                form.Close();
                form = null;
            }
        }

        private void textbox_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = getDateTime();

            if (form != null)
            {
                form.Close();
                form = null;
            }

            textBox.MouseClick -= textbox_MouseClick;           // 移除当前事件处理逻辑  
            addEvents(textBox, "MouseClick", textboxEvents);    // 还原TextBox原有事件处理逻辑  
        }

        #endregion


        // 获取给定控件的父窗体  
        public static Form getParentForm(Control control)
        {
            if (control is Form) return control as Form;

            Control C = control;
            while (C.Parent != null)
            {
                if (C.Parent is Form) return C.Parent as Form;
                else C = C.Parent;
            }

            return null;
        }

        #region 获取控件的坐标信息

        /// <summary>  
        /// 获取任意控件相对于屏幕的坐标  
        /// </summary>  
        public static Point getLocation(Control control)
        {
            Point P;
            if (control is Form) P = getFormClientLocation(control as Form);
            else P = control.Location;
            if (control.Parent != null)
            {
                Control parent = control.Parent;
                Point P2 = getLocation(parent);

                P = new Point(P.X + P2.X, P.Y + P2.Y);
            }

            return P;
        }

        /// <summary>  
        /// 获取Form窗体有效显示区域的起点,相对于屏幕的坐标  
        /// </summary>  
        public static Point getFormClientLocation(Form form)
        {
            Rectangle rect = form.ClientRectangle;

            int offx = 0, offy = 0;
            if (form.FormBorderStyle != FormBorderStyle.None)
            {
                offx = (form.Width - rect.Width) / 2;
                offy = (form.Height - rect.Height) - 8;
            }

            Point P = new Point(form.Location.X + offx, form.Location.Y + offy);

            return P;
        }

        #endregion


        #region 动态修改控件对应的事件处理逻辑

        // ClearEvent(button1,"Click");//就会清除button1对象的Click事件的所有挂接事件。  
        private void ClearEvent(Control control, string eventname)
        {
            if (control == null) return;
            if (string.IsNullOrEmpty(eventname)) return;
            BindingFlags mPropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
            BindingFlags mFieldFlags = BindingFlags.Static | BindingFlags.NonPublic;
            Type controlType = typeof(System.Windows.Forms.Control);
            PropertyInfo propertyInfo = controlType.GetProperty("Events", mPropertyFlags);
            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(control, null);
            FieldInfo fieldInfo = (typeof(Control)).GetField("Event" + eventname, mFieldFlags);
            Delegate d = eventHandlerList[fieldInfo.GetValue(control)];
            if (d == null) return;
            EventInfo eventInfo = controlType.GetEvent(eventname);
            foreach (Delegate dx in d.GetInvocationList())
                eventInfo.RemoveEventHandler(control, dx);
        }

        // getEvent(button1,"Click"); //就会获取到button1对象的Click事件的所有挂接事件。  
        private Delegate[] getEvents(Control control, string eventname)
        {
            if (control == null) return null;
            if (string.IsNullOrEmpty(eventname)) return null;
            BindingFlags mPropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;
            BindingFlags mFieldFlags = BindingFlags.Static | BindingFlags.NonPublic;
            Type controlType = typeof(System.Windows.Forms.Control);
            PropertyInfo propertyInfo = controlType.GetProperty("Events", mPropertyFlags);
            EventHandlerList eventHandlerList = (EventHandlerList)propertyInfo.GetValue(control, null);
            FieldInfo fieldInfo = (typeof(Control)).GetField("Event" + eventname, mFieldFlags);
            Delegate d = eventHandlerList[fieldInfo.GetValue(control)];
            if (d == null) return null;

            Delegate[] events = new Delegate[d.GetInvocationList().Length];
            int i = 0;
            foreach (Delegate dx in d.GetInvocationList()) events[i++] = dx;

            return events;
        }

        // addEvents(button1,"Click"); // 为button1对象的Click事件挂接事件  
        private void addEvents(Control control, string eventname, Delegate[] evenets)
        {
            if (control == null) return;
            if (string.IsNullOrEmpty(eventname)) return;

            Type controlType = typeof(System.Windows.Forms.Control);
            EventInfo eventInfo = controlType.GetEvent(eventname);

            foreach (Delegate e in evenets)
                eventInfo.AddEventHandler(control, e);
        }

        #endregion
    }
}
