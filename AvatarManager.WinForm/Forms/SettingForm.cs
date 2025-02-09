using AvatarManager.Core.Models;
using AvatarManager.Core.Models.Binding;
using AvatarManager.Core.Services.interfaces;
using System.ComponentModel;

namespace AvatarManager.WinForm.Forms
{
    public partial class SettingForm : Form
    {
        private readonly IAvatarService _avatarService;
        private readonly IFolderService _folderService;
        private string? _folderId;
        private BindingList<EditFormAvatarGrid> _grid = new BindingList<EditFormAvatarGrid>();

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
            
            avatarGrid.DataSource = _grid;
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


        private async void searchTextBox_TextChanged(object sender, EventArgs e)
        {

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
                _grid.Add(new EditFormAvatarGrid
                {
                    IsSelected = await SetAvatarGridCheckBoxAsync(c.Id),
                    AvatarThumbnail = new Bitmap(c.ImagePath),
                    AvatarName = c.Name,
                    AvatarId = c.Id
                });
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
