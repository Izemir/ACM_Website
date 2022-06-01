using ACM_API.Dtos.Executor;
using ACM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.SearchService
{
    public interface ISearchService
    {
        Task<ServiceResponse<List<ExecutorDto>>> GetExesForConstructions(long constructionId);

        Task<ServiceResponse<List<ExecutorDto>>> GetConstructionForExes(long constructionId);
    }
}
