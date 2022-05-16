using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.ModeratorService
{
    public class ModeratorService : IModeratorService
    {

            private readonly HttpClient _http;
            private readonly string apiConnection;


            public ModeratorService(HttpClient http)
            {
                _http = http;
#if (DEBUG)
                apiConnection = GlobalConstants.Api.DebugApiAddress;
#else
            apiConnection = GlobalConstants.Api.ApiAddress;
#endif
            }


            public async Task<ServiceResponse<bool>> SaveCompetencyService(List<Service> data)
        {
            var result = await _http.PutAsJsonAsync($"{apiConnection}/Moderator/CompService", data);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}
