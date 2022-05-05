using ACM_API.DB;
using ACM_API.Dtos.Executor;
using ACM_API.Models;
using ACM_API.Models.Executor;
using ACM_API.Services.ExecutorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExecutorController : Controller
    {

        private readonly IExecutorService _executorService;

        public ExecutorController(IExecutorService executorService)
        {
            _executorService = executorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExecutorDto>>> Get()
        {
            return Ok(await _executorService.GetAllExecutors());
        }

        [HttpGet("GetExecutor/{id}")]
        public async Task<ActionResult<ExecutorDto>> Get(long id)
        {
            return Ok(await _executorService.GetExecutorById(id));
        }

        [HttpPost("AddExecutor/{userId}")]
        public async Task<ActionResult<ExecutorDto>> AddExecutor(ExecutorDto Executor, long userId)
        {
            var result = await _executorService.AddExecutor(Executor, userId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<ExecutorDto>>> UpdateExecutor(ExecutorDto request)
        {
            var response = await _executorService.UpdateExecutor(request);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteExecutor/{userId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteExecutor(int userId)
        {
            var response = await _executorService.DeleteExecutor(userId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetSpecialityList")]
        public async Task<ActionResult<List<SpecialityDto>>> GetSpecialityList()
        {
            return Ok(await _executorService.GetSpecialityList());
        }

        [HttpGet("GetCompetencyList")]
        public async Task<ActionResult<List<CompetencyDto>>> GetCompetencyList()
        {
            return Ok(await _executorService.GetCompetencyList());
        }

        [HttpGet("ExistExecutor/{userId}")]
        public async Task<ActionResult<long>> ExistExecutor(long userId)
        {
            return Ok(await _executorService.ExistExecutor(userId));
        }
    }
}
