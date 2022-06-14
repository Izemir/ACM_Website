using ACM_API.Dtos.Order;
using ACM_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACM_API.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderDto>> NewOrder(long constructionId, long executorId);

        Task<ServiceResponse<OrderDto>> GetOrder(long orderId);

        Task<ServiceResponse<List<OrderDto>>> GetCustomerOrders(long customerId);

        Task<ServiceResponse<List<OrderDto>>> GetExecutorOrders(long executorId);

        Task<ServiceResponse<OrderDto>> ChangeOrderStatus(long orderId);
    }
}
