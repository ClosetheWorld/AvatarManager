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
    private readonly AuthForm _authForm;
    private readonly LoadingForm _loadingForm;
    private readonly DisplayNameEditForm _displayNameEditForm;
    private readonly SettingForm _settingForm;
    private readonly IAvatarService _avatarService;
    private readonly IFolderService _folderService;
    private int currentFolderIndex = 0;
    private DataTable _folderDataTable = new DataTable();
    private BindingSource _folderBindingSource = new BindingSource();
    private DataTable _avatarDataTable = new DataTable();
    private BindingSource _avatarBindingSource = new BindingSource();
    private List<Tuple<Bitmap, string>> _avatarThumbnails = new List<Tuple<Bitmap, string>>();

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="vrcApiClient"></param>
    /// <param name="dbContext"></param>
    /// <param name="avatarService"></param>
    /// <param name="folderService"></param>
    /// <param name="displayNameEditForm"></param>
    /// <param name="settingForm"></param>
    /// <param name="loadingForm"></param>
    /// <param name="authForm"></param>
    public MainWindow(IVRChatApiClient vrcApiClient, ApplicationDbContext dbContext,
        IAvatarService avatarService, IFolderService folderService,
        DisplayNameEditForm displayNameEditForm, SettingForm settingForm, LoadingForm loadingForm, AuthForm authForm)
    {
        _vrcApi = vrcApiClient;
        _avatarService = avatarService;
        _folderService = folderService;
        _authForm = authForm;
        _loadingForm = loadingForm;
        _displayNameEditForm = displayNameEditForm;
        _settingForm = settingForm;

        Settings.Default.Upgrade();
        if (string.IsNullOrEmpty(Settings.Default.authToken))
        {
            _authForm.StartPosition = FormStartPosition.CenterParent;
            _authForm.ShowDialog();
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
        Text = $"AvatarManager v{Application.ProductVersion}";
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
        _loadingForm.SetCurrentUser(_user);
        _loadingForm.StartPosition = FormStartPosition.CenterParent;
        _loadingForm.ShowDialog();

        // avatarThumbnail����
        await GenerateAvatarThumbnailBitMapListAsync();

        // DataTable������
        InitDataTable();

        // folderGridView �ݒ�
        folderGrid.RowTemplate.Height = 100;
        folderGrid.RowTemplate.ContextMenuStrip = folderRightClickMenu;
        folderGrid.RowTemplate.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12);

        // avatarGridView �ݒ�
        avatarGrid.RowTemplate.Height = 70;
        avatarGrid.RowTemplate.ContextMenuStrip = avatarRightClickMenu;
        avatarGrid.RowTemplate.DefaultCellStyle.Font = new Font("Yu Gothic UI", 12);        

        // folderGridView BindingSource�ݒ�
        _folderBindingSource.DataSource = _folderDataTable;
        folderGridBindingSource.DataSource = _folderBindingSource;

        // folderGridView ����
        await GenerateFolderGridAsync();

        // avatarGridView BindingSource�ݒ�
        _avatarBindingSource.DataSource = _avatarDataTable;
        avatarGridBindingSource.DataSource = _avatarBindingSource;

        avatarGrid.CurrentCell = null;
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
    /// �t�H���_�ǉ��{�^���������ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void settingButton_Click(object sender, EventArgs e)
    {
        _settingForm.SetFolderId(null);
        _settingForm.SetBitmapList(_avatarThumbnails);
        _settingForm.StartPosition = FormStartPosition.CenterParent;
        _settingForm.ShowDialog();

        // update folder grid
        currentFolderIndex = 0;
        _folderDataTable.Clear();
        await GenerateFolderGridAsync();
    }

    /// <summary>
    /// �A�o�^�[�O���b�h���N���b�N���ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void avatarGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        // avatarGrid�̉摜���N���b�N���ꂽ�ꍇ�t�H�[�J�X�Z���𖼑O�̃J�����ɕύX
        if (e.RowIndex > -1 && e.ColumnIndex == 0)
        {
            avatarGrid.CurrentCell = avatarGrid.Rows[e.RowIndex].Cells[1];
        }

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
            _folderDataTable.Clear();
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
        // �E�N���b�N���ꂽ�s��index��Tag����擾
        if (folderRightClickMenu.Tag is int rowIndex && rowIndex >= 0)
        {
            _settingForm.SetFolderId(folderGrid.Rows[rowIndex].Cells[1].Value.ToString());
            _settingForm.SetBitmapList(_avatarThumbnails);
            _settingForm.StartPosition = FormStartPosition.CenterParent;
            _settingForm.ShowDialog();
            currentFolderIndex = 0;
            _folderDataTable.Clear();
            await GenerateFolderGridAsync();
        }
    }

    /// <summary>
    /// folderGrid�ŉE�N���b�N���j���[���J�����Ƃ��̏���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void folderRightClickMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var mousePosition = folderGrid.PointToClient(Cursor.Position);
        var hitTestInfo = folderGrid.HitTest(mousePosition.X, mousePosition.Y);

        // �����ނ��E�N���b�N�����Ƃ��͕ҏW�ƍ폜���\��
        if (hitTestInfo.RowIndex == folderGrid.RowCount - 1)
        {
            e.Cancel = true;
        }

        if (hitTestInfo.RowIndex >= 0)
        {
            // �E�N���b�N���ꂽ�s��index��tag�ɃZ�b�g
            folderRightClickMenu.Tag = hitTestInfo.RowIndex;
        }
        else
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
            _folderDataTable.Clear();
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
        SetFolderDataTable(folders);

        // ����
        SetFolderDataTableLastRow();

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
            avatarGrid.CurrentCell = null;
        }
        else
        {
            var allAvatars = await _avatarService.GetCachedAvatarsAsync();
            SetAvatarDataTable(allAvatars.Where(x => folder.ContainAvatarIds.Contains(x.Id)).ToList());
            avatarGrid.CurrentCell = null;
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
        Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{Application.CompanyName}"), true);
        _authForm.StartPosition = FormStartPosition.CenterParent;
        _authForm.ShowDialog();
        _user = _vrcApi.GetCurrentUser();
    }

    /// <summary>
    /// DataTable������������
    /// </summary>
    private void InitDataTable()
    {
        _folderDataTable.Columns.Add("Id", typeof(string));
        _folderDataTable.Columns.Add("FolderName", typeof(string));

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
    /// DataTable�Ƀt�H���_�����Z�b�g����
    /// </summary>
    /// <param name="folders"></param>
    private void SetFolderDataTable(List<Folder> folders)
    {
        foreach (var f in folders)
        {
            var row = _folderDataTable.NewRow();
            row["Id"] = f.Id;
            row["FolderName"] = f.Name;
            _folderDataTable.Rows.Add(row);
        }
    }

    /// <summary>
    /// DataTable�ɖ����ނ̃t�H���_�����Z�b�g����
    /// </summary>
    private void SetFolderDataTableLastRow()
    {
        var row = _folderDataTable.NewRow();
        row["Id"] = "unLabeled";
        row["FolderName"] = "������";
        _folderDataTable.Rows.Add(row);
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
