using ACM_API.Dtos.Customer;
using ACM_API.Dtos.Executor;
using ACM_API.Services.SearchService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("GetExesForConstructions/{constructionId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<ExecutorDto>>> GetExesForConstructions(long constructionId)
        {
            var result = await _searchService.GetExesForConstructions(constructionId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetConstructionsForEx/{executorId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<ConstructionDto>>> GetConstructionsForEx(long executorId)
        {
            var result = await _searchService.GetConstructionsForEx(executorId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
