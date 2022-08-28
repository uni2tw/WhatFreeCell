using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{

    public class GeneralColumnPanel : Panel
    {
        public List<CardControl> CardControls { get; set; }
        public GeneralColumnPanel()
        {
            CardControls = new List<CardControl>();
        }        
        public void AddCardControl(CardControl cardControl)
        {
            cardControl.SetIndex(CardControls.Count);
            CardControls.Add(cardControl);
            this.Controls.Add(cardControl);
            cardControl.BringToFront();
        }
        public void RemoveCardControlsAfter(int index)
        {            
            while (this.Controls.Count > index)
            {
                var cardControl = CardControls[index];
                CardControls.Remove(cardControl);
                this.Controls.Remove(cardControl);
            }
        }
        public int GetCardControlCount()
        {
            return CardControls.Count;
        }

        public CardControl GetCardControl(int i)
        {
            if (CardControls.Count > i)
            {
                return CardControls[i];
            }
            return null;
        }
    }


}
