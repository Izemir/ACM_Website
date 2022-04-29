using ACM_API.DB;
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
                var comps = newExecutor.Competency.Select(i => i.Id).ToList();
                //var specs = newExecutor.Speciality.Select(i => i.Id).ToList();
                executor.Competency.Clear();
                executor.Speciality.Clear();
                //executor.Competency.Add(await _context.Competencies.FirstAsync(i => comps.Contains(i.Id)));
               // executor.Competency = _context.Competencies.Where(i => comps.Contains(i.Id)).ToList();
                // executor.Speciality = _context.Specialities.Where(i => specs.Contains(i.Id)).ToList();
                //var comps = _context.Competencies.Where(i => newExecutor.Competency.Contains(i)).ToList();
                //var specs = _context.Specialities.Where(i => newExecutor.Speciality.Contains(i)).ToList();
                //foreach (var spec in specs)
                //{
                //    executor.Speciality.Add(spec);
                //}
                //executor.Competency.Clear();
                //executor.Speciality.Clear();
                
                //executor.Speciality.AddRange(specs);
                //executor.Competency.AddRange(comps);
                _context.Executors.Add(executor);
                
                await _context.SaveChangesAsync();

                //executor.Competency.Add(await _context.Competencies.FirstAsync(i => comps.Contains(i.Id)));
                //    await _context.SaveChangesAsync();

                var comp = await _context.Competencies.FirstAsync(i => comps.Contains(i.Id));
                executor.Competency.Add(comp);
                await _context.SaveChangesAsync();


                //executor.Speciality = specs;
                //executor.Competency = comps;

                //_context.Entry(executor).State = EntityState.Modified;
                //_context.SaveChanges();

                var user = await _context.Users.FirstAsync(i => i.Id == userId);
                user.ExecutorId = executor.Id;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<ExecutorDto>(executor);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<ExecutorDto>>> DeleteExecutor(long id)
        {
            var serviceResponse = new ServiceResponse<List<ExecutorDto>>();
            try
            {
                var dBExecutor = await _context.Executors.FindAsync(id);

                _context.Executors.Remove(dBExecutor);
                await _context.SaveChangesAsync();

                serviceResponse.Data = (await _context.Executors.ToListAsync()).Select(i => _mapper.Map<ExecutorDto>(i)).ToList();

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
            serviceResponse.Data = _mapper.Map<ExecutorDto>(await _context.Executors.FindAsync(id));
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
