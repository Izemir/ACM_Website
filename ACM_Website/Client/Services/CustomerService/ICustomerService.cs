﻿using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<ServiceResponse<WebCustomer>> AddCustomer(WebCustomer customer, long userId);
        Task<ServiceResponse<WebCustomer>> GetCustomer(long customerId);
        Task<ServiceResponse<long>> ExistCustomer(long userId);
        Task<ServiceResponse<List<CustomerType>>> GetCustomerTypes();
        Task<ServiceResponse<bool>> DeleteCustomer(long userId);
        Task<ServiceResponse<List<Construction>>> GetConstructions(long customerId);
        Task<ServiceResponse<Construction>> GetConstruction(long constructionId);
        Task<ServiceResponse<List<Service>>> GetServices();
        Task<ServiceResponse<List<Construction>>> AddConstruction(long customerId, Construction construction);
        Task<ServiceResponse<List<Construction>>> UpdateConstruction(long customerId, Construction construction);

        Task<ServiceResponse<List<Construction>>> DeleteConstruction(long customerId, long constructionId);

        Task<ServiceResponse<WebCustomer>> UpdateCustomer(WebCustomer customer);

        Task<ServiceResponse<WebSubCustomer>> AddSubCustomer(WebSubCustomer sub, long userId);

        Task<ServiceResponse<WebSubCustomer>> UpdateSubCustomer(WebSubCustomer sub);

        Task<ServiceResponse<bool>> DeleteSubCustomer(long userId);

        Task<ServiceResponse<long>> ExistSubCustomer(long userId);

        Task<ServiceResponse<List<WebSubCustomer>>> GetSubCustomers(long customerId);

        Task<ServiceResponse<List<WebSubCustomer>>> AddSubCustomerToCustomer(long customerId, long subCustomerId);

        Task<ServiceResponse<WebSubCustomer>> GetSubCustomerById(long customerId, long subCustomerId);

        Task<ServiceResponse<List<WebSubCustomer>>> GetCustomerSubs(long customerId);
    }
}
