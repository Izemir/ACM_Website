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

        [HttpGet("GetSender/{userId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<SenderDto>>> GetSender(long userId)
        {
            var response = await _chatService.GetSender(userId);
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

        [HttpGet("GetSubCustomerChats/{subCustomerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<List<ChatDto>>>> GetSubCustomerChats(long subCustomerId)
        {
            var response = await _chatService.GetSubCustomerChats(subCustomerId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("AddSubToChat/{chatId}/{subCustomerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<ChatDto>>> AddSubToChat(long chatId,long subCustomerId)
        {
            var response = await _chatService.AddSubToChat(chatId, subCustomerId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("DeleteSubFromChat/{chatId}/{subCustomerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ServiceResponse<ChatDto>>> DeleteSubFromChat(long chatId, long subCustomerId)
        {
            var response = await _chatService.DeleteSubFromChat(chatId, subCustomerId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
