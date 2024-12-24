using AvatarManager.Core.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AvatarManager.Core.Infrastructures.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions opt) : base(opt) { }

    public DbSet<Folder> Folders { get; set; }
    public DbSet<OwnedAvatar> OwnedAvatars { get; set; }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connection = new SqliteConnection("Data Source=AvatarManager.db");
        connection.Open();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;
        return new ApplicationDbContext(options);
    }
}