using ACM_API.Dtos;
using ACM_API.Models;
using System.Threading.Tasks;

namespace ACM_API.Services.FileService
{
    public class FileService : IFileService
    {
        public Task<ServiceResponse<bool>> AddFileToExecutor(long executorId, UserFileDto file)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<UserFileDto>> GetFile(string link)
        {
            throw new System.NotImplementedException();
        }
    }
}
