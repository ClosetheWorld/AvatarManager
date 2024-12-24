using System.Text;

namespace AvatarManager.Core.Helper;

/// <summary>
/// Helper class for token operations
/// </summary>
public static class TokenHelper
{
    private static Encoding _encoding = Encoding.UTF8;

    /// <summary>
    /// decode token from application settings
    /// </summary>
    /// <returns></returns>
    public static string DecodeToken(string encodedToken)
    {
        return _encoding.GetString(Convert.FromBase64String(encodedToken));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static string EncodeToken(string token)
    {
        return Convert.ToBase64String(_encoding.GetBytes(token));
    }
}
