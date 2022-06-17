
using ACM_API.DB;
using ACM_API.Dtos;
using ACM_API.Dtos.Executor;
using ACM_API.Helpers;
using ACM_API.Models;
using ACM_API.Models.Executor;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACM_API.Services.ModeratorService
{
    public class ModeratorService : IModeratorService
    {

        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public ModeratorService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<SpecialityDto>>> AddSpeciality(SpecialityDto speciality)
        {
            var serviceResponse = new ServiceResponse<List<SpecialityDto>>();
            try
            {
                if (speciality.Id == 0) _context.Specialities.Add(_mapper.Map<Speciality>(speciality));
                await _context.SaveChangesAsync();

                var dBData = await _context.Specialities.ToListAsync();
                serviceResponse.Data = dBData.Select(i => _mapper.Map<SpecialityDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<SpecialityDto>>> DeleteSpeciality(long specialityId)
        {
            var serviceResponse = new ServiceResponse<List<SpecialityDto>>();
            try
            {
                var spec = await _context.Specialities
                    .FirstOrDefaultAsync(i => i.Id == specialityId);
                if (spec == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такой специальности";
                    return serviceResponse;
                }

                _context.Entry(spec).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                var dBData = await _context.Specialities.ToListAsync();
                serviceResponse.Data = dBData.Select(i => _mapper.Map<SpecialityDto>(i)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CompetencyDto>>> AddCompetency(CompetencyDto competency)
        {
            var serviceResponse = new ServiceResponse<List<CompetencyDto>>();
            try
            {
                if (competency.Id == 0) _context.Competencies.Add(_mapper.Map<Competency>(competency));
                await _context.SaveChangesAsync();

                var dBData = await _context.Competencies.ToListAsync();
                serviceResponse.Data = dBData.Select(i => _mapper.Map<CompetencyDto>(i)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CompetencyDto>>> DeleteCompetency(long сompetencyId)
        {
            var serviceResponse = new ServiceResponse<List<CompetencyDto>>();
            try
            {
                var comp = await _context.Competencies
                    .Include(i=>i.Service)
                    .FirstOrDefaultAsync(i => i.Id == сompetencyId);
                if (comp == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого навыка";
                    return serviceResponse;
                }
                if (comp.Service!=null) 
                {
                    if (comp.Service.Count > 0)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Есть привязки к услугам, удаление невозможно";
                        return serviceResponse;
                    }
                }

                _context.Entry(comp).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                var dBData = await _context.Competencies.ToListAsync();
                serviceResponse.Data = dBData.Select(i => _mapper.Map<CompetencyDto>(i)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ServiceDto>>> AddService(ServiceDto service)
        {
            var serviceResponse = new ServiceResponse<List<ServiceDto>>();
            try
            {
                if (service.Id == 0) _context.Services.Add(_mapper.Map<Service>(service));
                await _context.SaveChangesAsync();

                var dBData = await _context.Services.ToListAsync();
                serviceResponse.Data = dBData.Select(i => _mapper.Map<ServiceDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ServiceDto>>> DeleteService(long serviceId)
        {
            var serviceResponse = new ServiceResponse<List<ServiceDto>>();
            try
            {
                var service = await _context.Services
                    .Include(i => i.Competency)
                    .FirstOrDefaultAsync(i => i.Id == serviceId);
                if (service == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такой услуги";
                    return serviceResponse;
                }
                if (service.Competency!=null)
                {
                    if (service.Competency.Count() > 0)
                    {
                        serviceResponse.Success = false;
                        serviceResponse.Message = "Есть привязки к навыкам, удаление невозможно";
                        return serviceResponse;
                    }
                }

                _context.Entry(service).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                var dBData = await _context.Services.ToListAsync();
                serviceResponse.Data = dBData.Select(i => _mapper.Map<ServiceDto>(i)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ExecutorDto>>> ApproveExecutor(long userId, long executorId)
        {
            var serviceResponse = new ServiceResponse<List<ExecutorDto>>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);
                if (user == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого пользователя";
                    return serviceResponse;
                }
                if (user.Role != UserRoles.Администратор.ToString())
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет прав для изменения";
                    return serviceResponse;
                }

                var executor = await _context.Executors
                .Include(i => i.User)
                .Include(i => i.Competency)
                .Include(i => i.Contacts)
                .Include(i => i.Speciality)
                .FirstAsync(i => i.Id == executorId);

                if (executor == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Нет такого исполнителя";
                    return serviceResponse;
                }

                executor.Approved = true;
                _context.Entry(executor).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var executors = _context.Executors
                    .Include(i => i.Contacts)
                    .Where(i => i.Approved != true);


                serviceResponse.Data = executors.Select(i => _mapper.Map<ExecutorDto>(i)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ExecutorDto>>> GetExecutorsForApproval()
        {
            var serviceResponse = new ServiceResponse<List<ExecutorDto>>();
            try
            {
                var executors = _context.Executors
                    .Include(i=>i.Contacts)
                    .Where(i => i.Approved != true);


                serviceResponse.Data = executors.Select(i => _mapper.Map<ExecutorDto>(i)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> SaveCompService(List<ServiceDto> newServices)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
               foreach(var s in newServices)
                {
                    Service service = new Service();
                    if (_context.Services.Find(s.Id) != null)
                    {
                        service = _context.Services
                        .Include(i => i.Competency)
                        .First(i => i.Id == s.Id);
                    }
                    else
                    {
                        service = _mapper.Map<Service>(s);
                        _context.Services.Add(service);
                    }                    

                    var competencies = s.Competencies.Select(i => _mapper.Map<Competency>(i)).ToList();

                    service.Competency = _context.Competencies.Where(i => competencies.Contains(i)).ToList();

                    foreach (var c in competencies)
                    {
                        if (c.Id==0) service.Competency.Add(new Competency() { CompetencyName=c.CompetencyName});
                    }


                    await _context.SaveChangesAsync();
                }               

                serviceResponse.Data = true;

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
