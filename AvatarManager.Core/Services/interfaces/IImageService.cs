namespace AvatarManager.Core.Services.interfaces;

public interface IImageService
{
    Task<string> DownloadAndCacheImageAsync(string id, string url);
}
