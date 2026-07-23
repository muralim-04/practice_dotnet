using practice_dotnet.DTOs;
using practice_dotnet.Helpers;

namespace practice_dotnet.Services.UserServices
{
    public interface IUserService
    {
        Task<Response<string>> Register(UserReqDto user);
        Task SignIn();
        Task SignOut();
        Task<Response<UserResDto>> GetUser(int id);
        Task UpdateUserDeatail();
        Task UpadateUserPassword();
        Task<Response<bool>> DeleteAccount(int id);

        //  ADMIN SECTION
        Task<Response<PagedResult<UserResDto>>> GetAllUsers(int pageNumber = 1, int pageSize = 10);
        Task<Response<bool>> MakeAdmin(int id);

    }
}
