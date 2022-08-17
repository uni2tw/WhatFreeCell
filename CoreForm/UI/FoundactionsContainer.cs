using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{
    public class FoundationsContainer : TableLayoutPanel
    {
        private List<FoundationColumnPanel> _columnPanels;
        private IGameForm _form;
        
        public FoundationsContainer(IGameForm form, int columnNumber)
        {            
            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;            
            this._form = form;

            _columnPanels = new List<FoundationColumnPanel>(columnNumber);
            this.ColumnCount = columnNumber;
            float columnWidth = 100F / columnNumber;
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, columnWidth));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, columnWidth));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, columnWidth));
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, columnWidth));
            this.Location = new System.Drawing.Point(40, 40);
            this.Name = nameof(FoundationsContainer);
            this.RowCount = 1;
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Size = new System.Drawing.Size(640, 240);

            for (int i = 0; i < columnNumber; i++)
            {
                var panel = new FoundationColumnPanel();
                _columnPanels.Add(panel);
                this.Controls.Add(panel);
            }
        }

        public void Resize(int left, int top, int cardWidth, int cardHeight)
        {
            int right = left + cardWidth * 4;
            int bottom = top + cardHeight;

            this.Left = left;
            this.Top = top;
            this.Width = right - this.Left;
            this.Height = bottom - this.Top;
            this._form.SetControlReady(this);

            //_columnPanels.ForEach(x => this.Controls.Add(x));
        }
    }

    public class FoundationColumnPanel : GeneralColumnPanel
    {
        public FoundationColumnPanel()
        {
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.None;
            this.Paint += delegate (object sender, PaintEventArgs e)
            {
                var self = sender as Panel;
                ControlPaint.DrawBorder(e.Graphics, self.ClientRectangle, Color.Green, ButtonBorderStyle.Inset);        
            };
        }


        public FoundationColumnPanel(int left, int top, int cardWidth, int cardHeight)
        {
            Location = new Point(left, top);
            Width = cardWidth;
            Height = cardHeight;
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.FixedSingle;
            BackgroundImageLayout = ImageLayout.Stretch;
        }
    }

}
