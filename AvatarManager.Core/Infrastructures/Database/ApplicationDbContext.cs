using AvatarManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AvatarManager.Core.Infrastructures.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions opt) : base(opt) { }

    public DbSet<Folder> Folders { get; set; }
    public DbSet<OwnedAvatar> OwnedAvatars { get; set; }
}
