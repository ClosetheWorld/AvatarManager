using AvatarManager.Core.Models;

namespace AvatarManager.Core.Services.interfaces;

public interface IAvatarService
{
    Task<OwnedAvatar> GetCachedAvatarAsync(string id);
    Task<List<OwnedAvatar>> GetCachedAvatarsAsync();
    Task CacheAvatarAsync(OwnedAvatar avatar);
    Task<List<OwnedAvatar>> GetUnCategorizedAvatarsAsync();
    Task<List<OwnedAvatar>> GetAvatarsByFolderIdAsync(string folderId);
    Task UpdateCachedAvatarAsync(OwnedAvatar avatar, string newImagePath);
    Task DeleteCachedAvatarAsync(string id);
    Task<string?> GetDisplayNameByAvatarIdAsync(string id);
}
