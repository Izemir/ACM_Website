using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.SearchService
{
    public interface ISearchService
    {
        Task<ServiceResponse<List<WebExecutor>>> GetExecutorsForConstruction(long constructionId);
    }
}
