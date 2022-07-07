using ACM_API.Dtos;
using ACM_API.Dtos.Chat;
using ACM_API.Dtos.Customer;
using ACM_API.Dtos.Executor;
using ACM_API.Dtos.Order;
using ACM_API.Dtos.User;
using ACM_API.Models;
using ACM_API.Models.Chat;
using ACM_API.Models.Customer;
using ACM_API.Models.Executor;
using ACM_API.Models.Order;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Executor, ExecutorDto>();
            CreateMap<ExecutorDto, Executor>();
            CreateMap<CustomerType, CustomerTypeDto>();
            CreateMap<Competency, CompetencyDto>();
            CreateMap<CompetencyDto, Competency>();
            CreateMap<Speciality, SpecialityDto>();
            CreateMap<ConstructionDto, Construction>();
            CreateMap<Construction, ConstructionDto>();
            CreateMap<ServiceDto, Service>().IncludeAllDerived();
            CreateMap<Service, ServiceDto>().IncludeAllDerived();

            CreateMap<ChatDto, Chat>();
            CreateMap<Chat, ChatDto>();
            CreateMap<MessageDto, Message>();
            CreateMap<Message, MessageDto>();

            CreateMap<UserFileDto, UserFile>();
            CreateMap<UserFile, UserFileDto>();

            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderStatusDto, OrderStatus>();
            CreateMap<OrderStatus, OrderStatusDto>();

            CreateMap<SubCustomerDto, SubCustomer>();
            CreateMap<SubCustomer, SubCustomerDto>();
        }
    }
}
