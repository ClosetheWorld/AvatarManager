using AvatarManager.Core.Models;
using AvatarManager.Core.Services.interfaces;
using System.Data;

namespace AvatarManager.WinForm.Forms
{
    public partial class SettingForm : Form
    {
        private readonly IAvatarService _avatarService;
        private readonly IFolderService _folderService;
        private string? _folderId;
        private DataTable _dataTable = new DataTable();
        private BindingSource _bindingSource = new BindingSource();

        public SettingForm(IAvatarService avatarService, IFolderService folderService, string? folderId)
        {
            _avatarService = avatarService;
            _folderService = folderService;
            _folderId = folderId;

            InitializeComponent();
        }

        #region EventHandlers
        /// <summary>
        /// フォームが表示されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SettingForm_Shown(object sender, EventArgs e)
        {
            await GenerateAllAvatarGridAsync();
            if (_folderId != null)
            {
                await SetFolderNameAsync();
                this.Text = "フォルダ編集";
            }

            _bindingSource.DataSource = _dataTable;
            editFormAvatarGridBindingSource.DataSource = _bindingSource;
        }

        /// <summary>
        /// 保存ボタンが押されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void saveButton_Click(object sender, EventArgs e)
        {
            var avatars = new List<string>();
            for (var i = 0; i < avatarGrid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(avatarGrid.Rows[i].Cells[0].Value) == true)
                {
                    avatars.Add(avatarGrid.Rows[i].Cells[3].Value.ToString());
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
                await _folderService.UpdateFolderAsync(new Folder
                {
                    Id = _folderId,
                    Name = folderNameTextBox.Text,
                    ContainAvatarIds = avatars
                });
            }

            this.Close();
            this.Dispose();
        }
        #endregion

        #region Methods
        /// <summary>
        /// アバターグリッドを生成する
        /// </summary>
        /// <returns></returns>
        private async Task GenerateAllAvatarGridAsync()
        {
            var cachedAvatars = await _avatarService.GetCachedAvatarsAsync();
            _dataTable.Columns.Add("IsSelected", typeof(bool));
            _dataTable.Columns.Add("AvatarThumbnail", typeof(Bitmap));
            _dataTable.Columns.Add("AvatarName", typeof(string));
            _dataTable.Columns.Add("AvatarId", typeof(string));

            foreach (var c in cachedAvatars)
            {
                var row = _dataTable.NewRow();
                row["IsSelected"] = string.IsNullOrEmpty(_folderId) ? false : await SetAvatarGridCheckBoxAsync(c.Id);
                row["AvatarThumbnail"] = new Bitmap(c.ImagePath);
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
}
