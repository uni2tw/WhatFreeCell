using FreeCellSolitaire.Data;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public class DialogForms
{
    public class SelectGameNumberDialog : Form
    {
        public SelectGameNumberDialog(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.ShowIcon = false;
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Padding = new Padding(0);
            this.Font = new Font("新細明體", this.Width / 24);
        }
        public string YesText { get; set; }
        public string Caption
        {
            set
            {
                this.Text = value;
            }
        }

        public string Message { get; set; }
        public string InputText { get; set; }


        protected override void OnLoad(EventArgs e)
        {
            TableLayoutPanel container = new TableLayoutPanel();
            container.Dock = DockStyle.Fill;

            container.ColumnCount = 1;
            container.RowCount = 3;
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            container.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));

            this.Controls.Add(container);


            Label lbMessage = new Label { Text = Message, Margin = new Padding(0), TextAlign = ContentAlignment.TopCenter };
            lbMessage.Dock = DockStyle.Fill;
            lbMessage.AutoSize = true;
            container.Controls.Add(lbMessage, 0, 0);

            Button btn = null;
            TextBox tbNumber = new TextBox();
            tbNumber.Text = this.InputText;
            tbNumber.MaxLength = 4;
            tbNumber.KeyPress += delegate (object sender, KeyPressEventArgs e)
            {
                if (char.IsDigit(e.KeyChar) == false)
                {
                    e.Handled = true;
                }
                if (e.KeyChar == 13)
                {                    
                    e.Handled = true;
                    btn?.PerformClick();
                }
            };
            container.Controls.Add(tbNumber, 0, 1);            
            tbNumber.Margin = new Padding(
                (int)((tbNumber.Parent.Width - tbNumber.Width) / 2), 0, 0, 0);
            tbNumber.TextAlign = HorizontalAlignment.Center;

            btn = new Button();
            btn.Text = this.YesText;
            btn.AutoSize = true;            
            container.Controls.Add(btn, 0, 2);
            btn.Margin = new Padding(
                (int)((btn.Parent.Width - btn.Width) / 2), 0, 0, 0);

            btn.Click += delegate (object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Yes;
                this.InputText = tbNumber.Text;
                this.Close();
            };

            base.OnLoad(e);
        }

    }

    public class AboutGameDialogForm : Form
    {
        public AboutGameDialogForm(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.ShowIcon = false;
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Padding = new Padding(0);
            this.Font = new Font("新細明體", this.Width / 24);
        }

        public string Caption
        {
            set
            {
                this.Text = value;
            }
        }


        public string MessageTopic { get; set; }
        public string MessageBody { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            TableLayoutPanel container = new TableLayoutPanel();
            container.Dock = DockStyle.Fill;
            this.Padding = new Padding(10);
            container.ColumnCount = 1;
            container.RowCount = 3;
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            container.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            this.Controls.Add(container);

            Label lbMessage = new Label { Text = MessageTopic, Margin = new Padding(0), TextAlign = ContentAlignment.MiddleCenter };
            lbMessage.Dock = DockStyle.Fill;
            lbMessage.AutoSize = true;
            container.Controls.Add(lbMessage, 0, 0);

            Label lbMessage2 = new Label { Text = MessageBody, Margin = new Padding(0), TextAlign = ContentAlignment.TopLeft };
            lbMessage2.Dock = DockStyle.Fill;
            lbMessage2.AutoSize = true;

            container.Controls.Add(lbMessage2, 0, 1);

            Button btn = new Button();
            btn.Text = "確定";
            btn.AutoSize = true;
            container.Controls.Add(btn, 0, 2);
            btn.Margin = new Padding(
                (int)((btn.Parent.Width - btn.Width) / 2), 0, 0, 0);

            btn.Click += delegate (object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Yes;                
                this.Close();
            };

            base.OnLoad(e);
        }
    }
    public class ConfirmDialogForm : Form
    {
        protected FlowLayoutPanel container;
        protected FlowLayoutPanel btnGroup;
        protected FlowLayoutPanel confirmPanel;
        protected TableLayoutPanel messagePanel;

        protected Button btnYes;
        protected Button btnNo;
        protected Label lbMessage;
        protected CheckBox chkConfirm = new CheckBox();
        public bool ConfirmYes
        {
            get
            {
                return chkConfirm.Checked;
            }
        }


        public static ConfirmDialogForm CreateYouWinContinueDialog(int width)
        {
            return new ConfirmDialogForm(width, (int)(width * 0.618f))
            {
                YesText = "是(&Y)",
                NoText = "否(&N)",
                Caption = "本局結束",
                Message = "恭喜，您贏了!\r\n\r\n您想再玩一次?",
                MessageTextAlign = ContentAlignment.TopLeft,
                ConfirmText = "選擇牌局(&S)",
                ConfirmChecked = true
            };
        }

        public static ConfirmDialogForm CreateGameoverContinueDialog(int width)
        {
            return new ConfirmDialogForm(width, (int)(width * 0.618f))
            {
                YesText = "是(&Y)",
                NoText = "否(&N)",
                Caption = "本局結束",
                Message = "很抱歉，這局您輸了，您不能再移動任何一張牌。\r\n您想再玩一次？",
                MessageTextAlign = ContentAlignment.TopLeft,
                ConfirmText = "相同牌局(&S)",
                ConfirmChecked = true
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

            messagePanel = new TableLayoutPanel
            {
                Margin = new Padding(0),
                Padding = new Padding(cellWidth, cellHeight, cellWidth, cellHeight),
                Height = cellHeight * 8,

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
            //messagePanel.Width = container.ClientRectangle.Width;                
            confirmPanel.Width = container.ClientRectangle.Width;
            btnGroup.Width = container.ClientRectangle.Width;
            container.Controls.Add(messagePanel);
            container.Controls.Add(confirmPanel);
            container.Controls.Add(btnGroup);


            lbMessage = new Label { Text = Message, Margin = new Padding(0), TextAlign = MessageTextAlign };
            lbMessage.Dock = DockStyle.Fill;
            lbMessage.AutoSize = true;
            messagePanel.Controls.Add(lbMessage);

            chkConfirm = new CheckBox { Text = ConfirmText, Margin = new Padding(0) };
            chkConfirm.AutoSize = true;
            chkConfirm.Checked = ConfirmChecked;
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
        public string InputText { get; set; }
        public ContentAlignment MessageTextAlign { get; set; }
        public string Message { get; set; }
        public string ConfirmText { get; set; }
        public bool ConfirmChecked { get; set; }
        public string YesText { get; set; }
        public string NoText { get; set; }
        public bool ConfirmResult { get; set; }
    }

    public class StatisticalResultDiualogForm : Form
    {
        public StatisticalResultDiualogForm(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.ShowIcon = false;
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Padding = new Padding(0);
            this.Font = new Font("新細明體", this.Height / 32);
        }

        public string Caption
        {
            set
            {
                this.Text = value;
            }
        }
        public GameRecordSummary Summary { get; set; }
        public Delegate ClearRecordsCallback { get; set; }

        protected override void OnLoad(EventArgs e)
        {            

            TableLayoutPanel container = new TableLayoutPanel();
            container.Dock = DockStyle.Fill;
            this.Padding = new Padding(10);
            container.ColumnCount = 1;
            container.RowCount = 3;
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            container.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));            
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            this.Controls.Add(container);

            TableLayoutPanel tablePanel = new TableLayoutPanel
            {
                 AutoSize = true,
                 Dock =  DockStyle.Fill,
                 Margin = new Padding(0),                 
            };
            tablePanel.ColumnCount = 4;
            tablePanel.ColumnStyles.Add(new ColumnStyle { Width = 25, SizeType = SizeType.Percent });
            tablePanel.ColumnStyles.Add(new ColumnStyle { Width = 25, SizeType = SizeType.Percent });
            tablePanel.ColumnStyles.Add(new ColumnStyle { Width = 25, SizeType = SizeType.Percent });
            tablePanel.ColumnStyles.Add(new ColumnStyle { Width = 25, SizeType = SizeType.Percent });
            tablePanel.RowCount = 12;
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });
            tablePanel.RowStyles.Add(new RowStyle { Height = 8, SizeType = SizeType.Percent });

            tablePanel.Controls.Add(new Label { Text = "本局:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 0, 0);
            tablePanel.Controls.Add(new Label { Text = "贏:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 1, 1);
            tablePanel.Controls.Add(new Label { Text = Summary.TimesWonThisGame.ToString(), AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 2, 1);
            tablePanel.Controls.Add(new Label { Text = "輸:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 1, 2);            
            tablePanel.Controls.Add(new Label { Text =  Summary.TimesLostThisGame.ToString(), AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 2, 2);
            tablePanel.Controls.Add(new Label { Text = "最佳記錄:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 1, 3);
            tablePanel.Controls.Add(new Label { Text = Summary.BestRecordOfTheGame?.ToString("s") ?? "無", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 2, 3);
            

            tablePanel.Controls.Add(new Label { Text = "總計:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 0, 4);
            tablePanel.Controls.Add(new Label { Text = "贏:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 1, 5);
            tablePanel.Controls.Add(new Label { Text = Summary.TotalTimesWon.ToString(), AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 2, 5);
            tablePanel.Controls.Add(new Label { Text = "輸:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 1, 6);
            tablePanel.Controls.Add(new Label { Text = Summary.TotalTimesLost.ToString(), AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 2, 6);

            tablePanel.Controls.Add(new Label { Text = "連勝/連敗:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 0, 8);
            tablePanel.Controls.Add(new Label { Text = "贏:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 1, 9);
            tablePanel.Controls.Add(new Label { Text = Summary.WinningStreak.ToString(), AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 2, 9);
            tablePanel.Controls.Add(new Label { Text = "輸:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 1, 10);
            tablePanel.Controls.Add(new Label { Text = Summary.LosingStreak.ToString(), AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 2, 10);
            tablePanel.Controls.Add(new Label { Text = "目前:", AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 1, 11);
            string recentWinningOrLosingStreakDisplay = string.Format("{0}{1} 次",
                Summary.RecentWinningOrLosingStreak > 0 ? "贏 " : "輸 ", Math.Abs(Summary.RecentWinningOrLosingStreak));
            tablePanel.Controls.Add(new Label { Text = recentWinningOrLosingStreakDisplay, AutoSize = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight }, 2, 11);           

            container.Controls.Add(tablePanel, 0, 0);


            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.ColumnCount = 5;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            panel.RowCount = 3;

            container.Controls.Add(panel, 0, 1);

            {
                Button btn = new Button();
                btn.Text = "確定";
                btn.Dock = DockStyle.Fill;
                panel.Controls.Add(btn, 1, 1);

                btn.Click += delegate (object sender, EventArgs e)
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                };
            }

            {
                Button btn = new Button();
                btn.Text = "清除";
                btn.Dock = DockStyle.Fill;
                panel.Controls.Add(btn, 3, 1);
                btn.Click += delegate (object sender, EventArgs e)
                {
                    this.DialogResult = DialogResult.Yes;                   
                    ClearRecordsCallback.DynamicInvoke();
                    Console.WriteLine("清除個人記錄");
                    this.Close();
                };
            }

            base.OnLoad(e);
        }
    }
}