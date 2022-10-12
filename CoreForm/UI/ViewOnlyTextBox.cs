using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FreeCellSolitaire.UI
{
    public class ViewOnlyTextBox : System.Windows.Forms.TextBox
    {
        // constants for the message sending
        const int WM_SETFOCUS = 0x0007;
        const int WM_KILLFOCUS = 0x0008;

        public ViewOnlyTextBox()
        {
            this.Multiline = true;
            this.Dock = DockStyle.Bottom;
            this.Height = 100;
            this.Margin = new Padding(3);            
            this.ReadOnly = true;
            this.BorderStyle = BorderStyle.None;            
            this.ScrollBars = ScrollBars.Horizontal;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SETFOCUS) m.Msg = WM_KILLFOCUS;

            base.WndProc(ref m);
        }
    }
}
