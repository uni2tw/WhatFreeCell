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
        public FormMain()
        {
            InitializeComponent();

            this.Text = _GAME_TITLE;
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

            var menu = new System.Windows.Forms.MenuStrip();

            menu.Dock = DockStyle.Top;
            menu.BackColor = Color.Silver;


            var menuItem0 = new ToolStripMenuItem();
            menuItem0.Text = "遊戲(&G)";
            menu.Items.Add(menuItem0);
            menuItem0.DropDownItems.Add("新遊戲", null).Click += delegate (object sender, EventArgs e)
            {
                gui.StartGame();
            };
            menuItem0.DropDownItems.Add("選擇牌局", null).Click += delegate (object sender, EventArgs e)
            {
                gui.PickNumberStartGame();
            };
            menuItem0.DropDownItems.Add("重啟牌局", null).Click += delegate (object sender, EventArgs e)
            {
                gui.RestartGame();
            };
            menuItem0.DropDownItems.Add(new ToolStripSeparator());
            menuItem0.DropDownItems.Add("復原", null).Click += delegate (object sender, EventArgs e)
            {
                //gui.BackToPreviousStep();
            };
            menuItem0.DropDownItems.Add(new ToolStripSeparator());
            menuItem0.DropDownItems.Add("結束(&X)", null).Click += delegate (object sender, EventArgs e)
            {
                gui.QuitGame();
            };

            var menuItem1 = new ToolStripMenuItem();
            menuItem1.Text = "說明(&H))";
            menu.Items.Add(menuItem1);
            menuItem1.DropDownItems.Add("關於新接龍", null).Click += delegate (object sender, EventArgs e)
            {
                gui.AbouteGame();
            };

            SetControl(menu);
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
            //gui.Start(26458);
            //gui.CreateScripts(out scripts);
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