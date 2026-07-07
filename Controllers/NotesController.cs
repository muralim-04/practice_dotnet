using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using practice_dotnet.Entities;
using practice_dotnet.Services;

namespace practice_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NoteService _noteService;
        public NotesController(NoteService noteService)
        {
            _noteService = noteService;
        }
    }
}
