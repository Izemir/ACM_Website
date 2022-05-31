using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.ModeratorService
{
    public interface IModeratorService
    {
        Task<ServiceResponse<bool>> SaveCompetencyService(List<Service> services);

        Task<ServiceResponse<List<WebExecutor>>> GetExecutorsForApproval();

        Task<ServiceResponse<WebExecutor>> ApproveExecutor(long userId, long executorId);
    }
}
