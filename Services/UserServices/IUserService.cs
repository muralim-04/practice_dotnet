using practice_dotnet.DTOs;
using practice_dotnet.Entities;
using practice_dotnet.Helpers;

namespace practice_dotnet.Services.UserServices
{
    public interface IUserService
    {
        Task<Response<User>> Register(UserReqDto user);
        Task<Response<User>> SignIn(UserReqDto user);
        Task<Response<UserResDto>> GetUser(int id);
        Task UpdateUserDeatail();
        Task<Response<bool>> UpadateUserPassword(int userId, UpdatePasswordDto dto);
        Task<Response<bool>> DeleteAccount(int id);
        string GenerateToken(User user);

        //  ADMIN SECTION
        Task<Response<PagedResult<UserResDto>>> GetAllUsers(int pageNumber = 1, int pageSize = 10);
        Task<Response<bool>> MakeAdmin(int id);

    }
}
