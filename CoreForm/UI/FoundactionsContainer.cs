using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class FoundationsContainer : FlowLayoutPanel
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

        public void Setup(int left, int top, int cardWidth, int cardHeight)
        {
            int right = left + cardWidth * 4;
            int bottom = top + cardHeight;
            //TODO
            throw new NotImplementedException();
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
