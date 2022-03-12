using HealthCareApi.Entities;
using HealthCareApi.Helpers;

using Microsoft.EntityFrameworkCore;

namespace HealthCareApi.Services
{
    public interface INoteForMedicalCareService
    {

        public Task<NoteForMedicalCare> Create(NoteForMedicalCare note);
        public Task<NoteForMedicalCare> GetById(int id);
        public Task<List<NoteForMedicalCare>> GetAll();
        public Task Update(NoteForMedicalCare noteIn, int id);
        public Task Delete(int id);


    }
    public class NoteForMedicalCareService : INoteForMedicalCareService
    {

        private readonly DataContext _context;

        public NoteForMedicalCareService(DataContext dContext)
        {
            _context = dContext;
        }

        public async Task<NoteForMedicalCare> Create(NoteForMedicalCare note)
        {
            
            _context.NoteForMedicalCares.Add(note);
            await _context.SaveChangesAsync();

            return note;
        }

        public async Task Delete(int id)
        {
            NoteForMedicalCare noteForMedicalCareDb = await _context.NoteForMedicalCares.SingleOrDefaultAsync(u => u.Id == id);

            if (noteForMedicalCareDb == null)
            {
                throw new Exception($"NoteForMedicalCare {id} not found");
               
            }
            _context.NoteForMedicalCares.Remove(noteForMedicalCareDb);
            await _context.SaveChangesAsync();

        }

        public async Task<List<NoteForMedicalCare>> GetAll()
        {
            return await _context.NoteForMedicalCares.ToListAsync();
        }

        public async Task<NoteForMedicalCare> GetById(int id)
        {
            NoteForMedicalCare noteForMedicalCareDb = await _context.NoteForMedicalCares.SingleOrDefaultAsync(u => u.Id == id);

            if (noteForMedicalCareDb == null)
            {
                throw new Exception($"NoteForMedicalCare {id} not found");

            }
            return noteForMedicalCareDb;

        }

        public async Task Update(NoteForMedicalCare noteIn, int id)
        {

            if (noteIn.Id != id)
            {
                throw new Exception("Route Id is differs NoteForMedicalCare id");
            } 

            NoteForMedicalCare noteForMedicalCareDb = await _context.NoteForMedicalCares.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);

            if (noteForMedicalCareDb == null)
            {
                throw new Exception($"NoteForMedicalCare {id} not found");
            }

            _context.Entry(noteIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
