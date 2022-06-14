using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.ExecutorService
{
    public interface IExecutorService
    {
        Task<ServiceResponse<WebExecutor>> AddExecutor(WebExecutor executor, long userId);
        Task<ServiceResponse<WebExecutor>> GetExecutor(long executorId);
        Task<ServiceResponse<long>> ExistExecutor(long userId);
        Task<ServiceResponse<List<Competency>>> GetCompetencyList();
        Task<ServiceResponse<List<Speciality>>> GetSpecialityList();
        Task<ServiceResponse<bool>> DeleteExecutor(long userId);
        Task<ServiceResponse<WebExecutor>> UpdateExecutor(WebExecutor executor);
        Task<ServiceResponse<bool>> IsExecutorApproved(long executorId);
    }
}
