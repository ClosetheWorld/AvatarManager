using AvatarManager.Core.Models;
using AvatarManager.Core.Services.interfaces;
using System.Data;

namespace AvatarManager.WinForm.Forms;

public partial class SettingForm : Form
{
    private readonly IAvatarService _avatarService;
    private readonly IFolderService _folderService;
    private string? _folderId;
    private DataTable _dataTable = new DataTable();
    private BindingSource _bindingSource = new BindingSource();
    private List<Tuple<Bitmap, string>> _avatarThumbnails = new List<Tuple<Bitmap, string>>();

    public SettingForm(IAvatarService avatarService, IFolderService folderService)
    {
        _avatarService = avatarService;
        _folderService = folderService;

        InitializeComponent();
        InitDataTable();
    }

    #region EventHandlers
    /// <summary>
    /// フォームが表示されたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void SettingForm_Shown(object sender, EventArgs e)
    {
        // 再表示時に初期化処理を行う
        _dataTable.Clear();
        searchTextBox.Text = "";
        _bindingSource.DataSource = _dataTable;
        avatarGridBindingSource.DataSource = _bindingSource;
        avatarGrid.Focus();
        avatarSelectTabControl.SelectedIndex = 0;

        await GenerateAllAvatarGridAsync();
        if (_folderId != null)
        {
            await SetFolderNameAsync();
            Text = "フォルダ編集";
        }
        else
        {
            folderNameTextBox.Text = "";
        }
    }

    /// <summary>
    /// 保存ボタンが押されたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void saveButton_Click(object sender, EventArgs e)
    {
        // Filterをクリアしないとチェックボックスが正しく取得できない？
        _bindingSource.Filter = "";

        var avatars = new List<string>();
        for (var i = 0; i < avatarGrid.Rows.Count; i++)
        {
            if ((bool)_dataTable.Rows[i][0] == true)
            {
                avatars.Add(_dataTable.Rows[i][3].ToString());
            }
        }

        if (string.IsNullOrEmpty(_folderId))
        {
            await _folderService.AddFolderAsync(new Folder
            {
                Id = Guid.NewGuid().ToString(),
                Name = folderNameTextBox.Text,
                ContainAvatarIds = avatars
            });
        }
        else
        {
            switch (avatarSelectTabControl.SelectedIndex)
            {
                // 全アバタータブの時はフォルダを置換
                case 0:
                    // TODO: Folderをnewしない
                    await _folderService.UpdateFolderAsync(new Folder
                    {
                        Id = _folderId,
                        Name = folderNameTextBox.Text,
                        ContainAvatarIds = avatars
                    });
                    break;
                // 未分類アバタータブの時はフォルダに追加
                case 1:
                    await _folderService.AddContainAvatarIdToExistsFolderAsync(_folderId, avatars);
                    break;
            }
        }

        Visible = false;
    }

    /// <summary>
    /// 検索ワードが変更された時の処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void searchTextBox_TextChanged(object sender, EventArgs e)
    {
        _bindingSource.Filter = $"AvatarName like '%{searchTextBox.Text}%'";
    }

    /// <summary>
    /// キーダウン時の処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void avatarGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            Visible = false;
        }
    }

    /// <summary>
    /// タブが変更されたときの処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void avatarSelectTabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        _dataTable.Clear();
        _bindingSource.DataSource = _dataTable;
        searchTextBox.Text = "";

        switch (avatarSelectTabControl.SelectedIndex)
        {
            // 全アバター
            case 0:
                await GenerateAllAvatarGridAsync();
                break;
            // 未分類アバター
            case 1:
                await GenerateUncategorizedAvatarGridAsync();
                break;
        }
    }

    #endregion

    #region Methods
    /// <summary>
    /// フォルダIDを設定する
    /// </summary>
    /// <param name="folderId"></param>
    public void SetFolderId(string? folderId)
    {
        _folderId = folderId;
    }

    /// <summary>
    /// ビットマップリストを設定する
    /// </summary>
    /// <param name="list"></param>
    public void SetBitmapList(List<Tuple<Bitmap, string>> list)
    {
        _avatarThumbnails = list;
    }

    /// <summary>
    /// アバターグリッドを生成する
    /// </summary>
    /// <returns></returns>
    private async Task GenerateAllAvatarGridAsync()
    {
        var cachedAvatars = await _avatarService.GetCachedAvatarsAsync();
        await SetDataTable(cachedAvatars);
    }

    /// <summary>
    /// 未分類アバターグリッドを生成する
    /// </summary>
    /// <returns></returns>
    private async Task GenerateUncategorizedAvatarGridAsync()
    {
        var uncategorizedAvatars = await _avatarService.GetUnCategorizedAvatarsAsync();
        await SetDataTable(uncategorizedAvatars);
    }

    /// <summary>
    /// データテーブルを初期化する
    /// </summary>
    private void InitDataTable()
    {
        _dataTable.Columns.Add("IsSelected", typeof(bool));
        _dataTable.Columns.Add("AvatarThumbnail", typeof(Bitmap));
        _dataTable.Columns.Add("AvatarName", typeof(string));
        _dataTable.Columns.Add("AvatarId", typeof(string));
    }

    /// <summary>
    /// データテーブルにデータを設定する
    /// </summary>
    /// <param name="avatars"></param>
    /// <returns></returns>
    private async Task SetDataTable(List<OwnedAvatar> avatars)
    {
        foreach (var c in avatars)
        {
            var row = _dataTable.NewRow();
            row["IsSelected"] = string.IsNullOrEmpty(_folderId) ? false : await SetAvatarGridCheckBoxAsync(c.Id);
            row["AvatarThumbnail"] = _avatarThumbnails.Single(x => x.Item2 == c.Id).Item1;
            row["AvatarName"] = c.Name;
            row["AvatarId"] = c.Id;
            _dataTable.Rows.Add(row);
        }
    }

    /// <summary>
    /// フォルダ名を設定する
    /// </summary>
    /// <returns></returns>
    private async Task SetFolderNameAsync()
    {
        folderNameTextBox.Text = (await _folderService.GetFolderAsync(_folderId)).Name;
    }

    /// <summary>
    /// アバターグリッドのチェックボックスを設定する
    /// </summary>
    /// <returns></returns>
    private async Task<bool> SetAvatarGridCheckBoxAsync(string avatarId)
    {
        var folder = await _folderService.GetFolderAsync(_folderId);

        if (folder.ContainAvatarIds.Contains(avatarId))
        {
            return true;
        }

        return false;
    }
    #endregion
}
