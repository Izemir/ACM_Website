using ACM_API.Dtos.Executor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Dtos
{
    public class ServiceDto
    {
        public long Id { get; set; }

        public string ServiceName { get; set; }

        public List<CompetencyDto> Competencies { get; set; }
    }
}
