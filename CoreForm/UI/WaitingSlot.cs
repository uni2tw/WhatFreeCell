using CoreForm.UI;
using System.Collections.Generic;

namespace CoreForm.UI
{

    public class WaitingSlot
    {
        public string Name { get; set; }        
        public CardView[] Cards { get; set; }

        public WaitingSlot()
        {
            Cards = new CardView[13];
        }
    }
    
}
