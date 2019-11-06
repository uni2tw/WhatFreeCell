using System.Collections.Generic;

namespace CoreForm.UI
{
    public class TempZone
    {
        List<TempSlot> slots = new List<TempSlot>();
        public TempZone()
        {
            slots.Add(new TempSlot());
            slots.Add(new TempSlot());
            slots.Add(new TempSlot());
            slots.Add(new TempSlot());
        }

    }

    public class TempSlot
    {


    }
}
