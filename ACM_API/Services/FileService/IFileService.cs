using ACM_API.Dtos;
using ACM_API.Models;
using System.Threading.Tasks;

namespace ACM_API.Services.FileService
{
    public interface IFileService
    {
        Task<ServiceResponse<bool>> AddFileToExecutor(long executorId,UserFileDto file);

        Task<ServiceResponse<UserFileDto>> GetFile(string link);
    }
}
