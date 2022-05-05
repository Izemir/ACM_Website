using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models.Customer
{
    public class Construction
    {
        public long Id { get; set; }
        public string ConstructionName { get; set; }
        public string Description { get; set; }
        public List<Service> Services { get; set; }
    }
}
