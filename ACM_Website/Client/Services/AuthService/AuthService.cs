using ACM_Website.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly string apiConnection;
        public AuthService(HttpClient http)
        {
            _http = http;
#if (DEBUG)
            apiConnection = GlobalConstants.Api.DebugApiAddress;
#else
            apiConnection = GlobalConstants.Api.ApiAddress;
#endif
        }

        public async Task<ServiceResponse<bool>> IsAdmin(long userId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Auth/admin/{userId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<WebUser>> Login(UserLogin user)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Auth/login/", user);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebUser>>();
        }

        public async Task<ServiceResponse<long>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<long>>();
        }
    }
}
