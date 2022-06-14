using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class Order
    {
        public long Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public OrderStatus NextOrderStatus { get; set; }

        public long ConstructionId { get; set; }

        public long ExecutorId { get; set; }

        public long ChatId { get; set; }
    }
}
