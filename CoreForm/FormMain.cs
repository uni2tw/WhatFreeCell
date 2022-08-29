using CoreForm.UI;
using FreeCellSolitaire.Core.GameModels;
using System;
using System.Collections;
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
    public partial class FormMain : Form, IGameForm
    {
        IGameUI gui;
        public const string _GAME_TITLE = "新接龍";
        Queue<string> scripts = new Queue<string>();

        public FormMain()
        {
            InitializeComponent();
            
            this.Text = _GAME_TITLE;

            gui = new GameUI(this);
            gui.InitScreen();
            gui.InitEvents();

            gui.Reset();
            gui.Start();

            gui.Redraw();

            scripts.Enqueue("t1h0");
            scripts.Enqueue("t2t0");
            scripts.Enqueue("t7t3");
            scripts.Enqueue("t7t1");
            scripts.Enqueue("t7f0");
            scripts.Enqueue("t7t2");
            scripts.Enqueue("t7f1");
            scripts.Enqueue("t6f2");
            scripts.Enqueue("t2h3");
            scripts.Enqueue("t1h3");
            scripts.Enqueue("t2f0");
            scripts.Enqueue("f2t7");
            scripts.Enqueue("t2t7");
            scripts.Enqueue("t2t3");
            scripts.Enqueue("t2t5");       
            scripts.Enqueue("t5f2");
            scripts.Enqueue("t5t7");
            scripts.Enqueue("f2t7");
            scripts.Enqueue("t0h2");
            scripts.Enqueue("t6f2");
            scripts.Enqueue("t5f3");
            scripts.Enqueue("t5t2");
            scripts.Enqueue("t5t2");
            scripts.Enqueue("t5h2");
            scripts.Enqueue("t4f0");
            scripts.Enqueue("t4t1");
            scripts.Enqueue("f2t5");
            scripts.Enqueue("t4f2");
            scripts.Enqueue("t4t5");
            scripts.Enqueue("t4t2");
            scripts.Enqueue("t6t2");
            scripts.Enqueue("f0t4");
            scripts.Enqueue("t0t4");
            scripts.Enqueue("t0t4");
            scripts.Enqueue("t0t6");
            scripts.Enqueue("f2h1");
            scripts.Enqueue("f3h2");
            scripts.Enqueue("f1t6");
            scripts.Enqueue("t3t4");
            scripts.Enqueue("t3h2");
            scripts.Enqueue("t3t2");
            scripts.Enqueue("t3f0");
            scripts.Enqueue("t3f1");              
            scripts.Enqueue("t1f0");
            scripts.Enqueue("t1f1");

            this.DoubleBuffered = true;
            this.KeyPress += FormMain_KeyPress;
            this.Load += FormMain_Load;
            this.FormClosing += Form1_FormClosing;
        }

        private void FormMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            string notation;
            if (scripts.TryDequeue(out notation))
            {
                gui.Move(notation);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (this.gui.IsPlaying)
            {
                if (MessageBox.Show("是否放棄這一局", _GAME_TITLE, MessageBoxButtons.YesNo) == DialogResult.No) {
                    e.Cancel = true;
                }
            } 
            */
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
            //        scripts.EnqueueCardToTemp(cardView, targetCardView);
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
            //        scripts.EnqueueCardToTemp(cardView, targetCardView);
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

        public void ConfirmNewGame()
        {
            Form frmDailog = new Form();
            frmDailog.StartPosition = FormStartPosition.CenterParent;
            
            frmDailog.Width = 200;
            frmDailog.Height = 100;
            frmDailog.ShowIcon = false;
            frmDailog.MinimizeBox = false;
            frmDailog.MaximizeBox = false;
            frmDailog.FormBorderStyle = FormBorderStyle.FixedSingle;

            frmDailog.ShowDialog(this);
            frmDailog.Close();
//            MessageBox.Show(@"恭喜，您贏了!
//您想再玩一次?", "本局結束", MessageBoxButtons.YesNo);
        }
    }
}
