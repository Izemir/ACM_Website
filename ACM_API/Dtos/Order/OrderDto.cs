using ACM_API.Models.Order;

namespace ACM_API.Dtos.Order
{
    public class OrderDto
    {
        public long Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public OrderStatus NextOrderStatus { get; set; }

        public long ConstructionId { get; set; }

        public long ExecutorId { get; set; }

        public long ChatId { get; set; }
    }
}
