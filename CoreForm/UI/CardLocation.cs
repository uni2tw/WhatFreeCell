using FreeCell.Entities.GameEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreForm.UI
{
    public class CardLocation
    {
        public GameZoneType ZoneType { get; set; }
        public int SlotIndex { get; set; }
        public CardView DataView { get; set; }

        public override string ToString()
        {
            return string.Format("{0}@{1} {2}", DataView, ZoneType, SlotIndex);
        }
    }
}
