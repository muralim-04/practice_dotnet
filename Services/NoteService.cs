using practice_dotnet.Data;
using practice_dotnet.Entities;
using Microsoft.EntityFrameworkCore;

namespace practice_dotnet.Services
{
    public class NoteService
    {
        private readonly DataContext _context;
        public NoteService(DataContext context)
        {
            _context = context;
        }

    }
}
