using ACM_API.Dtos;
using ACM_API.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACM_API.Services.FileService
{
    public interface IFileService
    {
        Task<ServiceResponse<List<UserFileDto>>> AddFileToExecutor(long executorId,UserFileDto file);

        Task<ServiceResponse<UserFileDto>> GetFile(long fileId);

        Task<ServiceResponse<List<UserFileDto>>> GetExecutorFiles(long executorId);

        Task<ServiceResponse<List<UserFileDto>>> DeleteFileOfExecutor(long executorId, long fileId);

        Task<ServiceResponse<List<UserFileDto>>> AddFileToOrder(long orderId, UserFileDto file);


        Task<ServiceResponse<List<UserFileDto>>> GetOrderFiles(long orderId);

        Task<ServiceResponse<List<UserFileDto>>> DeleteFileOfOrder(long orderId, long fileId);
    }
}
