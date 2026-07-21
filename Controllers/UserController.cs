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

        [HttpGet]
        public async Task<ActionResult<List<UserResDto>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();

            if (!result.Success)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    detail: result.Message
                );
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult<UserResDto>> CreateUser([FromBody] UserReqDto user)
        {
            var result = await _userService.AddUser(user);

            if(!result.Success)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    detail: result.Message
                );
            }

            return Ok(result.Data);           
        }

    }
}
