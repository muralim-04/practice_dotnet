using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using practice_dotnet.DTOs;
using practice_dotnet.Entities;
using practice_dotnet.Services.UserServices;

namespace practice_dotnet.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResDto>> Register([FromBody] UserReqDto user)
        {
            var response = await _userService.Register(user);

            if (!response.Success)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    detail: response.Message
                );
            }

            return Ok(response.Data);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResDto>> LogIn([FromBody] LogInDto user)
        {
            var response = await _userService.LogIn(user);

            if (!response.Success)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    detail: response.Message
                );
            }

            return Ok(response.Data);
        }

    }
}
