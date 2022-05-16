using ACM_API.Dtos.User;
using ACM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<long>> Register(AddUserDto user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<GetUserDto>> Login(string email, string password);

        Task<ServiceResponse<bool>> IsAdmin(long userId);
    }
}
