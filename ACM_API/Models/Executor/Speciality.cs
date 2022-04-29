using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Models.Executor
{
    public class Speciality
    {
        

        public long Id { get; set; }

        public string SpecialityName { get; set; }

        public List<Executor> Executor { get; set; }

    }
}
