using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.SearchService
{
    public class SearchService : ISearchService
    {

        private readonly HttpClient _http;
        private readonly string apiConnection;


        public SearchService(HttpClient http)
        {
            _http = http;
#if(DEBUG)
            apiConnection = GlobalConstants.Api.DebugApiAddress;
#else
            apiConnection = GlobalConstants.Api.ApiAddress;
#endif
        }

        public async Task<ServiceResponse<List<Construction>>> GetConstructionsForExecutor(long executorId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Search/GetConstructionsForEx/{executorId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Construction>>>();
        }

        public async Task<ServiceResponse<List<WebExecutor>>> GetExecutorsForConstruction(long constructionId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Search/GetExesForConstructions/{constructionId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<WebExecutor>>>();
        }
    }
}
