using ACM_API.Dtos;
using ACM_API.Services.FileService;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACM_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : Controller
    {

        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("AddFileToExecutor/{executorId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<UserFileDto>>> AddFileToExecutor(long executorId, UserFileDto file)
        {
            var response = await _fileService.AddFileToExecutor(executorId, file);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetFile/{fileId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<UserFileDto>> GetFile(long fileId)
        {
            var response = await _fileService.GetFile(fileId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetExecutorFiles/{executorId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<UserFileDto>>> GetExecutorFiles(long executorId)
        {
            var response = await _fileService.GetExecutorFiles(executorId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteFileOfExecutor/{executorId}/{fileId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<UserFileDto>>> DeleteFileOfExecutor(long executorId, long fileId)
        {
            var response = await _fileService.DeleteFileOfExecutor(executorId, fileId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("AddFileToOrder/{orderId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<UserFileDto>>> AddFileToOrder(long orderId, UserFileDto file)
        {
            var response = await _fileService.AddFileToOrder(orderId, file);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpGet("GetOrderFiles/{orderId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<UserFileDto>>> GetOrderFiles(long orderId)
        {
            var response = await _fileService.GetOrderFiles(orderId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteFileOfOrder/{orderId}/{fileId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<UserFileDto>>> DeleteFileOfOrder(long orderId, long fileId)
        {
            var response = await _fileService.DeleteFileOfOrder(orderId, fileId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


    }
}
