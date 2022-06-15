using ACM_API.DB;
using ACM_API.Dtos.Executor;
using ACM_API.Models;
using ACM_API.Models.Executor;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.ExecutorService
{
    public class ExecutorService : IExecutorService
    {

        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public ExecutorService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ExecutorDto>> AddExecutor(ExecutorDto newExecutor, long userId)
        {
            var serviceResponse = new ServiceResponse<ExecutorDto>();
            try
            {
                var executor = _mapper.Map<Executor>(newExecutor);
                executor.Competency = _context.Competencies.Where(i => newExecutor.Competency.Contains(i)).ToList();
                executor.Speciality = _context.Specialities.Where(i => newExecutor.Speciality.Contains(i)).ToList();
                executor.Approved = false;
                _context.Executors.Add(executor);
                var user = await _context.Users.FirstAsync(i => i.Id == userId);
                user.Executor = executor;
                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                
                //user.ExecutorId = executor.Id;
                //_context.Entry(user).State = EntityState.Modified;
                //await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<ExecutorDto>(executor);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<bool>> DeleteExecutor(long userId)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var user = await _context.Users.FindAsync(userId);
                var executor = await _context.Executors
                    .Include(i=>i.Speciality)
                    .Include(i=>i.Competency)
                    .Include(i=>i.Contacts)
                    .Include(i => i.Chats)
                    .ThenInclude(j => j.Messages)
                    .Include(i=>i.Orders)
                    .ThenInclude(j=>j.Chat)
                    .Include(i=>i.Orders)
                    .ThenInclude(j=>j.Files)
                    .FirstAsync(i=>i.User==user);

                foreach (var chat in executor.Chats)
                {
                    foreach(var message in chat.Messages)
                    {
                        _context.Entry(message).State = EntityState.Deleted;
                    }
                    _context.Entry(chat).State = EntityState.Deleted;
                }
                foreach(var contact in executor.Contacts)
                {
                    _context.Entry(contact).State = EntityState.Deleted;
                }

                foreach (var order in executor.Orders)
                {
                    foreach (var file in order.Files)
                    {
                        await DeleteFileAsync(file);
                        _context.Entry(file).State = EntityState.Deleted;
                    }
                    var chat = order.Chat;
                    if (chat != null)
                    {
                        if (chat.Messages != null)
                        {
                            foreach (var message in chat.Messages)
                            {
                                _context.Entry(message).State = EntityState.Deleted;
                            }
                        }
                        _context.Entry(chat).State = EntityState.Deleted;
                    }                
                    
                    _context.Entry(order).State = EntityState.Deleted;
                }

                user.Executor = null;
                _context.Executors.Remove(executor);

                await _context.SaveChangesAsync();

                serviceResponse.Data = true;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<long>> ExistExecutor(long userId)
        {
            var serviceResponse = new ServiceResponse<long>();
            var user = await _context.Users.FirstAsync(i => i.Id == userId);
            serviceResponse.Data = user.ExecutorId ?? 0;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ExecutorDto>>> GetAllExecutors()
        {
            var serviceResponse = new ServiceResponse<List<ExecutorDto>>();
            var dBData = await _context.Executors
                .Include(i => i.Competency)
                .Include(i => i.Contacts)
                .Include(i => i.Speciality)
                .ToListAsync();
            serviceResponse.Data = dBData.Select(i => _mapper.Map<ExecutorDto>(i)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CompetencyDto>>> GetCompetencyList()
        {
            var serviceResponse = new ServiceResponse<List<CompetencyDto>>();
            var dBData = await _context.Competencies.ToListAsync();
            serviceResponse.Data = dBData.Select(i => _mapper.Map<CompetencyDto>(i)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<ExecutorDto>> GetExecutorById(long id)
        {
            var serviceResponse = new ServiceResponse<ExecutorDto>();
            var executor = await _context.Executors
                .Include(i=>i.User)
                .Include(i => i.Competency)
                .Include(i => i.Contacts)
                .Include(i => i.Speciality)
                .FirstAsync(i=>i.Id==id);
            var data = _mapper.Map<ExecutorDto>(executor);
            data.UserId = executor.User.Id;
            serviceResponse.Data = data;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<SpecialityDto>>> GetSpecialityList()
        {
            var serviceResponse = new ServiceResponse<List<SpecialityDto>>();
            var dBData = await _context.Specialities.ToListAsync();
            serviceResponse.Data = dBData.Select(i => _mapper.Map<SpecialityDto>(i)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> IsExecutorApproved(long executorId)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var executor = await _context.Executors
                    .FirstOrDefaultAsync(i => i.Id == executorId);

                if (executor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого исполнителя";
                    return serviceResponse;
                }


                serviceResponse.Data = executor.Approved;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<ExecutorDto>> UpdateExecutor(ExecutorDto updatedExecutor)
        {
            var serviceResponse = new ServiceResponse<ExecutorDto>();
            try
            {
                var dBExecutor = await _context.Executors
                    .Include(i => i.Speciality)
                    .Include(i => i.Competency)
                    .ThenInclude(c=>c.Executor)
                    .Include(i => i.Contacts)
                    .FirstOrDefaultAsync(i => i.Id == updatedExecutor.Id);

                if (dBExecutor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого исполнителя";
                    return serviceResponse;
                }

                dBExecutor.Competency = _context.Competencies.Where(i => updatedExecutor.Competency.Contains(i)).ToList();
                dBExecutor.Speciality = _context.Specialities.Where(i => updatedExecutor.Speciality.Contains(i)).ToList();

                dBExecutor.Name = updatedExecutor.Name;
                dBExecutor.FullName = updatedExecutor.FullName;
                dBExecutor.INN = updatedExecutor.INN;
                dBExecutor.Contacts = updatedExecutor.Contacts;
                dBExecutor.Approved = false;

                _context.Entry(dBExecutor).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<ExecutorDto>(await _context.Executors.FindAsync(updatedExecutor.Id));
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
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
