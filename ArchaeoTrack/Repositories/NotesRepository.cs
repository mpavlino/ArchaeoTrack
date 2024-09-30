using ArcheoTrack.DAL;
using ArcheoTrack.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArcheoTrack.Repositories {
    public class NotesRepository {
        private readonly AppDbContext _context;

        public NotesRepository( AppDbContext context ) {
            _context = context;
        }

        public async Task<List<Note>> GetNotesAsync() {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> GetNoteAsync( int id ) {
            return await _context.Notes.FindAsync( id );
        }

        public async Task AddNoteAsync( Note note ) {
            _context.Notes.Add( note );
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync( Note note ) {
            _context.Notes.Update( note );
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync( int id ) {
            var note = await _context.Notes.FindAsync( id );
            if( note != null ) {
                _context.Notes.Remove( note );
                await _context.SaveChangesAsync();
            }
        }

        public bool NoteExists( int id ) {
            return _context.Notes.Any( e => e.Id == id );
        }
    }
}
