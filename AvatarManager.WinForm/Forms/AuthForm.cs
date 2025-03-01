using AvatarManager.Core.Helper;
using AvatarManager.Core.Infrastructures.ExternalServices.Interfaces;
using AvatarManager.WinForm.Properties;
using System.Diagnostics;

namespace AvatarManager.WinForm.Forms;

public partial class AuthForm : Form
{
    private IVRChatApiClient _vrcApi;

    public AuthForm(IVRChatApiClient vrcApi)
    {
        InitializeComponent();
        _vrcApi = vrcApi;
    }

    #region EventHandlers
    /// <summary>
    /// 認証ボタンが押されたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void authButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(authTokenInput.Text))
        {
            try
            {
                _vrcApi.Init(authTokenInput.Text);
                await _vrcApi.AuthAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "認証に失敗しました。\n" +
                    "入力されているauthcookieが不正か、既に無効になっている可能性があります。\n" +
                    "再度認証からやり直してみてください。",
                    "認証エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            var user = await _vrcApi.GetCurrentUserAsync();
            Settings.Default.authToken = TokenHelper.EncodeToken(authTokenInput.Text);
            Settings.Default.Save();
        }
        this.Close();
        this.Dispose();
    }

    /// <summary>
    /// リンクがクリックされたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        linkLabel1.LinkVisited = true;
        var sInfo = new ProcessStartInfo()
        {
            FileName = "https://vrchat.com/home",
            UseShellExecute = true
        };
        Process.Start(sInfo);
    }

    /// <summary>
    /// リンクがクリックされたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        linkLabel2.LinkVisited = true;
        var sInfo = new ProcessStartInfo()
        {
            FileName = "https://vrchat.com/api/1/auth",
            UseShellExecute = true
        };
        Process.Start(sInfo);
    }

    private void authTokenInput_KeyDown(object sender, KeyEventArgs e)
    {
        if(e.KeyCode == Keys.Enter)
        {
            this.authButton_Click(sender, e);
        }
    }
    #endregion
}
