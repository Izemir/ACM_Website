using ACM_API.Models.Customer;

namespace ACM_API.Models.Order
{
    public class Order
    {
        public long Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public OrderStatus NextOrderStatus { get; set; }

        public Construction Construction { get; set; }

        public Executor.Executor Executor { get; set; }

        public Chat.Chat Chat { get; set; }
    }
}
