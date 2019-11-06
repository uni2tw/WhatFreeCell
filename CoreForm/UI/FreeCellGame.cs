﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public class FreeCellGame
    {
        Form form;
        List<CardView> tempZones = new List<CardView>();
        List<CardView> completionZones = new List<CardView>();
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

            InitTempZones(cardWidth, cardHeight, 0, 0);
            InitCompletionZones(cardWidth, cardHeight, boardWidth - (4 * cardWidth) - 9, 0);
            InitWaitingZone(boardWidth, boardHeight, cardWidth, cardHeight);

        }

        public void Reset(Deck deck)
        {
            //reset temp data
            foreach (CardView cardView in tempZones)
            {                
                cardView.Data = null;
            }
            //reset temp image

            //reset completion data
            foreach (CardView cardView in completionZones)
            {
                cardView.Data = null;
            }
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

            int idx = 0;
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
                    waitingZone.Init(i, j, viewControl);
                    top = top + paddingTop;
                }
                left = left + cardWidth + paddingWidth;
            }




        }

        private void InitTempZones(int cardWidth, int cardHeight, int right, int top)
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
                tempZones.Add(new CardView
                {
                    View = viewControl,
                    Data = null
                });
                right = right + cardWidth;
            }
        }


        private void InitCompletionZones(int cardWidth, int cardHeight, int right, int top)
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
                completionZones.Add(new CardView
                {
                    View = viewControl,
                    Data = null
                });
                right = right + cardWidth;
            }
        }


        public void MoveCardToTemp()
        {

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
