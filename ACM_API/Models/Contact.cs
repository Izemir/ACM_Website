using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models
{
    public class Contact
    {
        public long Id { get; set; }

        public ContactType ContactType { get; set; }

        public string Address { get; set; }
    }
}
