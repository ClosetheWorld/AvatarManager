using AvatarManager.Core.Helper;
using AvatarManager.Core.Infrastructures.ExternalServices.Interfaces;
using AvatarManager.WinForm.Properties;
using System.Diagnostics;

namespace AvatarManager.Winform;

public partial class AuthForm : Form
{
    private IVRChatApiClient _vrcApi;

    public AuthForm(IVRChatApiClient vrcApi)
    {
        InitializeComponent();
        _vrcApi = vrcApi;
    }

    /// <summary>
    /// 認証ボタンが押されたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void authButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(authTokenInput.Text))
        {
            _vrcApi.Init(authTokenInput.Text);
            await _vrcApi.AuthAsync();
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
}
