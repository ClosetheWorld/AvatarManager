using AvatarManager.Core.Helper;
using AvatarManager.Core.Infrastructures.Database;
using AvatarManager.Core.Infrastructures.ExternalServices.Interfaces;
using AvatarManager.Core.Models;
using AvatarManager.Core.Services.interfaces;
using AvatarManager.WinForm.Properties;
using System.Data;
using VRChat.API.Model;

namespace AvatarManager.WinForm.Forms;

public partial class MainWindow : Form
{
    private IVRChatApiClient _vrcApi;
    private CurrentUser _user;
    private readonly AuthForm _authForm;
    private readonly LoadingForm _loadingForm;
    private readonly DisplayNameEditForm _displayNameEditForm;
    private readonly SettingForm _settingForm;
    private readonly IAvatarService _avatarService;
    private readonly IFolderService _folderService;
    private int currentFolderIndex = 0;
    private DataTable _folderDataTable = new DataTable();
    private BindingSource _folderBindingSource = new BindingSource();
    private DataTable _avatarDataTable = new DataTable();
    private BindingSource _avatarBindingSource = new BindingSource();
    private List<Tuple<Bitmap, string>> _avatarThumbnails = new List<Tuple<Bitmap, string>>();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="vrcApiClient"></param>
    /// <param name="dbContext"></param>
    /// <param name="avatarService"></param>
    /// <param name="folderService"></param>
    /// <param name="displayNameEditForm"></param>
    /// <param name="settingForm"></param>
    /// <param name="loadingForm"></param>
    /// <param name="authForm"></param>
    public MainWindow(IVRChatApiClient vrcApiClient, ApplicationDbContext dbContext,
        IAvatarService avatarService, IFolderService folderService,
        DisplayNameEditForm displayNameEditForm, SettingForm settingForm, LoadingForm loadingForm, AuthForm authForm)
    {
        _vrcApi = vrcApiClient;
        _avatarService = avatarService;
        _folderService = folderService;
        _authForm = authForm;
        _loadingForm = loadingForm;
        _displayNameEditForm = displayNameEditForm;
        _settingForm = settingForm;

        Settings.Default.Upgrade();
        if (string.IsNullOrEmpty(Settings.Default.authToken))
        {
            _authForm.StartPosition = FormStartPosition.CenterParent;
            _authForm.ShowDialog();
            _user = _vrcApi.GetCurrentUser();
        }
        else
        {
            _vrcApi.Init(TokenHelper.DecodeToken(Settings.Default.authToken));
            try
            {
                _vrcApi.Auth();
            }
            catch (Exception e)
            {
                HandleAuthException("認証に失敗しました。再度ログインしてください。");
            }
            _user = _vrcApi.GetCurrentUser();
        }
        InitializeComponent();
        userName.Text = _user.DisplayName;
        Text = $"AvatarManager v{Application.ProductVersion}";
    }

    #region EventHandlers
    /// <summary>
    /// フォームが表示されたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void MainWindow_Shown(object sender, EventArgs e)
    {
        // avatar キャッシュ処理
        _loadingForm.SetCurrentUser(_user);
        _loadingForm.StartPosition = FormStartPosition.CenterParent;
        _loadingForm.ShowDialog();

        // avatarThumbnail生成
        await GenerateAvatarThumbnailBitMapListAsync();

        // DataTable初期化
        InitDataTable();

        // folderGridView 設定
        folderGrid.RowTemplate.Height = 100;
        folderGrid.RowTemplate.ContextMenuStrip = folderRightClickMenu;
        folderGrid.RowTemplate.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12);

