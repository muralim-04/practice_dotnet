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

        public async Task<Note> GetUserNote(int userId, int noteId)
        {
            var note = await _context.Notes.Where(n => n.User.Id == userId && n.Id == noteId).FirstOrDefaultAsync();
            return note;
        }

        public async Task<List<Note>> GetAllUserNotes(int userId)
        {
            var notes = await _context.Notes.Where(n => n.User.Id == userId).ToListAsync();
            return notes;
        }

    }
}
