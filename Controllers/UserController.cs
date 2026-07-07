using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practice_dotnet.DTOs;
using practice_dotnet.Entities;
using practice_dotnet.Services;

namespace practice_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<bool> CreateUser([FromBody] UserReqDto user)
        {
            return true;
        }
    }
}
