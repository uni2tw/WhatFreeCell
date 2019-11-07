using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public class FreeCellGame
    {
        Form form;
        CompletionZone completionZone = new CompletionZone();
        TempZone tempZone = new TempZone(); 
        WaitingZone waitingZone = new WaitingZone();
        public int cardWidth { get; set; }
        public int cardHeight { get; set; }
        public FreeCellGame(Form form)
        {
            this.form = form;
        }
        public void Init()
        {
            int boardWidth = 800;
            int boardHeight = 600;


            form.Width = boardWidth;
            form.Height = boardHeight;
            form.BackColor = Color.FromArgb(0, 123, 0);

            cardWidth = (int)(Math.Floor((decimal)boardWidth / 9));
            cardHeight = (int)(cardWidth * 1.38);

            InitTempZone(cardWidth, cardHeight, 0, 0);
            InitCompletionZone(cardWidth, cardHeight, boardWidth - (4 * cardWidth) - 9, 0);
            InitWaitingZone(boardWidth, boardHeight, cardWidth, cardHeight);

        }

        public void DeselectWaitingCard()
        {
            CardView cardView = GetActivedWaitingCard();
            if (cardView != null)
            {
                cardView.Actived = false;
            }
        }

        public CardView SelectTempCard(int slotNo)
        {
            return tempZone.SelectCardView(slotNo);
        }

        private CardView GetActivedWaitingCard()
        {
            CardView cardView = waitingZone.GetActivedCard();
            return cardView;
        }

        public CardView SelectWaitingCard(int slotNo)
        {
            return waitingZone.SelectCard(slotNo);
        }

        public void Reset(Deck deck)
        {
            //reset temp data
            tempZone.Reset();
            //reset temp image

            completionZone.Reset();
            //reset completion data

            //reset completion image 
            
            waitingZone.Clear();



            while (waitingZone.AppendCard(deck.Draw()))
            {
            };

            waitingZone.Refresh();
        }



        private void InitWaitingZone(int boardWidth, int boardHeight, int cardWidth, int cardHeight)
        {
            int paddingWidth = (boardWidth - cardWidth * 8) / 9;
            int topBase = cardHeight + 12;
            int paddingTop = cardHeight / 6;
            int left = paddingWidth;

            for (int i = 0; i < waitingZone.GetSlotCount(); i++)
            {
                int top = topBase;
                for (int j = 0; j < waitingZone.GetSlotCardLimit(); j++)
                {
                    Control viewControl = new PictureBox
                    {
                        Location = new Point(left, top),
                        Width = cardWidth,
                        Height = cardHeight,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = null
                    };
                    form.Controls.Add(viewControl);
                    waitingZone.InitView(i, j, viewControl);
                    top = top + paddingTop;
                }
                left = left + cardWidth + paddingWidth;
            }
        }

        private void InitTempZone(int cardWidth, int cardHeight, int right, int top)
        {
            for (int i = 0; i < 4; i++)
            {                
                Control viewControl = new Button
                {
                    Location = new Point(right, top),
                    Width = cardWidth,
                    Height = cardHeight,
                    FlatStyle = FlatStyle.Flat,
                    BackgroundImageLayout = ImageLayout.Stretch
                };
                form.Controls.Add(viewControl);
                tempZone.InitView(
                    new CardView
                {
                    View = viewControl,
                    Data = null
                });
                right = right + cardWidth;
            }
        }

        private void InitCompletionZone(int cardWidth, int cardHeight, int right, int top)
        {
            for (int i = 0; i < 4; i++)
            {
                Control viewControl = new Button
                {
                    Location = new Point(right, top),
                    Width = cardWidth,
                    Height = cardHeight,
                    FlatStyle = FlatStyle.Flat
                };
                form.Controls.Add(viewControl);
                completionZone.InitView(new CardView
                {
                    View = viewControl,
                    Data = null
                });
                right = right + cardWidth;
            }
        }

        public void MoveCardToTemp(CardView source, CardView target)
        {
            target.SetCard(source.Data, false);
            source.SetEmpty();
        }

        public int GetCardHeight()
        {
            return cardHeight;
        }

        public int GetCardWidth()
        {
            return cardWidth;
        }


    }
    
}
