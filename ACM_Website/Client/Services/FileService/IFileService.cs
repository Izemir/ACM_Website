using ACM_Website.Shared;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_Website.Client.Services.FileService
{
    public interface IFileService
    {
        Task<ServiceResponse<List<UserFile>>> AddFileToExecutor(long executorId, UserFile file);

        Task<ServiceResponse<UserFile>> GetFile(long fileId);

        Task<ServiceResponse<List<UserFile>>> GetExecutorFiles(long executorId);

        Task<ServiceResponse<List<UserFile>>> DeleteFileOfExecutor(long executorId, long fileId);
    }
}
