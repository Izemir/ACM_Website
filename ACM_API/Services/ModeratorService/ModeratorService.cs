
using ACM_API.DB;
using ACM_API.Dtos;
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

                    //_context.Entry(service).State = EntityState.Modified;



                    //var service = _mapper.Map<Service>(s);
                    //service.Competency = s.Competencies.Select(i => _mapper.Map<Competency>(i)).ToList();
                    //List<Competency> competencies = new List<Competency>();
                    //foreach(var c in service.Competency)
                    //{
                    //    var oldComp = _context.Competencies.First(i => i.Id == c.Id);
                    //    if (oldComp != null)
                    //    {
                    //        competencies.Add(oldComp);
                    //        //_context.Entry(oldComp).State = EntityState.Modified;
                    //    }
                    //    else competencies.Add(c);                        
                    //}
                    //service.Competency = competencies;

                    //var oldService = _context.Services.First(i => i.Id == service.Id);
                    //if (oldService == null)
                    //{
                    //    _context.Services.Add(service);
                    //}
                    //else
                    //{
                    //    oldService.Competency = service.Competency;
                    //    _context.Entry(oldService).State = EntityState.Modified;
                    //}

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
