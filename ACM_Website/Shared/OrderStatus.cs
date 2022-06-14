using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class OrderStatus
    {
        public long Id { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public bool IsOrderVisible { get; set; }
    }
}
