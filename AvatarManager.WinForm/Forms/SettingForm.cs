using AvatarManager.Core.Models;
using AvatarManager.Core.Services.interfaces;

namespace AvatarManager.WinForm.Forms
{
    public partial class SettingForm : Form
    {
        private readonly IAvatarService _avatarService;
        private readonly IFolderService _folderService;

        public SettingForm(IAvatarService avatarService, IFolderService folderService)
        {
            _avatarService = avatarService;
            _folderService = folderService;

            InitializeComponent();
            avatarGrid.Columns[1].ReadOnly = true;
            avatarGrid.Columns[2].ReadOnly = true;
        }

        /// <summary>
        /// フォームが表示されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SettingForm_Shown(object sender, EventArgs e)
        {
            await GenerateAllAvatarGridAsync();
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
                if (avatarGrid.Rows[i].Cells[0].Value != null)
                {
                    avatars.Add(avatarGrid.Rows[i].Cells[3].Value.ToString());
                }
            }

            await _folderService.AddFolderAsync(new Folder
            {
                Id = Guid.NewGuid().ToString(),
                Name = folderNameTextBox.Text,
                ContainAvatarIds = avatars
            });

            this.Close();
            this.Dispose();
        }

        private async Task GenerateAllAvatarGridAsync()
        {
            var cachedAvatars = await _avatarService.GetCachedAvatarsAsync();
            foreach (var c in cachedAvatars)
            {
                var row = avatarGrid.Rows.Add(null, new Bitmap(c.ImagePath), c.Name, c.Id);
                avatarGrid.Rows[row].Height = 70;
            }
        }
    }
}
