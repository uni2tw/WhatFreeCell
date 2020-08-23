using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreForm
{
    public partial class Form1 : Form, IGameForm
    {
        Game game;
        public const string _GAME_TITLE = "新接龍";

        public Form1()
        {
            InitializeComponent();
        
            this.Text = _GAME_TITLE;

            game = new Game(this);
            game.InitScreen();
            game.InitEvents();

            game.Reset();
            game.Start();

            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.game.IsPlaying)
            {
                if (MessageBox.Show("是否放棄這一局", _GAME_TITLE, MessageBoxButtons.YesNo) == DialogResult.No) {
                    e.Cancel = true;
                }
            }           
        }

        /// <summary>
        /// 結束遊戲
        /// </summary>
        public void Quit()
        {
            this.Close();
        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F3)
            //{
            //    //game.DeselectTempCard();
            //    game.DeselectWaitingCard();
            //    CardView cardView = game.SelectWaitingCard(0);
            //    if (cardView != null)
            //    {
            //        cardView.Actived = true;
            //    }

            //    CardView targetCardView = game.SelectTempCard(0);

            //    if (targetCardView.Data == null)
            //    {
            //        game.MoveCardToTemp(cardView, targetCardView);
            //    } 
            //    else
            //    {
            //        MessageBox.Show("此步犯規");
            //    }
            //} 
            //else if (e.KeyCode == Keys.F4)
            //{
            //    game.DeselectWaitingCard();
            //    CardView cardView = game.SelectWaitingCard(1);
            //    if (cardView != null)
            //    {
            //        cardView.Actived = true;
            //    }

            //    CardView targetCardView = game.SelectTempCard(0);

            //    if (targetCardView.Data == null)
            //    {
            //        game.MoveCardToTemp(cardView, targetCardView);
            //    }
            //    else
            //    {
            //        MessageBox.Show("此步犯規");
            //        game.DeselectWaitingCard();
            //    }
            //}
        }

        public void SetControlReady(Control control)
        {
            this.Controls.Add(control);
            control.BringToFront();
        }


        public void SetControl(Control control)
        {
            this.Controls.Add(control);
        }
    }
}
