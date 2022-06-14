using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.ChatService
{
    public class ChatService: IChatService
    {
        private readonly HttpClient _http;
        private readonly string apiConnection;
        public ChatService(HttpClient http)
        {
            _http = http;
#if (DEBUG)
            apiConnection = GlobalConstants.Api.DebugApiAddress;
#else
            apiConnection = GlobalConstants.Api.ApiAddress;
#endif
        }

        public async Task<ServiceResponse<long>> ExistChat(long customerId, long executorId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Chat/ExistChat/{customerId}/{executorId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<long>>();
        }

        public async Task<ServiceResponse<WebChat>> GetChat(long chatId, long requestId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Chat/GetChat/{chatId}/{requestId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebChat>>();
        }

        public async Task<ServiceResponse<List<WebChat>>> GetCustomerChats(long customerId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Chat/GetCustomerChats/{customerId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<WebChat>>>();
        }

        public async Task<ServiceResponse<List<WebChat>>> GetExecutorChats(long executorId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Chat/GetExecutorChats/{executorId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<WebChat>>>();
        }

        public async Task<ServiceResponse<Sender>> GetSenderInfo(long userId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Chat/GetSender/{userId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Sender>>();
        }

        public async Task<ServiceResponse<WebChat>> SendMessage(long chatId, Message message)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Chat/SendMessage/{chatId}", message);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebChat>>();
        }

        public async Task<ServiceResponse<WebChat>> StartNewChat(long customerId, long executorId)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Chat/StartChat/{customerId}/{executorId}", 0);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebChat>>();
        }
    }
}
