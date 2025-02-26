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
        #endregion
    }
}
