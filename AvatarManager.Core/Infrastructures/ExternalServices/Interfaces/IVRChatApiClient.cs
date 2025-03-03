using VRChat.API.Model;

namespace AvatarManager.Core.Infrastructures.ExternalServices.Interfaces;

public interface IVRChatApiClient
{
    void Init(string authToken);
    bool Auth();
    Task<bool> AuthAsync();
    CurrentUser GetCurrentUser();
    Task<CurrentUser> GetCurrentUserAsync();
    Task<List<Avatar>> GetAvatarsAsync(string userId);
    Task<HttpResponseMessage> DownloadImageAsync(string url);
    Task SetCurrentAvatarAsync(string id);
}
