using AvatarManager.Core.Helper;
using AvatarManager.Core.Infrastructures.Database;
using AvatarManager.Core.Infrastructures.ExternalServices.Interfaces;
using AvatarManager.Core.Services.interfaces;
using AvatarManager.WinForm.Properties;
using VRChat.API.Model;

namespace AvatarManager.WinForm.Forms;

public partial class MainWindow : Form
{
    private IVRChatApiClient _vrcApi;
    private CurrentUser _user;
    private readonly IAvatarService _avatarService;
    private readonly IImageService _imageService;
    private readonly IFolderService _folderService;
    private int currentFolderIndex = 0;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="vrcApiClient"></param>
    /// <param name="dbContext"></param>
    /// <param name="avatarService"></param>
    /// <param name="imageService"></param>
    /// <param name="folderService"></param>
    public MainWindow(IVRChatApiClient vrcApiClient, ApplicationDbContext dbContext,
        IAvatarService avatarService, IImageService imageService, IFolderService folderService)
    {
        _vrcApi = vrcApiClient;
        _avatarService = avatarService;
        _imageService = imageService;
        _folderService = folderService;

        Settings.Default.Upgrade();
        if (string.IsNullOrEmpty(Settings.Default.authToken))
        {
            var auth = new AuthForm(_vrcApi);
            auth.StartPosition = FormStartPosition.CenterParent;
            auth.ShowDialog();
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
                HandleAuthException("�F�؂Ɏ��s���܂����B�ēx���O�C�����Ă��������B");
            }
            _user = _vrcApi.GetCurrentUser();
        }
        InitializeComponent();
        userName.Text = _user.DisplayName;
    }

    #region EventHandlers
    /// <summary>
    /// �t�H�[�����\�����ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void MainWindow_Shown(object sender, EventArgs e)
    {
        // avatar �L���b�V������
        var load = new LoadingForm(_user, _vrcApi, _avatarService, _imageService);
        load.StartPosition = FormStartPosition.CenterParent;
        load.ShowDialog();

        // folderGridView ����
        await GenerateFolderGridAsync();
    }

    /// <summary>
    /// �t�H���_���N���b�N���ꂽ�Ƃ��̏���
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
    /// �ݒ�{�^���������ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void settingButton_Click(object sender, EventArgs e)
    {
        var form = new SettingForm(_avatarService, _folderService, null);
        form.StartPosition = FormStartPosition.CenterParent;
        form.ShowDialog();

        // update folder grid
        currentFolderIndex = 0;
        folderGrid.Rows.Clear();
        await GenerateFolderGridAsync();
    }

    /// <summary>
    /// �A�o�^�[�O���b�h���N���b�N���ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void avatarGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        // avatarGrid�̃w�b�_�[���N���b�N���ꂽ�ꍇ�͉������Ȃ�
        if (e.RowIndex >= 0)
        {
            try
            {
                await _vrcApi.SetCurrentAvatarAsync(avatarGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
            catch (Exception ex)
            {
                HandleAuthException("�A�o�^�[�̐؂�ւ��Ɏ��s���܂����B�F�؏�񂪐؂�Ă���\��������܂��B");
            }
        }
    }

    /// <summary>
    /// folderGrid�ŉE�N���b�N���j���[�̍폜���N���b�N���ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void deleteMenuItem_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show($"�ȉ��̃t�H���_���폜���܂�\n{folderGrid.Rows[currentFolderIndex].Cells[0].Value.ToString()}", "�m�F", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
        {
            await _folderService.DeleteFolderAsync(folderGrid.Rows[currentFolderIndex].Cells[1].Value.ToString());
            currentFolderIndex = 0;
            folderGrid.Rows.Clear();
            await GenerateFolderGridAsync();
            MessageBox.Show("�폜���܂���");
        }
    }

    /// <summary>
    /// folderGrid�ŉE�N���b�N���j���[�̕ҏW���N���b�N���ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void editMenuItem_Click(object sender, EventArgs e)
    {
        var form = new SettingForm(_avatarService, _folderService, folderGrid.Rows[currentFolderIndex].Cells[1].Value.ToString());
        form.StartPosition = FormStartPosition.CenterParent;
        form.ShowDialog();
        currentFolderIndex = 0;
        folderGrid.Rows.Clear();
        await GenerateFolderGridAsync();
    }

    /// <summary>
    /// folderGrid�ŉE�N���b�N���j���[���J�����Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void folderRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        // �����ނ��E�N���b�N�����Ƃ��͕ҏW�ƍ폜���\��
        if (currentFolderIndex == folderGrid.RowCount - 1)
        {
            e.Cancel = true;
        }
    }

    /// <summary>
    /// avatarGrid�ŉE�N���b�N���j���[�̕\������ҏW���N���b�N���ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void editAvatarDisplayNameMenuItem_Click(object sender, EventArgs e)
    {
        var source = avatarRightClickMenu.SourceControl as DataGridView;
        var clickedRow = source.HitTest(source.PointToClient(Cursor.Position).X, source.PointToClient(Cursor.Position).Y).RowIndex;
        var clickedAvatarId = avatarGrid.Rows[clickedRow].Cells[2].Value.ToString();
        var currentAvatarDisplayName =
            await _avatarService.GetDisplayNameByAvatarIdAsync(clickedAvatarId);
        var form = new DisplayNameEditForm(currentAvatarDisplayName ?? null, clickedAvatarId, _avatarService);
        form.StartPosition = FormStartPosition.CenterParent;
        form.ShowDialog();
        currentFolderIndex = 0;
        folderGrid.Rows.Clear();
        await GenerateFolderGridAsync();
    }
    #endregion

    #region Methods
    /// <summary>
    /// �t�H���_�O���b�h�𐶐�����
    /// </summary>
    /// <returns></returns>
    private async Task GenerateFolderGridAsync()
    {
        // db�ɂ���t�H���_��folderGrid�𐶐�
        var folders = await _folderService.GetFoldersAsync();

        foreach (var f in folders)
        {
            var i = folderGrid.Rows.Add(f.Name, f.Id);
            folderGrid.Rows[i].Height = 100;
            folderGrid.Rows[i].Cells[0].Style.Font = new Font("Yu Gothic UI", 12);
            folderGrid.Rows[i].ContextMenuStrip = folderRightClickMenu;
        }

        // ����
        var idx = folderGrid.Rows.Add("������", "unLabeled");
        folderGrid.Rows[idx].Height = 100;
        folderGrid.Rows[idx].Cells[0].Style.Font = new Font("Yu Gothic UI", 12);

        // �擪�Ƀt�H�[�J�X
        folderGrid.CurrentCell = folderGrid.Rows[0].Cells[0];

        // �t�H�[�J�X�����悪�����ނ̏ꍇ�S�A�o�^�[�\��
        if ((string)folderGrid.CurrentCell.Value == "������")
        {
            await GenerateUnCategorizedAvatarGridAsync();
        }
        else
        {
            await GenerateFolderAvatarGridAsync();
        }
    }

    /// <summary>
    /// �t�H���_���̃A�o�^�[�O���b�h�𐶐�����
    /// </summary>
    /// <returns></returns>
    private async Task GenerateFolderAvatarGridAsync()
    {
        avatarGrid.Rows.Clear();
        var folderId = folderGrid.Rows[currentFolderIndex].Cells[1].Value.ToString();
        var folder = await _folderService.GetFolderAsync(folderId);

        if (folder == null)
        {
            var avatars = await _avatarService.GetUnCategorizedAvatarsAsync();
            foreach (var a in avatars)
            {
                var i = avatarGrid.Rows.Add(new Bitmap(a.ImagePath), a.DisplayName != null ? $"{a.DisplayName} ({a.Name})" : a.Name, a.Id);
                avatarGrid.Rows[i].Height = 70;
                avatarGrid.Rows[i].Cells[1].Style.Font = new Font("Yu Gothic UI", 12);
                avatarGrid.Rows[i].ContextMenuStrip = avatarRightClickMenu;
            }
        }
        else
        {
            var allAvatars = await _avatarService.GetCachedAvatarsAsync();
            foreach (var a in folder.ContainAvatarIds)
            {
                var i = avatarGrid.Rows.Add(new Bitmap(allAvatars.Single(x => x.Id == a).ImagePath), allAvatars.Single(x => x.Id == a).DisplayName != null ? $"{allAvatars.Single(x => x.Id == a).DisplayName} ({allAvatars.Single(x => x.Id == a).Name})" : allAvatars.Single(x => x.Id == a).Name, a);
                avatarGrid.Rows[i].Height = 70;
                avatarGrid.Rows[i].Cells[1].Style.Font = new Font("Yu Gothic UI", 12);
                avatarGrid.Rows[i].ContextMenuStrip = avatarRightClickMenu;
            }
        }
    }

    /// <summary>
    /// �����ނ̃A�o�^�[�O���b�h�𐶐�����
    /// </summary>
    /// <returns></returns>
    private async Task GenerateUnCategorizedAvatarGridAsync()
    {
        var cachedAvatars = await _avatarService.GetCachedAvatarsAsync();

        // create grid
        cachedAvatars.Clear();
        cachedAvatars = await _avatarService.GetUnCategorizedAvatarsAsync();
        foreach (var c in cachedAvatars)
        {
            var i = avatarGrid.Rows.Add(new Bitmap(c.ImagePath), c.DisplayName != null ? $"{c.DisplayName} ({c.Name})" : c.Name, c.Id);
            avatarGrid.Rows[i].Height = 70;
            avatarGrid.Rows[i].Cells[1].Style.Font = new Font("Yu Gothic UI", 12);
        }
    }

    /// <summary>
    /// �F�؃G���[���̏���
    /// </summary>
    private void HandleAuthException(string msg)
    {
        Settings.Default.Reset();
        MessageBox.Show(msg, "�F�؃G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
        var auth = new AuthForm(_vrcApi);
        auth.StartPosition = FormStartPosition.CenterParent;
        auth.ShowDialog();
        _user = _vrcApi.GetCurrentUser();
    }
    #endregion
}
