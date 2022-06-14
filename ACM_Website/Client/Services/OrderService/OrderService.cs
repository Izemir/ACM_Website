using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {

        private readonly HttpClient _http;
        private readonly string apiConnection;


        public OrderService(HttpClient http)
        {
            _http = http;
#if (DEBUG)
            apiConnection = GlobalConstants.Api.DebugApiAddress;
#else
            apiConnection = GlobalConstants.Api.ApiAddress;
#endif
        }

        public async Task<ServiceResponse<Order>> ChangeOrderStatus(long orderId)
        {
            var result = await _http.PutAsJsonAsync($"{apiConnection}/Order/ChangeOrderStatus/{orderId}",0);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Order>>();
        }

        public async Task<ServiceResponse<List<Order>>> GetCustomerOrders(long customerId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Order/GetCustomerOrders/{customerId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Order>>>();
        }

        public async Task<ServiceResponse<List<Order>>> GetExecutorOrders(long executorId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Order/GetExecutorOrders/{executorId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Order>>>();
        }

        public async Task<ServiceResponse<Order>> GetOrder(long orderId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Order/GetOrder/{orderId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Order>>();
        }

        public async Task<ServiceResponse<Order>> NewOrder(long constructionId, long executorId)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Order/NewOrder/{constructionId}/{executorId}",0);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Order>>();
        }
    }
}
