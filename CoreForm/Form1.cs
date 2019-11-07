using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreForm
{
    public partial class Form1 : Form
    {
        Deck deck;
        FreeCellGame game;

        public Form1()
        {
            InitializeComponent();
            this.Text = "新接龍";

            deck = Deck.Create().Shuffle(1);
            game = new FreeCellGame(this);
            game.Init();
            game.Reset(deck);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                //game.DeselectTempCard();
                game.DeselectWaitingCard();
                CardView cardView = game.SelectWaitingCard(0);
                if (cardView != null)
                {
                    cardView.Actived = true;
                }

                CardView targetCardView = game.SelectTempCard(0);

                if (targetCardView.Data == null)
                {
                    game.MoveCardToTemp(cardView, targetCardView);
                } 
                else
                {
                    MessageBox.Show("此步犯規");
                }
            } 
            else if (e.KeyCode == Keys.F4)
            {
                game.DeselectWaitingCard();
                CardView cardView = game.SelectWaitingCard(1);
                cardView.Actived = true;
            }
        }
    }
}
