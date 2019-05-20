using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevComponents.DotNetBar;

namespace GeoFun.Controls
{
    public partial class MessageBox : Office2007Form
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        public string Message
        {
            get
            {

            }
        }

        public MessageBoxButtons MessageBoxButtons { get; set; } = MessageBoxButtons.OK;

        public MessageBox()
        {
            InitializeComponent();
        }

        public MessageBox(string title="提示",string message="",MessageBoxButtons buttons=MessageBoxButtons.OK)
        {

        }
    }
}
