﻿using AvatarManager.Core.Models;

namespace AvatarManager.Core.Services.interfaces;

public interface IFolderService
{
    Task<List<Folder>> GetFoldersAsync();
    Task<Folder> GetFolderAsync(string id);
    Task AddFolderAsync(Folder f);
    Task DeleteFolderAsync(string id);
    Task UpdateFolderAsync(Folder f);
    Task AddContainAvatarIdToExistsFolderAsync(string folderId, List<string> avatarIds);
}
