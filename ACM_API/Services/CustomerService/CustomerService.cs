using ACM_API.DB;
using ACM_API.Dtos;
using ACM_API.Dtos.Customer;
using ACM_API.Dtos.Executor;
using ACM_API.Models;
using ACM_API.Models.Customer;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
                var user = await _context.Users.FirstAsync(i => i.Id == userId);
                user.Customer = cus;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                //serviceResponse.Data = (await _context.Customers.ToListAsync()).Select(i => _mapper.Map<GetCustomerDto>(i)).ToList();
                //var user = await _context.Users.FirstAsync(i => i.Id == userId);
                //user.CustomerId = cus.Id;
                //_context.Entry(user).State = EntityState.Modified;
                //await _context.SaveChangesAsync();
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
                    .ThenInclude(j=>j.Contacts)
                    .Include(i => i.CustomerType)
                    .Include(i=>i.Chats)
                    .ThenInclude(j=>j.Messages)
                    .Include(i=>i.Constructions)
                    .ThenInclude(j=>j.Orders)
                    .ThenInclude(o=>o.Chat)
                    .Include(i => i.Constructions)
                    .ThenInclude(j => j.Orders)
                    .ThenInclude(o => o.Files)
                    .FirstAsync(i => i.User == user);

                foreach(var person in customer.ContactPersons)
                {
                    foreach(var c in person.Contacts)
                    {
                        _context.Entry(c).State = EntityState.Deleted;
                    }
                    _context.Entry(person).State = EntityState.Deleted;
                }
                foreach (var chat in customer.Chats)
                {
                    foreach (var message in chat.Messages)
                    {
                        _context.Entry(message).State = EntityState.Deleted;
                    }
                    _context.Entry(chat).State = EntityState.Deleted;
                }
                foreach (var c in customer.Constructions)
                {
                    foreach(var order in c.Orders)
                    {
                        foreach(var file in order.Files)
                        {
                            await DeleteFileAsync(file);
                            _context.Entry(file).State = EntityState.Deleted;
                        }
                        var chat = order.Chat;
                        if (chat != null)
                        {
                            if (chat.Messages != null)
                            {
                                foreach (var message in chat.Messages)
                                {
                                    _context.Entry(message).State = EntityState.Deleted;
                                }
                            }
                            _context.Entry(chat).State = EntityState.Deleted;
                        }
                        _context.Entry(order).State = EntityState.Deleted;
                    }

                    _context.Entry(c).State = EntityState.Deleted;
                }

                customer.User = null;
                _context.Customers.Remove(customer);
                //user.CustomerId = null;

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
                .Include(i=>i.User)
                .Include(i=>i.CustomerType)
                .Include(i=>i.ContactPersons)
                .ThenInclude(j=>j.Contacts)
                .FirstOrDefaultAsync(i=>i.Id==id);
            var data = _mapper.Map<CustomerDto>(customer);
            data.UserId = customer.User.Id;
            serviceResponse.Data = data;
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
                var customer = await _context.Customers
                    .Include(i => i.ContactPersons)
                    .ThenInclude(j => j.Contacts)
                    .Include(i => i.CustomerType)
                    .FirstAsync(i => i.Id == updatedCustomer.Id);

                customer.CustomerType = _context.CustomerTypes.First(i => i.Id == updatedCustomer.CustomerType.Id);

                customer.Name=updatedCustomer.Name;
                customer.FullName=updatedCustomer.FullName;
                customer.Address=updatedCustomer.Address;
                customer.ActualAddress=updatedCustomer.ActualAddress;
                customer.OGRN=updatedCustomer.OGRN;
                customer.INN=updatedCustomer.INN;
                customer.KPP=updatedCustomer.KPP;
                customer.ContactPersons = updatedCustomer.ContactPersons;

                _context.Entry(customer).State = EntityState.Modified;
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


        public async Task<ServiceResponse<List<ConstructionDto>>> AddConstruction(long customerId, ConstructionDto construction)
        {
            var serviceResponse = new ServiceResponse<List<ConstructionDto>>();
            try
            {
                var customer = await _context.Customers
                    .Include(i=>i.Constructions)                    
                    .FirstAsync(i=>i.Id==customerId);

                var ids = construction.Services.Select(i => i.Id).ToList();
                var services = _context.Services.Where(i => ids.Contains(i.Id)).ToList();

                var newConstr = _mapper.Map<Construction>(construction);

                newConstr.Services = services;

                customer.Constructions.Add(newConstr);

                await _context.SaveChangesAsync();

                var data = customer.Constructions.ToList();
                serviceResponse.Data = data.Select(i => _mapper.Map<ConstructionDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ConstructionDto>>> UpdateConstruction(long customerId, ConstructionDto construction)
        {
            var serviceResponse = new ServiceResponse<List<ConstructionDto>>();
            try
            {
                var customer = await _context.Customers
                    .Include(i => i.Constructions)
                    .ThenInclude(j=>j.Services)
                    .FirstAsync(i => i.Id == customerId);

                if (customer == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет данного заказчика";
                    return serviceResponse;
                }

                var oldConstr = customer.Constructions.First(i => i.Id == construction.Id);
                if (oldConstr == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет данного объекта";
                    return serviceResponse;
                }

                var ids = construction.Services.Select(i => i.Id).ToList();
                var services = _context.Services.Where(i => ids.Contains(i.Id)).ToList();

                oldConstr.ConstructionName = construction.ConstructionName;
                oldConstr.Description = construction.Description;
                oldConstr.Services = services;

                await _context.SaveChangesAsync();

                var data = customer.Constructions.ToList();
                serviceResponse.Data = data.Select(i => _mapper.Map<ConstructionDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ConstructionDto>>> GetConstructions(long customerId)
        {
            var serviceResponse = new ServiceResponse<List<ConstructionDto>>();
            try
            {
                var customer = await _context.Customers
                    .Include(i => i.Constructions)
                    .ThenInclude(j => j.Services)
                    .FirstAsync(i => i.Id == customerId);

                if (customer == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет данного заказчика";
                    return serviceResponse;
                }

                var data = customer.Constructions.ToList();
                serviceResponse.Data = data.Select(i => _mapper.Map<ConstructionDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<ConstructionDto>> GetConstruction(long constructionId)
        {
            var serviceResponse = new ServiceResponse<ConstructionDto>();
            try
            {
                var construction = await _context.Constructions
                    .Include(i => i.Services)
                    .FirstAsync(i => i.Id == constructionId);

                if (construction == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет данного объекта";
                    return serviceResponse;
                }

                
                serviceResponse.Data = _mapper.Map<ConstructionDto>(construction);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ServiceDto>>> GetServices()
        {
            var serviceResponse = new ServiceResponse<List<ServiceDto>>();
            try
            {
                var data = await _context.Services
                    .Include(i=>i.Competency)
                    .ToListAsync();
                List<ServiceDto> services = new List<ServiceDto>();
                foreach(var s in data)
                {
                    var serviceDto = _mapper.Map<ServiceDto>(s);
                    List<CompetencyDto> competencies = new List<CompetencyDto>();
                    foreach (var c in s.Competency)
                    {
                        competencies.Add(_mapper.Map<CompetencyDto>(c));
                    }
                    serviceDto.Competencies = competencies;
                    services.Add(serviceDto);
                    
                }
                serviceResponse.Data = services;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ConstructionDto>>> DeleteConstruction(long customerId, long constructionId)
        {
            var serviceResponse = new ServiceResponse<List<ConstructionDto>>();
            try
            {
                var customer = await _context.Customers
                    .Include(i => i.Constructions)
                    .ThenInclude(j=>j.Orders)
                    .ThenInclude(o=>o.Chat)
                    .Include(i => i.Constructions)
                    .ThenInclude(j => j.Orders)
                    .ThenInclude(o => o.Files)
                    .FirstAsync(i => i.Id == customerId);

                var construction = customer.Constructions.Find(i => i.Id == constructionId);

                foreach (var order in construction.Orders)
                {
                    foreach (var file in order.Files)
                    {
                        await DeleteFileAsync(file);
                        _context.Entry(file).State = EntityState.Deleted;
                    }
                    var chat = order.Chat;
                    if (chat != null)
                    {
                        if (chat.Messages != null)
                        {
                            foreach (var message in chat.Messages)
                            {
                                _context.Entry(message).State = EntityState.Deleted;
                            }
                        }
                        _context.Entry(chat).State = EntityState.Deleted;
                    }
                    _context.Entry(order).State = EntityState.Deleted;
                }

                customer.Constructions.Remove(construction);

                await _context.SaveChangesAsync();

                var data = customer.Constructions.ToList();
                serviceResponse.Data = data.Select(i => _mapper.Map<ConstructionDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        private async Task<bool> DeleteFileAsync(UserFile file)
        {
            try
            {
                if (File.Exists(file.Path))
                {
                    File.Delete(file.Path);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }

    
}
