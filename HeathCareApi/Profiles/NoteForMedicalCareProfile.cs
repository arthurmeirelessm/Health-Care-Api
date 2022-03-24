using AutoMapper;
using HealthCareApi.Dto.Note;
using HealthCareApi.Dto.User;
using HealthCareApi.Entities;

namespace HealthCareApi.Profiles
{
    public class NoteForMedicalCareProfile : Profile
    {
        public NoteForMedicalCareProfile()
        {
            CreateMap<NoteForMedicalCare, NoteForMedicalCareRequest>();
            CreateMap<NoteForMedicalCare, NoteForMedicalCareResponse>();
        }
    }
}
