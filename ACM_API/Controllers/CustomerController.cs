using ACM_API.DB;
using ACM_API.Dtos;
using ACM_API.Dtos.Customer;
using ACM_API.Models;
using ACM_API.Models.Customer;
using ACM_API.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> Get()
        {
            return Ok(await _customerService.GetAllCustomers());
        }

        [HttpGet("GetCustomer/{id}")]
        public async Task<ActionResult<CustomerDto>> Get(long id)
        {            
            return Ok(await _customerService.GetCustomerById(id));
        }

        [HttpPost("AddCustomer/{userId}")]
        public async Task<ActionResult<CustomerDto>> AddCustomer(CustomerDto customer, long userId)
        {
            var result = await _customerService.AddCustomer(customer, userId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<CustomerDto>>> UpdateCustomer(CustomerDto request)
        {
            var response = await _customerService.UpdateCustomer(request);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteCustomer/{userId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCustomer(long userId)
        {
            var response = await _customerService.DeleteCustomer(userId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("ExistCustomer/{userId}")]
        public async Task<ActionResult<long>> ExistCustomer(long userId)
        {
            return Ok(await _customerService.ExistCustomer(userId));
        }

        [HttpGet("GetCustomerTypes")]
        public async Task<ActionResult<List<CustomerTypeDto>>> GetCustomerTypes()
        {
            return Ok(await _customerService.GetCustomerTypes());
        }

        [HttpGet("GetConstructions/{customerId}")]
        public async Task<ActionResult<List<ConstructionDto>>> GetConstructions(long customerId)
        {
            var response = await _customerService.GetConstructions(customerId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetServices")]
        public async Task<ActionResult<List<ServiceDto>>> GetServices()
        {
            var response = await _customerService.GetServices();
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("AddConstruction/{customerId}")]
        public async Task<ActionResult<ServiceResponse<List<ConstructionDto>>>> AddConstruction(long customerId, ConstructionDto construction)
        {
            var response = await _customerService.AddConstruction(customerId, construction);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("UpdateConstruction/{customerId}")]
        public async Task<ActionResult<ServiceResponse<List<ConstructionDto>>>> UpdateConstruction(long customerId, ConstructionDto construction)
        {
            var response = await _customerService.UpdateConstruction(customerId, construction);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteConstruction/{customerId}/{constructionId}")]
        public async Task<ActionResult<ServiceResponse<List<ConstructionDto>>>> DeleteConstruction(long customerId, long constructionId)
        {
            var response = await _customerService.DeleteConstruction(customerId,constructionId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }

    

}
