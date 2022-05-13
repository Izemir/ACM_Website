using ACM_API.Dtos.User;
using ACM_API.Models;
using ACM_API.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ACM_API.Controllers
{
    [EnableCors(origins: "*", headers:"*", methods:"*" )]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<long>>> Register(AddUserDto request)
        {
            //var response = await _authService.Register(
            //    new User
            //    {
            //        Email = request.Email
            //    },
            //    request.Password);

            var response = await _authService.Register(request, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Login(GetUserDto user)
        {
            var response = await _authService.Login(user.Username, user.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
