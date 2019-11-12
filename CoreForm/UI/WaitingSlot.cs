using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreForm.UI
{
    public delegate void SelectCardHandler(ZoneType zoneType, CardView cardView);
   

    public interface ISlot
    {
        void CardClicked();

        void CardDoubleClicked();

        ZoneType Type { get; set; }
    }

    public class WaitingSlot : ISlot
    {
        public string Name { get; set; }
        public CardView[] CardViews { get; set; }

        public event SelectCardHandler OnSelectCard;

        public void CardClicked()
        {
            SetLastCardActived();
        }

        public void CardDoubleClicked()
        {
            if (MoveLastCardToCompleiotn() == false)
            {
                if (MoveLastCardToTemp() == false)
                {

                }
            }
        }

        public ZoneType Type { get; set; }
        
        /// <summary>
        /// 移動最後一張牌到暫存區
        /// </summary>
        /// <returns></returns>
        private bool MoveLastCardToTemp()
        {
            //throw new NotImplementedException();
            return false;
        }
        /// <summary>
        /// 移動最後一張牌到完成區
        /// </summary>
        /// <returns></returns>
        private bool MoveLastCardToCompleiotn()
        {
            //throw new NotImplementedException();
            return false;
        }

        public bool SetLastCardActived()
        {
            var card = SelectLastCard();
            if (card == null)
            {
                return false;
            }
            card.Actived = !card.Actived;
            return true;
        }

        public CardView SelectLastCard()
        {
            var card = this.CardViews.LastOrDefault(t => t.Data != null);
            return card;
        }

        public WaitingSlot()
        {
            CardViews = new CardView[13];
        }
    }
    
}
