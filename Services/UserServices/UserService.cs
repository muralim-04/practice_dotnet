using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using practice_dotnet.Data;
using practice_dotnet.DTOs;
using practice_dotnet.Entities;
using practice_dotnet.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace practice_dotnet.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public UserService(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Response<bool>> DeleteAccount(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return Response<bool>.Fail("User was not found");
            }

            var userLikes = _context.PostLikes.Where(l => l.UserId == id);
            _context.PostLikes.RemoveRange(userLikes);

            var userComments = _context.Comments.Where(c => c.UserId == id);
            _context.Comments.RemoveRange(userComments);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Response<bool>.Ok(true);
        }

        public async Task<Response<PagedResult<UserResDto>>> GetAllUsers(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var totalCount = await _context.Users.CountAsync();

            var users = await _context.Users
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserResDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();

            var data = new PagedResult<UserResDto>
            {
                Items = users,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            return Response<PagedResult<UserResDto>>.Ok(data);
        }

        public async Task<Response<UserResDto>> GetUser(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserResDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return Response<UserResDto>.Fail("User was not found");
            }

            return Response<UserResDto>.Ok(user);
        }

        public async Task<Response<bool>> MakeAdmin(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return Response<bool>.Fail("User doesn't exist");
            }
            if (user.IsAdmin)
            {
                return Response<bool>.Fail("User is already an admin");
            }

            user.IsAdmin = true;
            await _context.SaveChangesAsync();
            return Response<bool>.Ok(true);
        }

        public async Task<Response<UserResDto>> Register(UserReqDto user)
        {
            var userExist = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if(userExist != null)
            {
                return Response<UserResDto>.Fail("User already exist");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var newUser = new User
            {
                Email = user.Email,
                Password = hashedPassword,
                UserName = user.UserName
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            var token = GenerateToken(newUser);

            var response = new UserResDto
            {
                Id = newUser.Id,
                UserName = newUser.UserName,
                Email = newUser.Email,
                Token = token
            };

            return Response<UserResDto>.Ok(response);
        }

        public async Task<Response<UserResDto>> SignIn(SignInDto user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser == null)
            {
                return Response<UserResDto>.Fail("User with this email doesn't exist");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);
            if (!isPasswordValid)
            {
                return Response<UserResDto>.Fail("Incorrect password");
            }

            var token = GenerateToken(existingUser);
            var response = new UserResDto
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                Email = existingUser.Email,
                Token = token
            };
            return Response<UserResDto>.Ok(response);
        }

        public async Task<Response<bool>> UpdateUserPassword(int userId, UpdatePasswordDto dto)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                return Response<bool>.Fail("User not found");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, existingUser.Password);
            if (!isPasswordValid)
            {
                return Response<bool>.Fail("Incorrect password");
            }

            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();

            return Response<bool>.Ok(true);
        }

        public async Task<Response<UserResDto>> UpdateUserDeatail(int userId, UpdateUserDto dto)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                return Response<UserResDto>.Fail("User not found");
            }

            existingUser.UserName = dto.UserName;
            await _context.SaveChangesAsync();

            var updatedUser = new UserResDto
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                Email = existingUser.Email
            };
            return Response<UserResDto>.Ok(updatedUser);
        }
        public string GenerateToken(User user)
        {
            // 1. Define the Claims (Payload data)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            // 2. Turn your secret key string into a byte array & cryptographic key
            var secretKey = _config["JwtSettings:SecretKey"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // 3. Define the Signing Credentials (Algorithm + Key)
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Create the Token Object
            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2), // Expiration time
                signingCredentials: credentials      // This signs the token!
            );

            // 5. Serialize the token object into a compact string (Header.Payload.Signature)
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
