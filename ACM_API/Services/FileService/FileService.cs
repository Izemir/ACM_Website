using ACM_API.DB;
using ACM_API.Dtos;
using ACM_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(DBContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ServiceResponse<List<UserFileDto>>> AddFileToExecutor(long executorId, UserFileDto file)
        {
            var serviceResponse = new ServiceResponse<List<UserFileDto>>();
            try
            {              

                var executor = await _context.Executors
                    .Include(i => i.Files)
                    .FirstOrDefaultAsync(i => i.Id == executorId);

                if(executor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого исполнителя";
                    return serviceResponse;
                }

                if (executor.Files.FirstOrDefault(i => i.FileName == file.FileName) != null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Уже существует файл с таким именем";
                    return serviceResponse;
                }

                var path = await UploadAsync(executor.Id, file);
                if(path == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Ошибка доступа к файловой системе";
                    return serviceResponse;
                }
                file.Path = path;

                var newFile = _mapper.Map<UserFile>(file);

                executor.Files.Add(newFile);

                _context.Entry(executor).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                serviceResponse.Data = executor.Files.Select(i => _mapper.Map<UserFileDto>(i)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserFileDto>>> DeleteFileOfExecutor(long executorId, long fileId)
        {
            var serviceResponse = new ServiceResponse<List<UserFileDto>>();
            try
            {
                var executor = await _context.Executors
                    .Include(i => i.Files)
                    .FirstOrDefaultAsync(i => i.Id == executorId);

                if (executor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого исполнителя";
                    return serviceResponse;
                }

                var file = executor.Files.FirstOrDefault(i => i.Id == fileId);

                if (file == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого файла";
                    return serviceResponse;
                }

                var delete = await DeleteFileAsync(file);

                if (!delete)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Не удалось удалить файл";
                    return serviceResponse;
                }

                _context.Entry(file).State = EntityState.Deleted;

                await _context.SaveChangesAsync();

                serviceResponse.Data = executor.Files.Select(i => _mapper.Map<UserFileDto>(i)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserFileDto>>> GetExecutorFiles(long executorId)
        {
            var serviceResponse = new ServiceResponse<List<UserFileDto>>();
            try
            {
                var executor = await _context.Executors
                    .Include(i => i.Files)
                    .FirstOrDefaultAsync(i => i.Id == executorId);

                if (executor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого исполнителя";
                    return serviceResponse;
                }

                List<UserFileDto> fileDtos = new List<UserFileDto>();
                if (executor.Files.Count > 0)
                {
                    fileDtos = executor.Files.Select(i => _mapper.Map<UserFileDto>(i)).ToList();
                }            

                serviceResponse.Data = fileDtos;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserFileDto>> GetFile(long fileId)
        {
            var serviceResponse = new ServiceResponse<UserFileDto>();
            try
            {
                var file = await _context.Files
                    .FirstOrDefaultAsync(i => i.Id == fileId);

                if (file == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого файла";
                    return serviceResponse;
                }

                var fileDto = _mapper.Map<UserFileDto>(file);

                using(var fileInput = new FileStream(file.Path, FileMode.Open, FileAccess.Read))
                {
                    MemoryStream memoryStream = new MemoryStream();
                    await fileInput.CopyToAsync(memoryStream);

                    var buffer = memoryStream.ToArray();

                    fileDto.FileContent = buffer;
                }

                serviceResponse.Data = fileDto;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private async Task<string> UploadAsync(long executorId,UserFileDto file)
        {
            var directory = Path.Combine(_webHostEnvironment.ContentRootPath, "Files\\Executor", executorId.ToString());

            try
            {
                if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            }
            catch (Exception ex)
            {
                return null;
            }

            var path = Path.Combine(directory, file.FileName);
            await using var fs = new FileStream(path, FileMode.Create);
            fs.Write(file.FileContent, 0, file.FileContent.Length);
            return path;
        }

        private async Task<bool> DeleteFileAsync(UserFile file)
        {
            try
            {
                if (File.Exists(file.Path))
                {
                    File.Delete(file.Path);
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
