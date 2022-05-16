using ACM_API.Dtos;
using ACM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.ModeratorService
{
    public interface IModeratorService
    {
        Task<ServiceResponse<bool>> SaveCompService(List<ServiceDto> services);
    }
}
