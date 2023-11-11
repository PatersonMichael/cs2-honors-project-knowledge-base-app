using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Common.Exceptions;
using KB.Domain.Models;
using KB.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KB.Domain.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ILogger<NoteRepository> _logger;
        private readonly KnowledgeBaseAppContext _context;

        public NoteRepository(ILogger<NoteRepository> logger, KnowledgeBaseAppContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET
        public async Task<IEnumerable<Note>> GetNotesAsync()
        {
            _logger.LogInformation("Begin GetNotesAsync from NoteRepository");

            return await _context.Notes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Note> GetNoteAsync(int id)
        {
            _logger.LogInformation("Begin GetNoteAsync from NoteRepository");

            if (id == 0)
            {
                throw new BadRequestException("id is needed");
            }


            try
            {
                var note = await _context.Notes
                    .AsNoTracking()
                    .Include(x => x.Keywords)
                    .SingleAsync(x => x.NoteId == id);
                return note;

            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Note not found");
            }
        }

        // POST
        public async Task<Note> PostNoteAsync(Note note)
        {
            _logger.LogInformation("Begin PostNoteAsync from NoteRepository");

            _context.Notes.Update(note);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NoteExists(note.NoteId))
                {
                    throw new ConflictException("Note already exists");
                }

                throw;
            }

            return note;
        }

        // PUT
        public async Task<Note> PutNoteAsync(int id, Note note)
        {
            _logger.LogInformation("Begin PutNoteAsync from NoteRepository");

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(note.NoteId))
                {
                    throw new NotFoundException("Note not found");
                }

                throw;
            }

            return note;
        }

        // DELETE
        public async Task DeleteNoteAsync(int id)
        {
            _logger.LogInformation("Begin DeleteNoteAsync from NoteRepository");

            var note = await _context.Notes.FindAsync(id);

            if (note != null)
            {
                _context.Notes.Remove(note);
            }
            else
            {
                throw new NullEntityException("note is null");
            }
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(x => x.NoteId == id);
        }
    }
}
