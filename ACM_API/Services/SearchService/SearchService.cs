using ACM_API.DB;
using ACM_API.Dtos.Customer;
using ACM_API.Dtos.Executor;
using ACM_API.Models;
using ACM_API.Models.Customer;
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

        public async Task<ServiceResponse<List<ConstructionDto>>> GetConstructionsForEx(long executorId)
        {
            var serviceResponse = new ServiceResponse<List<ConstructionDto>>();
            try
            {
                var executor = await _context.Executors
                    .Include(i=>i.Competency)
                    .ThenInclude(c=>c.Service)
                    .ThenInclude(s=>s.Constructions)
                    .ThenInclude(co=>co.Services)
                    .FirstAsync(i => i.Id == executorId);

                List<Construction> constructions = new List<Construction>();

                foreach(var c in executor.Competency){
                    foreach(var s in c.Service)
                    {
                        constructions.AddRange(s.Constructions);
                    }
                }

                List<ConstructionDto> constructionDtos = new List<ConstructionDto>();

                foreach (var c in constructions)
                {
                    var customer = await _context.Customers.FirstAsync(i => i.Constructions.Contains(c));
                    var dto = _mapper.Map<ConstructionDto>(c);
                    dto.CustomerId = customer.Id;
                    constructionDtos.Add(dto);
                }

                serviceResponse.Data = constructionDtos;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ExecutorDto>>> GetExesForConstructions(long constructionId)
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
