using ACM_API.Dtos;
using ACM_API.Services.ModeratorService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ModeratorController : Controller
    {
        private readonly IModeratorService _moderatorService;

        public ModeratorController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }

        [HttpPut("CompService")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<bool>> SaveCompService(List<ServiceDto> services)
        {
            var response = await _moderatorService.SaveCompService(services);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
