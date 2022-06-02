using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class Construction
    {
        public long Id { get; set; }
        public string ConstructionName { get; set; }
        public string Description { get; set; }
        public List<Service> Services { get; set; } = new List<Service>();
        public bool IsNew { get; set; } = true;
        public long CustomerId { get; set; }
    }
}
