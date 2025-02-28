using AvatarManager.Core.Infrastructures.ExternalServices.Interfaces;
using VRChat.API.Api;
using VRChat.API.Client;
using VRChat.API.Model;

namespace AvatarManager.Core.Infrastructures.ExternalServices;

public class VRChatApiClient : IVRChatApiClient
{
    private Configuration _configuration;
    private ApiClient _apiClient = new();
    private AuthenticationApi _authenticationApi;
    private AvatarsApi _avatarsApi;
    private static HttpClient _httpClient = new();

    public void Init(string authToken)
    {
        _configuration = new Configuration
        {
            BasePath = "https://api.vrchat.cloud/api/1",
            UserAgent = "AvatarManager/1.0.6",
            DefaultHeaders =
            {
                ["Cookie"] = $"apiKey=JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26; auth={authToken}"
            }
        };

        _authenticationApi = new AuthenticationApi(_apiClient, _apiClient, _configuration);
        _httpClient.DefaultRequestHeaders.Add("Cookie", $"auth={authToken}");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "AvatarManager/1.0.6");
    }

    public bool Auth()
    {
        try
        {
            var result = _authenticationApi.VerifyAuthToken();
            InitApiClients();
            return result.Ok;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public async Task<bool> AuthAsync()
    {
        var result = await _authenticationApi.VerifyAuthTokenAsync();
        InitApiClients();
        return result.Ok;
    }

    public CurrentUser GetCurrentUser()
    {
        return _authenticationApi.GetCurrentUser();
    }

    public async Task<CurrentUser> GetCurrentUserAsync()
    {
        var response = await _authenticationApi.GetCurrentUserWithHttpInfoAsync();
        return response.Data;
    }

    public List<Avatar> GetAvatars(string userId)
    {
        var avatars = _avatarsApi.SearchAvatars(user: "me", n: 100, releaseStatus: ReleaseStatus.All);
        if (avatars.Count == 100)
        {
            for (int i = 1; avatars.Count % 100 == 0; i++)
            {
                avatars.AddRange(_avatarsApi.SearchAvatars(user: "me", n: 100, releaseStatus: ReleaseStatus.All, offset: i * 100));
            }
        }
        return avatars;
    }

    public async Task<List<Avatar>> GetAvatarsAsync(string userId)
    {
        var avatars = await _avatarsApi.SearchAvatarsAsync(userId: userId);
        return avatars;
    }

    public async Task<HttpResponseMessage> DownloadImageAsync(string url)
    {
        return await _httpClient.GetAsync(url);
    }

    public async Task SetCurrentAvatarAsync(string id)
    {
        await _avatarsApi.SelectAvatarAsync(id);
    }


    private void InitApiClients()
    {
        _avatarsApi = new AvatarsApi(_apiClient, _apiClient, _configuration);
    }
}
