using ACM_Website.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.ModeratorService
{
    public class ModeratorService : IModeratorService
    {

            private readonly HttpClient _http;
            private readonly string apiConnection;


            public ModeratorService(HttpClient http)
            {
                _http = http;
#if (DEBUG)
                apiConnection = GlobalConstants.Api.DebugApiAddress;
#else
            apiConnection = GlobalConstants.Api.ApiAddress;
#endif
            }

        public async Task<ServiceResponse<List<Competency>>> AddCompetency(Competency competency)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Moderator/AddCompetency", competency);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Competency>>>();
        }

        public async Task<ServiceResponse<List<Service>>> AddService(Service service)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Moderator/AddService", service);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Service>>>();
        }

        public async Task<ServiceResponse<List<Speciality>>> AddSpeciality(Speciality speciality)
        {
            var result = await _http.PostAsJsonAsync($"{apiConnection}/Moderator/AddSpeciality", speciality);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Speciality>>>();
        }

        public async Task<ServiceResponse<WebExecutor>> ApproveExecutor(long userId, long executorId)
        {
            var result = await _http.PutAsJsonAsync($"{apiConnection}/Moderator/ApproveExe/{userId}/{executorId}",0);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<WebExecutor>>();
        }

        public async Task<ServiceResponse<List<Competency>>> DeleteCompetency(long competencyId)
        {
            var result = await _http.DeleteAsync($"{apiConnection}/Moderator/DeleteCompetency/{competencyId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Competency>>>();
        }

        public async Task<ServiceResponse<List<Service>>> DeleteService(long serviceId)
        {
            var result = await _http.DeleteAsync($"{apiConnection}/Moderator/DeleteService/{serviceId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Service>>>();
        }

        public async Task<ServiceResponse<List<Speciality>>> DeleteSpeciality(long specialityId)
        {
            var result = await _http.DeleteAsync($"{apiConnection}/Moderator/DeleteSpeciality/{specialityId}");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<Speciality>>>();
        }

        public async Task<ServiceResponse<List<WebExecutor>>> GetExecutorsForApproval()
        {
            var result = await _http.GetAsync($"{apiConnection}/Moderator/GetExes");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<List<WebExecutor>>>();
        }

        public async Task<ServiceResponse<bool>> SaveCompetencyService(List<Service> data)
        {
            var result = await _http.PutAsJsonAsync($"{apiConnection}/Moderator/CompService", data);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}
