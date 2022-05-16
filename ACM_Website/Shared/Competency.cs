using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM_Website.Shared
{
    public class Competency
    {
        public List<WebExecutor> Executor { get; set; }

        public List<Service> Service { get; set; }

        public long Id { get; set; }

        public string CompetencyName { get; set; } = string.Empty;
    }
}
