using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<Order>> NewOrder(long constructionId, long executorId);

        Task<ServiceResponse<Order>> GetOrder(long orderId);

        Task<ServiceResponse<List<Order>>> GetCustomerOrders(long customerId);

        Task<ServiceResponse<List<Order>>> GetExecutorOrders(long executorId);

        Task<ServiceResponse<Order>> ChangeOrderStatus(long orderId);
    }
}
