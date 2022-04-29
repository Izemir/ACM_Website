using ACM_API.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Dtos.Customer
{
    public class CustomerDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public List<ContactPerson> ContactPersons { get; set; }

        public bool IsDelete { get; set; }

        public string OGRN { get; set; }

        public string INN { get; set; }

        public string KPP { get; set; }

        public string Address { get; set; }

        public string ActualAddress { get; set; }
        public CustomerType CustomerType { get; set; }

        public List<Industry> Industries { get; set; }
    }
}
