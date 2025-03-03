using AvatarManager.Core.Infrastructures.ExternalServices.Interfaces;
using AvatarManager.Core.Models;
using AvatarManager.Core.Services.interfaces;
using System.Diagnostics;
using VRChat.API.Model;

namespace AvatarManager.WinForm.Forms
{
    public partial class LoadingForm : Form
    {
        private CurrentUser _user;
        private IVRChatApiClient _vrcApi;
        private IAvatarService _avatarService;
        private IImageService _imageService;

        public LoadingForm(CurrentUser user, IVRChatApiClient vrchatApiClient, IAvatarService avatarService, IImageService imageService)
        {
            _user = user;
            _vrcApi = vrchatApiClient;
            _avatarService = avatarService;
            _imageService = imageService;
            InitializeComponent();
        }

        #region EventHandlers
        /// <summary>
        /// フォームが表示されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoadingForm_Shown(object sender, EventArgs e)
        {
            await StartLoading();
        }

        /// <summary>
        /// フォームが閉じられるときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // this.Close()でも発火するけどなんかいい感じの動きするからこれでいいや
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// ロード処理を開始する
        /// </summary>
        /// <returns></returns>
        public async Task StartLoading()
        {
            processingAvatarName.Text = "Loading...";
            List<Avatar> avatars = new ();
            try
            {
                avatars = await _vrcApi.GetAvatarsAsync(_user.Id);
            }
            catch (Exception e)
            {
                MessageBox.Show($"アバター情報のロード中にエラーが発生しました。\nよろしければ開発者にこの画面のスクリーンショットをお送りください。\n" +
                    $"===\n" +
                    $"{e.Message}\n" +
                    $"===\n" +
                    $"{e.StackTrace}\n" +
                    $"===\n" +
                    $"{e.InnerException}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();
            }

            // distinct avatars from api
            var cachedAvatars = await _avatarService.GetCachedAvatarsAsync();
            foreach (var c in cachedAvatars)
            {
                processingAvatarName.Text = c.Name;
                var a = avatars.FirstOrDefault(x => x.Id == c.Id);
                if (a == null)
                {
                    await _avatarService.DeleteCachedAvatarAsync(c.Id);
                    continue;
                }
                // update cached avatar
                if (a.Name != c.Name || a.ThumbnailImageUrl != c.ThumbnailImageUrl || !System.IO.File.Exists(c.ImagePath))
                {
                    await _avatarService.UpdateCachedAvatarAsync(
                        new OwnedAvatar
                        {
                            Id = a.Id,
                            Name = a.Name,
                            ThumbnailImageUrl = a.ThumbnailImageUrl
                        },
                        await _imageService.DownloadAndCacheImageAsync(a.Id, a.ThumbnailImageUrl));
                }

                avatars.Remove(a);
            }

            // cache new avatars
            foreach (var avatar in avatars)
            {
                processingAvatarName.Text = avatar.Name;
                var imagePath = await _imageService.DownloadAndCacheImageAsync(avatar.Id, avatar.ThumbnailImageUrl);

                await _avatarService.CacheAvatarAsync(new OwnedAvatar
                {
                    Id = avatar.Id,
                    Name = avatar.Name,
                    ThumbnailImageUrl = avatar.ThumbnailImageUrl,
                    ImagePath = imagePath
                });
            }

            this.Close();
            this.Dispose();
        }
        #endregion
    }
}
