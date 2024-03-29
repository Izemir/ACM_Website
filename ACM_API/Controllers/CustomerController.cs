﻿using ACM_API.DB;
using ACM_API.Dtos;
using ACM_API.Dtos.Customer;
using ACM_API.Models;
using ACM_API.Models.Customer;
using ACM_API.Services.CustomerService;
using Microsoft.AspNetCore.Cors;
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
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<CustomerDto>>> Get()
        {
            return Ok(await _customerService.GetAllCustomers());
        }

        [HttpGet("GetCustomer/{id}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<CustomerDto>> Get(long id)
        {            
            return Ok(await _customerService.GetCustomerById(id));
        }

        [HttpPost("AddCustomer/{userId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<CustomerDto>> AddCustomer(CustomerDto customer, long userId)
        {
            var result = await _customerService.AddCustomer(customer, userId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("Update")]
        [EnableCors("CorsPolicy")]
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
        [EnableCors("CorsPolicy")]
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
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<long>> ExistCustomer(long userId)
        {
            return Ok(await _customerService.ExistCustomer(userId));
        }

        [HttpGet("GetCustomerTypes")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<CustomerTypeDto>>> GetCustomerTypes()
        {
            return Ok(await _customerService.GetCustomerTypes());
        }

        [HttpGet("GetConstructions/{customerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<ConstructionDto>>> GetConstructions(long customerId)
        {
            var response = await _customerService.GetConstructions(customerId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetConstruction/{constructionId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ConstructionDto>> GetConstruction(long constructionId)
        {
            var response = await _customerService.GetConstruction(constructionId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetServices")]
        [EnableCors("CorsPolicy")]
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
        [EnableCors("CorsPolicy")]
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
        [EnableCors("CorsPolicy")]
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
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<List<ConstructionDto>>>> DeleteConstruction(long customerId, long constructionId)
        {
            var response = await _customerService.DeleteConstruction(customerId,constructionId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost("AddSubCustomer/{userId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<SubCustomerDto>> AddSubCustomer(SubCustomerDto sub, long userId)
        {
            var result = await _customerService.AddSubCustomer(sub, userId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateSubCustomer")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<SubCustomerDto>>> UpdateSubCustomer(SubCustomerDto sub)
        {
            var response = await _customerService.UpdateSubCustomer(sub);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteSubCustomer/{userId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteSubCustomer(long userId)
        {
            var response = await _customerService.DeleteSubCustomer(userId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("ExistSubCustomer/{userId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<long>> ExistSubCustomer(long userId)
        {
            return Ok(await _customerService.ExistSubCustomer(userId));
        }

        [HttpGet("GetSubCustomers/{customerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<SubCustomerDto>>> GetSubCustomers(long customerId)
        {
            var response = await _customerService.GetSubCustomers(customerId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("AddSubCustomerToCustomer/{customerId}/{subCustomerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<SubCustomerDto>>> AddSubCustomerToCustomer(long customerId, long subCustomerId)
        {
            var result = await _customerService.AddSubCustomerToCustomer(customerId,subCustomerId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetSubCustomerById/{customerId}/{subCustomerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<SubCustomerDto>> GetSubCustomerById(long customerId, long subCustomerId)
        {
            var response = await _customerService.GetSubCustomerById(customerId, subCustomerId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetCustomerSubs/{customerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<SubCustomerDto>>> GetCustomerSubs(long customerId)
        {
            var response = await _customerService.GetCustomerSubs(customerId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }

    

}
