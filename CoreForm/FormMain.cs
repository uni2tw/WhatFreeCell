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
        private int _ratio = 100;
        public FormMain()
        {
            InitializeComponent();
            
            this.Text = _GAME_TITLE;

            this._ratio = 160;
            gui = new GameUI(this);
            gui.InitScreen(_ratio);
            gui.InitEvents();

            gui.Reset(26458);
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

        public void ShowNewGameDialog()
        {
            var frm = ConfirmDialogForm.CreateGameoverContinueDialog(210 * _ratio / 100);
            var dialogResult = frm.ShowDialog(this);
            if (dialogResult == DialogResult.Yes)
            {
                bool sameGame = frm.RetrySameGame;
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }
            return;
            Form frmDialog = new Form();
            frmDialog.StartPosition = FormStartPosition.CenterParent;

            frmDialog.Width = 210 * _ratio / 100;
            frmDialog.Height = 140 * _ratio / 100;
            frmDialog.ShowIcon = false;
            frmDialog.MinimizeBox = false;
            frmDialog.MaximizeBox = false;
            frmDialog.FormBorderStyle = FormBorderStyle.FixedSingle;
            frmDialog.StartPosition = FormStartPosition.CenterParent;
            frmDialog.Text = "本局結束";
            frmDialog.Font = new Font("新細明體", frmDialog.Width / 24);
            frmDialog.ControlBox = false;

            FlowLayoutPanel container = new FlowLayoutPanel();
            container.Dock = DockStyle.Fill;
            container.FlowDirection = FlowDirection.TopDown;
            frmDialog.Controls.Add(container);

            Label lb1 = new Label();
            lb1.Text = "\r\n恭喜，您贏了!\r\n\r\n您想再玩一次?";
            lb1.Width = container.Width;
            lb1.Height = container.Height * 6 / 12;
            lb1.TextAlign = ContentAlignment.TopCenter;
            lb1.Margin = new Padding(0);
            container.Controls.Add(lb1);

            CheckBox ch1 = new CheckBox();
            ch1.Text = "選擇牌局";

            ch1.Width = container.Width;
            ch1.Height = container.Height * 3 / 12;
            ch1.Margin = new Padding(0);
            container.Controls.Add(ch1);

            FlowLayoutPanel buttonGroupPanel = new FlowLayoutPanel
            {
                Width = container.Width,
                Height = container.Height * 3 / 12,
                FlowDirection = FlowDirection.LeftToRight,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(0)
            };
            container.Controls.Add(buttonGroupPanel);
            Button btnYes = new Button
            {
                Text = "確定",
                Width = 60 * _ratio / 100,
                Height = 24 * _ratio / 100
            };
            Button btnNo = new Button
            {
                Text = "取消",
                Width = 60 * _ratio / 100,
                Height = 24 * _ratio / 100
            };
            btnYes.Click += delegate (object sender, EventArgs e)
            {
                
                frmDialog.Close();
            };
            btnNo.Click += delegate (object sender, EventArgs e)
            {
                
                frmDialog.Close();
            };
            buttonGroupPanel.Controls.Add(btnYes);
            buttonGroupPanel.Controls.Add(btnNo);

            frmDialog.ShowDialog(this);
            frmDialog.Close();         
        }
    }
}

public class ConfirmDialogForm : Form
{    
    protected FlowLayoutPanel container;
    protected FlowLayoutPanel btnGroup;
    protected FlowLayoutPanel confirmPanel;
    protected FlowLayoutPanel messagePanel;

    protected Button btnYes;
    protected Button btnNo;
    protected Label lbMessage;
    protected CheckBox chkConfirm = new CheckBox();
    public bool RetrySameGame
    {
        get
        {
            return chkConfirm.Checked;
        }
    }
    public static ConfirmDialogForm CreateGameoverContinueDialog(int width)
    {
        return new ConfirmDialogForm(width, (int)(width * 0.618f))
        {
            YesText = "是(&Y)",
            NoText = "否(&N)",
            Caption = "本局結束",
            Message = "後抱歉，這局您輸了，您不能再移動任何一張牌。\r\n您想再玩一次？",
            MessageTextAlign = ContentAlignment.TopLeft,
            ConfirmText = "相同牌局(&S)"
        };
    }

    public ConfirmDialogForm(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this.ShowIcon = false;
        this.ControlBox = false;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Padding = new Padding(0);
        this.Font = new Font("新細明體", this.Width / 24);        
    }

    protected override void OnLoad(EventArgs e)
    {
        this.Text = this.Caption;

        base.OnLoad(e);

        container = new FlowLayoutPanel();
        container.Margin = new Padding(0);
        container.Padding = new Padding(0);
        container.Dock = DockStyle.Fill;
        container.Width = this.ClientSize.Width;
        container.Height = this.ClientSize.Height;

        
        int fullWidth = container.ClientSize.Width;
        int fullHeight = container.ClientSize.Height;
        int cellWidth = fullWidth / 15;
        int cellHeight = fullHeight / 15;        
 
        container.FlowDirection = FlowDirection.TopDown;
        
        this.Controls.Add(container);        

        messagePanel = new FlowLayoutPanel
        {
            Margin = new Padding(0),
            Padding = new Padding(cellWidth, cellHeight, cellWidth, cellHeight),
            FlowDirection = FlowDirection.TopDown,
            Height = cellHeight * 8
        };        
        confirmPanel = new FlowLayoutPanel
        {
            Margin = new Padding(0),
            Padding = new Padding(cellWidth, cellHeight, cellWidth, cellHeight),
            FlowDirection = FlowDirection.TopDown,
            Height = cellHeight * 3
        };        
        btnGroup = new FlowLayoutPanel
        {
            Margin = new Padding(0),
            Padding = new Padding(cellWidth, cellHeight, cellWidth, 0),
            FlowDirection = FlowDirection.LeftToRight,
            Height = cellHeight * 4
        };
        messagePanel.Width = container.ClientRectangle.Width;        
        confirmPanel.Width = container.ClientRectangle.Width;
        btnGroup.Width = container.ClientRectangle.Width;
        container.Controls.Add(messagePanel);
        container.Controls.Add(confirmPanel);
        container.Controls.Add(btnGroup);


        lbMessage = new Label { Text = Message, Margin = new Padding(0), TextAlign = MessageTextAlign };        
        lbMessage.AutoSize = true;
        messagePanel.Controls.Add(lbMessage);

        chkConfirm = new CheckBox { Text = ConfirmText, Margin = new Padding(0) };
        chkConfirm.AutoSize = true;
        confirmPanel.Controls.Add(chkConfirm);



        btnYes = new Button { Text = YesText, Margin = new Padding(0) };
        btnNo = new Button { Text = NoText, Margin = new Padding(0) };

        btnYes.AutoSize = true;
        btnNo.AutoSize = true;
        btnGroup.Controls.Add(btnYes);
        btnGroup.Controls.Add(btnNo);
        btnNo.Margin = new(btnNo.Width / 4, 0, 0, 0);

        btnNo.Click += delegate (object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();            
        };

        btnYes.Click += delegate (object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        };

    }


    public string Caption { get; set; }
    public ContentAlignment MessageTextAlign {get;set;}
    public string Message { get; set; }
    public string ConfirmText { get; set; }
    public string YesText { get; set; }
    public string NoText { get; set; }
    public bool ConfirmResult { get; set; }
}