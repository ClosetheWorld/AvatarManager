using AvatarManager.Core.Models;
using AvatarManager.Core.Services.interfaces;

namespace AvatarManager.WinForm.Forms
{
    public partial class SettingForm : Form
    {
        private readonly IAvatarService _avatarService;
        private readonly IFolderService _folderService;
        private string? _folderId;

        public SettingForm(IAvatarService avatarService, IFolderService folderService, string? folderId)
        {
            _avatarService = avatarService;
            _folderService = folderService;
            _folderId = folderId;

            InitializeComponent();
            avatarGrid.Columns[1].ReadOnly = true;
            avatarGrid.Columns[2].ReadOnly = true;
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
                await SetAvatarGridCheckBoxAsync();
                this.Text = "フォルダ編集";
            }
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
            foreach (var c in cachedAvatars)
            {
                var row = avatarGrid.Rows.Add(null, new Bitmap(c.ImagePath), c.Name, c.Id);
                avatarGrid.Rows[row].Height = 70;
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
        private async Task SetAvatarGridCheckBoxAsync()
        {
            var folder = await _folderService.GetFolderAsync(_folderId);
            for (var i = 0; i < avatarGrid.Rows.Count; i++)
            {
                if (folder.ContainAvatarIds.Contains(avatarGrid.Rows[i].Cells[3].Value.ToString()))
                {
                    avatarGrid.Rows[i].Cells[0].Value = true;
                }
            }
        }
        #endregion
    }
}
