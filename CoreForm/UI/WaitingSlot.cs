using CoreForm.UI;
using System.Collections.Generic;

namespace CoreForm.UI
{

    public class WaitingSlot
    {
        public string Name { get; set; }        
        public CardView[] CardViews { get; set; }

        public WaitingSlot()
        {
            CardViews = new CardView[13];
        }
    }
    
}
