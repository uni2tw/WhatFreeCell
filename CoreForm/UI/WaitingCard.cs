namespace CoreForm.UI
{
    public class WaitingCard
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public Card Card { get; set; }

        public override string ToString()
        {
            return string.Format("({0},{1}) {2}", 
                Left, Top, Card == null ? "null" : Card.ToString());
        }
    }
    
}
