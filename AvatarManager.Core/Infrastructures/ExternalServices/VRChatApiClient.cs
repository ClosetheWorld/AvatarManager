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
    private int _exceptionCount = 0;

    public void Init(string authToken)
    {
        _configuration = new Configuration
        {
            BasePath = "https://api.vrchat.cloud/api/1",
            UserAgent = "AvatarManager/1.0.10",
            DefaultHeaders =
            {
                ["Cookie"] = $"apiKey=JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26; auth={authToken}"
            }
        };

        _authenticationApi = new AuthenticationApi(_apiClient, _apiClient, _configuration);
        _httpClient.DefaultRequestHeaders.Add("Cookie", $"auth={authToken}");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "AvatarManager/1.0.10");
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
            throw;
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

    public async Task<List<Avatar>> GetAvatarsAsync(string userId)
    {
        List<Avatar> avatars = new();

        // get 0-100 avatars
        try
        {            
            avatars = _avatarsApi.SearchAvatars(user: "me", n: 100, releaseStatus: ReleaseStatus.All);
        }
        catch (Exception e)
        {
            do
            {
                await HandleExceptionWhileLoadingAvatarAsync(avatars.Count);
            } while (avatars.Count == 0);
        }

        // get 100+ avatars
        if (avatars.Count == 100)
        {
            for (int i = 1; avatars.Count % 100 == 0; i++)
            {
                try
                {
                    avatars.AddRange(await _avatarsApi.SearchAvatarsAsync(user: "me", n: 100, releaseStatus: ReleaseStatus.All, offset: i * 100));
                }
                catch (Exception e)
                {
                    do
                    {
                        avatars.AddRange(await HandleExceptionWhileLoadingAvatarAsync(avatars.Count, i));
                    } while (avatars.Count % 100 == 0);
                }
            }
        }

        _exceptionCount = 0;
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

    private async Task<List<Avatar>> HandleExceptionWhileLoadingAvatarAsync(int currentCount, int offset = 0)
    {
        List<Avatar> avatars = new();
        if (currentCount == 0)
        {
            try
            {                
                await Task.Delay(1000 * _exceptionCount * _exceptionCount);
                avatars = await _avatarsApi.SearchAvatarsAsync(user: "me", n: 100, releaseStatus: ReleaseStatus.All);
                return avatars;
            }
            catch (Exception e)
            {
                _exceptionCount++;
                if (_exceptionCount > 5)
                {
                    throw;
                }
            }
        }
        else
        {
            try
            {
                await Task.Delay(1000 * _exceptionCount * _exceptionCount);
                avatars = await _avatarsApi.SearchAvatarsAsync(user: "me", n: 100, releaseStatus: ReleaseStatus.All, offset: offset * 100);
                return avatars;
            }
            catch (Exception e)
            {
                _exceptionCount++;
                if (_exceptionCount > 5)
                {
                    throw;
                }
            }
        }

        return avatars;
    }
}
