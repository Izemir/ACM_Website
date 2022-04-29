using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models.Customer
{
    public class Subject
    {
        public long Id { get; set; }

        public string SubjectName { get; set; }

        public SubjectType SubjectType { get; set; }

        public List<Milestone> Milestones { get; set; }
    }
}
