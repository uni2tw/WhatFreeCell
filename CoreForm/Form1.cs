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

            this.Load += Form1_Load;
            deck = Deck.Create().Shuffle(1);
            game = new FreeCellGame(this);
            game.Init();
            game.Reset(deck);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Image img = Image.FromFile(@"c:\temp2\600x600-01.png");
            pictureBox1.Image = img;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(@"c:\temp2\600x600-02.png");
            pictureBox1.Image = img;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i=0;i<400; i=i+1)
            {
                System.Threading.Thread.Sleep(10);
                //this.SuspendLayout();
                pictureBox1.Left = i;
                //this.ResumeLayout();
            }
        }

        private void btnLoadCardSample_Click(object sender, EventArgs e)
        {

            var card = deck.Draw();
            pictureBox1.Image = card.Image;
            
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            //this.textBox1.Text = string.Format("{0}x{1}",
            //    Screen.PrimaryScreen.Bounds.Width,
            //    Screen.PrimaryScreen.Bounds.Height
            //    );
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var card = deck.Draw();
            pictureBox1.Width = game.GetCardWidth();
            pictureBox1.Height = game.GetCardHeight();
            game.MoveCardToTemp();
            pictureBox1.Image = card.Image;
            pictureBox1.Location = new Point(0, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var card = deck.Draw();
            
        }
    }
}
