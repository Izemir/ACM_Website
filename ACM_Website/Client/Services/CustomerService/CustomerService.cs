﻿using ACM_Website.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {

        private readonly HttpClient _http;
        private readonly string apiConnection;
        

        public CustomerService(HttpClient http)
        {
            _http = http;
#if(DEBUG)
            apiConnection = GlobalConstants.Api.DebugApiAddress;
#else
            apiConnection = GlobalConstants.Api.ApiAddress;
#endif
        }
        public async Task<ServiceResponse<WebCustomer>> AddCustomer(WebCustomer customer, long userId)
        {
            
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Customer/AddCustomer/{userId}", customer);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebCustomer>>();
        }

        public async Task<ServiceResponse<long>> ExistCustomer(long userId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/ExistCustomer/{userId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<long>>();
        }

        public async Task<ServiceResponse<WebCustomer>> GetCustomer(long customerId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/GetCustomer/{customerId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebCustomer>>();
        }

        public async Task<ServiceResponse<List<CustomerType>>> GetCustomerTypes()
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/GetCustomerTypes");
            var data =  await result.Content.ReadFromJsonAsync<ServiceResponse<List<CustomerType>>>();
            return data;
        }
    }
}