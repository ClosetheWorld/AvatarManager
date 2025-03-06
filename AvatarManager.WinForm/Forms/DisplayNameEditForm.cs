using AvatarManager.Core.Services.interfaces;

namespace AvatarManager.WinForm.Forms;

public partial class DisplayNameEditForm : Form
{
    private readonly IAvatarService _avatarService;
    private string _avatarId;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="avatarService"></param>
    public DisplayNameEditForm(IAvatarService avatarService)
    {
        _avatarService = avatarService;
        InitializeComponent();
    }

    #region EventHandlers
    /// <summary>
    /// 保存ボタンを押したときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void saveButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(displayNameTextBox.Text))
        {
            await _avatarService.UpdateDisplayNameByAvatarIdAsync(_avatarId, displayNameTextBox.Text);
        }
        else
        {
            await _avatarService.UpdateDisplayNameByAvatarIdAsync(_avatarId, null);
        }

        Visible = false;
    }

    /// <summary>
    /// Enterキーを押したときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void displayNameTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            this.saveButton_Click(sender, e);
        }

        if (e.KeyCode == Keys.Escape)
        {
            Visible = false;
        }
    }

    /// <summary>
    /// フォームが表示されたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DisplayNameEditForm_Shown(object sender, EventArgs e)
    {
        displayNameTextBox.Focus();
    }
    #endregion

    #region Methods
    /// <summary>
    /// パラメータをセットする
    /// </summary>
    /// <param name="currentAvatarDisplayName"></param>
    /// <param name="avatarId"></param>
    public void SetParameters(string currentAvatarDisplayName, string avatarId)
    {
        _avatarId = avatarId;
        displayNameTextBox.Text = currentAvatarDisplayName;
    }
    #endregion
}
