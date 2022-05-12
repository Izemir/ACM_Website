using ACM_API.Models.Customer;
using ACM_API.Models.Executor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models
{
    public class Service
    {
        
        public long Id { get; set; }

        public string ServiceName { get; set; }
        public List<Competency> Competency { get; set; }

        public List<Construction> Constructions { get; set; }
    }
}
