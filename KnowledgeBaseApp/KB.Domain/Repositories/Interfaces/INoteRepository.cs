using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KB.Domain.Models;

namespace KB.Domain.Repositories.Interfaces
{
    public interface INoteRepository
    {
        // GET
        public Task<IEnumerable<Note>> GetNotesAsync();

        public Task<Note> GetNoteAsync(int id);

        // POST
        public Task<Note> PostNoteAsync(Note note);

        // PUT
        public Task<Note> PutNoteAsync(int id,  Note note);

        // DELETE
        public Task DeleteNoteAsync(int id);
    }
}
