using ACM_API.DB;
using ACM_API.Dtos.Order;
using ACM_API.Models;
using ACM_API.Models.Chat;
using ACM_API.Models.Order;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.OrderService
{
    public class OrderService: IOrderService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public OrderService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<OrderDto>> ChangeOrderStatus(long orderId)
        {
            var serviceResponse = new ServiceResponse<OrderDto>();
            try
            {
                var order = await _context.Orders
                    .Include(i => i.OrderStatus)
                    .Include(i => i.NextOrderStatus)
                    .Include(i => i.Chat)
                    .Include(i => i.Construction)
                    .Include(i => i.Executor)
                    .FirstOrDefaultAsync(i => i.Id == orderId);                

                if (order == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого заказа";
                    return serviceResponse;
                }

                order.OrderStatus = order.NextOrderStatus;

                var status = GetNextStatus(order.NextOrderStatus.Id);

                order.NextOrderStatus = status;

                await _context.SaveChangesAsync();

                var orderDto = _mapper.Map<OrderDto>(order);
                orderDto.ExecutorId = order.Executor.Id;
                orderDto.ChatId = order.Chat.Id;
                orderDto.ConstructionId = order.Construction.Id;

                serviceResponse.Data = orderDto;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<OrderDto>>> GetCustomerOrders(long customerId)
        {
            var serviceResponse = new ServiceResponse<List<OrderDto>>();
            try
            {
                var customer = await _context.Customers
                 .Include(i => i.Constructions)
                 .ThenInclude(c => c.Orders)
                 .ThenInclude(c => c.Chat)
                 .Include(i => i.Constructions)
                 .ThenInclude(c => c.Orders)
                 .ThenInclude(c => c.Executor)
                 .FirstAsync(i => i.Id == customerId);

                List<Order> orders = new List<Order>();

                foreach(var construction in customer.Constructions)
                {
                    orders.AddRange(construction.Orders.ToList());
                }


                List<OrderDto> orderDtos = new List<OrderDto>();
                foreach (var order in orders)
                {
                    var orderDto = _mapper.Map<OrderDto>(order);
                    orderDto.ExecutorId = order.Executor.Id;
                    orderDto.ChatId = order.Chat.Id;
                    orderDto.ConstructionId = order.Construction.Id;
                    orderDtos.Add(orderDto);
                }

                serviceResponse.Data = orderDtos;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<OrderDto>>> GetExecutorOrders(long executorId)
        {
            var serviceResponse = new ServiceResponse<List<OrderDto>>();
            try
            {
                var executor = await _context.Executors
                 .Include(i => i.Orders)
                 .ThenInclude(c => c.Construction)
                 .Include(i=>i.Orders)
                 .ThenInclude(c=>c.Chat)
                 .FirstAsync(i => i.Id == executorId);

                List<OrderDto> orders = new List<OrderDto>();
                foreach (var order in executor.Orders)
                {
                    var orderDto = _mapper.Map<OrderDto>(order);
                    orderDto.ExecutorId = order.Executor.Id;
                    orderDto.ChatId = order.Chat.Id;
                    orderDto.ConstructionId = order.Construction.Id;
                    orders.Add(orderDto);
                }

                serviceResponse.Data = orders;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }


        public async Task<ServiceResponse<OrderDto>> GetOrder(long orderId)
        {
            var serviceResponse = new ServiceResponse<OrderDto>();
            try
            {
                var order = await _context.Orders
                    .Include(i => i.OrderStatus)
                    .Include(i => i.NextOrderStatus)
                    .Include(i => i.Chat)
                    .Include(i => i.Construction)
                    .Include(i => i.Executor)
                    .FirstOrDefaultAsync(i => i.Id == orderId);

                if (order== null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого заказа";
                    return serviceResponse;
                }

                

                var orderDto = _mapper.Map<OrderDto>(order);
                orderDto.ExecutorId = order.Executor.Id;
                orderDto.ChatId = order.Chat.Id;
                orderDto.ConstructionId = order.Construction.Id;

                serviceResponse.Data = orderDto;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<OrderDto>> NewOrder(long constructionId, long executorId)
        {
            var serviceResponse = new ServiceResponse<OrderDto>();
            try
            {
                var existedOrder = await _context.Orders
                    .Include(i => i.OrderStatus)
                    .Include(i => i.NextOrderStatus)
                    .Include(i => i.Chat)
                    .Include(i => i.Construction)
                    .Include(i => i.Executor)
                    .FirstOrDefaultAsync(i => i.Construction.Id == constructionId && i.Executor.Id==executorId);

                if (existedOrder != null)
                {
                    var existedOrderDto = _mapper.Map<OrderDto>(existedOrder);
                    existedOrderDto.ExecutorId = existedOrder.Executor.Id;
                    existedOrderDto.ChatId = existedOrder.Chat.Id;
                    existedOrderDto.ConstructionId = existedOrder.Construction.Id;

                    serviceResponse.Data = existedOrderDto;

                    return serviceResponse;
                }


                var construction = await _context.Constructions
                    .Include(i => i.Orders)
                    .FirstOrDefaultAsync(i => i.Id == constructionId);
                var executor = await _context.Executors
                    .Include(i => i.Orders)
                    .FirstOrDefaultAsync(i => i.Id == executorId);
                var customer = await _context.Customers
                    .Include(i => i.Chats)
                    .FirstOrDefaultAsync(i => i.Constructions.Contains(construction));
                var statuses = _context.OrderStatuses.ToList();

                var order = new Order() { 
                    Construction = construction,
                    Executor = executor,
                    OrderStatus = statuses.First(i => i.Id == 1),
                    NextOrderStatus = GetNextStatus(1)
                };

                var chat = new Chat() { Customer = customer, Executor = executor };

                chat.NameForCustomer = $"Чат {construction.ConstructionName} - с {executor.Name}";
                chat.NameForExecutor = $"Чат {construction.ConstructionName} - с {customer.Name}";

                order.Chat = chat;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                var orderDto = _mapper.Map<OrderDto>(order);
                orderDto.ExecutorId = order.Executor.Id;
                orderDto.ChatId = order.Chat.Id;
                orderDto.ConstructionId = order.Construction.Id;

                serviceResponse.Data = orderDto;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private OrderStatus GetNextStatus(long statusId)
        {
            var statuses = _context.OrderStatuses.ToList();

            OrderStatus status = new OrderStatus();

            switch (statusId)
            {
                case 1:
                    status = statuses.First(i => i.Id == 2);
                    break;
                case 2:
                    status = statuses.First(i => i.Id == 3);
                    break;
                case 3:
                    status = statuses.First(i => i.Id == 4);
                    break;
                case 4:
                    status = statuses.First(i => i.Id == 5);
                    break;
                case 5:
                    status = statuses.First(i => i.Id == 6);
                    break;
                case 6:
                    status = statuses.First(i => i.Id == 7);
                    break;
                case 7:
                    status = statuses.First(i => i.Id == 8);
                    break;
                case 8:
                    status = statuses.First(i => i.Id == 9);
                    break;
                case 9:
                    status = statuses.First(i => i.Id == 10);
                    break;
                default: break;
            }

            return status;
        }
    }
}
