using ACM_API.Dtos.Customer;
using ACM_API.Models;
using ACM_API.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<ServiceResponse<List<CustomerDto>>> GetAllCustomers();

        Task<ServiceResponse<CustomerDto>> GetCustomerById(long id);

        Task<ServiceResponse<CustomerDto>> AddCustomer(CustomerDto newCustomer, long userId);

        Task<ServiceResponse<CustomerDto>> UpdateCustomer(CustomerDto updatedCustomer);

        Task<ServiceResponse<bool>> DeleteCustomer(long userId);

        Task<ServiceResponse<long>> ExistCustomer(long userId);

        Task<ServiceResponse<List<CustomerTypeDto>>> GetCustomerTypes();
    }
}
