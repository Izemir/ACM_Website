using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models.Customer
{
    public class Milestone
    {
        public long Id { get; set; }

        public string MilestoneName { get; set; }

        public List<Service> Services { get; set; }
    }
}
