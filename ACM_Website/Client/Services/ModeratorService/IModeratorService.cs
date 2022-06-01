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

        Task<ServiceResponse<List<Service>>> AddService(Service service);
        Task<ServiceResponse<List<Competency>>> AddCompetency(Competency competency);
        Task<ServiceResponse<List<Speciality>>> AddSpeciality(Speciality speciality);

        Task<ServiceResponse<List<Service>>> DeleteService(long serviceId);
        Task<ServiceResponse<List<Competency>>> DeleteCompetency(long competencyId);
        Task<ServiceResponse<List<Speciality>>> DeleteSpeciality(long specialityId);
    }
}
