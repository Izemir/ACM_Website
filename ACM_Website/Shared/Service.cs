using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class Service
    {
        public long Id { get; set; }

        public string ServiceName { get; set; } = string.Empty;

        public List<Competency> Competencies { get; set; } = new List<Competency>();
    }
}
