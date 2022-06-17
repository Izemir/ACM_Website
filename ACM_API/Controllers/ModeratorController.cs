using ACM_API.Dtos;
using ACM_API.Dtos.Executor;
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

        [HttpGet("GetExes")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<ExecutorDto>>> GetExecutorsForApproval()
        {
            var response = await _moderatorService.GetExecutorsForApproval();
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("ApproveExe/{userId}/{executorId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<ExecutorDto>>> ApproveExecutor(long userId, long executorId)
        {
            var response = await _moderatorService.ApproveExecutor(userId,executorId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("AddSpeciality")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<SpecialityDto>>> AddSpeciality(SpecialityDto speciality)
        {
            var response = await _moderatorService.AddSpeciality(speciality);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("AddCompetency")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<CompetencyDto>>> AddCompetency(CompetencyDto competency)
        {
            var response = await _moderatorService.AddCompetency(competency);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("AddService")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<ServiceDto>>> AddService(ServiceDto service)
        {
            var response = await _moderatorService.AddService(service);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteSpeciality/{specialityId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<SpecialityDto>>> DeleteSpeciality(long specialityId)
        {
            var response = await _moderatorService.DeleteSpeciality(specialityId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteCompetency/{competencyId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<CompetencyDto>>> DeleteCompetency(long competencyId)
        {
            var response = await _moderatorService.DeleteCompetency(competencyId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteService/{serviceId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<ServiceDto>>> DeleteService(long serviceId)
        {
            var response = await _moderatorService.DeleteService(serviceId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
