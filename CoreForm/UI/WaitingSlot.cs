using System.Collections.Generic;

namespace CoreForm.UI
{
    public class WaitingSlot
    {
        public string Name { get; set; }
        public int Left { get; set; }
        List<WaitingCard> WaiingCards { get; set; }
    }
    
}
