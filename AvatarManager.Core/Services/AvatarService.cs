using AvatarManager.Core.Infrastructures.Database;
using AvatarManager.Core.Models;
using AvatarManager.Core.Services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvatarManager.Core.Services;

public class AvatarService : IAvatarService
{
    private readonly ApplicationDbContext _dbContext;
    public AvatarService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<OwnedAvatar>> GetCachedAvatarsAsync()
    {
        return await _dbContext.OwnedAvatars.ToListAsync();
    }

    public async Task CacheAvatarAsync(OwnedAvatar avatar)
    {
        await _dbContext.OwnedAvatars.AddAsync(avatar);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<OwnedAvatar>> GetUnCategorizedAvatarsAsync()
    {
        var all = await _dbContext.OwnedAvatars.ToListAsync();
        var folders = await _dbContext.Folders.ToListAsync();
        foreach (var f in folders)
        {
            foreach (var id in f.ContainAvatarIds)
            {
                var avatar = all.FirstOrDefault(x => x.Id == id);
                if (avatar != null)
                {
                    all.Remove(avatar);
                }
            }
        }

        return all;
    }

    public async Task<List<OwnedAvatar>> GetAvatarsByFolderIdAsync(string folderId)
    {
        var folder = await _dbContext.Folders.FirstOrDefaultAsync(x => x.Id == folderId);
        if (folder != null)
        {
            var response = new List<OwnedAvatar>();
            foreach(var a in folder.ContainAvatarIds)
            {
                response.Add(_dbContext.OwnedAvatars.FirstOrDefault(x => x.Id == a));
            }
        }
        return null;
    }

    public async Task UpdateCachedAvatarAsync(OwnedAvatar avatar,  string newImagePath)
    {
        var current = await _dbContext.OwnedAvatars.FirstOrDefaultAsync(x => x.Id == avatar.Id);
        if (current != null)
        {
            current.Name = avatar.Name;
            current.ThumbnailImageUrl = avatar.ThumbnailImageUrl;
            current.ImagePath = newImagePath;
            _dbContext.OwnedAvatars.Update(current);
            await _dbContext.SaveChangesAsync();
        }
    }
}
