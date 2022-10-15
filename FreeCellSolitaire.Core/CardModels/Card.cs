namespace FreeCellSolitaire.Core.CardModels
{
    /// <summary>
    /// 一張牌
    /// </summary>
    public class Card
    {
        public Card(CardSuit suit, int number)
        {
            this.Suit = suit;
            this.Number = number;
        }
        public Card()
        {

        }
        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                System.Diagnostics.Debug.Assert(value >= 1 && value <= 13);             
                _number = value;
            }
        }
        public CardSuit Suit { get; set; }

        public override string ToString()
        {
            if (Suit == CardSuit.Spade)
            {
                return "黑桃 " + Number;
            }
            if (Suit == CardSuit.Heart)
            {
                return "紅心 " + Number;
            }
            if (Suit == CardSuit.Diamond)
            {
                return "方塊 " + Number;
            }
            if (Suit == CardSuit.Club)
            {
                return "梅花 " + Number;
            }
            return string.Empty;
        }

        public string ToNotation()
        {
            if (Suit == CardSuit.Spade)
            {
                return "s" + Number;
            }
            if (Suit == CardSuit.Heart)
            {
                return "h" + Number;
            }
            if (Suit == CardSuit.Diamond)
            {
                return "d" + Number;
            }
            if (Suit == CardSuit.Club)
            {
                return "c" + Number;
            }
            return string.Empty;
        }

        public override int GetHashCode()
        {
            return (int)this.Suit * 100 + this.Number;            
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Card castObj = obj as Card;
            if (castObj == null) return false;
            return castObj.GetHashCode() == GetHashCode();
            //return castObj.Number == this.Number && castObj.Suit == this.Suit;
        }

        public bool IsRed()
        {
            if (this.Suit == CardSuit.Heart || this.Suit == CardSuit.Diamond)
            {
                return true;
            }
            return false;
        }
        public bool IsBlack()
        {
            if (this.Suit == CardSuit.Spade || this.Suit == CardSuit.Club)
            {
                return true;
            }
            return false;
        }
    }
}
