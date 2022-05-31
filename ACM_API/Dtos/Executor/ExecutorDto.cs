using ACM_API.Models;
using ACM_API.Models.Executor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Dtos.Executor
{
    public class ExecutorDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public List<Contact> Contacts { get; set; }

        public bool IsDelete { get; set; }

        public List<Speciality> Speciality { get; set; }

        public string INN { get; set; }

        public List<Competency> Competency { get; set; }

        public long UserId { get; set; }

        public bool Approved { get; set; }

    }
}
