namespace AvatarManager.WinForm.Forms
{
    public partial class DisplayNameEditForm : Form
    {
        public DisplayNameEditForm(string currentAvatarDisplayName)
        {
            InitializeComponent();
            displayNameTextBox.Text = currentAvatarDisplayName;
        }
    }
}
