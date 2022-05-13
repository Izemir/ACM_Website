using ACM_API.Dtos.Executor;
using ACM_API.Services.SearchService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ACM_API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("GetConstructionExes/{constructionId}")]
        public async Task<ActionResult<List<ExecutorDto>>> GetConstructionExes(long constructionId)
        {
            var result = await _searchService.GetConstructionExes(constructionId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
