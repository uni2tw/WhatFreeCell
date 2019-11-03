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

    }
}
