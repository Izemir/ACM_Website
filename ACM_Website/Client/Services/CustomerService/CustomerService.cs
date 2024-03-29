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

        public async Task<ServiceResponse<bool>> DeleteCustomer(long userId)
        {
            var result = await _http.DeleteAsync($"{apiConnection}/Customer/DeleteCustomer/{userId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
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

        public async Task<ServiceResponse<List<Service>>> GetServices()
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/GetServices");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Service>>>();
        }

        public async Task<ServiceResponse<List<Construction>>> GetConstructions(long customerId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/GetConstructions/{customerId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Construction>>>();
        }

        public async Task<ServiceResponse<List<Construction>>> AddConstruction(long customerId, Construction construction)
        {
            var result = await _http.PutAsJsonAsync($"{apiConnection}/Customer/AddConstruction/{customerId}", construction);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Construction>>>();
        }

        public async Task<ServiceResponse<List<Construction>>> UpdateConstruction(long customerId, Construction construction)
        {
            var result = await _http.PutAsJsonAsync($"{apiConnection}/Customer/UpdateConstruction/{customerId}", construction);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Construction>>>();
        }

        public async Task<ServiceResponse<List<Construction>>> DeleteConstruction(long customerId, long constructionId)
        {
            var result = await _http.DeleteAsync($"{apiConnection}/Customer/DeleteConstruction/{customerId}/{constructionId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Construction>>>();
        }

        public async Task<ServiceResponse<WebCustomer>> UpdateCustomer(WebCustomer customer)
        {
            var result = await _http.PutAsJsonAsync($"{apiConnection}/Customer/Update", customer);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebCustomer>>();
        }

        public async Task<ServiceResponse<Construction>> GetConstruction(long constructionId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/GetConstruction/{constructionId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<Construction>>();
        }

        public async Task<ServiceResponse<WebSubCustomer>> AddSubCustomer(WebSubCustomer sub, long userId)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Customer/AddSubCustomer/{userId}", sub);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebSubCustomer>>();
        }

        public async Task<ServiceResponse<WebSubCustomer>> UpdateSubCustomer(WebSubCustomer sub)
        {
            var result = await _http.PutAsJsonAsync($"{apiConnection}/Customer/UpdateSubCustomer", sub);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebSubCustomer>>();
        }

        public async Task<ServiceResponse<bool>> DeleteSubCustomer(long userId)
        {
            var result = await _http.DeleteAsync($"{apiConnection}/Customer/DeleteSubCustomer/{userId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<long>> ExistSubCustomer(long userId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/ExistSubCustomer/{userId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<long>>();
        }

        public async Task<ServiceResponse<List<WebSubCustomer>>> GetSubCustomers(long customerId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/GetSubCustomers/{customerId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<WebSubCustomer>>>();
        }

        public async Task<ServiceResponse<List<WebSubCustomer>>> AddSubCustomerToCustomer(long customerId, long subCustomerId)
        {
            var result = await _http.PutAsJsonAsync($"{apiConnection}/Customer/AddSubCustomerToCustomer/{customerId}/{subCustomerId}",0);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<WebSubCustomer>>>();
        }

        public async Task<ServiceResponse<WebSubCustomer>> GetSubCustomerById(long customerId, long subCustomerId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/GetSubCustomerById/{customerId}/{subCustomerId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebSubCustomer>>();
        }

        public async Task<ServiceResponse<List<WebSubCustomer>>> GetCustomerSubs(long customerId)
        {
            var result = await _http.GetAsync($"{apiConnection}/Customer/GetCustomerSubs/{customerId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<WebSubCustomer>>>();
        }
    }
}
