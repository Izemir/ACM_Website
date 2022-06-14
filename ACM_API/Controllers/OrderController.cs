using ACM_API.Dtos.Order;
using ACM_API.Services.OrderService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("NewOrder/{constructionId}/{executorId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<OrderDto>> NewOrder(long constructionId, long executorId)
        {
            var response = await _orderService.NewOrder(constructionId, executorId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetOrder/{orderId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<OrderDto>> GetOrder(long orderId)
        {
            var response = await _orderService.GetOrder(orderId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetCustomerOrders/{customerId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<OrderDto>>> GetCustomerOrders(long customerId)
        {
            var response = await _orderService.GetCustomerOrders(customerId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetExecutorOrders/{executorId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<List<OrderDto>>> GetExecutorOrders(long executorId)
        {
            var response = await _orderService.GetExecutorOrders(executorId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("ChangeOrderStatus/{orderId}")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<OrderDto>> ChangeOrderStatus(long orderId)
        {
            var response = await _orderService.ChangeOrderStatus(orderId);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
