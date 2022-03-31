using AutoMapper;
using HealthCareApi.Dto.User;
using HealthCareApi.Entities;
using HealthCareApi.Exceptions;
using HealthCareApi.Helpers;
using HealthCareApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace HealthCareApi.Services
{
   
    public class UserService : IUserService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public UserService(DataContext dContext, IMapper mapper, JwtService jwtService)
        {
            _context = dContext;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {

            var userDb = await _context.Users.SingleOrDefaultAsync(u => u.UserName == request.UserName);

            if (userDb == null)
            {
                throw new KeyNotFoundException($"User {request.UserName} not found");
            }
            else if (!BC.Verify(request.Password, userDb.Password))
            {
                throw new BadRequestException("Incorret Password");
            }

            string token = _jwtService.GenerateToken(userDb);
            return new AuthenticateResponse(userDb, token);
        }


        public async Task<UserResponse> Create(UserRequest userRequest)
        {
            // Metodo equals() verifica se o que foi passado tem valores iguais, ou seja, o user.password e o user.ConfirmPassword
            if (!userRequest.Password.Equals(userRequest.ConfirmPassword))
            {
                throw new BadRequestException ("Password does not match confirmPassword");
            }
            var userDb = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserName == userRequest.UserName);

            if (userDb != null)
            {
                throw new BadRequestException($"UserName {userRequest.UserName} already exist.");
            }

            User user = _mapper.Map<User>(userRequest);

            // O metodo Any() passado abaixo representa um validação de que o valor lá nos testes do postman da propriedade "SpecialtiesActivedIds" não foi passada como nula 
            if (user.TypeUser != Enuns.TypeUser.Patient && userRequest.SpecialtiesActivedIds.Any())
            {
                throw new BadRequestException("Only patients can have a history with specialties");
             }

               user.SpecialtiesActived = new List<Specialty>();
               List<Specialty> specialties = await _context.Specialtys.Where(e => userRequest.SpecialtiesActivedIds.Contains(e.Id)).ToListAsync();
               foreach (Specialty specialty in specialties)
               {
                    user.SpecialtiesActived.Add(specialty);
               }
 

            //HashPassword criptografa 
            userRequest.Password = BC.HashPassword(userRequest.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponse>(user);
        }

        public async Task Delete(int id)
        {
            var userDb = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (userDb == null)
            {
                throw new KeyNotFoundException($"User {id} not found");
               
            }
            _context.Users.Remove(userDb);
            await _context.SaveChangesAsync();

        }

        public async Task<List<UserResponse>> GetAll()
        {
            List<User> users = await _context.Users.ToListAsync();
            return users.Select(e => _mapper.Map<UserResponse>(e)).ToList();
        }

        public async Task<UserResponse> GetById(int id)
        {
            var userDb = await _context.Users
                .Include(a => a.SpecialtiesActived) // Patient
                .Include(a => a.SpecialtiesDoctorChiefing) //Doctor
                .SingleOrDefaultAsync(u => u.Id == id);

            if (userDb == null)
            {
                throw new KeyNotFoundException($"User {id} not found");

            }
            return _mapper.Map<UserResponse>(userDb);

        }

        public async Task Update(UserRequestUpdate userRequest, int id)
        {

            if (userRequest.Id != id)
            {
                throw new BadRequestException("Route Id is differs user id");
            } 
            else if (!userRequest.Password.Equals(userRequest.ConfirmPassword))
            {
              throw new BadRequestException("Password does not match confirmPassword");
            }

            var userDb = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);

            if (userDb == null)
            {
                throw new KeyNotFoundException($"User {id} not found");
            }
            else if (!BC.Verify(userRequest.CurrentPassword, userDb.Password))
            {
                throw new BadRequestException("Incorret Password");
            }

            userDb = _mapper.Map<User>(userRequest); // talvez o createdId possa ser default

            await AddOrRemoveSpecialty(userDb, userRequest.SpecialtiesActivedIds);

            userDb.Password = BC.HashPassword(userRequest.Password);
            _context.Entry(userDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        private async Task AddOrRemoveSpecialty(User userDb, int[] specialtiesIds)
        {
            int[] removedIds = userDb.SpecialtiesActived.Where(e => !specialtiesIds.Contains(e.Id)).Select(e => e.Id).ToArray();
            int[] addedIds = specialtiesIds.Where(e => !userDb.SpecialtiesActived.Select(u => u.Id).ToArray().Contains(e)).ToArray();

            if (!removedIds.Any() && !addedIds.Any())
            {
                _context.Entry(userDb).State = EntityState.Detached;
                return;
            }

            List<Specialty> tempSpecialty = await _context.Specialtys.Where(e => removedIds.Contains(e.Id) || addedIds.Contains(e.Id)).ToListAsync();


            List<Specialty> specialtyToBeRemoved = tempSpecialty.Where(c => removedIds.Contains(c.Id)).ToList();
            foreach (Specialty specialty in specialtyToBeRemoved)
            {
                userDb.SpecialtiesActived.Remove(specialty);
            }

            List<Specialty> specialtyToBeAdded = tempSpecialty.Where(c => addedIds.Contains(c.Id)).ToList();
            foreach (Specialty specialty in specialtyToBeAdded)
            {
                userDb.SpecialtiesActived.Add(specialty);
            }

            await _context.SaveChangesAsync();
            _context.Entry(userDb).State = EntityState.Detached;
        }
    }
}
