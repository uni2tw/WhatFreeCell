using CoreForm.UI;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class FoundationsContainer
    {
        private List<FoundationColumnPanel> _columnContainers;
        private IGameForm form;

        public FoundationsContainer()
        {
            _columnContainers = new List<FoundationColumnPanel>(4);
        }

        public FoundationsContainer(IGameForm form)
        {
            this.form = form;
        }
    }

    public class FoundationColumnPanel : GeneralColumnPanel
    {
        public FoundationColumnPanel(int left, int top, int cardWidth, int cardHeight)
        {
            Location = new Point(left, top);
            Width = cardWidth;
            Height = cardHeight;
            BorderStyle = BorderStyle.FixedSingle;
            BackgroundImageLayout = ImageLayout.Stretch;
        }
    }

}
