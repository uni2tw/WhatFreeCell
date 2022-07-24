namespace FreeCellSolitaire.Core.CardModels
{
    /// <summary>
    /// 一張牌
    /// </summary>
    public class Card
    {
        public int Number { get; set; }
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
    }
}
