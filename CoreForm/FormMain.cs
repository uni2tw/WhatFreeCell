using CoreForm.UI;
using CoreForm.Utilities;
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
using static System.Windows.Forms.AxHost;

namespace CoreForm
{
    public partial class FormMain : Form, IGameForm
    {
        IGameUI gui;
        DialogManager _dialog;
        public const string _GAME_TITLE = "新接龍";
        Queue<string> scripts = new Queue<string>();
        private int _ratio = 100;
        MenuStrip _menu;

        public FormMain()
        {
            InitializeComponent();

            this.Text = _GAME_TITLE;

            Bitmap bmp = (Bitmap)ImageHelper.LoadFromResource("Icon.png");
            this.Icon = Icon.FromHandle(bmp.GetHicon());

            _dialog = new DialogManager(this);

            this._ratio = 160;
            gui = new GameUI(this, _dialog);
            gui.InitScreen(_ratio);
            gui.InitEvents();            

            InitializeMenu();

            this.DoubleBuffered = true;
            this.KeyPress += FormMain_KeyPress;
            this.Load += FormMain_Load;
            this.FormClosing += Form1_FormClosing;
        }

        /// <summary>
        /// 初始化功能表單
        /// </summary>
        private void InitializeMenu()
        {
            var self = this;

            _menu = new System.Windows.Forms.MenuStrip();

            _menu.Dock = DockStyle.Top;
            _menu.BackColor = Color.Silver;


            var menuItemGame = new ToolStripMenuItem();
            
            menuItemGame.Text = "遊戲(&G)";
            _menu.Items.Add(menuItemGame);

            menuItemGame.DropDownItems.Add(
                new ToolStripMenuItem()
                {
                    ShortcutKeys = Keys.F2,
                    Text = "新遊戲(&N)"
                }
                .AddEvent("Click", delegate (object sender, EventArgs e)
                {
                    gui.StartGame();
                })
            );
            menuItemGame.DropDownItems.Add(
                new ToolStripMenuItem()
                {
                    ShortcutKeys = Keys.F3,
                    Text = "選擇牌局(&S)"
                }.AddEvent("Click", delegate (object sender, EventArgs e)
                {
                    gui.PickNumberStartGame();
                })
            );
            menuItemGame.DropDownItems.Add(
                new ToolStripMenuItem()
                {
                    Text = "重啟牌局(&R)"
                }.AddEvent("Click", delegate (object sender, EventArgs e)
                {
                    gui.RestartGame();
                })
            );
            menuItemGame.DropDownItems.Add(new ToolStripSeparator());
            menuItemGame.DropDownItems.Add(
                new ToolStripMenuItem()
                {
                    Name = "BackToPreviousStep",
                    ShortcutKeys = Keys.F10,
                    Enabled = false,
                    Text = "復原"
                }.AddEvent("Click", delegate (object sender, EventArgs e)
                {
                    MessageBox.Show("b");
                    //gui.BackToPreviousStep();
                })
            );
            menuItemGame.DropDownItems.Add(new ToolStripSeparator());
            menuItemGame.DropDownItems.Add(
                new ToolStripMenuItem()
                {
                    Text = "結束(&X)"
                }.AddEvent("Click", delegate (object sender, EventArgs e)
                {
                    gui.QuitGame();
                })
            );

            var menuItemHelp = new ToolStripMenuItem();
            menuItemHelp.Text = "說明(&H)";
            _menu.Items.Add(menuItemHelp);
            menuItemHelp.DropDownItems.Add(
                new ToolStripMenuItem()
                {
                    Text = "關於新接龍"
                }.AddEvent("Click", delegate (object sender, EventArgs e)
                {
                    gui.AbouteGame();
                })
            );

            gui.SetMovedCallback(delegate ()
            {
                _menu.Items.Find("BackToPreviousStep", true).First().Enabled = gui.SteppingNumber > 0;
            });

            SetControl(_menu);
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
            gui.Start(26458);
            gui.CreateScripts(out scripts);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gui.QuitGameConfirm() == false)
            {
                e.Cancel = true;
            }            
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

        public void RestartGame()
        {
            
        }

        public void SetCaption(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                this.Text = _GAME_TITLE;
            }
            else
            {
                this.Text = $"{_GAME_TITLE} #{text}";
            }
        }
    }
}