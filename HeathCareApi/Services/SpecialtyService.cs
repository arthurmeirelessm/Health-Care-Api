using HealthCareApi.Entities;
using HealthCareApi.Helpers;

using Microsoft.EntityFrameworkCore;

namespace HealthCareApi.Services
{
    public interface ISpecialtyService
    {

        public Task<Specialty> Create(Specialty specialty);
        public Task<Specialty> GetById(int id);
        public Task<List<Specialty>> GetAll();
        public Task Update(Specialty specialtyIn, int id);
        public Task Delete(int id);


    }
    public class SpecialtyService : ISpecialtyService
    {

        private readonly DataContext _context;

        public SpecialtyService(DataContext dContext)
        {
            _context = dContext;
        }

        public async Task<Specialty> Create(Specialty specialty)
        {
            Specialty SpecialtyDb = await _context.Specialtys.AsNoTracking().SingleOrDefaultAsync(u => u.NameForSpecialty == specialty.NameForSpecialty);

            if (SpecialtyDb != null)
            {
                throw new Exception($"SpecialtyName {specialty.NameForSpecialty} already exist.");
            }
            _context.Specialtys.Add(specialty);
            await _context.SaveChangesAsync();

            return specialty;
        }

        public async Task Delete(int id)
        {
            Specialty specialtyDb = await _context.Specialtys.SingleOrDefaultAsync(u => u.Id == id);

            if (specialtyDb == null)
            {
                throw new Exception($"Specialty {id} not found");
               
            }
            _context.Specialtys.Remove(specialtyDb);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Specialty>> GetAll()
        {
            return await _context.Specialtys.ToListAsync();
        }

        public async Task<Specialty> GetById(int id)
        {
            Specialty specialtyDb = await _context.Specialtys.SingleOrDefaultAsync(u => u.Id == id);

            if (specialtyDb == null)
            {
                throw new Exception($"Specialty {id} not found");

            }
            return specialtyDb;

        }

        public async Task Update(Specialty specialtyIn, int id)
        {

            if (specialtyIn.Id != id)
            {
                throw new Exception("Route Id is differs Specialty id");
            } 

            Specialty specialtyDb = await _context.Specialtys.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);

            if (specialtyDb == null)
            {
                throw new Exception($"Specialty {id} not found");
            }

            specialtyIn.CreatedId = specialtyDb.CreatedId;

            _context.Entry(specialtyIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
