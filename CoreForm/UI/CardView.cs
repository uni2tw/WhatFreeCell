using System;
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
        public void SetEmpty()
        {
            this.Data = null;
            if (View is PictureBox)
            {
                (View as PictureBox).Image = null;
            }
            else if (View is Button)
            {
                (View as Button).BackgroundImage = null;
            }
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
                    if (View is PictureBox)
                    {
                        (View as PictureBox).Image = Data.ActivedImage;
                    }
                    else if (View is Button)
                    {
                        (View as Button).BackgroundImage = Data.ActivedImage;
                    }
                }
                else
                {
                    if (View is PictureBox)
                    {
                        (View as PictureBox).Image = Data.Image;
                    } 
                    else if (View is Button)
                    {
                        (View as Button).BackgroundImage = Data.Image;
                    }
                }
            }
        }
        private Control _view;
        public Control View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
                _view.Click += delegate (object sender, System.EventArgs e)
                {
                    this.Slot.CardClicked();
                };
                _view.DoubleClick += delegate (object sender, System.EventArgs e)
                {
                    this.Slot.CardDoubleClicked();
                };
            }
        }
        public Card Data { get; set; }
        public WaitingSlot Slot { get; set; }

        public override string ToString()
        {
            if (Data == null)
            {
                return "[未設定]";
            }
            return Data.ToString();
        }


    }
    
}
