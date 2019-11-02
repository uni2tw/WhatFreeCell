using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CoreForm.UI
{
    public class FreeCellGame
    {
        Form form;
        List<Button> tempZones = new List<Button>();
        List<Button> completionZones = new List<Button>();
        
        List<List<Point>> waitingAreas = new List<List<Point>>();
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
            InitWaitingAreas(boardWidth, boardHeight, cardWidth, cardHeight);

        }

        private void InitWaitingAreas(int boardWidth, int boardHeight, int cardWidth, int cardHeight)
        {
            int paddingWidth = (boardWidth - cardWidth * 8) / 9;
            int topBase = cardHeight + 12;
            int left = paddingWidth;

            waitingAreas.Add(new List<Point>());
            waitingAreas.Add(new List<Point>());
            waitingAreas.Add(new List<Point>());
            waitingAreas.Add(new List<Point>());

            for (int i = 0; i < waitingAreas.Count; i++)
            {
                int top = topBase;
                for (int j = 0; j < 12; j++)
                {
                    waitingAreas[i].Add(new Point { X = left, Y = top });
                    top = top + 12;
                }
                left = left + cardWidth + paddingWidth;                
            }
        }

        private void InitTempZones(int cardWidth, int cardHeight, int right, int top)
        {
            tempZones.Add(new Button
            {
                Location = new Point(right, top),
                Width = cardWidth,
                Height = cardHeight,
                FlatStyle = FlatStyle.Flat
            });
            right = right + cardWidth;

            tempZones.Add(new Button
            {
                Location = new Point(right, top),
                Width = cardWidth,
                Height = cardHeight,
                FlatStyle = FlatStyle.Flat
            });
            right = right + cardWidth;

            tempZones.Add(new Button
            {
                Location = new Point(right, top),
                Width = cardWidth,
                Height = cardHeight,
                FlatStyle = FlatStyle.Flat
            });
            right = right + cardWidth;

            tempZones.Add(new Button
            {
                Location = new Point(right, top),
                Width = cardWidth,
                Height = cardHeight,
                FlatStyle = FlatStyle.Flat
            });


            foreach (var tempZone in tempZones)
            {
                form.Controls.Add(tempZone);
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

        private void InitCompletionZones(int cardWidth, int cardHeight, int right, int top)
        {
            completionZones.Add(new Button
            {
                Location = new Point(right, top),
                Width = cardWidth,
                Height = cardHeight,
                FlatStyle = FlatStyle.Flat
            });
            right = right + cardWidth;

            completionZones.Add(new Button
            {
                Location = new Point(right, top),
                Width = cardWidth,
                Height = cardHeight,
                FlatStyle = FlatStyle.Flat
            });
            right = right + cardWidth;

            completionZones.Add(new Button
            {
                Location = new Point(right, top),
                Width = cardWidth,
                Height = cardHeight,
                FlatStyle = FlatStyle.Flat
            });
            right = right + cardWidth;

            completionZones.Add(new Button
            {
                Location = new Point(right, top),
                Width = cardWidth,
                Height = cardHeight,
                FlatStyle = FlatStyle.Flat
            });

            foreach (var zone in completionZones)
            {
                form.Controls.Add(zone);
            }
        }
    }
    
}
