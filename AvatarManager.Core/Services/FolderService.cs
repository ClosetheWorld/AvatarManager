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
}
