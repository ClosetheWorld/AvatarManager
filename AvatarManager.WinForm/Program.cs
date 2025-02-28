using AvatarManager.Core.Helper;
using AvatarManager.Core.Infrastructures.Database;
using AvatarManager.Core.Infrastructures.ExternalServices;
using AvatarManager.Core.Infrastructures.ExternalServices.Interfaces;
using AvatarManager.Core.Services;
using AvatarManager.Core.Services.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace AvatarManager.Winform;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        EnsureCreateDirectories();
        ServiceCollection services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        BackupDatabase();
        EnsureCreateDatabase(serviceProvider.GetRequiredService<ApplicationDbContext>());

        MainWindow main = (MainWindow)serviceProvider.GetRequiredService<MainWindow>();
        Application.Run(main);
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddScoped<IVRChatApiClient, VRChatApiClient>();
        services.AddScoped<IAvatarService, AvatarService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IFolderService, FolderService>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(DbHelper.GetConnectionString());
        });
    }

    private static void EnsureCreateDirectories()
    {
        if (!Directory.Exists("Data"))
        {
            Directory.CreateDirectory("Data");
        }

        if (!Directory.Exists("Data/CachedImages"))
        {
            Directory.CreateDirectory("Data/CachedImages");
        }

        if(!Directory.Exists("Data/Backups"))
        {
            Directory.CreateDirectory("Data/Backups");
        }
    }

    private static void EnsureCreateDatabase(ApplicationDbContext db)
    {
        var migratorSource = db.Database as IInfrastructure<IServiceProvider>;
        var migrator = migratorSource.Instance.GetService<IMigrator>();

        // 初回起動時はMigratorによるマイグレーションを実行
        if (!db.Database.CanConnect())
        {
            migrator.Migrate();
        }
        else
        {
            // 過去バージョンからのアップデート時は__EFMigrationsHistoryテーブルの存在確認
            var tableExists = db.Database.SqlQuery<int>($"SELECT * FROM sqlite_master WHERE type='table' AND name='__EFMigrationsHistory'").Count();
            if (tableExists == 0)
            {
                // テーブルが存在しない場合は作成
                db.Database.ExecuteSqlRaw($"" +
                    $"CREATE TABLE \"__EFMigrationsHistory\" (" +
                        $"\"MigrationId\" TEXT NOT NULL CONSTRAINT \"PK___EFMigrationsHistory\" PRIMARY KEY," +
                        $"\"ProductVersion\" TEXT NOT NULL)");
            }

            // マイグレーション履歴が存在しない場合は初期バージョンの履歴データを挿入
            var migrationHistoryCount = db.Database.SqlQuery<int>($"SELECT * FROM __EFMigrationsHistory").Count();
            if (migrationHistoryCount == 0)
            {
                db.Database.ExecuteSqlRaw("INSERT INTO \"__EFMigrationsHistory\" (MigrationId, ProductVersion) VALUES ('20241224080145_init', '8.0.11');");
            }

            // マイグレーションを実行
            migrator.Migrate();
        }
    }

    private static void BackupDatabase()
    {
        if (File.Exists(DbHelper.GetDatabasePath()))
        {
            var backupPath = $"Data/Backups/AvatarManagerBackup-{DateTime.Now:yyyyMMddHHmmss}.db";
            var origpath = DbHelper.GetDatabasePath();
            File.Copy(DbHelper.GetDatabasePath(), backupPath);
        }
    }
}