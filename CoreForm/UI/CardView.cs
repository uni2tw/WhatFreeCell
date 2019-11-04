using System.Windows.Forms;

namespace CoreForm.UI
{
    public class CardView
    {
        public void SetCard(Card data, bool actived)
        {
            this.Data = data;
            this.Actived = actived;
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
    }
    
}
