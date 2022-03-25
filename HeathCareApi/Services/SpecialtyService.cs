using AutoMapper;
using HealthCareApi.Dto.Specialty;
using HealthCareApi.Entities;
using HealthCareApi.Exceptions;
using HealthCareApi.Helpers;

using Microsoft.EntityFrameworkCore;

namespace HealthCareApi.Services
{
    public interface ISpecialtyService
    {

        public Task<SpecialtyResponse> Create(SpecialtyRequest specialtyRequest);
        public Task<SpecialtyResponse> GetById(int id);
        public Task<List<SpecialtyResponse>> GetAll();
        public Task Update(SpecialtyRequest specialtyRequest, int id);
        public Task Delete(int id);


    }
    public class SpecialtyService : ISpecialtyService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SpecialtyService(DataContext dContext, IMapper mapper)
        {
            _context = dContext;
            _mapper = mapper;
        }

        public async Task<SpecialtyResponse> Create(SpecialtyRequest specialtyRequest)
        {
            Specialty specialty = _mapper.Map<Specialty>(specialtyRequest);

            _context.Specialtys.Add(specialty);
            await _context.SaveChangesAsync();

            return _mapper.Map<SpecialtyResponse>(specialty);
        }

        public async Task Delete(int id)
        {
            Specialty specialtyDb = await _context.Specialtys.SingleOrDefaultAsync(u => u.Id == id);

            if (specialtyDb == null)
            {
                throw new KeyNotFoundException($"Specialty {id} not found");
               
            }
            _context.Specialtys.Remove(specialtyDb);
            await _context.SaveChangesAsync();

        }

        public async Task<List<SpecialtyResponse>> GetAll()
        {   
            List<Specialty> specialtys = await _context.Specialtys.ToListAsync();

            return specialtys.Select(x => _mapper.Map<SpecialtyResponse>(x)).ToList();
        }

        public async Task<SpecialtyResponse> GetById(int id)
        {
            Specialty specialtyDb = await _context.Specialtys.SingleOrDefaultAsync(u => u.Id == id);

            if (specialtyDb == null)
            {
                throw new KeyNotFoundException($"Specialty {id} not found");

            }
            return _mapper.Map<SpecialtyResponse>(specialtyDb); ;

        }

        public async Task Update(SpecialtyRequest specialtyIn, int id)
        {

            if (specialtyIn.Id != id)
            {
                throw new BadRequestException("Route Id is differs Specialty id");
            } 

            Specialty specialtyDb = await _context.Specialtys.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);

            if (specialtyDb == null)
            {
                throw new KeyNotFoundException($"Specialty {id} not found");
            }
            
            specialtyDb = _mapper.Map<Specialty>(specialtyIn);

            _context.Entry(specialtyIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
