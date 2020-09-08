using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoFun.GNSS;

namespace GIon
{
    public class MessageHelper : IMessageBox
    {
        public TextBox MyTextBox = null;

        public MessageHelper(TextBox tbx)
        {
            MyTextBox = tbx;
        }

        public void Print(string msg)
        {
            if (MyTextBox is null) return;

            MyTextBox.Invoke(new Action(() =>
            {
                MyTextBox.Text = MyTextBox.Text + msg;
                MyTextBox.Select(MyTextBox.Text.Length,0);
                MyTextBox.ScrollToCaret();
            }));
        }
    }
}
