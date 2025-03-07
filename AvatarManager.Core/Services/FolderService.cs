using AvatarManager.Core.Infrastructures.Database;
using AvatarManager.Core.Models;
using AvatarManager.Core.Services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace AvatarManager.Core.Services;

public class FolderService : IFolderService
{
    private readonly ApplicationDbContext _dbContext;

    public FolderService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Folder>> GetFoldersAsync()
    {
        return await _dbContext.Folders.ToListAsync();
    }

    public async Task<Folder> GetFolderAsync(string id)
    {
        return await _dbContext.Folders.FindAsync(id);
    }

    public async Task AddFolderAsync(Folder f)
    {
        _dbContext.Folders.Add(f);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteFolderAsync(string id)
    {
        var folder = await _dbContext.Folders.FindAsync(id);
        if (folder != null)
        {
            _dbContext.Folders.Remove(folder);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateFolderAsync(Folder f)
    {
        var current = await _dbContext.Folders.FindAsync(f.Id);
        if (current != null)
        {
            current.Name = f.Name;
            current.ContainAvatarIds = f.ContainAvatarIds;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task AddContainAvatarIdToExistsFolderAsync(string folderId, List<string> avatarIds)
    {
        var current = await _dbContext.Folders.FindAsync(folderId);
        if(current != null)
        {
            current.ContainAvatarIds.AddRange(avatarIds);
            await _dbContext.SaveChangesAsync();
        }
    }
}
