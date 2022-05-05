using ACM_API.Dtos.Executor;
using ACM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.ExecutorService
{
    public interface IExecutorService
    {
        Task<ServiceResponse<List<ExecutorDto>>> GetAllExecutors();

        Task<ServiceResponse<ExecutorDto>> GetExecutorById(long id);

        Task<ServiceResponse<ExecutorDto>> AddExecutor(ExecutorDto newExecutor, long userId);

        Task<ServiceResponse<ExecutorDto>> UpdateExecutor(ExecutorDto updatedExecutor);

        Task<ServiceResponse<bool>> DeleteExecutor(long userId);

        Task<ServiceResponse<List<SpecialityDto>>> GetSpecialityList();

        Task<ServiceResponse<List<CompetencyDto>>> GetCompetencyList();

        Task<ServiceResponse<long>> ExistExecutor(long userId);
    }
}
