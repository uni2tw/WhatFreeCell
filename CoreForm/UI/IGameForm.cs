using System.Drawing;
using System.Windows.Forms;

namespace CoreForm.UI
{
    /// <summary>
    /// 畫面
    /// </summary>
    public interface IGameForm
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Size ClientSize { get;  }
        public Color BackColor { get; set; }

        void Close();
        void SetControl(Control control);
        void SetControlReady(Control control);
        void RestartGame();
        void SetCaption(string text);
    }    

}
