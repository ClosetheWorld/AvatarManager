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

        EnsureCreateDirectorys();
        ServiceCollection services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider serviceProvider = services.BuildServiceProvider();
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

    private static void EnsureCreateDirectorys()
    {
        if (!Directory.Exists("Data"))
        {
            Directory.CreateDirectory("Data");
        }

        if (!Directory.Exists("Data/CachedImages"))
        {
            Directory.CreateDirectory("Data/CachedImages");
        }
    }

    private static void EnsureCreateDatabase(ApplicationDbContext db)
    {
        db.Database.ExecuteSqlRaw("INSERT INTO \"__EFMigrationsHistory\"\r\n(MigrationId, ProductVersion)\r\nVALUES('20241224080145_init', '8.0.11');");

        var mig = db.Database as IInfrastructure<IServiceProvider>;
        var migrator = mig.Instance.GetService<IMigrator>();
        migrator.Migrate();
    }
}