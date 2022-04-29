using ACM_Website.Shared;
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
    }
}
