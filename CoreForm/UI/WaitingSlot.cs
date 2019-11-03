using CoreForm.UI;
using System.Collections.Generic;

namespace CoreForm.UI
{

    public class WaitingSlot
    {
        public string Name { get; set; }        
        public WaitingCard[] Cards { get; set; }

        public WaitingSlot()
        {
            Cards = new WaitingCard[13];
        }
    }
    
}
