using Microsoft.EntityFrameworkCore;
using practice_dotnet.Data;
using practice_dotnet.DTOs;
using practice_dotnet.Entities;

namespace practice_dotnet.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }


        public async Task<Response<List<UserResDto>>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                var dto = users.Select(u => new UserResDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                }).ToList();

                return Response<List<UserResDto>>.Ok(dto);     
            }
            catch
            {
                return Response<List<UserResDto>>.Fail("Unexpected error occured while retrieving the users");
            }
        }
        public async Task<User> GetUserById(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
        }
        public async Task<Response<UserResDto>> AddUser(UserReqDto user)
        {
            // Checking if user already exists
            var existingUser = await _context.Users.AnyAsync(u => u.Email == user.Email);

            //if user exists send an error message
            if (existingUser)
            {
                return Response<UserResDto>.Fail("User already exists"); 
            }
            // creating a new user
            var userEntity = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
            //saving the user in the database
            try
            {
                _context.Users.Add(userEntity);
                await _context.SaveChangesAsync();
                var dto = new UserResDto 
                {
                    Id = userEntity.Id,
                    Name = userEntity.Name,
                    Email = userEntity.Email
                };
                return Response<UserResDto>.Ok(dto);
            }
            catch
            {
                return Response<UserResDto>.Fail("Unexpected error occured while saving the user");
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateUser(int id, User updatedFields)
        {
            
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return false;

          
            existingUser.Name = updatedFields.Name;
            existingUser.Email = updatedFields.Email;

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> MakeUserAdmin(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            user.IsAdmin = true;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
