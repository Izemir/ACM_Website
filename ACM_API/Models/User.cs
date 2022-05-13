using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;


        public long? CustomerId { get; set; }
        public Customer.Customer Customer { get; set; }        

        public long? ExecutorId { get; set; }

        public Executor.Executor Executor { get; set; }        

        public string Role { get; set; } = "Заказчик";
    }
}
