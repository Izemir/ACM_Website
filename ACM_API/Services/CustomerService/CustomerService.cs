﻿using ACM_API.DB;
using ACM_API.Dtos.Customer;
using ACM_API.Models;
using ACM_API.Models.Customer;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public CustomerService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CustomerDto>> AddCustomer(CustomerDto newCustomer, long userId)
        {
            var serviceResponse = new ServiceResponse<CustomerDto>();
            try
            {
                var cus = _mapper.Map<Customer>(newCustomer);
                cus.CustomerType = _context.CustomerTypes.First(i => i.Id == newCustomer.CustomerType.Id);
                _context.Customers.Add(cus);
                await _context.SaveChangesAsync();
                //serviceResponse.Data = (await _context.Customers.ToListAsync()).Select(i => _mapper.Map<GetCustomerDto>(i)).ToList();
                var user = await _context.Users.FirstAsync(i => i.Id == userId);
                user.CustomerId = cus.Id;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<CustomerDto>(cus);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteCustomer(long userId)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var user = await _context.Users.FindAsync(userId);
                var customer = await _context.Customers
                    .Include(i => i.Industries)
                    .Include(i => i.ContactPersons)
                    .Include(i => i.CustomerType)
                    .FirstAsync(i => i.Id == user.CustomerId);


                _context.Customers.Remove(customer);
                user.CustomerId = null;

                await _context.SaveChangesAsync();

                serviceResponse.Data = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<long>> ExistCustomer(long userId)
        {
            var serviceResponse = new ServiceResponse<long>();
            var user = await _context.Users.FirstAsync(i => i.Id == userId);
            serviceResponse.Data = user.CustomerId ?? 0;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CustomerDto>>> GetAllCustomers()
        {
            var serviceResponse = new ServiceResponse<List<CustomerDto>>();
            var dBData = await _context.Customers
                .Include(i => i.Industries)
                .Include(i => i.ContactPersons)
                .Include(i => i.CustomerType)
                .ToListAsync();
            serviceResponse.Data = dBData.Select(i => _mapper.Map<CustomerDto>(i)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<CustomerDto>> GetCustomerById(long id)
        {
            var serviceResponse = new ServiceResponse<CustomerDto>();
            var customer = await _context.Customers
                .Include(i=>i.CustomerType)
                .Include(i=>i.ContactPersons)
                .ThenInclude(j=>j.Contacts)
                .FirstOrDefaultAsync(i=>i.Id==id);
            serviceResponse.Data = _mapper.Map<CustomerDto>(customer);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CustomerTypeDto>>> GetCustomerTypes()
        {
            var serviceResponse = new ServiceResponse<List<CustomerTypeDto>>();
            var data = await _context.CustomerTypes.ToListAsync();
            serviceResponse.Data = data.Select(i => _mapper.Map<CustomerTypeDto>(i)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<CustomerDto>> UpdateCustomer(CustomerDto updatedCustomer)
        {
            var serviceResponse = new ServiceResponse<CustomerDto>();
            try
            {
                var dBCustomer = await _context.Customers.FindAsync(updatedCustomer.Id);

                dBCustomer.ActualAddress = updatedCustomer.ActualAddress;
                dBCustomer.Address = updatedCustomer.Address;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<CustomerDto>(await _context.Customers.FindAsync(updatedCustomer.Id));
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
