using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using practice_dotnet.Entities;
using practice_dotnet.Services.PostServices;

namespace practice_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _noteService;
        public PostController(IPostService noteService)
        {
            _noteService = noteService;
        }
    }
}
