using AvatarManager.Core.Services.interfaces;

namespace AvatarManager.WinForm.Forms
{
    public partial class DisplayNameEditForm : Form
    {
        private readonly IAvatarService _avatarService;
        private string _avatarId;

        public DisplayNameEditForm(string currentAvatarDisplayName, string avatarId, IAvatarService avatarService)
        {
            _avatarService = avatarService;
            _avatarId = avatarId;
            InitializeComponent();
            displayNameTextBox.Text = currentAvatarDisplayName;
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

            Close();
            Dispose();
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
        }
        #endregion
    }
}
