using FreeCell.Entities.GameEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreForm.UI
{
    public class SlotUtil
    {
        public static bool Move(Slot srcSlot, Slot destSlot)
        {
            if (srcSlot.ZoneType == GameZoneType.Temp && destSlot.ZoneType == GameZoneType.Temp)
            {

            }
            else if (srcSlot.ZoneType == GameZoneType.Temp && destSlot.ZoneType == GameZoneType.Completion)
            {
                if (destSlot.IsFull)
                {
                    return false;
                }
                if (SlotUtil.CheckLinkable(destSlot.LastCard() , srcSlot.LastCard()) == false)
                {
                    return false;
                }
                var card = srcSlot.LastCard();
                srcSlot.RemoveCard(card);
                card.Actived = false;
                return destSlot.Zone.SetCard(destSlot.Index, card);
            }
            else if (srcSlot.ZoneType == GameZoneType.Temp && destSlot.ZoneType == GameZoneType.Waiting)
            {

            }
            else if (srcSlot.ZoneType == GameZoneType.Waiting && destSlot.ZoneType == GameZoneType.Waiting)
            {

            }
            else if (srcSlot.ZoneType == GameZoneType.Waiting && destSlot.ZoneType == GameZoneType.Temp)
            {

            }
            else if (srcSlot.ZoneType == GameZoneType.Waiting && destSlot.ZoneType == GameZoneType.Completion)
            {
                if (destSlot.IsFull)
                {
                    return false;
                }
                if (destSlot.CheckMoveable(srcSlot) == 0)
                {
                    //return false;
                }
                bool moveable = false;
                var srcCard = srcSlot.LastCard();
                var destCard = destSlot.LastCard();
                if (destCard == null && srcCard.Number == 1)
                {
                    moveable = true;
                }
                else if (destCard.Suit == srcCard.Suit && srcCard.Number - destCard.Number == 1)
                {
                    moveable = true;
                }

                /*
            Slot pSlot = card.Slot;
            if (CheckCanMoveIn(pSlot, card) == false)
            {
                return false;
            }
            pSlot.RemoveCard(card);
            card.Actived = false;


                //SetCard(slotIndex, card);
                if (this.Slots[x].IsFull)
                {
                    return false;
                }

                Slot slot = this.Slots[x];

                this.Slots[x].AddCard(card);
                card.View.Visible = true;
                card.View.Location = this.Slots[x].GetLocation(0);
                card.View.BringToFront();
                card.ZoneType = GameZoneType.Completion;
                return true;            
                 */
                
                //if (completionZone.MoveCard(srcSlot.Index, selectedCard) == false)
                //{
                //    return false;
                //}
            }

            return false;
        }
        /// <summary>
        /// 檢查2張卡能不能連結
        /// </summary>
        /// <param name="prevCard"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool CheckLinkable(CardView prevCard, CardView card)
        {
            if (card == null)
            {
                return false;
            }
            if (prevCard == null)
            {
                if (card.Number == 1)
                {
                    return true;
                }
                return false;
            }            
            if (prevCard.Suit != card.Suit) 
            {
                return false;
            }
            if (card.Number - prevCard.Number != 1)
            {
                return false;
            }            
            return true;
        }
    }
}
