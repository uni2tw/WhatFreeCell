using System.Collections.Generic;

namespace CoreForm.UI
{
    public delegate void ZoneHolderHandler(GameZoneType zoneType, Slot slot);
    public interface IZone
    {
        bool CanSwap { get; }
        bool CanMoveIn { get; }
        bool CanMoveOut { get; }
        int Capacity { get; }

        List<Slot> Slots { get; set; }

        bool IsAvailableFor(int x, CardView card);

        bool SetCard(int x, CardView card);

        bool TrySelect(int slotIndex, out string message);
        void DeselectSlots();

        int GetSlotSelectedIndex();
        CardLocation GetSelectedInfo();
    }
}
