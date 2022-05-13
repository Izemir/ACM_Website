﻿using ACM_API.DB;
using ACM_API.Dtos.Executor;
using ACM_API.Models;
using ACM_API.Models.Executor;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                    .FirstAsync(i=>i.User==user);

                
                _context.Executors.Remove(executor);
                //user.ExecutorId = null;

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

        public async Task<ServiceResponse<ExecutorDto>> UpdateExecutor(ExecutorDto updatedExecutor)
        {
            var serviceResponse = new ServiceResponse<ExecutorDto>();
            try
            {
                var dBExecutor = await _context.Executors.FindAsync(updatedExecutor.Id);

                dBExecutor.Name = updatedExecutor.Name;
                dBExecutor.Speciality = updatedExecutor.Speciality;

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
    }
}
