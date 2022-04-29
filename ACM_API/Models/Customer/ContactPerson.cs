using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models.Customer
{
    public class ContactPerson
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}
