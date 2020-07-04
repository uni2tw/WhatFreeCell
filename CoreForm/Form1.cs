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

        public Form1()
        {
            InitializeComponent();

            int menuHeight = 0;
            //InitializeMenu(out menuHeight);

            this.Text = "新接龍";

            game = new Game(this);
            game.Init(menuHeight);
            game.OnFinish += delegate ()
            {
                if (MessageBox.Show("你還要再一次嗎?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    game.Reset();
                    game.Start();
                } 
                else
                {
                    this.Quit();
                }
            };

            game.OnFail += delegate ()
            {
                if (MessageBox.Show("你還要再一次嗎?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    game.Reset();
                    game.Start();
                }
                else
                {
                    this.Quit();
                }
            };
            game.Reset();
            game.Start();            
        }

        private void InitializeMenu()
        {
            var menu = new System.Windows.Forms.MenuStrip();            

            menu.Dock = DockStyle.Top;
            menu.BackColor = Color.Silver;            


            var menuItem0 = new ToolStripMenuItem();
            menuItem0.Text = "遊戲(&G)";
            menu.Items.Add(menuItem0);
            menuItem0.DropDownItems.Add("新遊戲", null);
            menuItem0.DropDownItems.Add("選擇牌局", null);
            menuItem0.DropDownItems.Add("重啟牌局", null);
            menuItem0.DropDownItems.Add(new ToolStripSeparator());
            menuItem0.DropDownItems.Add("結束(&X)", null).Click += delegate (object sender, EventArgs e)
            {
                this.Close();
            };

            var menuItem1 = new ToolStripMenuItem();
            menuItem1.Text = "說明(&H))";
            menu.Items.Add(menuItem1);
            menuItem1.DropDownItems.Add("關於新接龍", null);

            this.Controls.Add(menu);
        }

        private void Quit()
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
    }
}
