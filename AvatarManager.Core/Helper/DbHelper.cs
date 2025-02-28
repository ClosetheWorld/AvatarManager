﻿namespace AvatarManager.Core.Helper;

public static class DbHelper
{
    public static string GetConnectionString()
    {
        return "Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), "Data\\AvatarManager.db");
    }

    public static string GetDatabasePath()
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "Data\\AvatarManager.db");
    }
}
