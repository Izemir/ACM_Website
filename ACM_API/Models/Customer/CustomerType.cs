using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ACM_API.Models.Customer
{
    
    public class CustomerType
    {
        public long Id { get; set; }

        public string TypeName { get; set; }
    }
}
