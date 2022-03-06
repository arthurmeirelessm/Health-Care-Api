using HealthCareApi.Entities;
using HealthCareApi.Helpers;

using Microsoft.EntityFrameworkCore;

namespace HealthCareApi.Services
{
    public interface IUserService
    {

        public Task<User> Create(User user);
        public Task<User> GetById(int id);
        public Task<List<User>> GetAll();
        public Task Update(User userIn, int id);
        public Task Delete(int id);


    }
    public class UserService : IUserService
    {

        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> Create(User user)
        {
            User userDb = await _dataContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserName == user.UserName);

            if (userDb is not null)
            {
                throw new Exception($"UserName {user.UserName} already exist.");
            }
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        public async Task Delete(int id)
        {
            User userDb = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is not null)
            {
                throw new Exception($"User {id} not found");
               
            }
            _dataContext.Users.Remove(userDb);
            await _dataContext.SaveChangesAsync();

        }

        public async Task<List<User>> GetAll()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            User userDb = await _dataContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
            {
                throw new Exception($"User {id} not found");

            }
            return userDb;

        }

        public async Task Update(User userIn, int id)
        {

            if (userIn.Id != id)
            {
                throw new Exception("Route Id is differs user id");
            } 

            User userDb = await _dataContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
            {
                throw new Exception($"User {id} not found");
            }

            _dataContext.Entry(userIn).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
    }
}
