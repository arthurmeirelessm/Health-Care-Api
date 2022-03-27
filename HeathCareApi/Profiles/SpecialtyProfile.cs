using AutoMapper;
using HealthCareApi.Dto.Specialty;
using HealthCareApi.Entities;

namespace HealthCareApi.Profiles
{
    public class SpecialtyProfile : Profile
    {
        public SpecialtyProfile()
        {
            CreateMap<Specialty, SpecialtyRequest>();
            CreateMap<Specialty, SpecialtyResponse>();

            CreateMap<SpecialtyRequest, Specialty>();
            CreateMap<SpecialtyResponse, Specialty>();

        }
    }
}
