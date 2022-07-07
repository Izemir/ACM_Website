using ACM_API.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models.Chat
{
    public class Chat
    {
        public long Id { get; set; }

        public string NameForCustomer { get; set; }

        public string NameForExecutor { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
        public Executor.Executor Executor { get; set; }
        public Customer.Customer Customer { get; set; }

        public List<SubCustomer> SubCustomers { get; set; }

        public long OrderId { get; set; }
        public Order.Order  Order { get; set; }
    }
}
