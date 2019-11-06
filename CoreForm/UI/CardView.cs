using System.Windows.Forms;

namespace CoreForm.UI
{
    public class CardView
    {
        public void SetCard(Card data, bool actived)
        {
            this.Data = data;
            this.Actived = actived;
            this.View.Click += delegate (object sender, System.EventArgs e)
            {
                this.Slot.CardClicked();
            };
            this.View.DoubleClick += delegate (object sender, System.EventArgs e)
            {
                this.Slot.CardDoubleClicked();
            };
        }

        private bool _actived;
        public bool Actived
        {
            get
            {
                return _actived;
            }
            set
            {
                _actived = value;
                if (value)
                {
                    (View as PictureBox).Image = Data.ActivedImage;
                }
                else
                {
                    (View as PictureBox).Image = Data.Image;
                }
            }
        }
        public Control View { get; set; }
        public Card Data { get; set; }
        public WaitingSlot Slot { get; set; }
    }
    
}
