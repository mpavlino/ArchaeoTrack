﻿using ArcheoTrack.DAL;
using ArcheoTrack.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArcheoTrack.API.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class NotesController : ControllerBase {
        private readonly AppDbContext _context;

        public NotesController( AppDbContext context ) {
            _context = context;
        }

        // GET: api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes() {
            return await _context.Notes.ToListAsync();
        }

        // GET: api/notes/{id}
        [HttpGet( "{id}" )]
        public async Task<ActionResult<Note>> GetNote( int id ) {
            var note = await _context.Notes.FindAsync( id );
            if( note == null ) {
                return NotFound();
            }
            return note;
        }

        // POST: api/notes
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote( Note note ) {
            _context.Notes.Add( note );
            await _context.SaveChangesAsync();

            return CreatedAtAction( "GetNote", new { id = note.Id }, note );
        }

        // PUT: api/notes/{id}
        [HttpPut( "{id}" )]
        public async Task<IActionResult> PutNote( int id, Note note ) {
            if( id != note.Id ) {
                return BadRequest();
            }

            _context.Entry( note ).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch( DbUpdateConcurrencyException ) {
                if( !NoteExists( id ) ) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/notes/{id}
        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteNote( int id ) {
            var note = await _context.Notes.FindAsync( id );
            if( note == null ) {
                return NotFound();
            }

            _context.Notes.Remove( note );
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists( int id ) {
            return _context.Notes.Any( e => e.Id == id );
        }
    }
}
