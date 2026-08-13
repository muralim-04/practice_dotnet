using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using practice_dotnet.DTOs;
using practice_dotnet.Entities;
using practice_dotnet.Helpers;
using practice_dotnet.Services.PostServices;
using System.Security.Claims;

namespace practice_dotnet.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost("createPost")]
        public async Task<ActionResult<PostResDto>> CreatePost([FromForm] PostReqDto post)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var response = await _postService.CreatePost(post, userId);

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

        [HttpGet("getAllPosts")]
        public async Task<ActionResult<PagedResult<PostResDto>>> GetAllPosts(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _postService.GetAllPosts(pageNumber, pageSize);

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

        [Authorize]
        [HttpDelete("deletePost")]
        public async Task<ActionResult<bool>> DeletePost(int postId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var response = await _postService.DeletePost(postId, userId);

            if (!response.Success)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    detail: response.Message
                );
            }

            return Ok(true);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletePostAdmin")]
        public async Task<ActionResult<bool>> DeletePostAdmin(int postId)
        { 
            var response = await _postService.DeletePostAdmin(postId);

            if (!response.Success)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Bad Request",
                    detail: response.Message
                );
            }

            return Ok(true);
        }
    }
}
