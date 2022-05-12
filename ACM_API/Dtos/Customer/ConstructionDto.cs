using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Dtos.Customer
{
    public class ConstructionDto
    {
        public long Id { get; set; }
        public string ConstructionName { get; set; }
        public string Description { get; set; }
        public List<ServiceDto> Services { get; set; }
    }
}
