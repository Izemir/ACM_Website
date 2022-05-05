using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.ExecutorService
{
    public class ExecutorService : IExecutorService
    {

        private readonly HttpClient _http;
        private readonly string apiConnection;


        public ExecutorService(HttpClient http)
        {
            _http = http;
#if(DEBUG)
            apiConnection = GlobalConstants.Api.DebugApiAddress;
#else
            apiConnection = GlobalConstants.Api.ApiAddress;
#endif
        }
        public async Task<ServiceResponse<WebExecutor>> AddExecutor(WebExecutor executor, long userId)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Executor/AddExecutor/{userId}", executor);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebExecutor>>();
        }

        public async Task<ServiceResponse<bool>> DeleteExecutor(long userId)
        {
            var result = await _http.DeleteAsync($"{apiConnection}/Executor/DeleteExecutor/{userId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<long>> ExistExecutor(long userId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Executor/ExistExecutor/{userId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<long>>();
        }

        public async Task<ServiceResponse<List<Competency>>> GetCompetencyList()
        {
            var result = await _http.GetAsync($"{apiConnection}/Executor/GetCompetencyList");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Competency>>>();
        }

        public async Task<ServiceResponse<WebExecutor>> GetExecutor(long executorId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Executor/GetExecutor/{executorId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebExecutor>>();
        }

        public async Task<ServiceResponse<List<Speciality>>> GetSpecialityList()
        {
            var result = await _http.GetAsync($"{apiConnection}/Executor/GetSpecialityList");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Speciality>>>();
        }
    }
}
