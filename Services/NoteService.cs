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
        public async Task<bool> AddNote(Note note)
        {
            _context.Notes.Add(note);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteNote(int noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if (note == null) return false;
            _context.Notes.Remove(note);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateNote(int noteId, Note updatedFields)
        {
            var existingNote = await _context.Notes.FindAsync(noteId);
            if (existingNote == null) return false;
            existingNote.Title = updatedFields.Title;
            existingNote.Description = updatedFields.Description;
            return await _context.SaveChangesAsync() > 0;
        }
        public async 
    }
}