        // avatarGridView 設定
        avatarGrid.RowTemplate.Height = 70;
        avatarGrid.RowTemplate.ContextMenuStrip = avatarRightClickMenu;
        avatarGrid.RowTemplate.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12);        

        // folderGridView BindingSource設定
        _folderBindingSource.DataSource = _folderDataTable;
        folderGridBindingSource.DataSource = _folderBindingSource;

        // folderGridView 生成
        await GenerateFolderGridAsync();

        // avatarGridView BindingSource設定
        _avatarBindingSource.DataSource = _avatarDataTable;
        avatarGridBindingSource.DataSource = _avatarBindingSource;

        avatarGrid.CurrentCell = null;
    }

    /// <summary>
    /// フォルダがクリックされたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void folderGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == currentFolderIndex)
        {
            return;
        }
        else
        {
            currentFolderIndex = e.RowIndex;
        }

        await GenerateFolderAvatarGridAsync();
    }

    /// <summary>
    /// フォルダ追加ボタンが押されたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void settingButton_Click(object sender, EventArgs e)
    {
        _settingForm.SetFolderId(null);
        _settingForm.SetBitmapList(_avatarThumbnails);
        _settingForm.StartPosition = FormStartPosition.CenterParent;
        _settingForm.ShowDialog();

        // update folder grid
        currentFolderIndex = 0;
        _folderDataTable.Clear();
        await GenerateFolderGridAsync();
    }

    /// <summary>
    /// アバターグリッドがクリックされたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void avatarGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        // avatarGridの画像がクリックされた場合フォーカスセルを名前のカラムに変更
        if (e.RowIndex > -1 && e.ColumnIndex == 0)
        {
            avatarGrid.CurrentCell = avatarGrid.Rows[e.RowIndex].Cells[1];
        }

        // avatarGridのヘッダーがクリックされた場合は何もしない
        if (e.RowIndex >= 0)
        {
            try
            {
                await _vrcApi.SetCurrentAvatarAsync(avatarGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
            catch (Exception ex)
            {
                HandleAuthException("アバターの切り替えに失敗しました。認証情報が切れている可能性があります。");
            }
        }
    }

    /// <summary>
    /// folderGridで右クリックメニューの削除がクリックされたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void deleteMenuItem_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show($"以下のフォルダを削除します\n{folderGrid.Rows[currentFolderIndex].Cells[0].Value.ToString()}", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
        {
            await _folderService.DeleteFolderAsync(folderGrid.Rows[currentFolderIndex].Cells[1].Value.ToString());
            currentFolderIndex = 0;
            _folderDataTable.Clear();
            await GenerateFolderGridAsync();
            MessageBox.Show("削除しました");
        }
    }

    /// <summary>
    /// folderGridで右クリックメニューの編集がクリックされたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void editMenuItem_Click(object sender, EventArgs e)
    {
        // 右クリックされた行のindexをTagから取得
        if (folderRightClickMenu.Tag is int rowIndex && rowIndex >= 0)
        {
            _settingForm.SetFolderId(folderGrid.Rows[rowIndex].Cells[1].Value.ToString());
            _settingForm.SetBitmapList(_avatarThumbnails);
            _settingForm.StartPosition = FormStartPosition.CenterParent;
            _settingForm.ShowDialog();
            currentFolderIndex = 0;
            _folderDataTable.Clear();
            await GenerateFolderGridAsync();
        }
    }

    /// <summary>
    /// folderGridで右クリックメニューが開かれるときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void folderRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var mousePosition = folderGrid.PointToClient(Cursor.Position);
        var hitTestInfo = folderGrid.HitTest(mousePosition.X, mousePosition.Y);

        // 未分類を右クリックしたときは編集と削除を非表示
        if (hitTestInfo.RowIndex == folderGrid.RowCount - 1)
        {
            e.Cancel = true;
        }

        if (hitTestInfo.RowIndex >= 0)
        {
            // 右クリックされた行のindexをtagにセット
            folderRightClickMenu.Tag = hitTestInfo.RowIndex;
        }
        else
        {
            e.Cancel = true;
        }
    }

    /// <summary>
    /// avatarGridで右クリックメニューが開かれるときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void avatarRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var mousePosition = avatarGrid.PointToClient(Cursor.Position);
        var hitTestInfo = avatarGrid.HitTest(mousePosition.X, mousePosition.Y);

        if (hitTestInfo.RowIndex >= 0)
        {
            // 右クリックされた行のindexをtagにセット
            avatarRightClickMenu.Tag = hitTestInfo.RowIndex;
        }
        else
        {
            e.Cancel = true;
        }
    }

    /// <summary>
    /// avatarGridで右クリックメニューの表示名を編集がクリックされたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void editAvatarDisplayNameMenuItem_Click(object sender, EventArgs e)
    {
        // 右クリックされた行のindexをTagから取得
        if (avatarRightClickMenu.Tag is int rowIndex && rowIndex >= 0)
        {
            var clickedAvatarId = avatarGrid.Rows[rowIndex].Cells[2].Value.ToString();
            var currentAvatarDisplayName = await _avatarService.GetDisplayNameByAvatarIdAsync(clickedAvatarId);
            _displayNameEditForm.SetParameters(currentAvatarDisplayName ?? null, clickedAvatarId);
            _displayNameEditForm.StartPosition = FormStartPosition.CenterParent;
            _displayNameEditForm.ShowDialog();
            currentFolderIndex = 0;
            _folderDataTable.Clear();
            await GenerateFolderGridAsync();
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// フォルダグリッドを生成する
    /// </summary>
    /// <returns></returns>
    private async Task GenerateFolderGridAsync()
    {
        // dbにあるフォルダでfolderGridを生成
        var folders = await _folderService.GetFoldersAsync();
        SetFolderDataTable(folders);

        // 末尾
        SetFolderDataTableLastRow();

        // 先頭にフォーカス
        folderGrid.CurrentCell = folderGrid.Rows[0].Cells[0];

        // フォーカスした先が未分類の場合全アバター表示
        if ((string)folderGrid.CurrentCell.Value == "未分類")
        {
            await GenerateUnCategorizedAvatarGridAsync();
        }
        else
        {
            await GenerateFolderAvatarGridAsync();
        }
    }

    /// <summary>
    /// フォルダ内のアバターグリッドを生成する
    /// </summary>
    /// <returns></returns>
    private async Task GenerateFolderAvatarGridAsync()
    {
        _avatarDataTable.Clear();
        var folderId = folderGrid.Rows[currentFolderIndex].Cells[1].Value.ToString();
        var folder = await _folderService.GetFolderAsync(folderId);

        if (folder == null)
        {
            var avatars = await _avatarService.GetUnCategorizedAvatarsAsync();
            SetAvatarDataTable(avatars);
            avatarGrid.CurrentCell = null;
        }
        else
        {
            var allAvatars = await _avatarService.GetCachedAvatarsAsync();
            SetAvatarDataTable(allAvatars.Where(x => folder.ContainAvatarIds.Contains(x.Id)).ToList());
            avatarGrid.CurrentCell = null;
        }
    }

    /// <summary>
    /// 未分類のアバターグリッドを生成する
    /// </summary>
    /// <returns></returns>
    private async Task GenerateUnCategorizedAvatarGridAsync()
    {
        var cachedAvatars = await _avatarService.GetUnCategorizedAvatarsAsync();
        SetAvatarDataTable(cachedAvatars);
    }

    /// <summary>
    /// 認証エラー時の処理
    /// </summary>
    private void HandleAuthException(string msg)
    {
        Settings.Default.Reset();
        MessageBox.Show(msg, "認証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Application.CompanyName}"), true);
        _authForm.StartPosition = FormStartPosition.CenterParent;
        _authForm.ShowDialog();
        _user = _vrcApi.GetCurrentUser();
    }

    /// <summary>
    /// DataTableを初期化する
    /// </summary>
    private void InitDataTable()
    {
        _folderDataTable.Columns.Add("Id", typeof(string));
        _folderDataTable.Columns.Add("FolderName", typeof(string));

        _avatarDataTable.Columns.Add("AvatarThumbnail", typeof(Bitmap));
        _avatarDataTable.Columns.Add("AvatarName", typeof(string));
        _avatarDataTable.Columns.Add("AvatarId", typeof(string));
    }

    /// <summary>
    /// DataTableにアバター情報をセットする
    /// </summary>
    /// <param name="avatars"></param>
    private void SetAvatarDataTable(List<OwnedAvatar> avatars)
    {
        foreach (var a in avatars)
        {
            var row = _avatarDataTable.NewRow();
            row["AvatarThumbnail"] = _avatarThumbnails.Single(x => x.Item2 == a.Id).Item1;
            row["AvatarName"] = a.DisplayName != null ? $"{a.DisplayName} ({a.Name})" : a.Name;
            row["AvatarId"] = a.Id;
            _avatarDataTable.Rows.Add(row);
        }
    }

    /// <summary>
    /// DataTableにフォルダ情報をセットする
    /// </summary>
    /// <param name="folders"></param>
    private void SetFolderDataTable(List<Folder> folders)
    {
        foreach (var f in folders)
        {
            var row = _folderDataTable.NewRow();
            row["Id"] = f.Id;
            row["FolderName"] = f.Name;
            _folderDataTable.Rows.Add(row);
        }
    }

    /// <summary>
    /// DataTableに未分類のフォルダ情報をセットする
    /// </summary>
    private void SetFolderDataTableLastRow()
    {
        var row = _folderDataTable.NewRow();
        row["Id"] = "unLabeled";
        row["FolderName"] = "未分類";
        _folderDataTable.Rows.Add(row);
    }

    /// <summary>
    /// アバターサムネイルのビットマップリストを生成する
    /// </summary>
    /// <returns></returns>
    private async Task GenerateAvatarThumbnailBitMapListAsync()
    {
        var avatars = await _avatarService.GetCachedAvatarsAsync();
        foreach (var a in avatars)
        {
            _avatarThumbnails.Add(new(new Bitmap(a.ImagePath), a.Id));
        }
    }
    #endregion
}
