using AvatarManager.Core.Models;

namespace AvatarManager.Core.Services.interfaces;

public interface IAvatarService
{
    Task<List<OwnedAvatar>> GetCachedAvatarsAsync();
    Task CacheAvatarAsync(OwnedAvatar avatar);
    Task<List<OwnedAvatar>> GetUnCategorizedAvatarsAsync();
    Task<List<OwnedAvatar>> GetAvatarsByFolderIdAsync(string folderId);
}
