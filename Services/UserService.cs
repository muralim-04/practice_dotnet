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

            var users = await _context.Users.ToListAsync();
            var dto = users.Select(u => new UserResDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();

            return Response<List<UserResDto>>.Ok(dto);     
            
        }
        public async Task<Response<UserResDto>> GetUserById(int userId)
        {
            // Checking if user already exists
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (userEntity == null)
            {
                return Response<UserResDto>.Fail("User doesn't exist");
            }
            var user = new UserResDto
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email,
            };

            return Response<UserResDto>.Ok(user);
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
        public async Task<Response<bool>> DeleteUser(int userId)
        { 
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return Response<bool>.Fail("User doesn't exist");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Response<bool>.Ok(true);
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
