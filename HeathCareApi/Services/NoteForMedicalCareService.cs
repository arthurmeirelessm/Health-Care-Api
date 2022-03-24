using AutoMapper;
using HealthCareApi.Dto.Note;
using HealthCareApi.Dto.User;
using HealthCareApi.Entities;
using HealthCareApi.Exceptions;
using HealthCareApi.Helpers;

using Microsoft.EntityFrameworkCore;

namespace HealthCareApi.Services
{
    public interface INoteForMedicalCareService
    {

        public Task<NoteForMedicalCareResponse> Create(NoteForMedicalCareRequest noteRequest);
        public Task<NoteForMedicalCareResponse> GetById(int id);
        public Task<List<NoteForMedicalCareResponse>> GetAll();
        public Task Update(NoteForMedicalCareRequest noteRequest, int id);
        public Task Delete(int id);


    }
    public class NoteForMedicalCareService : INoteForMedicalCareService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public NoteForMedicalCareService(DataContext dContext, IMapper mapper)
        {
            _context = dContext;
            _mapper = mapper;   
        }

        public async Task<NoteForMedicalCareResponse> Create(NoteForMedicalCareRequest noteRequest)
        {
            
           NoteForMedicalCare note = _mapper.Map<NoteForMedicalCare>(noteRequest);

            _context.NoteForMedicalCares.Add(note);
            await _context.SaveChangesAsync();

            return _mapper.Map<NoteForMedicalCareResponse>(note);
        }

        public async Task Delete(int id)
        {
            NoteForMedicalCare noteForMedicalCareDb = await _context.NoteForMedicalCares.SingleOrDefaultAsync(u => u.Id == id);

            if (noteForMedicalCareDb == null)
            {
                throw new KeyNotFoundException($"NoteForMedicalCare {id} not found");
               
            }
            _context.NoteForMedicalCares.Remove(noteForMedicalCareDb);
            await _context.SaveChangesAsync();

        }

        public async Task<List<NoteForMedicalCareResponse>> GetAll()
        {
            List<NoteForMedicalCare> notes =  await _context.NoteForMedicalCares.ToListAsync();
            return notes.Select(n => _mapper.Map<NoteForMedicalCareResponse>(n)).ToList();
        }

        public async Task<NoteForMedicalCareResponse> GetById(int id)
        {
            NoteForMedicalCare noteForMedicalCareDb = await _context.NoteForMedicalCares.SingleOrDefaultAsync(u => u.Id == id);

            if (noteForMedicalCareDb == null)
            {
                throw new KeyNotFoundException($"NoteForMedicalCare {id} not found");

            }
            return _mapper.Map<NoteForMedicalCareResponse>(noteForMedicalCareDb);   

        }

        public async Task Update(NoteForMedicalCareRequest noteRequest, int id)
        {

            if (noteRequest.Id != id)
            {
                throw new BadRequestException("Route Id is differs NoteForMedicalCare id");
            } 

            NoteForMedicalCare noteForMedicalCareDb = await _context.NoteForMedicalCares.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);

            if (noteForMedicalCareDb == null)
            {
                throw new KeyNotFoundException($"NoteForMedicalCare {id} not found");
            }
            // mapper converte o response do método, convervete de entidade para N
            noteForMedicalCareDb = _mapper.Map<NoteForMedicalCare>(noteRequest);

            _context.Entry(noteForMedicalCareDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
