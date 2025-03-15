using AvatarManager.Core.Infrastructures.ExternalServices.Interfaces;
using AvatarManager.Core.Services.interfaces;

namespace AvatarManager.Core.Services;

public class ImageService : IImageService
{
    private readonly IVRChatApiClient _vrcApi;

    public ImageService(IVRChatApiClient vrcApi)
    {
        _vrcApi = vrcApi;
    }

    public async Task<string> DownloadAndCacheImageAsync(string id, string url)
    {
        var response = await _vrcApi.DownloadImageAsync(url);
        var path = $"Data\\CachedImages\\{id}_{DateTime.Now.ToString("yyyyMMddhhmmss")}.png";
        if (response.IsSuccessStatusCode)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                await response.Content.CopyToAsync(fs);
            }
        }
        return path;
    }
}
