using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly HttpClient _http;
        private readonly string apiConnection;


        public FileService(HttpClient http)
        {
            _http = http;
#if (DEBUG)
            apiConnection = GlobalConstants.Api.DebugApiAddress;
#else
            apiConnection = GlobalConstants.Api.ApiAddress;
#endif
        }

        public async Task<ServiceResponse<List<UserFile>>> AddFileToExecutor(long executorId, UserFile file)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/File/AddFileToExecutor/{executorId}", file);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<UserFile>>>();
        }

        public async Task<ServiceResponse<List<UserFile>>> DeleteFileOfExecutor(long executorId, long fileId)
        {
            var result = await _http.DeleteAsync($"{apiConnection}/File/DeleteDeleteFileOfExecutor/{executorId}/{fileId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<UserFile>>>();
        }

        public async Task<ServiceResponse<List<UserFile>>> GetExecutorFiles(long executorId)
        {
            var result = await _http.GetAsync($"{apiConnection}/File/GetExecutorFiles/{executorId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<UserFile>>>();
        }

        public async Task<ServiceResponse<UserFile>> GetFile(long fileId)
        {
            var result = await _http.GetAsync($"{apiConnection}/File/GetFile/{fileId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<UserFile>>();
        }
    }
}
