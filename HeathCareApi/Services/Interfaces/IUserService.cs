using HealthCareApi.Dto.User;

namespace HealthCareApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
        public Task<UserResponse> Create(UserRequest userRequest);
        public Task<UserResponse> GetById(int id);
        public Task<List<UserResponse>> GetAll();
        public Task Update(UserRequestUpdate userRequest, int id);
        public Task Delete(int id);

    }
}
