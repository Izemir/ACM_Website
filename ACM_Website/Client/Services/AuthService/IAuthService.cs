using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<long>> Register(UserRegister request);
        Task<ServiceResponse<WebUser>> Login(UserLogin request);
    }
}
