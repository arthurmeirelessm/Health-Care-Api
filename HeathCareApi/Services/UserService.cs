using HealthCareApi.Entities;
using HealthCareApi.Exceptions;
using HealthCareApi.Helpers;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

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

        private readonly DataContext _context;

        public UserService(DataContext dContext)
        {
            _context = dContext;
        }

        public async Task<User> Create(User user)
        {
            // Metodo equals() verifica se o que foi passado tem valores iguais, ou seja, o user.password e o user.ConfirmPassword
            if (!user.Password.Equals(user.ConfirmPassword))
            {
                throw new BadRequestException ("Password does not match confirmPassword");
            }
            User userDb = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserName == user.UserName);

            if (userDb != null)
            {
                throw new BadRequestException($"UserName {user.UserName} already exist.");
            }

            //HashPassword criptografa 
            user.Password = BC.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task Delete(int id)
        {
            User userDb = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (userDb == null)
            {
                throw new KeyNotFoundException($"User {id} not found");
               
            }
            _context.Users.Remove(userDb);
            await _context.SaveChangesAsync();

        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            User userDb = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (userDb == null)
            {
                throw new KeyNotFoundException($"User {id} not found");

            }
            return userDb;

        }

        public async Task Update(User userIn, int id)
        {

            if (userIn.Id != id)
            {
                throw new BadRequestException("Route Id is differs user id");
            } 
            else if (!userIn.Password.Equals(userIn.ConfirmPassword))
            {
              throw new BadRequestException("Password does not match confirmPassword");
            }

            User userDb = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);

            if (userDb == null)
            {
                throw new KeyNotFoundException($"User {id} not found");
            }
            else if (!BC.Verify(userIn.CurrentPassword, userDb.Password))
            {
                throw new BadRequestException("Incorret Password");
            }

            userIn.CreatedId = userDb.CreatedId;
            userIn.Password = BC.HashPassword(userIn.Password);
            _context.Entry(userIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
