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
    private static DisplayNameEditForm _displayNameEditForm;
    private readonly IAvatarService _avatarService;
    private readonly IImageService _imageService;
    private readonly IFolderService _folderService;
    private int currentFolderIndex = 0;
    private DataTable _avatarDataTable = new DataTable();
    private BindingSource _avatarBindingSource = new BindingSource();
    private List<Tuple<Bitmap, string>> _avatarThumbnails = new List<Tuple<Bitmap, string>>();

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="vrcApiClient"></param>
    /// <param name="dbContext"></param>
    /// <param name="avatarService"></param>
    /// <param name="imageService"></param>
    /// <param name="folderService"></param>
    public MainWindow(IVRChatApiClient vrcApiClient, ApplicationDbContext dbContext,
        IAvatarService avatarService, IImageService imageService, IFolderService folderService,
        DisplayNameEditForm displayNameEditForm)
    {
        _vrcApi = vrcApiClient;
        _avatarService = avatarService;
        _imageService = imageService;
        _folderService = folderService;
        _displayNameEditForm = displayNameEditForm;

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

        // avatarThumbnail����
        await GenerateAvatarThumbnailBitMapListAsync();

        // DataTable������
        InitDataTable();

        // avatarGridView �ݒ�
        avatarGrid.RowTemplate.Height = 70;
        avatarGrid.RowTemplate.ContextMenuStrip = avatarRightClickMenu;
        avatarGrid.RowTemplate.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12);

        // folderGridView ����
        await GenerateFolderGridAsync();

        // avatarGridView BindingSource�ݒ�
        _avatarBindingSource.DataSource = _avatarDataTable;
        avatarGridBindingSource.DataSource = _avatarBindingSource;
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
    /// avatarGrid�ŉE�N���b�N���j���[���J�����Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void avatarRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var mousePosition = avatarGrid.PointToClient(Cursor.Position);
        var hitTestInfo = avatarGrid.HitTest(mousePosition.X, mousePosition.Y);

        if (hitTestInfo.RowIndex >= 0)
        {
            // �E�N���b�N���ꂽ�s��index��tag�ɃZ�b�g
            avatarRightClickMenu.Tag = hitTestInfo.RowIndex;
        }
        else
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
        // �E�N���b�N���ꂽ�s��index��Tag����擾
        if (avatarRightClickMenu.Tag is int rowIndex && rowIndex >= 0)
        {
            var clickedAvatarId = avatarGrid.Rows[rowIndex].Cells[2].Value.ToString();
            var currentAvatarDisplayName = await _avatarService.GetDisplayNameByAvatarIdAsync(clickedAvatarId);
            _displayNameEditForm.SetParameters(currentAvatarDisplayName ?? null, clickedAvatarId);
            _displayNameEditForm.StartPosition = FormStartPosition.CenterParent;
            _displayNameEditForm.ShowDialog();
            currentFolderIndex = 0;
            folderGrid.Rows.Clear();
            await GenerateFolderGridAsync();
        }
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
        _avatarDataTable.Clear();
        var folderId = folderGrid.Rows[currentFolderIndex].Cells[1].Value.ToString();
        var folder = await _folderService.GetFolderAsync(folderId);

        if (folder == null)
        {
            var avatars = await _avatarService.GetUnCategorizedAvatarsAsync();
            SetAvatarDataTable(avatars);
        }
        else
        {
            var allAvatars = await _avatarService.GetCachedAvatarsAsync();
            SetAvatarDataTable(allAvatars.Where(x => folder.ContainAvatarIds.Contains(x.Id)).ToList());
        }
    }

    /// <summary>
    /// �����ނ̃A�o�^�[�O���b�h�𐶐�����
    /// </summary>
    /// <returns></returns>
    private async Task GenerateUnCategorizedAvatarGridAsync()
    {
        var cachedAvatars = await _avatarService.GetUnCategorizedAvatarsAsync();
        SetAvatarDataTable(cachedAvatars);
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

    /// <summary>
    /// DataTable������������
    /// </summary>
    private void InitDataTable()
    {
        _avatarDataTable.Columns.Add("AvatarThumbnail", typeof(Bitmap));
        _avatarDataTable.Columns.Add("AvatarName", typeof(string));
        _avatarDataTable.Columns.Add("AvatarId", typeof(string));
    }

    /// <summary>
    /// DataTable�ɃA�o�^�[�����Z�b�g����
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
    /// �A�o�^�[�T���l�C���̃r�b�g�}�b�v���X�g�𐶐�����
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
