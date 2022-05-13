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

namespace ACM_API.Services.SearchService
{
    public class SearchService : ISearchService
    {

        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public SearchService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<ExecutorDto>>> GetConstructionExes(long constructionId)
        {
            var serviceResponse = new ServiceResponse<List<ExecutorDto>>();
            try
            {
                var construction = await _context.Constructions
                    .Include(i => i.Services)
                    .FirstAsync(i => i.Id == constructionId);

                List<Competency> competencies = new List<Competency>();

                foreach(var s in construction.Services)
                {
                    competencies.AddRange( _context.Competencies
                        .Include(c => c.Executor)
                        .ThenInclude(e=>e.Contacts)
                        .Where(c => c.Service.Contains(s))
                        .ToList());
                }

                List<Executor> executors = new List<Executor>();

                foreach(var c in competencies)
                {
                    foreach(var e in c.Executor)
                    {
                        if (!executors.Contains(e))
                        {
                            executors.Add(e);
                        }
                    }
                }

                serviceResponse.Data = executors.Select(i => _mapper.Map<ExecutorDto>(i)).ToList();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
