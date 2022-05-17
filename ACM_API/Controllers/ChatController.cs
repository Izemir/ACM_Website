using ACM_API.Dtos.Chat;
using ACM_API.Models;
using ACM_API.Services.ChatService;
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
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("GetChat/{chatId}/{requestId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<ChatDto>>> GetChat(long chatId, long requestId)
        {
            var response = await _chatService.GetChat(chatId, requestId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetCustomerChats/{customerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<List<ChatDto>>>> GetCustomerChats(long customerId)
        {
            var response = await _chatService.GetCustomerChats(customerId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("GetExecutorChats/{executorId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<List<ChatDto>>>> GetExecutorChats(long executorId)
        {
            var response = await _chatService.GetExecutorChats(executorId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("SendMessage/{chatId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<ChatDto>>> SendMessage(long chatId, MessageDto message)
        {
            var response = await _chatService.SendMessage(chatId, message);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("StartChat/{customerId}/{executorId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<ChatDto>>> StartNewChat(long customerId, long executorId)
        {
            var response = await _chatService.StartNewChat(customerId, executorId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("ExistChat/{customerId}/{executorId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<long>>> ExistChat(long customerId, long executorId)
        {
            var response = await _chatService.ExistChat(customerId, executorId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
