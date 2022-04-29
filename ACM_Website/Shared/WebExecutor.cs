using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class WebExecutor
    {
        public List<Competency> Competency { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public List<Contact> Contacts { get; set; }

        public bool IsDelete { get; set; }

        public List<Speciality> Speciality { get; set; }

        public string INN { get; set; }
    }
}
