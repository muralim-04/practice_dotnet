using Microsoft.EntityFrameworkCore;
using practice_dotnet.Data;
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


        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task<User> GetUserById(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
        }
        public async Task<bool> AddUser(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
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
