using System.Windows.Forms;

public class DialogManager
{
    private Form _owner;
    public DialogManager(Form owner)
    {
        this._owner = owner;
    }
    /// <summary>
    /// 是否放棄這一局
    /// </summary>
    /// <param name="width"></param>
    /// <returns></returns>
    public FormDialogResult ShowCancelGameDialog()
    {
        return new FormDialogResult(MessageBox.Show("是否放棄這一局?", "新接龍", MessageBoxButtons.YesNo, MessageBoxIcon.Question));        
    }
    public FormDialogResult ShowYouWinContinueDialog(int width)
    {
        var frm =DialogForms.ConfirmDialogForm.CreateYouWinContinueDialog(width);
        var dialogResult = frm.ShowDialog(_owner);
        FormDialogResult result = new FormDialogResult(dialogResult)
        {            
            CheckedYes = frm.ConfirmYes
        };
        return result;
    }

    public FormDialogResult ShowGameoverContinueDialog(int width)
    {
        var frm = DialogForms.ConfirmDialogForm.CreateGameoverContinueDialog(width);
        var dialogResult = frm.ShowDialog(_owner);
        FormDialogResult result = new FormDialogResult(dialogResult)
        {
            CheckedYes = frm.ConfirmYes
        };
        return result;
    }

    public FormDialogResult ShowSelectGameNumberDialog(int width, int gameNumber)
    {
        DialogForms.SelectGameNumberDialog frm = new (width, (int)(width * 0.618f))
        {
            YesText = "確定",
            Caption = "牌局編號",
            Message = "\r\n請選擇牌局編號\r\n從 1 到10000\r\n",
            InputText = gameNumber.ToString()
        };
        var dialogResult = frm.ShowDialog(_owner);
        FormDialogResult result = new FormDialogResult(dialogResult)
        {
            ReturnText = frm.InputText
        };
        return result;
    }
}

public class FormDialogResult
{
    public FormDialogResult(DialogResult dialogResult)
    {
        this.Reuslt = dialogResult;
    }

    public DialogResult Reuslt { get; set; }
    public string ReturnText { get; set; }
    public bool CheckedYes { get; set; }
}