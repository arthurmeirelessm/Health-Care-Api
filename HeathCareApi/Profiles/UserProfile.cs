using AutoMapper;
using HealthCareApi.Dto.User;
using HealthCareApi.Entities;

namespace HealthCareApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRequest>();
            CreateMap<User, UserRequestUpdate>();
            CreateMap<User, UserResponse>();

            CreateMap<UserRequest, User>();
            CreateMap<UserRequestUpdate, User>();
            CreateMap<UserResponse, User>();
        }
    }
}
