using ACM_API.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models.Executor
{
    public class Competency
    {
       

        public long Id { get; set; }

        public string CompetencyName { get; set; }

        public List<Executor> Executor { get; set; }

        public List<Service> Service { get; set; }
    }
}
